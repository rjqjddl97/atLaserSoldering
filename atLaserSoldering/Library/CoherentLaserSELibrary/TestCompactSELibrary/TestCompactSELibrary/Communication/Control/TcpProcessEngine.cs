using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CompactSECommunication.Communication.Data;
using CompactSECommunication.Communication.Control;
using CompactSECommunication.Communication.DataProcessor;
using System.Collections;

namespace CompactSECommunication.Communication.Control
{
    public class TcpProcessEngine
    {
        public const int COMMUNICATE_INTERVAL = 100;
        private const int ReceiveBuffSize = 4096;
        public enum TcpEngineStep
        {
            Idle,
            RequestPeriodData
        };
        public enum TcpReceiveStep
        {
            Idle,
            Start,
            End
        };
        private TcpEngineStep mTcpEngineStep;
        private string TargetIPAddress = "127.0.0.1";
        private Queue<Tuple<string, uint>> mWriteToServerDataQueue;        
        private Queue<byte[]> mReadFromServerDataQueue;
        
        private Thread engine;
        private List<byte[]> mContinuousCheckList = new List<byte[]>();
        private Queue<byte[]> mCommandList = new Queue<byte[]>();
        private Queue<byte[]> mDataTransferList = new Queue<byte[]>();
        private byte[] ReceivePacketBuff = new byte[ReceiveBuffSize];
        private bool IsDequeueData = false;
        private bool IsEnqueueData = false;
        private double CurrentcountForDataTransfer = 0;
        private double MaximumCountForDataTransfer = 0;        

        private int SendNothingCounter = 0;
        private bool ContinuousWorking = false;
        public TcpIpHandler mTcpHandler = null;
        public UserCompactSEData mLaserData = null;
        public event Action<UserCompactSEData> ReceiveDataCompactSE;
        
        //public SimpleTCP mtcpiphan;
        public delegate void RequestData(byte[] buffer, int offset, int count);
        public event RequestData RequestDataEventHandler;
        public bool IsReceiveStart { get; set; }
        public bool IsConnected { get; set; }
        public bool IsResponseReceiveError { get; set; } = false;
        public bool IsReceiveAck { get; set; } = true;

        public UInt32 uiReceiveCount { get; set; } = 0;        
        private void ThreadRun()
        {
            while (ContinuousWorking)
            {    
                Thread.Sleep(COMMUNICATE_INTERVAL);
            }
        }
        public TcpProcessEngine()
        {            
            mWriteToServerDataQueue = new Queue<Tuple<string, uint>>();            
            mReadFromServerDataQueue = new Queue<byte[]>();
            IsConnected = false;
            mTcpHandler = new TcpIpHandler();
            mLaserData = new UserCompactSEData();
            mTcpEngineStep = TcpEngineStep.Idle;            
            Array.Clear(ReceivePacketBuff, 0x00, ReceiveBuffSize);
            InitCheckDatas();
            engine = new Thread(Run);
            engine.Start();
            
        }

        ~TcpProcessEngine()
        {
            engine.Abort();            
        }
        private void InitCheckDatas()
        {
            /* 주기적인 요청 데이터가 필요한 경우 요청 명령어 추가 구문 */
            //Get Info.
            //mContinuousCheckList.Add(new CodesysDataHandler().GetCommand(UserCodesysData.Protocol_MSG.MSG_GET_INFO)[0]);            
        }
        public void PauseEngine()
        {
            mTcpEngineStep = TcpEngineStep.Idle;
        }

