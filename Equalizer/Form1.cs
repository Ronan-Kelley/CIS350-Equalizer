namespace Equalizer
{
    /// <summary>
    /// UI implementation
    /// </summary>
    public partial class _form_eq : Form
    {
        // I don't know what file types are actually supported,
        // but this gives us an easy way to change what files can
        // be selected later on down the line
        private readonly string[] _filetypes = { "mp3", "wav", "ogg" };

        // constants for media button text; pause looks weird right now
        private const string _mediaPlay = "\u25B6";
        private const string _mediaPause = "||";

        // Audio playback and altering
        private MediaPlayer _mediaPlayer;

        /// <summary>
        /// create a new _form_eq form object
        /// </summary>
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
             * Media Setup 
             **********************************************/

            _mediaPlayer = new MediaPlayer();
            EQOpts.OnChanged = _mediaPlayer.UpdateEqualizer;
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
            _mediaPlayer.SetVolumePercentage(_tb_volume.Value);
        }

        /// <summary>
        /// Switches between enabled and disabled states for the live 
        /// EQ filter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btn_eq_enable_Click_1(object sender, EventArgs e) {
            var btn = sender as Button;
            if (btn == null) {
                return;
            }

            if (btn.Text.Equals("Enable EQ")) {
                _mediaPlayer.EnableEqualizer();
                btn.Text = "Disable EQ";
            } else {
                _mediaPlayer.DisableEqualizer();
                btn.Text = "Enable EQ";
            }
        }

        /// <summary>
        /// event-driven UI callback for select folder button's being pressed
        /// </summary>
        /// <param name="sender">standard UI callback sender parameter</param>
        /// <param name="e">standard UI callback event paremeter</param>
        private void _btn_selectFolder_Click(object sender, EventArgs e) {
            // show the dialog
            _dialog_folderSelect.ShowDialog();
            // update the contents of _txt_folder
            _txt_folder.Text = _dialog_folderSelect.SelectedPath;
        }

        /// <summary>
        /// event-driven UI callback for change in text of the folder path input box.
        /// </summary>
        /// <param name="sender">standard UI callback sender parameter</param>
        /// <param name="e">standard UI callback event paremeter</param>
        private void _txt_folder_TextChanged(object sender, EventArgs e) {
            // get information about the directory selected
            DirectoryInfo di = new(_txt_folder.Text);
            // make sure the directory exists
            if (di.Exists) {
                // clear the previously loaded items
                lb_FolderContents.Items.Clear();
                // iterate over the contents of the directory
                foreach (FileInfo fi in di.GetFiles()) {
                    // iterate over each file type in _filetypes
                    foreach (string ftype in _filetypes) {
                        // make sure the extension of the current file is one
                        // listed in _filetypes to prevent loading things like
                        // exe files or text files
                        if (ftype.Equals(fi.Extension.TrimStart('.'))) {
                            // add the file to the items in the listbox
                            lb_FolderContents.Items.Add(fi.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// event-driven UI callback for change in selected index of the folder contents combobox.
        /// </summary>
        /// <param name="sender">standard UI callback sender parameter</param>
        /// <param name="e">standard UI callback event paremeter</param>
        private void lb_FolderContents_SelectedIndexChanged(object sender, EventArgs e) {
            _mediaPlayer.Stop();
            _btn_playpause.Text = _mediaPause;
            _mediaPlayer.Play();

        }
    }
}