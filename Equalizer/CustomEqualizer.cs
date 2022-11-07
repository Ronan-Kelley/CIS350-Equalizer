using NAudio.Dsp;
using NAudio.Wave;
using System.ComponentModel;

public class CustomEqualizer : ISampleProvider
{
	private readonly ISampleProvider _sourceProvider;
	private readonly EqualizerBand[] _bands;
	private readonly BiQuadFilter[,] _filters;
	/// <summary>
	/// number of sound channels to manage
	/// </summary>
	private readonly int _channels;
	/// <summary>
	/// number of bands to apply
	/// </summary>
	private readonly int _bandCount;
	private bool _updated;

    public WaveFormat WaveFormat => _sourceProvider.WaveFormat;

    public CustomEqualizer(ISampleProvider sourceProvider, EqualizerBand[] bands)
	{
		_sourceProvider = sourceProvider;
		_bands = bands;
		_channels = sourceProvider.WaveFormat.Channels;
		_bandCount = bands.Length;
		_filters = new BiQuadFilter[_channels, _bandCount];
	}

	private void CreateFilters()
	{
		for (int bandIndex = 0; bandIndex < _bandCount; bandIndex++)
		{
			var band = _bands[bandIndex];
			for (int n = 0; n < _channels; n++)
			{
				if (_filters[n, bandIndex] == null)
				{
					_filters[n, bandIndex] = BiQuadFilter.PeakingEQ(_sourceProvider.WaveFormat.SampleRate, band.Frequency, band.Bandwidth, band.Gain);
				} else
				{
					_filters[n, bandIndex].SetPeakingEq(_sourceProvider.WaveFormat.SampleRate, band.Frequency, band.Bandwidth, band.Gain);
				}
			}
		}
	}

	public void Update()
	{
		_updated = true;
		CreateFilters();
	}

	public int Read(float[] buffer, int offset, int count)
	{
		int samplesRead = _sourceProvider.Read(buffer, offset, count);

		if (_updated)
		{
			CreateFilters();
			_updated = false;
		}

		for (int n = 0; n < samplesRead; n++)
		{
			int ch = n % _channels;

			for (int band = 0; band < _bandCount; band++)
			{
				buffer[offset + n] = _filters[ch, band].Transform(buffer[offset + n]);
			}
		}

		return samplesRead;
	}
}

public struct EqualizerBand
{
	public float Frequency { get; set; }
	public float Gain { get; set; }
	public float Bandwidth { get; set; }
}