        public void StartEngine()
        {
            mTcpEngineStep = TcpEngineStep.RequestPeriodData;
        }
        public void StopEngine()
        {
            engine.Abort();
        }
        public void InitialBuff()
        {
            Array.Clear(ReceivePacketBuff, 0, ReceiveBuffSize);
            uiReceiveCount = 0;            
        }
        public void ParsingData(byte[] data)
        {
            try
            {
                UserCompactSEData.RequestCommand reCommMessage = new UserCompactSEData.RequestCommand();
                reCommMessage = mLaserData.GetRequestedCommand();
                int length = data.Length;
                int index = 0;
                int startindex = 0;
                int endindex = 0;
                int sublength = 0;
                int recookie = 0;
                byte[] subdata = null;
                for (int i = 0; i < length; i++)
                {                    
                    if (data[i] == 0x20)
                    {
                        if (index == 0)
                        {
                            recookie = (int)(data[endindex] - '0');
                        }   
                        index++;
                        startindex = i+1;
                    }
                    else if (data[i] == 0x0d)
                    {                        
                        subdata = new byte[endindex - startindex + 1];
                        if ((startindex == 0) && (endindex == 0))                            
                            recookie = (int)(data[startindex] - '0');
                        else
                            Buffer.BlockCopy(data, startindex, subdata, 0, endindex - startindex + 1);                        
                        break;
                    }
                    else
                    {
                        endindex = i;
                    }
                }
                
                if ((recookie == reCommMessage.cookie) && (reCommMessage != null))
                {
                    if (reCommMessage.command == UserCompactSEData.CommandMessage.LOGIN)
                    {
                        ;
                    }
                    else if (reCommMessage.command == UserCompactSEData.CommandMessage.LOGOUT)
                    {
                        ;
                    }
                    else if ((reCommMessage.command == UserCompactSEData.CommandMessage.GET_LASERON) || (reCommMessage.command == UserCompactSEData.CommandMessage.GET_PILOTON) || (reCommMessage.command == UserCompactSEData.CommandMessage.GET_POWERON))
                    {
                        if ((subdata != null) && (subdata.Length == 1))
                        {
                            if (reCommMessage.command == UserCompactSEData.CommandMessage.GET_LASERON)
                                mLaserData.ReceiveSetReadLaserOn(Encoding.Default.GetString(subdata));
                            else if (reCommMessage.command == UserCompactSEData.CommandMessage.GET_PILOTON)
                                mLaserData.ReceiveSetReadPilotOn(Encoding.Default.GetString(subdata));
                            else 
                                mLaserData.ReceiveSetReadPowerOn(Encoding.Default.GetString(subdata));
                        }                            
                    }
                    else if (reCommMessage.command == UserCompactSEData.CommandMessage.GET_USEROUTPUT)
                    {
                        if(subdata != null)
                            mLaserData.ReceiveSetUserOutput(Encoding.Default.GetString(subdata));
                    }
                    else if (reCommMessage.command == UserCompactSEData.CommandMessage.GET_ACT_CURRENT)
                    {
                        if (subdata != null)
                            mLaserData.ReceiveSetActCurrnet(Encoding.Default.GetString(subdata));
                    }
                    else if (reCommMessage.command == UserCompactSEData.CommandMessage.GET_ACT_POWER)
                    {
                        if (subdata != null)
                            mLaserData.ReceiveSetActPower(Encoding.Default.GetString(subdata));
                    }
                    else if (reCommMessage.command == UserCompactSEData.CommandMessage.GET_SETCURRENT)
                    {
                        if (subdata != null)
                            mLaserData.ReceiveSetTargetCurrent(Encoding.Default.GetString(subdata));
                    }
                    else if (reCommMessage.command == UserCompactSEData.CommandMessage.GET_SETPOWER)
                    {
                        if (subdata != null)
                            mLaserData.ReceiveSetTargetPower(Encoding.Default.GetString(subdata));
                    }
                    else if (reCommMessage.command == UserCompactSEData.CommandMessage.GET_WATCHDOGTIMEOUT)
                    {
                        if (subdata != null)
                            mLaserData.ReceiveSetWatchdogTimeout(Encoding.Default.GetString(subdata));
                    }
                    else if (reCommMessage.command == UserCompactSEData.CommandMessage.GET_SW_VERSION)
                    {
                        if (subdata != null)
                            mLaserData.ReceiveSetSoftwareVersion(Encoding.Default.GetString(subdata));
                    }
                    else if (reCommMessage.command == UserCompactSEData.CommandMessage.GET_SERIAL)
                    {
                        if (subdata != null)
                            mLaserData.ReceiveSetSerialNumber(Encoding.Default.GetString(subdata));
                    }
                    else if ((reCommMessage.command == UserCompactSEData.CommandMessage.SET_LASERON) || (reCommMessage.command == UserCompactSEData.CommandMessage.SET_PILOTON) || (reCommMessage.command == UserCompactSEData.CommandMessage.SET_POWERON)
                            || (reCommMessage.command == UserCompactSEData.CommandMessage.SET_RESET) || (reCommMessage.command == UserCompactSEData.CommandMessage.SET_CURRENT) || (reCommMessage.command == UserCompactSEData.CommandMessage.SET_POWER)
                            || (reCommMessage.command == UserCompactSEData.CommandMessage.SET_WATCHDOGTIMEOUT)
                            )
                    {
                        ;
                    }
                    ReceiveDataCompactSE.Invoke(mLaserData);
                }                                
            }
            catch (Exception ex)
            {
                ;
            }
        }
        public void SendCommand(byte[] data)
        {
            if (IsConnected)
            {
                //byte[][] SeData = mUserCodesysDataHandler.GetCommand(msg, Encoding.Default.GetString(data), "");                
                if (data == null)
                    return;
                
                mDataTransferList.Enqueue(data);                
            }            
        }
        public void SendData(byte[] data)
        {
            if (IsConnected)
            {
                if (IsDequeueData || (mDataTransferList.Count != 0))
                {
                    //Data is downloading please try again it later.
                    return;
                }
                IsEnqueueData = true;
                //byte[][] SeData = mUserCodesysDataHandler.GetCommand(msg, Encoding.Default.GetString(data), "");                

                if (data == null)
                    return;

                mDataTransferList.Enqueue(data);

                if (mDataTransferList.Count != 0)
                {
                    CurrentcountForDataTransfer = 0;
                    MaximumCountForDataTransfer = mDataTransferList.Count;                    
                }
                else
                {
                    mDataTransferList.Clear();
                }

                IsEnqueueData = false;
            }
            
        }
        public void ReceivePacket()
        {
            int buffsize = mTcpHandler._ReceiveDataQueue.Count;
            if (buffsize != 0)
            {                
                byte[] recvData = mTcpHandler._ReceiveDataQueue.Dequeue();                
                //IsReceiveStart = true;                
                for (int i = 0; i < recvData.Length; i++)
                {
                    if (i >= 1)
                    {
                        if ((recvData[i - 1] == 0x0D) && (recvData[i - 0] == 0x0A) && (IsReceiveStart))
                        {
                            if (!IsResponseReceiveError)
                            {
                                //ReceivePacketBuff[uiReceiveCount - 1] = 0;
                                byte[] buff = new byte[uiReceiveCount - 1];
                                //Array.Copy(ReceivePacketBuff, buff, uiReceiveCount - 1);
                                Buffer.BlockCopy(ReceivePacketBuff, 1, buff, 0, (int)(uiReceiveCount - 1));
                                ParsingData(buff);
                                IsReceiveAck = true;
                            }
                            IsReceiveStart = false;
                            uiReceiveCount = 0;
                        }
                        else if ((recvData[i - 1] == 'r') && (recvData[i - 0] == 'v') && (IsReceiveStart == false))
                        {
                            IsReceiveStart = true;
                            IsResponseReceiveError = false;
                            uiReceiveCount = 0;
                        }
                        else if ((recvData[i - 1] == 'e') && (recvData[i - 0] == 'x') && (IsReceiveStart == false))
                        {
                            IsReceiveStart = true;
                            IsResponseReceiveError = true;
                            uiReceiveCount = 0;
                        }
                        else
                        {                            
                            ReceivePacketBuff[uiReceiveCount++] = recvData[i];
                        }
                    }
                }
            }
        }
        private void Run()
        {
            byte[] data = null;
            int mContinuousCheckIndex = 0;
            while (true)
            {
                try
                {
                    IsConnected = mTcpHandler.isConnected;
                    if (!IsConnected)
                    {
                        mCommandList.Clear();
                        mDataTransferList.Clear();
                        IsEnqueueData = IsDequeueData = false;
                        IsReceiveAck = true;
                        InitialBuff();
                        mTcpHandler._ReceiveDataQueue.Clear();
                        uiReceiveCount = 0;
                        IsReceiveStart = false;
                        Thread.Sleep(COMMUNICATE_INTERVAL);
                        continue;
                    }

                    if (IsEnqueueData && IsDequeueData)
                    {
                        mDataTransferList.Clear();
                        IsEnqueueData = IsDequeueData = false;
                    }
                    // receive Data Packet Parsor 구문 추가 필요.
                    if (mTcpHandler._ReceiveDataQueue.Count > 0)
                    {
                        ReceivePacket();
                    }
                    /////////////////////////////////////////////
                    switch (mTcpEngineStep)
                    {
                        case TcpEngineStep.Idle:
                            //Do nothing
                            mCommandList.Clear();
                            mDataTransferList.Clear();
                            IsReceiveAck = true;
                            uiReceiveCount = 0;
                            break;

                        case TcpEngineStep.RequestPeriodData:
                            if (IsReceiveAck)
                            {
                                if ((mDataTransferList.Count != 0) && !IsEnqueueData)
                                {
                                    IsDequeueData = true;
                                    data = mDataTransferList.Dequeue();
                                    if (data != null)
                                    {
                                        CurrentcountForDataTransfer++;
                                        if (CurrentcountForDataTransfer >= MaximumCountForDataTransfer)
                                        {
                                            CurrentcountForDataTransfer = MaximumCountForDataTransfer = 0;
                                        }
                                    }
                                    if (mDataTransferList.Count == 0)
                                    {
                                        IsDequeueData = false;
                                    }
                                }
                                else if (mCommandList.Count != 0)
                                {
                                    data = mCommandList.Dequeue();
                                }
                                else if (mContinuousCheckList.Count != 0)
                                {
                                    data = mContinuousCheckList.ElementAt(mContinuousCheckIndex++);
                                    if (mContinuousCheckIndex >= mContinuousCheckList.Count)
                                        mContinuousCheckIndex = 0;
                                }
                            }
                            break;
                        default:
                            //Do some error action.
                            break;
                    }

                    if ((data != null) && (mTcpEngineStep != TcpEngineStep.Idle))
                    {
                        RequestDataEventHandler?.Invoke(data, 0, data.Length);
                        IsReceiveAck = false;
                        data = null;
                    }
                }
                catch (Exception)
                {
                    //Log.LogManager.AddSystemLog(Log.Log.LogType.Error, "CommunicateEngine::Run -> Fail to working.");
                }
                Thread.Sleep(COMMUNICATE_INTERVAL);
            }
        }
    }
}
