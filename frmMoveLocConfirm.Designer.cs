namespace AutoTech
{
    partial class frmMoveLocConfirm
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
            this.lbl_MoveConfirm = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.lbl_MoveConfirm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_MoveConfirm
            // 
            this.lbl_MoveConfirm.Location = new System.Drawing.Point(42, 31);
            this.lbl_MoveConfirm.Name = "lbl_MoveConfirm";
            this.lbl_MoveConfirm.Size = new System.Drawing.Size(77, 18);
            this.lbl_MoveConfirm.TabIndex = 0;
            this.lbl_MoveConfirm.Text = "激光移动至：";
            // 
            // frmMoveLocConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 96);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_MoveConfirm);
            this.Name = "frmMoveLocConfirm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "frmMoveLocConfirm";
            ((System.ComponentModel.ISupportInitialize)(this.lbl_MoveConfirm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel lbl_MoveConfirm;
    }
}
