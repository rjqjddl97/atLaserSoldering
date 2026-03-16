using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using AiCControlLibrary.SerialCommunication.Data;
using AiCControlLibrary.SerialCommunication.DataProcessor;
using AiCControlLibrary.SerialCommunication.Control;
using System.Collections;

namespace AiCControlLibrary.SerialCommunication.Control
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
        //private double CurrentcountForDataTransfer = 0;
        //private double MaximumCountForDataTransfer = 0;        
        public Queue<byte[]> mSendCommand = new Queue<byte[]>();
        private bool IsDequeueData = false;                   
        private bool IsEnqueueData = false;
        private SerialReceiveStep mReceiveStep;

        private byte[] ReceivePacketBuff = new byte[ReceiveBuffSize];
        //private int ReceiveCountIndex;
        public SerialHandler m_SerialHandler;
        public AiCData m_AiCDataCtrl;
        public Modulo256 mModulo256 = new Modulo256();
        //public event Action<byte[]> ReceivedDataEvent;
        //public event Action<byte[]> ParsedDataReceivedEvent;
        public event Action<AiCData> ReceiveAiCData;

        public delegate void RequestData(byte[] buffer, int offset, int count);
        //public delegate void ReceiveMT4xData(MT4xPanelMeta data);
        public event RequestData RequestDataEventHandler;        


        public bool IsConnected { get; set; }
        public SerialProcessEngine()
        {
            IsConnected = false;
            m_SerialHandler = new SerialHandler();
            m_AiCDataCtrl = new AiCData();
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
                CheckSum = mModulo256.Checksum(data, 0, data.Length - 1);
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
            byte[] Sedata = new byte[data.Length + 1];
            Buffer.BlockCopy(data, 0, Sedata, 0, data.Length);
            Sedata[data.Length] = mModulo256.Checksum(data, 0, data.Length);
            mDataTransferList.Enqueue(Sedata);
        }
        public void SendData(byte[] data)
        {
            byte[] Sedata = new byte[data.Length + 1];
            Buffer.BlockCopy(data, 0, Sedata, 0, data.Length);
            Sedata[data.Length] = mModulo256.Checksum(data, 0, data.Length);
            mDataTransferList.Enqueue(Sedata);
        }
        public void ReceiveQueueData(byte[] data, int length)
        {
            for (int i = 0; i < length; i++)
            {
                m_SerialHandler.mQueue.Push(data[i]);
            }
        }
        public void ReceivePacket()
        {
            int i;
            byte ReData = 0;
            UInt32 buffsize = m_SerialHandler.mQueue.GetFilledSize();
            if (buffsize != 0)
            {
                byte[] MainData = new byte[buffsize];                
                int ReCounter = 0;
                for (i = 0; i < buffsize; i++)
                {
                    m_SerialHandler.mQueue.Pop(ref ReData);
                    MainData[ReCounter] = ReData;
                    ReCounter++;
                    if (ReCounter > 2)
                    {
                        if (ReCounter == MainData[2] + 4)
                        {
                            byte[] ParData = new byte[ReCounter];
                            Buffer.BlockCopy(MainData, 0, ParData,0, ReCounter);
                            ParsingData(ParData);
                            ReCounter = 0;
                        }
                        else if (ReCounter > MainData[2] + 4)
                        {
                            for (int j = 0; j < ReCounter; j++) MainData[j] = 0;
                            ReCounter = 0;
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
                    if (m_SerialHandler.mQueue.GetFilledSize() != 0)
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
                                data = mContinuousCheckList.ElementAt(mContinuousCheckIndex++);
                                if (mContinuousCheckIndex >= mContinuousCheckList.Count)
                                    mContinuousCheckIndex = 0;
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
