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
        private int m_iImageWidth = 0;
        private int m_iImageHeight = 0;
        private Point m_PointSel;

        private clsFixture m_objFixture;

        public clsFixture Fixture
        {
            set
            {
                m_objFixture = value;
            }
        }

        public string TitleText
        {
            set
            {
                this.Text = value;
            }
        }

        public int ImageWidth
        {
            set
            {
                this.m_iImageWidth = value;
            }
        }

        public int ImageHeight
        {
            set
            {
                this.m_iImageHeight = value;
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

            tsslbl_imageSize.Text = string.Format("{0} x {1}", m_iImageWidth, m_iImageHeight);

        }

        private void pbx_ShowImage_MouseMove(object sender, MouseEventArgs e)
        {
            int iPointX, iPointY;

            iPointX = e.X;
            iPointY = e.Y;
            m_PointSel.X = iPointX;
            m_PointSel.Y = iPointY;

            tsslbl_imageLocation.Text = string.Format("{0} x {1}", iPointX, iPointY);
        }

        private void pbx_ShowImage_Click(object sender, EventArgs e)
        {

            int x, y;
            string strAsk;
            DialogResult dlgRes;
            strAsk = string.Format("将激光移动至 点 X={0} Y= {1}", m_PointSel.X, m_PointSel.Y);
            dlgRes = MessageBox.Show(strAsk, "确认信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dlgRes == DialogResult.No) return;

            x =(int) (m_PointSel.X * 17.32 -5835.6);
            y = (int)(m_PointSel.Y * 16.843 - 9793.96);

            m_objFixture.MoveToPostion(clsFixture.en_Postion._PostionXY, x,y );
        }

    }
}
