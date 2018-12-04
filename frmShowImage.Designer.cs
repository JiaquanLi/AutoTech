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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShowImage));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel_ShowImage = new System.Windows.Forms.Panel();
            this.pbx_ShowImage = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslbl_imageSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslbl_imageLocation = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_ShowImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_ShowImage)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel_ShowImage, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(384, 261);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel_ShowImage
            // 
            this.panel_ShowImage.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel_ShowImage.Controls.Add(this.pbx_ShowImage);
            this.panel_ShowImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ShowImage.Location = new System.Drawing.Point(3, 3);
            this.panel_ShowImage.Name = "panel_ShowImage";
            this.panel_ShowImage.Size = new System.Drawing.Size(378, 225);
            this.panel_ShowImage.TabIndex = 0;
            // 
            // pbx_ShowImage
            // 
            this.pbx_ShowImage.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pbx_ShowImage.Location = new System.Drawing.Point(55, 45);
            this.pbx_ShowImage.Name = "pbx_ShowImage";
            this.pbx_ShowImage.Size = new System.Drawing.Size(100, 50);
            this.pbx_ShowImage.TabIndex = 0;
            this.pbx_ShowImage.TabStop = false;
            this.pbx_ShowImage.Click += new System.EventHandler(this.pbx_ShowImage_Click);
            this.pbx_ShowImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbx_ShowImage_MouseMove);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslbl_imageSize,
            this.toolStripSplitButton1,
            this.tsslbl_imageLocation});
            this.statusStrip1.Location = new System.Drawing.Point(0, 235);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(384, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslbl_imageSize
            // 
            this.tsslbl_imageSize.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsslbl_imageSize.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.tsslbl_imageSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslbl_imageSize.Name = "tsslbl_imageSize";
            this.tsslbl_imageSize.Size = new System.Drawing.Size(82, 21);
            this.tsslbl_imageSize.Text = "1920 x 1080";
            // 
            // tsslbl_imageLocation
            // 
            this.tsslbl_imageLocation.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsslbl_imageLocation.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.tsslbl_imageLocation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslbl_imageLocation.Name = "tsslbl_imageLocation";
            this.tsslbl_imageLocation.Size = new System.Drawing.Size(82, 21);
            this.tsslbl_imageLocation.Text = "1920 x 1080";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.toolStripSeparator1});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(32, 24);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // frmShowImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Name = "frmShowImage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "frmShowImage";
            this.Load += new System.EventHandler(this.frmShowImage_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel_ShowImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbx_ShowImage)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel_ShowImage;
        private System.Windows.Forms.PictureBox pbx_ShowImage;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslbl_imageSize;
        private System.Windows.Forms.ToolStripStatusLabel tsslbl_imageLocation;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}