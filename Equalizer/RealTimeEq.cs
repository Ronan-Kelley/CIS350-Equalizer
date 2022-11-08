using NAudio;
using NAudio.Dsp;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using NAudio.CoreAudioApi;
using System.Diagnostics;
using System.Net;

public class RealTimeEq
{
	private WasapiLoopbackCapture _waveIn;
	private BufferedWaveProvider _bufWaveProvider;
	private VolumeSampleProvider _volumeProvider;
	private WasapiOut _wasapiOut;
	private BiQuadFilter _filter;

	// Temporary solution to test EQ
	private WaveFileWriter _writer;
	private string _savePath;

	// Temporary solution to fix feedback
	private MMDeviceEnumerator _devicesEnumerator;
	private MMDevice[] _devices;

	public RealTimeEq()
	{
		// Testing separate outputs
		_devicesEnumerator = new MMDeviceEnumerator();
		_devices = _devicesEnumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active).ToArray();

		_waveIn = new WasapiLoopbackCapture(_devices[0]);
		_bufWaveProvider = new BufferedWaveProvider(_waveIn.WaveFormat);
		_volumeProvider = new VolumeSampleProvider(_bufWaveProvider.ToSampleProvider());
		_wasapiOut = new WasapiOut(_devices[1], AudioClientShareMode.Shared, true, 0);
		_filter = BiQuadFilter.PeakingEQ(_waveIn.WaveFormat.SampleRate, 4000, .2f, 20);

        // Outputs recordingWithEQ.wav in desktop folder
        var outputFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "NAudio");
		Directory.CreateDirectory(outputFolder);
		_savePath = Path.Combine(outputFolder, "recordingWithEQ.wav");
		_writer = new WaveFileWriter(_savePath, _waveIn.WaveFormat);
	}

	public void doThings()
	{
		_wasapiOut.Init(_volumeProvider);
		_wasapiOut.Play();
		_waveIn.StartRecording();

		_waveIn.DataAvailable += delegate (object? sender, WaveInEventArgs e)
        {		
			for (int i = 0; i < e.BytesRecorded; i += 4)
			{
				byte[] transformed = BitConverter.GetBytes(_filter.Transform(BitConverter.ToSingle(e.Buffer, i)));
				Buffer.BlockCopy(transformed, 0, e.Buffer, i, 4);
			}

			/*
			// Write filtered audio to desktop folder file (records ~10 seconds)
			_writer.Write(e.Buffer, 0, e.BytesRecorded);
			if(_writer.Position > _waveIn.WaveFormat.AverageBytesPerSecond*10) {
				_waveIn.StopRecording();
				_writer.Dispose();
			}
			*/

			_bufWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
            _volumeProvider.Volume = .8f;
        };
    }

	public void setFilter(float centerFreq, float q, float dbGain) {
		_filter.SetPeakingEq(_waveIn.WaveFormat.SampleRate, centerFreq, q, dbGain);
	}
}
