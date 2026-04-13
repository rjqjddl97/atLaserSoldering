using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using LFineLightLibrary.SerialCommunication.Data;
using LFineLightLibrary.SerialCommunication.DataProcessor;
using LFineLightLibrary.SerialCommunication.Control;
using System.Collections;

namespace LFineLightLibrary.SerialCommunication.Control
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
        public LFineData m_LightData;        
        //public event Action<byte[]> ReceivedDataEvent;
        //public event Action<byte[]> ParsedDataReceivedEvent;
        public event Action<LFineData> ReceiveLightData;

        public delegate void RequestData(byte[] buffer, int offset, int count);      
        
        public event RequestData RequestDataEventHandler;        
        public bool IsResponseReceiveError { get; set; } = false;
        public bool IsReceiveStart { get; set; }
        public bool IsReceiveAck { get; set; } = true;
        public bool IsConnected { get; set; }
        public UInt32 uiReceiveCount { get; set; } = 0;
        public SerialProcessEngine()
        {
            IsConnected = false;
            m_SerialHandler = new SerialHandler();
            m_LightData = new LFineData();
            mSerialEngineStep = SerialEngineStep.Idle;
            mReceiveStep = SerialReceiveStep.Idle;
            //m_SerialHandler.ReceivedQueueDataEventHandler += ReceiveQueueData;
            InitCheckDatas();
            Array.Clear(ReceivePacketBuff, 0x00, ReceiveBuffSize);
            //ReceiveCountIndex = 0;
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

            //Get Info. 주기적 요청.
            //mContinuousCheckList.Add(RobotDataHandler.GetCommand(RobotData.ROBOT_MSG.MSG_GET_INFO)[0]);
        }
        public void ParsingData(byte[] data)
        {
            try
            {
                byte CheckSum = 0;                
                if (CheckSum == data[data.Length - 1])
                {
                    ;
                }
                else
                {
                    // CheckSum Error( Modulo256 Error )
                }              
            }
            catch (Exception)
            {
                //Log.LogManager.AddSystemLog(Log.Log.LogType.Error, "CommunicateEngine::ParsingData -> Received data parsing error.");
            }
        }
        public void SendCommand(byte[] data)
        {
            byte[] Sedata = new byte[data.Length];
            Buffer.BlockCopy(data, 0, Sedata, 0, data.Length);
            mDataTransferList.Enqueue(Sedata);
        }
        public void SendData(byte[] data)
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
        public void ReceivePacket()
        {
            int i;
            byte ReData = 0;
            UInt32 buffsize = (UInt32)m_SerialHandler._ReceiveDataQueue.Count;
            if (buffsize != 0)
            {
                byte[] recvData = m_SerialHandler._ReceiveDataQueue.Dequeue();                
                for (i = 0; i < buffsize; i++)
                {
                    ReData = recvData[i];
                    if ((IsReceiveStart == false) && (ReData == LFineData.STX))
                    {
                        IsReceiveStart = true;
                        uiReceiveCount = 0;
                    }
                    if (IsReceiveStart)
                    {
                        if (ReData == LFineData.ETX)
                        {
                            IsReceiveStart = false;
                            IsReceiveAck = true;
                        }
                        else
                        {
                            ReceivePacketBuff[uiReceiveCount] = ReData;
                            uiReceiveCount++;
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
                    if (!IsConnected)
                    {
                        mCommandList.Clear();
                        mDataTransferList.Clear();
                        m_SerialHandler._ReceiveDataQueue.Clear();
                        //IsEnqueueData = IsDequeueData = false;
                        Thread.Sleep(EngineSleepTime);
                        continue;
                    }

                    if (IsEnqueueData && IsDequeueData)
                    {
                        mDataTransferList.Clear();
                        //IsEnqueueData = IsDequeueData = false;
                        
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
                            break;

                        case SerialEngineStep.RequestPeriodData:
                            if ((mDataTransferList.Count != 0) && !IsEnqueueData)
                            {
                                //IsDequeueData = true;
                                data = mDataTransferList.Dequeue();
                                if (mDataTransferList.Count == 0)
                                {
                                    //IsDequeueData = false;                                    
                                }
                            }
                            else if (mCommandList.Count != 0)
                            {
                                data = mCommandList.Dequeue();
                            }
                            else if (mContinuousCheckList.Count != 0)
                            {
                                if (mContinuousCheckIndex >= mContinuousCheckList.Count)
                                    mContinuousCheckIndex = 0;
                                data = mContinuousCheckList.ElementAt(mContinuousCheckIndex++);

                            }

                            break;
                        default:
                            //Do some error action.
                            break;
                    }

                    if ((data != null) && (mSerialEngineStep != SerialEngineStep.Idle))
                    {
                        RequestDataEventHandler?.Invoke(data, 0, data.Length);
                        data = null;
                    }
                }
                catch (Exception)
                {
                    //Log.LogManager.AddSystemLog(Log.Log.LogType.Error, "CommunicateEngine::Run -> Fail to working.");
                }
                Thread.Sleep(EngineSleepTime);
            }
        }
    }
}
