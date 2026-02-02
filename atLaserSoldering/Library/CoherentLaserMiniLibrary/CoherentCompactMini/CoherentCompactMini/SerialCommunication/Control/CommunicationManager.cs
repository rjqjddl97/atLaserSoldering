using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoherentCompactMini.SerialCommunication.Data;
using CoherentCompactMini.SerialCommunication.DataProcessor;
using CoherentCompactMini.SerialCommunication.Control;

namespace CoherentCompactMini.SerialCommunication.Control
{
    public class CommunicationManager
    {
        public delegate void DataSendRequestEventHandler(byte[] data);
        public delegate void DataPasorEventHandler(CompactMiniData data);
        public static event DataSendRequestEventHandler CommandSendRequestEvent;
        public static event DataSendRequestEventHandler DataSendRequestEvent;
        //        public static event Action<byte[]> SerialSendedData;                // Send Data를 Log 기록 이벤트
        //        public static event Action<byte[]> SerialReceivedRawData;           // Receive Raw Data를 Log 기록 이벤트
        //        public static event Action<byte[]> SerialReceivedParsedData;        // Receive Parsor Data를 Log 기록 이벤트
        //        public event Action<string, bool> PortOpenedEvent;
        public event Action<CompactMiniData> ReceiveDataUpdateEvent;
        public event Action<byte[]> ReceiveRawDataEvent;

        readonly SerialProcessEngine mSerialEngine;
        readonly SerialHandler mSerialHandler;

        public CompactMiniData mLaserSourceCtrl = null;
        public static bool IsConnected { get; private set; } = false;
        private int mPortOpenedButNoDataReceived = 0;
        public double PresentVoltage = 0;
        public CommunicationManager()
        {
            mSerialEngine = new SerialProcessEngine();
            mSerialHandler = mSerialEngine.m_SerialHandler;
            mLaserSourceCtrl = mSerialEngine.m_CompactMiniDataCtrl;
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
            mSerialEngine.ReceiveCompactMiniData += ReceiveUpdateData;            
            //mSerialHandler.ParsedDataReceivedEvent += mSerialEngine.ParsingData;
            CommandSendRequestEvent += SendCommandToEngine;
            DataSendRequestEvent += SendDataToEngine;
            mSerialHandler.ReceivePacketRawDataEvent += ReceiveRawData;

            //DataReceivePasorEvent += mSerialEngine.ParsedDataReceivedEvent;
            // mSerialHandler.SendDataEvent += SerialCommunicateSendedDataReceiver;      // Send Data Log 기록 이벤트                  
        }
        private void DisconnectEvents()
        {
            mSerialEngine.RequestDataEventHandler -= mSerialHandler.SendData;
            mSerialEngine.ReceiveCompactMiniData -= ReceiveUpdateData;            
            CommandSendRequestEvent -= SendCommandToEngine;
            DataSendRequestEvent -= SendDataToEngine;
            mSerialHandler.ReceivePacketRawDataEvent -= ReceiveRawData;
        }

        public void SetSerialData(SerialPortSetData data)
        {
            mSerialHandler.SetSettings(data);
        }
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
        public void ReceiveUpdateData(CompactMiniData data)
        {            
            mLaserSourceCtrl = data;
            ReceiveDataUpdateEvent.Invoke(mLaserSourceCtrl);
        }
        public void ReceiveRawData(byte[] data)
        {
            ReceiveRawDataEvent.Invoke(data);
        }
        public void StopEngine()
        {
            mSerialEngine.StopEngine();            
        }

        public bool CheckDataSetted()
        {
            return mSerialHandler.IsRightValueSetted;
        }        
        public bool Connect()
        {
            if (mSerialHandler.OpenSerialPort())
            {                
                ConnectEvents();
                mSerialEngine.IsConnected = true;
                mSerialEngine.StartEngine();
                return true;
            }
            else
            {             
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
        public bool IsReceiveAck()
        {
            return mSerialEngine.IsReceiveAck;
        }
    }
}
