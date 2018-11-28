namespace AutoTech
{
    partial class frmShowImage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_ShowImage = new System.Windows.Forms.Panel();
            this.pbx_ShowImage = new System.Windows.Forms.PictureBox();
            this.panel_ShowImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_ShowImage)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_ShowImage
            // 
            this.panel_ShowImage.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel_ShowImage.Controls.Add(this.pbx_ShowImage);
            this.panel_ShowImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ShowImage.Location = new System.Drawing.Point(0, 0);
            this.panel_ShowImage.Name = "panel_ShowImage";
            this.panel_ShowImage.Size = new System.Drawing.Size(374, 261);
            this.panel_ShowImage.TabIndex = 0;
            // 
            // pbx_ShowImage
            // 
            this.pbx_ShowImage.Location = new System.Drawing.Point(28, 32);
            this.pbx_ShowImage.Name = "pbx_ShowImage";
            this.pbx_ShowImage.Size = new System.Drawing.Size(100, 50);
            this.pbx_ShowImage.TabIndex = 0;
            this.pbx_ShowImage.TabStop = false;
            // 
            // frmShowImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(374, 261);
            this.Controls.Add(this.panel_ShowImage);
            this.Name = "frmShowImage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "frmShowImage";
            this.Load += new System.EventHandler(this.frmShowImage_Load);
            this.panel_ShowImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbx_ShowImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_ShowImage;
        private System.Windows.Forms.PictureBox pbx_ShowImage;
    }
}