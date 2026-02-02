using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompactSECommunication.Communication.Data;
using CompactSECommunication.Communication.Control;
using CompactSECommunication.Communication.DataProcessor;

namespace CompactSECommunication.Communication.Control
{
    public class CommunicationManager
    {
        public delegate void DataSendRequestEventHandler(byte[] data);
        //public delegate void ReceiveRawDataUpdateHandler(byte[] data);
        public static event DataSendRequestEventHandler CommandSendRequestEvent;
        public static event DataSendRequestEventHandler DataSendRequestEvent;
        //public static event ReceiveRawDataUpdateHandler recvRawDataEvent;
        public event Action<UserCompactSEData> ReceiveDataUpdateEvent;
        public event Action<byte[]> ReceiveRawDataEvent;

        readonly TcpProcessEngine mTcpEngine;
        readonly TcpIpHandler mTcpIpHandler;
        public UserCompactSEData mLaserSourceData = null;

        public double PresentVoltage = 0;
        public CommunicationManager()
        {               
            mTcpEngine = new TcpProcessEngine();
            mTcpIpHandler = mTcpEngine.mTcpHandler;                        
            mLaserSourceData = mTcpEngine.mLaserData;
        }
        ~CommunicationManager()
        {
            StopEngine();
        }
        private void ConnectEvents()
        {
            //Connect Events.
            mTcpEngine.RequestDataEventHandler += mTcpIpHandler.SendData;
            mTcpEngine.ReceiveDataCompactSE += ReceiveDataUpdateData;            
            CommandSendRequestEvent += SendCommandToEngine;
            DataSendRequestEvent += SendDataToEngine;
            mTcpIpHandler.ReceivePacketRawDataEvent += ReceiveRawData;
        }
        private void DisconnectEvents()
        {
            mTcpEngine.RequestDataEventHandler -= mTcpIpHandler.SendData;
            mTcpEngine.ReceiveDataCompactSE -= ReceiveDataUpdateData;            
            CommandSendRequestEvent -= SendCommandToEngine;
            DataSendRequestEvent -= SendDataToEngine;
            mTcpIpHandler.ReceivePacketRawDataEvent -= ReceiveRawData;
            mTcpEngine.InitialBuff();            
        }

        public void SetTcpIpData(TcpIpSetData data)
        {
            mTcpIpHandler.SetSettings(data);
        }
        public void SetTcpIpData(string IpAddress, int Port)
        {
            TcpIpSetData setData = new TcpIpSetData();
            setData.IpAddress = IpAddress;
            setData.Port = Port;
            setData.WriteTimeout = 2000;
            setData.ReadTimeout = 2000;
            mTcpIpHandler.SetSettings(setData);
        }
        #region Command Data Send
        public void SendCommand(byte[] data)
        {
            CommandSendRequestEvent?.Invoke(data);
        }

        private void SendCommandToEngine(byte[] data)
        {
            mTcpEngine.SendCommand(data);
        }
        #endregion

        #region Program Data Send
        public void SendData(byte[] data)
        {
            DataSendRequestEvent?.Invoke(data);
        }

        private void SendDataToEngine(byte[] data)
        {
            if (mTcpEngine.IsReceiveAck)
                mTcpEngine.SendData(data);
        }
        #endregion          
        public void ReceiveDataUpdateData(UserCompactSEData data)
        {
            mLaserSourceData = data;
            ReceiveDataUpdateEvent.Invoke(mLaserSourceData);
        }
        public void ReceiveRawData(byte[] data)
        {
            ReceiveRawDataEvent.Invoke(data);
        }
        public void StopEngine()
        {
            mTcpEngine.StopEngine();
            //mSerialPortHandler.StopEngine();
        }

        public bool CheckDataSetted()
        {
            return mTcpIpHandler.IsRightValueSetted;
        }
        public bool Connect()
        {
            if (mTcpIpHandler.ConnectServer())
            {                
                ConnectEvents();
                mTcpEngine.IsConnected = true;
                mTcpEngine.StartEngine();
                return true;
            }
            else
            {                
                mTcpEngine.IsConnected = false;
                mTcpEngine.PauseEngine();
                return false;
            }
        }

        public bool Disconnect()
        {
            if (mTcpIpHandler.Disconnect())
            {
                mTcpEngine.IsConnected = false;
                mTcpEngine.PauseEngine();
                DisconnectEvents();                
                return true;
            }
            else
            {
                mTcpEngine.IsConnected = true;                
                return false;
            }
        }
        public bool IsConnected()
        {
            return mTcpIpHandler.isConnected;
        }
        public bool IsReceiveAck()
        {
            return mTcpEngine.IsReceiveAck;
        }
    }
}
