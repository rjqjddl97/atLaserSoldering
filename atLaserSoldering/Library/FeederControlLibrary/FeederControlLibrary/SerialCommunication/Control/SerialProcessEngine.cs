using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using FeederControlLibrary.SerialCommunication.Data;
using FeederControlLibrary.SerialCommunication.DataProcessor;
using FeederControlLibrary.SerialCommunication.Control;
using System.Collections;

namespace FeederControlLibrary.SerialCommunication.Control
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

        private const int EngineSleepTime = 37;
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
        private double CurrentcountForDataTransfer = 0;
        private double MaximumCountForDataTransfer = 0;
        private SerialReceiveStep mReceiveStep;

        private byte[] ReceivePacketBuff = new byte[ReceiveBuffSize];
        //private int ReceiveCountIndex;
        public SerialHandler m_SerialHandler;
        public FeedData m_FeedDataCtrl;
        //public event Action<byte[]> ReceivedDataEvent;
        //public event Action<byte[]> ParsedDataReceivedEvent;
        public event Action<FeedData> ReceiveFeedData;

        public delegate void RequestData(byte[] buffer, int offset, int count);
        
        public event RequestData RequestDataEventHandler;
        public bool IsResponseReceiveError { get; set; } = false;
        public bool IsReceiveStart { get; set; }
        public bool IsReceiveAck { get; set; } = true;
        public bool IsConnected { get; set; }
        public UInt32 uiReceiveCount { get; set; } = 0;
        public List<byte[]> _ContinuousDataList
        {
            get { return mContinuousCheckList; }
            set { mContinuousCheckList = value; }
        }
        public SerialProcessEngine()
        {
            IsConnected = false;
            m_SerialHandler = new SerialHandler();
            m_FeedDataCtrl = new FeedData();
            mSerialEngineStep = SerialEngineStep.Idle;
            mReceiveStep = SerialReceiveStep.Idle;            
            //InitCheckDatas();
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
            //InitCheckDatas();
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
            //mContinuousCheckList.Add(m_FeedDataCtrl.GetSettingMotionDatas(4));
        }
        public void InitialPeriodRequest(byte[] id)
        {
            for(int i = 0; i < id.Length ;i++)
                _ContinuousDataList.Add(m_FeedDataCtrl.GetSettingMotionDatas(id[i]));
        }
        public void ParsingData(byte[] data)
        {
            try
            {
                ushort CheckSum = 0,iCRC16 = 0;
                CheckSum = CRC.CRC16(data, data.Length - 2);
                iCRC16 = (ushort)data[data.Length - 1];
                iCRC16 = (ushort)((iCRC16 << 8) | data[data.Length - 2]);
                FeedData.CommandMassege reCommMassege = new FeedData.CommandMassege();
                if (CheckSum == iCRC16)
                {
                    reCommMassege = m_FeedDataCtrl.GetRequestedCommand();
                    //if (reCommMassege == AiCData.CommandMassege.MSG_MONITOR_DATA) 
                    {                        
                        if (data[1] == (byte)DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputRegisters)
                        {
                            if (data[2] == 32)
                            {
                                m_FeedDataCtrl.ReceiveSetMotionData(data);
                                ReceiveFeedData.Invoke(m_FeedDataCtrl);
                            }
                        }
                    }                    
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
            //byte[] Sedata = new byte[data.Length + 1];
            byte[] Sedata = new byte[data.Length];
            Buffer.BlockCopy(data, 0, Sedata, 0, data.Length);            
            mDataTransferList.Enqueue(Sedata);
        }
        public void SendData(byte[] data)
        {
            //byte[] Sedata = new byte[data.Length + 1];
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
        public void SendData(FeedData.CommandMassege msg, byte[] data)
        {
            //byte[] Sedata = new byte[data.Length + 1]; 
            m_FeedDataCtrl.SetRequestedCommand(msg);
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

                for (i = 0; i < recvData.Length; i++)
                {
                    ReData = recvData[i];
                    if ( (IsReceiveStart == false) && (ReData == 0x04))
                    {
                        IsReceiveStart = true;
                        uiReceiveCount = 0;
                    }                        

                    if (IsReceiveStart)
                    {
                        ReceivePacketBuff[uiReceiveCount] = ReData;
                        uiReceiveCount++;
                    }

                    if (uiReceiveCount > 2)
                    {
                        if (ReceivePacketBuff[1] > (byte)ModbusRTU.FunctionCodes.Exception)
                        {
                            if (uiReceiveCount >= 5)
                            {
                                //for (int j = 0; j < uiReceiveCount; j++) ReceivePacketBuff[j] = 0;
                                uiReceiveCount = 0;
                                IsReceiveStart = false;
                                IsReceiveAck = true;
                            }
                        }
                        else if ((ReceivePacketBuff[1] != (byte)ModbusRTU.WriteFunctionCodes.WriteSingleCoil) && (ReceivePacketBuff[1] != (byte)ModbusRTU.WriteFunctionCodes.WriteSingleRegister) && (ReceivePacketBuff[1] != (byte)ModbusRTU.MultipleWriteFunctionCodes.WriteMultipleCoils) && (ReceivePacketBuff[1] != (byte)ModbusRTU.MultipleWriteFunctionCodes.WriteMultipleRegisters))
                        {
                            if (uiReceiveCount >= ReceivePacketBuff[2] + 5)
                            {
                                if (uiReceiveCount == ReceivePacketBuff[2] + 5)
                                {
                                    byte[] MainBuffer = new byte[uiReceiveCount];
                                    Buffer.BlockCopy(ReceivePacketBuff, 0, MainBuffer, 0, (int)uiReceiveCount);
                                    ParsingData(MainBuffer);
                                }
                                uiReceiveCount = 0;
                                IsReceiveStart = false;
                                IsReceiveAck = true;
                            }
                        }
                        else if ((ReceivePacketBuff[1] == (byte)ModbusRTU.MultipleWriteFunctionCodes.WriteMultipleRegisters) || (ReceivePacketBuff[1] == (byte)ModbusRTU.WriteFunctionCodes.WriteSingleCoil) || (ReceivePacketBuff[1] == (byte)ModbusRTU.WriteFunctionCodes.WriteSingleRegister))
                        {
                            if (uiReceiveCount >= 8)
                            {                                
                                uiReceiveCount = 0;
                                IsReceiveStart = false;
                                IsReceiveAck = true;
                            }
                        }
                        else
                        {
                            if (uiReceiveCount >= ReceivePacketBuff[2] + 5)
                            {                                
                                uiReceiveCount = 0;
                                IsReceiveStart = false;
                                IsReceiveAck = true;
                            }
                        }
                    }
                }                
            }
        }
        private async void Run()
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
                        m_FeedDataCtrl.ClearRequestedCommand();
                        IsEnqueueData = IsDequeueData = false;
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
                            break;

                        case SerialEngineStep.RequestPeriodData:
                            if ((mDataTransferList.Count != 0) && !IsEnqueueData)
                            {
                                IsDequeueData = true;
                                data = mDataTransferList.Dequeue();

                                //if (data != null)
                                //{
                                //    CurrentcountForDataTransfer++;
                                //    if (CurrentcountForDataTransfer >= MaximumCountForDataTransfer)
                                //    {
                                //        CurrentcountForDataTransfer = MaximumCountForDataTransfer = 0;
                                //    }
                                //}
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
                                //if (!IsReceiveStart)
                                {
                                    if (mContinuousCheckIndex >= mContinuousCheckList.Count)
                                        mContinuousCheckIndex = 0;
                                    data = mContinuousCheckList.ElementAt(mContinuousCheckIndex++);
                                    //m_AiCDataCtrl.SetRequestedCommand(AiCData.CommandMassege.MSG_MONITOR_DATA);
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
                        data = null;
                    }
                }
                catch (Exception)
                {
                    //Log.LogManager.AddSystemLog(Log.Log.LogType.Error, "CommunicateEngine::Run -> Fail to working.");
                }
                await Task.Delay(EngineSleepTime);
                //Thread.Sleep(EngineSleepTime);
            }
        }
    }
}
