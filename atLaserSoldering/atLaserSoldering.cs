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
using Cognex.VisionPro;
using Cognex.VisionPro.Implementation;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.QuickBuild;
using Cognex.VisionPro.ToolGroup;
using Cognex.VisionPro.ToolBlock;

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
        CogToolBlock _AlignToolBlock = null;
        CogToolBlock _InspectToolBlock = null;

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
        bool _IsAutoSolderingRunning = false;
        bool _IsAutoSolderingEnd = false;        
        bool _IsMovementVision = false;
        int _CalibratoinMode = 0;
        double _dTotalElapsedTime = 0.0f;
        public atLaserSoldering()
        {
            InitializeComponent();
            _mMotionControlCommManager = new AiCControlLibrary.SerialCommunication.Control.CommunicationManager();
            _mRemoteIOCommManager = new ArioModbusLibrary.SerialCommunication.Control.CommunicationManager();

            _mFeederCommManager = new FeederControlLibrary.SerialCommunication.Control.CommunicationManager();
            _mLaserCommManager = new CoherentCompactMini.SerialCommunication.Control.CommunicationManager();
            _mLaserSoldering = new LaserSoldering.LaserSoderingProcess();
            //_mLaserSoldering.InitialCommunication(_mLaserCommManager, _mFeederCommManager);
            radioGroupCalibrationMode.SelectedIndex = 0;
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
                laserSolderingControl.LogWriteEvent += Soldering_StringToLogWriteEvent;
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
        private void Soldering_StringToLogWriteEvent(string strLog)
        {
            try
            {
                mLog.WriteLog(LogLevel.Info, LogClass.LaserSoldering.ToString(), strLog);
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "Laser Soldering 로그 이벤트에 오류가 있습니다.");
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
                laserSolderingControl.SetCommunicateManager(_mLaserSoldering,ref _mFeederCommManager,ref _mLaserCommManager, _waitHandle);
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

                if (!_isCameraOpen)
                    barButtonItemConnectionCamera.ImageOptions.Image = Properties.Resources.disconnect_16x16;
                else
                    barButtonItemConnectionCamera.ImageOptions.Image = Properties.Resources.connect_16x16;

                if (!_mLaserSoldering.IsLaserConnect)
                    barButtonItemConnectionLaser.ImageOptions.Image = Properties.Resources.disconnect_16x16;
                else
                    barButtonItemConnectionLaser.ImageOptions.Image = Properties.Resources.connect_16x16;

                if (!_mLaserSoldering.IsFeederConnect)
                    barButtonItemConnectionFeeder.ImageOptions.Image = Properties.Resources.disconnect_16x16;
                else
                    barButtonItemConnectionFeeder.ImageOptions.Image = Properties.Resources.connect_16x16;

                //if (!_mLaserSoldering.IsFeederConnect)
                //    barButtonItemConnectionCamera.ImageOptions.Image = Properties.Resources.disconnect_16x16;
                //else
                //    barButtonItemConnectionCamera.ImageOptions.Image = Properties.Resources.connect_16x16;

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
                        comboBoxEditCameraName.Properties.Items.Add(Cameraname);
                        if (_Camera.Open(liststrFriendlyNames[0]))
                        {
                            CameraParameters cameraParam = new CameraParameters();

                            

                            // 노출 시간
                            cameraParam = _Camera.ExposureTime;
                            textEditExposureTime.Text = cameraParam.Value.ToString();
                            trackBarControlExposureTime.EditValue = Convert.ToInt32(textEditExposureTime.Text);
                            //Cameraexposuretime = (int)cameraParam.Value;
                            //_systemParams.CameraParameters.ExposureTime = (int)cameraParam.Value;

                            // 게인
                            cameraParam = _Camera.Gain;
                            textEditGain.Text = cameraParam.Value.ToString();
                            trackBarControlGain.EditValue = Convert.ToInt32(textEditGain.Text);
                            //_systemParams.CameraParameters.Gain = (int)cameraParam.Value;                        

                            // Frame Rate
                            cameraParam = _Camera.FrameRate;
                            textEditFrameRatio.Text = cameraParam.Value.ToString();
                            trackBarControlFrameRatio.EditValue = Convert.ToInt32(textEditFrameRatio.Text);
                            //_systemParams.CameraParameters.FrameRate = (int)cameraParam.Value;                        

                            cameraParam = _Camera.Width;
                            _ImageHResolution = (int)cameraParam.Value;
                            //_systemParams.CameraParameters.HResolution = (int)cameraParam.Value;                        

                            cameraParam = _Camera.Height;
                            _ImageVResolution = (int)cameraParam.Value;
                            //_systemParams.CameraParameters.VResolution = (int)cameraParam.Value;                        


                            _isCameraInitialized = true;
                            _isCameraOpen = true;
                            comboBoxEditCameraName.SelectedIndex = 0;
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
                    comboBoxEditCameraName.SelectedIndex = 0;

                    timerImageUpdate.Start();
                    
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
                //mRobotInformation.PositionX = update.PositionX;
                //mRobotInformation.PositionY = update.PositionY;
                //mRobotInformation.PositionZ = update.PositionZ;

                //mRobotInformation.mStatus = update.mStatus;
                //mRobotInformation.mError = update.mError;
                //mRobotInformation.DrvStatus = update.DrvStatus;

                mRobotInformation = update;

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

                if ((mRobotInformation.TargetPositionX == _RobotTargetPosition[0]) &&
                    (mRobotInformation.TargetPositionY == _RobotTargetPosition[1]) &&
                    (mRobotInformation.TargetPositionZ == _RobotTargetPosition[2])
                    )
                {
                    if (Convert.ToBoolean((mRobotInformation.mStatus >> 6) & 0x01))
                    {
                        if(_IsAutoSolderingRunning)
                            _waitHandle.Set();
                    }
                }
                
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
                comboBoxEditCameraName.Properties.Items.Add(Cameraname);
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
                    comboBoxEditCameraName.SelectedIndex = 0;
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

            motionControl.UpdateTimer.Stop();
            remoteIOControl.UpdateTimer.Stop();
            laserSolderingControl.UpdateTimer.Stop();
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
                case "Set Vision Move":
                    if (_IsMovementVision == false)
                    {
                        _IsMovementVision = true;
                    }
                    else
                    {
                        _IsMovementVision = false;
                    }
                    break;

            }
        }

        private void pictureEditSystemImage_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (pictureEditSystemImage.Image == null || _IsAutoSolderingRunning)
                    return;

                GraphicsPath path = new GraphicsPath();

                float fScale = (float)(pictureEditSystemImage.Properties.ZoomPercent / 100f);
                float fHScroll = pictureEditSystemImage.HScrollBar.Value;
                float fVScroll = pictureEditSystemImage.VScrollBar.Value;

                path.AddRectangle(_frtCrop);
                path.AddRectangle(_frtArearect);

                PointF fptTemp = Utils.PointDrawToReal(e.Location, fScale, fHScroll, fVScroll);

                if (_IsMovementVision && e.Button == MouseButtons.Left)
                {
                    if (_IsMovementVision)
                    {
                        float fMoveX = (float)Math.Round((fptTemp.X - (_systemParams._cameraParams.HResolution / 2f)) * _systemParams._cameraParams.OnePixelResolution,3);// * 0.001f;
                        float fMoveY = (float)Math.Round((fptTemp.Y - (_systemParams._cameraParams.VResolution / 2f)) * _systemParams._cameraParams.OnePixelResolution,3);// * 0.001f;

                        // Robot Move Command
                        double[] pos = new double[3];
                        pos[0] = mRobotInformation.PositionX + (fMoveX * _systemParams._calibrationParams._imagetoSystemXcoordi);
                        pos[1] = mRobotInformation.PositionY + (fMoveY * _systemParams._calibrationParams._imagetoSystemYcoordi);
                        pos[2] = mRobotInformation.PositionZ;
                        motionControl.SendCmdPosition(pos);
                        mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("X:{0}mm, Y:{1}mm, Z:{2}mm 이동", pos[0], pos[1], pos[2]));
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    Point ptPos = new Point((int)fptTemp.X, (int)fptTemp.Y);
                    contextMenuStripImageViewControl.Show(pictureEditSystemImage,e.Location);
                }


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
        private void SetCameraExposureTime(CameraParameters cameraParam)
        {
            double oldValue = _Camera.ExposureTime.Value;

            if (cameraParam.Value >= trackBarControlExposureTime.Properties.Minimum && cameraParam.Value <= trackBarControlExposureTime.Properties.Maximum)
            {

                //trackBarControlExposureTime.PropertiesCollection[1].Value = cameraParam.Value;
                //trackBarControlExposureTime.PropertiesCollection[0].Value = cameraParam.Value;

                _Camera.ExposureTime = cameraParam;

                mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("카메라 노출 시간 변경(변경 전:{0}, 변경 후:{1})", (int)oldValue, (int)cameraParam.Value));
            }
        }
        private void SetCameraFrameRate(CameraParameters cameraParam)
        {
            double oldValue = _Camera.FrameRate.Value;

            if (cameraParam.Value >= trackBarControlFrameRatio.Properties.Minimum && cameraParam.Value <= trackBarControlFrameRatio.Properties.Maximum)
            {

                //rowCameraFrameRate.PropertiesCollection[1].Value = cameraParam.Value;
                //rowCameraFrameRate.PropertiesCollection[0].Value = cameraParam.Value;

                _Camera.FrameRate = cameraParam;

                mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("카메라 프레임 비(fps) 변경(변경 전:{0}, 변경 후:{1})", (int)oldValue, (int)cameraParam.Value));
            }
        }

        private void SetCameraGain(CameraParameters cameraParam)
        {
            double oldValue = _Camera.Gain.Value;

            if (cameraParam.Value >= trackBarControlGain.Properties.Minimum && cameraParam.Value <= trackBarControlGain.Properties.Maximum)
            {
                //rowCameraGain.PropertiesCollection[1].Value = cameraParam.Value;
                //rowCameraGain.PropertiesCollection[0].Value = cameraParam.Value;

                _Camera.Gain = cameraParam;

                mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("카메라 게인 변경(변경 전:{0}, 변경 후:{1})", (int)oldValue, (int)cameraParam.Value));
            }
        }
        private void textEditExposureTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CameraParameters cameraParam = new CameraParameters();
                cameraParam.Value = Convert.ToDouble((sender as TextEdit).Text);
                trackBarControlExposureTime.EditValue = Convert.ToDouble((sender as TextEdit).Text);
                SetCameraExposureTime(cameraParam);
            }
        }

        private void trackBarControlExposureTime_MouseUp(object sender, MouseEventArgs e)
        {
            DevExpress.XtraEditors.TrackBarControl trackBar = sender as DevExpress.XtraEditors.TrackBarControl;
            if (trackBar != null)
            {
                // 드래그가 끝난 최종 시점의 값을 가져옵니다.
                
                CameraParameters cameraParam = new CameraParameters();
                cameraParam.Value = trackBar.Value;
                SetCameraExposureTime(cameraParam);
                textEditExposureTime.Text = trackBar.Value.ToString();
            }
        }

        private void textEditFrameRatio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CameraParameters cameraParam = new CameraParameters();
                cameraParam.Value = Convert.ToDouble((sender as TextEdit).Text);                
                trackBarControlFrameRatio.EditValue = Convert.ToDouble((sender as TextEdit).Text);
                SetCameraFrameRate(cameraParam);
            }
        }
        private void trackBarControlFrameRatio_MouseUp(object sender, MouseEventArgs e)
        {
            DevExpress.XtraEditors.TrackBarControl trackBar = sender as DevExpress.XtraEditors.TrackBarControl;
            if (trackBar != null)
            {
                // 드래그가 끝난 최종 시점의 값을 가져옵니다.

                CameraParameters cameraParam = new CameraParameters();
                cameraParam.Value = trackBar.Value;
                SetCameraFrameRate(cameraParam);
                textEditFrameRatio.Text = trackBar.Value.ToString();
            }
        }
        private void textEditGain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CameraParameters cameraParam = new CameraParameters();
                cameraParam.Value = Convert.ToDouble((sender as TextEdit).Text);
                SetCameraGain(cameraParam);
                trackBarControlGain.EditValue = Convert.ToDouble((sender as TextEdit).Text);                
            }
        }
        private void trackBarControlGain_MouseUp(object sender, MouseEventArgs e)
        {
            DevExpress.XtraEditors.TrackBarControl trackBar = sender as DevExpress.XtraEditors.TrackBarControl;
            if (trackBar != null)
            {
                // 드래그가 끝난 최종 시점의 값을 가져옵니다.
                CameraParameters cameraParam = new CameraParameters();
                cameraParam.Value = trackBar.Value;
                textEditGain.Text = trackBar.Value.ToString();
                SetCameraGain(cameraParam);
            }
        }

        private void barButtonItemConnectAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (e.Item.Name == "barButtonItemConnectAll")
                {
                    if (!_mMotionControlCommManager.IsOpen())
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

                    if (!_mRemoteIOCommManager.IsOpen())
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
                            mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("ARIO 통신 연결 성공."));
                        }
                        else
                            mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("ARIO 통신 연결 실패."));
                    }

                    if (!_Camera.IsOpen)
                    {
                        if (_Camera.Open(Cameraname))
                        {
                            _isCameraOpen = true;
                            mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), string.Format("카메라 연결 성공:{0}", Cameraname));
                        }
                        else
                        {
                            _isCameraOpen = false;
                            mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), string.Format("카메라 연결 실패:{0}", Cameraname));
                        }
                    }
                }
                else if (e.Item.Name == "barButtonItemConnectionAiC")
                {
                    if (!_mMotionControlCommManager.IsOpen())
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
                        //_mMotionControlCommManager.Disconnect();
                        motionControl.ConnectionClosed();
                        mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("AiC 통신 연결 해제 성공."));
                    }
                }
                else if (e.Item.Name == "barButtonItemConnectionRemeteIO")
                {
                    if (!_mRemoteIOCommManager.IsOpen())
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
                            mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("ARM 통신 연결 성공."));
                        }
                        else
                            mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("ARM 통신 연결 실패."));
                    }
                    else
                    {
                        //_mMotionControlCommManager.Disconnect();
                        remoteIOControl.ConnectionClosed();
                        mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("ARM 통신 연결 해제 성공."));
                    }
                }
                else if (e.Item.Name == "barButtonItemConnectionCamera")
                {
                    if (_isContinuousShot)
                        _Camera.Stop();

                    if (!_Camera.IsAllocated)
                    {
                        if (!string.IsNullOrEmpty(Cameraname))
                        {
                            if (_Camera.Open(Cameraname))
                            {
                                _isCameraOpen = true;
                                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), string.Format("카메라 연결 성공:{0}", Cameraname));
                            }
                            else
                            {
                                _isCameraOpen = false;
                                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), string.Format("카메라 연결 실패:{0}", Cameraname));
                            }
                        }
                        else
                        {
                            _isCameraOpen = false;
                            mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), string.Format("카메라 이름이 없습니다:{0}", Cameraname));
                        }
                    }
                    else
                    {
                        _Camera.Close();
                        _isCameraOpen = false;
                        mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("카메라 연결이 끊겼습니다."));
                    }

                }
                else if ((e.Item.Name == "barButtonItemConnectionLaser") || (e.Item.Name == "barButtonItemConnectionFeeder"))
                {
                    if (!_mLaserSoldering.IsSolderingConnect)
                    {
                        LaserSolderingModuleConnect();
                    }
                    else
                    {
                        LaserSolderingModuleDisConnect();
                    }
                }
                else if (e.Item.Name == "barButtonItemConnectionLight")
                {

                }
                else
                {
                    ;
                }
                UpdateConnectStatusForAll();
            }
            catch (Exception ex)
            {
                ;
            }
        }

        private void simpleButtonSingleGrab_Click(object sender, EventArgs e)
        {
            if (_Camera.IsAllocated)
            {
                try
                {
                    _Camera.OneShot(_waitHandle);

                    pictureEditSystemImage.Refresh();
                    mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), "이미지를 획득하였습니다.");
                    ImageFitSize();
                }
                catch (Exception)
                {
                    mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), string.Format("이미지 획득 명령이 실행되지 않았습니다."));
                }
            }
        }

        private void simpleButtonImageLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialogImageFileOpen.ShowDialog() == DialogResult.OK)
                {
                    _sourceImage = System.Drawing.Image.FromFile(openFileDialogImageFileOpen.FileName);
                    pictureEditSystemImage.Image = _sourceImage;
                    //_patternMatching = false;
                    //_isOpticalMeasurement = false;
                    pictureEditSystemImage.Refresh();
                    ImageFitSize();
                }
                mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("{0}파일 이미지 불러오기", openFileDialogImageFileOpen.FileName));
            }
            catch (Exception ex)
            {
                mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("{0}\r\n{1}", ex.Message, ex.StackTrace));
            }
        }

        private void pictureEditSystemImage_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void trackBarControlLedBright_MouseUp(object sender, MouseEventArgs e)
        {
            DevExpress.XtraEditors.TrackBarControl trackBar = sender as DevExpress.XtraEditors.TrackBarControl;
            if (trackBar != null)
            {
                // 드래그가 끝난 최종 시점의 값을 가져옵니다.
                int bright = 0;
                bright = trackBar.Value;
                textEditLightBright.Text = trackBar.Value.ToString();
                
            }
        }

        private void textEditLightBright_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int bright = 0;
                bright = Convert.ToInt32((sender as TextEdit).Text);
                
                trackBarControlLedBright.EditValue = Convert.ToInt32((sender as TextEdit).Text);
            }
        }

        private void simpleButtonCalibration_Click(object sender, EventArgs e)
        {
            try
            {
                if (_systemParams._calibrationParams.CalibrationMode == 2)
                {
                    CalibrationParams.Calibration_Position p1 = new CalibrationParams.Calibration_Position();
                    CalibrationParams.Calibration_Position p2 = new CalibrationParams.Calibration_Position();

                    p1.X = Convert.ToDouble(textEditCalcPositoin1_X.Text);
                    p1.Y = Convert.ToDouble(textEditCalcPositoin1_Y.Text);
                    p1.Delta_X = Convert.ToDouble(textEditDeltaX1.Text);
                    p1.Delta_Y = Convert.ToDouble(textEditDeltaY1.Text);

                    p2.X = Convert.ToDouble(textEditCalcPositoin2_X.Text);
                    p2.Y = Convert.ToDouble(textEditCalcPositoin2_Y.Text);
                    p2.Delta_X = Convert.ToDouble(textEditDeltaX2.Text);
                    p2.Delta_Y = Convert.ToDouble(textEditDeltaY2.Text);

                    _systemParams._calibrationParams.Calibration_TwoPoint(p1, p2);

                    p1.X = Convert.ToDouble(textEditCalcTargetPosX.Text);
                    p1.Y = Convert.ToDouble(textEditCalcTargetPosY.Text);
                    _systemParams._calibrationParams.CalibrationOffset_TwoPoint(p1);
                    textEditOffsetX.Text = _systemParams._calibrationParams.OffsetX.ToString();
                    textEditOffsetY.Text = _systemParams._calibrationParams.OffsetY.ToString();
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }

        private void radioGroupCalibrationMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            _CalibratoinMode = radioGroupCalibrationMode.SelectedIndex;
            _systemParams._calibrationParams.CalibrationMode = _CalibratoinMode;
            if (_CalibratoinMode == 2)
            {
                textEditCalcPositoin1_X.Enabled = true;
                textEditCalcPositoin1_Y.Enabled = true;
                textEditDeltaX1.Enabled = true;
                textEditDeltaY1.Enabled = true;
                textEditCalcPositoin2_X.Enabled = true;
                textEditCalcPositoin2_Y.Enabled = true;
                textEditDeltaX2.Enabled = true;
                textEditDeltaY2.Enabled = true;
            }
            else
            {
                textEditCalcPositoin1_X.Enabled = false;
                textEditCalcPositoin1_Y.Enabled = false;
                textEditDeltaX1.Enabled = false;
                textEditDeltaY1.Enabled = false;
                textEditCalcPositoin2_X.Enabled = false;
                textEditCalcPositoin2_Y.Enabled = false;
                textEditDeltaX2.Enabled = false;
                textEditDeltaY2.Enabled = false;
            }

        }

        private void barButtonItemImageSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (pictureEditSystemImage.Image != null)
            {
                //if (barCheckItemImageCrop.Checked)
                //{
                //    string strBackupFilter = saveFileDialogImage.Filter;

                //    try
                //    {
                //        saveFileDialogImage.Filter = "Bitmap Files(*.bmp) | *.bmp";

                //        if (saveFileDialogImage.ShowDialog() == DialogResult.OK)
                //        {
                //            Bitmap templete = new Bitmap(_sourceImage.Width, _sourceImage.Height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                //            templete = ConverterColorToGray((Bitmap)pictureEditSystemImage.Image);
                //            if (_frtCrop.Width > 0 && _frtCrop.Height > 0)
                //                Utils.SaveImage((Bitmap)templete, _frtCrop, saveFileDialogImage.FileName);
                //            else
                //                mLog.WriteLog(LogLevel.Fatal, LogClass.atPhoto.ToString(), string.Format("이미지 저장 실패(너비:{0}, 높이:{1})", (int)_frtCrop.Width, (int)_frtCrop.Height));
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        mLog.WriteLog(LogLevel.Fatal, LogClass.atPhoto.ToString(), ex.StackTrace.ToString());
                //    }
                //    finally
                //    {
                //        saveFileDialogImage.Filter = strBackupFilter;
                //    }

                //}
                //else
                {
                    try
                    {
                        if (saveFileDialogImage.ShowDialog() == DialogResult.OK)
                        {
                            Bitmap templete = new Bitmap(pictureEditSystemImage.Image.Width, pictureEditSystemImage.Image.Height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                            templete = ConverterColorToGray((Bitmap)pictureEditSystemImage.Image);
                            pictureEditSystemImage.Image = templete;
                            
                            ///*
                            if (saveFileDialogImage.FilterIndex == 1)
                                pictureEditSystemImage.Image.Save(saveFileDialogImage.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                            else if (saveFileDialogImage.FilterIndex == 2)
                                pictureEditSystemImage.Image.Save(saveFileDialogImage.FileName, System.Drawing.Imaging.ImageFormat.Gif);
                            else if (saveFileDialogImage.FilterIndex == 3)
                                pictureEditSystemImage.Image.Save(saveFileDialogImage.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            else if (saveFileDialogImage.FilterIndex == 4)
                                pictureEditSystemImage.Image.Save(saveFileDialogImage.FileName, System.Drawing.Imaging.ImageFormat.Icon);
                            else if (saveFileDialogImage.FilterIndex == 5)
                                pictureEditSystemImage.Image.Save(saveFileDialogImage.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            //*/

                            mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("이미지 저장: {0}", saveFileDialogImage.FileName));
                        }
                    }
                    catch (Exception ex)
                    {
                        mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), ex.StackTrace.ToString());
                    }
                }
            }
        }
        public Bitmap ConverterColorToGray(Bitmap colorBitmap)
        {
            int w = colorBitmap.Width,
                h = colorBitmap.Height,
                r, ic, oc, bmpStride, outputStride, bytesPerPixel;
            PixelFormat pfIn = colorBitmap.PixelFormat;
            BitmapData bmpData, outputData;

            Bitmap outImage = colorBitmap;
            //Create the new bitmap
            outImage = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            //Build a grayscale color Palette
            System.Drawing.Imaging.ColorPalette cvtpalette = outImage.Palette;
            for (int i = 0; i < 256; i++)
            {
                Color tmp = Color.FromArgb(255, i, i, i);
                cvtpalette.Entries[i] = Color.FromArgb(255, i, i, i);
            }
            outImage.Palette = cvtpalette;

            //Get the number of bytes per pixel
            switch (pfIn)
            {
                case System.Drawing.Imaging.PixelFormat.Format24bppRgb: bytesPerPixel = 3; break;
                case System.Drawing.Imaging.PixelFormat.Format32bppArgb: bytesPerPixel = 4; break;
                case System.Drawing.Imaging.PixelFormat.Format32bppRgb: bytesPerPixel = 4; break;
                case System.Drawing.Imaging.PixelFormat.Format8bppIndexed: outImage = colorBitmap; return outImage; break;
                default: throw new InvalidOperationException("Image format not supported");
            }

            //Lock the images
            bmpData = colorBitmap.LockBits(new Rectangle(0, 0, w, h), System.Drawing.Imaging.ImageLockMode.ReadOnly,
                                    pfIn);
            outputData = outImage.LockBits(new Rectangle(0, 0, w, h), System.Drawing.Imaging.ImageLockMode.WriteOnly,
                                            System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            bmpStride = bmpData.Stride;
            outputStride = outputData.Stride;

            //Traverse each pixel of the image
            unsafe
            {
                byte* bmpPtr = (byte*)bmpData.Scan0.ToPointer(),
                outputPtr = (byte*)outputData.Scan0.ToPointer();

                if (bytesPerPixel == 3)
                {
                    //Convert the pixel to it's luminance using the formula:
                    // L = .299*R + .587*G + .114*B
                    //Note that ic is the input column and oc is the output column
                    for (r = 0; r < h; r++)
                        for (ic = oc = 0; oc < w; ic += 3, ++oc)
                            outputPtr[r * outputStride + oc] = (byte)(int)
                                (0.299f * bmpPtr[r * bmpStride + ic] +
                                    0.587f * bmpPtr[r * bmpStride + ic + 1] +
                                    0.114f * bmpPtr[r * bmpStride + ic + 2]);
                }
                else //bytesPerPixel == 4
                {
                    //Convert the pixel to it's luminance using the formula:
                    // L = alpha * (.299*R + .587*G + .114*B)
                    // L = (.299*R + .587*G + .114*B) * alpha
                    //Note that ic is the input column and oc is the output column
                    for (r = 0; r < h; r++)
                        for (ic = oc = 0; oc < w; ic += 4, ++oc)
                            outputPtr[r * outputStride + oc] = (byte)(int)
                                ((bmpPtr[r * bmpStride + ic + 3] / 255.0f) *
                                (0.299f * bmpPtr[r * bmpStride + ic] +
                                    0.587f * bmpPtr[r * bmpStride + ic + 1] +
                                    0.114f * bmpPtr[r * bmpStride + ic + 2]));
                }
            }

            //Unlock the images
            colorBitmap.UnlockBits(bmpData);
            outImage.UnlockBits(outputData);
            return outImage;
        }

        private void barButtonItemImageOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (openFileDialogImageFileOpen.ShowDialog() == DialogResult.OK)
                {
                    _sourceImage = System.Drawing.Image.FromFile(openFileDialogImageFileOpen.FileName);
                    pictureEditSystemImage.Image = _sourceImage;
                    _patternMatching = false;
                    _isOpticalMeasurement = false;
                    pictureEditSystemImage.Refresh();
                    ImageFitSize();
                }
                mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("{0}파일 이미지 불러오기", openFileDialogImageFileOpen.FileName));
            }
            catch (Exception ex)
            {
                mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("{0}\r\n{1}", ex.Message, ex.StackTrace));
            }
        }

        private void barButtonItemHomming_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (!_bwMotionHome.IsBusy)
                {
                    _IsHommingFinished = false;
                    mRobotInformation.SetStatus(RobotInformation.RobotStatus.OperationReady, _IsHommingFinished);
                    _bwMotionHome.RunWorkerAsync(mRobotInformation);
                    AutoStartButtonLock();
                    mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), "모션 원점 복귀 명령을 실행 하였습니다.");
                }
                else
                {
                    mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), "모션 원점 복귀중으로 명령을 생략합니다.");
                }
            }
            catch (Exception ex)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), string.Format("모션 원점 복귀 명령을 실행 하지 못하였습니다."));
            }
        }

        private void barButtonItemReset_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (_mMotionControlCommManager.IsOpen())
                {
                    if (_systemParams._SystemLanguageKoreaUse)
                    {
                        if (MessageBox.Show("알람 리셋을 진행을 합니다.", "알람 리셋", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            byte[] SeData = new byte[8];
                            for (int i = 0; i < _mMotionControlCommManager.mDrvCtrl.DeviceIDCount; i++)
                            {
                                SeData = _mMotionControlCommManager.mDrvCtrl.AlarmResetCommand((byte)_mMotionControlCommManager.mDrvCtrl.DrvID[i]);
                                _mMotionControlCommManager.SendData(SeData);
                            }
                            mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), "알람 리셋");
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Start Alarm Reset.", "Alarm Reset", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            byte[] SeData = new byte[8];
                            for (int i = 0; i < _mMotionControlCommManager.mDrvCtrl.DeviceIDCount; i++)
                            {
                                SeData = _mMotionControlCommManager.mDrvCtrl.AlarmResetCommand((byte)_mMotionControlCommManager.mDrvCtrl.DrvID[i]);
                                _mMotionControlCommManager.SendData(SeData);
                            }
                            mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), "알람 리셋");
                        }
                    }


                }
            }
            catch
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), string.Format("드라이버 알람 리셋 버튼 싫생 오류"));
            }
        }

        private void barButtonItemMoveStop_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (_bwMotionHome.IsBusy)
                {
                    _IsHommingFinished = false;
                    _HommingProcess = false;
                    _IsHommingCancle = true;
                    _bwMotionHome.CancelAsync();
                    mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), "모션 원점복귀 명령을 취소하였습니다.");
                }

                AutoStartButtonRelease();

                if (_mMotionControlCommManager.IsOpen())
                {
                    if (MessageBox.Show("Stop the Motion Move.", "Stop Motion", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        byte[] SeData = new byte[8];
                        for (int i = 0; i < _mMotionControlCommManager.mDrvCtrl.DeviceIDCount; i++)
                        {
                            SeData = _mMotionControlCommManager.mDrvCtrl.MoveStopCommand((byte)_mMotionControlCommManager.mDrvCtrl.DrvID[i]);
                            _mMotionControlCommManager.SendData(SeData);
                        }
                        mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), "모션 정지 명령을 실행하였습니다.");
                    }
                }
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "모션 정지 명령을 하지 못햇습니다.");
            }
        }

        private void barCheckItemLaserSolderingStart_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (_mMotionControlCommManager.IsOpen() & _mRemoteIOCommManager.IsOpen() & _mLaserSoldering.IsSolderingConnect & _Camera.IsAllocated)
                {

                    if (_workParams._AlignInspectionMode == 1)      // 0: None, 1: 2Point, 2: All
                    {
                        if (_workParams._PCBAlignVisionEnable)
                        {
                            switch (_workParams.SolderPositionParams.Count)
                            {
                                case 0:
                                    MessageBox.Show("등록된 검사 위치가 없습니다. 레시피를 확인 하십시오.", "검사 시작 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "등록된 검사 위치가 없습니다. 레시피를 확인 하십시오.");
                                    break;
                                default:
                                    int alignmentconut = 0;
                                    if (_workParams._AlignInspectionMode == 1)
                                    {
                                        for (int i = 0; i < _workParams.SolderPositionParams.Count; i++)
                                        {
                                            if (_workParams.SolderPositionParams[i].ePositionType == INSPECTION_POSITION_MODE.POSITION_INSPECTION_ALIGN_MODE)
                                            {
                                                alignmentconut++;
                                            }
                                        }
                                        if (alignmentconut != 2)
                                        {
                                            MessageBox.Show("Alignemnt가 등록되지 않았습니다.", "검사 시작 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "Alignemnt가 등록되지 않았습니다.");
                                            return;
                                        }
                                    }
                                    break;
                            }
                        }
                    }                    
                }
                else
                {
                    MessageBox.Show("카메라, 로봇, 레이저 모듈의 연결을 확인하십시오.", "검사 시작 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "카메라, 로봇, 레이저 모듈의 연결을 확인하십시오.");
                    return;
                }
                if (!_IsAutoSolderingRunning)
                {
                    //if (_isContinuousShot)
                    //{
                    //    _Camera.Stop();
                    //    _isContinuousShot = false;
                    //}

                    barEditItemAutoSolderingProgress.EditValue = 0;
                    repositoryItemAutoSolderingProgress.Maximum = 100;
                    barStaticItemAutoSolderingStatus.Caption = "진행: 검사 준비";
                    barStaticAutoSolderingTime.Caption = "검사 시간: 000.000 sec";
                    _dTotalElapsedTime = 0.0f;

                    if (_workParams._PCBAlignVisionEnable)
                    {
                        if (_workParams._InspectAlignVisionPath != string.Empty)
                        {
                            _AlignToolBlock = (CogToolBlock)CogSerializer.LoadObjectFromFile(_workParams._InspectAlignVisionPath);
                        }
                        else
                        {
                            _backgroundWorkerAutoSoldering.CancelAsync();
                            barCheckItemLaserSolderingStart.Enabled = false;
                            mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "PCB Align Vision 레시피 경로가 없습니다. 자동 납땜을 실행을 중지 했습니다.");
                        }
                        
                    }

                    if (_workParams._SolderingInspectVisionEnable)
                    {
                        if (_workParams._InspectSolderingVisionPath != string.Empty)
                        {
                            _InspectToolBlock = (CogToolBlock)CogSerializer.LoadObjectFromFile(_workParams._InspectAlignVisionPath);
                        }
                        else
                        {
                            _backgroundWorkerAutoSoldering.CancelAsync();
                            barCheckItemLaserSolderingStart.Enabled = false;
                            mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "납땜 검사 Vision 레시피 경로가 없습니다. 자동 납땜을 실행을 중지 했습니다.");
                        }
                    }                    
                    
                    //_IsAutoSolderingRunning = true;
                    //_IsAutoSolderingEnd = false;
                    // 검사 쓰레드 시작
                    _backgroundWorkerAutoSoldering.RunWorkerAsync();
                }
                else
                {
                    _backgroundWorkerAutoSoldering.CancelAsync();
                    barCheckItemLaserSolderingStart.Enabled = false;
                    mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "자동 납땜을 실행을 중지 했습니다.");
                }
            }
            catch (Exception ex)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "자동 납땜을 실행에 오류가 있습니다.");
            }
        }
    }
}