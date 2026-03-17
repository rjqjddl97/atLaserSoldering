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

        public AiCControlLibrary.SerialCommunication.Control.CommunicationManager _mMotionControlCommManager = null;
        public ArioModbusLibrary.SerialCommunication.Control.CommunicationManager _mRemoteIOCommManager = null;

        public FeederControlLibrary.SerialCommunication.Control.CommunicationManager _mFeederCommManager = null;
        public CoherentCompactMini.SerialCommunication.Control.CommunicationManager _mLaserCommManager = null;
        public LaserSoderingProcess _mLaserSoldering = null;
        //DBControl _JobWorkDbCtrl = new DBControl();
        public ADMSEquipmentInfo _admsEquipment = new ADMSEquipmentInfo();
        public ADMSProductInfo _admsProduct = new ADMSProductInfo();
        ManualResetEvent _waitHandle = new ManualResetEvent(false);
        BackgroundWorker _backgroundWorkerAutoSoldering = new BackgroundWorker();
        BackgroundWorker _bwMotionHome = new BackgroundWorker();

        private LoginForm _mLogin = new LoginForm();
        private Log mLog = new Log();
        private List<LogData> mLogList = new List<LogData>();
        public RobotInformation mRobotInformation = new RobotInformation();        

        string _strTitle = "레이저 자동 솔더링 시스템";
        bool _InspectionWorking = false;
        bool _HommingProcess = false;
        public event Action<Image> ImageGrabbed;
        bool _isContinuousShot = false;
        bool _isCameraOpen = false;

        bool _isOpticalMeasurement = false;

        bool _isCropMove = false;
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
                //if (InitializeCamera())
                //{
                //    _systemParams.InspectionOpticalSpotCenterX = _systemParams._cameraParams.HResolution / 2;
                //    _systemParams.InspectionOpticalSpotCenterY = _systemParams._cameraParams.VResolution / 2;
                //    mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("카메라 초기화 완료"));
                //}
                //else
                //    mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), string.Format("카메라 초기화 실패"));

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
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "ARM 모듈 초기화를 하지 못햇습니다.");
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
                        mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("ARM 통신 연결 성공."));
                    }
                    else
                        mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("ARM 통신 연결 실패."));
                }
                else
                {
                    remoteIOControl.ConnectionClosed();
                    mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), string.Format("ARM 통신 연결 해제 성공."));
                }
                return _mRemoteIOCommManager.IsOpen();
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "ARM 통신 연결을 하지 못햇습니다.");
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
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "ARM 통신 해제를 하지 못햇습니다.");
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
    }
}