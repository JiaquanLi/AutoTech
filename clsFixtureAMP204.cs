using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APS168_W32;
using APS_Define_W32;
using System.IO;
using System.Threading;

namespace AutoTech
{
    public class clsFixtureAMP204
    {

        public int StartAxisId = 0;
        public int TotalAxis = 0;
        public int CardName = 0;
        public int CardId;
        private const string txtXmlFilename = "param.xml";

        const int _selectAxisX = 0;
        const int _selectAxisY = 1;

        public bool IsInitialed { get; private set; }
        public bool IsLoadXmlFile { get; private set; }


        public clsFixtureAMP204()
        {
            IsInitialed = false;
        }
        ~clsFixtureAMP204()
        {

        }

        public bool InitialFixture(int cardId, int mode)
        {
            CardId = cardId;
            int boardIdInBits = 0;
            // Card(Board) initial,mode bit0(0:By system assigned, 1:By dip switch)  
            int ret = APS168.APS_initial(ref boardIdInBits, mode);
            if (ret >= 0)
            {
                APS168.APS_get_first_axisId(cardId, ref StartAxisId, ref TotalAxis);
                APS168.APS_get_card_name(cardId, ref CardName);
                if (CardName != (Int32)APS_Define.DEVICE_NAME_PCI_825458 && CardName != (Int32)APS_Define.DEVICE_NAME_AMP_20408C)
                {
                    //MessageBox.Show("运动控制是型号不是204C或208C！");
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("运动控制卡初始化失败，请检查驱动是否装好或者MotionCreatePro已经开启！");
                return false;
            }


            //判断配置文件是否存在
            if (File.Exists(txtXmlFilename))
            {
                if (LoadParamFromFile(txtXmlFilename) == false)
                {
                    return false;
                }
            }

            IsInitialed = true;
            return true;
        }

        public void GetPostionAbs(ref double x,ref double y)
        {
            APS168.APS_get_position_f(_selectAxisX, ref x);
            APS168.APS_get_position_f(_selectAxisY, ref y);
        }

        public bool LoadParamFromFile(string xmlfilename)
        {
            IsLoadXmlFile = (APS168.APS_load_param_from_file(xmlfilename) == 0);
            if (IsLoadXmlFile)
            {
                //AutoClosingMessageBox.Show("Load XML File OK !", "204C208C", 2000);
            }
            else
            {
                //MessageBox.Show("Load XML File Failed!");
            }
            return IsLoadXmlFile;
        }

        public void ServoOn(bool onOff)
        {
            bool status;
            if (IsInitialed == false)
            {              
                return;
            }

            int motionIoStatusX = APS168.APS_motion_io_status(_selectAxisX);
            //if (motionIoStatusX == 0)
            {
                status = (motionIoStatusX & (1 << 7)) != 0;
                APS168.APS_set_servo_on(_selectAxisX, Convert.ToInt32(!status));
            }
            Thread.Sleep(100);
            int motionIoStatusY = APS168.APS_motion_io_status(_selectAxisY);
            //if (motionIoStatusY == 0)
            {
                status = (motionIoStatusY & (1 << 7)) != 0;
                APS168.APS_set_servo_on(_selectAxisY, Convert.ToInt32(!status));
            }
        }

        public void HomeXY()
        {
            //_selectAxis = Convert.ToInt32(cmbSelectAxis.SelectedItem);
            int homeMode = 0;
            int homeDir = 1;
            double praCurve = 0.5;
            double praAcc = 1000000;
            double praVm = 1000;
            if (IsInitialed == false) return;

            Task taskX = new Task(() => this.StartHoming(_selectAxisX, homeMode, homeDir, praCurve, praAcc, praVm));

            Task taskY = new Task(() => this.StartHoming(_selectAxisY, homeMode, homeDir, praCurve, praAcc, praVm));
            taskX.Start();
            taskY.Start();

            taskX.Wait();
            taskY.Wait();
        }

        public void MoveRelative(int x,int y)
        {
            double praAcc = 1000000;
            double praDec = 1000000;
            int praVm = 1000;
            int pulseX = x;
            int pulseY = y;
            if (IsInitialed == false) return;

            APS168.APS_set_axis_param_f(_selectAxisX, (Int32)APS_Define.PRA_ACC, praAcc); // Set acceleration rate
            APS168.APS_set_axis_param_f(_selectAxisX, (Int32)APS_Define.PRA_DEC, praDec); // Set deceleration rate

            APS168.APS_set_axis_param_f(_selectAxisY, (Int32)APS_Define.PRA_ACC, praAcc); // Set acceleration rate
            APS168.APS_set_axis_param_f(_selectAxisY, (Int32)APS_Define.PRA_DEC, praDec); // Set deceleration rate


            //if (_card0.IsLoadXmlFile)
            {
                Task task = new Task(() =>
                {
                    APS168.APS_relative_move(_selectAxisX, pulseX, praVm);
                    APS168.APS_relative_move(_selectAxisY, pulseY, praVm);

                    //等待Motion Down完成
                    int motionStatusMdn = 5;
                    while ((APS168.APS_motion_status(_selectAxisX) & 1 << motionStatusMdn) == 0 && (APS168.APS_motion_status(_selectAxisY) & 1 << motionStatusMdn) == 0)
                    {
                        Thread.Sleep(100);
                    }
                    //MessageBox.Show("运动完成！");
                });
                task.Start();
            }

        }
        public void MoveAbs(int x,int y)
        {
            //_selectAxis = Convert.ToInt32(cmbSelectAxis.SelectedItem);
            double praAcc = 1000000;
            double praDec = 1000000;
            int praVm = 1000;
            int pulseX = x;
            int pulseY = y;
            if (IsInitialed == false) return;

            APS168.APS_set_axis_param_f(_selectAxisX, (Int32)APS_Define.PRA_ACC, praAcc); // Set acceleration rate
            APS168.APS_set_axis_param_f(_selectAxisX, (Int32)APS_Define.PRA_DEC, praDec); // Set deceleration rate

            APS168.APS_set_axis_param_f(_selectAxisY, (Int32)APS_Define.PRA_ACC, praAcc); // Set acceleration rate
            APS168.APS_set_axis_param_f(_selectAxisY, (Int32)APS_Define.PRA_DEC, praDec); // Set deceleration rate

            //if (_card0.IsLoadXmlFile)
            {
                Task task = new Task(() =>
                {
                    APS168.APS_absolute_move(_selectAxisX, pulseX, praVm);
                    APS168.APS_absolute_move(_selectAxisY, pulseY, praVm);

                    //等待Motion Down完成
                    int motionStatusMdn = 5;
                    while ((APS168.APS_motion_status(_selectAxisX) & 1 << motionStatusMdn) == 0 && (APS168.APS_motion_status(_selectAxisY) & 1 << motionStatusMdn) == 0)
                    {
                        Thread.Sleep(100);
                    }
                });
                task.Start();
            }

        }

        public void MovePT_Line(int x,int y)
        {

            Task t = new Task(() => PointTableMove(x,y));
            t.Start();

            Thread.Sleep(100);
            t.Wait();

        }

        private void PointTableMove(double x, double y)
        {
            PTSTS ptStatus = new PTSTS();
            PTLINE Line = new PTLINE();
            PTDWL dwell = new PTDWL();
            dwell.DwTime = 200;
            int _bufTotalPoint, _bufFreeSpace, _bufUsageSpace, _bufRunningCnt;

            Int32 ptbId = 0, dimension = 2;
            int[] axisIdArray = new int[] { 0, 1 };

            double vStart = 5000;
            double vMax = 40000;
            double vEnd = 5000;


            int ret = APS168.APS_pt_disable(0, 0);
            ret = APS168.APS_pt_enable(0, ptbId, dimension, axisIdArray);

            ret = APS168.APS_pt_set_absolute(0, ptbId);
            ret = APS168.APS_pt_set_trans_buffered(0, ptbId);
            ret = APS168.APS_pt_set_acc(0, ptbId, 500000);
            ret = APS168.APS_pt_set_dec(0, ptbId, 500000);

            {
                {
                    ret = APS168.APS_get_pt_status(0, ptbId, ref ptStatus);
                    _bufFreeSpace = ptStatus.PntBufFreeSpace;
                    _bufUsageSpace = ptStatus.PntBufUsageSpace;
                    _bufRunningCnt = (int)ptStatus.RunningCnt;
                    if (ptStatus.PntBufFreeSpace > 10)
                    {
                        ret = APS168.APS_pt_set_vm(0, ptbId, 1000);//最大速度
                        ret = APS168.APS_pt_set_ve(0, ptbId, 1000);//结束速度

                        Line.Dim = dimension;
                        Line.Pos = new Double[] { x, y, 0, 0, 0, 0 };
                        ret = APS168.APS_pt_line(0, ptbId, ref Line, ref ptStatus);
                        ret = APS168.APS_pt_ext_set_do_ch(0, ptbId, 8, 1);
                        ret = APS168.APS_pt_dwell(0, ptbId, ref dwell, ref ptStatus);
                        ret = APS168.APS_pt_ext_set_do_ch(0, ptbId, 8, 0);
                        ret = APS168.APS_pt_dwell(0, ptbId, ref dwell, ref ptStatus);
                    }
                    else
                    {
                        Thread.Sleep(1);
                    }
                }
            }
            APS168.APS_pt_start(0, 0);
            //等待buffer跑完, motionStatus的ptbFlag=false则连续运动结束。
            bool ptbFlag = true;
            while (ptbFlag)
            {
                int motionStatus = APS168.APS_motion_status(axisIdArray[0]);
                ptbFlag = (motionStatus & (1 << 11)) != 0;

                ret = APS168.APS_get_pt_status(0, ptbId, ref ptStatus);
                _bufFreeSpace = ptStatus.PntBufFreeSpace;
                _bufUsageSpace = ptStatus.PntBufUsageSpace;
                _bufRunningCnt = (int)ptStatus.RunningCnt;
                Thread.Sleep(100);
            }
        }

        private void JOGAX()
        {
            //APS168.APS_set_axis_param(_selectAxisX, (int)APS_Define.PRA_JG_MODE, 1);
            //APS168.APS_set_axis_param(_selectAxisX, (int)APS_Define.PRA_JG_DIR, Convert.ToInt32(((Label)sender).Tag));
            //APS168.APS_set_axis_param_f(_selectAxisX, (int)APS_Define.PRA_JG_ACC, Convert.ToDouble(txtPraAcc.Text));
            //APS168.APS_set_axis_param_f(_selectAxisX, (int)APS_Define.PRA_JG_DEC, Convert.ToDouble(txtPraAcc.Text));
            //APS168.APS_set_axis_param_f(_selectAxisX, (int)APS_Define.PRA_JG_VM, Convert.ToDouble(txtPraVm.Text));
            //APS168.APS_jog_start(_selectAxisX, 1);
        }

        private void JOGAY()
        {
            //APS168.APS_set_axis_param(_selectAxisY, (int)APS_Define.PRA_JG_MODE, 1);
            //APS168.APS_set_axis_param(_selectAxisY, (int)APS_Define.PRA_JG_DIR, Convert.ToInt32(((Label)sender).Tag));
            //APS168.APS_set_axis_param_f(_selectAxisY, (int)APS_Define.PRA_JG_ACC, Convert.ToDouble(txtPraAcc.Text));
            //APS168.APS_set_axis_param_f(_selectAxisY, (int)APS_Define.PRA_JG_DEC, Convert.ToDouble(txtPraAcc.Text));
            //APS168.APS_set_axis_param_f(_selectAxisY, (int)APS_Define.PRA_JG_VM, Convert.ToDouble(txtPraVm.Text));
            //APS168.APS_jog_start(_selectAxisY, 1);
        }

        private void StartHoming(int axisId, int homeMode, int homeDir, double praCurve, double praAcc, double praVm)
        {


            // 1. Select home mode and config home parameters 
            APS168.APS_set_axis_param(axisId, (Int32)APS_Define.PRA_HOME_MODE, homeMode); // Set home mode
            APS168.APS_set_axis_param(axisId, (Int32)APS_Define.PRA_HOME_DIR, homeDir); // Set home direction
            APS168.APS_set_axis_param_f(axisId, (Int32)APS_Define.PRA_HOME_CURVE, praCurve); // Set acceleration paten (T-curve)
            APS168.APS_set_axis_param_f(axisId, (Int32)APS_Define.PRA_HOME_ACC, praAcc); // Set homing acceleration rate
            APS168.APS_set_axis_param_f(axisId, (Int32)APS_Define.PRA_HOME_VM, praVm); // Set homing maximum velocity.
            APS168.APS_set_axis_param_f(axisId, (Int32)APS_Define.PRA_HOME_VO, praVm / 5); // Set homing VO speed
            APS168.APS_set_axis_param_f(axisId, (Int32)APS_Define.PRA_HOME_EZA, 0); // Set EZ signal alignment (yes or no)
            APS168.APS_set_axis_param_f(axisId, (Int32)APS_Define.PRA_HOME_SHIFT, 0); // Set home position shfit distance. 
            APS168.APS_set_axis_param_f(axisId, (Int32)APS_Define.PRA_HOME_POS, 0); // Set final home position.

            //servo on
            APS168.APS_set_servo_on(axisId, 1);
            Thread.Sleep(500); // Wait stable.


            // 2. Start home move
            APS168.APS_home_move(axisId);

            int motionStatusCstp = 0, motionStatusAstp = 16;
            while ((APS168.APS_motion_status(axisId) & 1 << motionStatusCstp) == 0)
            {
                Thread.Sleep(100);
            }
            Thread.Sleep(500);

            if ((APS168.APS_motion_status(axisId) & 1 << motionStatusAstp) == 0)
            {
                //MessageBox.Show("轴" + axisId + "回零成功！");
            }
            else
            {
                //MessageBox.Show("轴" + axisId + "回零失败！");
            }
        }

        private void ReadAI()
        {

        }
    }
}
