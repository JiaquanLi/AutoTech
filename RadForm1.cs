using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Basler.Pylon;
using GxMultiCam;
using Telerik.WinControls.UI;
using System.Threading;
using LmiScanner;
using System.IO;




namespace AutoTech
{
    public partial class RadForm1 : Telerik.WinControls.UI.RadForm
    {

        public delegate void DeShowLog(string strLog);

        frmAboutBox objfrmAbout;                                              /// <关于对话框>

        List<CCamerInfo> m_listCCamerInfo = new List<CCamerInfo>();           ///相机参数状态列表
        List<ICameraInfo> m_listallCameras = new List<ICameraInfo>();
        private clsFixture m_objFixture = null;
        private clsFixtureAMP204 m_objFixtureAMP204;
        private clsFixture8338 m_objFixture8338;
        private CCBasler m_objCameraLocl;
        private System.Windows.Forms.PictureBox pbxLocal;
        clsFixture.stComPort m_objComPort;
        private clsScanner m_objScanner;

        struct Line
        {
            public Point start;
            public Point end;
        }

        private List<Line> lstLines = new List<Line>();
        private Line m_Line;


        public RadForm1()
        {
            InitializeComponent();

            this.radTreeView_Devices.AllowEdit = false;
            this.radTreeView_Devices.AllowAdd = false;
            this.radTreeView_Devices.AllowRemove = false;
            this.radTreeView_Devices.AllowDefaultContextMenu = true;

        }

        private void RadForm1_Load(object sender, EventArgs e)
        {
            
            InitialHal();
            UpdateDeviceTree();
            InitListLines();

        }

        private void pb_image_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void radMenuItemAbout_Click(object sender, EventArgs e)
        {
            objfrmAbout = new frmAboutBox();
            objfrmAbout.Show();
        }

