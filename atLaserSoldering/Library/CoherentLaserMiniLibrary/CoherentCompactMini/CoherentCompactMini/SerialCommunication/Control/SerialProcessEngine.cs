using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CoherentCompactMini.SerialCommunication.Data;
using CoherentCompactMini.SerialCommunication.DataProcessor;
using CoherentCompactMini.SerialCommunication.Control;
using System.Collections;

namespace CoherentCompactMini.SerialCommunication.Control
{
    public class SerialProcessEngine
    {
        public enum SerialEngineStep
        {
            Idle,
            RequestPeriodData
        };
        public enum SerialReceiveStep
        {
            Idle,
            Start,
            End
        };
        private Thread engine;

        private const int EngineSleepTime = 200;
        private const int ReceiveBuffSize = 4096;
        private SerialEngineStep mSerialEngineStep;
        private List<byte[]> mContinuousCheckList = new List<byte[]>();
        private Queue<byte[]> mCommandList = new Queue<byte[]>();
        private Queue<byte[]> mDataTransferList = new Queue<byte[]>();
        private double CurrentcountForDataTransfer = 0;
        private double MaximumCountForDataTransfer = 0;        
        public Queue<byte[]> mSendCommand = new Queue<byte[]>();
        private bool IsDequeueData = false;                   
        private bool IsEnqueueData = false;
        private SerialReceiveStep mReceiveStep;

        private byte[] ReceivePacketBuff = new byte[ReceiveBuffSize];
        //private int ReceiveCountIndex;
        public SerialHandler m_SerialHandler;
        public CompactMiniData m_CompactMiniDataCtrl;        
        //public event Action<byte[]> ReceivedDataEvent;
        //public event Action<byte[]> ParsedDataReceivedEvent;
        public event Action<CompactMiniData> ReceiveCompactMiniData;

        public delegate void RequestData(byte[] buffer, int offset, int count);        
        public event RequestData RequestDataEventHandler;
        public bool IsReceiveStart { get; set; }
        public bool IsResponseReceiveError { get; set; } = false;
        public bool IsReceiveAck { get; set; } = true;
        public bool IsConnected { get; set; }
        public UInt32 uiReceiveCount { get; set; } = 0;
        public SerialProcessEngine()
        {
            IsConnected = false;
            m_SerialHandler = new SerialHandler();
            m_CompactMiniDataCtrl = new CompactMiniData();
            mSerialEngineStep = SerialEngineStep.Idle;
            mReceiveStep = SerialReceiveStep.Idle;            
            InitCheckDatas();
            Array.Clear(ReceivePacketBuff, 0x00, ReceiveBuffSize);            
            engine = new Thread(Run);
            engine.Start();
        }
        ~SerialProcessEngine()
        {
            engine.Abort();
        }

        public void PauseEngine()
        {
            mSerialEngineStep = SerialEngineStep.Idle;
        }

