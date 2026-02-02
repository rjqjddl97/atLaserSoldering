using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography;     // MD5 Hash
using CompactSECommunication.Communication.Control;
using CompactSECommunication.Communication.Data;
using CoherentCompactMini.SerialCommunication.Control;
using CoherentCompactMini.SerialCommunication.Data;
using CoherentCompactMini.SerialCommunication.DataProcessor;
using AiCControlLibrary.SerialCommunication.Control;
using AiCControlLibrary.SerialCommunication.Data;
using AiCControlLibrary.SerialCommunication.DataProcessor;

namespace LaserSoldering
{
    public class LaserSodering
    {
        public enum LaserSolderStepType
        {
            Stop = 0,
            Idle,
            Ready,
            PreHeat,
            PreHeatWireSupport,
            Heat,
            HeatWireSupport,
            LaserOff,
            ReverseWire,
            Finish,
            Error
        }        
        private enum LaserModule
        {
            CompactMini = 0,
            CompactSE
        }

        private const int EngineSleepTime = 50;                //11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199,479
        public FeederParameter _FeederParam;
        private LaserSolderStepType mSolderingEngineStep;
        public LaserSolderParameter _LaserSolderParam;

        public List<UserInformation> _mUserList;
        public UserInformation _userlnfo = new UserInformation();
        public MD5 _MD5Hash = null;
        public string _UserfilePath = "userdata.bin";
        private CompactSECommunication.Communication.Control.CommunicationManager _mCompactSEComm = null;
        private CompactSECommunication.Communication.Data.UserCompactSEData _mCompactSEData = null;
        private CoherentCompactMini.SerialCommunication.Control.CommunicationManager _mCompactMiniComm = null;
        private CoherentCompactMini.SerialCommunication.Data.CompactMiniData _mCompactMiniData = null;
        private AiCControlLibrary.SerialCommunication.Control.CommunicationManager _mFeederComm = null;
        private AiCControlLibrary.SerialCommunication.Data.AiCData _mFeederData = null;
        private string _TcpIpAddress = string.Format("192.168.3.11");                                           // CompactSE IP : 192.168.3.11
        private int _TcpPort = 12000;

        private bool _IsLaserConnect = false;
        private bool _IsFeederConnect = false;
        private bool _IsLaserError = false;
        private bool _IsLaserFatalError = false;
        private bool _IsLaserWarning = false;
        private bool _IsLaserBusy = false;
        private bool _IsLaserReady = false;
        private bool _IsLaserPilotOn = false;
        private bool _IsLaserPowerOn = false;
        private bool _IsLaserOn = false;
        private bool _IsAutoSoldering = false;
        private bool _IsFeederError = false;
        private bool _IsFeederInPosition = false;
        private bool _IsAutoSolderEnd = true;
        private bool _IsInitialSoldering = false;
        private double _LaserPresentCurrent = 0;
        private double _LaserPresentPower = 0;
        private double _FeederUsedLength = 0;
        private byte _LaserCookie = 0;

        private Thread ProcessEngine;
        private Thread InitSolderingSequence;

        private LaserModule _LaserSource = LaserModule.CompactMini;

        public int[][] DrvMotionMonitor = new int[1][];
        public bool IsLaserConnect { get { return _IsLaserConnect; } set { _IsLaserConnect = value; } }
        public bool IsFeederConnect { get { return _IsFeederConnect; } set { _IsFeederConnect = value; } }
        public bool IsLaserError { get { return _IsLaserError; } set { _IsLaserError = value; } }
        public bool IsLaserFatalError { get { return _IsLaserFatalError; } set { _IsLaserFatalError = value; } }
        public bool IsLaserWarning { get { return _IsLaserWarning; } set { _IsLaserWarning = value; } }
        public bool IsLaserBusy { get { return _IsLaserBusy; } set { _IsLaserBusy = value; } }
        public bool IsLaserReady { get { return _IsLaserReady; } set { _IsLaserReady = value; } }
        public bool IsLaserPowerOn { get { return _IsLaserPowerOn; } set { _IsLaserPowerOn = value; } }
        public bool IsLaserPilotOn { get { return _IsLaserPilotOn; } set { _IsLaserPilotOn = value; } }
        public bool IsLaserOn { get { return _IsLaserOn; } set { _IsLaserOn = value; } }
        public bool IsAutoSoldering { get { return _IsAutoSoldering; } set { _IsAutoSoldering = value; } }
        public bool IsAutoSolderEnd { get { return _IsAutoSolderEnd; } set { _IsAutoSolderEnd = value; } }
        public bool IsFeederError { get { return _IsFeederError; } set { _IsFeederError = value; } }
        public bool IsFeederInPosition { get { return _IsFeederInPosition; } set { _IsFeederInPosition = value; } }
        public bool IsInitialSoldering { get { return _IsInitialSoldering; } set { _IsInitialSoldering = value; } }
        public string LaserIpAddress { get { return _TcpIpAddress; } set { _TcpIpAddress = value; } }
        public int LaserPort { get { return _TcpPort; } set { _TcpPort = value; } }        
        public byte LaserCookie { get { return _LaserCookie; } set { _LaserCookie = value; } }

