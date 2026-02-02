using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompactSECommunication.Communication.DataProcessor;

namespace CompactSECommunication.Communication.Data
{
    public class UserCompactSEData
    {
        public const int FILE_NAME_SIZE = 16;
        public const int POINT_NAME_SIZE = 16;
        public const int PROGRAM_NAME_SIZE = 16;
        public const int MAXIMUM_PROGRAM_LIST_BUFFER_SIZE = 100;
        public const int MAXIMUM_LIST_BUFFER_SIZE = 100;
        public const int RobotId = 0x0100;
        public const int TUnitId = 0x0101;
        public const int HMIUnitId = 0x0102;
        public const int id = 0x0000;
        public const int HEADER_LENGTH = 24;
        private const int RecvBuffSize = 4096;
        public const string CMD_ENABLE = "Enable";
        public const string CMD_DISABLE = "Disable";

        public bool IsHomeCompeleted { get; private set; }
        public CommandMessage mSendCommand = new CommandMessage();
        public CommandMessage mReceivedCommand = new CommandMessage();

        public bool LaserOn { get; set; } = false;
        public bool PilotLaser { get; set; } = false;
        public bool PowerOn { get; set; } = false;
        public bool Reset { get; set; } = false;
        public double ActureCurrent { get; set; } = 0D;
        public double ActurePower { get; set; } = 0D;
        public double SetCurrent { get; set; } = 0D;
        public double SetPower { get; set; } = 0D;
        public UInt32 WatchDogTimeout { get; set; } = 0;
        public string strSerialNumber { get; set; } = string.Empty;
        public string strSoftwareVersion { get; set; } = string.Empty;
        public IO_8Bit LaserStatus { get; set; } = new IO_8Bit();
        public bool FullAutoCycle;
        private byte[] ReceiveDataRaw = new byte[RecvBuffSize];
        //private Queue<byte[]> RequestedCommand = new Queue<byte[]>();        
        public UserCompactSEData()
        {
            //Init here.
            IsHomeCompeleted = false;
        }
        ~UserCompactSEData()
        {

        }
        public enum CommandMessage
        {
            NONE = 0,
            LOGIN,
            LOGOUT,
            GET_LASERON,
            GET_PILOTON,
            GET_POWERON,            
            GET_USEROUTPUT,
            GET_ACT_CURRENT,
            GET_ACT_POWER,
            GET_SETCURRENT,
            GET_SETPOWER,
            GET_WATCHDOGTIMEOUT,
            GET_SW_VERSION,
            GET_SERIAL,
            SET_LASERON,
            SET_PILOTON,
            SET_POWERON,
            SET_RESET,
            SET_CURRENT,
            SET_POWER,
            SET_WATCHDOGTIMEOUT
        }
        public enum KindofAttribute
        {
            None = 0,
            LaserOn,
            Pilot,
            PowerOn,
            Reset,
            UserOutput,
            actCurrent,
            actPower,
            setCurrent,
            SetPower,
            WatchDogTimeout,
            SWVersion,
            Serial
        }
        public enum KindofObject
        {
            None = 0,
            RTI_IF,
            SystemControl,
            Controller
        }
        public class IO_8Bit
        {
            public byte Bit8;
            //public int B0 { get { return Bit8 & (1 << 0); } set { Bit8 |= (1 << 0); } }
            public bool B0 { get { return Convert.ToBoolean((Bit8 >> 0) & 0x01); } set { Bit8 |= (1 << 0); } }
            public bool B1 { get { return Convert.ToBoolean((Bit8 >> 1) & 0x01); } set { Bit8 |= (1 << 1); } }
            public bool B2 { get { return Convert.ToBoolean((Bit8 >> 2) & 0x01); } set { Bit8 |= (1 << 2); } }
            public bool B3 { get { return Convert.ToBoolean((Bit8 >> 3) & 0x01); } set { Bit8 |= (1 << 3); } }
            public bool B4 { get { return Convert.ToBoolean((Bit8 >> 4) & 0x01); } set { Bit8 |= (1 << 4); } }
            public bool B5 { get { return Convert.ToBoolean((Bit8 >> 5) & 0x01); } set { Bit8 |= (1 << 5); } }
            public bool B6 { get { return Convert.ToBoolean((Bit8 >> 6) & 0x01); } set { Bit8 |= (1 << 6); } }
            public bool B7 { get { return Convert.ToBoolean((Bit8 >> 7) & 0x01); } set { Bit8 |= (1 << 7); } }
            public void SetData(byte data)
            {
                Bit8 = data;
            }
        }
        public class IO_16Bit
        {
            public UInt16 Bit16;
            public bool B0 { get { return Convert.ToBoolean((Bit16 >> 0) & 0x01); } set { Bit16 |= (1 << 0); } }
            public bool B1 { get { return Convert.ToBoolean((Bit16 >> 1) & 0x01); } set { Bit16 |= (1 << 1); } }
            public bool B2 { get { return Convert.ToBoolean((Bit16 >> 2) & 0x01); } set { Bit16 |= (1 << 2); } }
            public bool B3 { get { return Convert.ToBoolean((Bit16 >> 3) & 0x01); } set { Bit16 |= (1 << 3); } }
            public bool B4 { get { return Convert.ToBoolean((Bit16 >> 4) & 0x01); } set { Bit16 |= (1 << 4); } }
            public bool B5 { get { return Convert.ToBoolean((Bit16 >> 5) & 0x01); } set { Bit16 |= (1 << 5); } }
            public bool B6 { get { return Convert.ToBoolean((Bit16 >> 6) & 0x01); } set { Bit16 |= (1 << 6); } }
            public bool B7 { get { return Convert.ToBoolean((Bit16 >> 7) & 0x01); } set { Bit16 |= (1 << 7); } }
            public bool B8 { get { return Convert.ToBoolean((Bit16 >> 8) & 0x01); } set { Bit16 |= (1 << 8); } }
            public bool B9 { get { return Convert.ToBoolean((Bit16 >> 9) & 0x01); } set { Bit16 |= (1 << 9); } }
            public bool B10 { get { return Convert.ToBoolean((Bit16 >> 10) & 0x01); } set { Bit16 |= (1 << 10); } }
            public bool B11 { get { return Convert.ToBoolean((Bit16 >> 11) & 0x01); } set { Bit16 |= (1 << 11); } }
            public bool B12 { get { return Convert.ToBoolean((Bit16 >> 12) & 0x01); } set { Bit16 |= (1 << 12); } }
            public bool B13 { get { return Convert.ToBoolean((Bit16 >> 13) & 0x01); } set { Bit16 |= (1 << 13); } }
            public bool B14 { get { return Convert.ToBoolean((Bit16 >> 14) & 0x01); } set { Bit16 |= (1 << 14); } }
            public bool B15 { get { return Convert.ToBoolean((Bit16 >> 15) & 0x01); } set { Bit16 |= (1 << 15); } }
            public void SetData(UInt16 data)
            {
                Bit16 = data;
            }
        }
        public class RequestCommand
        {
            public byte cookie;
            public CommandMessage command;
        }

