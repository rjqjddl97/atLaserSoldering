using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Basler.Pylon;
//using LogLibrary;

namespace Basler
{
    
    public class BaslerCamera
    {
        public delegate void EventCameraConnectionLost(object sender, EventArgs e);
        public event EventCameraConnectionLost OnCameraConnectionLost;

        public delegate void EventCameraConnectionOpen(object sender, EventArgs e);
        public event EventCameraConnectionOpen OnCameraConnectionOpen;

        public delegate void EventCameraClose(object sender, EventArgs e);
        public event EventCameraClose OnCameraClose;

        public delegate void EventCameraImageGrab(object sender, EventArgs e);
        public event EventCameraImageGrab OnCameraImageGrab;

        public delegate void EventCameraImageGrabStart(object sender, EventArgs e);
        public event EventCameraImageGrab OnCameraImageGrabStart;

        public delegate void EventCameraImageGrabEnd(object sender, EventArgs e);
        public event EventCameraImageGrab OnCameraImageGrabEnd;

        //public Log _log = new Log();

        List<ICameraInfo> _listCameraInfo = new List<ICameraInfo>();

        Camera _camera = null;
        public PixelDataConverter _converter = new PixelDataConverter();
        Stopwatch _stopWatch = new Stopwatch();

        ManualResetEvent _waitHandle = new ManualResetEvent(false);

        int _errorCode = -1;

        bool _isGrabbed = false;

        public bool IsGrabbed
        {
            get { return _isGrabbed; }
            set { _isGrabbed = value; }
        }

        public bool IsAllocated
        {
            get
            {
                return (_camera == null) ? false : true;
            }
        }

        public string Name
        {
            get { return _camera.CameraInfo[CameraInfoKey.FriendlyName]; }
        }

        public CameraParameters ExposureTime
        {
            get
            {
                CameraParameters cameraParam = new CameraParameters();

                if (_camera.IsOpen)
                {
                    if (_camera.Parameters.Contains(PLCamera.ExposureTimeAbs))
                    {
                        if (_camera.Parameters[PLCamera.ExposureTimeAbs].IsReadable)
                        {
                            cameraParam.Value = _camera.Parameters[PLCamera.ExposureTimeAbs].GetValue();
                            cameraParam.MaxValue = _camera.Parameters[PLCamera.ExposureTimeAbs].GetMaximum();
                            cameraParam.MinValue = _camera.Parameters[PLCamera.ExposureTimeAbs].GetMinimum();
                        }
                    }
                    else
                    {
                        if (_camera.Parameters[PLCamera.ExposureTime].IsReadable)
                        {
                            cameraParam.Value = _camera.Parameters[PLCamera.ExposureTime].GetValue();
                            cameraParam.MaxValue = _camera.Parameters[PLCamera.ExposureTime].GetMaximum();
                            cameraParam.MinValue = _camera.Parameters[PLCamera.ExposureTime].GetMinimum();
                        }
                    }
                }

                return cameraParam;
            }
            set
            {
                if (_camera.IsOpen)
                    if(_camera.Parameters.Contains(PLCamera.ExposureTimeAbs))
                        if(_camera.Parameters[PLCamera.ExposureTimeAbs].IsWritable)
                            _camera.Parameters[PLCamera.ExposureTimeAbs].SetValue(value.Value);
                    else
                        if (_camera.Parameters[PLCamera.ExposureTime].IsWritable)
                            _camera.Parameters[PLCamera.ExposureTime].SetValue(value.Value);
            }
        }

