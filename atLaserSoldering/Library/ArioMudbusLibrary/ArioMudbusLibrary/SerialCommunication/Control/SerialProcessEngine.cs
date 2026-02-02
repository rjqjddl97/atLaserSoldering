using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ArioModbusLibrary.SerialCommunication.Data;
using ArioModbusLibrary.SerialCommunication.DataProcessor;
using ArioModbusLibrary.SerialCommunication.Control;
using System.Collections;

namespace ArioModbusLibrary.SerialCommunication.Control
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

        private const int EngineSleepTime = 73;
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
        public ArioMRData m_ARioDataCtrl;        
        //public event Action<byte[]> ReceivedDataEvent;
        //public event Action<byte[]> ParsedDataReceivedEvent;
        public event Action<ArioMRData> ReceiveARioData;

        public delegate void RequestData(byte[] buffer, int offset, int count);
        //public delegate void ReceiveMT4xData(MT4xPanelMeta data);
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
            m_ARioDataCtrl = new ArioMRData();
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
            //Get Info. 주기적 요청.
            mContinuousCheckList.Add(m_ARioDataCtrl.GetSettingInputs(1,1));
            mContinuousCheckList.Add(m_ARioDataCtrl.GetSettingOutputs(1,1));
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
                //ARIOData.CommandMassege reCommMassege = new ARIOData.CommandMassege();
                if (CheckSum == iCRC16)
                {
                    if (data[1] == (byte)DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputRegisters)
                    {
                        m_ARioDataCtrl.ReceiveSetInput(data);
                    }
                    else if(data[1] == (byte)DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters)
                    {
                        m_ARioDataCtrl.ReceiveSetOutput(data);
                    }
                    ReceiveARioData.Invoke(m_ARioDataCtrl);
                    //reCommMassege = m_ARMDataCtrl.GetRequestedCommand();
                    //if (reCommMassege == ARMData.CommandMassege.MSG_INPUT)
                    //    m_ARMDataCtrl.ReceiveSetInput(data);
                    //else if (reCommMassege == ARMData.CommandMassege.MSG_OUTPUT)
                    //    m_ARMDataCtrl.ReceiveSetOutput(data);
                    //else if (reCommMassege == ARMData.CommandMassege.MSG_PRODUCT_INFO)
                    //    m_ARMDataCtrl.ReceiveSetProductInfo(data);
                    //else if (reCommMassege == ARMData.CommandMassege.MSG_EXT_UNIT_INFO)
                    //    m_ARMDataCtrl.ReceiveSetExtProductInfo(data);
                    //else if (reCommMassege == ARMData.CommandMassege.MSG_DEVICE_INFO)
                    //    m_ARMDataCtrl.ReceiveSetDeviceInfo(data);
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
                    if ((IsReceiveStart == false) && ((ReData == 0x01) || (ReData == 0x02)))
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
                            if (uiReceiveCount == 5)
                            {
                                uiReceiveCount = 0;
                                IsReceiveStart = false;
                            }
                            else if (uiReceiveCount > 5)
                            {
                                //for (int j = 0; j < ReCounter; j++) ReceivePacketBuff[j] = 0;
                                uiReceiveCount = 0;
                                IsReceiveStart = false;
                                IsReceiveAck = true;
                            }
                        }
                        else if ((ReceivePacketBuff[1] != (byte)ModbusRTU.WriteFunctionCodes.WriteSingleCoil) && (ReceivePacketBuff[1] != (byte)ModbusRTU.WriteFunctionCodes.WriteSingleRegister) && (ReceivePacketBuff[1] != (byte)ModbusRTU.MultipleWriteFunctionCodes.WriteMultipleCoils) && (ReceivePacketBuff[1] != (byte)ModbusRTU.MultipleWriteFunctionCodes.WriteMultipleRegisters))
                        {
                            if (uiReceiveCount == ReceivePacketBuff[2] + 5)
                            {
                                byte[] MainData = new byte[uiReceiveCount];
                                Buffer.BlockCopy(ReceivePacketBuff, 0, MainData, 0, (int)uiReceiveCount);
                                ParsingData(MainData);
                                uiReceiveCount = 0;
                                IsReceiveStart = false;
                                IsReceiveAck = true;
                            }
                            else if (uiReceiveCount > ReceivePacketBuff[2] + 5)
                            {
                                //for (int j = 0; j < uiReceiveCount; j++) ReceivePacketBuff[j] = 0;
                                uiReceiveCount = 0;
                                IsReceiveStart = false;
                                IsReceiveAck = true;
                            }
                        }
                        else if ((ReceivePacketBuff[1] == (byte)ModbusRTU.MultipleWriteFunctionCodes.WriteMultipleRegisters) || (ReceivePacketBuff[1] == (byte)ModbusRTU.WriteFunctionCodes.WriteSingleCoil) || (ReceivePacketBuff[1] == (byte)ModbusRTU.WriteFunctionCodes.WriteSingleRegister))
                        {
                            if (uiReceiveCount >= 8)
                            {
                                //ParsingData(MainData);
                                uiReceiveCount = 0;
                                IsReceiveStart = false;
                                IsReceiveAck = true;
                            }
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
                catch (Exception)
                {
                    //Log.LogManager.AddSystemLog(Log.Log.LogType.Error, "CommunicateEngine::Run -> Fail to working.");
                }
                Thread.Sleep(EngineSleepTime);
            }
        }
    }
}
