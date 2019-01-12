using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APS168_W32;
using System.IO;
using System.Threading;
using PCIe_8338;
using LmiScanner;

namespace AutoTech
{

    public struct LineData
    {
        public double X;
        public double Y;
        public double Z;
    }
    public class clsFixture8338
    {

        public int StartAxisId = 0;
        public int TotalAxis = 0;
        public int CardName = 0;
        public int CardId = -1;
        private const int MOD_No_AI = 2;
        private const string txtXmlFilename = "PCIe-8338.xml";

        const int _selectAxisX = 1;
        const int _selectAxisY = 0;

        public bool IsInitialed { get; private set; }
        public bool IsLoadXmlFile { get; private set; }
        private clsScanner m_objScanner;


        public clsScanner Scanner
        {
            set
            {
                m_objScanner = value;
            }
        }

        public clsFixture8338()
        {
            IsInitialed = false;
        }
        ~clsFixture8338()
        {
            if (CardId != -1)
            {
                SickLaserPowerOnOff(false);
                Class_8338.StopFieldBUS(CardId);//停止总线
                Class_8338.Close();//关闭卡片
            }
        }

        public bool InitialFixture(int cardId)
        {
            CardId = cardId;
            int boardIdInBits = 0;
            int ret = Class_8338.Initial(CardId);//初始化
            if (ret != 0)
            {
                Class_8338.Close();
                return false;
            }

            ret = Class_8338.StartFieldBUS(CardId, ref StartAxisId);
            if (ret < 0)//连接失败
            {
                return false;
            }

            if (File.Exists(txtXmlFilename))
            {
                ret = Class_8338.LoadXMLFile(txtXmlFilename);
                if (ret != 0)
                {
                    return false;
                }

            }

            IsInitialed = true;
            return true;
        }

        public void GetPostionAbs(ref double x, ref double y)
        {
            APS168.APS_get_position_f(_selectAxisX, ref x);
            APS168.APS_get_position_f(_selectAxisY, ref y);
        }

        public void ServoOn(bool onOff)
        {
            if (IsInitialed == false) return;

            int ret = Class_8338.Servo(_selectAxisX);
            if (ret < 0)
            {
                System.Windows.Forms.MessageBox.Show("ServerOn _selectAxisX fail");
            }

            ret = Class_8338.Servo(_selectAxisY);
            if (ret < 0)
            {
                System.Windows.Forms.MessageBox.Show("ServerOn _selectAxisY fail");
            }

        }

        public void HomeXY()
        {
            //_selectAxis = Convert.ToInt32(cmbSelectAxis.SelectedItem);
            int homeMode = 0;
            int homeDir = 1;
            double praCurve = 0.5;
            double praAcc = 100000;
            double praVmX = 5000;
            double praVmY = 20000;

            double curX, curY;
            Task taskX = null;
            Task taskY = null;
            curX = -1;
            curY = -1;
            if (IsInitialed == false) return;

            GetPostionAbs(ref curX, ref curY);

            if(curX != 0 || curY != 0)
            {
                taskX = new Task(() => Class_8338.StartHoming(_selectAxisX, homeMode, homeDir, praCurve, praAcc, praVmX));
                taskY = new Task(() => Class_8338.StartHoming(_selectAxisY, homeMode, homeDir, praCurve, praAcc, praVmY));
                taskX.Start();
                taskY.Start();
            }

        }

        public void MoveRelative(int x, int y)
        {

            int[] Axis_ID = new int[2] { _selectAxisX, _selectAxisY };
            double[] Position = new double[2] { x, y };

            Class_8338.Interpolation_2D_line_move(Axis_ID, Position, true);

        }