        public CompactSECommunication.Communication.Control.CommunicationManager CompactSEComm { get { return _mCompactSEComm; } set { _mCompactSEComm = value; } }
        public CompactSECommunication.Communication.Data.UserCompactSEData CompactSEData { get { return _mCompactSEData; } set { _mCompactSEData = value; } }
        public CoherentCompactMini.SerialCommunication.Control.CommunicationManager CompactMiniComm { get { return _mCompactMiniComm; } set { _mCompactMiniComm = value; } }
        public CoherentCompactMini.SerialCommunication.Data.CompactMiniData CompactMiniData { get { return _mCompactMiniData; } set { _mCompactMiniData = value; } }
        public AiCControlLibrary.SerialCommunication.Control.CommunicationManager FeederComm { get { return _mFeederComm; } set { _mFeederComm = value; } }
        public AiCControlLibrary.SerialCommunication.Data.AiCData FeederData { get { return _mFeederData; } set { _mFeederData = value; } }
        public FeederParameter FeederParam { get { return _FeederParam; } set { _FeederParam = value; } }
        public LaserSolderParameter LaserSolderParam { get { return _LaserSolderParam; } set { _LaserSolderParam = value; } }

        public double LaserPresentCurrent { get { return _LaserPresentCurrent; } set { _LaserPresentCurrent = value; }}
        public double LaserPresentPower { get { return _LaserPresentPower; } set { _LaserPresentPower = value; } }
        public double FeederUsedLength { get { return _FeederUsedLength; } set { _FeederUsedLength = value; } }
        public Stopwatch CheckTackTime = new Stopwatch();
        public int mCurrentReadyTime = 0;
        public int mCurrentPreHeatTime = 0;
        public int mCurrentHeatTime = 0;
        public LaserSodering()
        {
            _FeederParam = new FeederParameter();
            _LaserSolderParam = new LaserSolderParameter();
            _mCompactSEComm = new CompactSECommunication.Communication.Control.CommunicationManager();
            _mCompactMiniComm = new CoherentCompactMini.SerialCommunication.Control.CommunicationManager();
            _mFeederComm = new AiCControlLibrary.SerialCommunication.Control.CommunicationManager();
            _mCompactSEData = new UserCompactSEData();
            _mCompactMiniData = new CompactMiniData();
            _mFeederData = new AiCData();
            DrvMotionMonitor[0] = new int[Enum.GetValues(typeof(AiCData.MONITOR_DATA_MAP)).Length];
            //_FeederParam.InitialParameter();
            mSolderingEngineStep = LaserSolderStepType.Stop;
            ProcessEngine = new Thread(SolderingRun);
            ProcessEngine.Start();            
        }
        ~LaserSodering()
        {
            _FeederParam = null;
            _LaserSolderParam = null;
            _mCompactSEComm = null;
            _mCompactSEData = null;
            _mCompactMiniComm = null;
            _mCompactMiniData = null;
            _mFeederComm = null;
            _mFeederData = null;
            ProcessEngine.Abort();
        }
        public void PauseEngine()
        {
            mSolderingEngineStep = LaserSolderStepType.Stop;
        }

