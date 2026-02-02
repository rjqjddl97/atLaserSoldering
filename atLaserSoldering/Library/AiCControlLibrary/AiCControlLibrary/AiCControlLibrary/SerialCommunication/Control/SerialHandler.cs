using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using AiCControlLibrary.SerialCommunication.DataProcessor;

namespace AiCControlLibrary.SerialCommunication.Control
{
    public class SerialHandler
    {
        public enum CommunicateStateInfo
        {
            Sended,
            SendFailed,
            Received,
            ReceiveFailed,
            Parsed,
            ParseFailed,
            CRCErrorOccurred
        }
        public Action<CommunicateStateInfo> CommunicateStateInfoEvent;
        private SerialPort mSerialPort;
        private Base64 mBase64;
        public Queue mQueue;        
        public void SendData(byte[] buffer) => SendData(buffer, 0, buffer.Length);        
        List<byte> ReceivedData = new List<byte>();        
        public bool IsRightValueSetted { get; private set; } = false;
        private string readBufferData = string.Empty;
        private const int readbuffsize = 4096;
        private byte[] readBuffer = new byte[readbuffsize];
        //public event Action<byte[]> SendDataEvent;            // Send Data Log 기록이벤트      
        //public event Action<byte[]> ParsedDataReceivedEvent;
        public delegate void ReceiveQueueData(byte[] data,int length);
        //public event ReceiveQueueData ReceivedQueueDataEventHandler;
        public event Action<byte[], int> ReceivedQueueDataEventHandler;
        public SerialHandler()
        {
            mSerialPort = new SerialPort();
            mBase64 = new Base64();
            mQueue = new Queue(4096);
            mSerialPort.DataReceived += new SerialDataReceivedEventHandler(ReceiveData);            
        }
        public void SetSettings(SerialPortSetData data)
        {
            if (data == null) return;
            try
            {
                IsRightValueSetted = false;

                mSerialPort.PortName = data.PortName;
                mSerialPort.BaudRate = data.BaudRate;
                mSerialPort.DataBits = data.DataBits;
                mSerialPort.Parity = data.Parity;
                mSerialPort.ReadTimeout = data.ReadTimeout;
                mSerialPort.WriteTimeout = data.WriteTimeout;
                mSerialPort.Handshake = data.Handshake;
                if (data.StopBits == StopBits.None)
                    mSerialPort.StopBits = StopBits.One;
                else
                    mSerialPort.StopBits = data.StopBits;

                IsRightValueSetted = true;
            }
            catch (Exception)
            {
                IsRightValueSetted = false;
            }
        }

        public string GetPortName
        {
            get => mSerialPort.PortName;
        }
        public bool OpenSerialPort()
        {
            try
            {
                if (!IsRightValueSetted)
                    return false;

                string[] ports = SerialPort.GetPortNames();
                string result = ports.FirstOrDefault(x => x == mSerialPort.PortName);
                if (result == null)
                    return false;

                mSerialPort.Open();
                //mSerialPort.DtrEnable = true;

                if (mSerialPort.IsOpen)
                {                    
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                mSerialPort.Close();
            }
            return false;
        }
        public bool IsOpen
        {
            get => mSerialPort.IsOpen;
        }

        public bool ClosedSerialPort()
        {
            try
            {
                if (mSerialPort.IsOpen)
                {
                    mSerialPort.Close();
                }

                if (mSerialPort.IsOpen)
                    return false;
                else
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void SendData(byte[] buffer, int offset, int count)
        {
            try
            {
                if (mSerialPort.IsOpen)
                {
                    // 데이터 인코딩 및 패킷 생서 후 시리얼 데이터 전송 구문
                    mSerialPort.Write(buffer, offset, buffer.Length);
                }
            }
            catch (Exception)
            {
                ;
            }
        }
        public void ReceiveData(object sender, SerialDataReceivedEventArgs e)
        {
            if (mSerialPort.IsOpen)
            {
                try
                {
                    int Length = mSerialPort.Read(readBuffer, 0, readbuffsize);                    
                    ReceivedQueueDataEventHandler?.BeginInvoke(readBuffer,Length,null,null);
                }
                catch (Exception)
                {
                    //CommunicateStateInfoEvent?.BeginInvoke(CommunicateStateInfo.ReceiveFailed, null, null);                    
                }
            }
            else
            {
                Console.WriteLine("Error => SerialPortHandler::ReceiveData -> Serial Does not Connected.");
            }
        }        
    }
}
