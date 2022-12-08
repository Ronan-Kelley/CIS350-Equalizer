namespace Equalizer
{
    partial class _form_eq
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_form_eq));
            this._btn_playpause = new System.Windows.Forms.Button();
            this._tb_volume = new System.Windows.Forms.TrackBar();
            this.EQOpts = new Equalizer.EqualizerOptions();
            this._btn_eq_enable = new System.Windows.Forms.Button();
            this.lb_FolderContents = new System.Windows.Forms.ListBox();
            this._btn_selectFolder = new System.Windows.Forms.Button();
            this._txt_folder = new System.Windows.Forms.TextBox();
            this._dialog_folderSelect = new System.Windows.Forms.FolderBrowserDialog();
            this._ob_filesystem = new System.IO.FileSystemWatcher();
            this.txt_curPlaying = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._tb_volume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._ob_filesystem)).BeginInit();
            this.SuspendLayout();
            // 
            // _btn_playpause
            // 
            this._btn_playpause.Location = new System.Drawing.Point(14, 370);
            this._btn_playpause.Margin = new System.Windows.Forms.Padding(5);
            this._btn_playpause.Name = "_btn_playpause";
            this._btn_playpause.Size = new System.Drawing.Size(45, 45);
            this._btn_playpause.TabIndex = 2;
            this._btn_playpause.UseVisualStyleBackColor = true;
            this._btn_playpause.Click += new System.EventHandler(this._btn_playpause_Click);
            // 
            // _tb_volume
            // 
            this._tb_volume.Location = new System.Drawing.Point(69, 370);
            this._tb_volume.Margin = new System.Windows.Forms.Padding(5);
            this._tb_volume.Name = "_tb_volume";
            this._tb_volume.Size = new System.Drawing.Size(150, 45);
            this._tb_volume.TabIndex = 3;
            this._tb_volume.TickStyle = System.Windows.Forms.TickStyle.Both;
            this._tb_volume.Scroll += new System.EventHandler(this._tb_volume_Scroll);
            // 
            // EQOpts
            // 
            this.EQOpts.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("EQOpts.BackgroundImage")));
            this.EQOpts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.EQOpts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EQOpts.Location = new System.Drawing.Point(545, 42);
            this.EQOpts.Name = "EQOpts";
            this.EQOpts.Size = new System.Drawing.Size(507, 304);
            this.EQOpts.TabIndex = 6;
            // 
            // _btn_eq_enable
            // 
            this._btn_eq_enable.Location = new System.Drawing.Point(752, 386);
            this._btn_eq_enable.Name = "_btn_eq_enable";
            this._btn_eq_enable.Size = new System.Drawing.Size(115, 31);
            this._btn_eq_enable.TabIndex = 7;
            this._btn_eq_enable.Text = "Enable EQ";
            this._btn_eq_enable.UseVisualStyleBackColor = true;
            this._btn_eq_enable.Click += new System.EventHandler(this._btn_eq_enable_Click_1);
            // 
            // lb_FolderContents
            // 
            this.lb_FolderContents.FormattingEnabled = true;
            this.lb_FolderContents.ItemHeight = 15;
            this.lb_FolderContents.Location = new System.Drawing.Point(12, 43);
            this.lb_FolderContents.Name = "lb_FolderContents";
            this.lb_FolderContents.Size = new System.Drawing.Size(515, 304);
            this.lb_FolderContents.TabIndex = 8;
            this.lb_FolderContents.SelectedIndexChanged += new System.EventHandler(this.lb_FolderContents_SelectedIndexChanged);
            // 
            // _btn_selectFolder
            // 
            this._btn_selectFolder.Location = new System.Drawing.Point(934, 13);
            this._btn_selectFolder.Name = "_btn_selectFolder";
            this._btn_selectFolder.Size = new System.Drawing.Size(118, 23);
            this._btn_selectFolder.TabIndex = 9;
            this._btn_selectFolder.Text = "Select Folder";
            this._btn_selectFolder.UseVisualStyleBackColor = true;
            this._btn_selectFolder.Click += new System.EventHandler(this._btn_selectFolder_Click);
            // 
            // _txt_folder
            // 
            this._txt_folder.Location = new System.Drawing.Point(545, 13);
            this._txt_folder.Name = "_txt_folder";
            this._txt_folder.Size = new System.Drawing.Size(383, 23);
            this._txt_folder.TabIndex = 10;
            this._txt_folder.TextChanged += new System.EventHandler(this._txt_folder_TextChanged);
            // 
            // _ob_filesystem
            // 
            this._ob_filesystem.EnableRaisingEvents = true;
            this._ob_filesystem.SynchronizingObject = this;
            this._ob_filesystem.Changed += new System.IO.FileSystemEventHandler(this._ob_filesystem_Changed);
            // 
            // txt_curPlaying
            // 
            this.txt_curPlaying.AutoSize = true;
            this.txt_curPlaying.Location = new System.Drawing.Point(14, 17);
            this.txt_curPlaying.Name = "txt_curPlaying";
            this.txt_curPlaying.Size = new System.Drawing.Size(102, 15);
            this.txt_curPlaying.TabIndex = 11;
            this.txt_curPlaying.Text = "currently playing: ";
            // 
            // _form_eq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 429);
            this.Controls.Add(this.txt_curPlaying);
            this.Controls.Add(this._txt_folder);
            this.Controls.Add(this._btn_selectFolder);
            this.Controls.Add(this.lb_FolderContents);
            this.Controls.Add(this._btn_eq_enable);
            this.Controls.Add(this.EQOpts);
            this.Controls.Add(this._tb_volume);
            this.Controls.Add(this._btn_playpause);
            this.Name = "_form_eq";
            this.Text = "Equalizer Project";
            ((System.ComponentModel.ISupportInitialize)(this._tb_volume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._ob_filesystem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button _btn_playpause;
        private TrackBar _tb_volume;
        private NAudio.Gui.VolumeSlider volumeSlider1;
        private EqualizerOptions EQOpts;
        private Button _btn_eq_enable;
        private ListBox lb_FolderContents;
        private Button _btn_selectFolder;
        private TextBox _txt_folder;
        private FolderBrowserDialog _dialog_folderSelect;
        private FileSystemWatcher _ob_filesystem;
        private Label txt_curPlaying;
    }
}