        public void StartEngine()
        {
            mSolderingEngineStep = LaserSolderStepType.Idle;
        }
        public void StopEngine()
        {
            ProcessEngine.Abort();
        }
        public void SetFeederParameter(FeederParameter param)
        {
            try
            {
                _FeederParam = param;
            }
            catch (Exception ex)
            {
                ;
            }
        }
        public void SetLaserSolderParam(LaserSolderParameter param)
        {
            try
            {
                _LaserSolderParam = param;
            }
            catch (Exception ex)
            {
                ;
            }
        }
        public void InitialCommunication(CompactSECommunication.Communication.Control.CommunicationManager laser, AiCControlLibrary.SerialCommunication.Control.CommunicationManager feeder)
        {
            try
            {
                _mCompactSEComm = laser;
                _mCompactSEData = _mCompactSEComm.mLaserSourceData;
                _LaserSource = LaserModule.CompactSE;
                _mFeederComm = feeder;
                _mFeederData = _mFeederComm.mDrvCtrl;
            }
            catch (Exception ex)
            {
                ;
            }
        }
        public void InitialCommunication(CoherentCompactMini.SerialCommunication.Control.CommunicationManager laser, AiCControlLibrary.SerialCommunication.Control.CommunicationManager feeder)
        {
            try
            {
                _mCompactMiniComm = laser;
                _mCompactMiniData = _mCompactMiniComm.mLaserSourceCtrl;
                _LaserSource = LaserModule.CompactMini;
                _mFeederComm = feeder;
                _mFeederData = _mFeederComm.mDrvCtrl;
            }
            catch(Exception ex)
            {
                ;
            }
        }
        public void LaserSelectedModule(bool CompactMini)
        {
            if (CompactMini)            
                _LaserSource = LaserModule.CompactMini;            
            else
                _LaserSource = LaserModule.CompactSE;
        }
        public void SetCommunicationParam(CoherentCompactMini.SerialCommunication.Control.SerialPortSetData laser, AiCControlLibrary.SerialCommunication.Control.SerialPortSetData feeder)
        {
            try
            {
                if ((laser != null) && (feeder != null))
                {
                    _mCompactMiniComm.SetSerialData(laser);
                    _mFeederComm.SetSerialData(feeder);
                }
            }
            catch (Exception ex)
            {
                ;
            }

        }
        public void SetCommunicationParam(CompactSECommunication.Communication.Control.TcpIpSetData laser, AiCControlLibrary.SerialCommunication.Control.SerialPortSetData feeder)
        {
            try
            {
                if ((laser != null) && (feeder != null))
                {
                    _mCompactSEComm.SetTcpIpData(laser);
                    _mFeederComm.SetSerialData(feeder);
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }
        public void LaserSolderingStart()
        {
            //if (_IsFeederConnect && _IsLaserConnect && _IsInitialSoldering)
            {
                if (!_IsFeederError && !_IsFeederError)
                {
                    _IsAutoSoldering = true;
                    _IsAutoSolderEnd = false;
                }
            }
        }
        public void LaserSolderingStop()
        {
            //if (_IsFeederConnect && _IsLaserConnect)
            {
                if (!_IsFeederError && !_IsFeederError)
                {
                    _IsAutoSoldering = false;
                    _IsAutoSolderEnd = true;
                    int datasize = 0;
                    byte[] data = null;                    
                    if (_LaserSource == LaserModule.CompactMini)
                    {
                        //datasize = _mCompactMiniData.GetSetLaserOnPacketSize();
                        //data = _mCompactMiniData.GetSetLaserOn(false);
                        //mLaserSerial.SendData(data);
                        //data = null;
                    }
                    else if (_LaserSource == LaserModule.CompactSE)
                    {
                        //datasize = _mCompactSEData.GetLaserOnPacketSize(1);                                
                        //data = _mCompactSEData.GetLaserOn(1, false);
                        //_mCompactSEComm.SendData(data);
                        //data = null;
                    }
                    if ((mSolderingEngineStep == LaserSolderStepType.HeatWireSupport) || (mSolderingEngineStep == LaserSolderStepType.LaserOff))
                    {
                        //data = _mFeederData.MoveTargetPositionSendData(_mFeederData.DrvID[0], (int)Math.Round(-_LaserSolderParam.ReverseWireLength * FeederParam._dFeedermmToPulseRatio));
                        //_mFeederComm.SendData(data);
                        //data = _mFeederData.SetMoveTargetVelocity(_mFeederData.DrvID[0], (int)Math.Round(_LaserSolderParam.ReverseVelocity * FeederParam._dFeedermmToPulseRatio));
                        //_mFeederComm.SendData(data);
                        //data = _mFeederData.SetMoveTargetAccel(_mFeederData.DrvID[0], (int)Math.Round(_LaserSolderParam.ReverseAcceleration * FeederParam._dFeedermmToPulseRatio));
                        //_mFeederComm.SendData(data);
                        //data = _mFeederData.MoveReleativeCommand(_mFeederData.DrvID[0]);
                        //_mFeederComm.SendData(data);
                    }
                    mSolderingEngineStep = LaserSolderStepType.Idle;
                }
            }
        }
        public void ConnectDeviceModule()
        {
            try
            {
                if (_LaserSource == LaserModule.CompactMini)
                {
                    if (!_mCompactMiniComm.IsOpen())
                    {
                        _mCompactMiniComm.Connect();
                        if (_mCompactMiniComm.IsOpen())
                        {
                            _mCompactMiniComm.ReceiveDataUpdateEvent += ReceiveUpdateLaserMiniData;
                            _IsLaserConnect = true;
                        }
                    }
                }
                else if (_LaserSource == LaserModule.CompactSE)
                {
                    if (!_mCompactSEComm.IsConnected())
                    {
                        _mCompactSEComm.Connect();
                        if (_mCompactSEComm.IsConnected())
                        {
                            _mCompactSEComm.ReceiveDataUpdateEvent += ReceiveUpdateLaserSEData;
                            _IsLaserConnect = true;
                        }
                    }
                }

                if (!_mFeederComm.IsOpen())
                {
                    _mFeederComm.Connect();
                    if (_mFeederComm.IsOpen())
                    {
                        _mFeederComm.ReceiveDataUpdateEvent += ReceiveUpdateFeederData;
                        _IsFeederConnect = true;
                    }
                }
                if (_IsLaserConnect && _IsFeederConnect)
                {
                    InitSolderingSequence = new Thread(InitialSolderProcess);
                    InitSolderingSequence.Start();
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }
        public void DisconnectDeviceModule()
        {
            try
            {
                if (_LaserSource == LaserModule.CompactMini)
                {
                    if (_mCompactMiniComm.IsOpen())
                    {
                        _mCompactMiniComm.Disconnect();                        
                        _mCompactMiniComm.ReceiveDataUpdateEvent -= ReceiveUpdateLaserMiniData;
                        _IsLaserConnect = false;                        
                    }
                }
                else if (_LaserSource == LaserModule.CompactSE)
                {
                    if (_mCompactSEComm.IsConnected())
                    {
                        _mCompactSEComm.Disconnect();
                        _mCompactSEComm.ReceiveDataUpdateEvent -= ReceiveUpdateLaserSEData;
                        _IsLaserConnect = false;
                        
                    }
                }

                if (_mFeederComm.IsOpen())
                {
                    _mFeederComm.Disconnect();
                    _mFeederComm.ReceiveDataUpdateEvent -= ReceiveUpdateFeederData;
                    _IsFeederConnect = false;
                    
                }
                if (!_IsLaserConnect && !_IsFeederConnect)
                {
                    PauseEngine();
                    _IsInitialSoldering = false;
                    _IsAutoSolderEnd = true;
                    _IsAutoSoldering = false;
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }
        public void ReceiveUpdateLaserSEData(UserCompactSEData update)
        {
            if (_LaserSource == LaserModule.CompactSE)
            {
                _mCompactSEData = update;
            }            
        }
        public void ReceiveUpdateLaserMiniData(CompactMiniData update)
        {
            if (_LaserSource == LaserModule.CompactMini)
            {
                _mCompactMiniData = update;
            }
        }
        public void ReceiveUpdateFeederData(AiCData update)
        {
            if (_mFeederData != null)
            {
                _mFeederData = update;
            }
        }
        public void UpdateCommuncationDatas()
        {
            try
            {
                if (_IsLaserConnect)
                {
                    if (_LaserSource == LaserModule.CompactMini)
                    {
                        if (_mCompactMiniData.Status.B0)
                            _IsLaserPowerOn = true;
                        else
                            _IsLaserPowerOn = false;

                        //if (Convert.ToBoolean(mLaserData.LaserStatus.B3))
                        if (_mCompactMiniData.Status.B1)
                            _IsLaserOn = true;
                        else
                            _IsLaserOn = false;

                        if (_mCompactMiniData.Status.B2)
                            _IsLaserError = true;
                        else
                            _IsLaserError = false;

                        if (_mCompactMiniData.Status.B3)
                            _IsLaserFatalError = true;
                        else
                            _IsLaserFatalError = false;

                        if (_mCompactMiniData.Status.B4)
                            _IsLaserPilotOn = true;
                        else
                            _IsLaserPilotOn = false;
                        _LaserPresentPower = (double)_mCompactMiniData.LaserPowerRatio;
                    }
                    else if (_LaserSource == LaserModule.CompactSE)
                    {
                        if ((_mCompactSEData.LaserStatus.Bit8 & 0x03) == 0)
                        {
                            // Error Status
                            _IsLaserError = true;
                            _IsLaserWarning = false;
                            _IsLaserBusy = false;
                            _IsLaserReady = false;
                        }
                        else if ((_mCompactSEData.LaserStatus.Bit8 & 0x03) == 1)
                        {
                            // Warning Status
                            _IsLaserWarning = true;
                            _IsLaserError = false;
                            _IsLaserBusy = false;
                            _IsLaserReady = false;
                        }
                        else if ((_mCompactSEData.LaserStatus.Bit8 & 0x03) == 2)
                        {
                            // Busy Status
                            _IsLaserBusy = true;
                            _IsLaserError = false;
                            _IsLaserWarning = false;
                            _IsLaserReady = false;
                        }
                        else if ((_mCompactSEData.LaserStatus.Bit8 & 0x03) == 3)
                        {
                            // Ready Status
                            _IsLaserError = false;
                            _IsLaserWarning = false;
                            _IsLaserBusy = false;
                            _IsLaserReady = true;
                        }
                        else
                        {
                            _IsLaserWarning = false;
                            _IsLaserError = false;
                            _IsLaserBusy = false;
                            _IsLaserReady = false;
                        }
                        if (_mCompactSEData.LaserStatus.B2)
                            _IsLaserPowerOn = true;
                        else
                            _IsLaserPowerOn = false;

                        if (_mCompactSEData.LaserStatus.B3)
                            _IsLaserOn = true;
                        else
                            _IsLaserOn = false;

                        _LaserPresentPower = _mCompactSEData.ActurePower;
                        _LaserPresentCurrent = _mCompactSEData.ActureCurrent;
                    }
                }
                if (_IsFeederConnect)
                {
                    int[] itempval = new int[4];
                    int datasum = 0;

                    Array.Copy(_mFeederData._mAiCMotionDatas._CurrentDatas, 0, DrvMotionMonitor[_mFeederData._mAiCMotionDatas._Id - 1], 0, _mFeederData._mAiCMotionDatas._CurrentDatas.Length);
                    Array.Copy(_mFeederData._mAiCMotionDatas._CurrentDatas, 0, itempval, 0, 1);      // Op Mode;
                    Array.Copy(_mFeederData._mAiCMotionDatas._CurrentDatas, 1, itempval, 0, 2);      // Target Position
                    datasum = itempval[0];
                    datasum = (datasum << 16) | itempval[1];
                    Array.Copy(_mFeederData._mAiCMotionDatas._CurrentDatas, 3, itempval, 0, 2);      // Present Position
                    datasum = itempval[0];
                    datasum = (datasum << 16) | itempval[1];

                    //if (textBoxPresentPosition.InvokeRequired)
                    //{
                    //    textBoxPresentPosition.Invoke(new MethodInvoker(delegate { textBoxPresentPosition.Text = Convert.ToString(datasum * _dFeederPulseTommRatio); }));
                    //}
                    //else
                    //    textBoxPresentPosition.Text = Convert.ToString(datasum * _dFeederPulseTommRatio);

                    Array.Copy(_mFeederData._mAiCMotionDatas._CurrentDatas, 5, itempval, 0, 2);      // Target Velocity
                    datasum = itempval[0];
                    datasum = (datasum << 16) | itempval[1];

                    //if (textEditTargetVel1.InvokeRequired)
                    //{
                    //    textEditTargetVel1.Invoke(new MethodInvoker(delegate { textEditTargetVel1.EditValue = Convert.ToDouble(datasum * _MotionParam.Pulse2MMRatioX); }));
                    //}
                    //else
                    //    textEditTargetVel1.EditValue = Convert.ToDouble(datasum * _MotionParam.Pulse2MMRatioX);

                    Array.Copy(_mFeederData._mAiCMotionDatas._CurrentDatas, 7, itempval, 0, 2);      // Present Velocity
                    datasum = itempval[0];
                    datasum = (datasum << 16) | itempval[1];

                    //if (textEditPresentVel1.InvokeRequired)
                    //{
                    //    textEditPresentVel1.Invoke(new MethodInvoker(delegate { textEditPresentVel1.EditValue = Convert.ToDouble(datasum * _MotionParam.Pulse2MMRatioX); }));
                    //}
                    //else
                    //    textEditPresentVel1.EditValue = Convert.ToDouble(datasum * _MotionParam.Pulse2MMRatioX);

                    Array.Copy(_mFeederData._mAiCMotionDatas._CurrentDatas, 9, itempval, 0, 1);      // Motor RPM

                    //if (textEditMotorRPM1.InvokeRequired)
                    //{
                    //    textEditMotorRPM1.Invoke(new MethodInvoker(delegate { textEditMotorRPM1.EditValue = Convert.ToInt32(itempval[0]); }));
                    //}
                    //else
                    //    textEditMotorRPM1.EditValue = Convert.ToInt32(itempval[0]);

                    Array.Copy(_mFeederData._mAiCMotionDatas._CurrentDatas, 10, itempval, 0, 1);     // Program Step

                    //if (textEditProgramStep1.InvokeRequired)
                    //{
                    //    textEditProgramStep1.Invoke(new MethodInvoker(delegate { textEditProgramStep1.EditValue = Convert.ToInt32(itempval[0]); }));
                    //}
                    //else
                    //    textEditProgramStep1.EditValue = Convert.ToInt32(itempval[0]);

                    Array.Copy(_mFeederData._mAiCMotionDatas._CurrentDatas, 11, itempval, 0, 1);     // Drive Error1
                    _mFeederData.AlarmError1[0].SetData(Convert.ToUInt16(itempval[0]));

                    Array.Copy(_mFeederData._mAiCMotionDatas._CurrentDatas, 12, itempval, 0, 1);     // Drive Error2
                    _mFeederData.AlarmError2[0].SetData(Convert.ToUInt16(itempval[0]));

                    Array.Copy(_mFeederData._mAiCMotionDatas._CurrentDatas, 13, itempval, 0, 1);     // Drive Status1
                    _mFeederData.InfoStatus1[0].SetData((UInt16)itempval[0]);

                    Array.Copy(_mFeederData._mAiCMotionDatas._CurrentDatas, 14, itempval, 0, 1);     // Drive Status2
                    _mFeederData.InfoStatus2[0].SetData((UInt16)itempval[0]);

                    Array.Copy(_mFeederData._mAiCMotionDatas._CurrentDatas, 15, itempval, 0, 1);     // Drive Output Status
                    _mFeederData.OutputStaus[0].SetData((UInt16)itempval[0]);

                    if (Convert.ToBoolean(_mFeederData.OutputStaus[0].B1))
                        _IsFeederInPosition = true;
                    else
                        _IsFeederInPosition = false;

                    if ((_mFeederData.AlarmError1[0].Bit16 != 0) || (_mFeederData.AlarmError2[0].Bit16 != 0))
                    {
                        _IsFeederError = true;
                        //if (textBoxDriveStatus.InvokeRequired)
                        //{
                        //    textBoxDriveStatus.Invoke(new MethodInvoker(delegate { textBoxDriveStatus.Text = "Error"; }));
                        //}
                        //else
                        //    textBoxDriveStatus.Text = "Error";
                    }
                    else
                    {
                        _IsFeederError = false;
                        //if (textBoxDriveStatus.InvokeRequired)
                        //{
                        //    textBoxDriveStatus.Invoke(new MethodInvoker(delegate { textBoxDriveStatus.Text = "Nomal"; }));
                        //}
                        //else
                        //    textBoxDriveStatus.Text = "Nomal";
                    }
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }
        public void LaserReset()
        {
            try
            {
                if (_IsLaserConnect)
                {
                    int datasize = 0;
                    byte[] data = null;
                    if (_LaserSource == LaserModule.CompactMini)
                    {                       
                        //datasize = _mCompactMiniData.GetSetErrorResetPacketSize();
                        //data = _mCompactMiniData.GetSetErrorReset();
                        //_mCompactMiniComm.SendData(data);
                        //data = null;
                    }
                    else if (_LaserSource == LaserModule.CompactSE)
                    {
                        //datasize = _mCompactSEData.GetResetOnPacketSize((int)_LaserCookie);                                
                        //data = _mCompactSEData.GetResetOn((int)_LaserCookie,true);
                        //_mCompactSEComm.SendData(data);
                        //data = null;
                    }
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }
        public void FeederReset()
        {
            try
            {
                int datasize = 0;
                byte[] data = null;
                if (_IsFeederConnect)
                {
                    //data = _mFeederData.AlarmResetCommand(_mFeederComm.mDrvCtrl.DrvID[0]);
                    //_mFeederComm.SendData(data);
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }
        private void InitialSolderProcess()
        {
            try
            {
                int datasize = 0;
                byte[] data = null;
                if (!_IsInitialSoldering)
                {
                    if (_IsLaserConnect)
                    {
                        if (_LaserSource == LaserModule.CompactMini)
                        {
                            /* 1. VT100 Mode disable */
                            //datasize = _mCompactMiniData.GetSetVT100ModePacketSize(0);
                            //data = _mCompactMiniData.GetSetVT100Mode(0);
                            //_mCompactMiniComm.SendData(data);
                            Thread.Sleep(50);
                            /* 2. Internal control Enable */
                            //datasize = _mCompactMiniData.GetSetLaserControlPacketSize();
                            //data = _mCompactMiniData.GetSetLaserControl(false);
                            //_mCompactMiniComm.SendData(data);
                            Thread.Sleep(50);
                            data = null;
                        }
                        else if (_LaserSource == LaserModule.CompactSE)
                        {
                            //datasize = _mCompactSEData.GetSetPowerPacketSize(JobSolder.PreheatPowerRatio);                                
                            //data = _mCompactSEData.GetSetPower(JobSolder.PreheatPowerRatio);
                            //_mCompactSEComm.SendData(data);
                            //data = null;
                        }
                    }
                    if (_IsFeederConnect)
                    {
                        data = new byte[100];
                        data = _mFeederData.DriveInitialSetting(_mFeederComm.mDrvCtrl.DrvID[0], 100, (int)Math.Round(50D * _FeederParam._dFeedermmToPulseRatio), 50, 50);
                        _mFeederComm.SendData(data);
                    }

                    _IsInitialSoldering = true;
                    StartEngine();
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }
        private void SolderingRun()
        {
            int datasize = 0;
            byte[] data = null;
            LaserSolderParameter JobSolder = new LaserSolderParameter();
            while (true)
            {
                //if (_IsLaserConnect && _IsFeederConnect)
                {
                    try
                    {
                        TimeSpan ts = CheckTackTime.Elapsed;
                        if (_IsLaserError || _IsFeederError)
                        {
                            if (_LaserSource == LaserModule.CompactMini)
                            {
                                //datasize = _mCompactMiniData.GetSetLaserOnPacketSize();
                                //data = _mCompactMiniData.GetSetLaserOn(false);
                                //mLaserSerial.SendData(data);
                                //data = null;
                            }
                            else if (_LaserSource == LaserModule.CompactSE)
                            {
                                //datasize = _mCompactSEData.GetLaserOnPacketSize(1);                                
                                //data = _mCompactSEData.GetLaserOn(1, false);
                                //_mCompactSEComm.SendData(data);
                                //data = null;
                            }
                            mSolderingEngineStep = LaserSolderStepType.Idle;
                            _IsAutoSoldering = false;
                            _IsAutoSolderEnd = false;
                        }
                        switch (mSolderingEngineStep)
                        {
                            case LaserSolderStepType.Stop:
                                break;
                            case LaserSolderStepType.Idle:
                                if (_IsAutoSoldering)
                                {
                                    JobSolder = _LaserSolderParam;
                                    if (_LaserSource == LaserModule.CompactMini)
                                    {
                                        //datasize = _mCompactMiniData.GetSetLaserPowerPacketSize(JobSolder.PreheatPowerRatio);
                                        //data = new byte[datasize];
                                        //data = _mCompactMiniData.GetSetLaserPower(JobSolder.PreheatPowerRatio);
                                        //_mCompactMiniComm.SendData(data);
                                        //data = null;
                                    }
                                    else if (_LaserSource == LaserModule.CompactSE)
                                    {
                                        //datasize = _mCompactSEData.GetSetPowerPacketSize(JobSolder.PreheatPowerRatio);                                
                                        //data = _mCompactSEData.GetSetPower(JobSolder.PreheatPowerRatio);
                                        //_mCompactSEComm.SendData(data);
                                        //data = null;
                                    }
                                    data = new byte[20];
                                    data = _mFeederData.MoveTargetPositionSendData(_mFeederData.DrvID[0], (int)Math.Round(JobSolder.ForwordingWireLength * FeederParam._dFeedermmToPulseRatio));
                                    _mFeederComm.SendData(data);
                                    data = _mFeederData.SetMoveTargetVelocity(_mFeederData.DrvID[0], (int)Math.Round(JobSolder.ForwordingVelocity * FeederParam._dFeedermmToPulseRatio));
                                    _mFeederComm.SendData(data);
                                    data = _mFeederData.SetMoveTargetAccel(_mFeederData.DrvID[0], (int)Math.Round(JobSolder.ForwordingAcceleration * FeederParam._dFeedermmToPulseRatio));
                                    _mFeederComm.SendData(data);
                                    data = null; 
                                    FeederUsedLength += JobSolder.ForwordingWireLength;
                                    mCurrentHeatTime = 0;
                                    mCurrentPreHeatTime = 0;
                                    mCurrentReadyTime = 0;
                                    mSolderingEngineStep = LaserSolderStepType.Ready;
                                }
                                break;
                            case LaserSolderStepType.Ready:
                                if (_LaserSource == LaserModule.CompactMini)
                                {
                                    //datasize = _mCompactMiniData.GetSetLaserOnPacketSize();                                
                                    //data = mLaserMiniData.GetSetLaserOn(true);
                                    //mLaserSerial.SendData(data);
                                    //data = null;
                                }
                                else if (_LaserSource == LaserModule.CompactSE)
                                {
                                    //datasize = _mCompactSEData.GetLaserOnPacketSize();                                
                                    //data = _mCompactSEData.GetLaserOn(true);
                                    //_mCompactSEComm.SendData(data);
                                    //data = null;
                                }
                                CheckTackTime.Reset();
                                CheckTackTime.Start();
                                mSolderingEngineStep = LaserSolderStepType.PreHeat;
                                break;
                            case LaserSolderStepType.PreHeat:
                                if (((int)ts.TotalMilliseconds) > JobSolder.ReadyTime)
                                {
                                    mCurrentReadyTime = (int)ts.TotalMilliseconds;
                                    CheckTackTime.Reset();
                                    CheckTackTime.Start();
                                    mSolderingEngineStep = LaserSolderStepType.PreHeatWireSupport;
                                }
                                break;
                            case LaserSolderStepType.PreHeatWireSupport:
                                data = new byte[20];
                                data = _mFeederData.MoveReleativeCommand(_mFeederData.DrvID[0]);
                                _mFeederComm.SendData(data);
                                data = null;
                                mSolderingEngineStep = LaserSolderStepType.Heat;
                                break;
                            case LaserSolderStepType.Heat:
                                if (((int)ts.TotalMilliseconds) > JobSolder.PreHeatTime)
                                {
                                    mCurrentPreHeatTime = (int)ts.TotalMilliseconds;
                                    CheckTackTime.Reset();
                                    CheckTackTime.Start();
                                    if (_LaserSource == LaserModule.CompactMini)
                                    {
                                        //datasize = _mCompactMiniData.GetSetLaserPowerPacketSize(JobSolder.HeatPowerRatio);
                                        //data = _mCompactMiniData.GetSetLaserPower(JobSolder.HeatPowerRatio);
                                        //mLaserSerial.SendData(data);
                                        //data = null;
                                    }
                                    else if (_LaserSource == LaserModule.CompactSE)
                                    {
                                        //datasize = _mCompactSEData.GetSetTargetPowerPacketSize(1, (double)JobSolder.HeatPowerRatio);                                
                                        //data = _mCompactSEData.GetSetTargetPower(1, (double)JobSolder.HeatPowerRatio);
                                        //_mCompactSEComm.SendData(data);
                                        //data = null;
                                    }
                                    mSolderingEngineStep = LaserSolderStepType.HeatWireSupport;
                                }
                                break;
                            case LaserSolderStepType.HeatWireSupport:
                                //data = _mFeederData.MoveReleativeCommand(_mFeederData.DrvID[0]);
                                //_mFeederComm.SendData(data);
                                mSolderingEngineStep = LaserSolderStepType.LaserOff;
                                break;
                            case LaserSolderStepType.LaserOff:
                                if (((int)ts.TotalMilliseconds) > JobSolder.HeatTime)
                                {
                                    mCurrentHeatTime = (int)ts.TotalMilliseconds;
                                    CheckTackTime.Reset();  
                                    if (_LaserSource == LaserModule.CompactMini)
                                    {
                                        //datasize = _mCompactMiniData.GetSetLaserOnPacketSize();
                                        //data = _mCompactMiniData.GetSetLaserOn(false);
                                        //mLaserSerial.SendData(data);
                                        //data = null;
                                    }
                                    else if (_LaserSource == LaserModule.CompactSE)
                                    {
                                        //datasize = _mCompactSEData.GetLaserOnPacketSize(1);                                
                                        //data = _mCompactSEData.GetLaserOn(1, false);
                                        //_mCompactSEComm.SendData(data);
                                        //data = null;
                                    }

                                    // Wire Solder Reverse Support insert instruct !!                 
                                    data = new byte[20];
                                    data = _mFeederData.MoveTargetPositionSendData(_mFeederData.DrvID[0], (int)Math.Round(-JobSolder.ReverseWireLength * FeederParam._dFeedermmToPulseRatio));
                                    _mFeederComm.SendData(data);
                                    data = _mFeederData.SetMoveTargetVelocity(_mFeederData.DrvID[0], (int)Math.Round(JobSolder.ReverseVelocity * FeederParam._dFeedermmToPulseRatio));
                                    _mFeederComm.SendData(data);
                                    data = _mFeederData.SetMoveTargetAccel(_mFeederData.DrvID[0], (int)Math.Round(JobSolder.ReverseAcceleration * FeederParam._dFeedermmToPulseRatio));
                                    _mFeederComm.SendData(data);
                                    data = null;
                                    FeederUsedLength += (-JobSolder.ReverseWireLength);
                                    mSolderingEngineStep = LaserSolderStepType.ReverseWire;
                                }
                                break;
                            case LaserSolderStepType.ReverseWire:

                                // Wire Solder Reverse insert instruct !!
                                data = new byte[20];
                                data = _mFeederData.MoveReleativeCommand(_mFeederData.DrvID[0]);
                                _mFeederComm.SendData(data);
                                data = null;
                                mSolderingEngineStep = LaserSolderStepType.Finish;
                                break;
                            case LaserSolderStepType.Finish:
                                _IsAutoSoldering = false;
                                _IsAutoSolderEnd = true;
                                mSolderingEngineStep = LaserSolderStepType.Idle;
                                break;
                            case LaserSolderStepType.Error:
                                break;
                            default: break;
                        }
                    }
                    catch (Exception ex)
                    {
                        ;
                    }
                }
                Thread.Sleep(EngineSleepTime);
            }            
        }
    }
}
