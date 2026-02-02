using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using AiCControlLibrary.SerialCommunication;
using AiCControlLibrary.SerialCommunication.Control;
using AiCControlLibrary.SerialCommunication.Data;
using AiCControlLibrary.SerialCommunication.DataHandler;
using RecipeManager.Params;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

//using LogLibrary;

namespace CustomPages
{
    public partial class MotionRemoteControl : DevExpress.XtraEditors.XtraUserControl
    {
        private CommunicationManager _mAiCCommunicationManager = null;
        private AiCData _mAiCData;

        public AiCData = new AiCData();         
        private bool _isRobotEnable = false;
        private bool _isHWEmergencyStop = false;
        private bool _isSWEmergencyStop = false;
        private bool _isError = false;
        private bool _isInposition = false;
        private bool _isMoveStop = false;
        private bool _isEtherCATReady = false;
        private bool _isServoOn = false;
        private bool _isMoving = false;
        private bool _isHomming = false;
        private bool _isAutomode = false;

        public double _fUserdefineValue = 1;
        public double[] _fdefineStepValue = new double[3];
        public double[] _fdefineVelValue = new double[3];
        public double _fMenualMoveVelocity = 0;

        public double[] _fPresentPosition = new double[6];
        
        public double _fAcceleration = 1000F;
        public bool _OperatingMode = false;
        public bool _MenaulCoordinateMode = false;
        public bool _isPLCConnected = false;
        public int _MenaulControlMode = 0;
        public int _iCalibrationMode = 0;

        public string _IPAddress = string.Format("192.168.0.181");
        public int _PortNum = 1000;
        public ulong OutputData = 0;

        public event Action<AiCData> MotionInfomationUpdatedEvent;

        RecipeManager.CalibrationParams CalibrationParam = new RecipeManager.CalibrationParams();

        public System.Timers.Timer UpdateTimer = new System.Timers.Timer();

