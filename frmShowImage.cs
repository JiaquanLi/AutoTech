using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basler.Pylon;
using System.IO;

namespace AutoTech
{
    public partial class frmShowImage : Form
    {
        const int CP_NOCLOSE_BUTTON = 0x200;               ///<用于禁用窗体的关闭按钮
        private int m_iImageWidth = 0;
        private int m_iImageHeight = 0;
        private double m_PointSelX;
        private double m_PointSelY;

        private clsFixture m_objFixture;
        private clsFixtureAMP204 m_objFixtureAPM204;
        private CCBasler m_objBaslerLocalCamera;

        public clsFixture Fixture
        {
            set
            {
                m_objFixture = value;
            }
        }

        public CCBasler BaslerLocalCamera
        {
            set
            {
                m_objBaslerLocalCamera = value;
            }
        }

        public clsFixtureAMP204 Fixtureapm204
        {
            set
            {
                m_objFixtureAPM204 = value;
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
            m_PointSelX = iPointX;
            m_PointSelY = iPointY;

            tsslbl_imageLocation.Text = string.Format("{0} x {1}", iPointX, iPointY);
        }

        private void pbx_ShowImage_Click(object sender, EventArgs e)
        {

            int x, y;
            string strAsk;
            DialogResult dlgRes;
            strAsk = string.Format("将激光移动至 点 X={0} Y= {1}", m_PointSelX, m_PointSelY);
            dlgRes = MessageBox.Show(strAsk, "确认信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dlgRes == DialogResult.No) return;
            //激光点标定
            x =(int) (m_PointSelX * 17.4157303 -7326.4 + 400);
            y = (int)(m_PointSelY * 16.9871795 -9024.36 -900);

            //x = 17438;
            //y = 20771;
            ////相机相对平移
            x -= 1500;
            y -= 3500;
            m_objFixtureAPM204.MovePT_Line(x, y);
            MoveTheRealPoint();
        }

        private void MoveTheRealPoint()
        {
            double LaserX = 513;
            double LaserY = 1577;
            string strTemp;

            m_objBaslerLocalCamera.bSaveImgae = true;
            m_objBaslerLocalCamera.OneShot();

            System.Threading.Thread.Sleep(500);
            Process pcs = new Process();
            pcs.StartInfo.FileName = @"D:\MATPRO\MNF\crosscorner\for_testing\crosscorner.exe";
            pcs.Start();
            pcs.WaitForExit();

            System.Threading.Thread.Sleep(100);

            StreamReader sr = new StreamReader(@"D:\MATPRO\pointxy.txt");
            strTemp = sr.ReadLine();
            sr.Close();
            string []arrXY = strTemp.Split(' ');

             
            double GetReadX = 478;
            double GetReadY = 1335;

            GetReadX =double.Parse( arrXY[1]);
            GetReadY = double.Parse(arrXY[0]);

            double trasX, trasY;

            trasX = (LaserY - GetReadY) * 3.427 + 50;
            trasY = -(LaserX - GetReadX) * 3.636 - 100;
            m_objFixtureAPM204.MoveRelative((int)trasX, (int)trasY);

        }

    }
}