        public CameraParameters Gain
        {
            get
            {
                CameraParameters cameraParam = new CameraParameters();

                if (_camera.IsOpen)
                {
                    if(_camera.Parameters.Contains(PLCamera.GainSelector))
                    {
                        if(_camera.Parameters[PLCamera.GainSelector].IsWritable)
                        {
                            _camera.Parameters[PLCamera.GainSelector].SetValue("All");
                        }
                    }

                    if (_camera.Parameters.Contains(PLCamera.GainAbs))
                    {
                        if (_camera.Parameters[PLCamera.GainAbs].IsReadable)
                        {
                            cameraParam.Value = _camera.Parameters[PLCamera.GainAbs].GetValue();
                            cameraParam.MaxValue = _camera.Parameters[PLCamera.GainAbs].GetMaximum();
                            cameraParam.MinValue = _camera.Parameters[PLCamera.GainAbs].GetMinimum();
                        }
                    }
                    else if (_camera.Parameters.Contains(PLCamera.GainRaw))
                    {
                        if (_camera.Parameters[PLCamera.GainRaw].IsReadable)
                        {
                            cameraParam.Value = _camera.Parameters[PLCamera.GainRaw].GetValue();
                            cameraParam.MaxValue = _camera.Parameters[PLCamera.GainRaw].GetMaximum();
                            cameraParam.MinValue = _camera.Parameters[PLCamera.GainRaw].GetMinimum();
                        }
                    }
                    else
                    {
                        if (_camera.Parameters[PLCamera.Gain].IsReadable)
                        {
                            cameraParam.Value = _camera.Parameters[PLCamera.Gain].GetValue();
                            cameraParam.MaxValue = _camera.Parameters[PLCamera.Gain].GetMaximum();
                            cameraParam.MinValue = _camera.Parameters[PLCamera.Gain].GetMinimum();
                        }
                    }
                }

                return cameraParam;
            }
            set
            {
                if (_camera.IsOpen)
                    if (_camera.Parameters.Contains(PLCamera.GainAbs))
                        if (_camera.Parameters[PLCamera.GainAbs].IsWritable)
                            _camera.Parameters[PLCamera.GainAbs].SetValue(value.Value);
                    else if(_camera.Parameters.Contains(PLCamera.GainRaw))
                        if (_camera.Parameters[PLCamera.GainRaw].IsWritable)
                            _camera.Parameters[PLCamera.GainRaw].SetValue((long)value.Value);
                    else
                        if (_camera.Parameters[PLCamera.Gain].IsWritable)
                            _camera.Parameters[PLCamera.Gain].SetValue((long)value.Value);
            }
        }

        public CameraParameters Width
        {
            get
            {
                CameraParameters cameraParam = new CameraParameters();

                if (_camera.IsOpen)
                {
                    if (_camera.Parameters[PLCamera.Width].IsReadable)
                    {
                        cameraParam.Value = _camera.Parameters[PLCamera.Width].GetValue();
                        cameraParam.MaxValue = _camera.Parameters[PLCamera.Width].GetMaximum();
                        cameraParam.MinValue = _camera.Parameters[PLCamera.Width].GetMinimum();
                    }
                }

                return cameraParam;
            }
            set
            {
                if(_camera.Parameters[PLCamera.Width].IsWritable)
                {
                    _camera.Parameters[PLCamera.Width].SetValue((long)value.Value);
                }
            }
        }

        public CameraParameters Height
        {
            get
            {
                CameraParameters cameraParam = new CameraParameters();

                if (_camera.IsOpen)
                {
                    if (_camera.Parameters[PLCamera.Height].IsReadable)
                    {
                        cameraParam.Value = _camera.Parameters[PLCamera.Height].GetValue();
                        cameraParam.MaxValue = _camera.Parameters[PLCamera.Height].GetMaximum();
                        cameraParam.MinValue = _camera.Parameters[PLCamera.Height].GetMinimum();
                    }
                }

                return cameraParam;
            }
            set
            {
                if (_camera.Parameters[PLCamera.Height].IsWritable)
                {
                    _camera.Parameters[PLCamera.Height].SetValue((long)value.Value);
                }
            }
        }

