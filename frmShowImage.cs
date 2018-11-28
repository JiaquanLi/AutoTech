using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basler.Pylon;
namespace AutoTech
{
    public partial class frmShowImage : Form
    {
        const int CP_NOCLOSE_BUTTON = 0x200;               ///<用于禁用窗体的关闭按钮
        private ICameraInfo objICameraInfo;


        public ICameraInfo CameraInfo
        {
            set
            {
                objICameraInfo = value;
            }
        }

        public PictureBox PbxShowImage
        {
            get
            {
                return this.pbx_ShowImage;
            }
        }

        public frmShowImage()
        {
            InitializeComponent();
        }
        /// <summary>
        ///禁用窗体的关闭按钮只是ControlBox=false，会连同最小化和最大化按钮都不显示，所以，如果你想
        ///只想让关闭按钮不起作用，然后保留最小化、最大化的话，就重写窗体的CreateParams方法：
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams objCP = base.CreateParams;
                objCP.ClassStyle = objCP.ClassStyle | CP_NOCLOSE_BUTTON;
                return objCP;
            }
        }

        private void frmShowImage_Load(object sender, EventArgs e)
        {
            panel_ShowImage.AutoScroll = true;
            pbx_ShowImage.Location = new Point(0, 0);
            pbx_ShowImage.SizeMode = PictureBoxSizeMode.AutoSize;

        }

    }
}
