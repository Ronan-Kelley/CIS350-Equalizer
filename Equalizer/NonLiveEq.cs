using NAudio.Dsp;
using NAudio.Wave;

namespace Equalizer
{
    /// <summary>
    /// An equalizer targeting prerecorded audio
    /// 
    /// changes are applied realtime in playback, contrary to the name, but cannot be
    /// applied to system audio as a live equalizer may be expected to do.
    /// </summary>
    internal class NonLiveEq : ISampleProvider
    {
        private ISampleProvider _sourceProvider;
        private WaveFormat _waveFormat;

        // Filter variables
        private BiQuadFilter[] _filters;
        private bool _enabled;
        private int _numOfFilters;

        // TODO this could throw error if _sourceProvider is null
        WaveFormat ISampleProvider.WaveFormat => _waveFormat;

        /// <summary>
        /// create a new NonLiveEq object
        /// </summary>
        public NonLiveEq() {
            // Equalizer variables setup 
            _filters = new BiQuadFilter[10];
            _enabled = false;
            _numOfFilters = 0;

            _waveFormat = new WasapiOut().OutputWaveFormat;
        }

        /// <summary>
        /// set the equalizer's source; generally an audio file or waveout object
        /// </summary>
        /// <param name="newSourceProvider">new audio source</param>
        public void SetSource(ISampleProvider newSourceProvider) {
            _sourceProvider = newSourceProvider;
            _waveFormat = _sourceProvider.WaveFormat;
        }

        /// <summary>
        /// Stream producer from ISampleProvider inheritance
        /// </summary>
        /// <param name="buffer">buffer to read into</param>
        /// <param name="offset">offset from start of buffer to begin reading into</param>
        /// <param name="count">maximum number of bytes to read</param>
        /// <returns>number of bytes read</returns>
        public int Read(float[] buffer, int offset, int count) {
            if (_sourceProvider == null) {
                return 0;
            }

            int samplesRead = _sourceProvider.Read(buffer, offset, count);

            if (!_enabled) {
                return samplesRead;
            }

            for (int i = 0; i < samplesRead; i++) {
                for (int filterIndex = 0; filterIndex < _numOfFilters; filterIndex++) {
                    buffer[offset + i] = _filters[filterIndex].Transform(buffer[offset + i]);
                }
            }

            return samplesRead;
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
        public void SetFilter(int index, float centerFreq, float q, float dbGain) {
            if (index < 0 || index >= _numOfFilters) {
                return; //TODO error handeling?
            }
            _filters[index].SetPeakingEq(_waveFormat.SampleRate, centerFreq, q, dbGain);
        }

        /// <summary>
        /// add a filter to the equalizer
        /// 
        /// returns immediately without error or action if the number
        /// of filters is already 10.
        /// </summary>
        /// <param name="centerFreq">the center frequency to apply the filter to</param>
        /// <param name="q">the Q coefficient for the filter</param>
        /// <param name="dbGain">the gain to apply in decibels</param>
        public void AddFilter(float centerFreq, float q, float dbGain) {
            if (_numOfFilters == 10) {
                return;
            }
            _filters[_numOfFilters] = BiQuadFilter.PeakingEQ(_waveFormat.SampleRate, centerFreq, q, dbGain);
            _numOfFilters++;
        }

        /// <summary>
        /// remove a filter from the array at the given index
        /// 
        /// if the passed index is out of bounds, this function
        /// returns immediately without error or action
        /// </summary>
        /// <param name="index">index to remove</param>
        public void RemoveFilter(int index) {
            if (index < 0 || index >= _numOfFilters) {
                return;
            }

            // Shift all other filters in array
            while (index < _numOfFilters - 1) {
                _filters[index] = _filters[index + 1];
                index++;
            }

            _numOfFilters--;
        }

        /// <summary>
        /// Enables the captured audio to be altered based on filter
        /// before being played back.
        /// </summary>
        public void EnableFilter() {
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
        /// returns the number of active filters being applied
        /// </summary>
        /// <returns></returns>
        public int GetNumOfFilters() {
            return _numOfFilters;
        }
    }
}

