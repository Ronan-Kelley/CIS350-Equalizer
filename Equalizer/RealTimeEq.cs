using NAudio.CoreAudioApi;
using NAudio.Dsp;
using NAudio.Wave;

/// <summary>
/// Captures, alters, and plays back system audio.
/// </summary>
public class RealTimeEq
{
	private WasapiLoopbackCapture _waveIn;
	private BufferedWaveProvider _bufWaveProvider;
	private WasapiOut _wasapiOut;
	private BiQuadFilter _filter;

	// Filter variables
	private bool _enabled;
	private float _gain;
	private float _centerFreq;
	private float _q;

	private MMDeviceEnumerator _devicesEnumerator;
	private MMDevice[] _devices;

	public RealTimeEq() {
		// Temporarily use 2 devices to seperate audio to avoid feedback loops
		_devicesEnumerator = new();
		_devices = _devicesEnumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active).ToArray();

		// Variables setup 
		_enabled = false;
		_gain = 20;
		_centerFreq = 1000;
		_q = 1;

		// Audio capture, filter, and playback setup
		_waveIn = new(_devices[0]);
		_bufWaveProvider = new(_waveIn.WaveFormat);
		_filter = BiQuadFilter.PeakingEQ(_waveIn.WaveFormat.SampleRate, _centerFreq, _q, _gain);
		_wasapiOut = new(_devices[1], AudioClientShareMode.Shared, true, 0);
		_waveIn.DataAvailable += delegate (object? sender, WaveInEventArgs e) {
			if (_enabled) {
				for (int i = 0; i < e.BytesRecorded; i += 4) {
					byte[] transformed = BitConverter.GetBytes(_filter.Transform(BitConverter.ToSingle(e.Buffer, i)));
					Buffer.BlockCopy(transformed, 0, e.Buffer, i, 4);
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
	public void SetFilter(float centerFreq, float q, float dbGain) {
		_centerFreq = centerFreq;
		_q = q;
		_gain = dbGain;
		_filter.SetPeakingEq(_waveIn.WaveFormat.SampleRate, _centerFreq, _q, _gain);
	}

	public float GetGain() {
		return _gain;
	}

	public float GetQ() {
		return _q;
	}

	public float GetCenterFreq() {
		return _centerFreq;
	}
}
