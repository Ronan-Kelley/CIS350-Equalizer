namespace Equalizer
{
    partial class Form1
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
            this._txt_fileName = new System.Windows.Forms.TextBox();
            this._btn_browse = new System.Windows.Forms.Button();
            this._musicFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // _txt_fileName
            // 
            this._txt_fileName.Location = new System.Drawing.Point(11, 12);
            this._txt_fileName.Name = "_txt_fileName";
            this._txt_fileName.Size = new System.Drawing.Size(696, 23);
            this._txt_fileName.TabIndex = 0;
            // 
            // _btn_browse
            // 
            this._btn_browse.Location = new System.Drawing.Point(713, 12);
            this._btn_browse.Name = "_btn_browse";
            this._btn_browse.Size = new System.Drawing.Size(75, 23);
            this._btn_browse.TabIndex = 1;
            this._btn_browse.Text = "Browse";
            this._btn_browse.UseVisualStyleBackColor = true;
            this._btn_browse.Click += new System.EventHandler(this.button1_Click);
            // 
            // _musicFileDialog
            // 
            this._musicFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._btn_browse);
            this.Controls.Add(this._txt_fileName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox _txt_fileName;
        private Button _btn_browse;
        private OpenFileDialog _musicFileDialog;
    }
}