        private void UpdateTreeViewIco(string funtionClick)
        {
            //Connect
            
            RadTreeNode treeNodeConnect = radTreeView_Devices.GetNodeByName(CCBasler.ConnectionType._Connect);
            RadTreeNode treeNodeDisConnect = radTreeView_Devices.GetNodeByName(CCBasler.ConnectionType._DisConnect);
            RadTreeNode treeNodeOneShot = radTreeView_Devices.GetNodeByName(CCBasler.ConnectionType._OneShot);
            RadTreeNode treeNodeContinusShot = radTreeView_Devices.GetNodeByName(CCBasler.ConnectionType._ContinusShot);
            RadTreeNode treeNodeStopContinusShot = radTreeView_Devices.GetNodeByName(CCBasler.ConnectionType._StopContinusShot);
            RadTreeNode treeNodeStopConfiguration = radTreeView_Devices.GetNodeByName(CCBasler.ConnectionType._Configuration);


            switch (funtionClick)
            {
                case CCBasler.ConnectionType._Connect:
                    treeNodeConnect.ImageIndex = 1; //连接按键

                    treeNodeOneShot.Enabled = true;
                    treeNodeContinusShot.Enabled = true;
                    treeNodeDisConnect.Enabled = true;
                    treeNodeStopConfiguration.Enabled = true;
                    break;

                case CCBasler.ConnectionType._DisConnect:
                    treeNodeConnect.ImageIndex = 2; //连接按键
                    treeNodeDisConnect.Enabled = false;

                    treeNodeOneShot.Enabled = false;
                    treeNodeContinusShot.Enabled = false;
                    treeNodeConnect.Enabled = true;
                    treeNodeStopConfiguration.Enabled = false;
                    break;

                case CCBasler.ConnectionType._ContinusShot://关闭 onshot 打开 StopContinusShot

                    treeNodeStopContinusShot.Enabled = true;
                    treeNodeContinusShot.Enabled = false;
                    treeNodeOneShot.Enabled = false;
                    break;

                case CCBasler.ConnectionType._StopContinusShot://关闭 onshot 打开 StopContinusShot

                    treeNodeStopContinusShot.Enabled = false;
                    treeNodeContinusShot.Enabled = true;
                    treeNodeOneShot.Enabled = true;
                    break;
                default:
                    break;
            }


        }
        private void radTreeView_Devices_NodeMouseClick(object sender, RadTreeViewEventArgs e)
        {
            CCamerInfo objCCamerInfo = null;

            string  strCameralName ;
            string strFtName ;
            

            if (e.Node.Parent == null) return;

            strCameralName = e.Node.Parent.Text;
            strFtName = e.Node.Text.ToString();

            for (int i = 0; i < m_listCCamerInfo.Count; i++)
            {
                if(m_listCCamerInfo[i].m_objCameraInfo[CameraInfoKey.FriendlyName] == strCameralName)
                {
                    objCCamerInfo = m_listCCamerInfo[i];
                }
            }

            if (objCCamerInfo == null) return;

            switch(strFtName)
            {
                case CCBasler.ConnectionType._Connect:
                    if (objCCamerInfo.m_Flag == -1)
                    { 
                        objCCamerInfo.m_Flag = 0;
                        if(__SelectDeviceAndShow(objCCamerInfo, objCCamerInfo.m_objCameraInfo) == false)
                        {
                            return;
                        }
                        objCCamerInfo.m_bIsOpen = true;
                        e.Node.ImageIndex = 1;
                        UpdateTreeViewIco(strFtName);
                    }

                    break;

                case CCBasler.ConnectionType._DisConnect:
                    if (objCCamerInfo.m_Flag != -1)
                    {
                        objCCamerInfo.m_objBasler.Stop();
                        objCCamerInfo.m_objBasler.DestroyCamera();
                        objCCamerInfo.m_objImageShowFrom.Hide();
                        objCCamerInfo.m_Flag = -1;
                        objCCamerInfo.m_bIsOpen = false;
                        UpdateTreeViewIco(strFtName);
                    }
                    break;

                case CCBasler.ConnectionType._ContinusShot:
                    if (objCCamerInfo.m_Flag == -1)
                    {
                        MessageBox.Show("相机没有连接", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }

                    if (objCCamerInfo.m_bIsSnap == true) return;

                    objCCamerInfo.m_objBasler.ContinuousShot();
                    objCCamerInfo.m_bIsSnap = true;
                    UpdateTreeViewIco(strFtName);

                    break;

                case CCBasler.ConnectionType._StopContinusShot:
                    if (objCCamerInfo.m_Flag == -1)
                    {
                        MessageBox.Show("相机没有连接", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }

                    objCCamerInfo.m_objBasler.Stop();
                    objCCamerInfo.m_bIsSnap = false;
                    UpdateTreeViewIco(strFtName);
                    break;

                case CCBasler.ConnectionType._OneShot:
                    if (objCCamerInfo.m_Flag == -1)
                    {
                        MessageBox.Show("相机没有连接", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }

                    if(objCCamerInfo.m_bIsSnap == true)
                    {
                        MessageBox.Show("连续模式下拍照功能禁用", "Warnning", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                        return;
                    }
                    objCCamerInfo.m_objBasler.OneShot();
                    break;

                default:
                    break;
            }

        }
        private void UpdateDeviceTree()
        {
            try
            {
                m_listallCameras = CCBasler.GetDeviceList();

                foreach (ICameraInfo cameraInfo in m_listallCameras)
                {
                    bool newitem = true;
                    foreach (RadTreeNode radTreeNode in this.radTreeView_Devices.Nodes)
                    {
                        if(radTreeNode.Name == cameraInfo[CameraInfoKey.FriendlyName] || cameraInfo[CameraInfoKey.FriendlyName].Contains("22546306"))
                        {
                            newitem = false;
                            break;
                        }
                        
                    }

                    if(newitem)
                    {
                        RadTreeNode root = this.radTreeView_Devices.Nodes.Add(cameraInfo[CameraInfoKey.FriendlyName], 0);
                        root.Nodes.Add(CCBasler.ConnectionType._Connect, 2);
                        root.Nodes.Add(CCBasler.ConnectionType._DisConnect, 3);
                        root.Nodes.Add(CCBasler.ConnectionType._OneShot, 3);
                        root.Nodes.Add(CCBasler.ConnectionType._ContinusShot, 3);
                        root.Nodes.Add(CCBasler.ConnectionType._StopContinusShot, 3);
                        root.Nodes.Add(CCBasler.ConnectionType._Configuration, 3);

                        RadTreeNode treeNodeConnect = radTreeView_Devices.GetNodeByName(CCBasler.ConnectionType._Connect);
                        treeNodeConnect.Enabled = true;

                        RadTreeNode treeNodeDisConnect = radTreeView_Devices.GetNodeByName(CCBasler.ConnectionType._DisConnect);
                        treeNodeDisConnect.Enabled = false;

                        RadTreeNode treeNodeOneShot = radTreeView_Devices.GetNodeByName(CCBasler.ConnectionType._OneShot);
                        treeNodeOneShot.Enabled = false;

                        RadTreeNode treeNodeContinusShot = radTreeView_Devices.GetNodeByName(CCBasler.ConnectionType._ContinusShot);
                        treeNodeContinusShot.Enabled = false;

                        RadTreeNode treeNodeStopContinusShot = radTreeView_Devices.GetNodeByName(CCBasler.ConnectionType._StopContinusShot);
                        treeNodeStopContinusShot.Enabled = false;

                        RadTreeNode treeNodeConfiguration = radTreeView_Devices.GetNodeByName(CCBasler.ConnectionType._Configuration);
                        treeNodeConfiguration.Enabled = false;

                        CCamerInfo objCCamerInfo = new CCamerInfo();
                        frmShowImage objImageShowFrom = new frmShowImage();
                        //objImageShowFrom.Fixture = m_objFixture;
                        //objImageShowFrom.Fixtureapm204 = m_objFixtureAMP204;
                        objImageShowFrom.Fixture = m_objFixture8338;
                        objImageShowFrom.BaslerLocalCamera = m_objCameraLocl;
                        objCCamerInfo.m_objImageShowFrom = objImageShowFrom;

                        System.Windows.Forms.PictureBox pbx = objImageShowFrom.PbxShowImage;
                        CCBasler objBasler = new CCBasler(ref pbx);
                        objCCamerInfo.m_objBasler = objBasler;

                        objCCamerInfo.m_objCameraInfo = cameraInfo;

                        //add name
                        objCCamerInfo.m_strDisplayName = cameraInfo[CameraInfoKey.FriendlyName];
                        m_listCCamerInfo.Add(objCCamerInfo);
                    }
                }

                // Remove old camera devices that have been disconnected.
                List<RadTreeNode> lstRadTreeNodeRemove = new List<RadTreeNode>();
                List<ICameraInfo> lstCameraInfoRemove = new List<ICameraInfo>();
                List<CCamerInfo> lstCameraRemove = new List<CCamerInfo>();
                foreach (RadTreeNode radTreeNode in this.radTreeView_Devices.Nodes)
                {
                    bool exists = false;
                    ICameraInfo tempCamInfo = null;

                    // For each camera in the list, check whether it can be found by enumeration.
                    foreach (ICameraInfo cameraInfo in m_listallCameras)
                    {

                        tempCamInfo = cameraInfo;
                        if (radTreeNode.Name == cameraInfo[CameraInfoKey.FriendlyName])
                        {
                            exists = true;
                            break;
                        }

                    }
                    // If the camera has not been found, remove it from the list view.
                    if (!exists)
                    {
                        lstRadTreeNodeRemove.Add(radTreeNode);
                        //lstCameraInfoRemove.Add(tempCamInfo);
                    }
                }

                if(lstRadTreeNodeRemove.Count > 0)
                {
                    foreach(RadTreeNode rdTreeNode in lstRadTreeNodeRemove)
                    {
                        this.radTreeView_Devices.Nodes.Remove(rdTreeNode);
                    }
                }

                if(lstRadTreeNodeRemove.Count > 0)
                {
                    foreach (RadTreeNode rdTreeNode in lstRadTreeNodeRemove)
                    {
                        foreach(CCamerInfo cmInfo in m_listCCamerInfo)
                        {
                            if (cmInfo.m_objCameraInfo[CameraInfoKey.FriendlyName] == rdTreeNode.Text)
                            {
                                //获取需要删除的 相机
                                lstCameraRemove.Add(cmInfo);
                            }
                        }
                    }

                    foreach (CCamerInfo cmInfoForRemove in lstCameraRemove)
                    {
                        cmInfoForRemove.m_objBasler.Stop();
                        cmInfoForRemove.m_objBasler.DestroyCamera();
                        cmInfoForRemove.m_objImageShowFrom.Close();
                        m_listCCamerInfo.Remove(cmInfoForRemove);

                    }
                }

            }
            catch (Exception exception)
            {
                ShowException(exception);
            }

           
            this.radTreeView_Devices.Nodes.Refresh();
        }
        /// <summary>
        /// 根据对应的设备对象绑定显示控件
        /// </summary>
        /// <param name="nCamID">相机ID</param>
        private bool __SelectDeviceAndShow(CCamerInfo camera, ICameraInfo cameralInfo)
        {

            bool b_sts = false;
            camera.m_objBasler.SetDeviceInfo(cameralInfo);
            b_sts = camera.m_objBasler.ConnectToDevice();
            if (b_sts == false) return false;

            camera.m_objImageShowFrom.TitleText = camera.m_strDisplayName;
            camera.m_objImageShowFrom.ImageHeight = camera.m_objBasler.GetCameralImageHeight();
            camera.m_objImageShowFrom.ImageWidth = camera.m_objBasler.GetCameralImageWidth();

            camera.m_objImageShowFrom.TopLevel = false;
            camera.m_objImageShowFrom.Parent = this.panel_image;
            camera.m_objImageShowFrom.StartPosition = FormStartPosition.Manual;
            camera.m_objImageShowFrom.Location = new Point(0, 0);
            camera.m_objImageShowFrom.Show();

            return b_sts;
        }

        private void InitListLines()
        {

            this.lstBox_Lines.Columns.Add("",30,HorizontalAlignment.Left);
            this.lstBox_Lines.Columns.Add("起点", 100, HorizontalAlignment.Left);
            this.lstBox_Lines.Columns.Add("终点", 100, HorizontalAlignment.Left);
            this.lstBox_Lines.View = View.Details;
            this.lstBox_Lines.GridLines = true;
        }
        private void InitialHal()
        {
            m_objComPort = new clsFixture.stComPort();
            if (Properties.Settings.Default.PLC_ComNum == "")
            {
                MessageBox.Show("Com Number 输入错误！");
                return;
            }
            m_objComPort.strComPortNumber = Properties.Settings.Default.PLC_ComNum;
            m_objComPort.iBaundRate = Properties.Settings.Default.PLC_BaudRate;
            m_objComPort.iDataBit = Properties.Settings.Default.PLC_DataBit;
            m_objComPort.iStopBit = Properties.Settings.Default.PLC_StopBit;
            m_objComPort.strParity = Properties.Settings.Default.PLC_Parity;

            //m_objFixtureAMP204 = new clsFixtureAMP204();
            //if (m_objFixtureAMP204.InitialFixture(Properties.Settings.Default.Card204, 0) == false)
            //{
            //    MessageBox.Show("APM 204 card initial fail 通信初始化失败!!!");
            //    ShowLog("APM 204 初始化失败");
            //    //return;
            //}
            //else
            //{
            //    ShowLog("APM 204 初始化成功");
            //}

            //m_objFixtureAMP204.ServoOn(true);
            //m_objFixtureAMP204.HomeXY();

            //scanner
            try
            {
                clsScanner.Callback += new clsScanner.OnMessageCallback(ShowLog);
                m_objScanner = new clsScanner();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            m_objFixture8338 = new clsFixture8338();
            if(m_objFixture8338.InitialFixture(Properties.Settings.Default.Card8338ID) == false)
            {
                MessageBox.Show("card 8338  initial fail 通信初始化失败!!!");
                ShowLog("PCIE-8338 初始化失败");
                //return;
            }
            else
            {
                ShowLog("PCIE-8338 初始化成功");
            }

            m_objFixture8338.SickLaserPowerOnOff(true);
            m_objFixture8338.Scanner = m_objScanner;
            m_objFixture8338.ServoOn(true);
            m_objFixture8338.HomeXY();

            //frmShowImage objImageShowFromLocal = new frmShowImage();
            //pbxLocal = objImageShowFromLocal.PbxShowImage;
            m_objCameraLocl = new CCBasler();
            

            List<ICameraInfo> listCam = CCBasler.GetDeviceList();

            bool findLocalCam = false;
            foreach (ICameraInfo cam in listCam)
            {

                if (cam[CameraInfoKey.FriendlyName].Contains(Properties.Settings.Default.Camera_LocalSN))
                {
                    findLocalCam = true;
                    m_objCameraLocl.SetDeviceInfo(cam);
                    m_objCameraLocl.ConnectToDevice();
                    m_objCameraLocl.SetExposure(Properties.Settings.Default.Camera_LocalExp);

                    break;
                }
            }

            if (findLocalCam == false)
            {
                MessageBox.Show("局部相机 通信初始化失败!!!");
                ShowLog("未能找到局部相机 ID：" + Properties.Settings.Default.Camera_LocalSN);
                //return;
            }


            //remove plc
            //m_objFixture = new clsFixture();
            //m_objFixture.ComPort = m_objComPort;
            //if (m_objFixture.InitFixture() == false)
            //{
            //    MessageBox.Show("PLC 通信初始化失败!!!");
            //    ShowLog("PLC 通信失败");
            //    //return;
            //}



        }
        private void ShowException(Exception exception)
        {
            System.Windows.Forms.MessageBox.Show("Exception caught:\n" + exception.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        private void RadForm1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //for (int i = 0; i < m_listCCamerInfo.Count; i++)
            //{
            //    if (m_listCCamerInfo[i].m_Flag != -1)
            //    {
            //        m_listCCamerInfo[i].m_objBasler.DestroyCamera();
            //    }

            //}
        }

        private void radMenuItem_DevicesRefresh_Click(object sender, EventArgs e)
        {
            UpdateDeviceTree();
        }

        private void radMenuItemRecover_Click(object sender, EventArgs e)
        {
            tw_Camera.Show();
            tw_Devices.Show();
            tw_GlobalImage.Show();
            tw_log.Show();
        }

        private void radMenuItem_ConfigSetting_Click(object sender, EventArgs e)
        {
            frmConfig fmConfig = new frmConfig();
            fmConfig.Fixture = m_objFixture8338;
            //fmConfig.FixtureAMP204 = m_objFixtureAMP204;
            fmConfig.ShowDialog();
        }

        private void radTreeView_Devices_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            RadTreeNode rootNodeSel;
            rootNodeSel = e.Node.RootNode;

            //get camera

            foreach (CCamerInfo cmInfo in m_listCCamerInfo)
            {
                if (cmInfo.m_objCameraInfo[CameraInfoKey.FriendlyName] == rootNodeSel.Text)
                {
                    if (e.Node.Text == CCBasler.ConnectionType._Configuration)
                    {
                        //cmInfo.m_objBasler.SetParameter(ref this.trackbar_gain, ref this.trackbar_exposure,ref trackbar_width,ref trackbar_height);
                        cmInfo.m_objBasler.SetExposure(Properties.Settings.Default.Camera_GlobalExp);
                    }
                }
            }
        }


        private void radMenuItem_SaveImage_Click(object sender, EventArgs e)
        {
            m_objCameraLocl.bSaveImgae = true;
            m_objCameraLocl.OneShot();
            return;
            SaveFileDialog saveFrom = new SaveFileDialog();
            saveFrom.Filter = "BMP(*.bmp)|*.bmp|PNG(*.png)|*.png|JPEG(*.jpeg)|*.jpeg|所有文件(*.*)|*.*";

            if (saveFrom.ShowDialog() == DialogResult.OK)
            {              
                m_objCameraLocl.bSaveImgae = true;
                m_objCameraLocl.OneShot();

                ////get cameral 

                //foreach (CCamerInfo cmInfo in m_listCCamerInfo)
                //{
                //    if (cmInfo.m_objCameraInfo[CameraInfoKey.FriendlyName] == rootNodeSel.Text)
                //    {
                //        cmInfo.m_objImageShowFrom.PbxShowImage.Image.Save(saveFrom.FileName);
                //    }
                //}
            }
        }

        private void btn_PointStart_Click(object sender, EventArgs e)
        {
            string strLog;
            double curXpos = 0;
            double curYpos = 0;
            m_Line = new Line();

            m_objFixture8338.GetPostionAbs(ref curXpos, ref curYpos);

            if (MessageBox.Show("确认设置为起点?", "设置", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            m_Line.start.X =(int) curXpos;
            m_Line.start.Y = (int)curYpos;
            //ListViewItem lstItem = new ListViewItem();
            //lstItem.Text = this.lstBox_Lines.Columns.Count.ToString();
            //lstItem.SubItems.Add(curXpos.ToString() + "," + curYpos.ToString());
            //lstBox_Lines.Items.Add(lstItem);

            //btn_PointStart.BackColor = Color.Green;//(233, 240, 249)
            btn_PointStart.Enabled = false;

            strLog = string.Format("设置直线起点: X={0}  Y={1}", m_Line.start.X, m_Line.start.Y);
            ShowLog(strLog);
        }

        private void btn_PointEnd_Click(object sender, EventArgs e)
        {
            string strLog;
            double curXpos = 0;
            double curYpos = 0;

            m_objFixture8338.GetPostionAbs(ref curXpos, ref curYpos);

            if (MessageBox.Show("确认设置为起点?", "设置", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            m_Line.end.X = (int)curXpos;
            m_Line.end.Y = (int)curYpos;

            btn_PointEnd.Enabled = false;

            strLog = string.Format("设置直线终点: X={0}  Y={1}", m_Line.end.X, m_Line.end.Y);
            ShowLog(strLog);
        }

        private void btn_SaveLine_Click(object sender, EventArgs e)
        {

            if(m_Line.start.X == -1 || m_Line.start.Y == -1 || m_Line.end.X == -1 || m_Line.end.Y == -1)
            {
                MessageBox.Show("请添加起点和终点");
                return;
            }

            if (MessageBox.Show("确认保存直线?", "设置", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            ListViewItem lstItem = new ListViewItem();
            lstItem.Text = (lstLines.Count +1).ToString();
            lstItem.SubItems.Add(m_Line.start.X.ToString("f2") + " , " + m_Line.start.Y.ToString("f2"));
            lstItem.SubItems.Add(m_Line.end.X.ToString("f2") + " , " + m_Line.end.Y.ToString("f2"));
            lstBox_Lines.Items.Add(lstItem);

            lstLines.Add(m_Line);

            m_Line.start.X = -1;
            m_Line.start.Y = -1;
            m_Line.end.X = -1;
            m_Line.end.Y = -1;

            btn_PointStart.Enabled = true;
            btn_PointEnd.Enabled = true;

            ShowLog("直线保存成功:");

        }

        private void btn_RunLins_Click(object sender, EventArgs e)
        {
            List<LineData> data = new List<LineData>();
            data.Clear();
            foreach (Line line in lstLines)
            {

                //m_objFixture8338.MovePT_Line(line.start.X, line.start.Y);
                //m_objFixture8338.MovePT_Line(line.end.X, line.end.Y);

                m_objFixture8338.GetLineValues(line.start.X, line.start.Y, line.end.X, line.end.Y, ref data);

            }

            FileStream fs = new FileStream("line_data.txt", FileMode.Create);
            StreamWriter sr = new StreamWriter(fs);

            foreach(LineData ld in data)
            {
                string str = string.Format("{0} {1} {2}", ld.X* 452/50000 - 536.072, ld.Y*1202/ 300000 -185.91, ld.Z);

                sr.WriteLine(str);
            }
            sr.Close();
            fs.Close();
        }

        private void ShowLog(string log)
        {
            string strDateTime = DateTime.Now.ToString();
            string strLog;
            strLog = strDateTime + ": " + log;
            if (LstBox_Log.InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                LstBox_Log.BeginInvoke(new DeShowLog(ShowLog),new object[] { strLog});
                return;
            }

            LstBox_Log.Items.Add(strLog);

        }

        private void radMenuItem_Cameral_Click(object sender, EventArgs e)
        {

        }

        private void btn_Scan3D_Click(object sender, EventArgs e)
        {
            m_objFixture8338.SickLaserPowerOnOff(false);
            System.Threading.Thread.Sleep(200);
            m_objFixture8338.Scan();
            m_objFixture8338.SickLaserPowerOnOff(true);
        }
    }
}