        //private LogLibrary.Log mLog = null;
        public MotionRemoteControl()
        {
            InitializeComponent();
            PLCDisConnect.Enabled = false;
            radioGroupMenualMode.SelectedIndex = 0;
            radioGroupMenualControlMode.SelectedIndex = 0;
            radioGroupMenualValueMode.SelectedIndex = 0;
            radioGroupCalibration.SelectedIndex = 0;
            textEditIpAddress.Text = _IPAddress;
            textEditTcpPort.Text = _PortNum.ToString();
            checkButtonMenualMode.Text = "자동 모드";
            checkButtonLowValue.Text = "낮은 속도";
            checkButtonMiddleValue.Text = "중간 속도";
            checkButtonHighValue.Text = "높은 속도";
            checkEditCalibration.Text = "위치보정 활성화";
            textBoxUserDefineValue.Text = "사용자 정의 속도";
            textEditUserDefineValue.Enabled = false;
            textEditUserDefineValue.Text = Convert.ToString("1");
            textEditTargetPosX.Text = Convert.ToString("0");
            textEditTargetPosY1.Text = Convert.ToString("0");
            textEditTargetPosY2.Text = Convert.ToString("0");
            textEditTargetPosZ.Text = Convert.ToString("0");
            textEditTargetPosFZ.Text = Convert.ToString("0");
            textEditTargetPosFR.Text = Convert.ToString("0");
            textEditTargetVelocity.Text = Convert.ToString("1");
            textEditTargetAcceleration.Text = Convert.ToString("100");
            CoordinateControlPanelDisable();
            JogControlPannelDIsable();
            radioGroupMenualMode.Enabled = false;
            radioGroupMenualControlMode.Enabled = false;
            radioGroupMenualValueMode.Enabled = false;
            checkButtonLowValue.Enabled = false;
            checkButtonMiddleValue.Enabled = false;
            checkButtonHighValue.Enabled = false;
            checkEditCalibration.Enabled = false;
            //PLCDisConnect.Enabled = false;
            //ConnectPLCButton.Enabled = false;
            UpdateTimer.Interval = 500;
            UpdateTimer.Elapsed += new ElapsedEventHandler(UpdatePLCData);            

            for (int i = 0; i < 3; i++)
            {
                if (i == 0)
                {
                    if (_fdefineStepValue[i] == 0)
                        _fdefineStepValue[i] = 0.1;

                    if (_fdefineStepValue[i] < 1)
                        _fdefineVelValue[i] = _fdefineStepValue[i] * 10;        
                    else
                        _fdefineVelValue[i] = 10;                               // default 10 mm/s
                }
                else if (i == 1)
                {
                    if (_fdefineStepValue[i] == 0)
                        _fdefineStepValue[i] = 1;

                    if (_fdefineStepValue[i] < 1)
                        _fdefineVelValue[i] = 50;
                    else if (_fdefineStepValue[i] < 2)
                        _fdefineVelValue[i] = _fdefineStepValue[i] * 50;        // default 50 mm/s
                    else
                        _fdefineVelValue[i] = 100;
                }
                else
                {
                    if (_fdefineStepValue[i] == 0)
                        _fdefineStepValue[i] = 10;

                    if (_fdefineStepValue[i] < 10)
                        _fdefineVelValue[i] = 100;
                    if (_fdefineStepValue[i] < 20)
                        _fdefineVelValue[i] = _fdefineStepValue[i] * 10;
                    else
                        _fdefineVelValue[i] = 200;

                }
            }
            //if (mLog == null) mLog = new LogLibrary.Log();
        }
        public void SetCommunicateManager(ref CommunicationManager manager)
        {
            _mPLCCommunicationManager = manager;
            InitialPLCRemoteControl();
        }
        public void SetDefineStepValue(double[] data)
        {
            for (int i = 0; i < 3; i++)
            {
                _fdefineStepValue[i] = data[i];
            }
        }
        public void SetDefineVelValue(double[] data)
        {
            for (int i = 0; i < 3; i++)
            {
                _fdefineVelValue[i] = data[i];
            }
        }
        private void InitialPLCRemoteControl()
        {
            if (_mPLCCommunicationManager != null)
            {
                _mPLCData = _mPLCCommunicationManager.mCodesysDataHandler.GetUserCodesysPLCData();
                _mPLCData.mReceivedRobotInfomation.RobotInfomationUpdatedEvent += UpdatePLCInfomation;
                //_mPLCCommunicationManager. += ReceivedRawData;
            }
        }
        private void ConnectPLCButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mPLCCommunicationManager != null)
                {                    
                    if (!_mPLCCommunicationManager.IsConnected())
                    {
                        TcpIpSetData TcpSetData = new TcpIpSetData();
                        TcpSetData.IpAddress = textEditIpAddress.Text;
                        TcpSetData.Port = Convert.ToInt32(textEditTcpPort.Text);
                        TcpSetData.ReadTimeout = 3000;
                        TcpSetData.WriteTimeout = 3000;
                        _mPLCCommunicationManager.SetTcpIpData(TcpSetData);
                        if (!_mPLCCommunicationManager.Connect())
                        {
                            _isPLCConnected = false;
                            MessageBox.Show("서버 연결을 실패하였습니다..", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //mLog.WriteLog(LogLibrary.LogLevel.Info, LogLibrary.LogClass.atPhoto.ToString(), "PLC 통신 연결 실패.");
                        }
                        if (_mPLCCommunicationManager.IsConnected())
                        {
                            PLCDisConnect.Enabled = true;
                            ConnectPLCButton.Enabled = false;
                            _isPLCConnected = true;
                            InitialPLCRemoteControl();
                            //mLog.WriteLog(LogLibrary.LogLevel.Info, LogLibrary.LogClass.atPhoto.ToString(), "PLC 통신 연결 성공.");
                        }
                    }
                    else
                    {
                        PLCDisConnect.Enabled = true;
                        ConnectPLCButton.Enabled = false;
                    }
                }
            }
            catch(Exception)
            {
                ;
            }
        }

        private void PLCDisConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mPLCCommunicationManager != null)
                {
                    if (_mPLCCommunicationManager.IsConnected())
                    {
                        _mPLCCommunicationManager.Disconnect();
                        ConnectPLCButton.Enabled = true;
                        PLCDisConnect.Enabled = false;
                        _isPLCConnected = false;
                        _mPLCData.mReceivedRobotInfomation.RobotInfomationUpdatedEvent -= UpdatePLCInfomation;
                        //mLog.WriteLog(LogLibrary.LogLevel.Info, LogLibrary.LogClass.atPhoto.ToString(), "PLC 통신 끊기 성공.");
                        
                    }
                    else
                    {
                        ConnectPLCButton.Enabled = true;
                        PLCDisConnect.Enabled = false;
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        public void ReceivedRawData(byte[] data)
        {

        }
        public void UpdatePLCInfomation(UserCodesysData.RobotInfomation info)
        {
            //if (InvokeRequired)
            //{
            //    BeginInvoke(new Action<UserCodesysData.RobotInfomation>(UpdatePLCInfomation), info);
            //    return;
            //}
            BackupInfo = info;            
            PLCInfomationUpdatedEvent?.Invoke(info);
            //SetPLCStatus(info);
        }
        public void SetPLCStatus(UserCodesysData.RobotInfomation info)
        {
            try
            {
                if (info != null)
                {
                    if (_mPLCCommunicationManager.IsConnected())
                    {
                        if (textEditPresentPosX.InvokeRequired)
                        {
                            textEditPresentPosX.Invoke(new MethodInvoker(delegate { textEditPresentPosX.EditValue = Convert.ToString(info.mPosition.X); }));
                        }
                        else
                            textEditPresentPosX.EditValue = Convert.ToString(info.mPosition.X);

                        if (textEditPresentPosY1.InvokeRequired)
                        {
                            textEditPresentPosY1.Invoke(new MethodInvoker(delegate { textEditPresentPosY1.EditValue = Convert.ToString(info.mPosition.Y); }));
                        }
                        else
                            textEditPresentPosY1.EditValue = Convert.ToString(info.mPosition.Y);

                        if (textEditPresentPosY2.InvokeRequired)
                        {
                            textEditPresentPosY2.Invoke(new MethodInvoker(delegate { textEditPresentPosY2.EditValue = Convert.ToString(info.mPosition.Z); }));
                        }
                        else
                            textEditPresentPosY2.EditValue = Convert.ToString(info.mPosition.Z);

                        if (textEditPresentPosZ.InvokeRequired)
                        {
                            textEditPresentPosZ.Invoke(new MethodInvoker(delegate { textEditPresentPosZ.EditValue = Convert.ToString(info.mPosition.Roll); }));
                        }
                        else
                            textEditPresentPosZ.EditValue = Convert.ToString(info.mPosition.Roll);

                        if (textEditPresentPosFZ.InvokeRequired)
                        {
                            textEditPresentPosFZ.Invoke(new MethodInvoker(delegate { textEditPresentPosFZ.EditValue = Convert.ToString(info.mPosition.Pitch); }));
                        }
                        else
                            textEditPresentPosFZ.EditValue = Convert.ToString(info.mPosition.Pitch);

                        if (textEditPresentPosFR.InvokeRequired)
                        {
                            textEditPresentPosFR.Invoke(new MethodInvoker(delegate { textEditPresentPosFR.EditValue = Convert.ToString(info.mPosition.Yaw); }));
                        }
                        else
                            textEditPresentPosFR.EditValue = Convert.ToString(info.mPosition.Yaw);

                        //textEditPresentPosX.EditValue = Convert.ToString(info.mPosition.X);
                        //textEditPresentPosY1.EditValue = Convert.ToString(info.mPosition.Y);
                        //textEditPresentPosY2.EditValue = Convert.ToString(info.mPosition.Z);
                        //textEditPresentPosZ.EditValue = Convert.ToString(info.mPosition.Roll);
                        //textEditPresentPosFZ.EditValue = Convert.ToString(info.mPosition.Pitch);
                        //textEditPresentPosFR.EditValue = Convert.ToString(info.mPosition.Yaw);                       

                        _isRobotEnable = info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.OperationReady);
                        _isHWEmergencyStop = info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.HW_EmergencyStop);
                        _isSWEmergencyStop = info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.SW_EmergencyStop);
                        _isError = info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.Error);
                        _isInposition = info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.Inposition);
                        _isMoveStop = info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.MoveStop);
                        _isServoOn = info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.ServoOn);
                        _isEtherCATReady = info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.SystemReady);
                        _isMoving = info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.Moving);
                        _isHomming = info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.Homming);
                        _isAutomode = info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.OpMode);

                        OutputData = info.mOutputData.Bit64;

                        ShowStatus(labelControlPLCStatus1, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.SystemReady));
                        ShowStatus(labelControlPLCStatus2, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.ServoOn));
                        ShowStatus(labelControlPLCStatus3, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.Homming));
                        ShowStatus(labelControlPLCStatus4, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.OperationReady));
                        ShowStatus(labelControlPLCStatus5, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.MoveStop));
                        ShowStatus(labelControlPLCStatus6, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.Moving));
                        ShowStatus(labelControlPLCStatus7, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.Inposition));
                        ShowStatus(labelControlPLCStatus8, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.st7));
                        ShowStatus(labelControlPLCStatus9, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.OpMode));
                        ShowStatus(labelControlPLCStatus10, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.MenualMODE1));
                        ShowStatus(labelControlPLCStatus11, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.MenualMODE2));
                        ShowStatus(labelControlPLCStatus12, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.st11));
                        ShowStatus(labelControlPLCStatus13, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.st12));
                        ShowStatus(labelControlPLCStatus14, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.HW_EmergencyStop));
                        ShowStatus(labelControlPLCStatus15, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.SW_EmergencyStop));
                        ShowStatus(labelControlPLCStatus16, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.Error));
                        ShowStatus(labelControlPLCStatus17, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.st16));
                        ShowStatus(labelControlPLCStatus18, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.st17));
                        ShowStatus(labelControlPLCStatus19, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.st18));
                        ShowStatus(labelControlPLCStatus20, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.st19));
                        ShowStatus(labelControlPLCStatus21, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.st20));
                        ShowStatus(labelControlPLCStatus22, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.st21));
                        ShowStatus(labelControlPLCStatus23, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.st22));
                        ShowStatus(labelControlPLCStatus24, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.st23));
                        ShowStatus(labelControlPLCStatus25, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.st24));
                        ShowStatus(labelControlPLCStatus26, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.st25));
                        ShowStatus(labelControlPLCStatus27, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.st26));
                        ShowStatus(labelControlPLCStatus28, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.st27));
                        ShowStatus(labelControlPLCStatus29, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.st28));
                        ShowStatus(labelControlPLCStatus30, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.st29));
                        ShowStatus(labelControlPLCStatus31, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.st30));
                        ShowStatus(labelControlPLCStatus32, info.GetStatus(UserCodesysData.RobotInfomation.PLCStatus.st31));

                        ShowStatus(labelControlDIn1, info.GetInputStatus(0));
                        ShowStatus(labelControlDIn2, info.GetInputStatus(1));
                        ShowStatus(labelControlDIn3, info.GetInputStatus(2));
                        ShowStatus(labelControlDIn4, info.GetInputStatus(3));
                        ShowStatus(labelControlDIn5, info.GetInputStatus(4));
                        ShowStatus(labelControlDIn6, info.GetInputStatus(5));
                        ShowStatus(labelControlDIn7, info.GetInputStatus(6));
                        ShowStatus(labelControlDIn8, info.GetInputStatus(7));
                        ShowStatus(labelControlDIn9, info.GetInputStatus(8));
                        ShowStatus(labelControlDIn10, info.GetInputStatus(9));
                        ShowStatus(labelControlDIn11, info.GetInputStatus(10));
                        ShowStatus(labelControlDIn12, info.GetInputStatus(11));
                        ShowStatus(labelControlDIn13, info.GetInputStatus(12));
                        ShowStatus(labelControlDIn14, info.GetInputStatus(13));
                        ShowStatus(labelControlDIn15, info.GetInputStatus(14));
                        ShowStatus(labelControlDIn16, info.GetInputStatus(15));

                        ShowStatus(labelControlDOut1, info.GetOutputStatus(0));
                        ShowStatus(labelControlDOut2, info.GetOutputStatus(1));
                        ShowStatus(labelControlDOut3, info.GetOutputStatus(2));
                        ShowStatus(labelControlDOut4, info.GetOutputStatus(3));
                        ShowStatus(labelControlDOut5, info.GetOutputStatus(4));
                        ShowStatus(labelControlDOut6, info.GetOutputStatus(5));
                        ShowStatus(labelControlDOut7, info.GetOutputStatus(6));
                        ShowStatus(labelControlDOut8, info.GetOutputStatus(7));
                        ShowStatus(labelControlDOut9, info.GetOutputStatus(8));
                        ShowStatus(labelControlDOut10, info.GetOutputStatus(9));
                        ShowStatus(labelControlDOut11, info.GetOutputStatus(10));
                        ShowStatus(labelControlDOut12, info.GetOutputStatus(11));
                        ShowStatus(labelControlDOut13, info.GetOutputStatus(12));
                        ShowStatus(labelControlDOut14, info.GetOutputStatus(13));
                        ShowStatus(labelControlDOut15, info.GetOutputStatus(14));
                        ShowStatus(labelControlDOut16, info.GetOutputStatus(15));
                        
                        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PLCRemoteControl));
                        if (!_mPLCCommunicationManager.IsConnected())
                        {
                            ConnectPLCButton.Enabled = true;
                            PLCDisConnect.Enabled = false;
                        }
                        else
                        {
                            ConnectPLCButton.Enabled = false;
                            PLCDisConnect.Enabled = true;
                        }

                        if (_isRobotEnable)
                        {

                            this.RobotEnableButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("RobotDisableButton.ImageOptions.Image")));
                            RobotEnableButton.Invoke(new MethodInvoker(delegate { RobotEnableButton.Text = "모션제 비활성화"; }));
                            //stateIndicatorComponentMotionEnable.StateIndex = 3;
                        }
                        else
                        {
                            this.RobotEnableButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("RobotEnableButton.ImageOptions.Image")));
                            RobotEnableButton.Invoke(new MethodInvoker(delegate { RobotEnableButton.Text = "모션제어 활성화"; }));
                            //stateIndicatorComponentMotionEnable.StateIndex = 1;
                        }
                        if (_isAutomode)
                        {
                            checkButtonMenualMode.Invoke(new MethodInvoker(delegate { checkButtonMenualMode.Text = "수동 모드 전환"; }));
                            radioGroupMenualMode.Enabled = false;
                            radioGroupMenualControlMode.Enabled = false;
                            radioGroupMenualValueMode.Enabled = false;
                            checkButtonLowValue.Enabled = false;
                            checkButtonMiddleValue.Enabled = false;
                            checkButtonHighValue.Enabled = false;
                            checkEditCalibration.Enabled = true;
                            if (checkEditCalibration.Checked)
                            {
                                radioGroupCalibration.Enabled = true;
                            }
                            else
                            {
                                radioGroupCalibration.Enabled = false;
                            }

                            JogControlPannelDIsable();
                            CoordinateControlPanelEnable();
                        }
                        else
                        {
                            checkButtonMenualMode.Invoke(new MethodInvoker(delegate { checkButtonMenualMode.Text = "자동 모드 전환"; }));
                            radioGroupMenualMode.Enabled = true;
                            radioGroupMenualControlMode.Enabled = true;
                            radioGroupMenualValueMode.Enabled = true;
                            checkButtonLowValue.Enabled = true;
                            checkButtonMiddleValue.Enabled = true;
                            checkButtonHighValue.Enabled = true;
                            checkEditCalibration.Enabled = false;
                            radioGroupCalibration.Enabled = false;
                            if (radioGroupMenualControlMode.SelectedIndex != 2)
                            {
                                if (radioGroupMenualMode.SelectedIndex == 0)
                                    CoordinateControlPanelDisable();
                                JogControlPannelEnable();
                            }
                            else
                            {
                                if (radioGroupMenualMode.SelectedIndex == 1)
                                {
                                    CoordinateControlPanelEnable();
                                    JogControlPannelDIsable();
                                }
                                else
                                {
                                    if (radioGroupMenualMode.SelectedIndex == 0)
                                    {
                                        CoordinateControlPanelDisable();
                                    }
                                }
                            }
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                ;
            }
            
        }
        public void ShowStatus(DevExpress.XtraEditors.LabelControl label, bool status)
        {
            try
            {
                if (status)
                {
                    if (label.InvokeRequired)
                    {
                        label.Invoke(new MethodInvoker(delegate { label.ForeColor = Color.FromArgb(255, 255, 255, 255); label.BackColor = Color.FromArgb(255, 20, 200, 129); }));
                    }
                    else
                    {
                        label.ForeColor = Color.FromArgb(255, 255, 255, 255);
                        label.BackColor = Color.FromArgb(255, 20, 200, 129);
                    }
                }
                else
                {
                    if (label.InvokeRequired)
                    {
                        label.Invoke(new MethodInvoker(delegate { label.ForeColor = Color.FromArgb(255, 37, 37, 37); label.BackColor = Color.FromArgb(255, 224, 224, 224); }));
                    }
                    else
                    {
                        label.ForeColor = Color.FromArgb(255, 37, 37, 37);
                        label.BackColor = Color.FromArgb(255, 224, 224, 224);
                    }
                }
            }
            catch (Exception ex)
            {
                ;
            }

        }
        private void UpdatePLCData(object sender, ElapsedEventArgs e)
        {
            if (_mPLCCommunicationManager.IsConnected())
            {
                SetPLCStatus(BackupInfo);                

            }
        }
        private void RobotEnableButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mPLCCommunicationManager != null)
                {
                    byte[] data = new byte[1];                    
                    if (!_isRobotEnable)
                    {
                        data[0] = 1;
                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_ROBOT_ENABLE, data);
                        _isRobotEnable = true;
                    }
                    else
                    {
                        data[0] = 0;
                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_ROBOT_DISABLE, data);
                        _isRobotEnable = false;
                    }
                }
            }
            catch (Exception)
            {
            }

  
        }

        private void EmergencyStopButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mPLCCommunicationManager != null)
                {
                    if (_mPLCCommunicationManager.IsConnected())
                    {
                        // Emergency Stop and Release Send Command.
                        byte[] data = new byte[1];
                        if (_isSWEmergencyStop == false)
                        {                            
                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_EMG_STOP, data);
                            EmergencyStopButton.Invoke(new MethodInvoker(delegate { EmergencyStopButton.Text = "응급정지해제"; }));
                        }
                        else
                        {
                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_EMG_RELEASE, data);
                            EmergencyStopButton.Invoke(new MethodInvoker(delegate { EmergencyStopButton.Text = "응급정지"; }));
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void ErrorResetButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mPLCCommunicationManager != null)
                {
                    if (_mPLCCommunicationManager.IsConnected())
                    {
                        byte[] data = new byte[1];
                        if(_isError)
                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_ERROR_RESET, data);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void checkButtonMenualMode_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (_mPLCCommunicationManager != null)
                {
                    if ((_mPLCCommunicationManager.IsConnected()) && (_isRobotEnable))
                    {
                        byte[] data = new byte[4];
                        UserCodesysData.JogModeSwitch mJogData = new UserCodesysData.JogModeSwitch();
                        // Automation or Menual Mode Transition Command Send.       
                        if (_isAutomode)
                        {
                            data[0] = 1;
                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_OP_SELECT, data);
                            radioGroupMenualMode.Enabled = true;
                            radioGroupMenualControlMode.Enabled = true;
                            radioGroupMenualValueMode.Enabled = true;
                            checkButtonLowValue.Enabled = true;
                            checkButtonMiddleValue.Enabled = true;
                            checkButtonHighValue.Enabled = true;
                            //JogControlPannelDIsable();
                            //CoordinateControlPanelDisable();
                        }
                        else
                        {
                            data[0] = 0;
                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_OP_SELECT, data);                            
                            //JogControlPannelDIsable();
                            //CoordinateControlPanelDisable();
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void checkButtonLowValue_CheckedChanged(object sender, EventArgs e)
        {
            checkButtonLowValue.Checked = false;
        }

        private void checkButtonMiddleValue_CheckedChanged(object sender, EventArgs e)
        {
            checkButtonMiddleValue.Checked = false;
        }

        private void checkButtonHighValue_CheckedChanged(object sender, EventArgs e)
        {
            checkButtonHighValue.Checked = false;
        }

        private void textEditUserDefineValue_EditValueChanged(object sender, EventArgs e)
        {
           if ((_mPLCCommunicationManager != null) && (_mPLCCommunicationManager.IsConnected()) && (_isRobotEnable) && (!_isAutomode))
            {
                _fUserdefineValue = Convert.ToDouble(textEditUserDefineValue.Text);
                _fMenualMoveVelocity = _fUserdefineValue;
                byte[] data = new byte[8];
                UserCodesysData.JogModeSwitch mJogData = new UserCodesysData.JogModeSwitch();
                
                if (radioGroupMenualMode.SelectedIndex == 0)
                {
                    if (radioGroupMenualControlMode.SelectedIndex == 0)
                    {
                        mJogData.MenualMode = 0;
                        mJogData.MenualValue = Convert.ToSingle(_fMenualMoveVelocity);
                        data = mJogData.GetData();
                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_OP_MODE, data);
                    }
                    else if (radioGroupMenualControlMode.SelectedIndex == 1)
                    {
                        mJogData.MenualMode = 1;
                        mJogData.MenualValue = Convert.ToSingle(_fMenualMoveVelocity);
                        data = mJogData.GetData();
                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_MOTION_MODE, data);
                    }
                }
                else
                {
                    if (radioGroupMenualControlMode.SelectedIndex == 1)
                    {
                        mJogData.MenualMode = 1;
                        mJogData.MenualValue = Convert.ToSingle(_fMenualMoveVelocity);
                        data = mJogData.GetData();
                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_MOTION_MODE, data);
                    }
                    else if (radioGroupMenualControlMode.SelectedIndex == 2)
                    {
                        mJogData.MenualMode = 2;
                        mJogData.MenualValue = Convert.ToSingle(_fMenualMoveVelocity);
                        data = mJogData.GetData();
                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_COORDINATE_MODE, data);
                    }
                }
            }
        }

        private void SendCmdHommingButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mPLCCommunicationManager != null)
                {
                    if ((_mPLCCommunicationManager.IsConnected()) && (!_isError) && (_isServoOn))
                    {                        
                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_HOME, null);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void radioGroupMenualMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_mPLCCommunicationManager != null)
                {
                    if ((_mPLCCommunicationManager.IsConnected()) && (_isRobotEnable) && (!_isAutomode))
                    {                        
                        if (radioGroupMenualMode.SelectedIndex == 0)
                        {
                            _MenaulCoordinateMode = false;
                            radioGroupMenualControlMode.Properties.Items[0].Enabled = true;
                            radioGroupMenualControlMode.Properties.Items[2].Enabled = false;
                            CoordinateControlPanelDisable();
                            JogControlPannelEnable();
                            if (radioGroupMenualControlMode.SelectedIndex == 2)
                            {
                                radioGroupMenualControlMode.SelectedIndex = 0;
                            }
                            byte[] data = new byte[8];
                            UserCodesysData.JogModeSwitch mJogData = new UserCodesysData.JogModeSwitch();
                            mJogData.MenualMode = 0;
                            mJogData.MenualValue = Convert.ToSingle(_fMenualMoveVelocity);
                            data = mJogData.GetData();
                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_OP_MODE, data);
                        }
                        else
                        {
                            _MenaulCoordinateMode = true;
                            radioGroupMenualControlMode.Properties.Items[0].Enabled = false;
                            radioGroupMenualControlMode.Properties.Items[2].Enabled = true;
                            if (radioGroupMenualControlMode.SelectedIndex == 1)
                            {
                                JogControlPannelEnable();
                                CoordinateControlPanelDisable();
                            }
                            else if (radioGroupMenualControlMode.SelectedIndex == 2)
                            {
                                JogControlPannelDIsable();
                                CoordinateControlPanelEnable();
                            }
                            else
                            {
                                radioGroupMenualControlMode.SelectedIndex = 1;
                                JogControlPannelEnable();
                                CoordinateControlPanelDisable();
                            }
                            byte[] data = new byte[8];
                            UserCodesysData.JogModeSwitch mJogData = new UserCodesysData.JogModeSwitch();
                            mJogData.MenualMode = 1;
                            mJogData.MenualValue = Convert.ToSingle(_fMenualMoveVelocity);
                            data = mJogData.GetData();
                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_OP_MODE, data);
                        }
                        if (radioGroupMenualValueMode.SelectedIndex == 0)
                        {
                            defineValueButtonEnable();
                            textEditUserDefineValue.Enabled = false;
                        }
                        else
                        {
                            defineValueButtonDisable();
                            textEditUserDefineValue.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
 
        }
        private void radioGroupMenualValueMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_mPLCCommunicationManager != null)
            {
                //if ((_mPLCCommunicationManager.IsConnected()) && (_isRobotEnable) && (!_isAutomode))
                {
                    if (radioGroupMenualValueMode.SelectedIndex == 0)
                    {
                        defineValueButtonEnable();
                        textEditUserDefineValue.Enabled = false;
                    }
                    else
                    {
                        if (radioGroupMenualControlMode.SelectedIndex != 2)
                        {
                            defineValueButtonDisable();
                            textEditUserDefineValue.Enabled = true;
                        }
                    }
                }
            }
        }
        public void defineValueButtonEnable()
        {
            checkButtonLowValue.Enabled = true;
            checkButtonMiddleValue.Enabled = true;
            checkButtonHighValue.Enabled = true;
            //textEditUserDefineValue.Enabled = false;
        }
        public void defineValueButtonDisable()
        {
            checkButtonLowValue.Enabled = false;
            checkButtonMiddleValue.Enabled = false;
            checkButtonHighValue.Enabled = false;
            //textEditUserDefineValue.Enabled = true;
        }
        private void radioGroupMenualControlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            _MenaulControlMode = radioGroupMenualControlMode.SelectedIndex;
            if ((_mPLCCommunicationManager.IsConnected()) && (_isRobotEnable) && (!_isAutomode))
            {
                if (radioGroupMenualControlMode.SelectedIndex == 0)
                {
                    checkButtonLowValue.Text = "낮은 속도";
                    checkButtonMiddleValue.Text = "중간 속도";
                    checkButtonHighValue.Text = "높은 속도";
                    textBoxUserDefineValue.Text = "사용자 정의 속도";

                    if (radioGroupMenualValueMode.SelectedIndex == 0)
                    {
                        defineValueButtonEnable();
                        textBoxUserDefineValue.Enabled = false;
                    }
                    else
                    {
                        defineValueButtonDisable();
                        textBoxUserDefineValue.Enabled = true;
                    }
                    JogControlPannelEnable();
                    CoordinateControlPanelDisable();
                    byte[] data = new byte[8];
                    UserCodesysData.JogModeSwitch mJogData = new UserCodesysData.JogModeSwitch();
                    mJogData.MenualMode = 0;
                    mJogData.MenualValue = Convert.ToSingle(_fMenualMoveVelocity);
                    data = mJogData.GetData();
                    _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_MOTION_MODE, data);
                }
                else if (radioGroupMenualControlMode.SelectedIndex == 1)
                {
                    checkButtonLowValue.Text = "짧은 위치";
                    checkButtonMiddleValue.Text = "중간 위치";
                    checkButtonHighValue.Text = "긴 위치";
                    textBoxUserDefineValue.Text = "사용자 정의 위치";

                    if (radioGroupMenualValueMode.SelectedIndex == 0)
                    {
                        defineValueButtonEnable();
                        textBoxUserDefineValue.Enabled = false;
                    }
                    else
                    {
                        defineValueButtonDisable();
                        textBoxUserDefineValue.Enabled = true;
                    }

                    JogControlPannelEnable();
                    CoordinateControlPanelDisable();
                    byte[] data = new byte[8];
                    UserCodesysData.JogModeSwitch mJogData = new UserCodesysData.JogModeSwitch();
                    mJogData.MenualMode = 1;
                    mJogData.MenualValue = Convert.ToSingle(_fMenualMoveVelocity);
                    data = mJogData.GetData();
                    _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_MOTION_MODE, data);
                }
                else
                {
                    if (radioGroupMenualMode.SelectedIndex != 0)
                    {
                        defineValueButtonDisable();
                        textBoxUserDefineValue.Enabled = false;
                        JogControlPannelDIsable();
                        CoordinateControlPanelEnable();
                        byte[] data = new byte[8];
                        UserCodesysData.JogModeSwitch mJogData = new UserCodesysData.JogModeSwitch();
                        mJogData.MenualMode = 2;
                        mJogData.MenualValue = Convert.ToSingle(_fMenualMoveVelocity);
                        data = mJogData.GetData();
                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_COORDINATE_MODE, data);
                    }
                    else
                    {
                        radioGroupMenualControlMode.SelectedIndex = 0;
                    }
                }
            }

        }
        public void JogControlPannelEnable()
        {
            if (radioGroupMenualValueMode.SelectedIndex == 1)
            {
                textEditUserDefineValue.Enabled = true;                
            }
            radioGroupMenualValueMode.Enabled = true;
            labelControl1.Enabled = true;
            CheckButtonXMinusControlCommand.Enabled = true;
            CheckButtonXStopControlCommand.Enabled = true;
            CheckButtonXPlusControlCommand.Enabled = true;

            labelControl2.Enabled = true;
            CheckButtonY1MinusControlCommand.Enabled = true;
            CheckButtonY1StopControlCommand.Enabled = true;
            CheckButtonY1PlusControlCommand.Enabled = true;

            labelControl3.Enabled = true;
            CheckButtonY2MinusControlCommand.Enabled = true;
            CheckButtonY2StopControlCommand.Enabled = true;
            CheckButtonY2PlusControlCommand.Enabled = true;

            labelControl4.Enabled = true;
            CheckButtonZMinusControlCommand.Enabled = true;
            CheckButtonZStopControlCommand.Enabled = true;
            CheckButtonZPlusControlCommand.Enabled = true;

            labelControl5.Enabled = true;
            CheckButtonFZMinusControlCommand.Enabled = true;
            CheckButtonFZStopControlCommand.Enabled = true;
            CheckButtonFZPlusControlCommand.Enabled = true;

            labelControl6.Enabled = true;
            CheckButtonFRMinusControlCommand.Enabled = true;
            CheckButtonFRStopControlCommand.Enabled = true;
            CheckButtonFRPlusControlCommand.Enabled = true;
        }
        public void JogControlPannelDIsable()
        {
            if ((radioGroupMenualValueMode.SelectedIndex == 0)||(radioGroupMenualControlMode.SelectedIndex == 2))
            {
                textEditUserDefineValue.Enabled = false;
            }
            radioGroupMenualValueMode.Enabled = false;

            labelControl1.Enabled = false;
            CheckButtonXMinusControlCommand.Enabled = false;
            CheckButtonXStopControlCommand.Enabled = false;
            CheckButtonXPlusControlCommand.Enabled = false;

            labelControl2.Enabled = false;
            CheckButtonY1MinusControlCommand.Enabled = false;
            CheckButtonY1StopControlCommand.Enabled = false;
            CheckButtonY1PlusControlCommand.Enabled = false;

            labelControl3.Enabled = false;
            CheckButtonY2MinusControlCommand.Enabled = false;
            CheckButtonY2StopControlCommand.Enabled = false;
            CheckButtonY2PlusControlCommand.Enabled = false;

            labelControl4.Enabled = false;
            CheckButtonZMinusControlCommand.Enabled = false;
            CheckButtonZStopControlCommand.Enabled = false;
            CheckButtonZPlusControlCommand.Enabled = false;

            labelControl5.Enabled = false;
            CheckButtonFZMinusControlCommand.Enabled = false;
            CheckButtonFZStopControlCommand.Enabled = false;
            CheckButtonFZPlusControlCommand.Enabled = false;

            labelControl6.Enabled = false;
            CheckButtonFRMinusControlCommand.Enabled = false;
            CheckButtonFRStopControlCommand.Enabled = false;
            CheckButtonFRPlusControlCommand.Enabled = false;
        }
        public void CoordinateControlPanelEnable()
        {
            textEditTargetPosX.Enabled = true;
            textEditTargetPosY1.Enabled = true;
            textEditTargetPosY2.Enabled = true;
            textEditTargetPosZ.Enabled = true;
            textEditTargetPosFZ.Enabled = true;
            textEditTargetPosFR.Enabled = true;
            textEditTargetVelocity.Enabled = true;
            textEditTargetAcceleration.Enabled = true;
            SendCmdPositionButton.Enabled = true;
            SendCommandMoveStopButton.Enabled = true;
        }
        public void CoordinateControlPanelDisable()
        {
            textEditTargetPosX.Enabled = false;
            textEditTargetPosY1.Enabled = false;
            textEditTargetPosY2.Enabled = false;
            textEditTargetPosZ.Enabled = false;
            textEditTargetPosFZ.Enabled = false;
            textEditTargetPosFR.Enabled = false;
            SendCmdPositionButton.Enabled = false;
            textEditTargetVelocity.Enabled = false;
            textEditTargetAcceleration.Enabled = false;
            SendCommandMoveStopButton.Enabled = false;
        }
        private void checkButtonJogMove_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mPLCCommunicationManager != null)
                {
                    //if ((_mPLCCommunicationManager.IsConnected()) && (_isRobotEnable))
                    {
                        if (!(sender is DevExpress.XtraEditors.CheckButton button)) return;

                        double x, y1, y2, z, fz, fr, speed, acc = 0;
                        speed = _fMenualMoveVelocity;
                        acc = Convert.ToDouble(textEditTargetAcceleration.Text);
                        if (speed <= 0) speed = 10;
                        if (acc <= 0) acc = 1;                        
                        if (_MenaulCoordinateMode)
                        {
                            if (_MenaulControlMode == 1)
                            {
                                if ((button.Name == "CheckButtonXPlusControlCommand") || (button.Name == "CheckButtonXMinusControlCommand"))
                                {
                                    byte[] data = new byte[8];
                                    UserCodesysData.JogControl mJogCtrl = new UserCodesysData.JogControl();
                                    if (button.Name == "CheckButtonXPlusControlCommand")
                                    {
                                        CheckButtonXPlusControlCommand.Checked = false;
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_X_POSITIVE, data);
                                    }
                                    else
                                    {
                                        CheckButtonXMinusControlCommand.Checked = false;
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_X_NEGATIVE, data);
                                    }
                                }
                                else if ((button.Name == "CheckButtonY1PlusControlCommand") || (button.Name == "CheckButtonY1MinusControlCommand"))
                                {
                                    byte[] data = new byte[8];
                                    UserCodesysData.JogControl mJogCtrl = new UserCodesysData.JogControl();
                                    if (button.Name == "CheckButtonY1PlusControlCommand")
                                    {
                                        CheckButtonY1PlusControlCommand.Checked = false;
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_Y_POSITIVE, data);
                                    }
                                    else
                                    {
                                        CheckButtonY1MinusControlCommand.Checked = false;
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_Y_NEGATIVE, data);
                                    }
                                }
                                else if ((button.Name == "CheckButtonY2PlusControlCommand") || (button.Name == "CheckButtonY2MinusControlCommand"))
                                {
                                    byte[] data = new byte[8];
                                    UserCodesysData.JogControl mJogCtrl = new UserCodesysData.JogControl();
                                    if (button.Name == "CheckButtonY2PlusControlCommand")
                                    {
                                        CheckButtonY2PlusControlCommand.Checked = false;
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_Z_POSITIVE, data);
                                    }
                                    else
                                    {
                                        CheckButtonY2MinusControlCommand.Checked = false;
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_Z_NEGATIVE, data);
                                    }
                                }
                                else if ((button.Name == "CheckButtonZPlusControlCommand") || (button.Name == "CheckButtonZMinusControlCommand"))
                                {
                                    byte[] data = new byte[8];
                                    UserCodesysData.JogControl mJogCtrl = new UserCodesysData.JogControl();
                                    if (button.Name == "CheckButtonZPlusControlCommand")
                                    {
                                        CheckButtonZPlusControlCommand.Checked = false;
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_RX_POSITIVE, data);
                                    }
                                    else
                                    {
                                        CheckButtonZMinusControlCommand.Checked = false;
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_RX_NEGATIVE, data);
                                    }
                                }
                                else if ((button.Name == "CheckButtonFZPlusControlCommand") || (button.Name == "CheckButtonFZMinusControlCommand"))
                                {
                                    byte[] data = new byte[8];
                                    UserCodesysData.JogControl mJogCtrl = new UserCodesysData.JogControl();
                                    if (button.Name == "CheckButtonFZPlusControlCommand")
                                    {
                                        CheckButtonFZPlusControlCommand.Checked = false;
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_RY_POSITIVE, data);
                                    }
                                    else
                                    {
                                        CheckButtonFZMinusControlCommand.Checked = false;
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_RY_NEGATIVE, data);
                                    }
                                }
                                else if ((button.Name == "CheckButtonFRPlusControlCommand") || (button.Name == "CheckButtonFRMinusControlCommand"))
                                {
                                    byte[] data = new byte[8];
                                    UserCodesysData.JogControl mJogCtrl = new UserCodesysData.JogControl();
                                    if (button.Name == "CheckButtonFRPlusControlCommand")
                                    {
                                        CheckButtonFRPlusControlCommand.Checked = false;
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_RZ_POSITIVE, data);
                                    }
                                    else
                                    {
                                        CheckButtonFRMinusControlCommand.Checked = false;
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_RZ_NEGATIVE, data);
                                    }
                                }
                                else
                                {
                                    _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_MOVE_STOP, null);
                                }
                            }
                        }
                        else
                        {
                            if (_MenaulControlMode == 0)
                            {
                                if ((button.Name == "CheckButtonXPlusControlCommand") || (button.Name == "CheckButtonXMinusControlCommand"))
                                {
                                    //x = _fMenualMoveVelocity;
                                    byte[] data = new byte[8];
                                    UserCodesysData.JogControl mJogCtrl = new UserCodesysData.JogControl();
                                    if (button.Name == "CheckButtonXPlusControlCommand")
                                    {
                                        if (CheckButtonXMinusControlCommand.Checked)
                                        {                                            
                                            CheckButtonXMinusControlCommand.Checked = false;
                                            mJogCtrl.JogEnable = 0;
                                            mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                            data = mJogCtrl.GetData();
                                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_X_NEGATIVE, data);
                                        }                                            
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_X_POSITIVE, data);
                                        
                                    }
                                    else
                                    {
                                        if (CheckButtonXPlusControlCommand.Checked)
                                        {
                                            //CheckButtonXPlusControlCommand.Toggle();
                                            CheckButtonXPlusControlCommand.Checked = false;
                                            mJogCtrl.JogEnable = 0;
                                            mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                            data = mJogCtrl.GetData();
                                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_X_POSITIVE, data);
                                        }
                                        
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_X_NEGATIVE, data);
                                        
                                        //else
                                            //CheckButtonXMinusControlCommand.Toggle();
                                            //CheckButtonXMinusControlCommand.Checked = true;
                                    }
                                }
                                else if ((button.Name == "CheckButtonY1PlusControlCommand") || (button.Name == "CheckButtonY1MinusControlCommand"))
                                {
                                    //y1 = Convert.ToDouble(textEditJogAbsolutePosition.Text);
                                    byte[] data = new byte[8];
                                    UserCodesysData.JogControl mJogCtrl = new UserCodesysData.JogControl();
                                    if (button.Name == "CheckButtonY1PlusControlCommand")
                                    {
                                        if (CheckButtonY1MinusControlCommand.Checked)
                                        {
                                            CheckButtonY1MinusControlCommand.Checked = false;
                                            CheckButtonXMinusControlCommand.Checked = false;
                                            mJogCtrl.JogEnable = 0;
                                            mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                            data = mJogCtrl.GetData();
                                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_Y_NEGATIVE, data);
                                        }
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_Y_POSITIVE, data);
                                    }
                                    else
                                    {
                                        if (CheckButtonY1PlusControlCommand.Checked)
                                        {
                                            CheckButtonY1PlusControlCommand.Checked = false;
                                            mJogCtrl.JogEnable = 0;
                                            mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                            data = mJogCtrl.GetData();
                                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_Y_POSITIVE, data);
                                        }
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_Y_NEGATIVE, data);
                                    }                                        
                                }
                                else if ((button.Name == "CheckButtonY2PlusControlCommand") || (button.Name == "CheckButtonY2MinusControlCommand"))
                                {
                                    byte[] data = new byte[8];
                                    UserCodesysData.JogControl mJogCtrl = new UserCodesysData.JogControl();
                                    if (button.Name == "CheckButtonY2PlusControlCommand")
                                    {
                                        if (CheckButtonY2MinusControlCommand.Checked)
                                        {
                                            CheckButtonY2MinusControlCommand.Checked = false;                                            
                                            mJogCtrl.JogEnable = 0;
                                            mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                            data = mJogCtrl.GetData();
                                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_Z_NEGATIVE, data);
                                        }
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_Z_POSITIVE, data);
                                    }
                                    else
                                    {
                                        if (CheckButtonY2PlusControlCommand.Checked)
                                        {
                                            CheckButtonY2PlusControlCommand.Checked = false;
                                            mJogCtrl.JogEnable = 0;
                                            mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                            data = mJogCtrl.GetData();
                                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_Z_POSITIVE, data);
                                        }
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_Z_NEGATIVE, data);
                                    }
                                }
                                else if ((button.Name == "CheckButtonZPlusControlCommand") || (button.Name == "CheckButtonZMinusControlCommand"))
                                {
                                    byte[] data = new byte[8];
                                    UserCodesysData.JogControl mJogCtrl = new UserCodesysData.JogControl();
                                    if (button.Name == "CheckButtonZPlusControlCommand")
                                    {
                                        if (CheckButtonZMinusControlCommand.Checked)
                                        {
                                            CheckButtonZMinusControlCommand.Checked = false;
                                            mJogCtrl.JogEnable = 0;
                                            mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                            data = mJogCtrl.GetData();
                                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_RX_NEGATIVE, data);
                                        }
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_RX_POSITIVE, data);
                                    }
                                    else
                                    {
                                        if (CheckButtonZPlusControlCommand.Checked)
                                        {
                                            CheckButtonZPlusControlCommand.Checked = false;
                                            mJogCtrl.JogEnable = 0;
                                            mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                            data = mJogCtrl.GetData();
                                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_RX_POSITIVE, data);
                                        }
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_RX_NEGATIVE, data);
                                    }
                                }
                                else if ((button.Name == "CheckButtonFZPlusControlCommand") || (button.Name == "CheckButtonFZMinusControlCommand"))
                                {
                                    byte[] data = new byte[8];
                                    UserCodesysData.JogControl mJogCtrl = new UserCodesysData.JogControl();
                                    if (button.Name == "CheckButtonFZPlusControlCommand")
                                    {
                                        if (CheckButtonFZMinusControlCommand.Checked)
                                        {
                                            CheckButtonFZMinusControlCommand.Checked = false;
                                            mJogCtrl.JogEnable = 0;
                                            mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                            data = mJogCtrl.GetData();
                                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_RY_NEGATIVE, data);
                                        }
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_RY_POSITIVE, data);
                                    }
                                    else
                                    {
                                        if (CheckButtonFZPlusControlCommand.Checked)
                                        {
                                            CheckButtonFZPlusControlCommand.Checked = false;
                                            mJogCtrl.JogEnable = 0;
                                            mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                            data = mJogCtrl.GetData();
                                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_RY_POSITIVE, data);
                                        }
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_RY_NEGATIVE, data);
                                    }
                                }
                                else if ((button.Name == "CheckButtonFRPlusControlCommand") || (button.Name == "CheckButtonFRMinusControlCommand"))
                                {
                                    byte[] data = new byte[8];
                                    UserCodesysData.JogControl mJogCtrl = new UserCodesysData.JogControl();
                                    if (button.Name == "CheckButtonFRPlusControlCommand")
                                    {
                                        if (CheckButtonFRMinusControlCommand.Checked)
                                        {
                                            CheckButtonFRMinusControlCommand.Checked = false;
                                            mJogCtrl.JogEnable = 0;
                                            mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                            data = mJogCtrl.GetData();
                                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_RZ_NEGATIVE, data);
                                        }
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_RZ_POSITIVE, data);
                                    }
                                    else
                                    {
                                        if (CheckButtonFRPlusControlCommand.Checked)
                                        {
                                            CheckButtonFRPlusControlCommand.Checked = false;
                                            mJogCtrl.JogEnable = 0;
                                            mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                            data = mJogCtrl.GetData();
                                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_RZ_POSITIVE, data);
                                        }
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_RZ_NEGATIVE, data);
                                    }
                                }
                            }
                            else if (_MenaulControlMode == 1)
                            {
                                if ((button.Name == "CheckButtonXPlusControlCommand") || (button.Name == "CheckButtonXMinusControlCommand"))
                                {
                                    byte[] data = new byte[8];
                                    UserCodesysData.JogControl mJogCtrl = new UserCodesysData.JogControl();
                                    if (button.Name == "CheckButtonXPlusControlCommand")
                                    {
                                        CheckButtonXPlusControlCommand.Checked = false;
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_X_POSITIVE, data);
                                    }
                                    else
                                    {
                                        CheckButtonXMinusControlCommand.Checked = false;
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_X_NEGATIVE, data);
                                    }
                                }
                                else if ((button.Name == "CheckButtonY1PlusControlCommand") || (button.Name == "CheckButtonY1MinusControlCommand"))
                                {
                                    byte[] data = new byte[8];
                                    UserCodesysData.JogControl mJogCtrl = new UserCodesysData.JogControl();
                                    if (button.Name == "CheckButtonY1PlusControlCommand")
                                    {
                                        CheckButtonY1PlusControlCommand.Checked = false;
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_Y_POSITIVE, data);
                                    }
                                    else
                                    {
                                        CheckButtonY1MinusControlCommand.Checked = false;
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_Y_NEGATIVE, data);
                                    }
                                }
                                else if ((button.Name == "CheckButtonY2PlusControlCommand") || (button.Name == "CheckButtonY2MinusControlCommand"))
                                {
                                    byte[] data = new byte[8];
                                    UserCodesysData.JogControl mJogCtrl = new UserCodesysData.JogControl();
                                    if (button.Name == "CheckButtonY2PlusControlCommand")
                                    {
                                        CheckButtonY2PlusControlCommand.Checked = false;
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_Z_POSITIVE, data);
                                    }
                                    else
                                    {
                                        CheckButtonY2MinusControlCommand.Checked = false;
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_Z_NEGATIVE, data);
                                    }
                                }
                                else if ((button.Name == "CheckButtonZPlusControlCommand") || (button.Name == "CheckButtonZMinusControlCommand"))
                                {
                                    byte[] data = new byte[8];
                                    UserCodesysData.JogControl mJogCtrl = new UserCodesysData.JogControl();
                                    if (button.Name == "CheckButtonZPlusControlCommand")
                                    {
                                        CheckButtonZPlusControlCommand.Checked = false;
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_RX_POSITIVE, data);
                                    }
                                    else
                                    {
                                        CheckButtonZMinusControlCommand.Checked = false;
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_RX_NEGATIVE, data);
                                    }
                                }
                                else if ((button.Name == "CheckButtonFZPlusControlCommand") || (button.Name == "CheckButtonFZMinusControlCommand"))
                                {
                                    byte[] data = new byte[8];
                                    UserCodesysData.JogControl mJogCtrl = new UserCodesysData.JogControl();
                                    if (button.Name == "CheckButtonFZPlusControlCommand")
                                    {
                                        CheckButtonFZPlusControlCommand.Checked = false;
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_RY_POSITIVE, data);
                                    }
                                    else
                                    {
                                        CheckButtonFZMinusControlCommand.Checked = false;
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_RY_NEGATIVE, data);
                                    }
                                }
                                else if ((button.Name == "CheckButtonFRPlusControlCommand") || (button.Name == "CheckButtonFRMinusControlCommand"))
                                {
                                    byte[] data = new byte[8];
                                    UserCodesysData.JogControl mJogCtrl = new UserCodesysData.JogControl();
                                    if (button.Name == "CheckButtonFRPlusControlCommand")
                                    {
                                        CheckButtonFRPlusControlCommand.Checked = false;
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_RZ_POSITIVE, data);
                                    }
                                    else
                                    {
                                        CheckButtonFRMinusControlCommand.Checked = false;
                                        mJogCtrl.JogEnable = 1;
                                        mJogCtrl.JogValue = Convert.ToSingle(_fMenualMoveVelocity);
                                        data = mJogCtrl.GetData();
                                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_RZ_NEGATIVE, data);
                                    }
                                }
                                else
                                {
                                    _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_MOVE_STOP, null);
                                }
                            }
                            else if (_MenaulControlMode == 2)
                            {
                            }
                            else
                            {
                            }
                        }
                        if ((button.Name == "CheckButtonXStopControlCommand") || (button.Name == "CheckButtonY1StopControlCommand") ||
                            (button.Name == "CheckButtonY2StopControlCommand") || (button.Name == "CheckButtonZStopControlCommand") ||
                            (button.Name == "CheckButtonFZStopControlCommand") || (button.Name == "CheckButtonFRStopControlCommand"))
                        {
                            CheckButtonXPlusControlCommand.Checked = false;
                            CheckButtonXMinusControlCommand.Checked = false;
                            CheckButtonY1PlusControlCommand.Checked = false;
                            CheckButtonY1MinusControlCommand.Checked = false;
                            CheckButtonY2PlusControlCommand.Checked = false;
                            CheckButtonY2MinusControlCommand.Checked = false;
                            CheckButtonZPlusControlCommand.Checked = false;
                            CheckButtonZMinusControlCommand.Checked = false;
                            CheckButtonFZPlusControlCommand.Checked = false;
                            CheckButtonFZMinusControlCommand.Checked = false;
                            CheckButtonFRPlusControlCommand.Checked = false;
                            CheckButtonFRMinusControlCommand.Checked = false;
                            CheckButtonXStopControlCommand.Checked = false;
                            CheckButtonY1StopControlCommand.Checked = false;
                            CheckButtonY2StopControlCommand.Checked = false;
                            CheckButtonZStopControlCommand.Checked = false;
                            CheckButtonFZStopControlCommand.Checked = false;
                            CheckButtonFRStopControlCommand.Checked = false;
                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_ALL_STOP, null);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private void CheckButtonStateControlCommand_CheckedChanged(object sender, EventArgs e)
        {
           try
            {
                if (_mPLCCommunicationManager != null)
                {
                    //if ((_mPLCCommunicationManager.IsConnected()) && (_isRobotEnable))
                    {
                        if (!(sender is DevExpress.XtraEditors.CheckButton button)) return;
                        
                        if ((_MenaulControlMode == 1) || (_MenaulControlMode == 2))
                        {
                            if ((button.Name == "CheckButtonXPlusControlCommand") || (button.Name == "CheckButtonXMinusControlCommand"))
                            {
                                if (button.Name == "CheckButtonXMinusControlCommand")
                                {
                                    CheckButtonXMinusControlCommand.Checked = false;
                                }
                                else
                                {
                                    CheckButtonXPlusControlCommand.Checked = false;
                                }
                            }
                            else if ((button.Name == "CheckButtonY1PlusControlCommand") || (button.Name == "CheckButtonY1MinusControlCommand"))
                            {

                                if (button.Name == "CheckButtonY1MinusControlCommand")
                                {
                                    CheckButtonY1MinusControlCommand.Checked = false;
                                }
                                else
                                {
                                    CheckButtonY1PlusControlCommand.Checked = false;
                                }
                            }
                            else if ((button.Name == "CheckButtonY2PlusControlCommand") || (button.Name == "CheckButtonY2MinusControlCommand"))
                            {
                                if (button.Name == "CheckButtonY2MinusControlCommand")
                                {
                                    CheckButtonY2MinusControlCommand.Checked = false;
                                }
                                else
                                {
                                    CheckButtonY2PlusControlCommand.Checked = false;
                                }
                            }
                            else if ((button.Name == "CheckButtonZPlusControlCommand") || (button.Name == "CheckButtonZMinusControlCommand"))
                            {
                                if (button.Name == "CheckButtonZMinusControlCommand")
                                {
                                    CheckButtonZMinusControlCommand.Checked = false;
                                }
                                else
                                {
                                    CheckButtonZPlusControlCommand.Checked = false;
                                }
                            }
                            else if ((button.Name == "CheckButtonFZPlusControlCommand") || (button.Name == "CheckButtonFZMinusControlCommand"))
                            {
                                if (button.Name == "CheckButtonFZMinusControlCommand")
                                {
                                    CheckButtonFZMinusControlCommand.Checked = false;
                                }
                                else
                                {
                                    CheckButtonFZPlusControlCommand.Checked = false;
                                }
                            }
                            else if ((button.Name == "CheckButtonFRPlusControlCommand") || (button.Name == "CheckButtonFRMinusControlCommand"))
                            {
                                if (button.Name == "CheckButtonFRMinusControlCommand")
                                {
                                    CheckButtonFRMinusControlCommand.Checked = false;
                                }
                                else
                                {
                                    CheckButtonFRPlusControlCommand.Checked = false;
                                }
                            }
                            else if ((button.Name == "CheckButtonXStopControlCommand") || (button.Name == "CheckButtonY1StopControlCommand") ||
                                        (button.Name == "CheckButtonY2StopControlCommand") || (button.Name == "CheckButtonZStopControlCommand") ||
                                        (button.Name == "CheckButtonFZStopControlCommand") || (button.Name == "CheckButtonFRStopControlCommand"))
                            {
                                CheckButtonXPlusControlCommand.Checked = false;
                                CheckButtonXMinusControlCommand.Checked = false;
                                CheckButtonY1PlusControlCommand.Checked = false;
                                CheckButtonY1MinusControlCommand.Checked = false;
                                CheckButtonY2PlusControlCommand.Checked = false;
                                CheckButtonY2MinusControlCommand.Checked = false;
                                CheckButtonZPlusControlCommand.Checked = false;
                                CheckButtonZMinusControlCommand.Checked = false;
                                CheckButtonFZPlusControlCommand.Checked = false;
                                CheckButtonFZMinusControlCommand.Checked = false;
                                CheckButtonFRPlusControlCommand.Checked = false;
                                CheckButtonFRMinusControlCommand.Checked = false;
                                CheckButtonXStopControlCommand.Checked = false;
                                CheckButtonY1StopControlCommand.Checked = false;
                                CheckButtonY2StopControlCommand.Checked = false;
                                CheckButtonZStopControlCommand.Checked = false;
                                CheckButtonFZStopControlCommand.Checked = false;
                                CheckButtonFRStopControlCommand.Checked = false;                                
                            }
                        }
                        else
                        {
                            ;
                        }
                        
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        private void ButtonStopControlCommand_CheckedChanged(object sender, EventArgs e)
        {

            CheckButtonXPlusControlCommand.Checked = false;
            CheckButtonXMinusControlCommand.Checked = false;
            CheckButtonY1PlusControlCommand.Checked = false;
            CheckButtonY1MinusControlCommand.Checked = false;
            CheckButtonY2PlusControlCommand.Checked = false;
            CheckButtonY2MinusControlCommand.Checked = false;
            CheckButtonZPlusControlCommand.Checked = false;
            CheckButtonZMinusControlCommand.Checked = false;
            CheckButtonFZPlusControlCommand.Checked = false;
            CheckButtonFZMinusControlCommand.Checked = false;
            CheckButtonFRPlusControlCommand.Checked = false;
            CheckButtonFRMinusControlCommand.Checked = false;
            CheckButtonXStopControlCommand.Checked = false;
            CheckButtonY1StopControlCommand.Checked = false;
            CheckButtonY2StopControlCommand.Checked = false;
            CheckButtonZStopControlCommand.Checked = false;
            CheckButtonFZStopControlCommand.Checked = false;
            CheckButtonFRStopControlCommand.Checked = false;            
        }

        private void SendCmdPositionButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mPLCCommunicationManager != null)
                {
                    if ((_mPLCCommunicationManager.IsConnected()) && (_isRobotEnable) )
                    {
                        byte[] data = new byte[32];
                        UserCodesysData.TargetRobotPosition mCmdPosMove = new UserCodesysData.TargetRobotPosition();
                        RecipeManager.CalibrationParams.Position PrePos = new RecipeManager.CalibrationParams.Position();
                        RecipeManager.CalibrationParams.Position DeltaPos = new RecipeManager.CalibrationParams.Position();

                        mCmdPosMove.X = (double)Convert.ToSingle(textEditTargetPosX.Text);
                        if ((double)Convert.ToSingle(textEditTargetPosY1.Text) <= 9)
                            mCmdPosMove.Y = 9f;
                        else
                            mCmdPosMove.Y = (double)Convert.ToSingle(textEditTargetPosY1.Text);
                        if ((double)Convert.ToSingle(textEditTargetPosY2.Text) <= 100)
                            mCmdPosMove.Z = 100f;
                        else
                            mCmdPosMove.Z = (double)Convert.ToSingle(textEditTargetPosY2.Text);
                        mCmdPosMove.Roll = (double)Convert.ToSingle(textEditTargetPosZ.Text);
                        mCmdPosMove.Pitch = (double)Convert.ToSingle(textEditTargetPosFZ.Text);
                        mCmdPosMove.Yaw = (double)Convert.ToSingle(textEditTargetPosFR.Text);
                        mCmdPosMove.Speed = (double)Convert.ToSingle(textEditTargetVelocity.Text);
                        mCmdPosMove.Acceleration = (double)Convert.ToSingle(textEditTargetAcceleration.Text);

                        if (checkEditCalibration.Checked)
                        {
                            PrePos.X = mCmdPosMove.X;
                            PrePos.Y1 = mCmdPosMove.Y;
                            PrePos.Y2 = mCmdPosMove.Z;
                            PrePos.Z = mCmdPosMove.Roll;
                            PrePos.FZ = mCmdPosMove.Pitch;
                            PrePos.FR = mCmdPosMove.Yaw;

                            if (_iCalibrationMode == 0)
                            {
                                DeltaPos = CalibrationParam.EmiterCalibratinoDeltaPosition(PrePos);
                            }
                            else
                            {
                                DeltaPos = CalibrationParam.InspectCalibratinoDeltaPosition(PrePos);
                            }
                            mCmdPosMove.X += DeltaPos.X;
                            mCmdPosMove.Y += DeltaPos.Y1;
                            mCmdPosMove.Z += DeltaPos.Y2;
                            mCmdPosMove.Roll += DeltaPos.Z;
                            mCmdPosMove.Pitch += DeltaPos.FZ;
                            mCmdPosMove.Yaw += DeltaPos.FR;
                        }

                        if (mCmdPosMove.Speed <= 0) mCmdPosMove.Speed = 1;
                        if (mCmdPosMove.Acceleration <= 0) mCmdPosMove.Acceleration = 1;
                        data = mCmdPosMove.GetData();
                        // Send Cordinate Move Command
                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_GOPOS_ABS, data);
                    }
                }
            }
            catch(Exception)
            {
                ;
            }

        }

        private void SendCommandMoveStopButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mPLCCommunicationManager != null)
                {                    
                    if ((_mPLCCommunicationManager.IsConnected()) && (_isRobotEnable))
                    {
                        // Send Move Stop Command                        
                        _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_MOVE_STOP, null);
                    }                    
                }
            }
            catch (Exception)
            {
                ;
            }
        }

        private void checkButtonLowValue_Click(object sender, EventArgs e)
        {
            if (_mPLCCommunicationManager != null)
            {
                if ((_mPLCCommunicationManager.IsConnected()) && (_isRobotEnable) && (!_isAutomode))
                {                    
                    byte[] data = new byte[8];
                    UserCodesysData.JogModeSwitch mJogData = new UserCodesysData.JogModeSwitch();
                    if (radioGroupMenualMode.SelectedIndex == 0)
                    {                        
                        if (radioGroupMenualControlMode.SelectedIndex == 0)
                        {
                            _fMenualMoveVelocity = _fdefineVelValue[0];
                            mJogData.MenualMode = 0;
                            mJogData.MenualValue = Convert.ToSingle(_fMenualMoveVelocity);
                            data = mJogData.GetData();
                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_OP_MODE, data);                            
                        }
                        else if (radioGroupMenualControlMode.SelectedIndex == 1)
                        {
                            _fMenualMoveVelocity = _fdefineStepValue[0];
                            mJogData.MenualMode = 1;
                            mJogData.MenualValue = Convert.ToSingle(_fMenualMoveVelocity);
                            data = mJogData.GetData();
                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_MOTION_MODE, data);
                        }
                    }
                    else
                    {
                        _fMenualMoveVelocity = _fdefineStepValue[0];
                        if (radioGroupMenualControlMode.SelectedIndex == 1)
                        {
                            mJogData.MenualMode = 1;
                            mJogData.MenualValue = Convert.ToSingle(_fMenualMoveVelocity);
                            data = mJogData.GetData();
                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_MOTION_MODE, data);                               
                        }
                        else if (radioGroupMenualControlMode.SelectedIndex == 2)
                        {
                            mJogData.MenualMode = 2;
                            mJogData.MenualValue = Convert.ToSingle(_fMenualMoveVelocity);
                            data = mJogData.GetData();
                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_COORDINATE_MODE, data);                               
                        }
                    }
                }
            }
        }

        private void checkButtonMiddleValue_Click(object sender, EventArgs e)
        {
            if (_mPLCCommunicationManager != null)
            {
                if ((_mPLCCommunicationManager.IsConnected()) && (_isRobotEnable) && (!_isAutomode))
                {                    
                    byte[] data = new byte[8];
                    UserCodesysData.JogModeSwitch mJogData = new UserCodesysData.JogModeSwitch();

                    if (radioGroupMenualMode.SelectedIndex == 0)
                    {                        
                        if (radioGroupMenualControlMode.SelectedIndex == 0)
                        {
                            _fMenualMoveVelocity = _fdefineVelValue[1];
                            mJogData.MenualMode = 0;
                            mJogData.MenualValue = Convert.ToSingle(_fMenualMoveVelocity);
                            data = mJogData.GetData();
                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_OP_MODE, data);
                        }
                        else if (radioGroupMenualControlMode.SelectedIndex == 1)
                        {
                            _fMenualMoveVelocity = _fdefineStepValue[1];
                            mJogData.MenualMode = 1;
                            mJogData.MenualValue = Convert.ToSingle(_fMenualMoveVelocity);
                            data = mJogData.GetData();
                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_MOTION_MODE, data);
                        }
                    }
                    else
                    {
                        _fMenualMoveVelocity = _fdefineStepValue[1];
                        if (radioGroupMenualControlMode.SelectedIndex == 1)
                        {
                            mJogData.MenualMode = 1;
                            mJogData.MenualValue = Convert.ToSingle(_fMenualMoveVelocity);
                            data = mJogData.GetData();
                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_MOTION_MODE, data);
                        }
                        else if (radioGroupMenualControlMode.SelectedIndex == 2)
                        {
                            mJogData.MenualMode = 2;
                            mJogData.MenualValue = Convert.ToSingle(_fMenualMoveVelocity);
                            data = mJogData.GetData();
                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_COORDINATE_MODE, data);
                        }
                    }
                }
            }
        }

        private void checkButtonHighValue_Click(object sender, EventArgs e)
        {
            if (_mPLCCommunicationManager != null)
            {
                if ((_mPLCCommunicationManager.IsConnected()) && (_isRobotEnable) && (!_isAutomode))
                {                    
                    byte[] data = new byte[8];
                    UserCodesysData.JogModeSwitch mJogData = new UserCodesysData.JogModeSwitch();

                    if (radioGroupMenualMode.SelectedIndex == 0)
                    {                        
                        if (radioGroupMenualControlMode.SelectedIndex == 0)
                        {
                            _fMenualMoveVelocity = _fdefineVelValue[2];
                            mJogData.MenualMode = 0;
                            mJogData.MenualValue = Convert.ToSingle(_fMenualMoveVelocity);
                            data = mJogData.GetData();
                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_OP_MODE, data);
                        }
                        else if (radioGroupMenualControlMode.SelectedIndex == 1)
                        {
                            _fMenualMoveVelocity = _fdefineStepValue[2];
                            mJogData.MenualMode = 1;
                            mJogData.MenualValue = Convert.ToSingle(_fMenualMoveVelocity);
                            data = mJogData.GetData();
                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_MOTION_MODE, data);
                        }
                    }
                    else
                    {
                        _fMenualMoveVelocity = _fdefineStepValue[2];
                        if (radioGroupMenualControlMode.SelectedIndex == 1)
                        {
                            mJogData.MenualMode = 1;
                            mJogData.MenualValue = Convert.ToSingle(_fMenualMoveVelocity);
                            data = mJogData.GetData();
                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_MOTION_MODE, data);
                        }
                        else if (radioGroupMenualControlMode.SelectedIndex == 2)
                        {
                            mJogData.MenualMode = 2;
                            mJogData.MenualValue = Convert.ToSingle(_fMenualMoveVelocity);
                            data = mJogData.GetData();
                            _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_JOG_COORDINATE_MODE, data);
                        }
                    }
                }
            }
        }
        private void OutputControl_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mPLCCommunicationManager != null)
                {
                    if (_mPLCCommunicationManager.IsConnected())
                    {
                        if (!(sender is DevExpress.XtraEditors.LabelControl label)) return;
                        byte[] data = new byte[8];
                        UserCodesysData.DigitalOutputControl mOutputCtrl = new UserCodesysData.DigitalOutputControl();
                        ulong maskdata = 0;
                        switch (label.Name)
                        {
                            case "labelControlDOut1":
                                if ( Convert.ToBoolean(OutputData & (1 << 0)))
                                {
                                    maskdata = (1 << 0);
                                    OutputData &= ~maskdata;
                                }
                                else
                                {
                                    OutputData |= (1 << 0);
                                }
                                mOutputCtrl.Bit64 = OutputData;
                                data = mOutputCtrl.GetData();
                                _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_OUT_CTRL, data);
                                break;
                            case "labelControlDOut2":
                                if (Convert.ToBoolean(OutputData & (1 << 1)))
                                {
                                    maskdata = (1 << 1);
                                    OutputData &= ~maskdata;
                                }
                                else
                                {
                                    OutputData |= (1 << 1);
                                }
                                mOutputCtrl.Bit64 = OutputData;
                                data = mOutputCtrl.GetData();
                                _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_OUT_CTRL, data);
                                break;
                            case "labelControlDOut3":
                                if (Convert.ToBoolean(OutputData & (1 << 2)))
                                {
                                    maskdata = (1 << 2);
                                    OutputData &= ~maskdata;
                                }
                                else
                                {
                                    OutputData |= (1 << 2);
                                }
                                mOutputCtrl.Bit64 = OutputData;
                                data = mOutputCtrl.GetData();
                                _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_OUT_CTRL, data);
                                break;
                            case "labelControlDOut4":
                                if (Convert.ToBoolean(OutputData & (1 << 3)))
                                {
                                    maskdata = (1 << 3);
                                    OutputData &= ~maskdata;
                                }
                                else
                                {
                                    OutputData |= (1 << 3);
                                }
                                mOutputCtrl.Bit64 = OutputData;
                                data = mOutputCtrl.GetData();
                                _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_OUT_CTRL, data);
                                break;
                            case "labelControlDOut5":
                                if (Convert.ToBoolean(OutputData & (1 << 4)))
                                {
                                    maskdata = (1 << 4);
                                    OutputData &= ~maskdata;
                                }
                                else
                                {
                                    OutputData |= (1 << 4);
                                }
                                mOutputCtrl.Bit64 = OutputData;
                                data = mOutputCtrl.GetData();
                                _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_OUT_CTRL, data);
                                break;
                            case "labelControlDOut6":
                                if (Convert.ToBoolean(OutputData & (1 << 5)))
                                {
                                    maskdata = (1 << 5);
                                    OutputData &= ~maskdata;
                                }
                                else
                                {
                                    OutputData |= (1 << 5);
                                }
                                mOutputCtrl.Bit64 = OutputData;
                                data = mOutputCtrl.GetData();
                                _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_OUT_CTRL, data);
                                break;
                            case "labelControlDOut7":
                                if (Convert.ToBoolean(OutputData & (1 << 6)))
                                {
                                    maskdata = (1 << 6);
                                    OutputData &= ~maskdata;
                                }
                                else
                                {
                                    OutputData |= (1 << 6);
                                }
                                mOutputCtrl.Bit64 = OutputData;
                                data = mOutputCtrl.GetData();
                                _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_OUT_CTRL, data);
                                break;
                            case "labelControlDOut8":
                                if (Convert.ToBoolean(OutputData & (1 << 7)))
                                {
                                    maskdata = (1 << 7);
                                    OutputData &= ~maskdata;
                                }
                                else
                                {
                                    OutputData |= (1 << 7);
                                }
                                mOutputCtrl.Bit64 = OutputData;
                                data = mOutputCtrl.GetData();
                                _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_OUT_CTRL, data);
                                break;
                            case "labelControlDOut9":
                                if (Convert.ToBoolean(OutputData & (1 << 8)))
                                {
                                    maskdata = (1 << 8);
                                    OutputData &= ~maskdata;
                                }
                                else
                                {
                                    OutputData |= (1 << 8);
                                }
                                mOutputCtrl.Bit64 = OutputData;
                                data = mOutputCtrl.GetData();
                                _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_OUT_CTRL, data);
                                break;
                            case "labelControlDOut10":
                                if (Convert.ToBoolean(OutputData & (1 << 9)))
                                {
                                    maskdata = (1 << 9);
                                    OutputData &= ~maskdata;
                                }
                                else
                                {
                                    OutputData |= (1 << 9);
                                }
                                mOutputCtrl.Bit64 = OutputData;
                                data = mOutputCtrl.GetData();
                                _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_OUT_CTRL, data);
                                break;
                            case "labelControlDOut11":
                                if (Convert.ToBoolean(OutputData & (1 << 10)))
                                {
                                    maskdata = (1 << 10);
                                    OutputData &= ~maskdata;
                                }
                                else
                                {
                                    OutputData |= (1 << 10);
                                }
                                mOutputCtrl.Bit64 = OutputData;
                                data = mOutputCtrl.GetData();
                                _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_OUT_CTRL, data);
                                break;
                            case "labelControlDOut12":
                                if (Convert.ToBoolean(OutputData & (1 << 11)))
                                {
                                    maskdata = (1 << 11);
                                    OutputData &= ~maskdata;
                                }
                                else
                                {
                                    OutputData |= (1 << 11);
                                }
                                mOutputCtrl.Bit64 = OutputData;
                                data = mOutputCtrl.GetData();
                                _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_OUT_CTRL, data);
                                break;
                            case "labelControlDOut13":
                                if (Convert.ToBoolean(OutputData & (1 << 12)))
                                {
                                    maskdata = (1 << 12);
                                    OutputData &= ~maskdata;
                                }
                                else
                                {
                                    OutputData |= (1 << 12);
                                }
                                mOutputCtrl.Bit64 = OutputData;
                                data = mOutputCtrl.GetData();
                                _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_OUT_CTRL, data);
                                break;
                            case "labelControlDOut14":
                                if (Convert.ToBoolean(OutputData & (1 << 13)))
                                {
                                    maskdata = (1 << 13);
                                    OutputData &= ~maskdata;
                                }
                                else
                                {
                                    OutputData |= (1 << 13);
                                }
                                mOutputCtrl.Bit64 = OutputData;
                                data = mOutputCtrl.GetData();
                                _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_OUT_CTRL, data);
                                break;
                            case "labelControlDOut15":
                                if (Convert.ToBoolean(OutputData & (1 << 14)))
                                {
                                    maskdata = (1 << 14);
                                    OutputData &= ~maskdata;
                                }
                                else
                                {
                                    OutputData |= (1 << 14);
                                }
                                mOutputCtrl.Bit64 = OutputData;
                                data = mOutputCtrl.GetData();
                                _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_OUT_CTRL, data);
                                break;
                            case "labelControlDOut16":
                                if (Convert.ToBoolean(OutputData & (1 << 15)))
                                {
                                    maskdata = (1 << 15);
                                    OutputData &= ~maskdata;
                                }
                                else
                                {
                                    OutputData |= (1 << 15);
                                }
                                mOutputCtrl.Bit64 = OutputData;
                                data = mOutputCtrl.GetData();
                                _mPLCCommunicationManager.SendCommand(UserCodesysData.Protocol_MSG.MSG_CMD_OUT_CTRL, data);
                                break;
                            default :
                                break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                ;
            }
        }
        private void textEditchangedText(object sender, EventArgs e)
        {
            textEditPresentPosX.EditValue = BackupInfo.mPosition.X;
        }
        //private void textEditchangingText(object sender, EventArgs e)
        //{
        //    textEditPresentPosX.Text = Convert.ToString(BackupInfo.mPosition.X);
        //}
    }
}