        public Queue<RequestCommand> RequestedCommandQueue = new Queue<RequestCommand>();
        
        public void SetRequestedCommand(RequestCommand cmd)
        {
            RequestedCommandQueue.Enqueue(cmd);
        }
        public RequestCommand GetRequestedCommand()
        {
            if (RequestedCommandQueue.Count > 0)
                return RequestedCommandQueue.Dequeue();
            else
                return null;
        }
        public void ClearRequestedCommand()
        {
            RequestedCommandQueue.Clear();
        }
        public byte[] GetSetActCurrent(int cookie)
        {
            string strcmd = "ga";
            strcmd += " " + cookie.ToString() + " " + "actCurrent System-control" + " " + "\r\n";
            RequestCommand msg = new RequestCommand();
            msg.cookie = (byte)cookie;
            msg.command = CommandMessage.GET_ACT_CURRENT;
            SetRequestedCommand(msg);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetSetActCurrentPacketSize(int cookie)
        {
            string strcmd = "ga";
            strcmd += " " + cookie.ToString() + " " + "actCurrent System-control" + " " + "\r\n";
            return strcmd.Length;
        }
        public void ReceiveSetActCurrnet(string data)
        {
            if (data != string.Empty)
                ActureCurrent = Convert.ToDouble(data);
            else
                ActureCurrent = 0;
        }
        public void ReceiveSetActCurrnet(byte[] data)
        {
            string strdata = Encoding.Default.GetString(data);
            ActureCurrent = Convert.ToDouble(strdata);
        }
        public byte[] GetSetActPower(int cookie)
        {
            string strcmd = "ga";
            strcmd += " " + cookie.ToString() + " " + "actPower System-control" + " " + "\r\n";
            RequestCommand msg = new RequestCommand();
            msg.cookie = (byte)cookie;
            msg.command = CommandMessage.GET_ACT_POWER;
            SetRequestedCommand(msg);            
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetSetActPowerPacketSize(int cookie)
        {
            string strcmd = "ga";
            strcmd += " " + cookie.ToString() + " " + "actPower System-control" + " " + "\r\n";
            return strcmd.Length;
        }
        public void ReceiveSetActPower(string data)
        {
            if (data != string.Empty)
                ActurePower = Convert.ToDouble(data);
            else
                ActurePower = 0;
        }
        public void ReceiveSetActPower(byte[] data)
        {
            string strdata = Encoding.Default.GetString(data);
            ActurePower = Convert.ToDouble(strdata);
        }
        public byte[] GetSetTargetCurrent(int cookie,double value)
        {
            string strcmd = "sa";
            strcmd += " " + cookie.ToString() + " " + "setCurrent System-control" + " " + value.ToString("F1") + " " + "\r\n";
            RequestCommand msg = new RequestCommand();
            msg.cookie = (byte)cookie;
            msg.command = CommandMessage.SET_CURRENT;
            SetRequestedCommand(msg);            
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetSetTargetCurrentPacketSize(int cookie, double value)
        {
            string strcmd = "sa";
            strcmd += " " + cookie.ToString() + " " + "setCurrent System-control" + " " + value.ToString("F1") + " " + "\r\n";
            return strcmd.Length;
        }
        public byte[] GetSetCurrent(int cookie)
        {
            string strcmd = "ga";
            strcmd += " " + cookie.ToString() + " " + "setCurrent System-control" + " " + "\r\n";
            RequestCommand msg = new RequestCommand();
            msg.cookie = (byte)cookie;
            msg.command = CommandMessage.GET_SETCURRENT;
            SetRequestedCommand(msg);            
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetSetCurrentPacketSize(int cookie)
        {
            string strcmd = "ga";
            strcmd += " " + cookie.ToString() + " " + "setCurrent System-control" + " " + "\r\n";
            return strcmd.Length;
        }
        public void ReceiveSetTargetCurrent(string data)
        {
            if (data != string.Empty)
                SetCurrent = Convert.ToDouble(data);
            else
                SetCurrent = 0;
        }
        public void ReceiveSetTargetCurrent(byte[] data)
        {
            string strdata = Encoding.Default.GetString(data);
            SetCurrent = Convert.ToDouble(strdata);
        }
        public byte[] GetSetTargetPower(int cookie, double value)
        {
            string strcmd = "sa";
            strcmd += " " + cookie.ToString() + " " + "setPower System-control" + " " + value.ToString("F1") + " " + "\r\n";
            RequestCommand msg = new RequestCommand();
            msg.cookie = (byte)cookie;
            msg.command = CommandMessage.SET_POWER;
            SetRequestedCommand(msg);            
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetSetTargetPowerPacketSize(int cookie, double value)
        {
            string strcmd = "sa";
            strcmd += " " + cookie.ToString() + " " + "setPower System-control" + " " + value.ToString("F1") + " " + "\r\n";
            return strcmd.Length;
        }
        public byte[] GetSetPower(int cookie)
        {
            string strcmd = "ga";
            strcmd += " " + cookie.ToString() + " " + "setPower System-control" + " " + "\r\n";
            RequestCommand msg = new RequestCommand();
            msg.cookie = (byte)cookie;
            msg.command = CommandMessage.GET_SETPOWER;
            SetRequestedCommand(msg);            
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetSetPowerPacketSize(int cookie)
        {
            string strcmd = "ga";
            strcmd += " " + cookie.ToString() + " " + "setPower System-control" + " " + "\r\n";
            return strcmd.Length;
        }
        public void ReceiveSetTargetPower(string data)
        {
            if (data != string.Empty)
                SetPower = Convert.ToDouble(data);
            else
                SetPower = 0;
        }
        public void ReceiveSetTargetPower(byte[] data)
        {
            string strdata = Encoding.Default.GetString(data);
            SetPower = Convert.ToDouble(strdata);
        }
        public byte[] GetSetWatchdogTimeout(int cookie, double value)
        {
            string strcmd = "sa";
            strcmd += " " + cookie.ToString() + " " + "WatchDogTimeout System-control" + " " + value.ToString("F1") + " " + "\r\n";
            RequestCommand msg = new RequestCommand();
            msg.cookie = (byte)cookie;
            msg.command = CommandMessage.SET_WATCHDOGTIMEOUT;
            SetRequestedCommand(msg);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetSetWatchdogTimeoutPacketSize(int cookie, double value)
        {
            string strcmd = "sa";
            strcmd += " " + cookie.ToString() + " " + "WatchDogTimeout System-control" + " " + value.ToString("F1") + " " + "\r\n";
            return strcmd.Length;
        }
        public byte[] GetWatchdogTimeout(int cookie)
        {
            string strcmd = "ga";
            strcmd += " " + cookie.ToString() + " " + "WatchDogTimeout System-control" + " " + "\r\n";
            RequestCommand msg = new RequestCommand();
            msg.cookie = (byte)cookie;
            msg.command = CommandMessage.GET_WATCHDOGTIMEOUT;
            SetRequestedCommand(msg);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetWatchdogTimeoutPacketSize(int cookie)
        {
            string strcmd = "ga";
            strcmd += " " + cookie.ToString() + " " + "WatchDogTimeout System-control" + " " + "\r\n";
            return strcmd.Length;
        }
        public void ReceiveSetWatchdogTimeout(string data)
        {
            if (data != string.Empty)
                WatchDogTimeout = Convert.ToUInt32(data);
            else
                WatchDogTimeout = 0;
        }
        public void ReceiveSetWatchdogTimeout(byte[] data)
        {
            string strdata = Encoding.Default.GetString(data);
            WatchDogTimeout = Convert.ToUInt32(strdata);
        }
        public byte[] GetLaserOn(int cookie, bool OnOff)
        {
            string strcmd = "sa";
            if (OnOff)
            {
                strcmd += " " + cookie.ToString() + " " + "LaserOn RTI-IF" + " " + "1" + " " + "\r\n";
            }
            else
            {
                strcmd += " " + cookie.ToString() + " " + "LaserOn RTI-IF" + " " + "0" + " " + "\r\n";
            }
            RequestCommand msg = new RequestCommand();
            msg.cookie = (byte)cookie;
            msg.command = CommandMessage.SET_LASERON;
            SetRequestedCommand(msg);            
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetLaserOnPacketSize(int cookie)
        {
            string strcmd = "sa";
            strcmd += " " + cookie.ToString() + " " + "LaserOn RTI-IF" + " " + "1" + " " + "\r\n";            
            return strcmd.Length;
        }
        public byte[] GetReadLaserOn(int cookie)
        {
            string strcmd = "ga";
            strcmd += " " + cookie.ToString() + " " + "LaserOn RTI-IF" + " " + "\r\n";
            RequestCommand msg = new RequestCommand();
            msg.cookie = (byte)cookie;
            msg.command = CommandMessage.GET_LASERON;
            SetRequestedCommand(msg);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetReadLaserOnPacketSize(int cookie)
        {
            string strcmd = "ga";
            strcmd += " " + cookie.ToString() + " " + "LaserOn RTI-IF" + " " + "\r\n";
            return strcmd.Length;
        }
        public void ReceiveSetReadLaserOn(string data)
        {
            if (data != string.Empty)
                LaserOn = Convert.ToBoolean(data);
            else
                LaserOn = false;
        }
        public void ReceiveSetReadLaserOn(byte[] data)
        {
            string strdata = Encoding.Default.GetString(data);
            LaserOn = Convert.ToBoolean(strdata);
        }
        public byte[] GetPilotOn(int cookie, bool OnOff)
        {
            string strcmd = "sa";
            if (OnOff)
            {
                strcmd += " " + cookie.ToString() + " " + "Pilot RTI-IF" + " " + "1" + " " + "\r\n";
            }
            else
            {
                strcmd += " " + cookie.ToString() + " " + "Pilot RTI-IF" + " " + "0" + " " + "\r\n";
            }
            RequestCommand msg = new RequestCommand();
            msg.cookie = (byte)cookie;
            msg.command = CommandMessage.SET_PILOTON;
            SetRequestedCommand(msg);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetPilotOnPacketSize(int cookie)
        {
            string strcmd = "sa";
            strcmd += " " + cookie.ToString() + " " + "Pilot RTI-IF" + " " + "1" + " " + "\r\n";
            return strcmd.Length;
        }
        public byte[] GetReadPilotOn(int cookie)
        {
            string strcmd = "ga";
            strcmd += " " + cookie.ToString() + " " + "Pilot RTI-IF" + " " + "\r\n";            
            RequestCommand msg = new RequestCommand();
            msg.cookie = (byte)cookie;
            msg.command = CommandMessage.GET_PILOTON;
            SetRequestedCommand(msg);            
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetReadPilotOnPacketSize(int cookie)
        {
            string strcmd = "ga";
            strcmd += " " + cookie.ToString() + " " + "Pilot RTI-IF" + " " + "\r\n";
            return strcmd.Length;
        }
        public void ReceiveSetReadPilotOn(string data)
        {
            if (data != string.Empty)
                PilotLaser = Convert.ToBoolean(data);
            else
                PilotLaser = false;
        }
        public void ReceiveSetReadPilotOn(byte[] data)
        {
            string strdata = Encoding.Default.GetString(data);
            PilotLaser = Convert.ToBoolean(strdata);
        }
        public byte[] GetPowerOn(int cookie, bool OnOff)
        {
            string strcmd = "sa";
            if (OnOff)
            {
                strcmd += " " + cookie.ToString() + " " + "PowerOn RTI-IF" + " " + "1" + " " + "\r\n";
            }
            else
            {
                strcmd += " " + cookie.ToString() + " " + "PowerOn RTI-IF" + " " + "0" + " " + "\r\n";
            }
            RequestCommand msg = new RequestCommand();
            msg.cookie = (byte)cookie;
            msg.command = CommandMessage.SET_POWERON;
            SetRequestedCommand(msg);            
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetPowerOnPacketSize(int cookie)
        {
            string strcmd = "sa";
            strcmd += " " + cookie.ToString() + " " + "PowerOn RTI-IF" + " " + "1" + " " + "\r\n";
            return strcmd.Length;
        }
        public byte[] GetReadPowerOn(int cookie)
        {
            string strcmd = "ga";
            strcmd += " " + cookie.ToString() + " " + "LaserOn RTI-IF" + " " + "\r\n";
            RequestCommand msg = new RequestCommand();
            msg.cookie = (byte)cookie;
            msg.command = CommandMessage.GET_POWERON;
            SetRequestedCommand(msg);            
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetReadPowerOnPacketSize(int cookie)
        {
            string strcmd = "ga";
            strcmd += " " + cookie.ToString() + " " + "LaserOn RTI-IF" + " " + "\r\n";
            return strcmd.Length;
        }
        public void ReceiveSetReadPowerOn(string data)
        {
            if (data != string.Empty)
                PowerOn = Convert.ToBoolean(data);
            else
                PowerOn = false;
        }
        public void ReceiveSetReadPowerOn(byte[] data)
        {
            string strdata = Encoding.Default.GetString(data);
            PowerOn = Convert.ToBoolean(strdata);
        }
        public int GetResetOnPacketSize(int cookie)
        {
            string strcmd = "sa";
            strcmd += " " + cookie.ToString() + " " + "Reset RTI-IF" + " " + "1" + " " + "\r\n";
            return strcmd.Length;
        }
        public byte[] GetResetOn(int cookie, bool OnOff)
        {
            string strcmd = "sa";
            if (OnOff)
            {
                strcmd += " " + cookie.ToString() + " " + "Reset RTI-IF" + " " + "1" + " " + "\r\n";
            }
            else
            {
                strcmd += " " + cookie.ToString() + " " + "Reset RTI-IF" + " " + "0" + " " + "\r\n";
            }
            RequestCommand msg = new RequestCommand();
            msg.cookie = (byte)cookie;
            msg.command = CommandMessage.SET_RESET;
            SetRequestedCommand(msg);            
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public byte[] GetUserOutput(int cookie)
        {
            string strcmd = "ga";            
            strcmd += " " + cookie.ToString() + " " + "UserOutput RTI-IF \r\n";
            RequestCommand msg = new RequestCommand();
            msg.cookie = (byte)cookie;
            msg.command = CommandMessage.GET_USEROUTPUT;
            SetRequestedCommand(msg);            
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetUserOutputPacketSize(int cookie)
        {
            string strcmd = "ga";
            strcmd += " " + cookie.ToString() + " " + "UserOutput RTI-IF \r\n";
            return strcmd.Length;
        }
        public void ReceiveSetUserOutput(byte data)
        {
            //LaserStatus.SetData(data);
            LaserStatus.Bit8 = (byte)(data - '0');
        }
        public void ReceiveSetUserOutput(string data)
        {
            if (data != "")
                LaserStatus.SetData((byte)int.Parse(data, System.Globalization.NumberStyles.HexNumber));                //LaserStatus.Bit8 = (byte)int.Parse(data, System.Globalization.NumberStyles.HexNumber);

            else
                LaserStatus.SetData(0);                                                                                 //LaserStatus.Bit8 = 0;
        }
        public byte[] GetSoftwareVersion(int cookie)
        {
            string strcmd = "ga";
            strcmd += " " + cookie.ToString() + " " + "SWVerstion Controller \r\n";
            RequestCommand msg = new RequestCommand();
            msg.cookie = (byte)cookie;
            msg.command = CommandMessage.GET_SW_VERSION;
            SetRequestedCommand(msg);            
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetSoftwareVersionPacketSize(int cookie)
        {
            string strcmd = "ga";
            strcmd += " " + cookie.ToString() + " " + "SWVerstion Controller \r\n";
            return strcmd.Length;
        }
        public void ReceiveSetSoftwareVersion(string data)
        {
            strSoftwareVersion = data;
        }
        public void ReceiveSetSoftwareVersion(int data)
        {
            strSoftwareVersion = data.ToString();
        }
        public byte[] GetSerialNumber(int cookie)
        {
            string strcmd = "ga";
            strcmd += " " + cookie.ToString() + " " + "Serial Controller \r\n";
            RequestCommand msg = new RequestCommand();
            msg.cookie = (byte)cookie;
            msg.command = CommandMessage.GET_SERIAL;
            SetRequestedCommand(msg);            
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetSerialNumberPacketSize(int cookie)
        {
            string strcmd = "ga";
            strcmd += " " + cookie.ToString() + " " + "Serial Controller \r\n";
            return strcmd.Length;
        }
        public void ReceiveSetSerialNumber(string data)
        {
            strSerialNumber = data;
        }
        public void ReceiveSetSerialNumber(int data)
        {
            strSerialNumber = data.ToString();
        }
        public byte[] GetLogin(int cookie,string user, string pass)
        {
            string strcmd = "li";
            strcmd += " "+cookie.ToString() + " " + user + " " + pass + " " + "0 \r\n";
            RequestCommand msg = new RequestCommand();
            msg.cookie = (byte)cookie;
            msg.command = CommandMessage.LOGIN;
            SetRequestedCommand(msg);            
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetLoginPacketSize(int cookie, string user, string pass)
        {
            string strcmd = "li";
            strcmd += " " + cookie.ToString() + " " + user + " " + pass + " " + "0 \r\n";
            return strcmd.Length;
        }
        public byte[] GetLogout(int cookie)
        {
            string strcmd = "lo";
            strcmd += " " + cookie.ToString() + " " + " \r\n";
            RequestCommand msg = new RequestCommand();
            msg.cookie = (byte)cookie;
            msg.command = CommandMessage.LOGOUT;
            SetRequestedCommand(msg);            
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetLogoutPacketSize(int cookie)
        {
            string strcmd = "lo";
            strcmd += " " + cookie.ToString() + " " + " \r\n";
            return strcmd.Length;
        }
        #region Additianl Functions
        public ushort GetCRCValue(byte[] seedData)
        {
            if (seedData == null)
                return 0;

            ushort crcValue = CRC.CRC16(seedData, seedData.Length);

            return crcValue;
        }

        public byte[] AddCRCValue(byte[] seedData)
        {
            if (seedData == null)
                return null;

            if (seedData.Length < HEADER_LENGTH)
                return null;

            byte[] mCheckData = new byte[seedData.Length - 4];

            Buffer.BlockCopy(seedData, 0, mCheckData, 0, 16);
            seedData[18] = seedData[19] = 0x00;

            if (seedData.Length > HEADER_LENGTH)
                Buffer.BlockCopy(seedData, HEADER_LENGTH, mCheckData, 16, seedData.Length - HEADER_LENGTH);

            ushort crcValue = CRC.CRC16(mCheckData, mCheckData.Length);

            seedData[16] = (byte)(crcValue & 0x00FF);
            seedData[17] = (byte)((crcValue >> 8) & 0xFF);

            return seedData;
        }

        public bool CheckCRCValue(byte[] seedData)
        {
            bool mRetValue = false;

            if (seedData == null)
                return false;

            if (seedData.Length < HEADER_LENGTH)
                return false;

            byte[] mCheckData = new byte[seedData.Length - 4];

            Buffer.BlockCopy(seedData, 0, mCheckData, 0, 16);

            if (seedData.Length > HEADER_LENGTH)
                Buffer.BlockCopy(seedData, HEADER_LENGTH, mCheckData, 16, seedData.Length - HEADER_LENGTH);

            ushort crcValue = CRC.CRC16(mCheckData, mCheckData.Length);

            if ((seedData[16] == (byte)(crcValue & 0x00FF)) && (seedData[17] == (byte)((crcValue >> 8) & 0xFF)))
                mRetValue = true;

            return mRetValue;
        }
        #endregion Additianl Functions

    }
}
