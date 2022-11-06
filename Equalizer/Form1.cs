namespace Equalizer
{
    public partial class Form1 : Form
    {
        // I don't know what file types are actually supported,
        // but this gives us an easy way to change what files can
        // be selected later on down the line
        private string[] _filetypes = { "mp3", "wav", "ogg" };

        public Form1()
        {
            InitializeComponent();

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
        }
    }
}