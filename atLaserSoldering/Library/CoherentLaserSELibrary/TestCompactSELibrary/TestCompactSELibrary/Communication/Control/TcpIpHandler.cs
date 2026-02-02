using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using SuperSimpleTcp;
using CompactSECommunication.Communication.Control;
using CompactSECommunication.Communication.Data;

namespace CompactSECommunication.Communication.Control
{
    public class TcpIpHandler
    {
        public enum CommunicateStateInfo
        {
            Sended,
            SendFailed,
            Received,
            ReceiveFailed,
            Parsed,
            ParseFailed            
        }
        private SimpleTcpClient mTcpIpClient;
        public string strIpAddres;
        public int NetPort;
        public bool IsRightValueSetted { get; set; } = false;
        private const int readBuffsize = 4096;
        private byte[] readBuffer = new byte[readBuffsize];
        public delegate void ReceiveQueueData(byte[] data, int length);
        public delegate void ReceiveRawData(byte[] data);
        public event Action<byte[], int> ReceivedQueueDataEventHandler;
        public event Action<byte[]> SendedDataEvent;
        public event Action<byte[]> ReceivePacketRawDataEvent;
        public void SendData(byte[] buffer) => SendData(buffer, 0, buffer.Length);
        List<byte> ReceivedData = new List<byte>();

        public Queue<byte[]> _ReceiveDataQueue = new Queue<byte[]>();        
        byte[] _clientMessageData = null;
        public TcpIpHandler()
        {
            mTcpIpClient = null;
        }        
        public string GetIpAddress
        {
            get => strIpAddres;
        }
        public void SetTcpHandeler(SimpleTcpClient tcphandle)
        {
            mTcpIpClient = tcphandle;
        }
        public void SetSettings(TcpIpSetData data)
        {
            if (data == null) return;
            try
            {
                IsRightValueSetted = false;
                strIpAddres = data.IpAddress;
                NetPort = data.Port;
                IsRightValueSetted = true;
            }
            catch (Exception)
            {
                IsRightValueSetted = false;
            }
        }
        public bool ConnectServer()
        {
            //mTcpIpClient.Connect();
            try
            {
                if (!IsRightValueSetted)
                    return false;
                if (mTcpIpClient != null)
                    mTcpIpClient.Disconnect();

                mTcpIpClient = new SimpleTcpClient(strIpAddres, NetPort);
                
                mTcpIpClient.Events.DataReceived += ReceiveData;
                mTcpIpClient.Connect();
            }
            catch (Exception)
            {
                mTcpIpClient.Disconnect();
            }
            if (isConnected)
                return true;
            else
                return false;
        }
        public bool Disconnect()
        {
            try
            {
                if (isConnected)
                {
                    mTcpIpClient.Disconnect();
                    mTcpIpClient.Events.DataReceived -= ReceiveData;
                    mTcpIpClient = null;
                }
                else
                    return true;                
            }
            catch (Exception)
            {
                return false;
            }
            if (isConnected)
                return false;
            else
                return true;
        }
        public bool isConnected
        {
            get { return (mTcpIpClient == null) ? false : mTcpIpClient.IsConnected; }            
        }
        public void SendData(byte[] buffer, int offset, int count)
        {
            if (isConnected)
            {
                try
                {
                    mTcpIpClient.Send(buffer);
                }
                catch (Exception)
                {
                    ;
                }
            }             
        }
        public void ReceiveData(object sender,DataReceivedEventArgs args)
        {
            try
            {
                if (args.Data != null)
                {
                    byte[] tempbuff = new byte[args.Data.Count];
                    Buffer.BlockCopy(args.Data.Array, 0, tempbuff, 0, args.Data.Count);
                    //ReceivedQueueDataEventHandler?.BeginInvoke(tempbuff, tempbuff.Length, null, null);
                    _ReceiveDataQueue.Enqueue(tempbuff);
                    ReceivePacketRawDataEvent.BeginInvoke(tempbuff,null,null);
                }
            }
            catch (Exception)
            {
                ;
            }
        }
    }
}
