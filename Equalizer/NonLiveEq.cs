using NAudio.CoreAudioApi;
using NAudio.Dsp;
using NAudio.Wave;
using System.DirectoryServices.ActiveDirectory;
using System;
using NAudio.MediaFoundation;
using System.Security.Cryptography.X509Certificates;
using NAudio.Wave.SampleProviders;

namespace Equalizer
{
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

        public NonLiveEq() {
            // Equalizer variables setup 
            _filters = new BiQuadFilter[10];
            _enabled = false;
            _numOfFilters = 0;

            _waveFormat = new WasapiOut().OutputWaveFormat;
        }

        public void SetSource(ISampleProvider newSourceProvider) {
            _sourceProvider = newSourceProvider;
            _waveFormat = _sourceProvider.WaveFormat;
        }

        public int Read(float[] buffer, int offset, int count) {
            // TODO I have no idea if this is a good idea
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

        public void AddFilter(float centerFreq, float q, float dbGain) {
            if (_numOfFilters == 10) {
                return;
            }
            _filters[_numOfFilters] = BiQuadFilter.PeakingEQ(_waveFormat.SampleRate, centerFreq, q, dbGain);
            _numOfFilters++;
        }

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

        public int GetNumOfFilters() {
            return _numOfFilters;
        }
    }
}