        public CameraParameters FrameRate
        {
            get
            {
                CameraParameters cameraParam = new CameraParameters();

                if (_camera.IsOpen)
                {
                    if (_camera.Parameters.Contains(PLCamera.AcquisitionFrameRateAbs))
                    {
                        cameraParam.Value = _camera.Parameters[PLCamera.AcquisitionFrameRateAbs].GetValue();
                        cameraParam.MaxValue = _camera.Parameters[PLCamera.AcquisitionFrameRateAbs].GetMaximum();
                        cameraParam.MinValue = _camera.Parameters[PLCamera.AcquisitionFrameRateAbs].GetMinimum();
                    }
                }

                return cameraParam;
            }
            set
            {
                if (_camera.Parameters[PLCamera.AcquisitionFrameRateEnable].IsWritable)
                {
                    _camera.Parameters[PLCamera.AcquisitionFrameRateEnable].TrySetValue(true);

                    if (_camera.Parameters[PLCamera.AcquisitionFrameRateAbs].IsWritable)
                    {
                        _camera.Parameters[PLCamera.AcquisitionFrameRateAbs].SetValue((float)value.Value);
                    }
                }
            }
        }

        
        public string AqusitionMode
        {
            get
            {
                if (_camera.IsOpen)
                    if (_camera.Parameters[PLCamera.AcquisitionMode].IsReadable)
                        return _camera.Parameters[PLCamera.AcquisitionMode].GetValue();

                return _errorCode.ToString();
            }
        }

        public bool IsOpen
        {
            get
            {
                return _camera.IsOpen;
            }
        }
        
        
        public bool Open(string strFriendlyName)
        {
            try
            {
                if (_camera != null)
                    DestroyCamera();

                for(int i = 0; i < _listCameraInfo.Count; ++i)
                {
                    if(strFriendlyName.Equals(_listCameraInfo[i][CameraInfoKey.FriendlyName]))
                    {
                        _camera = new Camera(_listCameraInfo[i]);

                        DeviceAccessibilityInfo eAccess = CameraFinder.GetDeviceAccessibilityInfo(_listCameraInfo[i]);

                        if (eAccess != DeviceAccessibilityInfo.Ok)
                        {
                            
                            _camera = new Camera(_listCameraInfo[i]);
                        }

                        _camera.CameraOpened += Configuration.AcquireContinuous;

                        _camera.ConnectionLost += OnConnectionLost;
                        _camera.CameraOpened += OnCameraOpened;
                        _camera.CameraClosed += OnCameraClosed;
                        _camera.StreamGrabber.GrabStarted += OnGrabStarted;
                        _camera.StreamGrabber.ImageGrabbed += OnImageGrabbed;
                        _camera.StreamGrabber.GrabStopped += OnGrabStopped;

                        _camera.Open();

                        return true;
                    }
                }

                return false;
            }
            catch(Exception ex)
            {
                for (int i = 0; i < _listCameraInfo.Count; ++i)
                {
                    if (strFriendlyName.Equals(_listCameraInfo[i][CameraInfoKey.FriendlyName]))
                    {
                       DeviceAccessibilityInfo eAccess = CameraFinder.GetDeviceAccessibilityInfo(_listCameraInfo[i]);
                    }
                }

                _camera.Dispose();
                _camera = null;

                //_log.WriteLog(LogLevel.Error, LogClass.Camera.ToString(), ex.Message);
                //_log.WriteLog(LogLevel.Error, LogClass.Camera.ToString(), ex.StackTrace);

                return false;
            }
            
        }

        private void OnConnectionLost(Object sender, EventArgs e)
        {
            // Close the camera object.
            Close();

            // Because one device is gone, the list needs to be updated.
            if (OnCameraConnectionLost != null)
                OnCameraConnectionLost(sender, e);
        }


        // Occurs when the connection to a camera device is opened.
        private void OnCameraOpened(Object sender, EventArgs e)
        {
            if (OnCameraConnectionOpen != null)
                OnCameraConnectionOpen(sender, e);
        }


        // Occurs when the connection to a camera device is closed.
        private void OnCameraClosed(Object sender, EventArgs e)
        {
            if (OnCameraClose != null)
                OnCameraClose(sender, e);
        }


        // Occurs when a camera starts grabbing.
        private void OnGrabStarted(Object sender, EventArgs e)
        {
            _isGrabbed = false;

            if (OnCameraImageGrabStart != null)
                OnCameraImageGrabStart(sender, e);
        }

