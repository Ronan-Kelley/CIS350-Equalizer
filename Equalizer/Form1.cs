
using NAudio.Wave;

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

        // Audio playback and altering
        private MediaPlayer _mediaPlayer;
        private NonLiveEq _eq;

        public _form_eq() {
            InitializeComponent();

            /***********************************************
             * Set up multimedia components
             **********************************************/

            // play/pause button
            _btn_playpause.Text = _mediaPlay;

            // volume slider
            _tb_volume.Minimum = 0;
            _tb_volume.Maximum = 100;
            _tb_volume.Value = 100;
            _tb_volume.TickFrequency = 10;

            /***********************************************
             * Set up file picker filters
             **********************************************/
            
            // clear the file dialog filter
            _musicFileDialog.Filter = "";

            // set up the new filter based on the entries in _filetypes
            string _filterText = "Audio Files (";
            string _filterFilter = "";
            foreach (var filetype in _filetypes) {
                _filterText += $"*.{filetype},";
                _filterFilter += $"*.{filetype};";
            }

            // fix the end of both strings before concatenating them together into one
            _filterText = _filterText.TrimEnd(',') + ")";
            _filterFilter = _filterFilter.TrimEnd(';');

            // assign the filter to the file picker dialog
            _musicFileDialog.Filter = _filterText + "|" + _filterFilter;

            /***********************************************
             * Media Setup 
             **********************************************/

            _mediaPlayer = new MediaPlayer();
        }

        /// <summary>
        /// Opens a dialog menu to pick an audio file which is saved for when audio is played.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btn_browse_Click(object sender, EventArgs e) {
            // open the file picker dialog when browse is clicked
            _musicFileDialog.ShowDialog();
        }

        /// <summary>
        /// Updates text box and saves the filename to be played.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _musicFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e) {
            // display the selected file's path
            _txt_fileName.Text = _musicFileDialog.FileName;
            // stop the media player
            _mediaPlayer.Stop();
            // ensure the play/pause button is correct
            _btn_playpause.Text = _mediaPlay;
        }

        /// <summary>
        /// Switches between play and pause states for the button and
        /// media player.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btn_playpause_Click(object sender, EventArgs e) {
            if (_btn_playpause.Text == _mediaPlay) {
                // Play
                _btn_playpause.Text = _mediaPause;
                _mediaPlayer.LoadFile(_txt_fileName.Text);
                _mediaPlayer.Play();
            } else {
                // pause
                _btn_playpause.Text = _mediaPlay;
                _mediaPlayer.Pause();
            }
        }

        /// <summary>
        /// Sets media output volume percent to trackbar value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _tb_volume_Scroll(object sender, EventArgs e) {
            _mediaPlayer.setVolumePercentage(_tb_volume.Value);
        }

        /// <summary>
        /// Switches between enabled and disabled states for the live 
        /// EQ filter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btn_eq_enable_Click(object sender, EventArgs e) {
            var btn = sender as Button;
            if (btn == null)
            {
                return;
            }
            if (btn.Text.Equals("Enable EQ")) {
                //_rteq.EnableFilter();
                btn.Text = "Disable EQ";
            } else {
                //_rteq.DisableFilter();
                btn.Text = "Enable EQ";
            }
        }

        /// <summary>
        /// Maps trackbar value (0 to 10) to an audio freq (50 to 16000)
        /// and sets that to be the live EQ center frequency.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _tb_eq_freq_ValueChanged(object sender, EventArgs e) {
            var bar = sender as TrackBar;
            if (bar == null)
            {
                return;
            }
            var eqMin = 50;
            var eqMax = 16000;
            var percent = (float)bar.Value / (float)bar.Maximum;
            var finalEQFreq = eqMin + (percent * (eqMax - eqMin));
            //_rteq.SetFilter(finalEQFreq, _rteq.GetQ(), _rteq.GetGain());
        }
    }
}