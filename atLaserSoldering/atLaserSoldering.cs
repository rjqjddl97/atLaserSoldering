using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using RecipeManager;
using System.IO;
using System.IO.Ports;
using System.Timers;
using System.Threading;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using CustomPages;
using LogLibrary;
using AiCControlLibrary;
using ArioModbusLibrary;
using LaserSoldering;


namespace atLaserSoldering
{
    public partial class atLaserSoldering : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private const int MAX_LOG_QUEUE_COUNT = 10;

        WorkParams _workParams = new WorkParams();
        SystemParams _systemParams = new SystemParams();

        public AiCControlLibrary.SerialCommunication.Control.CommunicationManager _mMotionControlCommManager = null;
        public ArioModbusLibrary.SerialCommunication.Control.CommunicationManager _mRemoteIOCommManager = null;
        public LaserSodering _mLaserSoldering = null;
        //DBControl _JobWorkDbCtrl = new DBControl();
        public ADMSEquipmentInfo _admsEquipment = new ADMSEquipmentInfo();
        public ADMSProductInfo _admsProduct = new ADMSProductInfo();
        ManualResetEvent _waitHandle = new ManualResetEvent(false);
        private LoginForm _mLogin = new LoginForm();
        private Log mLog = new Log();
        private List<LogData> mLogList = new List<LogData>();
        public atLaserSoldering()
        {
            InitializeComponent();
        }

        private void barButtonItemSystemEditor_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                SystemEditor editor = new SystemEditor(_systemParams._SystemLanguageKoreaUse);
                editor._log.WriteLogViewer += LogUpdated;
                editor.ShowDialog();
                string strTemp = string.Format(@"{0}\{1}", SystemDirectoryParams.SystemFolderPath, SystemDirectoryParams.SystemFileName);

