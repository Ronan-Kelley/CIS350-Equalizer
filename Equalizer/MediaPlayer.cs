using NAudio.Wave;

namespace Equalizer
{
    internal class MediaPlayer
    {
        // player objects
        private AudioFileReader? _audioFile;
        private NonLiveEq? _equalizer;
        private WaveOutEvent? _outputDevice;

        // path of the currently loaded file
        private string? _curFilePath;

        public MediaPlayer() {
            _audioFile = null;
            _equalizer = null;
            _outputDevice = null;
        }

        public bool LoadFile(string filePath) {
            // ensure file isn't already loaded
            if (filePath == _curFilePath) {
                return true;
            }

            // initialize the output device
            if (_outputDevice == null) {
                // create the waveout event
                _outputDevice = new WaveOutEvent();
                // set up a handler for the PlaybackStopped event (defined below)
                _outputDevice.PlaybackStopped += OnPlaybackStopped;
                _outputDevice.Volume = 1;
            }
            // initialize the audio file if the path exists
            if (File.Exists(filePath)) {
                _audioFile = new AudioFileReader(filePath);
                _curFilePath = filePath;
                if (_equalizer == null) {
                    _equalizer = new NonLiveEq(_audioFile);
                } else {
                    _equalizer.SetSource(_audioFile);
                }
                _outputDevice.Init(_equalizer);
            } else {
                Stop();
                return false;
            }

            return true;
        }

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

        public bool isReady() {
            return (_outputDevice != null && _audioFile != null);
        }

        private void OnPlaybackStopped(object? sender, StoppedEventArgs e) {
        }
    }
}

