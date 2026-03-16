using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiCControlLibrary.SerialCommunication.Data;
using AiCControlLibrary.SerialCommunication.DataProcessor;
using AiCControlLibrary.SerialCommunication.Control;

namespace AiCControlLibrary.SerialCommunication.Control
{
    public class CommunicationManager
    {
        public delegate void DataSendRequestEventHandler(byte[] data);
        public delegate void DataPasorEventHandler(AiCData data);
        public static event DataSendRequestEventHandler CommandSendRequestEvent;
        public static event DataSendRequestEventHandler DataSendRequestEvent;
        //public static event DataPasorEventHandler DataReceivePasorEvent;
        //public event Action<byte[]> SendDataEvnet;
        //        public static event Action<byte[]> SerialSendedData;                // Send Data를 Log 기록 이벤트
        //        public static event Action<byte[]> SerialReceivedRawData;           // Receive Raw Data를 Log 기록 이벤트
        //        public static event Action<byte[]> SerialReceivedParsedData;        // Receive Parsor Data를 Log 기록 이벤트
        //        public event Action<string, bool> PortOpenedEvent;
        public event Action<AiCData> ReceiveDataUpdateEvent;

        readonly SerialProcessEngine mSerialEngine;
        readonly SerialHandler mSerialHandler;

        public AiCData mPhotoSensorCtrl;
        public static bool IsConnected { get; private set; } = false;
        private int mPortOpenedButNoDataReceived = 0;
        public double PresentVoltage = 0;
        public CommunicationManager()
        {
            mSerialEngine = new SerialProcessEngine();
            mSerialHandler = new SerialHandler();
            mPhotoSensorCtrl = new AiCData();
            //ConnectEvents();
        }
        ~CommunicationManager() 
        {
            StopEngine();
        }
        private void ConnectEvents()
        {
            //Connect Events.
            mSerialEngine.RequestDataEventHandler += mSerialHandler.SendData;
            mSerialEngine.ReceiveAiCData += PrevalueUpdateData;
            mSerialHandler.ReceivedQueueDataEventHandler += mSerialEngine.ReceiveQueueData;
            //mSerialHandler.ParsedDataReceivedEvent += mSerialEngine.ParsingData;
            CommandSendRequestEvent += SendCommandToEngine;
            DataSendRequestEvent += SendDataToEngine;

            //DataReceivePasorEvent += mSerialEngine.ParsedDataReceivedEvent;
           // mSerialHandler.SendDataEvent += SerialCommunicateSendedDataReceiver;      // Send Data Log 기록 이벤트                  
        }
        private void DisconnectEvents()
        {
            mSerialEngine.RequestDataEventHandler -= mSerialHandler.SendData;
            mSerialEngine.ReceiveAiCData -= PrevalueUpdateData;
            mSerialHandler.ReceivedQueueDataEventHandler -= mSerialEngine.ReceiveQueueData;
            CommandSendRequestEvent -= SendCommandToEngine;
            DataSendRequestEvent -= SendDataToEngine;
        }

        public void SetSerialData(SerialPortSetData data)
        {
            mSerialHandler.SetSettings(data);
        }
        //public void ReceiveQueueData(byte[] data, int length)
        //{
        //    for (int i = 0; i < length; i++)
        //    {
        //        mSerialHandler.mQueue.Push(data[i]);
        //    }
        //}
        #region Command Data Send
        public static void SendCommand(byte[] data)
        {
            CommandSendRequestEvent?.Invoke(data);
        }

        private void SendCommandToEngine(byte[] data)
        {
            mSerialEngine.SendCommand(data);
        }
        #endregion

        #region Program Data Send
        public void SendData(byte[] data)
        {
            DataSendRequestEvent?.Invoke(data);
        }

        private void SendDataToEngine(byte[] data)
        {
            mSerialEngine.SendData(data);
        }
        #endregion        
        public void CommandByButton(string name)
        {
            if ((name.Contains("button_Connect")) && !mSerialHandler.IsOpen && CheckDataSetted())
            {
                Connect();
            }
            else if (name.Contains("button_Disconnect"))
            {
                Disconnect();
            }
            else if (name.Contains("button_Connect") && mSerialHandler.IsOpen && CheckDataSetted())
            {
                mPortOpenedButNoDataReceived++;
                if (mPortOpenedButNoDataReceived > 10)
                {
                    mPortOpenedButNoDataReceived = 0;
                }
            }

            if (IsConnected)
                mPortOpenedButNoDataReceived = 0;
        }
        public void PrevalueUpdateData(AiCData data)
        {
            //PresentVoltage = data.PresentValue;
            ReceiveDataUpdateEvent.Invoke(data);
        }
        public void StopEngine()
        {
            mSerialEngine.StopEngine();
            //mSerialPortHandler.StopEngine();
        }

        public bool CheckDataSetted()
        {
            return mSerialHandler.IsRightValueSetted;
        }        
        public bool Connect()
        {
            if (mSerialHandler.OpenSerialPort())
            {
                //PortOpenedEvent?.Invoke(mSerialHandler.GetPortName, true);
                ConnectEvents();
                mSerialEngine.IsConnected = true;
                mSerialEngine.StartEngine();
                return true;
            }
            else
            {
                //PortOpenedEvent?.Invoke(mSerialHandler.GetPortName, false);                
                mSerialEngine.IsConnected = false;
                mSerialEngine.PauseEngine();
                return false;
            }
        }

        public bool Disconnect()
        {
            if (mSerialHandler.ClosedSerialPort())
            {
                mSerialEngine.IsConnected = false;
                mSerialEngine.PauseEngine();
                DisconnectEvents();
                //PortOpenedEvent?.Invoke(mSerialHandler.GetPortName, false);
                return true;
            }
            else
            {
                mSerialEngine.IsConnected = true;
                //PortOpenedEvent?.Invoke(mSerialHandler.GetPortName, true);
                return false;
            }
        }

        private static event Action CheckConnectionStateEvent;
        public static void RequestCheckConnectionState() => CheckConnectionStateEvent?.Invoke();

        private void CheckConnectionState()
        {
            if (IsConnected && !mSerialHandler.IsOpen)
                Disconnect();
        }

        public bool IsOpen()
        {
            return mSerialHandler.IsOpen;
        }
    }
}
