using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;


namespace AutoTech
{
    public partial class frmConfig : Telerik.WinControls.UI.RadForm
    {
        private  clsFixture m_objFixture = null;
        //clsFixture.stComPort m_objComPort = new clsFixture.stComPort();

        public clsFixture Fixture
        {
            set
            {
                this.m_objFixture = value;
            }
        }

        public frmConfig()
        {
            InitializeComponent();
            
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            gb_X.Enabled = false;
            gb_Y.Enabled = false;
            btn_CloseCom.Enabled = false;

        }

        private void btb_OpenCom_Click(object sender, EventArgs e)
        {

            //if(tb_ComNum.Text == "")
            //{
            //    MessageBox.Show("Com Number 输入错误！");
            //    return;
            //}
            //m_objComPort.strComPortNumber = tb_ComNum.Text;

            //if (!int.TryParse(tb_Baud.Text, out int baudRate))
            //{
            //    MessageBox.Show("波特率输入错误！");
            //    return;
            //}
            //m_objComPort.iBaundRate = baudRate;

            //if (!int.TryParse(tb_Databit.Text, out int dataBits))
            //{
            //    MessageBox.Show("数据位输入错误！");
            //    return;
            //}
            //m_objComPort.iDataBit = dataBits;

            //if (!int.TryParse(tb_StopBit.Text, out int stopBits))
            //{
            //    MessageBox.Show("停止位输入错误！");
            //    return;
            //}
            //m_objComPort.iStopBit = stopBits;

            //if (cb_Parity.Text == "")
            //{
            //    MessageBox.Show("奇偶校验位输入错误！");
            //    return;
            //}
            //m_objComPort.strParity = cb_Parity.Text;

            //m_objFixture.ComPort = m_objComPort;
            //if (m_objFixture.InitFixture() == false)
            //{
            //    MessageBox.Show("设备初始化失败!!!");
            //    return;
            //}

            gb_X.Enabled = true;
            gb_Y.Enabled = true;
            btn_CloseCom.Enabled = true;

        }
       
        private void btn_MoveHomeX_Click(object sender, EventArgs e)
        {
            m_objFixture.MoveToPostion(clsFixture.en_Postion._HomeX);
        }

        private void btn_MovePosX_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(tb_MoveLocX.Text, out int posX))
            {
                MessageBox.Show("位置输入错误！");
                return;
            }

            m_objFixture.MoveToPostion(clsFixture.en_Postion._PostionX,(int)posX);
        }

        private void btn_MovePosY_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(tb_MoveLocX.Text, out int posY))
            {
                MessageBox.Show("位置输入错误！");
                return;
            }

            m_objFixture.MoveToPostion(clsFixture.en_Postion._PostionY, 0,(int)posY);
        }

        private void btn_MoveHomeY_Click(object sender, EventArgs e)
        {
            m_objFixture.MoveToPostion(clsFixture.en_Postion._HomeY);
        }
    }
}
