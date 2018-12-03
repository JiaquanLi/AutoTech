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
 

namespace AutoTech
{
    public partial class RadForm1 : Telerik.WinControls.UI.RadForm
    {
        frmAboutBox objfrmAbout;                                              /// <关于对话框>

        List<CCamerInfo> m_listCCamerInfo = new List<CCamerInfo>();           ///相机参数状态列表
        List<ICameraInfo> m_listallCameras = new List<ICameraInfo>();
        private clsFixture m_objFixture = null;
        clsFixture.stComPort m_objComPort;

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

        }

        private void pb_image_MouseMove(object sender, MouseEventArgs e)
        {
            tb_imgX.Text = e.X.ToString();
            tb_imgY.Text = e.Y.ToString();
        }

        private void radMenuItemAbout_Click(object sender, EventArgs e)
        {
            objfrmAbout = new frmAboutBox();
            objfrmAbout.Show();
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

                    break;

                case CCBasler.ConnectionType._StopContinusShot:
                    if (objCCamerInfo.m_Flag == -1)
                    {
                        MessageBox.Show("相机没有连接", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }

                    objCCamerInfo.m_objBasler.Stop();
                    objCCamerInfo.m_bIsSnap = false;
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
                        if(radTreeNode.Name == cameraInfo[CameraInfoKey.FriendlyName])
                        {
                            newitem = false;
                            break;
                        }
                        
                    }

                    if(newitem)
                    {
                        RadTreeNode root = this.radTreeView_Devices.Nodes.Add(cameraInfo[CameraInfoKey.FriendlyName], 0);
                        root.Nodes.Add(CCBasler.ConnectionType._Connect, 1);
                        root.Nodes.Add(CCBasler.ConnectionType._DisConnect, 1);
                        root.Nodes.Add(CCBasler.ConnectionType._OneShot, 1);
                        root.Nodes.Add(CCBasler.ConnectionType._ContinusShot, 1);
                        root.Nodes.Add(CCBasler.ConnectionType._StopContinusShot, 1);
                        
                        CCamerInfo objCCamerInfo = new CCamerInfo();
                        frmShowImage objImageShowFrom = new frmShowImage();
                        objImageShowFrom.Fixture = m_objFixture;
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
        private bool __SelectDeviceAndShow(CCamerInfo cameral, ICameraInfo cameralInfo)
        {

            bool b_sts = false;
            cameral.m_objBasler.SetDeviceInfo(cameralInfo);
            b_sts = cameral.m_objBasler.ConnectToDevice();
            if (b_sts == false) return false;

            cameral.m_objImageShowFrom.TitleText = cameral.m_strDisplayName;
            cameral.m_objImageShowFrom.ImageHeight = cameral.m_objBasler.GetCameralImageHeight();
            cameral.m_objImageShowFrom.ImageWidth = cameral.m_objBasler.GetCameralImageWidth();

            cameral.m_objImageShowFrom.TopLevel = false;
            cameral.m_objImageShowFrom.Parent = this.panel_image;
            cameral.m_objImageShowFrom.StartPosition = FormStartPosition.Manual;
            cameral.m_objImageShowFrom.Location = new Point(0, 0);
            cameral.m_objImageShowFrom.Show();


            return b_sts;

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


            m_objFixture = new clsFixture();
            m_objFixture.ComPort = m_objComPort;
            if (m_objFixture.InitFixture() == false)
            {
                MessageBox.Show("设备初始化失败!!!");
                return;
            }
        }
        private void ShowException(Exception exception)
        {
            System.Windows.Forms.MessageBox.Show("Exception caught:\n" + exception.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        private void RadForm1_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i < m_listCCamerInfo.Count; i++)
            {
                if (m_listCCamerInfo[i].m_Flag != -1)
                {
                    m_listCCamerInfo[i].m_objBasler.DestroyCamera();
                }

            }
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
            fmConfig.Fixture = m_objFixture;
            fmConfig.ShowDialog();
        }
    }
}
