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
using FeederControlLibrary.SerialCommunication.Control;
using FeederControlLibrary.SerialCommunication.Data;
using FeederControlLibrary.SerialCommunication.DataProcessor;
using LogLibrary;

namespace LaserSoldering
{
    public class LaserSoderingProcess
    {
        public enum LaserSolderStepType
        {
            Stop = 0,
            Idle,
            Ready,
            ReadyWireSupport,
            PreHeat,            
            PreHeatWireSupport,
            PreHeatWireSupportWait,
            Heat,
            HeatWireSupport,
            LaserOff,            
            ReverseWire,
            PowerOff,
            PowerRatioReady,
            Finish,
            Error
        }        
        private enum LaserModule
        {
            CompactMini = 0,
            CompactSE
        }

        private const int EngineSleepTime = 47;                //11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199,479
        public RecipeManager.FeederParams _FeederParam;
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
        private FeederControlLibrary.SerialCommunication.Control.CommunicationManager _mFeederComm = null;
        private FeederControlLibrary.SerialCommunication.Data.FeedData _mFeederData = null;
        private string _TcpIpAddress = string.Format("192.168.3.11");                                           // CompactSE IP : 192.168.3.11
        private int _TcpPort = 12000;

        private bool _IsSolderingConnect = false;
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
        private bool _IsCommandFlag = false;
        private bool _IsFeederSeqReady = false;
        private double _LaserPresentCurrent = 0;
        private double _LaserPresentPower = 0;
        private double _FeederUsedLength = 0;
        private byte _LaserCookie = 0;

        private Thread ProcessEngine;
        private Thread FeederProcess;
        private Thread InitSolderingSequence;

        private LaserModule _LaserSource = LaserModule.CompactMini;

        public int[][] DrvMotionMonitor = new int[1][];
        public bool IsSolderingConnect { get { return _IsSolderingConnect; } set { _IsSolderingConnect = value; } }
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
        public bool IsFeederSeqReady { get { return _IsFeederSeqReady; } set { _IsFeederSeqReady = value; } }
        public bool IsFeederInPosition { get { return _IsFeederInPosition; } set { _IsFeederInPosition = value; } }
        public bool IsInitialSoldering { get { return _IsInitialSoldering; } set { _IsInitialSoldering = value; } }
        public bool IsCommandFlag { get { return _IsCommandFlag; } set { _IsCommandFlag = value; } }
        public string LaserIpAddress { get { return _TcpIpAddress; } set { _TcpIpAddress = value; } }
        public int LaserPort { get { return _TcpPort; } set { _TcpPort = value; } }        
        public byte LaserCookie { get { return _LaserCookie; } set { _LaserCookie = value; } }

        public CompactSECommunication.Communication.Control.CommunicationManager CompactSEComm { get { return _mCompactSEComm; } set { _mCompactSEComm = value; } }
        public CompactSECommunication.Communication.Data.UserCompactSEData CompactSEData { get { return _mCompactSEData; } set { _mCompactSEData = value; } }
        public CoherentCompactMini.SerialCommunication.Control.CommunicationManager CompactMiniComm { get { return _mCompactMiniComm; } set { _mCompactMiniComm = value; } }
        public CoherentCompactMini.SerialCommunication.Data.CompactMiniData CompactMiniData { get { return _mCompactMiniData; } set { _mCompactMiniData = value; } }
        public FeederControlLibrary.SerialCommunication.Control.CommunicationManager FeederComm { get { return _mFeederComm; } set { _mFeederComm = value; } }
        public FeederControlLibrary.SerialCommunication.Data.FeedData FeederData { get { return _mFeederData; } set { _mFeederData = value; } }
        public RecipeManager.FeederParams FeederParam { get { return _FeederParam; } set { _FeederParam = value; } }
        public LaserSolderParameter LaserSolderParam { get { return _LaserSolderParam; } set { _LaserSolderParam = value; } }

