using System;
using System.Collections;
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
using Basler;
using CustomPages;
using LogLibrary;
using AiCControlLibrary;
using FeederControlLibrary;
using ArioModbusLibrary;
using LaserSoldering;
using atLaserSoldering;

namespace atLaserSoldering
{
    public partial class atLaserSoldering : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private const int MAX_LOG_QUEUE_COUNT = 10;

        WorkParams _workParams = new WorkParams();
        SystemParams _systemParams = new SystemParams();
        BaslerCamera _Camera = new BaslerCamera();

        public AiCControlLibrary.SerialCommunication.Control.CommunicationManager _mMotionControlCommManager = null;
        public ArioModbusLibrary.SerialCommunication.Control.CommunicationManager _mRemoteIOCommManager = null;

        public FeederControlLibrary.SerialCommunication.Control.CommunicationManager _mFeederCommManager = null;
        public CoherentCompactMini.SerialCommunication.Control.CommunicationManager _mLaserCommManager = null;
        public LaserSoderingProcess _mLaserSoldering = null;
        //DBControl _JobWorkDbCtrl = new DBControl();
        public ADMSEquipmentInfo _admsEquipment = new ADMSEquipmentInfo();
        public ADMSProductInfo _admsProduct = new ADMSProductInfo();
        ManualResetEvent _waitHandle = new ManualResetEvent(false);
        public event Action<System.Drawing.Image> UpdateImageEvent;
        BackgroundWorker _backgroundWorkerAutoSoldering = new BackgroundWorker();
        BackgroundWorker _bwMotionHome = new BackgroundWorker();

        private LoginForm _mLogin = new LoginForm();
        private Log mLog = new Log();
        private List<LogData> mLogList = new List<LogData>();
        public RobotInformation mRobotInformation = new RobotInformation();        

        string _strTitle = "레이저 자동 솔더링 시스템";
        string Cameraname = "";

        int _frameCount;
        int _ImageVResolution;
        int _ImageHResolution;

        PointF _fptCropStart = new PointF();
        PointF _fptCropEnd = new PointF();
        PointF _fptMoveStart = new PointF();
        RectangleF _frtCrop = new RectangleF();
        PointF _fptAreaStart = new PointF();
        PointF _fptAreaEnd = new PointF();
        RectangleF _frtArearect = new RectangleF();

        System.Drawing.Image _sourceImage = null;

        bool _InspectionWorking = false;
        bool _HommingProcess = false;
        public event Action<Image> ImageGrabbed;
        bool _isContinuousShot = false;
        bool _isCameraOpen = false;
        bool _isCameraInitialized = false;
        bool _isOpticalMeasurement = false;