                if (File.Exists(strTemp))
                {
                    // System 파일 열기
                    RecipeFileIO.ReadSystemFile(_systemParams, strTemp);
                    mLog.WriteLog(LogLevel.Info, LogClass.atPhoto.ToString(), string.Format("시스템 파일 읽기 성공:{0}", strTemp));
                }
                else
                {
                    // Default File 생성
                    mLog.WriteLog(LogLevel.Fatal, LogClass.atPhoto.ToString(), string.Format("시스템 파라미터를 읽을 수 없습니다.{0}", strTemp));
                    mLog.WriteLog(LogLevel.Fatal, LogClass.atPhoto.ToString(), string.Format("메뉴-시스템 편집기를 이용하여, 시스템 파일을 생성하십시오."));
                }
                _systemParams._motionParams.SetParameterInitial();
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atPhoto.ToString(), "시스템 파라미터를 편집하지 못햇습니다.");
            }
        }
        private void InitializeFileSystem()
        {
            try
            {
                if (string.IsNullOrEmpty(global::atLaserSoldering.Properties.Settings.Default.strRootFolderPath)
                    || string.IsNullOrWhiteSpace(global::atLaserSoldering.Properties.Settings.Default.strRootFolderPath)
                    || !Directory.Exists(global::atLaserSoldering.Properties.Settings.Default.strRootFolderPath))
                {
                    string strRootFolder = string.Empty;
                    string strTempFolder = string.Empty;

                    strRootFolder = string.Format(@"{0}\Autonics\atLaserSoldering", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                    global::atLaserSoldering.Properties.Settings.Default.strRootFolderPath = strRootFolder;
                    SystemDirectoryParams.RootFolderPath = strRootFolder;

                    strTempFolder = string.Format(@"{0}\System", strRootFolder);
                    global::atLaserSoldering.Properties.Settings.Default.strSystemFolderPath = strTempFolder;
                    SystemDirectoryParams.SystemFolderPath = strTempFolder;

                    strTempFolder = string.Format(@"{0}\Recipe", strRootFolder);
                    global::atLaserSoldering.Properties.Settings.Default.strRecipeFolderPath = strTempFolder;
                    SystemDirectoryParams.RecipeFolderPath = strTempFolder;

                    strTempFolder = string.Format(@"{0}\Log", strRootFolder);
                    global::atLaserSoldering.Properties.Settings.Default.strLogFolderPath = strTempFolder;
                    SystemDirectoryParams.LogFolderPath = strTempFolder;

                    strTempFolder = string.Format(@"{0}\Result", strRootFolder);
                    global::atLaserSoldering.Properties.Settings.Default.strResultFolderPath = strTempFolder;
                    SystemDirectoryParams.ResultFolderPath = strTempFolder;

                    strTempFolder = string.Format(@"{0}\Image", strRootFolder);
                    global::atLaserSoldering.Properties.Settings.Default.strImageFolderPath = strTempFolder;
                    SystemDirectoryParams.ImageFolderPath = strTempFolder;

                    global::atLaserSoldering.Properties.Settings.Default.Save();
                }
                else
                {
                    SystemDirectoryParams.RootFolderPath = global::atLaserSoldering.Properties.Settings.Default.strRootFolderPath;
                    SystemDirectoryParams.SystemFolderPath = global::atLaserSoldering.Properties.Settings.Default.strSystemFolderPath;
                    SystemDirectoryParams.RecipeFolderPath = global::atLaserSoldering.Properties.Settings.Default.strRecipeFolderPath;
                    SystemDirectoryParams.LogFolderPath = global::atLaserSoldering.Properties.Settings.Default.strLogFolderPath;
                    SystemDirectoryParams.ResultFolderPath = global::atLaserSoldering.Properties.Settings.Default.strResultFolderPath;
                    SystemDirectoryParams.ImageFolderPath = global::atLaserSoldering.Properties.Settings.Default.strImageFolderPath;
                }

                SystemDirectoryParams.CreateSystemDirectory();
                SystemDirectoryParams.WriteFileSystem();

                // 변경된 경로로 로그 파일을 저장
                mLog.SetLogPath(SystemDirectoryParams.LogFolderPath);
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atPhoto.ToString(), "시스템 파일 초기화를 하지 못햇습니다.");
            }
        }
        private void LogUpdated(object obj, LogEventArgs e)
        {
            try
            {
                if (gridControlLog.InvokeRequired)
                {
                    BeginInvoke(new Action<object, LogEventArgs>(LogUpdated), obj, e);
                    return;
                }

                mLogList.Add(e.Data);

                if (mLogList.Count > MAX_LOG_QUEUE_COUNT)
                    mLogList.RemoveAt(0);

                gridControlLogView.RefreshData();
                gridControlLogView.MoveLast();
            }
            catch
            {
                ;
            }
        }

        private void barButtonItemSystemFolderPathSetting_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                SystemDirectorySetting system = new SystemDirectorySetting();

                if (system.ShowDialog(this) == DialogResult.OK)
                {
                    if (MessageBox.Show("시스템 폴더 경로를 변경하시겠습니까?", "시스템 폴더 경로 변경", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        // 변경된 경로로 로그파일 저장
                        //_log.SetLogPath(system.LogFolderPath);

                        SystemDirectoryParams.RootFolderPath = system.RootFolderPath;
                        SystemDirectoryParams.LogFolderPath = system.LogFolderPath;
                        SystemDirectoryParams.RecipeFolderPath = system.RecipeFolderPath;
                        SystemDirectoryParams.ResultFolderPath = system.ResultFolderPath;
                        SystemDirectoryParams.SystemFolderPath = system.SystemFolderPath;
                        SystemDirectoryParams.ImageFolderPath = system.ImageFolderPath;

                        global::atLaserSoldering.Properties.Settings.Default.strRootFolderPath = SystemDirectoryParams.RootFolderPath;
                        global::atLaserSoldering.Properties.Settings.Default.strLogFolderPath = SystemDirectoryParams.LogFolderPath;
                        global::atLaserSoldering.Properties.Settings.Default.strRecipeFolderPath = SystemDirectoryParams.RecipeFolderPath;
                        global::atLaserSoldering.Properties.Settings.Default.strResultFolderPath = SystemDirectoryParams.ResultFolderPath;
                        global::atLaserSoldering.Properties.Settings.Default.strSystemFolderPath = SystemDirectoryParams.SystemFolderPath;
                        global::atLaserSoldering.Properties.Settings.Default.strImageFolderPath = SystemDirectoryParams.ImageFolderPath;
                        global::atLaserSoldering.Properties.Settings.Default.Save();

                        SystemDirectoryParams.CreateSystemDirectory();
                    }
                }
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atPhoto.ToString(), "시스템 폴더 경로를 설정하지 못햇습니다.");
            }
        }

        private void atLaserSoldering_Load(object sender, EventArgs e)
        {
            try
            {
                mLog.WriteLogViewer += LogUpdated;
                motionControl.LogWriteEvent += Motion_StringToLogWriteEvent;
                remoteIOControl.LogWriteEvent += RemoteIO_StringToLogWriteEvent;
                gridControlLog.DataSource = mLogList;
                InitializeFileSystem();
                string strTemp = string.Format(@"{0}\{1}", SystemDirectoryParams.SystemFolderPath, SystemDirectoryParams.SystemFileName);

                if (File.Exists(strTemp))
                {
                    // System 파일 열기
                    RecipeFileIO.ReadSystemFile(_systemParams, strTemp);
                    _systemParams._motionParams.SetParameterInitial();
                    mLog.WriteLog(LogLevel.Info, LogClass.atPhoto.ToString(), string.Format("시스템 파일 읽기 성공:{0}", strTemp));
                }
                else
                {
                    // Default File 생성
                    mLog.WriteLog(LogLevel.Fatal, LogClass.atPhoto.ToString(), string.Format("시스템 파라미터를 읽을 수 없습니다.{0}", strTemp));
                    mLog.WriteLog(LogLevel.Fatal, LogClass.atPhoto.ToString(), string.Format("메뉴-시스템 편집기를 이용하여, 시스템 파일을 생성하십시오."));
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void Motion_StringToLogWriteEvent(string strLog)
        {
            try
            {
                mLog.WriteLog(LogLevel.Info, LogClass.MotionControl.ToString(), strLog);
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atPhoto.ToString(), "모션 로그 이벤트에 오류가 있습니다.");
            }
        }
        private void RemoteIO_StringToLogWriteEvent(string strLog)
        {
            try
            {
                mLog.WriteLog(LogLevel.Info, LogClass.RemoteIO.ToString(), strLog);
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atPhoto.ToString(), "RemoteIO 로그 이벤트에 오류가 있습니다.");
            }
        }
    }
}