using NAudio.Wave;

namespace Equalizer
{
    /// <summary>
    /// backend for media player elements
    /// 
    /// designed to integrate with NonLiveEq and
    /// EqualizerOptions.
    /// </summary>
    internal class MediaPlayer
    {
        // player objects
        private AudioFileReader? _audioFile;
        private NonLiveEq _equalizer;
        private WaveOutEvent? _outputDevice;

        // path of the currently loaded file
        private string? _curFilePath;

        /// <summary>
        /// create a new MediaPlayer object
        /// </summary>
        public MediaPlayer() {
            _audioFile = null;
            _outputDevice = null;
            _equalizer = new NonLiveEq();
        }

        /***************************************************
         * Function to load initial audio data from file
         **************************************************/

        /// <summary>
        /// load an audio file based on its path.
        /// 
        /// error checking is done for the file's path, but not the file's type.
        /// </summary>
        /// <param name="filePath">path of file to load</param>
        /// <returns>true on success, false otherwise</returns>
        public bool LoadFile(string filePath) {
            // ensure file isn't already loaded
            if (filePath == _curFilePath) {
                return true;
            }

            if (!File.Exists(filePath)) {
                return false;
            }

            _audioFile = new(filePath);
            _curFilePath = filePath;

            _equalizer.SetSource(_audioFile);

            _outputDevice ??= new();

            _outputDevice.Volume = 1;

            _outputDevice.Init(_equalizer);
            return true;
        }

        /***************************************************
         * Functions to modify audio equalizer / filters
         **************************************************/

        /// <summary>
        /// enable the equalizer
        /// 
        /// has no effect if the equalizer is already enabled
        /// </summary>
        public void EnableEqualizer() {
            _equalizer.EnableFilter();
        }

        /// <summary>
        /// disable the equalizer, leaving regular playback unaffected.
        /// 
        /// has no effect if the equalizer is already disabled.
        /// </summary>
        public void DisableEqualizer() {
            _equalizer.DisableFilter();
        }

        /// <summary>
        /// update the equalizer settings based on node data
        /// </summary>
        /// <param name="data"></param>
        public void UpdateEqualizer(NodeData data) {
            // This is when a node was deleted
            if (data.IsDeleted()) {
                _equalizer.RemoveFilter(data.GetIndex());
                return;
            }

            // This case occurs when a new node was created
            if (data.GetIndex() == _equalizer.GetNumOfFilters()) {
                _equalizer.AddFilter(data.GetFreq(), data.GetQ(), data.GetGain());
                return;
            }

            // This is when an existing node is changed
            if (data.GetIndex() < _equalizer.GetNumOfFilters()) {
                _equalizer.SetFilter(data.GetIndex(), data.GetFreq(), data.GetQ(), data.GetGain());
                return;
            }
        }

        /***************************************************
         * Functions to control and check audio playback
         **************************************************/

        /// <summary>
        /// resume or start playback of audio assets
        /// </summary>
        public void Play() {
            if (_outputDevice != null) {
                _outputDevice.Play();
            }
        }

        /// <summary>
        /// pause playback, keeping audio assets loaded
        /// </summary>
        public void Pause() {
            if (_outputDevice != null) {
                _outputDevice.Pause();
            }
        }

        /// <summary>
        /// stop playing and unload audio assets
        /// </summary>
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

        /// <summary>
        /// set the volume for the player as a percentage
        /// </summary>
        /// <param name="volume">percent volume 0-100</param>
        public void SetVolumePercentage(float volume) {
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

        /// <summary>
        /// set the position of the track in terms of percent completion
        /// </summary>
        /// <param name="percentDone">what percent completion to set playback to; 0 to 100</param>
        public void SetPositionPercent(float percentDone) {
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