        bool _isShowCenterMark = false;
        bool _isGrabbed = false;
        bool _isImageFitSize = false;
        bool _patternMatching = false;
        bool _isAreaMove = false;
        bool _isSetROICheck = false;
        bool _IsLogin = false;
        bool _IsReciepLoad = false;
        bool _IsHommingFinished = false;
        bool _IsDrvErr = false;
        bool _IsHommingCancle = false;
        public atLaserSoldering()
        {
            InitializeComponent();
            _mMotionControlCommManager = new AiCControlLibrary.SerialCommunication.Control.CommunicationManager();
            _mRemoteIOCommManager = new ArioModbusLibrary.SerialCommunication.Control.CommunicationManager();

            _mFeederCommManager = new FeederControlLibrary.SerialCommunication.Control.CommunicationManager();
            _mLaserCommManager = new CoherentCompactMini.SerialCommunication.Control.CommunicationManager();
            _mLaserSoldering = new LaserSoldering.LaserSoderingProcess();
            //_mLaserSoldering.InitialCommunication(_mLaserCommManager, _mFeederCommManager);
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
                    mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("시스템 파일 읽기 성공:{0}", strTemp));
                }
                else
                {
                    // Default File 생성
                    mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), string.Format("시스템 파라미터를 읽을 수 없습니다.{0}", strTemp));
                    mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), string.Format("메뉴-시스템 편집기를 이용하여, 시스템 파일을 생성하십시오."));
                }
                _systemParams._motionParams.SetParameterInitial();
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "시스템 파라미터를 편집하지 못햇습니다.");
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
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "시스템 파일 초기화를 하지 못햇습니다.");
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
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "시스템 폴더 경로를 설정하지 못햇습니다.");
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
                    mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("시스템 파일 읽기 성공:{0}", strTemp));
                }
                else
                {
                    // Default File 생성
                    mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), string.Format("시스템 파라미터를 읽을 수 없습니다.{0}", strTemp));
                    mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), string.Format("메뉴-시스템 편집기를 이용하여, 시스템 파일을 생성하십시오."));
                }
                /*
                if (mLogin.ShowDialog() == DialogResult.OK)
                {
                    _IsLogin = true;
                    //xtraTabControlMainSetting.Enabled = true;

                    //_admsEquipment.WorkerID = logIn.WorkerID;
                    //_admsEquipment.WorkerName = logIn.WorkerName;
                    //_admsEquipment.JobInformation = logIn.JobInformation;

                    //barStaticItemLogIn.Caption = string.Format("사번: {0}, 이름: {1}, 작업지시서: {2}", _admsEquipment.WorkerID, _admsEquipment.WorkerName, _admsEquipment.JobInformation);
                }
                else
                {
                    Application.Exit();
                }
                */

                // Camera 연결
                if (InitializeCamera())
                {
                    _systemParams.InspectionOpticalSpotCenterX = _systemParams._cameraParams.HResolution / 2;
                    _systemParams.InspectionOpticalSpotCenterY = _systemParams._cameraParams.VResolution / 2;
                    mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("카메라 초기화 완료"));
                }
                else
                    mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), string.Format("카메라 초기화 실패"));

                InitailProgramFormLanguage();
                // Motion Control Initial - Communication,
                InitializeMotionDriveModule();
                InitializeArioRemoteIOModule();
                InitializeSolderingModule();

                //InitializeStatistics();
                //InitializeTackTimes();
                //InitializeChartPhotoInspect();

                //// 검사 및 결과 UI의 구분자 추가
                barEditItemAutoSolderingProgress.Links[0].BeginGroup = true;

                mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), "리본 메뉴 구성을 초기화 완료");

                InitializeRecipe();
                mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), "레시피 초기화 완료");

                InitializedBackGroundWorkers();
                mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), "납땜 작업 스레드 등록 완료");

                if (_systemParams.LatestUsedRecipe != string.Empty)
                    RecipeOpen(_systemParams.LatestUsedRecipe);

                UpdateConnectStatusForAll();

                _bwMotionHome.RunWorkerAsync(mRobotInformation);
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
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "모션 로그 이벤트에 오류가 있습니다.");
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
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "RemoteIO 로그 이벤트에 오류가 있습니다.");
            }
        }

        private void barButtonItemRecipeEditorOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (_systemParams != null)
                {
                    RecipeEditor edit = new RecipeEditor(_systemParams._SystemLanguageKoreaUse);
                    edit.SetSystemParam(_systemParams);
                    edit._log.WriteLogViewer += LogUpdated;
                    edit.Show(this);
                }
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "레시피 편집기 실행을 하지 못햇습니다.");
            }
        }
        private void InitailProgramFormLanguage()
        {
            try
            {
                if (!_systemParams._SystemLanguageKoreaUse)
                {
                    ribbonPageEquipementFunctions.Text = "Function";

                    ribbonSystemPage.Text = "Set System";
                    barButtonItemSystemFolderPathSetting.Caption = "Set Path";
                    barButtonItemSystemEditor.Caption = "Set Parameter";
                    barButtonItemWorkInfo.Caption = "Login Information";

                    ribbonPageGroupFile.Text = "Recipe";
                    barButtonItemRecipeOpen.Caption = "Load";
                    barButtonItemRecipeEditorOpen.Caption = "Recipe Editor";
                    //barListItemRecipeOpen.Caption = "Open";

                    ribbonPageGroupConnection.Text = "Communication";
                    barButtonItemConnectAll.Caption = "Connection All";
                    barButtonItemConnectionAiC.Caption = "AiC";
                    barButtonItemConnectionRemeteIO.Caption = "Remote I/O";

                    ribbonPageGroupMotionControl.Text = "Motion Control";
                    barButtonItemHomming.Caption = "Homming";
                    barButtonItemReset.Caption = "Alram Reset";

                    ribbonPageGroupInspection.Text = "Soldering And Result";
                    barCheckItemLaserSolderingStart.Caption = "Start";
                    barStaticItemAutoSolderingStatus.Caption = "Status";
                    barStaticAutoSolderingTime.Caption = "Time:";
                    barEditItemAutoSolderingResult.EditValue = "Ready";
                    //barEditItemTotalInspectionCount.EditValue = "Total Count: 00000";
                    //barEditItemTotalPassCount.EditValue = "Pass Count: 00000";
                    //barEditItemTotalFailCount.EditValue = "Fail Count: 00000";
                    //barButtonItemInitializeStatistics.Caption = "Initial Chart";

                    dockPanelLogView.Text = "Log";
                    gridColumn1.Caption = "Level";
                    gridColumn2.Caption = "Time";
                    gridColumn3.Caption = "Path";
                    gridColumn4.Caption = "Message";
                                        
                    xtraTabPageMotion.Text = "Motion Control";
                    xtraTabPageImageProcess.Text = "Vision Control";
                    xtraTabPageLaserSolder.Text = "Laser Soldering Contrl";
                    xtraTabPageStatics.Text = "Chart";

                    //barStaticItemMotionStatus.Caption = "Motion Status :";
                    //barButtonItemMoveStop.Caption = "Motion Stop";

                    _strTitle = "Laser Auto Soldering System";
                    this.Text = _strTitle;
                }
                else
                {
                    ribbonPageEquipementFunctions.Text = "기능설정";

                    ribbonSystemPage.Text = "시스템 설정";
                    barButtonItemSystemFolderPathSetting.Caption = "경로설정";
                    barButtonItemSystemEditor.Caption = "시스템 설정";
                    barButtonItemWorkInfo.Caption = "로그인 정보";

                    ribbonPageGroupFile.Text = "레시피";
                    barButtonItemRecipeOpen.Caption = "불러오기";
                    barButtonItemRecipeEditorOpen.Caption = "레시피 편집기";
                    //barListItemRecipeOpen.Caption = "레시피 선택";

                    ribbonPageGroupConnection.Text = "통신 연결";
                    barButtonItemConnectAll.Caption = "전체 연결";
                    barButtonItemConnectionAiC.Caption = "AiC";
                    barButtonItemConnectionRemeteIO.Caption = "Remote I/O";

                    ribbonPageGroupMotionControl.Text = "모션 제어";
                    barButtonItemHomming.Caption = "원점 복귀";
                    barButtonItemReset.Caption = "알람 리셋";

                    ribbonPageGroupInspection.Text = "납땜 작업 및 결과";
                    barCheckItemLaserSolderingStart.Caption = "검사 시작";
                    barStaticItemAutoSolderingStatus.Caption = "진행";
                    barStaticAutoSolderingTime.Caption = "검사 시간:";
                    barEditItemAutoSolderingResult.EditValue = "Ready";
                    //barEditItemTotalInspectionCount.EditValue = "총 검사 수: 00000";
                    //barEditItemTotalPassCount.EditValue = "양품 개수: 00000";
                    //barEditItemTotalFailCount.EditValue = "불량 개수: 00000";
                    //barButtonItemInitializeStatistics.Caption = "통계 초기화";

                    dockPanelLogView.Text = "로그";
                    gridColumn1.Caption = "레벨";
                    gridColumn2.Caption = "시간";
                    gridColumn3.Caption = "위치";
                    gridColumn4.Caption = "메세지";

                    
                    xtraTabPageMotion.Text = "모션 제어";
                    xtraTabPageImageProcess.Text = "비젼 제어";
                    xtraTabPageLaserSolder.Text = "레이저납땜 제어";
                    xtraTabPageStatics.Text = "통계";

                    //barStaticItemMotionStatus.Caption = "모션 상태 :";
                    //barButtonItemMoveStop.Caption = "모션 정지";

                    _strTitle = "레이저 자동 솔더링 시스템";
                    this.Text = _strTitle;
                }
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "시스템 언어 설정을 하지 못햇습니다.");
            }
        }
        public bool InitializeMotionDriveModule()
        {
            try
            {
                if (_systemParams != null)
                    motionControl.ChangeSystemLanguage(_systemParams._SystemLanguageKoreaUse);

                motionControl.SetCommunicateManager(ref _mMotionControlCommManager);
                motionControl.SetMotionParam(ref _systemParams._motionParams);               
                
                byte[] _id = new byte[3];
                for (int i = 0; i < 3; i++)
                {
                    if (i == 0)
                    {
                        motionControl._fdefineStepValue[i] = (double)0.1;
                        motionControl._fdefineVelValue[i] = (double)10;
                    }
                    else if (i == 1)
                    {
                        motionControl._fdefineStepValue[i] = (double)1;
                        motionControl._fdefineVelValue[i] = (double)50;
                    }
                    else
                    {
                        motionControl._fdefineStepValue[i] = (double)10;
                        motionControl._fdefineVelValue[i] = (double)100;
                    }
                }
                for (int j = 0; j < _systemParams._AiCParams.IDs.Count; j++)
                    _id[j] = (byte)_systemParams._AiCParams.IDs[j]._idNumber;

                motionControl.SetCommunicationData(_systemParams._AiCParams.IDs.Count, _id);
                DriveModuleConnect(); // connect command
                return true;
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "AiC 모듈 초기화를 하지 못햇습니다.");
                return false;
            }
        }

        public bool InitializeArioRemoteIOModule()
        {
            try
            {
                if (_systemParams != null)
                    remoteIOControl.ChangeSystemLanguage(_systemParams._SystemLanguageKoreaUse);
                remoteIOControl.SetCommunicateManager(ref _mRemoteIOCommManager);
                byte[] _id = new byte[_systemParams._remoteIOParams.ConnectedNumber];

                for (int i = 0; i < _systemParams._remoteIOParams.ConnectedNumber; i++)
                {
                    _id[i] = (byte)_systemParams._remoteIOParams.IDs[i]._idNumber;
                }
                remoteIOControl.SetCommunicationData(_systemParams._remoteIOParams.ConnectedNumber, _id);
                ArioRemoteIOModuleConnect();// connect command
                return true;
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "Ario 모듈 초기화를 하지 못햇습니다.");
                return false;
            }
        }
        private bool DriveModuleConnect(string sComport = null)
        {
            try
            {
                if (!_mMotionControlCommManager.IsOpen() && (_mMotionControlCommManager != null))
                {
                    AiCControlLibrary.SerialCommunication.Control.SerialPortSetData setPort = new AiCControlLibrary.SerialCommunication.Control.SerialPortSetData();
                    setPort.PortName = _systemParams._AiCParams.SerialParameters.PortName;
                    setPort.BaudRate = (int)_systemParams._AiCParams.SerialParameters.BaudRates;
                    setPort.DataBits = (int)_systemParams._AiCParams.SerialParameters.DataBits;
                    setPort.StopBits = System.IO.Ports.StopBits.One; //(StopBits)_systemParams._AiCParams.SerialParameters.StopBits;
                    setPort.Parity = System.IO.Ports.Parity.None;

                    motionControl.ConnectionOpen(setPort);

                    if (_mMotionControlCommManager.IsOpen())
                    {
                        motionControl.RobotInfomationUpdatedEvent += UpdateRobotInfomation;
                        mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("AiC 통신 연결 성공."));
                    }
                    else
                        mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("AiC 통신 연결 실패."));
                }
                else
                {
                    motionControl.ConnectionClosed();
                    mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("AiC 통신 연결 해제 성공."));
                }
                return _mMotionControlCommManager.IsOpen();
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "AiC 통신 연결을 하지 못햇습니다.");
                return false;
            }
        }
        private bool DriveModuleDisConnect()
        {
            try
            {
                if (_mMotionControlCommManager.IsOpen() && (_mMotionControlCommManager != null))
                {
                    motionControl.ConnectionClosed();
                    mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("AiC 통신 연결 해제 성공."));
                }
                return _mMotionControlCommManager.IsOpen();
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "AiC 통신 해제를 하지 못햇습니다.");
                return false;
            }
        }
        private bool ArioRemoteIOModuleConnect(string sComport = null)
        {
            try
            {
                if (!_mRemoteIOCommManager.IsOpen() && (_mRemoteIOCommManager != null))
                {
                    ArioModbusLibrary.SerialCommunication.Control.SerialPortSetData setPort = new ArioModbusLibrary.SerialCommunication.Control.SerialPortSetData();
                    setPort.PortName = _systemParams._remoteIOParams.SerialParameters.PortName;
                    setPort.BaudRate = (int)_systemParams._remoteIOParams.SerialParameters.BaudRates;
                    setPort.DataBits = (int)_systemParams._remoteIOParams.SerialParameters.DataBits;
                    setPort.StopBits = System.IO.Ports.StopBits.One;
                    setPort.Parity = System.IO.Ports.Parity.None;

                    remoteIOControl.ConnectionOpen(setPort);

                    if (_mRemoteIOCommManager.IsOpen())
                    {
                        remoteIOControl.RobotInfomationUpdatedEvent += UpdateRobotIOInfomation;
                        mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("Ario 통신 연결 성공."));
                    }
                    else
                        mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("Ario 통신 연결 실패."));
                }
                else
                {
                    remoteIOControl.ConnectionClosed();
                    mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("Ario 통신 연결 해제 성공."));
                }
                return _mRemoteIOCommManager.IsOpen();
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "Ario 통신 연결을 하지 못햇습니다.");
                return false;
            }
        }
        private bool ArioRemoteIOModuleDisconnet()
        {
            try
            {
                if (_mRemoteIOCommManager.IsOpen() && (_mRemoteIOCommManager != null))
                {
                    _mRemoteIOCommManager.Disconnect();
                }
                return _mRemoteIOCommManager.IsOpen();
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "Ario 통신 해제를 하지 못햇습니다.");
                return false;
            }
        }
        public bool InitializeSolderingModule()
        {
            try
            {
                laserSolderingControl.SetCommunicateManager(_mLaserSoldering,ref _mFeederCommManager,ref _mLaserCommManager);
                if (_systemParams != null)
                {
                    laserSolderingControl.ChangeSystemLanguage(_systemParams._SystemLanguageKoreaUse);
                    laserSolderingControl.SetFeederParam(_systemParams._FeederParams);
                }
                LaserSolderingModuleConnect();// connect command
                return true;
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "Soldering 모듈 초기화를 하지 못햇습니다.");
                return false;
            }
        }
        private bool LaserSolderingModuleConnect(string sComport = null)
        {
            try
            {
                if ((!_mLaserCommManager.IsOpen() && (_mLaserCommManager != null)) && (!_mFeederCommManager.IsOpen() && (_mFeederCommManager != null)))
                {
                    FeederControlLibrary.SerialCommunication.Control.SerialPortSetData feedsetPort = new FeederControlLibrary.SerialCommunication.Control.SerialPortSetData();
                    feedsetPort.PortName = _systemParams._FeederParams.SerialParameters.PortName;
                    feedsetPort.BaudRate = (int)_systemParams._FeederParams.SerialParameters.BaudRates;
                    feedsetPort.DataBits = (int)_systemParams._FeederParams.SerialParameters.DataBits;
                    feedsetPort.StopBits = System.IO.Ports.StopBits.One; //(StopBits)_systemParams._AiCParams.SerialParameters.StopBits;
                    feedsetPort.Parity = System.IO.Ports.Parity.None;

                    CoherentCompactMini.SerialCommunication.Control.SerialPortSetData lasersetPort = new CoherentCompactMini.SerialCommunication.Control.SerialPortSetData();
                    lasersetPort.PortName = _systemParams._LaserParams.SerialParameters.PortName;
                    lasersetPort.BaudRate = (int)_systemParams._LaserParams.SerialParameters.BaudRates;
                    lasersetPort.DataBits = (int)_systemParams._LaserParams.SerialParameters.DataBits;
                    lasersetPort.StopBits = System.IO.Ports.StopBits.One; //(StopBits)_systemParams._AiCParams.SerialParameters.StopBits;
                    lasersetPort.Parity = System.IO.Ports.Parity.None;

                    //laserSolderingControl.SetCommunicationParams(lasersetPort, feedsetPort, (byte)_systemParams._FeederParams.FeederCommunicationID);
                    laserSolderingControl.ConnectionOpen(lasersetPort, feedsetPort, (byte)_systemParams._FeederParams.FeederCommunicationID);

                    if (laserSolderingControl.IsOpenStatus)
                    {
                        //laserSolderingControl.RobotInfomationUpdatedEvent += UpdateRobotInfomation;
                        mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("Soldering 통신 연결 성공."));
                    }
                    else
                        mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("Soldering 통신 연결 실패."));
                }
                else
                {
                    laserSolderingControl.ConnectionClosed();
                    mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("Soldering 통신 연결 해제 성공."));
                }
                return laserSolderingControl.IsOpenStatus;
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "Soldering 통신 연결을 하지 못햇습니다.");
                return false;
            }
        }
        private bool LaserSolderingModuleDisConnect()
        {
            try
            {
                if ((_mLaserCommManager.IsOpen() && (_mLaserCommManager != null)) && (_mFeederCommManager.IsOpen() && (_mFeederCommManager != null)))
                {
                    laserSolderingControl.ConnectionClosed();
                    mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("Soldering 통신 연결 해제 성공."));
                }
                return laserSolderingControl.IsOpenStatus;
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "Soldering 통신 해제를 하지 못햇습니다.");
                return false;
            }
        }
        private void UpdateConnectStatusForAll()
        {
            try
            {
                ///*
                if (!_mMotionControlCommManager.IsOpen())
                    barButtonItemConnectionAiC.ImageOptions.Image = Properties.Resources.disconnect_16x16;
                else
                    barButtonItemConnectionAiC.ImageOptions.Image = Properties.Resources.connect_16x16;

                if (!_mRemoteIOCommManager.IsOpen())
                    barButtonItemConnectionRemeteIO.ImageOptions.Image = Properties.Resources.disconnect_16x16;
                else
                    barButtonItemConnectionRemeteIO.ImageOptions.Image = Properties.Resources.connect_16x16;

                if ((_mMotionControlCommManager.IsOpen()) && (_mRemoteIOCommManager.IsOpen()) && (_isCameraOpen))
                    barButtonItemConnectAll.ImageOptions.LargeImage = Properties.Resources.connectedall_32x32;

                else
                    barButtonItemConnectAll.ImageOptions.LargeImage = Properties.Resources.connectedpart_32x32;
                //*/
            }
            catch (Exception ex)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), string.Format("UpdateConnectionStatusForAll. \n{0}", ex.ToString()));
            }
        }
        private void InitializeRecipe()
        {
            try
            {
                if (Directory.Exists(SystemDirectoryParams.RecipeFolderPath))
                {
                    string[] recipes = null;
                    recipes = Directory.GetDirectories(SystemDirectoryParams.RecipeFolderPath);

                    for (int i = 0; i < recipes.Length; ++i)
                    {
                        string[] strTemp = recipes[i].Split('\\');

                        string strRecipeName = strTemp[strTemp.Length - 1];

                        //if (!_dicTackTimes.ContainsKey(strRecipeName))
                        //{
                        //    _dicTackTimes.Add(strRecipeName, new TackParams(0, 0));
                        //}

                        barListItemRecipeOpen.Strings.Add(strRecipeName);
                    }
                }
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "레시피 설정 초기화를 하지 못햇습니다.");
            }
        }
        private bool InitializeCamera()
        {
            bool IsInitialized = false;
            _isCameraInitialized = IsInitialized;

            _Camera.OnCameraConnectionLost += new BaslerCamera.EventCameraConnectionLost(OnCameraConnectionLost);
            _Camera.OnCameraConnectionOpen += new BaslerCamera.EventCameraConnectionOpen(OnCameraConnectionOpen);
            _Camera.OnCameraImageGrab += new BaslerCamera.EventCameraImageGrab(OnCameraImageGrab);
            _Camera.OnCameraClose += new BaslerCamera.EventCameraClose(OnCameraClose);
            _Camera.OnCameraImageGrabStart += new BaslerCamera.EventCameraImageGrab(OnCameraImageGrabStart);
            _Camera.OnCameraImageGrabEnd += new BaslerCamera.EventCameraImageGrab(OnCameraImageGrabEnd);

            // 카메라 라이브러리 로그 연결
            _Camera._log.WriteLogViewer += new Log.EventWriteLogViewer(LogUpdated);

            List<string> liststrFriendlyNames = _Camera.FindCameras();

            //// System 파일에 카메라가 등록되지 않은 경우
            if (_systemParams._cameraParams.FriendlyName == "None" || string.IsNullOrEmpty(_systemParams._cameraParams.FriendlyName))
            {
                for (int i = 0; i < liststrFriendlyNames.Count; ++i)
                {
                    comboBoxEditCameraName.Properties.Items.Add(liststrFriendlyNames[i]);
                }

                // 카메라가 있는 경우
                try
                {
                    if (liststrFriendlyNames.Count > 0)
                    {
                        //rowCameraFriendlyName.Properties.Value = liststrFriendlyNames[0];

                        Cameraname = liststrFriendlyNames[0];
                        if (_Camera.Open(liststrFriendlyNames[0]))
                        {
                            CameraParameters cameraParam = new CameraParameters();

                            

                            // 노출 시간
                            cameraParam = _Camera.ExposureTime;
                            textEditExposureTime.Text = cameraParam.Value.ToString();
                            //Cameraexposuretime = (int)cameraParam.Value;
                            //_systemParams.CameraParameters.ExposureTime = (int)cameraParam.Value;

                            // 게인
                            cameraParam = _Camera.Gain;
                            textEditGain.Text = cameraParam.Value.ToString();
                            //_systemParams.CameraParameters.Gain = (int)cameraParam.Value;                        

                            // Frame Rate
                            cameraParam = _Camera.FrameRate;
                            textEditFrameRatio.Text = cameraParam.Value.ToString();
                            //_systemParams.CameraParameters.FrameRate = (int)cameraParam.Value;                        

                            cameraParam = _Camera.Width;
                            _ImageHResolution = (int)cameraParam.Value;
                            //_systemParams.CameraParameters.HResolution = (int)cameraParam.Value;                        

                            cameraParam = _Camera.Height;
                            _ImageVResolution = (int)cameraParam.Value;
                            //_systemParams.CameraParameters.VResolution = (int)cameraParam.Value;                        


                            _isCameraInitialized = true;
                            _isCameraOpen = true;
                            // System 파라미터를 Update한다.
                            //RecipeFileIO.WriteSystemFile(_systemParams, string.Format(@"{0}\{1}", SystemDirectoryParams.SystemFolderPath, SystemDirectoryParams.SystemFileName));
                            //UpdateImageEvent += UpdateImageData;
                            timerImageUpdate.Start();
                            mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("카메라 연결 성공:{0}", liststrFriendlyNames[0]));
                            mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(),
                                string.Format("노출 시간:{0}, 게인:{1}, 프레임비:{2}", textEditExposureTime.Text, textEditGain.Text, textEditFrameRatio.Text));

                        }
                        else
                        {
                            mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), string.Format("카메라 연결 실패:{0}", liststrFriendlyNames[0]));
                        }
                    }
                }
                catch (Exception ex)
                {
                    _Camera.Close();
                    _isCameraOpen = false;
                    mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), string.Format("카메라 연결 실패:{0}", liststrFriendlyNames[0]));
                    mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), string.Format("{0}\r\n{1}", ex.Message, ex.StackTrace));
                }
            }
            else
            {
                comboBoxEditCameraName.Properties.Items.Add(_systemParams._cameraParams.FriendlyName);
                //rowCameraName.Properties.Value = repositoryItemComboBoxCameraName.Items[repositoryItemComboBoxCameraName.Items.IndexOf(_systemParams._cameraParams.FriendlyName)].ToString();
                textEditExposureTime.Text = _systemParams._cameraParams.ExposureTime.ToString();
                textEditGain.Text = _systemParams._cameraParams.Gain.ToString();
                textEditFrameRatio.Text = _systemParams._cameraParams.FrameRate.ToString();
                if (_Camera.Open(_systemParams._cameraParams.FriendlyName))
                {
                    IsInitialized = true;
                    _isCameraOpen = true;
                    // System 파라미터를 Update한다.
                    //RecipeFileIO.WriteSystemFile(_systemParams, string.Format(@"{0}\{1}", SystemDirectoryParams.SystemFolderPath, SystemDirectoryParams.SystemFileName));
                }
            }
            return IsInitialized;
        }
        private void OnCameraImageGrabStart(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler<EventArgs>(OnCameraImageGrabStart), sender, e);
                return;
            }

            if (_isContinuousShot)
                _frameCount = 0;
        }

        private void OnCameraImageGrabEnd(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler<EventArgs>(OnCameraImageGrabEnd), sender, e);
                return;
            }
        }
        private void OnCameraClose(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler<EventArgs>(OnCameraClose), sender, e);
                return;
            }
        }
        private void OnCameraConnectionLost(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler<EventArgs>(OnCameraConnectionLost), sender, e);
                return;
            }
        }

        private void OnCameraConnectionOpen(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler<EventArgs>(OnCameraConnectionOpen), sender, e);
                return;
            }
        }

        private void OnCameraImageGrab(object sender, EventArgs e)
        {
            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new EventHandler<EventArgs>(OnCameraImageGrab), sender, e);
                    return;
                }

                GrabEndParam grabEnd = (GrabEndParam)sender as GrabEndParam;
                //System.Drawing.Image sourceImage = _sourceImage;

                if (grabEnd != null)
                {
                    //if (_isContinuousShot)
                    //{
                    //    pictureEditSystemImage.Image = grabEnd.Image;
                    //    _sourceImage = grabEnd.Image;
                    //}
                    //else
                    //{
                    //    pictureEditSystemImage.Image = grabEnd.Image;
                        
                    //}
                    _sourceImage = grabEnd.Image;
                    //ImageGrabbed?.Invoke(_sourceImage);
                    //UpdateImageEvent.Invoke(grabEnd.Image);
                    if (grabEnd.WaitHandle != null)
                        grabEnd.WaitHandle.Set();
                    _patternMatching = false;
                    _isOpticalMeasurement = false;

                }

                _isGrabbed = true;


                //if (sourceImage != null)
                //{
                //    sourceImage.Dispose();
                //    sourceImage = null;
                //}

                if (_isContinuousShot)
                {
                    _frameCount++;
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), ex.Message);
                mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), ex.StackTrace.ToString());
            }
        }


        public void UpdateRobotInfomation(RobotInformation update)
        {
            try
            {
                mRobotInformation.PositionX = update.PositionX;
                mRobotInformation.PositionY = update.PositionY;
                mRobotInformation.PositionZ = update.PositionZ;
                
                mRobotInformation.mStatus = update.mStatus;
                mRobotInformation.mError = update.mError;

                if (_IsHommingFinished)
                    mRobotInformation.SetStatus(RobotInformation.RobotStatus.OperationReady, _IsHommingFinished);

                if (mRobotInformation.GetStatus(RobotInformation.RobotStatus.OperationReady))
                    _IsHommingFinished = true;

                if (mRobotInformation.GetStatus(RobotInformation.RobotStatus.Error))
                {
                    _IsDrvErr = true;
                    mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "모션 드라이버에 알람 또는 에러가 발생했습니다.");
                }
                else
                    _IsDrvErr = false;
                
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "모션 정보 업데이트를 하지 못햇습니다.");
            }
        }
        public void UpdateRobotIOInfomation(RobotInformation update)
        {
            try
            {
                mRobotInformation.mInputData = update.mInputData;
                mRobotInformation.mOutputData = update.mOutputData;

            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "I/O 정보 업데이트를 하지 못햇습니다.");
            }
        }

        private void barListItemRecipeOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                barListItemRecipeOpen.Strings.Clear();

                InitializeRecipe();
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "레시피 불러오기 실행을 하지 못햇습니다.");
            }
        }

        private void barListItemRecipeOpen_ListItemClick(object sender, ListItemClickEventArgs e)
        {
            try
            {
                if (e.Index < 0)
                    return;

                RecipeOpen(barListItemRecipeOpen.Strings[e.Index]);
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "레시피 선택하기 실행을 하지 못햇습니다.");
            }
        }
        private void RecipeOpen(string strRecipeName)
        {
            try
            {
                string strSelectedPath = string.Format(@"{0}\{1}", SystemDirectoryParams.RecipeFolderPath, strRecipeName);

                if (!string.IsNullOrEmpty(strSelectedPath))
                {
                    string strRecipeFilePath = string.Format(@"{0}\{1}.rcp", strSelectedPath, strRecipeName);

                    if (!File.Exists(strRecipeFilePath))
                    {
                        MessageBox.Show(string.Format("레시피 파일을 불러올 수 없습니다. 경로를 확인해 주십시오.\r\n{0}", strRecipeFilePath), "불러오기 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Recipe File 읽기
                    RecipeFileIO.ReadRecipeFile(_workParams, strRecipeFilePath);

                    _workParams.ImageCenterX = (_systemParams._cameraParams.HResolution / 2);
                    _workParams.ImageCenterY = (_systemParams._cameraParams.VResolution / 2);
                    // inspection flag

                    _IsReciepLoad = true;
                    mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("레시피 읽기 완료. 레시피 경로:{0}", strRecipeFilePath));
                }
                this.Text = string.Format("{0} - {1}.rcp", _strTitle, strRecipeName);
            }
            catch (Exception ex)
            {
                mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), string.Format("{0}\r\n{1}", ex.Message, ex.StackTrace));
            }
        }
        private void InitializedBackGroundWorkers()
        {
            try
            {
                _backgroundWorkerAutoSoldering.WorkerReportsProgress = true;
                _backgroundWorkerAutoSoldering.WorkerSupportsCancellation = true;
                _backgroundWorkerAutoSoldering.DoWork += new DoWorkEventHandler(backgroundWorkerAutoSoldering_DoWork);
                _backgroundWorkerAutoSoldering.ProgressChanged += new ProgressChangedEventHandler(backgroundWorkerAutoSoldering_ProgressChanged);
                _backgroundWorkerAutoSoldering.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorkerAutoSoldering_RunWorkerCompleted);
                _bwMotionHome.DoWork += new DoWorkEventHandler(backgroundWorkerMotionHome_DoWork);
                _bwMotionHome.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorkerMotionHome_RunWorkerCompleted);
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "백그라운드 작업 등록을 하지 못햇습니다.");
            }
        }
        public void AutoStartButtonLock()
        {
            try
            {
                ribbonSystemPage.Enabled = false;
                ribbonPageGroupFile.Enabled = false;
                ribbonPageGroupConnection.Enabled = false;
                //ribbonPageGroupMotionControl.Enabled = false;
                xtraTabControlMainCtrl.Enabled = false;
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "프로그램 자동 모드의 버튼들을 설정하지 못햇습니다.");
            }
        }
        public void AutoStartButtonRelease()
        {
            try
            {
                ribbonSystemPage.Enabled = true;
                ribbonPageGroupFile.Enabled = true;
                ribbonPageGroupConnection.Enabled = true;
                //ribbonPageGroupMotionControl.Enabled = true;
                xtraTabControlMainCtrl.Enabled = true;
                motionControl.Enabled = true;
                remoteIOControl.Enabled = true;
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "프로그램 자동 모드의 버튼들을 해제하지 못햇습니다.");
            }
        }

        private void barButtonItemCameraListRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool IsInitialized = false;
            _isCameraInitialized = IsInitialized;
            
            _Camera.OnCameraConnectionLost += new BaslerCamera.EventCameraConnectionLost(OnCameraConnectionLost);            
            _Camera.OnCameraConnectionOpen += new BaslerCamera.EventCameraConnectionOpen(OnCameraConnectionOpen);            
            _Camera.OnCameraImageGrab += new BaslerCamera.EventCameraImageGrab(OnCameraImageGrab);            
            _Camera.OnCameraClose += new BaslerCamera.EventCameraClose(OnCameraClose);            
            _Camera.OnCameraImageGrabStart += new BaslerCamera.EventCameraImageGrab(OnCameraImageGrabStart);            
            _Camera.OnCameraImageGrabEnd += new BaslerCamera.EventCameraImageGrab(OnCameraImageGrabEnd);

            // 카메라 라이브러리 로그 연결
            _Camera._log.WriteLogViewer += new Log.EventWriteLogViewer(LogUpdated);

            List<string> liststrFriendlyNames = _Camera.FindCameras();
            try
            {
                //rowCameraFriendlyName.Properties.Value = liststrFriendlyNames[0];

                Cameraname = liststrFriendlyNames[0];
                if (_Camera.Open(liststrFriendlyNames[0]))
                {
                    CameraParameters cameraParam = new CameraParameters();



                    // 노출 시간
                    cameraParam = _Camera.ExposureTime;
                    textEditExposureTime.Text = cameraParam.Value.ToString();
                    //Cameraexposuretime = (int)cameraParam.Value;
                    //_systemParams.CameraParameters.ExposureTime = (int)cameraParam.Value;

                    // 게인
                    cameraParam = _Camera.Gain;
                    textEditGain.Text = cameraParam.Value.ToString();
                    //_systemParams.CameraParameters.Gain = (int)cameraParam.Value;                        

                    // Frame Rate
                    cameraParam = _Camera.FrameRate;
                    textEditFrameRatio.Text = cameraParam.Value.ToString();
                    //_systemParams.CameraParameters.FrameRate = (int)cameraParam.Value;                        

                    cameraParam = _Camera.Width;
                    _ImageHResolution = (int)cameraParam.Value;
                    //_systemParams.CameraParameters.HResolution = (int)cameraParam.Value;                        

                    cameraParam = _Camera.Height;
                    _ImageVResolution = (int)cameraParam.Value;
                    //_systemParams.CameraParameters.VResolution = (int)cameraParam.Value;                        


                    _isCameraInitialized = true;
                    _isCameraOpen = true;
                    // System 파라미터를 Update한다.
                    //UpdateImageEvent += UpdateImageData;
                    timerImageUpdate.Start();
                    //RecipeFileIO.WriteSystemFile(_systemParams, string.Format(@"{0}\{1}", SystemDirectoryParams.SystemFolderPath, SystemDirectoryParams.SystemFileName));

                    mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("카메라 연결 성공:{0}", liststrFriendlyNames[0]));
                    mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(),
                        string.Format("노출 시간:{0}, 게인:{1}, 프레임비:{2}", textEditExposureTime.Text, textEditGain.Text, textEditFrameRatio.Text));

                }
                else
                {
                    mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), string.Format("카메라 연결 실패:{0}", liststrFriendlyNames[0]));
                }
            } 
            catch (Exception ex)
            {
                _Camera.Close();
                _isCameraOpen = false;
                mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), string.Format("카메라 연결 실패:{0}", liststrFriendlyNames[0]));
                mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), string.Format("{0}\r\n{1}", ex.Message, ex.StackTrace));
            }
        }

        private void barButtonItemSingleShot_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_Camera.IsAllocated)
            {
                try
                {
                    _Camera.OneShot(_waitHandle);
                    
                    pictureEditSystemImage.Refresh();
                    mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), "싱글 샷");
                    ImageFitSize();
                }
                catch (Exception)
                {
                    mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), string.Format("싱글 샷 명령이 실행되지 않았습니다."));
                }
            }
        }

        private void barButtonItemContinueousShot_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (_Camera.IsAllocated)
                {
                    try
                    {
                        _Camera.ContinuousShot(_systemParams._cameraParams.FrameRate);
                        _isContinuousShot = true;
                        _frameCount = 0;
                        mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), "연속 샷 시작");
                    }
                    catch (Exception)
                    {
                        mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), string.Format("연속 샷 명령이 실행되지 않았습니다."));
                    }
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }

        private void barButtonItemCameraStop_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (_Camera.IsAllocated)
                {
                    try
                    {
                        _Camera.Stop();
                        _isContinuousShot = false;
                        mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), "연속 샷 정지");
                    }
                    catch (Exception)
                    {
                        mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), string.Format("연속 샷 정지 명령이 실행되지 않았습니다."));
                    }
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }
        public void ImageFitSize()
        {
            try
            {
                if (_sourceImage != null)
                {
                    try
                    {
                        float width = pictureEditSystemImage.ClientSize.Width * 100.0f / _sourceImage.Width;
                        float height = (pictureEditSystemImage.ClientSize.Height - pictureEditSystemImage.ClientSize.Height * 0.01f) * 100.0f / _sourceImage.Height;

                        float i = Math.Min(100.0f, Math.Min(width, height));

                        pictureEditSystemImage.Properties.ZoomPercent = i;
                        pictureEditSystemImage.HScrollBar.Value = 0;
                        pictureEditSystemImage.VScrollBar.Value = 0;

                        _isImageFitSize = true;
                        //mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("원본 화면 맞춤: {0:0.0}%", pictureEditSystemImage.Properties.ZoomPercent));
                    }
                    catch (Exception)
                    {
                        mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), string.Format("이미지 화면 맞춤 명령이 실행되지 않았습니다."));
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void UpdateImageData(System.Drawing.Image image)
        {
            pictureEditSystemImage.Image = image;
            pictureEditSystemImage.Refresh();
            //ImageFitSize();
            //pictureEditSystemImage.Properties.SizeMode = PictureBoxSizeMode.Zoom;
            //ICogImage img = new CogImage8Grey((Bitmap)image);
            //cogDisplayImage.Record = null;
            //grabImage = img;
            //cogDisplayImage.Image = grabImage;
            //cogDisplayImage.Fit();
            GC.Collect();
        }
        private void atLaserSoldering_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_systemParams._SystemLanguageKoreaUse)
            {
                if (MessageBox.Show(string.Format("레이저 자동납땜 시스템을 종료하시겠습니까?"), "시스템 종료", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                if (MessageBox.Show(string.Format("Are you closing laser autosoldering Program?"), "System Closed", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
            }
            if (_isContinuousShot)
                _Camera.Stop();            

            if (_Camera.IsAllocated)
                _Camera.Close();
            
            timerCurrentTime.Stop();
            timerImageUpdate.Stop();
            mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), "프로그램 종료.");
        }

        private void pictureEditSystemImage_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (pictureEditSystemImage.Image == null)
                    return;

                Graphics gp = e.Graphics;

                int InspectionOpticalSpotCenterX = _systemParams._cameraParams.HResolution / 2;
                int InspectionOpticalSpotCenterY = _systemParams._cameraParams.VResolution / 2;
                float fScale = (float)(pictureEditSystemImage.Properties.ZoomPercent / 100.0f);
                float fHScroll = pictureEditSystemImage.HScrollBar.Value;
                float fVScroll = pictureEditSystemImage.VScrollBar.Value;
                float fCharacter = (fScale > 1f) ? 1f : fScale;

                float fImageWidth = pictureEditSystemImage.Image.Width;
                float fImageHeight = pictureEditSystemImage.Image.Height;
                float fCenterx = InspectionOpticalSpotCenterX;
                float fCentery = InspectionOpticalSpotCenterY;

                Matrix matrix = new Matrix();
                matrix.Scale(fScale, fScale);
                matrix.Translate(-fHScroll / fScale, -fVScroll / fScale);

                gp.Transform = matrix;
                
                // 중심선 그리기
                if (_isShowCenterMark)
                {
                    if (fScale <= 1.0f)
                    {
                        float imageWidth = pictureEditSystemImage.Image.Width * fScale;
                        float imageHeight = pictureEditSystemImage.Image.Height * fScale;

                        //PointF fptHLineStart = new PointF(0, imageHeight / 2f);
                        //PointF fptHLineEnd = new PointF(imageWidth, imageHeight / 2f);
                        //PointF fptVLineStart = new PointF(imageWidth / 2f, 0);
                        //PointF fptVLineEnd = new PointF(imageWidth / 2f, imageHeight);
                        PointF fptHLineStart = new PointF(0, InspectionOpticalSpotCenterY);
                        PointF fptHLineEnd = new PointF(InspectionOpticalSpotCenterX * 2, InspectionOpticalSpotCenterY);
                        PointF fptVLineStart = new PointF(InspectionOpticalSpotCenterX, 0);
                        PointF fptVLineEnd = new PointF(InspectionOpticalSpotCenterX, InspectionOpticalSpotCenterY * 2);

                        gp.DrawLine(Pens.Red, fptHLineStart, fptHLineEnd);
                        gp.DrawLine(Pens.Red, fptVLineStart, fptVLineEnd);
                    }
                    else
                    {
                        float imageWidth = pictureEditSystemImage.Image.Width * fScale;
                        float imageHeight = pictureEditSystemImage.Image.Height * fScale;

                        PointF fptHLineStart = new PointF(0, (imageHeight / 2f) / fScale);
                        PointF fptHLineEnd = new PointF(imageWidth, (imageHeight / 2f) / fScale);
                        PointF fptVLineStart = new PointF((imageWidth / 2f) / fScale, 0);
                        PointF fptVLineEnd = new PointF((imageWidth / 2f) / fScale, imageHeight);
                        //PointF fptHLineStart = new PointF(0, imageHeight / 2f - fVScroll);                // 사이즈가 유동적일 때 스크롤 위치에 따른 중심점 보정
                        //PointF fptHLineEnd = new PointF(imageWidth, imageHeight / 2f - fVScroll);
                        //PointF fptVLineStart = new PointF(imageWidth / 2f - fHScroll, 0);
                        //PointF fptVLineEnd = new PointF(imageWidth / 2f - fHScroll, imageHeight);


                        gp.DrawLine(Pens.Red, fptHLineStart, fptHLineEnd);
                        gp.DrawLine(Pens.Red, fptVLineStart, fptVLineEnd);
                    }
                }



            }
            catch (Exception ex)
            {
                ;
            }
        }

        private void contextMenuStripImageViewControl_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "View Center Mark":
                    if (_isShowCenterMark == false)
                    {
                        _isShowCenterMark = true;
                        //e.ClickedItem.Image = global::atOpticalDecenter.Properties.Resources.Apply_16x16;
                    }
                    else
                    {
                        _isShowCenterMark = false;
                        //e.ClickedItem.Image = global::atOpticalDecenter.Properties.Resources.Cancel_16x16;
                        pictureEditSystemImage.Refresh();
                    }
                    break;
                case "Set Work ROI":
                    if (_isSetROICheck == false)
                    {
                        _isSetROICheck = true;
                        //e.ClickedItem.Image = global::atOpticalDecenter.Properties.Resources.Apply_16x16;
                    }
                    else
                    {
                        _isSetROICheck = false;
                        pictureEditSystemImage.Refresh();
                    }
                    break;
                case "Clear Work ROI":
                    _fptAreaStart = new PointF();
                    _fptAreaEnd = new PointF();
                    _frtArearect = new RectangleF();
                    pictureEditSystemImage.Refresh();
                    break;
                case "Fit Size Image":
                    ImageFitSize();
                    break;

            }
        }

        private void pictureEditSystemImage_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (pictureEditSystemImage.Image == null)//|| barCheckItemInspectionStart.Checked)
                    return;

                GraphicsPath path = new GraphicsPath();

                float fScale = (float)(pictureEditSystemImage.Properties.ZoomPercent / 100f);
                float fHScroll = pictureEditSystemImage.HScrollBar.Value;
                float fVScroll = pictureEditSystemImage.VScrollBar.Value;

                path.AddRectangle(_frtCrop);
                path.AddRectangle(_frtArearect);

                if (e.Button == MouseButtons.Right)
                {
                    contextMenuStripImageViewControl.Show(e.Location);
                }
                //PointF fptTemp = Utils.PointDrawToReal(e.Location, fScale, fHScroll, fVScroll);

                //if (barCheckItemImageCrop.Checked)
                //{
                //    if (e.Button == MouseButtons.Left)
                //    {
                //        if (!path.IsVisible(fptTemp))
                //        {
                //            _fptCropStart = fptTemp;
                //            _fptCropEnd = fptTemp;

                //            pictureEditSystemImage.Cursor = Cursors.Cross;
                //        }
                //        else
                //        {
                //            _fptMoveStart = fptTemp;
                //            pictureEditSystemImage.Cursor = Cursors.SizeAll;

                //            _isCropMove = true;
                //        }
                //    }
                //    else if (e.Button == MouseButtons.Right)
                //    {
                //        Point ptPos = new Point(e.X, pictureEditSystemImage.Size.Height + e.Y);

                //        if (path.IsVisible(fptTemp))
                //        {
                //            popupMenuTemplateCrop.ShowPopup(ptPos);
                //        }
                //    }
                //}
                //else
                //{
                //    if (e.Button == MouseButtons.Right)
                //    {
                //        contextMenuStripImageViewControl.Show(e.Location);
                //    }
                //}
                //if (_isSetROICheck)
                //{
                //    if (e.Button == MouseButtons.Left)
                //    {
                //        if (!path.IsVisible(fptTemp))
                //        {
                //            _fptAreaStart = fptTemp;
                //            _fptAreaEnd = fptTemp;

                //            pictureEditSystemImage.Cursor = Cursors.Cross;
                //        }
                //        else
                //        {
                //            _fptMoveStart = fptTemp;
                //            pictureEditSystemImage.Cursor = Cursors.SizeAll;
                //            _isAreaMove = true;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                ;
            }
        }

        private void timerImageUpdate_Tick(object sender, EventArgs e)
        {
            pictureEditSystemImage.Image = _sourceImage;
            pictureEditSystemImage.Refresh();
            GC.Collect();
        }
    }
}