        public void MovePT_Line(int x, int y)
        {
            int[] Axis_ID = new int[2] { _selectAxisX, _selectAxisY };
            double[] Position = new double[2] { x, y };

            Class_8338.Interpolation_2D_line_move(Axis_ID, Position,false);

        }
        public void GetLineValues(int pxStart,int pyStart,int pxEnd,int pyEnd,ref List<LineData> data)
        {
            LineData tempDate = new LineData();
            int[] Axis_ID = new int[2] { _selectAxisX, _selectAxisY };
            double[] PositionStart = new double[2] { pxStart, pyStart };
            double[] PositionEnd = new double[2] { pxEnd, pyEnd };

            Class_8338.Interpolation_2D_line_move(Axis_ID, PositionStart, false);
            //add start
            tempDate.X = pxStart;
            tempDate.Y = pyStart;
            tempDate.Z = ReadSickValue();
            data.Add(tempDate);

            Class_8338.Interpolation_2D_line_moveNowait(Axis_ID, PositionEnd, false);
            int motionStatusMdn = 5;
            while ((APS168.APS_motion_status(Axis_ID[0]) & 1 << motionStatusMdn) == 0 && (APS168.APS_motion_status(Axis_ID[1]) & 1 << motionStatusMdn) == 0)
            {
                Thread.Sleep(300);
                tempDate.Z = ReadSickValue();
                GetPostionAbs(ref tempDate.X, ref tempDate.Y);
                data.Add(tempDate);
            }
            //add end
            tempDate.X = pxEnd;
            tempDate.Y = pyEnd;
            tempDate.Z = ReadSickValue();
            data.Add(tempDate);

        }

        private void JOGAX()
        { 
        }

        private void JOGAY()
        {

        }

        public void SickLaserPowerOnOff(bool onOff)
        {
            System.UInt32 rawData = 0x00;

            if(onOff)
            {
                rawData = 0x01;
            }

            int ret = APS168.APS_set_field_bus_od_data(0,
                                       0,
                                       2,
                                       3,
                                       0,
                                       rawData
                                       );
        }
        public double ReadSickValue()
        {
            double AnalogValue = 0;
            const int an_subNo = 5;
            const int an_channel = 0;
            int Analog = 0;
            uint value = 0;
            int ODIndex;//索引
            int live;

            if (IsInitialed == false) return -1;

            Class_8338.GetSlaveOnlineStatus(CardId, MOD_No_AI, out live);

            if(live == 33)
            {
                ODIndex = decimal.ToInt16(an_channel);
                Class_8338.GetInputStatus(CardId, MOD_No_AI, an_subNo, ODIndex, ref value);
                if (IsNBitOne(value, 16))//判断 16bit 位是不是 1
                {
                    Analog = ((int)value ^ (int)(Math.Pow(2, 15) - 1)) + 1;//如果第 16 位是1，则电压值为 负值 ，先去除符号位
                    AnalogValue = ((Analog - Math.Pow(2, 15)) / (Math.Pow(2, 15) - 1)) * (-10);//计算实际电压
                }
                else
                    AnalogValue = (value / (Math.Pow(2, 15))) * 10;

                AnalogValue = AnalogValue * (-103.7) + 2.1;

                AnalogValue = 794.49 - AnalogValue;


            }

            return AnalogValue;
        }
        private bool IsNBitOne(uint Num, int n)
        {
            bool Result = false;
            //uint str = Convert.ToString(Num,2);
            uint bit = (Num >> (n - 1)) & 1;
            Result = bit == 1;

            return Result;
        }

        public void Scan()
        {

            int Y_Start = 0;
            int Y_End = 300000;
            double Acc = 80000.0;//设置加速度
            double Dec = 80000.0;//设置减速度
            int Vm = 100000;     //设置最大速度

            // move to origial postion
            MovePT_Line(0, 0);

            //open scanner
            m_objScanner.StartGetPoint();
            int ret = Class_8338.AbsluteMove(_selectAxisY, Y_End, Acc, Dec, Vm);//开始绝对

            m_objScanner.StopGetPoint();
            ret = Class_8338.AbsluteMove(_selectAxisY, Y_Start, Acc, Dec, Vm);//开始绝对运动
           

        }
    }
}