        public double LaserPresentCurrent { get { return _LaserPresentCurrent; } set { _LaserPresentCurrent = value; }}
        public double LaserPresentPower { get { return _LaserPresentPower; } set { _LaserPresentPower = value; } }
        public double FeederUsedLength { get { return _FeederUsedLength; } set { _FeederUsedLength = value; } }

        public Stopwatch CheckTackTime = new Stopwatch();
        public int mCurrentReadyTime = 0;
        public int mCurrentPreHeatTime = 0;
        public int mCurrentHeatTime = 0;
        public event Action<FeedData> ReceiveDataFeederUpdateEvent;
        public event Action<CompactMiniData> ReceiveDataLaserUpdateEvent;
        public event Action<string> LogWriteEvent;
        public ManualResetEvent _waitHandle = new ManualResetEvent(false);
        public ManualResetEvent _waitHandelFeeder = new ManualResetEvent(false);
        public LaserSoderingProcess()
        {
            _FeederParam = new RecipeManager.FeederParams();
            _LaserSolderParam = new LaserSolderParameter();
            //_mCompactSEComm = new CompactSECommunication.Communication.Control.CommunicationManager();
            //_mCompactMiniComm = new CoherentCompactMini.SerialCommunication.Control.CommunicationManager();
            //_mFeederComm = new FeederControlLibrary.SerialCommunication.Control.CommunicationManager();
            _mCompactSEData = new UserCompactSEData();
            _mCompactMiniData = new CompactMiniData();
            _mFeederData = new FeedData();
            DrvMotionMonitor[0] = new int[Enum.GetValues(typeof(FeedData.MONITOR_DATA_MAP)).Length];
            //_FeederParam.InitialParameter();
            mSolderingEngineStep = LaserSolderStepType.Stop;
            ProcessEngine = new Thread(SolderingRun);
            ProcessEngine.Start();
            FeederProcess = new Thread(FeederSequenceRun);
            FeederProcess.Start();
        }
        ~LaserSoderingProcess()
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
            FeederProcess.Abort();
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
        public void SetFeederParameter(RecipeManager.FeederParams param)
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
        public void InitialCommunication(ref CompactSECommunication.Communication.Control.CommunicationManager laser, ref FeederControlLibrary.SerialCommunication.Control.CommunicationManager feeder)
        {
            try
            {
                _mCompactSEComm = laser;
                _mCompactSEData = _mCompactSEComm.mLaserSourceData;
                _LaserSource = LaserModule.CompactSE;
                _mFeederComm = feeder;
                _mFeederData = _mFeederComm.mFeedCtrl;
            }
            catch (Exception ex)
            {
                ;
            }
        }
        public void InitialCommunication(ref CoherentCompactMini.SerialCommunication.Control.CommunicationManager laser, ref FeederControlLibrary.SerialCommunication.Control.CommunicationManager feeder)
        {
            try
            {
                _mCompactMiniComm = laser;
                _mCompactMiniData = _mCompactMiniComm.mLaserSourceCtrl;
                _LaserSource = LaserModule.CompactMini;
                _mFeederComm = feeder;
                _mFeederData = _mFeederComm.mFeedCtrl;
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
        public void SetCommunicationParam(CoherentCompactMini.SerialCommunication.Control.SerialPortSetData laser, FeederControlLibrary.SerialCommunication.Control.SerialPortSetData feeder, byte id)
        {
            try
            {
                if ((laser != null) && (feeder != null))
                {
                    _mCompactMiniComm.SetSerialData(laser);                    
                    _mFeederComm.SetSerialData(feeder);
                    _mFeederComm.InitialPeriodData(id);
                }
            }
            catch (Exception ex)
            {
                ;
            }

        }
        public void SetCommunicationParam(CompactSECommunication.Communication.Control.TcpIpSetData laser, FeederControlLibrary.SerialCommunication.Control.SerialPortSetData feeder, byte id)
        {
            try
            {
                if ((laser != null) && (feeder != null))
                {
                    _mCompactSEComm.SetTcpIpData(laser);
                    _mFeederComm.SetSerialData(feeder);
                    _mFeederComm.InitialPeriodData(id);
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
                if (!_IsLaserError && !_IsFeederError)
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
                if (!_IsLaserError && !_IsFeederError)
                {
                    _IsAutoSoldering = false;
                    _IsAutoSolderEnd = true;
                    _waitHandelFeeder.Set();
                    int datasize = 0;
                    byte[] data = null;                    
                    if (_LaserSource == LaserModule.CompactMini)
                    {
                        datasize = _mCompactMiniData.GetSetLaserOnPacketSize();
                        data = _mCompactMiniData.GetSetLaserOn(false);
                        _mCompactMiniComm.SendData(data);
                        datasize = _mCompactMiniData.GetSetPowerOnPacketSize();
                        data = _mCompactMiniData.GetSetPowerOn(false);
                        _mCompactMiniComm.SendData(data);
                        //data = null;
                    }
                    else if (_LaserSource == LaserModule.CompactSE)
                    {
                        //datasize = _mCompactSEData.GetLaserOnPacketSize(1);                                
                        //data = _mCompactSEData.GetLaserOn(1, false);
                        //_mCompactSEComm.SendData(data);
                        //data = null;
                    }
                    data = _mFeederData.MoveStopCommand(_mFeederData.DrvID[0]);
                    _mFeederComm.SendData(data);
                    _IsAutoSoldering = false;
                    _IsAutoSolderEnd = true;
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
                    IsSolderingConnect = true;
                    InitSolderingSequence = new Thread(InitialSolderProcess);
                    InitSolderingSequence.Start();
                }
                else
                    IsSolderingConnect = false;
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
                    IsSolderingConnect = false;
                }
                else
                    IsSolderingConnect = true;
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
            //if (_LaserSource == LaserModule.CompactMini)
            {
                _mCompactMiniData = update;

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

                ReceiveDataLaserUpdateEvent.Invoke(_mCompactMiniData);
            }
        }
        public void ReceiveUpdateFeederData(FeedData update)
        {
            if (_mFeederData != null)
            {
                _mFeederData = update;
                UpdateCommuncationDatas();
                ReceiveDataFeederUpdateEvent.Invoke(_mFeederData);
            }
        }
        public void UpdateCommuncationDatas()
        {
            try
            {
                //if (_IsLaserConnect)
                //{
                //    if (_LaserSource == LaserModule.CompactMini)
                //    {
                //        if (_mCompactMiniData.Status.B0)
                //            _IsLaserPowerOn = true;
                //        else
                //            _IsLaserPowerOn = false;

                //        //if (Convert.ToBoolean(mLaserData.LaserStatus.B3))
                //        if (_mCompactMiniData.Status.B1)
                //            _IsLaserOn = true;
                //        else
                //            _IsLaserOn = false;

                //        if (_mCompactMiniData.Status.B2)
                //            _IsLaserError = true;
                //        else
                //            _IsLaserError = false;

                //        if (_mCompactMiniData.Status.B3)
                //            _IsLaserFatalError = true;
                //        else
                //            _IsLaserFatalError = false;

                //        if (_mCompactMiniData.Status.B4)
                //            _IsLaserPilotOn = true;
                //        else
                //            _IsLaserPilotOn = false;
                //        _LaserPresentPower = (double)_mCompactMiniData.LaserPowerRatio;
                //    }
                //    else if (_LaserSource == LaserModule.CompactSE)
                //    {
                //        if ((_mCompactSEData.LaserStatus.Bit8 & 0x03) == 0)
                //        {
                //            // Error Status
                //            _IsLaserError = true;
                //            _IsLaserWarning = false;
                //            _IsLaserBusy = false;
                //            _IsLaserReady = false;
                //        }
                //        else if ((_mCompactSEData.LaserStatus.Bit8 & 0x03) == 1)
                //        {
                //            // Warning Status
                //            _IsLaserWarning = true;
                //            _IsLaserError = false;
                //            _IsLaserBusy = false;
                //            _IsLaserReady = false;
                //        }
                //        else if ((_mCompactSEData.LaserStatus.Bit8 & 0x03) == 2)
                //        {
                //            // Busy Status
                //            _IsLaserBusy = true;
                //            _IsLaserError = false;
                //            _IsLaserWarning = false;
                //            _IsLaserReady = false;
                //        }
                //        else if ((_mCompactSEData.LaserStatus.Bit8 & 0x03) == 3)
                //        {
                //            // Ready Status
                //            _IsLaserError = false;
                //            _IsLaserWarning = false;
                //            _IsLaserBusy = false;
                //            _IsLaserReady = true;
                //        }
                //        else
                //        {
                //            _IsLaserWarning = false;
                //            _IsLaserError = false;
                //            _IsLaserBusy = false;
                //            _IsLaserReady = false;
                //        }
                //        if (_mCompactSEData.LaserStatus.B2)
                //            _IsLaserPowerOn = true;
                //        else
                //            _IsLaserPowerOn = false;

                //        if (_mCompactSEData.LaserStatus.B3)
                //            _IsLaserOn = true;
                //        else
                //            _IsLaserOn = false;

                //        _LaserPresentPower = _mCompactSEData.ActurePower;
                //        _LaserPresentCurrent = _mCompactSEData.ActureCurrent;
                //    }
                //}
                if (_IsFeederConnect)
                {
                    int[] itempval = new int[4];
                    int datasum = 0;

                    Array.Copy(_mFeederData._mAiCMotionDatas._CurrentDatas, 0, DrvMotionMonitor[_mFeederData.DrvID.Length - 1], 0, _mFeederData._mAiCMotionDatas._CurrentDatas.Length);
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
                        datasize = _mCompactMiniData.GetSetErrorResetPacketSize();
                        data = _mCompactMiniData.GetSetErrorReset();
                        _mCompactMiniComm.SendData(data);
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
                    data = _mFeederData.AlarmResetCommand(_mFeederComm.mFeedCtrl.DrvID[0]);
                    _mFeederComm.SendData(data);
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
                            datasize = _mCompactMiniData.GetSetVT100ModePacketSize(0);
                            data = _mCompactMiniData.GetSetVT100Mode(0);
                            _mCompactMiniComm.SendData(data);
                            Thread.Sleep(100);
                            data = null;
                            /* 2. Internal control Enable */
                            datasize = _mCompactMiniData.GetSetLaserControlPacketSize();
                            data = _mCompactMiniData.GetSetLaserControl(false);
                            _mCompactMiniComm.SendData(data);
                            Thread.Sleep(100);
                            data = null;
                            /* 3. Write Current presentting Internal Enable */
                            datasize = _mCompactMiniData.GetLaserCurrentSetPacketSize();
                            data = _mCompactMiniData.GetLaserCurrentSet(false);
                            _mCompactMiniComm.SendData(data);
                            Thread.Sleep(100);
                            data = null;
                            /* 4. Write Operating Mode CW Enable */
                            datasize = _mCompactMiniData.GetSetLaserOperationModePacketSize();
                            data = _mCompactMiniData.GetSetLaserOperationMode(false);
                            _mCompactMiniComm.SendData(data);
                            Thread.Sleep(100);
                            data = null;
                        }
                    }
                    if (_IsFeederConnect)
                    {
                        data = new byte[100];
                        data = _mFeederData.DriveInitialSetting(_mFeederComm.mFeedCtrl.DrvID[0], 100, (int)Math.Round(30D * _FeederParam.FeedermmToPulseRatio), 50, 50);
                        _mFeederComm.SendData(data);
                        data = _mFeederData.SetMoveTargetAccel(_mFeederData.DrvID[0], (int)1000);
                        _mFeederComm.SendData(data);
                    }
                    _mCompactMiniComm.InitialPeriodReqStatus();
                    _IsInitialSoldering = true;
                    StartEngine();
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }
        private void FeederSequenceRun()
        {
            // _waitHandelFeeder
            try
            {
                while (true)
                {
                    if (_IsAutoSoldering)
                    {
                        int datasize = 0;
                        byte[] data = null;
                        LaserSolderParameter JobSolder = new LaserSolderParameter();
                        JobSolder = _LaserSolderParam;
                        LogWriteEvent?.Invoke(string.Format("실납 공급 시퀀스 시작."));
                        data = _mFeederData.SetMoveTargetVelocity(_mFeederData.DrvID[0], (int)Math.Round(JobSolder.ForwordingVelocity * FeederParam.FeedermmToPulseRatio, 1));
                        _mFeederComm.SendData(data);
                        Thread.Sleep(50);
                        data = null;
                        data = _mFeederData.SetMoveTargetAccel(_mFeederData.DrvID[0], (int)Math.Round(JobSolder.ForwordingAcceleration * FeederParam.FeedermmToPulseRatio, 1));
                        _mFeederComm.SendData(data);
                        Thread.Sleep(50);
                        _IsFeederSeqReady = true;
                        _waitHandelFeeder.Reset();
                        _waitHandelFeeder.WaitOne();
                        data = null;
                        data = _mFeederData.MoveTargetPositionSendData(_mFeederData.DrvID[0], (int)(JobSolder.ForwordingWireLength * FeederParam.FeedermmToPulseRatio));
                        _mFeederComm.SendData(data);
                        Thread.Sleep(100);
                        _waitHandelFeeder.Reset();                        
                        _waitHandelFeeder.WaitOne(JobSolder.ReadyTime + 5000);
                        LogWriteEvent?.Invoke(string.Format("실납 공급 속도,길이,가속도 설정 완료."));
                        Thread.Sleep(100);
                        _waitHandelFeeder.Reset();
                        _waitHandelFeeder.WaitOne(JobSolder.PreHeatTime + 5000);
                        //_waitHandelFeeder.WaitOne();
                        data = null;
                        data = _mFeederData.MoveReleativeCommand(_mFeederData.DrvID[0]);
                        _mFeederComm.SendData(data);
                        LogWriteEvent?.Invoke(string.Format("실납 공급 시작."));
                        Thread.Sleep(500);
                        while (!_IsFeederInPosition) ;
                        //_waitHandelFeeder.Reset();
                        //_waitHandelFeeder.WaitOne(JobSolder.HeatTime / 2);
                        data = null;
                        data = _mFeederData.MoveTargetPositionSendData(_mFeederData.DrvID[0], (int)(-JobSolder.ReverseWireLength * FeederParam.FeedermmToPulseRatio));
                        _mFeederComm.SendData(data);
                        //Thread.Sleep(100);
                        //Thread.Sleep(50);
                        //data = _mFeederData.SetMoveTargetVelocity(_mFeederData.DrvID[0], (int)Math.Round(JobSolder.ReverseVelocity * FeederParam.FeedermmToPulseRatio,1));
                        //_mFeederComm.SendData(data);
                        LogWriteEvent?.Invoke(string.Format("실납 회수 속도,길이 설정 완료."));
                        _waitHandelFeeder.Reset();
                        _waitHandelFeeder.WaitOne(JobSolder.HeatTime + 5000);
                        //_waitHandelFeeder.WaitOne();
                        data = null;
                        data = _mFeederData.MoveReleativeCommand(_mFeederData.DrvID[0]);
                        _mFeederComm.SendData(data);
                        LogWriteEvent?.Invoke(string.Format("실납 회수 시작."));
                        Thread.Sleep(200);
                        _waitHandelFeeder.Reset();
                        _waitHandelFeeder.WaitOne(5000);
                        LogWriteEvent?.Invoke(string.Format("실납 공급 시퀀스 종료."));
                        _IsFeederSeqReady = false;
                        data = null;
                        data = _mFeederData.MoveTargetPositionSendData(_mFeederData.DrvID[0], (int)(JobSolder.ForwordingWireLength * FeederParam.FeedermmToPulseRatio));
                        _mFeederComm.SendData(data);
                        Thread.Sleep(100);
                    }
                    else
                    {
                        _IsFeederSeqReady = false;
                    }
                    Thread.Sleep(100);
                }                
            }
            catch (Exception ex)
            {
                ;
            }
        }
        private async void SolderingRun()
        {
            int datasize = 0;
            byte[] data = null;
            LaserSolderParameter JobSolder = new LaserSolderParameter();            
            while (true)
            {
                //if (_IsSolderingConnect)
                {
                    try
                    {
                        TimeSpan ts = CheckTackTime.Elapsed;
                        if (_IsLaserError || _IsFeederError)
                        {
                            if (_LaserSource == LaserModule.CompactMini)
                            {
                                //datasize = _mCompactMiniData.GetSetPowerOnPacketSize();
                                //data = _mCompactMiniData.GetSetPowerOn(true);
                                //_mCompactMiniComm.SendData(data);
                                data = null;
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
                            _waitHandelFeeder.Set();
                        }
                        else
                        {
                            if (!_IsAutoSoldering)
                            {
                                _IsAutoSolderEnd = true;
                            }
                        }
                        switch (mSolderingEngineStep)
                        {
                            case LaserSolderStepType.Stop:
                                break;
                            case LaserSolderStepType.Idle:
                                if (_IsAutoSoldering)
                                {
                                    JobSolder = _LaserSolderParam;

                                    data = new byte[20];
                                    //datasize = _mCompactMiniData.GetSetPowerOnPacketSize();
                                    //data = _mCompactMiniData.GetSetPowerOn(true);
                                    //_mCompactMiniComm.SendData(data);
                                    //datasize = _mCompactMiniData.GetSetLaserPowerPacketSize(JobSolder.PreheatPowerRatio);                                        
                                    //data = _mCompactMiniData.GetSetLaserPower(JobSolder.PreheatPowerRatio);
                                    //_mCompactMiniComm.SendData(data);                                    
                                    
                                    //data = _mFeederData.SetMoveTargetVelocity(_mFeederData.DrvID[0], (int)Math.Round(JobSolder.ForwordingVelocity * FeederParam.FeedermmToPulseRatio));
                                    //_mFeederComm.SendData(data);
                                    //data = _mFeederData.SetMoveTargetAccel(_mFeederData.DrvID[0], (int)Math.Round(JobSolder.ForwordingAcceleration * FeederParam.FeedermmToPulseRatio));
                                    //_mFeederComm.SendData(data);
                                    _IsCommandFlag = false;
                                    FeederUsedLength += JobSolder.ForwordingWireLength;
                                    mCurrentHeatTime = 0;
                                    mCurrentPreHeatTime = 0;
                                    mCurrentReadyTime = 0;
                                    mSolderingEngineStep = LaserSolderStepType.Ready;
                                    LogWriteEvent?.Invoke(string.Format("레이저 솔더링 시작."));
                                }
                                break;
                            //case LaserSolderStepType.ReadyWireSupport:
                            //    data = new byte[20];
                            //    data = _mFeederData.MoveReleativeCommand(_mFeederData.DrvID[0]);
                            //    _mFeederComm.SendData(data);
                            //    mSolderingEngineStep = LaserSolderStepType.Ready;
                            //    LogWriteEvent?.Invoke(string.Format("레이저 솔더링 시작."));
                            //    break;
                            case LaserSolderStepType.Ready:
                                if (_IsFeederSeqReady)
                                {
                                    CheckTackTime.Reset();
                                    _waitHandelFeeder.Set();
                                    data = new byte[20];
                                    datasize = _mCompactMiniData.GetSetPowerOnPacketSize();
                                    data = _mCompactMiniData.GetSetPowerOn(true);
                                    _mCompactMiniComm.SendData(data);
                                    datasize = _mCompactMiniData.GetSetLaserPowerPacketSize(JobSolder.PreheatPowerRatio);
                                    data = _mCompactMiniData.GetSetLaserPower(JobSolder.PreheatPowerRatio);
                                    _mCompactMiniComm.SendData(data);                                    
                                    // Insert Laser Gate On I/O Command !!
                                    CheckTackTime.Start();
                                    mSolderingEngineStep = LaserSolderStepType.PreHeat;
                                    LogWriteEvent?.Invoke(string.Format("레이저 솔더링 예열 파워량 {0}.",JobSolder.PreheatPowerRatio));
                                }
                                break;
                            case LaserSolderStepType.PreHeat:
                                if (((int)ts.TotalMilliseconds) > JobSolder.ReadyTime)
                                {
                                    mCurrentReadyTime = (int)ts.TotalMilliseconds;
                                    CheckTackTime.Reset();
                                    _waitHandelFeeder.Set();
                                    data = new byte[20];
                                    //data = _mFeederData.MoveReleativeCommand(_mFeederData.DrvID[0]);
                                    //_mFeederComm.SendData(data);
                                    datasize = _mCompactMiniData.GetSetLaserOnPacketSize();
                                    data = _mCompactMiniData.GetSetLaserOn(true);
                                    _mCompactMiniComm.SendData(data);
                                    //data = _mFeederData.MoveTargetPositionSendData(_mFeederData.DrvID[0], (int)Math.Round(JobSolder.ForwordingWireLength * FeederParam.FeedermmToPulseRatio));
                                    //_mFeederComm.SendData(data);
                                    
                                    CheckTackTime.Start();
                                    mSolderingEngineStep = LaserSolderStepType.PreHeatWireSupport;
                                    LogWriteEvent?.Invoke(string.Format("레이저 솔더링 준비시간 설정{0}, 실행{1}.",JobSolder.ReadyTime, mCurrentReadyTime));
                                }
                                break;
                            case LaserSolderStepType.PreHeatWireSupport:
                                if (((int)ts.TotalMilliseconds) > JobSolder.PreHeatTime)
                                {
                                    mCurrentPreHeatTime = (int)ts.TotalMilliseconds;                                                 
                                    //data = new byte[20];                                    
                                    //data = _mFeederData.MoveReleativeCommand(_mFeederData.DrvID[0]);
                                    //_mFeederComm.SendData(data);          
                                    _waitHandelFeeder.Set();
                                    CheckTackTime.Reset();
                                    mSolderingEngineStep = LaserSolderStepType.Heat;
                                    LogWriteEvent?.Invoke(string.Format("레이저 솔더링 예열시간 설정{0}, 실행{1}.", JobSolder.PreHeatTime, mCurrentPreHeatTime));
                                }
                                break;
                            case LaserSolderStepType.Heat:
                                data = new byte[20];
                                datasize = _mCompactMiniData.GetSetLaserPowerPacketSize(JobSolder.HeatPowerRatio);                                
                                data = _mCompactMiniData.GetSetLaserPower(JobSolder.HeatPowerRatio);
                                _mCompactMiniComm.SendData(data);                                                                     
                                CheckTackTime.Start();
                                mSolderingEngineStep = LaserSolderStepType.HeatWireSupport;
                                LogWriteEvent?.Invoke(string.Format("레이저 솔더링 납땜 파워량 {0}.", JobSolder.HeatPowerRatio));
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
                                    //data = new byte[20];
                                    //datasize = _mCompactMiniData.GetSetLaserOnPacketSize();
                                    //data = _mCompactMiniData.GetSetLaserOn(false);
                                    //_mCompactMiniComm.SendData(data);
                                    //datasize = _mCompactMiniData.GetSetPowerOnPacketSize();
                                    //data = _mCompactMiniData.GetSetPowerOn(false);
                                    //_mCompactMiniComm.SendData(data);                                    
                                    //data = _mFeederData.MoveTargetPositionSendData(_mFeederData.DrvID[0], (int)Math.Round(-JobSolder.ReverseWireLength * FeederParam.FeedermmToPulseRatio));
                                    //_mFeederComm.SendData(data);
                                    //data = _mFeederData.MoveReleativeCommand(_mFeederData.DrvID[0]);
                                    //_mFeederComm.SendData(data);                                    
                                    CheckTackTime.Start();
                                    _IsCommandFlag = false;
                                    mSolderingEngineStep = LaserSolderStepType.ReverseWire;
                                    LogWriteEvent?.Invoke(string.Format("레이저 솔더링 납땜 시간 설정{0}, 실행{1}.", JobSolder.HeatTime, mCurrentHeatTime));
                                }
                                else if (((int)ts.TotalMilliseconds) > (JobSolder.HeatTime - 200))
                                {
                                    _waitHandelFeeder.Set();
                                }
                                    //else if (_IsCommandFlag == false)
                                    //{
                                    //    data = new byte[20];                                    
                                    //    data = _mFeederData.SetMoveTargetVelocity(_mFeederData.DrvID[0], (int)Math.Round(JobSolder.ReverseVelocity * FeederParam.FeedermmToPulseRatio));
                                    //    _mFeederComm.SendData(data);
                                    //    _IsCommandFlag = true;
                                    //}

                                    break;
                            case LaserSolderStepType.ReverseWire:
                                if (((int)ts.TotalMilliseconds) >= 300)
                                {
                                    data = new byte[20];
                                    datasize = _mCompactMiniData.GetSetPowerOnPacketSize();
                                    data = _mCompactMiniData.GetSetPowerOn(false);
                                    //datasize = _mCompactMiniData.GetSetLaserOnPacketSize();
                                    //data = _mCompactMiniData.GetSetLaserOn(false);
                                    _mCompactMiniComm.SendData(data);
                                    CheckTackTime.Reset();                                    
                                    mSolderingEngineStep = LaserSolderStepType.PowerOff;
                                    LogWriteEvent?.Invoke(string.Format("레이저 솔더링 레이저 Off 지연 시간{0}.", (int)ts.TotalMilliseconds));
                                }
                                //else if (_IsCommandFlag == false)
                                //{
                                //    //if (_LaserSource == LaserModule.CompactMini)
                                //    //{
                                //    //    datasize = _mCompactMiniData.GetSetLaserOnPacketSize();
                                //    //    data = _mCompactMiniData.GetSetLaserOn(false);
                                //    //    _mCompactMiniComm.SendData(data);
                                //    //    datasize = _mCompactMiniData.GetSetPowerOnPacketSize();
                                //    //    data = _mCompactMiniData.GetSetPowerOn(false);
                                //    //    _mCompactMiniComm.SendData(data);
                                //    //    _IsCommandFlag = true;
                                //    //}

                                //    //data = new byte[20];
                                //    //data = _mFeederData.MoveReleativeCommand(_mFeederData.DrvID[0]);
                                //    //_mFeederComm.SendData(data);


                                //    _IsCommandFlag = true;                                                                    
                                //}
                                break;
                            case LaserSolderStepType.PowerOff:
                                data = new byte[20];
                                datasize = _mCompactMiniData.GetSetLaserOnPacketSize();
                                data = _mCompactMiniData.GetSetLaserOn(false);
                                _mCompactMiniComm.SendData(data);
                                CheckTackTime.Start();
                                mSolderingEngineStep = LaserSolderStepType.Finish;
                                break;
                            case LaserSolderStepType.Finish:
                                // Insert Laser Gate Off I/O Command !!
                                if (((int)ts.TotalMilliseconds) >= 1000)
                                //if (_IsFeederInPosition)
                                {
                                    CheckTackTime.Reset();
                                    _IsAutoSoldering = false;
                                    _IsAutoSolderEnd = true;
                                    _waitHandelFeeder.Set();                                    
                                    _IsCommandFlag = false;
                                    data = new byte[20];
                                    datasize = _mCompactMiniData.GetSetLaserPowerPacketSize(200);
                                    data = _mCompactMiniData.GetSetLaserPower(200);
                                    _mCompactMiniComm.SendData(data);                                    
                                    mSolderingEngineStep = LaserSolderStepType.Idle;
                                    LogWriteEvent?.Invoke(string.Format("레이저 솔더링 종료."));
                                    _waitHandle.Set();
                                    
                                }
                                //_IsCommandFlag = false;
                                //mSolderingEngineStep = LaserSolderStepType.Idle;
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
                await Task.Delay(EngineSleepTime);
                //Thread.Sleep(EngineSleepTime);
            }            
        }
    }
}
