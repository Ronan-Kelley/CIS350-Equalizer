using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Diagnostics;

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

        // player object
        private MediaPlayer _media = new();

        private RealTimeEq _rteq = new();

        public _form_eq()
        {
            InitializeComponent();

            //
            // set up multimedia components
            //

            // play/pause button
            _btn_playpause.Text = _mediaPlay;

            // volume slider
            _tb_volume.Minimum = 0;
            _tb_volume.Maximum = 100;
            _tb_volume.Value = 100;
            _tb_volume.TickFrequency = 10;

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

        private void _btwn_browse_Click(object sender, EventArgs e)
        {
            // open the file picker dialog when browse is clicked
            _musicFileDialog.ShowDialog();
        }

        private void _musicFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // display the selected file's path
            _txt_fileName.Text = _musicFileDialog.FileName;
            // stop the media player
            _media.Stop();
            // ensure the play/pause button is correct
            _btn_playpause.Text = _mediaPlay;
        }

        private void _btn_playpause_Click(object sender, EventArgs e)
        {
            if (_btn_playpause.Text == _mediaPlay)
            {
                // Play
                _btn_playpause.Text = _mediaPause;
                _media.LoadFile(_txt_fileName.Text);
                _media.Play();
            } else
            {
                // pause
                _btn_playpause.Text = _mediaPlay;
                _media.Pause();
            }
        }

        private void _tb_volume_Scroll(object sender, EventArgs e)
        {
            _media.setVolumePercentage(_tb_volume.Value);
        }

        private void _btn_tmp_Click(object sender, EventArgs e)
        {
            _rteq.doThings();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e) {
            var bar = sender as TrackBar;
            var eqMin = 50;
            var eqMax = 16000;
            var percent = (float)bar.Value / (float)bar.Maximum;
            var finalEQFreq = eqMin + (percent*(eqMax - eqMin));
            Debug.Print(finalEQFreq.ToString());
            _rteq.setFilter(finalEQFreq, 0.2f, 20);
        }
    }
}