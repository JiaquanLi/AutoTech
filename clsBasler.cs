using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using Basler.Pylon;

namespace AutoTech
{
    public class CCBasler
    {

        private System.Windows.Forms.PictureBox pbx_Image;
        private Camera objCamera = null;
        private ICameraInfo objICameraInfo;


        private PixelDataConverter converter = new PixelDataConverter();
        private Stopwatch stopWatch = new Stopwatch();

        public struct ConnectionType
        {
            public const string _Connect = "Connect";
            public const string _DisConnect = "DisConnect";
            public const string _StopContinusShot = "StopContinusShot";
            public const string _OneShot = "OneShot";
            public const string _ContinusShot = "ContinusShot";
            public const string _Configuration = "Configuration";
        }

        public enum en_ConnectStatue
        {
            _Invalid = -1,
            _Connected,
            _DisConnected,
            _ContinusShot,
            _StopContinusShot,
            _OneShot

        }

        public CCBasler(ref System.Windows.Forms.PictureBox pbxImage)
        {
            this.pbx_Image = pbxImage;
        }

        ~CCBasler()
        {
            DestroyCamera();
        }

        public static List<ICameraInfo> GetDeviceList()
        {
            List<ICameraInfo> lstDevInfo = null;
            try
            {
                // Ask the camera finder for a list of camera devices.
                lstDevInfo = CameraFinder.Enumerate();

            }
            catch (Exception exception)
            {
                //ShowException(exception);
                System.Windows.Forms.MessageBox.Show("Exception caught:\n" + exception.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return lstDevInfo;
        }

        public int GetCameralImageWidth()
        {
            if (objCamera == null) return -1;

            return (int)objCamera.Parameters[PLCamera.Width].GetMaximum();


        }

        public int GetCameralImageHeight()
        {
            if (objCamera == null) return -1;

            return (int)objCamera.Parameters[PLCamera.Height].GetMaximum();


        }

        public void SetDeviceInfo(ICameraInfo devInfo)
        {
            this.objICameraInfo = devInfo;
        }
        // Starts the continuous grabbing of images and handles exceptions.
        public void ContinuousShot()
        {
            try
            {
                // Start the grabbing of images until grabbing is stopped.
                objCamera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);
                objCamera.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
            }
            catch (Exception exception)
            {
                ShowException(exception);
            }
        }

        // Occurs when a new camera has been selected in the list. Destroys the object of the currently opened camera device and
        // creates a new object for the selected camera device. After that, the connection to the selected camera device is opened.
        public bool ConnectToDevice()
        {
            // Destroy the old camera object.
            if (objCamera != null)
            {
                DestroyCamera();
            }

            // Open the connection to the selected camera device.
            //if (deviceListView.SelectedItems.Count > 0)
            {
                // Get the attached device data.
                ICameraInfo selectedCamera = objICameraInfo;//item.Tag as ICameraInfo;
                try
                {
                    // Create a new camera object.
                    objCamera = new Camera(selectedCamera);

                    objCamera.CameraOpened += Configuration.AcquireContinuous;

                    // Register for the events of the image provider needed for proper operation.
                    objCamera.ConnectionLost += OnConnectionLost;
                    objCamera.CameraOpened += OnCameraOpened;
                    objCamera.CameraClosed += OnCameraClosed;
                    objCamera.StreamGrabber.GrabStarted += OnGrabStarted;
                    objCamera.StreamGrabber.ImageGrabbed += OnImageGrabbed;
                    objCamera.StreamGrabber.GrabStopped += OnGrabStopped;

                    // Open the connection to the camera device.
                    objCamera.Open();

                }
                catch (Exception exception)
                {
                    ShowException(exception);
                    return false;
                }
            }

            return true;
        }

        public void DestroyCamera()
        {
            // Disable all parameter controls.
            try
            {
                if (objCamera != null)
                {

                }
            }
            catch (Exception exception)
            {
                ShowException(exception);
            }

            // Destroy the camera object.
            try
            {
                if (objCamera != null)
                {
                    objCamera.Close();
                    objCamera.Dispose();
                    objCamera = null;
                }
            }
            catch (Exception exception)
            {
                ShowException(exception);
            }
        }


        //
        private void OnConnectionLost(Object sender, EventArgs e)
        {
            if (pbx_Image.InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                pbx_Image.BeginInvoke(new EventHandler<EventArgs>(OnConnectionLost), sender, e);
                return;
            }

            // Close the camera object.
            DestroyCamera();
            // Because one device is gone, the list needs to be updated.
            //UpdateDeviceList();
        }


        // Occurs when the connection to a camera device is opened.
        private void OnCameraOpened(Object sender, EventArgs e)
        {
            if (pbx_Image.InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                pbx_Image.BeginInvoke(new EventHandler<EventArgs>(OnCameraOpened), sender, e);
                return;
            }

        }


        // Occurs when the connection to a camera device is closed.
        private void OnCameraClosed(Object sender, EventArgs e)
        {
            if (pbx_Image.InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                pbx_Image.BeginInvoke(new EventHandler<EventArgs>(OnCameraClosed), sender, e);
                return;
            }

        }


        // Occurs when a camera starts grabbing.
        private void OnGrabStarted(Object sender, EventArgs e)
        {
            if (pbx_Image.InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                pbx_Image.BeginInvoke(new EventHandler<EventArgs>(OnGrabStarted), sender, e);
                return;
            }

            // Reset the stopwatch used to reduce the amount of displayed images. The camera may acquire images faster than the images can be displayed.

            stopWatch.Reset();
        }


        // Occurs when an image has been acquired and is ready to be processed.
        private void OnImageGrabbed(Object sender, ImageGrabbedEventArgs e)
        {
            if (pbx_Image.InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper GUI thread.
                // The grab result will be disposed after the event call. Clone the event arguments for marshaling to the GUI thread.
                pbx_Image.BeginInvoke(new EventHandler<ImageGrabbedEventArgs>(OnImageGrabbed), sender, e.Clone());
                return;
            }

            try
            {
                // Acquire the image from the camera. Only show the latest image. The camera may acquire images faster than the images can be displayed.

                // Get the grab result.
                IGrabResult grabResult = e.GrabResult;

                // Check if the image can be displayed.
                if (grabResult.IsValid)
                {
                    // Reduce the number of displayed images to a reasonable amount if the camera is acquiring images very fast.
                    if (!stopWatch.IsRunning || stopWatch.ElapsedMilliseconds > 33)
                    {
                        stopWatch.Restart();

                        Bitmap bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format32bppRgb);
                        // Lock the bits of the bitmap.
                        BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                        // Place the pointer to the buffer of the bitmap.
                        converter.OutputPixelFormat = PixelType.BGRA8packed;
                        IntPtr ptrBmp = bmpData.Scan0;
                        converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult); //Exception handling TODO
                        bitmap.UnlockBits(bmpData);

                        // Assign a temporary variable to dispose the bitmap after assigning the new bitmap to the display control.
                        Bitmap bitmapOld = pbx_Image.Image as Bitmap;
                        // Provide the display control with the new bitmap. This action automatically updates the display.
                        pbx_Image.Image = bitmap;
                        if (bitmapOld != null)
                        {
                            // Dispose the bitmap.
                            bitmapOld.Dispose();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                ShowException(exception);
            }
            finally
            {
                // Dispose the grab result if needed for returning it to the grab loop.
                e.DisposeGrabResultIfClone();
            }
        }


        // Occurs when a camera has stopped grabbing.
        private void OnGrabStopped(Object sender, GrabStopEventArgs e)
        {
            if (pbx_Image.InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                pbx_Image.BeginInvoke(new EventHandler<GrabStopEventArgs>(OnGrabStopped), sender, e);
                return;
            }

            // Reset the stopwatch.
            stopWatch.Reset();

            // If the grabbed stop due to an error, display the error message.
            if (e.Reason != GrabStopReason.UserRequest)
            {
                System.Windows.Forms.MessageBox.Show("A grab error occured:\n" + e.ErrorMessage, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        // Starts the grabbing of a single image and handles exceptions.
        public void OneShot()
        {
            try
            {
                // Starts the grabbing of one image.
                objCamera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.SingleFrame);
                objCamera.StreamGrabber.Start(1, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
            }
            catch (Exception exception)
            {
                ShowException(exception);
            }
        }

        public void Stop()
        {
            // Stop the grabbing.
            try
            {
                objCamera.StreamGrabber.Stop();
            }
            catch (Exception exception)
            {
                ShowException(exception);
            }
        }

        public void SetParameter(ref FloatSliderUserControl expControl, ref FloatSliderUserControl gainControl ,ref IntSliderUserControl widthControl ,ref IntSliderUserControl heighControl )
        {
            // Set the parameter for the controls.
            //testImageControl.Parameter = camera.Parameters[PLCamera.TestImageSelector];
            //pixelFormatControl.Parameter = camera.Parameters[PLCamera.PixelFormat];
            widthControl.Parameter = objCamera.Parameters[PLCamera.Width];
            heighControl.Parameter = objCamera.Parameters[PLCamera.Height];

            gainControl.DefaultName = "Gain";
            expControl.DefaultName = "Exposure Time";
            if (objCamera == null) return;
            if (objCamera.Parameters.Contains(PLCamera.GainAbs))
            {
                gainControl.Parameter = objCamera.Parameters[PLCamera.GainAbs];
            }
            else
            {
                gainControl.Parameter = objCamera.Parameters[PLCamera.Gain];
            }
            if (objCamera.Parameters.Contains(PLCamera.ExposureTimeAbs))
            {
                expControl.Parameter = objCamera.Parameters[PLCamera.ExposureTimeAbs];
            }
            else
            {
                expControl.Parameter = objCamera.Parameters[PLCamera.ExposureTime];
            }

        }

        private void  ShowException(Exception exception)
        {
            System.Windows.Forms.MessageBox.Show("Exception caught:\n" + exception.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }
    }
}