        // Occurs when an image has been acquired and is ready to be processed.
        private void OnImageGrabbed(Object sender, ImageGrabbedEventArgs e)
        {
            try
            {
                // Get the grab result.
                IGrabResult grabResult = e.GrabResult;

                // Check if the image can be displayed.
                if (grabResult.IsValid)
                {
                    // Reduce the number of displayed images to a reasonable amount if the camera is acquiring images very fast.
                    if (!_stopWatch.IsRunning || _stopWatch.ElapsedMilliseconds > 33)
                    {
                        _stopWatch.Restart();

                        Bitmap bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format32bppArgb);
                        //Bitmap bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format8bppIndexed);
                        
                        // 팔레트 수정
                        if (bitmap.PixelFormat == PixelFormat.Format8bppIndexed)
                        {
                            ColorPalette colorPalette = bitmap.Palette;
                            for (int i = 0; i < 256; i++)
                            {
                                colorPalette.Entries[i] = Color.FromArgb(i, i, i);
                            }
                            bitmap.Palette = colorPalette;
                        }

                        // Lock the bits of the bitmap.
                        BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

                        // Place the pointer to the buffer of the bitmap.
                        _converter.OutputPixelFormat = PixelType.BGRA8packed;
                        //_converter.OutputPixelFormat = PixelType.Mono8;
                        IntPtr ptrBmp = bmpData.Scan0;
                        _converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult); //Exception handling TODO
                        bitmap.UnlockBits(bmpData);

                        GrabEndParam grabEnd = new GrabEndParam();

                        grabEnd.Image = (Bitmap)bitmap.Clone();
                        grabEnd.WaitHandle = _waitHandle;

                        if (OnCameraImageGrab != null)
                            OnCameraImageGrab(grabEnd, e);

                        _isGrabbed = true;
                    }
                }
            }
            catch (Exception exception)
            {
                //ShowException(exception);
            }
            finally
            {
                // Dispose the grab result if needed for returning it to the grab loop.
                e.DisposeGrabResultIfClone();
                Thread.Sleep(10);
            }
        }

        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }


        // Occurs when a camera has stopped grabbing.
        private void OnGrabStopped(Object sender, GrabStopEventArgs e)
        {
            if (OnCameraImageGrabEnd != null)
                OnCameraImageGrabEnd(sender, e);

            // Reset the stopwatch.
            _stopWatch.Reset();
        }


        public List<string> FindCameras()
        {
            _listCameraInfo.Clear();

            _listCameraInfo = CameraFinder.Enumerate(DeviceType.GigE);
            List<string> strFriendlyName = new List<string>();

            for(int i = 0; i < _listCameraInfo.Count; ++i)
                strFriendlyName.Add(_listCameraInfo[i][CameraInfoKey.FriendlyName]);

            return strFriendlyName;
        }

        public void Close()
        {
            if (IsAllocated)
                DestroyCamera();
        }

        public void OneShot(ManualResetEvent waitHandle)
        {
            try
            {
                _waitHandle = waitHandle;

                // Starts the grabbing of one image.
                _camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.SingleFrame);
                _camera.StreamGrabber.Start(1, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
            }
            catch (Exception exception)
            {
                
            }
        }


        // Starts the continuous grabbing of images and handles exceptions.
        public void ContinuousShot(int frameRate)
        {
            try
            {
                // Start the grabbing of images until grabbing is stopped.
                _camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);
                _camera.StreamGrabber.Start(GrabStrategy.LatestImages, GrabLoop.ProvidedByStreamGrabber);
            }
            catch (Exception exception)
            {
                //ShowException(exception);
            }
        }

        public void Stop()
        {
            // Stop the grabbing.
            try
            {
                _camera.StreamGrabber.Stop();
            }
            catch (Exception exception)
            {
                
            }
        }

        private void DestroyCamera()
        {
            // Destroy the camera object.
            try
            {
                if (_camera != null)
                {
                    _camera.Close();
                    _camera.Dispose();
                    _camera = null;
                }
            }
            catch (Exception exception)
            {

            }
        }
    }
}
