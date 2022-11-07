using NAudio;
using NAudio.Dsp;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using NAudio.CoreAudioApi;

public class RealTimeEq
{
	private WasapiLoopbackCapture _waveIn;
	private BufferedWaveProvider _bufWaveProvider;
	private VolumeSampleProvider _volumeProvider;
	private WasapiOut _wasapiOut;
	private BiQuadFilter _filter;
	public RealTimeEq()
	{
		_waveIn = new WasapiLoopbackCapture();
		_bufWaveProvider = new BufferedWaveProvider(_waveIn.WaveFormat);
		_volumeProvider = new VolumeSampleProvider(_bufWaveProvider.ToSampleProvider());
		_wasapiOut = new WasapiOut(AudioClientShareMode.Shared, 0);
		_filter = BiQuadFilter.HighPassFilter(44000, 200, 1);
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
            _bufWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
            _volumeProvider.Volume = .8f;
        };
    }
}
