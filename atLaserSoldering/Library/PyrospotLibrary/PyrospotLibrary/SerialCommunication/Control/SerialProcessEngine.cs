using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using PyrospotControlLibrary.SerialCommunication.Data;
using PyrospotControlLibrary.SerialCommunication.DataProcessor;
using PyrospotControlLibrary.SerialCommunication.Control;
using System.Collections;

namespace PyrospotControlLibrary.SerialCommunication.Control
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

        private const int EngineSleepTime = 47;         //11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199,479
        private const int ReceiveBuffSize = 1024;
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
        public PyrospotData m_PyrospotDataCtrl;
        public event Action<int> ReceivePyrospotData;

        public delegate void RequestData(byte[] buffer, int offset, int count);

        public event RequestData RequestDataEventHandler;
        public bool IsResponseReceiveError { get; set; } = false;
        public bool IsReceiveStart { get; set; } = false;
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
            m_PyrospotDataCtrl = new PyrospotData();
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
            mContinuousCheckList.Add(m_PyrospotDataCtrl.GetSettingMonitor1Data(6,PyrospotData.MONITOR_DATA_MAP1.MeasuremnetTemperature,1));
            //Get Info. 주기적 요청.
            //mContinuousCheckList.Add(RobotDataHandler.GetCommand(RobotData.ROBOT_MSG.MSG_GET_INFO)[0]);
        }
        private void InitCheckDatas(byte _id)
        {
            //Get Info. 주기적 요청.
            mContinuousCheckList.Add(m_PyrospotDataCtrl.GetSettingMonitor1Datas(_id));            
            //mContinuousCheckList.Add(RobotDataHandler.GetCommand(RobotData.ROBOT_MSG.MSG_GET_INFO)[0]);
        }
        public void ParsingData(byte[] data)
        {
            try
            {
                ushort CheckSum = 0, iCRC16 = 0;
                CheckSum = CRC.CRC16(data, data.Length - 2);
                iCRC16 = (ushort)data[data.Length - 1];
                iCRC16 = (ushort)((iCRC16 << 8) | data[data.Length - 2]);
                //PyrospotData.CommandMassege reCommMassege = new PyrospotData.CommandMassege();
                if (CheckSum == iCRC16)
                {
                    //reCommMassege = m_PyrospotDataCtrl.GetRequestedCommand();
                    //if (reCommMassege == AiCData.CommandMassege.MSG_MONITOR_DATA) 
                    {
                        if (data[1] == (byte)DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters)
                        {
                            if (data[2] == 2)
                            {
                                int temp = 0;
                                temp = data[3];
                                temp = (temp << 8) | data[4];
                                m_PyrospotDataCtrl.PresentTemperature = temp;
                                //m_PyrospotDataCtrl.ReceiveSetMonitor1Data(data);
                                ReceivePyrospotData.Invoke(m_PyrospotDataCtrl.PresentTemperature);
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
            byte[] Sedata = new byte[data.Length];
            Buffer.BlockCopy(data, 0, Sedata, 0, data.Length);
            mDataTransferList.Enqueue(Sedata);
        }
        public void SendData(byte[] data)
        {
            try
            {
                if (IsConnected)
                {
                    byte[] Sedata = new byte[data.Length];
                    Buffer.BlockCopy(data, 0, Sedata, 0, data.Length);
                    mDataTransferList.Enqueue(Sedata);
                }
            }
            catch (Exception ex)
            {
                ;
            }
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
                    if ((IsReceiveStart == false) && ((ReData == 0x06) ))
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
                                IsReceiveAck = true;
                                IsReceiveStart = false;
                            }
                        }
                        else if ((ReceivePacketBuff[1] == (byte)ModbusRTU.ReadFunctionCodes.ReadInputs) || (ReceivePacketBuff[1] == (byte)ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters) ||
                                (ReceivePacketBuff[1] == (byte)ModbusRTU.ReadFunctionCodes.ReadInputRegisters))
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
                        else if ((ReceivePacketBuff[1] == (byte)ModbusRTU.MultipleWriteFunctionCodes.WriteMultipleRegisters) || (ReceivePacketBuff[1] == (byte)ModbusRTU.WriteFunctionCodes.WriteSingleCoil) ||
                                (ReceivePacketBuff[1] == (byte)ModbusRTU.WriteFunctionCodes.WriteSingleRegister))
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
                            uiReceiveCount = 0;
                            IsReceiveAck = true;
                            IsReceiveStart = false;                            
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
                        //m_PyrospotDataCtrl.ClearRequestedCommand();
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
