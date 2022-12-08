using NAudio.Wave;

namespace Equalizer
{
    internal class MediaPlayer
    {
        // player objects
        private AudioFileReader? _audioFile;
        private NonLiveEq _equalizer;
        private WasapiOut? _outputDevice;

        // path of the currently loaded file
        private string? _curFilePath;

        public MediaPlayer() {
            _audioFile = null;
            _outputDevice = new WasapiOut();
            _equalizer = new NonLiveEq(_outputDevice.OutputWaveFormat);

            _outputDevice.Volume = 1;
        }

        /***************************************************
         * Function to load initial audio data from file
         **************************************************/

        public bool LoadFile(string filePath) {
            // ensure file isn't already loaded
            if (filePath == _curFilePath) {
                return true;
            }

            if (!File.Exists(filePath)) {
                return false;
            }
            
            _audioFile = new AudioFileReader(filePath);
            _curFilePath = filePath;

            _equalizer.SetSource(_audioFile);

            if (_outputDevice == null) {
                _outputDevice = new WasapiOut();
            }

            _outputDevice.Init(_equalizer);
            return true;
        }

        /***************************************************
         * Functions to modify audio equalizer / filters
         **************************************************/

        public void EnableEqualizer() {
            _equalizer.EnableFilter();
        }

        public void DisableEqualizer() {
            _equalizer.DisableFilter();
        }

        public void UpdateEqualizer(NodeData data) {
            // TODO this could potentially have bad errors,
            // errors shouldn't be normally possible though,
            // so for now it should be fine

            // This case occurs when a new node was created
            if (data.GetIndex() == _equalizer.GetNumOfFilters()) {
                _equalizer.AddFilter(data.GetFreq(), data.GetQ(), data.GetGain());
            }

            // This is when an existing node is changed
            if (data.GetIndex() < _equalizer.GetNumOfFilters()) {
                _equalizer.SetFilter(data.GetIndex(), data.GetFreq(), data.GetQ(), data.GetGain());
            }

            // Any other case is ignored, which could case awful errors, but hey, who cares???
        }

        /***************************************************
         * Functions to control and check audio playback
         **************************************************/

        public void Play() {
            if (_outputDevice != null) {
                // if song is over, restart it
                if (_audioFile != null && _audioFile.Position >= _audioFile.Length) {
                    _audioFile.Position = 0;
                }
                _outputDevice.Play();
            }
        }

        public void Pause() {
            if (_outputDevice != null) {
                _outputDevice.Pause();
            }
        }

        public void Stop() {
            if (_outputDevice != null) {
                _outputDevice.Dispose();
            }
            _outputDevice = null;
            if (_audioFile != null) {
                _audioFile.Dispose();
            }
            _audioFile = null;
            _curFilePath = null;
        }

        public void setVolumePercentage(float volume) {
            // clamp passed value
            if (volume > 100f) {
                volume = 100f;
            } else if (volume < 0f) {
                volume = 0f;
            }

            // set volume
            if (_outputDevice != null) {
                _outputDevice.Volume = volume / 100f;
            }
        }

        public void setPositionPercent(float percentDone) {
            // clamp percentDone
            if (percentDone > 100f) {
                percentDone = 100f;
            } else if (percentDone < 0f) {
                percentDone = 0f;
            }

            // assign the value
            if (_audioFile != null) {
                _audioFile.Position = (long)(_audioFile.Length / 100 * (percentDone));
            }
        }
    }
}

