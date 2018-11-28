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
            
            CCamerInfo objCCamerInfo = new CCamerInfo();
            CCamerInfo objCCamerInfo2 = new CCamerInfo();
            m_listCCamerInfo.Add(objCCamerInfo);
            m_listCCamerInfo.Add(objCCamerInfo2);


            frmShowImage objImageShowFrom = new frmShowImage();
            m_listCCamerInfo[0].m_objImageShowFrom = objImageShowFrom;

            System.Windows.Forms.PictureBox pbx = objImageShowFrom.PbxShowImage;
            CCBasler objBasler = new CCBasler(ref pbx);
            m_listCCamerInfo[0].m_objBasler = objBasler;


            frmShowImage objImageShowFrom2 = new frmShowImage();
            m_listCCamerInfo[1].m_objImageShowFrom = objImageShowFrom2;

            System.Windows.Forms.PictureBox pbx2 = objImageShowFrom2.PbxShowImage;
            CCBasler objBasler2 = new CCBasler(ref pbx2);
            m_listCCamerInfo[1].m_objBasler = objBasler2;


            //__SelectDeviceAndShow(0);

            //__SelectDeviceAndShow(1);

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


        private void UpdateDeviceTree()
        {
            //RadTreeNode root = this.radTreeView_Devices.Nodes.Add("Cameral Device");
            //root.Nodes.Add("Microsoft Research News and Highlights", 1);

            //root.Nodes.Add("Joel on Software", 1);
            //root.Nodes.Add("Miguel de Icaza", 1);
            //root.Nodes.Add("channel 9", 1);

            //root = this.radTreeView_Devices.Nodes.Add("News (1)");
            //root.Nodes.Add("cnn.com (1)", 1);
            //root.Nodes.Add("msnbc.com", 1);
            //root.Nodes.Add("reuters.com", 1);
            //root.Nodes.Add("bbc.co.uk", 1);

            //root = this.radTreeView_Devices.Nodes.Add("Personal (19)");
            //root.Nodes.Add("sports (2)", 1);
            //RadTreeNode folder = root.Nodes.Add("fun (17)");
            //folder.Nodes.Add("Lolcats (2)", 1);
            //folder.Nodes.Add("FFFOUND (15)", 1);

            //this.radTreeView_Devices.Nodes.Add("Telerik blogs", 1);
            //this.radTreeView_Devices.Nodes.Add("Techcrunch", 1);
            //this.radTreeView_Devices.Nodes.Add("Engadget", 1);

            try
            {
                // Ask the camera finder for a list of camera devices.
                //List<ICameraInfo> allCameras = CameraFinder.Enumerate();
                m_listallCameras = CCBasler.GetDeviceList();

                //ListView.ListViewItemCollection items = deviceListView.Items;

                // Loop over all cameras found.
                foreach (ICameraInfo cameraInfo in m_listallCameras)
                {
                    // Loop over all cameras in the list of cameras.
                    bool newitem = true;
                    //foreach (ListViewItem item in items)
                    //{
                    //    ICameraInfo tag = item.Tag as ICameraInfo;

                    //    // Is the camera found already in the list of cameras?
                    //    if (tag[CameraInfoKey.FullName] == cameraInfo[CameraInfoKey.FullName])
                    //    {
                    //        tag = cameraInfo;
                    //        newitem = false;
                    //        break;
                    //    }
                    //}

                    ICameraInfo tag = cameraInfo;

                    // Is the camera found already in the list of cameras?
                    //if (tag[CameraInfoKey.FullName] == cameraInfo[CameraInfoKey.FullName])
                    //{
                    //    tag = cameraInfo;
                    //    newitem = false;
                    //    break;
                    //}

                    // If the camera is not in the list, add it to the list.
                    //if (newitem)
                    {
                        // Create the item to display.
                        //ListViewItem item = new ListViewItem(cameraInfo[CameraInfoKey.FriendlyName]);
                        //RadTreeNode root = this.radTreeView_Devices.Nodes.Add("Cameral Device");

                        RadTreeNode root = this.radTreeView_Devices.Nodes.Add(cameraInfo[CameraInfoKey.FriendlyName],0);
                        root.Nodes.Add("Video", 1);
                        root.Nodes.Add("Shot", 1);

                        // Create the tool tip text.
                        //string toolTipText = "";
                        //foreach (KeyValuePair<string, string> kvp in cameraInfo)
                        //{
                        //    toolTipText += kvp.Key + ": " + kvp.Value + "\n";
                        //}
                        //item.ToolTipText = toolTipText;

                        // Store the camera info in the displayed item.
                        //item.Tag = cameraInfo;

                        // Attach the device data.
                        //deviceListView.Items.Add(item);
                    }
                }



                //// Remove old camera devices that have been disconnected.
                //foreach (ListViewItem item in items)
                //{
                //    bool exists = false;

                //    // For each camera in the list, check whether it can be found by enumeration.
                //    foreach (ICameraInfo cameraInfo in allCameras)
                //    {
                //        if (((ICameraInfo)item.Tag)[CameraInfoKey.FullName] == cameraInfo[CameraInfoKey.FullName])
                //        {
                //            exists = true;
                //            break;
                //        }
                //    }
                //    // If the camera has not been found, remove it from the list view.
                //    if (!exists)
                //    {
                //        deviceListView.Items.Remove(item);
                //    }
                //}
            }
            catch (Exception exception)
            {
                ShowException(exception);
            }
        }
        /// <summary>
        /// 根据对应的设备对象绑定显示控件
        /// </summary>
        /// <param name="nCamID">相机ID</param>
        private void __SelectDeviceAndShow(int nCamID)
        {
            switch (nCamID)
            {
                case 0:
                    {
                        m_listCCamerInfo[nCamID].m_objBasler.SetDeviceInfo(m_listallCameras[nCamID]);

                        m_listCCamerInfo[nCamID].m_objImageShowFrom.TopLevel = false;
                        m_listCCamerInfo[nCamID].m_objImageShowFrom.Parent = this.panel_image;
                        m_listCCamerInfo[nCamID].m_objImageShowFrom.StartPosition = FormStartPosition.Manual;
                        m_listCCamerInfo[nCamID].m_objImageShowFrom.Location = new Point(0, 0);
                        m_listCCamerInfo[nCamID].m_objImageShowFrom.Show();

                        m_listCCamerInfo[nCamID].m_objBasler.ConnectToDevice();
                        m_listCCamerInfo[nCamID].m_objBasler.ContinuousShot();
                    }
                    break;
                case 1:
                    {
;
                        m_listCCamerInfo[nCamID].m_objBasler.SetDeviceInfo(m_listallCameras[nCamID]);

                        m_listCCamerInfo[nCamID].m_objImageShowFrom.TopLevel = false;
                        m_listCCamerInfo[nCamID].m_objImageShowFrom.Parent = this.panel_image;
                        m_listCCamerInfo[nCamID].m_objImageShowFrom.StartPosition = FormStartPosition.Manual;
                        m_listCCamerInfo[nCamID].m_objImageShowFrom.Location = new Point(400, 0);
                        m_listCCamerInfo[nCamID].m_objImageShowFrom.Show();

                        m_listCCamerInfo[nCamID].m_objBasler.ConnectToDevice();
                        m_listCCamerInfo[nCamID].m_objBasler.ContinuousShot();
                    }
                    break;
                case 2:
                    {
                        m_listCCamerInfo[nCamID].m_objImageShowFrom.MdiParent = this;
                        m_listCCamerInfo[nCamID].m_objImageShowFrom.StartPosition = FormStartPosition.Manual;
                        m_listCCamerInfo[nCamID].m_objImageShowFrom.Location = new Point(0, 330);
                        m_listCCamerInfo[nCamID].m_objImageShowFrom.Show();
                    }
                    break;
                case 3:
                    {
                        m_listCCamerInfo[nCamID].m_objImageShowFrom.MdiParent = this;
                        m_listCCamerInfo[nCamID].m_objImageShowFrom.StartPosition = FormStartPosition.Manual;
                        m_listCCamerInfo[nCamID].m_objImageShowFrom.Location = new Point(390, 330);
                        m_listCCamerInfo[nCamID].m_objImageShowFrom.Show();
                    }
                    break;
                default:
                    break;
            }

        }

        private void ShowException(Exception exception)
        {
            System.Windows.Forms.MessageBox.Show("Exception caught:\n" + exception.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }
    }
}
