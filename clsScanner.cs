using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Lmi3d.GoSdk;
using Lmi3d.Zen;
using Lmi3d.Zen.Io;
using Lmi3d.GoSdk.Messages;
using System.Runtime.InteropServices;


namespace LmiScanner
{
    public class clsScanner
    {
        public delegate void OnMessageCallback(string message);
        public static event OnMessageCallback Callback;

        private bool bDataSaved;

        public bool IsDataSaved
        {
            get
            {
                return bDataSaved;
            }
        }

        GoSystem system;
        GoDataSet dataSet;
        GoSensor sensor;

        private const int port = 1000;
        private const string ip = "192.168.11.13";
        private const string strFileSave = "model.txt";

        //double dSpeed = ReadIni.objIniValue.iniScanner.speed;//365 / 5;   //单位：mm      5910/87
        //double dTime = ReadIni.objIniValue.iniScanner.frequency;//0.01;  //单位：s
        double offset = 0;   //-41.0; // mm
        private double length = 0;

        public class DataContext
        {
            public Double xResolution;
            public double yResolution;
            public Double zResolution;
            public Double xOffset;
            public double yOffset;
            public Double zOffset;
        }

        public struct ProfilePoint
        {
            public double x;
            public double y;
            public double z;
        }

        public struct GoPoints
        {
            public Int16 x;
            public Int16 y;
        }
        public struct point
        {
            public double z;
        }
        public clsScanner()
        {
            Callback("start connect scanner");
            KApiLib.Construct();
            GoSdkLib.Construct();
            system = new GoSystem();
            KIpAddress ipAddress = KIpAddress.Parse(ip);

            dataSet = new GoDataSet();
            try
            {
                sensor = system.FindSensorByIpAddress(ipAddress);
            }
            catch (Exception ex)
            {
                Callback("scanner connect failed");
                return;
            }

            sensor.Connect();
            Callback("scanner connected.");
        }

        ~clsScanner()
        {
  
        }
        public void StartGetPoint()
        {
            if (File.Exists(strFileSave) == true) File.Delete(strFileSave);
            length = 0;

            system.EnableData(true);
            system.SetDataHandler(onData);

            try
            {
                sensor.CopyFile("test.job", "_live.job");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("请检查相机连接状态");
                return;
            }
            system.Start();
        }
        public void StopGetPoint()
        {
            system.Stop();
        }

