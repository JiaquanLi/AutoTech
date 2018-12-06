using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HslCommunication.Profinet;
using System.Threading;
using HslCommunication.Profinet.Melsec;
using HslCommunication;
namespace AutoTech
{
    public class clsFixture
    {
        public struct stComPort
        {
            public string strComPortNumber;
            public int iBaundRate;
            public int iDataBit;
            public int iStopBit;
            public string strParity;
        }

        public enum en_Postion
        {
            _HomeX,
            _HomeY,
            _HomeXY,
            _PostionX,
            _PostionY,
            _PostionXY,
        }

        public struct st_AddressX
        {
            public const string _Home         = "M30";
            public const string _JogAdd       = "M400";
            public const string _JogMinus     = "M401";
            public const string _JogSpeed     = "D114";
            public const string _LocSet       = "D180";
            public const string _EnLocMove    = "M120";
            public const string _LocMoveSpeed = "D116";
            public const string _CurLoc       = "D178";
        }

        public struct st_AddressY
        {
            public const string _Home         = "M40";
            public const string _JogAdd       = "M410";
            public const string _JogMinus     = "M411";
            public const string _JogSpeed     = "D124";
            public const string _LocSet       = "D230";
            public const string _EnLocMove    = "M220";
            public const string _LocMoveSpeed = "D126";
            public const string _CurLoc       = "D220";
        }

        private MelsecFxSerial melsecSerial = null;
        private stComPort m_objComPort ;

        public stComPort ComPort
        {
            get
            {
                return m_objComPort;
            }
            set
            {
                m_objComPort = value;
            }
        }

        public clsFixture()
        {
            melsecSerial = new MelsecFxSerial();
        }

        ~clsFixture()
        {
            if (melsecSerial != null)
                melsecSerial.Close();
        }

        public bool InitFixture()
        {
            try
            {
                melsecSerial.SerialPortInni(sp =>
                {
                    sp.PortName = m_objComPort.strComPortNumber;
                    sp.BaudRate = m_objComPort.iBaundRate;
                    sp.DataBits = m_objComPort.iDataBit;
                    sp.StopBits = m_objComPort.iStopBit == 0 ? System.IO.Ports.StopBits.None : (m_objComPort.iStopBit == 1 ? System.IO.Ports.StopBits.One : System.IO.Ports.StopBits.Two);
                    sp.Parity = m_objComPort.strParity.ToUpper() == "NONE" ? System.IO.Ports.Parity.None : (m_objComPort.strParity.ToUpper().ToUpper() == "ODD" ? System.IO.Ports.Parity.Odd : System.IO.Ports.Parity.Even);
                });

                melsecSerial.Open();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }


        public void MoveToPostion(en_Postion postion, int xPostion =0, int yPostion =0)
        {
            switch(postion)
            {
                case en_Postion._HomeXY:
                    try
                    {
                        writeResultRender(melsecSerial.Write(st_AddressX._Home, true), st_AddressX._Home);
                        writeResultRender(melsecSerial.Write(st_AddressY._Home, true), st_AddressY._Home);
                        System.Threading.Thread.Sleep(500);
                        writeResultRender(melsecSerial.Write(st_AddressX._Home, false), st_AddressX._Home);
                        writeResultRender(melsecSerial.Write(st_AddressY._Home, false), st_AddressY._Home);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                    }
                    break;

                case en_Postion._HomeX:
                    try
                    {
                        writeResultRender(melsecSerial.Write(st_AddressX._Home, true), st_AddressX._Home);
                        System.Threading.Thread.Sleep(500);
                        writeResultRender(melsecSerial.Write(st_AddressX._Home, false), st_AddressX._Home);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                    }
                    break;
                case en_Postion._HomeY:
                    try
                    {
                        writeResultRender(melsecSerial.Write(st_AddressY._Home, true), st_AddressY._Home);
                        System.Threading.Thread.Sleep(500);
                        writeResultRender(melsecSerial.Write(st_AddressY._Home, false), st_AddressY._Home);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                    }
                    break;

                case en_Postion._PostionXY:
                    try
                    {
                        writeResultRender(melsecSerial.Write(st_AddressX._LocSet, (int)xPostion), st_AddressX._LocSet);
                        writeResultRender(melsecSerial.Write(st_AddressY._LocSet, (int)yPostion), st_AddressY._LocSet);
                        System.Threading.Thread.Sleep(200);
                        writeResultRender(melsecSerial.Write(st_AddressX._EnLocMove, true), st_AddressX._Home);
                        writeResultRender(melsecSerial.Write(st_AddressY._EnLocMove, true), st_AddressY._Home);
                        System.Threading.Thread.Sleep(200);
                        writeResultRender(melsecSerial.Write(st_AddressX._EnLocMove, false), st_AddressX._Home);
                        writeResultRender(melsecSerial.Write(st_AddressY._EnLocMove, false), st_AddressY._Home);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                    }
                    break;
                case en_Postion._PostionX:
                    try
                    {

                        writeResultRender(melsecSerial.Write(st_AddressX._LocSet, xPostion), st_AddressX._LocSet);
                        System.Threading.Thread.Sleep(100);
                        writeResultRender(melsecSerial.Write(st_AddressX._EnLocMove, true), st_AddressX._EnLocMove);
                        System.Threading.Thread.Sleep(200);
                        writeResultRender(melsecSerial.Write(st_AddressX._EnLocMove, false), st_AddressX._EnLocMove);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                    }
                    break;

                case en_Postion._PostionY:
                    try
                    {
                        writeResultRender(melsecSerial.Write(st_AddressY._LocSet, yPostion), st_AddressY._LocSet);
                        System.Threading.Thread.Sleep(100);
                        writeResultRender(melsecSerial.Write(st_AddressY._EnLocMove, true), st_AddressY._EnLocMove);
                        System.Threading.Thread.Sleep(200);
                        writeResultRender(melsecSerial.Write(st_AddressY._EnLocMove, false), st_AddressY._EnLocMove);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                    }
                    break;
                default:
                    break;

            }
        }


        /// <summary>
        /// 统一的数据写入的结果显示
        /// </summary>
        /// <param name="result"></param>
        /// <param name="address"></param>
        private void writeResultRender(OperateResult result, string address)
        {
            if (result.IsSuccess)
            {
                //MessageBox.Show(DateTime.Now.ToString("[HH:mm:ss] ") + $"[{address}] 写入成功");
                
            }
            else
            {
                //MessageBox.Show(DateTime.Now.ToString("[HH:mm:ss] ") + $"[{address}] 写入失败{Environment.NewLine}原因：{result.ToMessageShowString()}");
            }
        }

    }
}
