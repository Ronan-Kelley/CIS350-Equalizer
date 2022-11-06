using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Equalizer
{
    public partial class _form_eq : Form
    {
        // I don't know what file types are actually supported,
        // but this gives us an easy way to change what files can
        // be selected later on down the line
        private string[] _filetypes = { "mp3", "wav", "ogg" };

        // constants for media button text; pause looks weird right now
        private const string _mediaPlay = "\u25B6";
        private const string _mediaPause = "||";

        // player objects
        private WaveOutEvent? outputDevice;
        private AudioFileReader? audioFile;

        public _form_eq()
        {
            InitializeComponent();

            //
            // set up multimedia buttons
            //

            _btn_playpause.Text = _mediaPlay;

            //
            // set up file picker filters
            //

            // clear the file dialog filter
            _musicFileDialog.Filter = "";

            // set up the new filter based on the entries in _filetypes
            string _filterText = "Audio Files (";
            string _filterFilter = "";
            foreach (var filetype in _filetypes)
            {
                _filterText += $"*.{filetype},";
                _filterFilter += $"*.{filetype};";
            }

            // fix the end of both strings before concatenating them together into one
            _filterText = _filterText.TrimEnd(',') + ")";
            _filterFilter = _filterFilter.TrimEnd(';');

            // assign the filter to the file picker dialog
            _musicFileDialog.Filter  = _filterText + "|" + _filterFilter;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // open the file picker dialog when browse is clicked
            _musicFileDialog.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // display the selected file's path
            _txt_fileName.Text = _musicFileDialog.FileName;
            // reset the audio player objects
            if (outputDevice != null)
            {
                outputDevice.Dispose();
            }
            outputDevice = null;
            if (audioFile != null)
            {
                audioFile.Dispose();
            }
            audioFile = null;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (_btn_playpause.Text == _mediaPlay)
            {
                // play
                _btn_playpause.Text = _mediaPause;

                // create the output device if it is not already created
                if (outputDevice == null)
                {
                    // create the object
                    outputDevice = new WaveOutEvent();
                    // set up a handler for the PlaybackStopped event (defined below)
                    outputDevice.PlaybackStopped += OnPlaybackStopped;
                }
                if (audioFile == null)
                {
                    audioFile = new AudioFileReader(_txt_fileName.Text);
                    outputDevice.Init(audioFile);
                }
                outputDevice.Play();
            } else
            {
                // pause
                _btn_playpause.Text = _mediaPlay;

                outputDevice?.Stop();
            }
        }

        private void OnPlaybackStopped(object? sender, StoppedEventArgs e)
        {
            // realistically, these checks for null aren't required
            // in a single-threaded application such as this, but
            // it makes the linter happy.
            if (outputDevice != null)
            {
                outputDevice.Dispose();
            }
            outputDevice = null;
            if (audioFile != null)
            {
                audioFile.Dispose();
            }
            audioFile = null;
        }
    }
}