        public void onData(KObject data)
        {
            bDataSaved = false;

            double yPoint = 1;
            string strWrite = "";
            bool length_incr = true;
            GoDataSet dataSet = (GoDataSet)data;
            DataContext context = new DataContext();
            for (UInt32 i = 0; i < dataSet.Count; i++)
            {
                GoDataMsg dataObj = (GoDataMsg)dataSet.Get(i);
                switch (dataObj.MessageType)
                {
                    case GoDataMessageType.Stamp:
                        {
                            GoStampMsg stampMsg = (GoStampMsg)dataObj;
                            for (UInt32 j = 0; j < stampMsg.Count; j++)
                            {
                                GoStamp stamp = stampMsg.Get(j);
                                //Console.WriteLine("Frame Index = {0}", stamp.FrameIndex);
                                //Console.WriteLine("Time Stamp = {0}", stamp.Timestamp);
                                //Console.WriteLine("Encoder Value = {0}", stamp.Encoder);
                            }
                        }
                        break;
                    case GoDataMessageType.Surface:  // Surface
                        {
                            GoSurfaceMsg surfaceMsg = (GoSurfaceMsg)dataObj;
                            long width = surfaceMsg.Width;
                            long height = surfaceMsg.Length;
                            long bufferSize = width * height;
                            IntPtr bufferPointer = surfaceMsg.Data;

                            short[] ranges = new short[bufferSize];
                            Marshal.Copy(bufferPointer, ranges, 0, ranges.Length);

                            context.xResolution = (double)surfaceMsg.XResolution / 1000000.0;
                            context.zResolution = (double)surfaceMsg.ZResolution / 1000000.0;
                            context.yResolution = (double)surfaceMsg.YResolution / 1000000.0;
                            context.yOffset = (double)surfaceMsg.YOffset / 1000.0;
                            context.xOffset = (double)surfaceMsg.XOffset / 1000.0;
                            context.zOffset = (double)surfaceMsg.ZOffset / 1000.0;

                            double phy_x;
                            double phy_y;
                            double phy_z;

                            FileStream fs = new FileStream(strFileSave, FileMode.Create);
                            StreamWriter sw = new StreamWriter(fs);

                            for (int m = 0; m < height; m++)
                            {
                                for (int j = 0; j < width; j++)
                                {
                                    phy_z = ranges[m * width + j] * context.zResolution + context.zOffset;
                                    if (/*phy_z > 20*/true)  //这个过滤阈值根据实际情况选取
                                    {
                                        phy_x = j * context.xResolution + context.xOffset;
                                        phy_y = m * context.yResolution + context.yOffset;

                                        strWrite = string.Format("{0} {1} {2}", phy_x, phy_y, phy_z);
                                        sw.WriteLine(strWrite);
                                    }
                                }
                            }
                            sw.Flush();
                            //关闭流
                            sw.Close();
                            fs.Close();
                        }
                        break;

                    case GoDataMessageType.Profile:
                        {
                            StreamWriter write = new StreamWriter(strFileSave, true);

                            GoProfileMsg profileMsg = (GoProfileMsg)dataObj;
                            Console.WriteLine("  Profile Message batch count: {0}", profileMsg.Count);
                            for (UInt32 k = 0; k < profileMsg.Count; ++k)
                            {
                                int validPointCount = 0;
                                long profilePointCount = profileMsg.Width;
                                Console.WriteLine("  Item[{0}]: Profile data ({1} points)", i, profileMsg.Width);
                                context.xResolution = (profileMsg.XResolution / 1000000.0);
                                context.zResolution = profileMsg.ZResolution / 1000000.0;
                                context.xOffset = profileMsg.XOffset / 1000.0;
                                context.zOffset = profileMsg.ZOffset / 1000.0;
                                GoPoints[] points = new GoPoints[profilePointCount];
                                point[] point111 = new point[profilePointCount];
                                ProfilePoint[] profileBuffer = new ProfilePoint[profilePointCount];
                                int structSize = Marshal.SizeOf(typeof(GoPoints));
                                IntPtr pointsPtr = profileMsg.Data;
                                for (UInt32 array = 0; array < profilePointCount; ++array)
                                {
                                    IntPtr incPtr = new IntPtr(pointsPtr.ToInt64() + array * structSize);
                                    points[array] = (GoPoints)Marshal.PtrToStructure(incPtr, typeof(GoPoints));

                                    double real_x = (context.xOffset + context.xResolution * points[array].x);
                                    double real_z = (context.zOffset + context.zResolution * points[array].y);

                                    if (length_incr == true)
                                    {
                                        length += 1;//ReadIniSettings.ReadIni.objIniValue.iniScanner.step;
                                        length_incr = false;
                                    }

                                    if (real_z > 60  && real_z < 800)
                                    {
                                        write.WriteLine(real_x + " " + length + " " + real_z);
                                    }
                                }

                                write.Flush();
                            }
                            write.Close();

                        }
                        break;

                    case GoDataMessageType.ProfileIntensity:
                        {
                            GoProfileIntensityMsg profileMsg = (GoProfileIntensityMsg)dataObj;
                            Console.WriteLine("  Profile Intensity Message batch count: {0}", profileMsg.Count);
                            for (UInt32 k = 0; k < profileMsg.Count; ++k)
                            {
                                byte[] intensity = new byte[profileMsg.Width];
                                IntPtr intensityPtr = profileMsg.Data;
                                Marshal.Copy(intensityPtr, intensity, 0, intensity.Length);
                            }
                        }
                        break;

                    case GoDataMessageType.Measurement:     // Measurement
                        {
                            GoMeasurementMsg measurementMsg = (GoMeasurementMsg)dataObj;
                        }
                        break;
                }
            }

            bDataSaved = true;
        }
    }
}