namespace Equalizer
{
    partial class EqualizerOptions
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this._btn_addnode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _btn_addnode
            // 
            this._btn_addnode.AllowDrop = true;
            this._btn_addnode.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this._btn_addnode.Location = new System.Drawing.Point(445, 124);
            this._btn_addnode.Name = "_btn_addnode";
            this._btn_addnode.Size = new System.Drawing.Size(52, 52);
            this._btn_addnode.TabIndex = 0;
            this._btn_addnode.Text = "+";
            this._btn_addnode.UseVisualStyleBackColor = true;
            this._btn_addnode.Click += new System.EventHandler(this._btn_addnode_Click);
            // 
            // EqualizerOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._btn_addnode);
            this.Name = "EqualizerOptions";
            this.Size = new System.Drawing.Size(500, 300);
            this.ResumeLayout(false);

        }

        #endregion

        private Button _btn_addnode;
    }
}
