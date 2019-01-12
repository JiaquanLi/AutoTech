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
using System.Runtime.InteropServices;

namespace AutoTech
{
    public partial class frmShowImage : Form
    {

        public delegate void OnMessageCallback(string message);
        public static event OnMessageCallback Callback;

        [DllImport("cvProgress.dll", EntryPoint = "Mul")]
        public extern static double Mul(int locx, int locy,double[]num);

        const int CP_NOCLOSE_BUTTON = 0x200;               ///<用于禁用窗体的关闭按钮
        private int m_iImageWidth = 0;
        private int m_iImageHeight = 0;
        private double m_PointSelX;
        private double m_PointSelY;

        //private clsFixture m_objFixture;
        //private clsFixtureAMP204 m_objFixtureAPM204;
        private clsFixture8338 m_objFixture;
        private CCBasler m_objBaslerLocalCamera;

        //public clsFixture Fixture
        //{
        //    set
        //    {
        //        m_objFixture = value;
        //    }
        //}

        public CCBasler BaslerLocalCamera
        {
            set
            {
                m_objBaslerLocalCamera = value;
            }
        }

        //public clsFixtureAMP204 Fixtureapm204
        //{
        //    set
        //    {
        //        m_objFixture = value;
        //    }
        //}

        public clsFixture8338 Fixture
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
            // x =(int) (m_PointSelX * (51.744) - m_PointSelY*(105.08)+87296.7);
            //y = (int)(m_PointSelX * (105.08) + m_PointSelY* (51.744) -46792.34);
            x = (int)(-67.1875 * m_PointSelX + 143431.0625);
            y = (int)(151.646706586826 * m_PointSelY + 33815.8263473054);
            //x = 17438;
            //y = 20771;
            ////相机相对平移
            //x -= 1500;
            //y -= 3500;
            m_objFixture.SickLaserPowerOnOff(true);
            m_objFixture.MovePT_Line(x, y);
            MoveTheRealPoint();
        }

        private void MoveTheRealPoint()
        {
            double dReturn;
            double LaserX = 1238;
            double LaserY = 1250;
            string strTemp;
            double[] num = new double[2];



            m_objFixture.SickLaserPowerOnOff(false);
            System.Threading.Thread.Sleep(200);
            m_objBaslerLocalCamera.bSaveImgae = true;
            m_objBaslerLocalCamera.OneShot();

            System.Threading.Thread.Sleep(500);
            ////Process pcs = new Process();
            ////pcs.StartInfo.FileName = @"D:\MATPRO\MNF\crosscorner\for_testing\crosscorner.exe";
            ////pcs.Start();
            ////pcs.WaitForExit();
            ///
            try
            {
                dReturn = Mul((int)LaserX, (int)LaserY, num);

                if(dReturn == -1)
                {
                    Callback("没有找到标记点");
                    return;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            System.Threading.Thread.Sleep(100);
            Callback("Get Cross X:" + num[0].ToString());
            Callback("Get Cross Y:" + num[1].ToString());
            //StreamReader sr = new StreamReader(@"D:\MATPRO\pointxy.txt");
            //strTemp = sr.ReadLine();
            //sr.Close();
            //string[] arrXY = strTemp.Split(' ');


            double GetReadX = 2410;
            double GetReadY = 1623;

            GetReadX = num[0];
            GetReadY = num[1];

            double trasX, trasY;

            ///TEST
            trasX = (LaserX - GetReadX) * (11.066) + 0;
            trasY = -(LaserY - GetReadY) * 25.3216 + 0;

            m_objFixture.SickLaserPowerOnOff(true);
            m_objFixture.MoveRelative((int)trasX, (int)trasY);

        }

    }
}
