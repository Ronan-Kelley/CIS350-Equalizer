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
            this._txt_fileName = new System.Windows.Forms.TextBox();
            this._btn_browse = new System.Windows.Forms.Button();
            this._musicFileDialog = new System.Windows.Forms.OpenFileDialog();
            this._btn_playpause = new System.Windows.Forms.Button();
            this._tb_volume = new System.Windows.Forms.TrackBar();
            this.equalizerOptions1 = new Equalizer.EqualizerOptions();
            this._btn_eq_enable = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._tb_volume)).BeginInit();
            this.SuspendLayout();
            // 
            // _txt_fileName
            // 
            this._txt_fileName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._txt_fileName.Location = new System.Drawing.Point(14, 14);
            this._txt_fileName.Margin = new System.Windows.Forms.Padding(5);
            this._txt_fileName.Name = "_txt_fileName";
            this._txt_fileName.Size = new System.Drawing.Size(951, 25);
            this._txt_fileName.TabIndex = 0;
            // 
            // _btn_browse
            // 
            this._btn_browse.Location = new System.Drawing.Point(975, 14);
            this._btn_browse.Margin = new System.Windows.Forms.Padding(5);
            this._btn_browse.Name = "_btn_browse";
            this._btn_browse.Size = new System.Drawing.Size(75, 25);
            this._btn_browse.TabIndex = 1;
            this._btn_browse.Text = "Browse";
            this._btn_browse.UseVisualStyleBackColor = true;
            this._btn_browse.Click += new System.EventHandler(this._btn_browse_Click);
            // 
            // _musicFileDialog
            // 
            this._musicFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this._musicFileDialog_FileOk);
            // 
            // _btn_playpause
            // 
            this._btn_playpause.Location = new System.Drawing.Point(14, 49);
            this._btn_playpause.Margin = new System.Windows.Forms.Padding(5);
            this._btn_playpause.Name = "_btn_playpause";
            this._btn_playpause.Size = new System.Drawing.Size(45, 45);
            this._btn_playpause.TabIndex = 2;
            this._btn_playpause.UseVisualStyleBackColor = true;
            this._btn_playpause.Click += new System.EventHandler(this._btn_playpause_Click);
            // 
            // _tb_volume
            // 
            this._tb_volume.Location = new System.Drawing.Point(69, 49);
            this._tb_volume.Margin = new System.Windows.Forms.Padding(5);
            this._tb_volume.Name = "_tb_volume";
            this._tb_volume.Size = new System.Drawing.Size(150, 45);
            this._tb_volume.TabIndex = 3;
            this._tb_volume.TickStyle = System.Windows.Forms.TickStyle.Both;
            this._tb_volume.Scroll += new System.EventHandler(this._tb_volume_Scroll);
            // 
            // equalizerOptions1
            // 
            this.equalizerOptions1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("equalizerOptions1.BackgroundImage")));
            this.equalizerOptions1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.equalizerOptions1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.equalizerOptions1.Location = new System.Drawing.Point(273, 82);
            this.equalizerOptions1.Name = "equalizerOptions1";
            this.equalizerOptions1.Size = new System.Drawing.Size(498, 298);
            this.equalizerOptions1.TabIndex = 6;
            // 
            // _btn_eq_enable
            // 
            this._btn_eq_enable.Location = new System.Drawing.Point(465, 386);
            this._btn_eq_enable.Name = "_btn_eq_enable";
            this._btn_eq_enable.Size = new System.Drawing.Size(115, 31);
            this._btn_eq_enable.TabIndex = 7;
            this._btn_eq_enable.Text = "Enable EQ";
            this._btn_eq_enable.UseVisualStyleBackColor = true;
            this._btn_eq_enable.Click += new System.EventHandler(this._btn_eq_enable_Click_1);
            // 
            // _form_eq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 429);
            this.Controls.Add(this._btn_eq_enable);
            this.Controls.Add(this.equalizerOptions1);
            this.Controls.Add(this._tb_volume);
            this.Controls.Add(this._btn_playpause);
            this.Controls.Add(this._btn_browse);
            this.Controls.Add(this._txt_fileName);
            this.Name = "_form_eq";
            this.Text = "Equalizer Project";
            ((System.ComponentModel.ISupportInitialize)(this._tb_volume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox _txt_fileName;
        private Button _btn_browse;
        private OpenFileDialog _musicFileDialog;
        private Button _btn_playpause;
        private TrackBar _tb_volume;
        private NAudio.Gui.VolumeSlider volumeSlider1;
        private EqualizerOptions equalizerOptions1;
        private Button _btn_eq_enable;
    }
}