        public void StartEngine()
        {
            mSerialEngineStep = SerialEngineStep.RequestPeriodData;
        }
        public void StopEngine()
        {
            engine.Abort();            
        }
        public void SwitchMode(SerialEngineStep mMode)
        {
            if (mMode != SerialEngineStep.Idle)
                mSerialEngineStep = mMode;
        }
        private void InitCheckDatas()
        {

            //Laser Status. 주기적 요청.
            //mContinuousCheckList.Add(m_CompactMiniDataCtrl.GetLaserStatus());
        }
        public void ParsingData(byte[] data)
        {
            try
            {
                CompactMiniData.CommandMessage reCommMessage = new CompactMiniData.CommandMessage();
                reCommMessage = m_CompactMiniDataCtrl.GetRequestedCommand();
                if ((reCommMessage == CompactMiniData.CommandMessage.PWC) || (reCommMessage == CompactMiniData.CommandMessage.PWH) || (reCommMessage == CompactMiniData.CommandMessage.PWL) ||
                     (reCommMessage == CompactMiniData.CommandMessage.STI) || (reCommMessage == CompactMiniData.CommandMessage.STL) || (reCommMessage == CompactMiniData.CommandMessage.STO) ||
                     (reCommMessage == CompactMiniData.CommandMessage.STR) || (reCommMessage == CompactMiniData.CommandMessage.STP) || (reCommMessage == CompactMiniData.CommandMessage.WRMC) ||
                     (reCommMessage == CompactMiniData.CommandMessage.WRML) || (reCommMessage == CompactMiniData.CommandMessage.WRMT) || (reCommMessage == CompactMiniData.CommandMessage.WRS) ||
                     (reCommMessage == CompactMiniData.CommandMessage.WRV) || (reCommMessage == CompactMiniData.CommandMessage.WRW)
                   )
                {
                    string strResponse = string.Empty;
                    strResponse = Encoding.Default.GetString(data);
                    if (strResponse.Equals("OK"))
                    {
                        ;
                    }
                    else if (strResponse.Equals("ERROR"))
                    {
                        ;
                    }
                    else
                        ;
                }
                else if (reCommMessage == CompactMiniData.CommandMessage.PRC)
                {
                    m_CompactMiniDataCtrl.ReceiveSetPulseCount(Encoding.Default.GetString(data));
                }
                else if (reCommMessage == CompactMiniData.CommandMessage.PRH)
                {
                    m_CompactMiniDataCtrl.ReceiveSetPulseHighTime(Encoding.Default.GetString(data));
                }
                else if (reCommMessage == CompactMiniData.CommandMessage.PRL)
                {
                    m_CompactMiniDataCtrl.ReceiveSetPulseLowTime(Encoding.Default.GetString(data));
                }
                else if (reCommMessage == CompactMiniData.CommandMessage.RDE)
                {
                    m_CompactMiniDataCtrl.ReceiveSetErrorStatus(Encoding.Default.GetString(data));
                }
                else if (reCommMessage == CompactMiniData.CommandMessage.RDH)
                {
                    m_CompactMiniDataCtrl.ReceiveSetLaserDiodeTemperature(Encoding.Default.GetString(data));
                }
                else if (reCommMessage == CompactMiniData.CommandMessage.RDK)
                {
                    m_CompactMiniDataCtrl.ReceiveSetInternalPower(Encoding.Default.GetString(data));
                }
                else if (reCommMessage == CompactMiniData.CommandMessage.RDM)
                {
                    m_CompactMiniDataCtrl.ReceiveSetOperationMode(Encoding.Default.GetString(data));
                }
                else if (reCommMessage == CompactMiniData.CommandMessage.RDN)
                {
                    m_CompactMiniDataCtrl.ReceiveSetSerialNumber(Encoding.Default.GetString(data));
                }
                else if (reCommMessage == CompactMiniData.CommandMessage.RDO)
                {
                    m_CompactMiniDataCtrl.ReceiveSetLaserPower(Encoding.Default.GetString(data));
                }
                else if (reCommMessage == CompactMiniData.CommandMessage.RDQ)
                {
                    m_CompactMiniDataCtrl.ReceiveSetFanTemperature(Encoding.Default.GetString(data));
                }
                else if (reCommMessage == CompactMiniData.CommandMessage.RDS)
                {
                    m_CompactMiniDataCtrl.ReceiveSetCoolingPlateTemperature(Encoding.Default.GetString(data));
                }
                else if (reCommMessage == CompactMiniData.CommandMessage.RDT)
                {
                    m_CompactMiniDataCtrl.ReceiveSetFiberTemperature(Encoding.Default.GetString(data));
                }
                else if (reCommMessage == CompactMiniData.CommandMessage.RDV)
                {
                    m_CompactMiniDataCtrl.ReceiveSetFirmwareVersion(Encoding.Default.GetString(data));
                }
                else if (reCommMessage == CompactMiniData.CommandMessage.RDW)
                {
                    m_CompactMiniDataCtrl.ReceiveSetPeltierTemperature(Encoding.Default.GetString(data));
                }
                else if (reCommMessage == CompactMiniData.CommandMessage.RDX)
                {
                    m_CompactMiniDataCtrl.ReceiveSetStatus(Encoding.Default.GetString(data));
                }
                else if (reCommMessage == CompactMiniData.CommandMessage.TRD)
                {
                    m_CompactMiniDataCtrl.ReceiveSetLaserOnTime(Encoding.Default.GetString(data));
                }
                else
                    ;
                ReceiveCompactMiniData.Invoke(m_CompactMiniDataCtrl);                
            }
            catch (Exception)
            {
                //Log.LogManager.AddSystemLog(Log.Log.LogType.Error, "CommunicateEngine::ParsingData -> Received data parsing error.");
            }
        }
        public void SendCommand(byte[] data)
        {
            //byte[] Sedata = new byte[data.Length + 1];
            byte[] Sedata = new byte[data.Length];
            Buffer.BlockCopy(data, 0, Sedata, 0, data.Length);
            mDataTransferList.Enqueue(Sedata);
        }
        public void SendData(byte[] data)
        {
            //byte[] Sedata = new byte[data.Length + 1];
            if (IsConnected)
            {
                byte[] Sedata = new byte[data.Length];
                Buffer.BlockCopy(data, 0, Sedata, 0, data.Length);
                mDataTransferList.Enqueue(Sedata);
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
        public void SendData(CompactMiniData.CommandMessage msg, byte[] data)
        {
            //byte[] Sedata = new byte[data.Length + 1]; 
            if (IsConnected)
            {
                m_CompactMiniDataCtrl.SetRequestedCommand(msg);
                byte[] Sedata = new byte[data.Length];
                Buffer.BlockCopy(data, 0, Sedata, 0, data.Length);
                mDataTransferList.Enqueue(Sedata);
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
            int i;            
            UInt32 buffsize = (UInt32)m_SerialHandler._ReceiveDataQueue.Count;
            if (buffsize != 0)
            {
                byte[] recvData = m_SerialHandler._ReceiveDataQueue.Dequeue();
                for (i = 0; i < recvData.Length; i++)
                {                                 
                    if (recvData[i] == 0x0D)                     // Carriage Return '\r'
                    {
                        byte[] ParData = new byte[uiReceiveCount];
                        Buffer.BlockCopy(ReceivePacketBuff, 0, ParData, 0, (int)uiReceiveCount);
                        ParsingData(ParData);
                        uiReceiveCount = 0;
                        IsReceiveStart = false;
                        IsReceiveAck = true;
                    }
                    else if((recvData[i] != 0x0D) && (recvData[i] != 0x0A))
                    {
                        ReceivePacketBuff[uiReceiveCount] = recvData[i];
                        uiReceiveCount++;
                        IsReceiveStart = true;
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
                    if (!IsConnected)
                    {
                        mCommandList.Clear();
                        mDataTransferList.Clear();
                        m_CompactMiniDataCtrl.ClearRequestedCommand();
                        m_SerialHandler._ReceiveDataQueue.Clear();
                        IsEnqueueData = IsDequeueData = false;
                        uiReceiveCount = 0;
                        IsReceiveStart = false;
                        IsReceiveAck = true;
                        Thread.Sleep(EngineSleepTime);
                        continue;
                    }

                    if (IsEnqueueData && IsDequeueData)
                    {
                        mDataTransferList.Clear();
                        IsEnqueueData = IsDequeueData = false;
                        
                    }
                    // receive Data Packet Parsor 구문 추가 필요.
                    if (m_SerialHandler._ReceiveDataQueue.Count > 0)
                    {
                        ReceivePacket();
                    }
                    /////////////////////////////////////////////
                    switch (mSerialEngineStep)
                    {
                        case SerialEngineStep.Idle:
                            //Do nothing
                            mCommandList.Clear();
                            mDataTransferList.Clear();
                            IsReceiveAck = true;
                            uiReceiveCount = 0;
                            break;

                        case SerialEngineStep.RequestPeriodData:
                            if (IsReceiveAck)
                            {
                                if ((mDataTransferList.Count != 0) && !IsEnqueueData)
                                {
                                    IsDequeueData = true;
                                    data = mDataTransferList.Dequeue();
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

                    if ((data != null) && (mSerialEngineStep != SerialEngineStep.Idle))
                    {
                        RequestDataEventHandler?.Invoke(data, 0, data.Length);
                        IsReceiveAck = false;
                        data = null;
                    }
                }
                catch (Exception ex)
                {
                    //Log.LogManager.AddSystemLog(Log.Log.LogType.Error, "CommunicateEngine::Run -> Fail to working.");
                }
                Thread.Sleep(EngineSleepTime);
            }
        }
    }
}
