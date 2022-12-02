using NAudio.CoreAudioApi;
using NAudio.Dsp;
using NAudio.Wave;
using System.DirectoryServices.ActiveDirectory;
using System;
using NAudio.MediaFoundation;

/// <summary>
/// Captures, alters, and plays back system audio.
/// </summary>
public class RealTimeEq
{
	private WasapiLoopbackCapture _waveIn;
	private BufferedWaveProvider _bufWaveProvider;
	private WasapiOut _wasapiOut;

    // Filter variables
    private BiQuadFilter[] _filters;
    private bool _enabled;
    private int _numOfFilters;

    private MMDeviceEnumerator _devicesEnumerator;
	private MMDevice[] _devices;

	public RealTimeEq() {
		// Temporarily use 2 devices to seperate audio to avoid feedback loops
		_devicesEnumerator = new();
		_devices = _devicesEnumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active).ToArray();

		// Variables setup 
		_enabled = false;
		_numOfFilters = 0;

		// Audio capture, filter, and playback setup
		_waveIn = new(_devices[0]);
		_bufWaveProvider = new(_waveIn.WaveFormat);
		_filters = new BiQuadFilter[10];
		_wasapiOut = new(_devices[1], AudioClientShareMode.Shared, true, 0);
		_waveIn.DataAvailable += delegate (object? sender, WaveInEventArgs e) {
			if (_enabled) {
				for (int i = 0; i < e.BytesRecorded; i += 4) {
					foreach (BiQuadFilter filter in _filters)
					{
                        byte[] transformed = BitConverter.GetBytes(filter.Transform(BitConverter.ToSingle(e.Buffer, i)));
                        Buffer.BlockCopy(transformed, 0, e.Buffer, i, 4);
                    }
				}
			}

			_bufWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
		};

		// Start audio playback on 2nd device
		_waveIn.StartRecording();
		_wasapiOut.Init(_bufWaveProvider);
		_wasapiOut.Play();
	}

	/// <summary>
	/// Enables the captured audio to be altered based on filter
	/// before being played back.
	/// </summary>
	public void EnableFilter() {
		// Right now this only works with 2 audio devices (or more)
		if (_devices.Length < 2) {
			return;
		}

		_enabled = true;
	}

	/// <summary>
	/// Stops the captured audio from being altered before
	/// being played back.
	/// </summary>
	public void DisableFilter() {
		_enabled = false;
	}

	/// <summary>
	/// Sets one band of audio equalization at the given params. 
	/// </summary>
	/// <param name="centerFreq">
	/// Center frequency for the filter
	/// </param>
	/// <param name="q">
	/// The range around the center frequency. (A high <c>q</c>
	/// value only affects freqencies very close to it).
	/// </param>
	/// <param name="dbGain">
	/// The maximum decibal gain at the center frequency 
	/// </param>
	public void SetFilter(int index, float centerFreq, float q, float dbGain)
	{
		if (index < 0 || index >= _numOfFilters)
		{
			return; //TODO error handeling?
		}
		_filters[index].SetPeakingEq(_waveIn.WaveFormat.SampleRate, centerFreq, q, dbGain);
	}

	public void AddFilter(float centerFreq, float q, float dbGain)
	{
		if (_numOfFilters == 10)
		{
			return;
		}
		_filters[_numOfFilters].SetPeakingEq(_waveIn.WaveFormat.SampleRate, centerFreq, q, dbGain);
		_numOfFilters++;
	}

    public void RemoveFilter(int index)
	{
		if (index < 0 || index > _numOfFilters)
		{
			return; //TODO error handeling?
		}

		// Shift all other filters in array
		while (index < _numOfFilters - 1)
		{
			_filters[index] = _filters[index + 1];
			index++;
		}
		_numOfFilters--;
	}
}
