using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoherentCompactMini.SerialCommunication.DataProcessor;
using System.Runtime.InteropServices;

namespace CoherentCompactMini.SerialCommunication.Data
{
    public class CompactMiniData
    {
        private const int PERCENTRANGE1000 = 1000;
        private const int PERCENTRANGE100 = 100;
        private const int TEMPERATURERANGE100 = 100;
        private const int TEMPERATURERANGE50 = 50;
        private const int MULTIPLY_TIME_VALUE = 10;
        private const int DIODE_TEMPERATURE_MIN = 200;
        private const int DIODE_TEMPERATURE_MAX = 300;
        private const double D_10_MULTIFLY = 10D;

        public enum ErrorInfo
        {
            PeltierCooler_OverTemp = 0,
            PeltierCooler_UnterTemp,
            Fiber_OverTemp,
            FiberPlugged,
            InterLock,
            FatalError_PeltierOverTemp,
            FatalError_PeltierUnderTemp,
            Diode_OverTemp,
            Diode_UnderTemp,
            USB_Heartbeat
        }
        public enum LaserModeInfo
        {
            PeltierCooler = 0,
            CurrentSet,
            LaserControl,
            Pulser,
            VT100
        }
        public enum StatusInfo
        {
            PowerOn = 0,
            LaserOn,
            Error,
            FatalError,
            Emission
        }
        public enum CommandMessage
        {
            NONE = 0,
            PRC,
            PRH,
            PRL,
            PWC,
            PWH,
            PWL,
            RDE,
            RDH,
            RDK,
            RDM,
            RDN,
            RDO,
            RDQ,
            RDS,
            RDT,
            RDV,
            RDW,
            RDX,
            STI,
            STL,
            STO,
            STP,
            STR,
            TRD,
            WRH,
            WRMC,
            WRML,
            WRMT,
            WRS,
            WRV,
            WRW
        }
        public bool LaserOn { get; set; }
        public bool PowerOn { get; set; }
        public bool PositionLaserOn { get; set; }
        public bool HeartbeatOn { get; set; }
        public bool ResetError { get; set; }
        public string strSerialNumber { get; set; }
        public string srtHWVersion { get; set; }
        public string strFirmwareVersion { get; set; }
        public string ModelName { get; set; }
        public int OperationMode { get; set; }                
        public int LaserPowerRatio { get; set; }
        public int PulseCount { get; set; }
        public int LaserOnTime { get; set; }
        public double InternalPowerRatio { get; set; }
        public double PulseHighTime { get; set; }
        public double PulseLowTime { get; set; }
        public double LaserDiodeTemperature { get; set; }
        public double FanTemperature { get; set; }
        public double CoolPlateTemperature { get; set; }
        public double FiberTemperature { get; set; }
        public double PeltierTemperature { get; set; }
        public IO_16Bit ErrorStatus { get; set; } = new IO_16Bit();
        public IO_8Bit LaserOpMode { get; set; } = new IO_8Bit();
        public IO_8Bit Status { get; set; } = new IO_8Bit();
        public Queue<CommandMessage> RequestedCommandQueue = new Queue<CommandMessage>();
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
        public CompactMiniData()
        {
        }
        ~CompactMiniData()
        {

        }
        public void SetRequestedCommand(CommandMessage cmd)
        {
            RequestedCommandQueue.Enqueue(cmd);
        }
        public CommandMessage GetRequestedCommand()
        {
            if (RequestedCommandQueue.Count > 0)
                return RequestedCommandQueue.Dequeue();
            else
                return CommandMessage.NONE;
        }
        public void ClearRequestedCommand()
        {
            RequestedCommandQueue.Clear();
        }
        public void ReceiveSetPulseCount(string data)
        {
            if (data != "")
                PulseCount = Convert.ToInt32(data);
            else
                PulseCount = 999999;
        }
        public void ReceiveSetPulseCount(int data)
        {
            if (data > 0)
                PulseCount = data;
            else
                PulseCount = 999999;
        }
        public int GetPulseCountPacketSize()
        {
            string strcmd = "Prc\r";
            return strcmd.Length;
        }
        public byte[] GetPulseCount()
        {
            string strcmd = "Prc\r";
            SetRequestedCommand(CommandMessage.PRC);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetSetPulseCountPacketSize(int data)
        {
            string strcmd = "Pwc";
            strcmd += data.ToString() + "\r";
            return strcmd.Length;
        }
        public byte[] GetSetPulseCount(int data)
        {
            string strcmd = "Pwc";
            strcmd += data.ToString() + "\r";
            SetRequestedCommand(CommandMessage.PWC);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public byte[] GetSetPulseCount(string data)
        {
            string strcmd = "Pwc";
            strcmd += data + "\r";
            SetRequestedCommand(CommandMessage.PWC);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public void ReceiveSetPulseHighTime(string data)
        {
            if (data != "")
                PulseHighTime = Convert.ToDouble(data);
            else
                PulseHighTime = 0D;
        }
        public void ReceiveSetPulseHighTime(int data)
        {
            if (data > 0)
                PulseHighTime = Convert.ToDouble(data);
            else
                PulseHighTime = 0D;
        }
        public int GetPulseHighTimePacketSize()
        {
            string strcmd = "Prh\r";            
            return strcmd.Length;
        }
        public byte[] GetPulseHighTime()
        {
            string strcmd = "Prh\r";
            SetRequestedCommand(CommandMessage.PRH);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetSetPulseHighTimePacketSize(int data)
        {
            string strcmd = "Pwh";
            strcmd += data.ToString() + "\r";
            return strcmd.Length;
        }
        public byte[] GetSetPulseHighTime(int data)
        {
            string strcmd = "Pwh";
            strcmd += data.ToString() + "\r";
            SetRequestedCommand(CommandMessage.PWH);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public byte[] GetSetPulseHighTime(string data)
        {
            string strcmd = "Pwh";
            strcmd += data + "\r";
            SetRequestedCommand(CommandMessage.PWH);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public void ReceiveSetPulseLowTime(string data)
        {
            if (data != "")
                PulseLowTime = Convert.ToDouble(data);
            else
                PulseLowTime = 0D;
        }
        public void ReceiveSetPulseLowTime(int data)
        {
            if (data > 0)
                PulseLowTime = Convert.ToDouble(data);
            else
                PulseLowTime = 0D;
        }
        public int GetPulseLowTimePacketSize()
        {
            string strcmd = "Prl\r";
            
            return strcmd.Length;
        }
        public byte[] GetPulseLowTime()
        {
            string strcmd = "Prl\r";
            SetRequestedCommand(CommandMessage.PRL);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetSetPulseLowTimePacketSize(int data)
        {
            string strcmd = "Pwl";
            strcmd += data.ToString() + "\r";
            return strcmd.Length;
        }
        public byte[] GetSetPulseLowTime(int data)
        {
            string strcmd = "Pwl";
            strcmd += data.ToString() + "\r";
            SetRequestedCommand(CommandMessage.PWL);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public byte[] GetSetPulseLowTime(string data)
        {
            string strcmd = "Pwl";
            strcmd += data + "\r";
            SetRequestedCommand(CommandMessage.PWL);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public void ReceiveSetErrorStatus(string data)
        {
            if (data != "")
                ErrorStatus.Bit16 = (ushort)int.Parse(data, System.Globalization.NumberStyles.HexNumber); //Convert.ToUInt16(data);
            else
                ErrorStatus.Bit16 = 0;
        }
        public void ReceiveSetErrorStatus(UInt16 data)
        {
            if (data > 0)
                ErrorStatus.Bit16 = data;
            else
                ErrorStatus.Bit16 = 0;
        }
        public int GetErrorStatusPacketSize()
        {
            string strcmd = "Rde\r";

            return strcmd.Length;
        }
        public byte[] GetErrorStatus()
        {
            string strcmd = "Rde\r";
            SetRequestedCommand(CommandMessage.RDE);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public void ReceiveSetLaserDiodeTemperature(string data)
        {
            if (data != "")
                LaserDiodeTemperature = Convert.ToDouble(data);
            else
                LaserDiodeTemperature = 0;
        }
        public void ReceiveSetLaserDiodeTemperature(byte data)
        {
            if (data > 0)
                LaserDiodeTemperature = Convert.ToDouble(data);
            else
                LaserDiodeTemperature = 0;
        }
        public int GetLaserDiodeTemperaturePacketSize()
        {
            string strcmd = "Rdh\r";

            return strcmd.Length;
        }
        public byte[] GetLaserDiodeTemperature()
        {
            string strcmd = "Rdh\r";
            SetRequestedCommand(CommandMessage.RDH);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public void ReceiveSetInternalPower(string data)
        {
            if (data != "")
                InternalPowerRatio = Convert.ToDouble(data) / D_10_MULTIFLY;
            else
                InternalPowerRatio = 0;
        }
        public void ReceiveSetInternalPower(int data)
        {
            if ((data > 0) && (data <= PERCENTRANGE1000))
                InternalPowerRatio = (double)(data / D_10_MULTIFLY);
            else
                InternalPowerRatio = 0;
        }
        public int GetInternalPowerPacketSize()
        {
            string strcmd = "Rdk\r";
            return strcmd.Length;
        }
        public byte[] GetInternalPower()
        {
            string strcmd = "Rdk\r";
            SetRequestedCommand(CommandMessage.RDK);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public void ReceiveSetOperationMode(string data)
        {
            if (data != "")
                LaserOpMode.Bit8 = (byte)int.Parse(data, System.Globalization.NumberStyles.HexNumber);
            else
                LaserOpMode.Bit8 = 0;
        }
        public void ReceiveSetOperationMode(byte data)
        {            
            LaserOpMode.Bit8 = data;         
        }
        public int GetOperationModePacketSize()
        {
            string strcmd = "Rdm\r";

            return strcmd.Length;
        }
        public byte[] GetOperationMode()
        {
            string strcmd = "Rdm\r";
            SetRequestedCommand(CommandMessage.RDM);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public void ReceiveSetSerialNumber(string data)
        {            
            strSerialNumber = data;            
        }
        public void ReceiveSetSerialNumber(int data)
        {
            strSerialNumber = data.ToString();
        }
        public int GetSerialNumberPacketSize()
        {
            string strcmd = "Rdn\r";

            return strcmd.Length;
        }
        public byte[] GetSerialNumber()
        {
            string strcmd = "Rdn\r";
            SetRequestedCommand(CommandMessage.RDN);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public void ReceiveSetLaserPower(string data)
        {
            if (data != "")
                LaserPowerRatio = Convert.ToInt32(data);
            else
                LaserPowerRatio = 0;
        }
        public void ReceiveSetLaserPower(int data)
        {
            if ((data > 0) && (data <= PERCENTRANGE100))
                LaserPowerRatio = data;
            else
                LaserPowerRatio = 0;
        }
        public int GetLaserPowerPacketSize()
        {
            string strcmd = "Rdo\r";

            return strcmd.Length;
        }
        public byte[] GetLaserPower()
        {
            string strcmd = "Rdo\r";
            SetRequestedCommand(CommandMessage.RDO);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public void ReceiveSetFanTemperature(string data)
        {
            if (data != "")
                FanTemperature = Convert.ToDouble(data) / D_10_MULTIFLY;
            else
                FanTemperature = 0;
        }
        public void ReceiveSetFanTemperature(int data)
        {
            if ((data > 0) && (data <= PERCENTRANGE1000))
                FanTemperature = (double)(data / D_10_MULTIFLY);
            else
                FanTemperature = 0;
        }
        public int GetFanTemperaturePacketSize()
        {
            string strcmd = "Rdq\r";

            return strcmd.Length;
        }
        public byte[] GetFanTemperature()
        {
            string strcmd = "Rdq\r";
            SetRequestedCommand(CommandMessage.RDQ);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public void ReceiveSetCoolingPlateTemperature(string data)
        {
            if (data != "")
                CoolPlateTemperature = Convert.ToDouble(data);
            else
                CoolPlateTemperature = 0;
        }
        public void ReceiveSetCoolingPlateTemperature(int data)
        {
            if ((data > 0) && (data <= TEMPERATURERANGE50))
                CoolPlateTemperature = (double)(data);
            else
                CoolPlateTemperature = 0;
        }
        public int GetCoolingPlateTemperaturePacketSize()
        {
            string strcmd = "Rds\r";
            return strcmd.Length;
        }
        public byte[] GetCoolingPlateTemperature()
        {
            string strcmd = "Rds\r";
            SetRequestedCommand(CommandMessage.RDS);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public void ReceiveSetFiberTemperature(string data)
        {
            if (data != "")
                FiberTemperature = Convert.ToDouble(data);
            else
                FiberTemperature = 0;
        }
        public void ReceiveSetFiberTemperature(int data)
        {
            if ((data > 0) && (data <= TEMPERATURERANGE100))
                FiberTemperature = (double)(data);
            else
                FiberTemperature = 0;
        }
        public int GetFiberTemperaturePacketSize()
        {
            string strcmd = "Rdt\r";
            return strcmd.Length;
        }
        public byte[] GetFiberTemperature()
        {
            string strcmd = "Rdt\r";
            SetRequestedCommand(CommandMessage.RDT);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public void ReceiveSetFirmwareVersion(string data)
        {
            strFirmwareVersion = data;
        }
        public void ReceiveSetFirmwareVersion(int data)
        {
            strFirmwareVersion = data.ToString();
        }
        public int GetFirmwareVersionPacketSize()
        {
            string strcmd = "Rdv\r";
            return strcmd.Length;
        }
        public byte[] GetFirmwareVersion()
        {
            string strcmd = "Rdv\r";
            SetRequestedCommand(CommandMessage.RDV);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public void ReceiveSetPeltierTemperature(string data)
        {
            if (data != "")
                PeltierTemperature = Convert.ToDouble(data);
            else
                PeltierTemperature = 0;
        }
        public void ReceiveSetPeltierTemperature(int data)
        {
            if ((data > 0) && (data <= TEMPERATURERANGE50))
                PeltierTemperature = (double)(data);
            else
                PeltierTemperature = 0;
        }
        public int GetPeltierTemperaturePacketSize()
        {
            string strcmd = "Rdw\r";
            return strcmd.Length;
        }
        public byte[] GetPeltierTemperature()
        {
            string strcmd = "Rdw\r";
            SetRequestedCommand(CommandMessage.RDW);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public void ReceiveSetStatus(string data)
        {
            if (data != "")
                Status.Bit8 = (byte)int.Parse(data, System.Globalization.NumberStyles.HexNumber);
            else
                Status.Bit8 = 0;
        }
        public void ReceiveSetStatus(byte data)
        {
            Status.Bit8 = data;
        }
        public int GetLaserStatusPacketSize()
        {
            string strcmd = "Rdx\r";
            return strcmd.Length;
        }
        public byte[] GetLaserStatus()
        {
            string strcmd = "Rdx\r";
            SetRequestedCommand(CommandMessage.RDX);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetSetLaserPowerPacketSize(int data)
        {
            string strcmd = "Sti";
            strcmd += data.ToString() + "\r";
            return strcmd.Length;
        }
        public byte[] GetSetLaserPower(int data)
        {
            if ((data > 0) && (data <= PERCENTRANGE1000))
            {                
                string strcmd = "Sti";
                strcmd += data.ToString() + "\r";
                SetRequestedCommand(CommandMessage.STI);
                return Encoding.UTF8.GetBytes(strcmd);
            }
            else
                return null;
        }
        public byte[] GetSetLaserPower(string data)
        {            
            string strcmd = "Sti";
            strcmd += data + "\r";
            SetRequestedCommand(CommandMessage.STI);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetSetLaserOnPacketSize()
        {
            string strcmd = "Stl1\r";
            return strcmd.Length;
        }
        public byte[] GetSetLaserOn(int On)
        {
            if ((On == 0) || (On == 1))
            {                
                string strcmd = "Stl";
                strcmd += On.ToString() + "\r";
                SetRequestedCommand(CommandMessage.STL);
                return Encoding.UTF8.GetBytes(strcmd);
            }
            else
                return null;
        }
        public byte[] GetSetLaserOn(bool On)
        {            
            if (On)
            {                
                string strcmd = "Stl1\r";
                SetRequestedCommand(CommandMessage.STL);
                return Encoding.UTF8.GetBytes(strcmd);
            }
            else
            {
                string strcmd = "Stl0\r";
                SetRequestedCommand(CommandMessage.STL);
                return Encoding.UTF8.GetBytes(strcmd);
            }
        }
        public int GetSetPositionLaserOnPacketSize()
        {
            string strcmd = "Sto1\r";
            return strcmd.Length;
        }
        public byte[] GetSetPositioningLaserOn(int On)
        {
            if ((On == 0) || (On == 1))
            {                
                string strcmd = "Sto";
                strcmd += On.ToString() + "\r";
                SetRequestedCommand(CommandMessage.STO);
                return Encoding.UTF8.GetBytes(strcmd);
            }
            else
                return null;
        }
        public byte[] GetSetPositioningLaserOn(bool On)
        {         
            if (On)
            {
                string strcmd = "Sto1\r";
                SetRequestedCommand(CommandMessage.STO);
                return Encoding.UTF8.GetBytes(strcmd);
            }
            else
            {
                string strcmd = "Sto0\r";
                SetRequestedCommand(CommandMessage.STO);
                return Encoding.UTF8.GetBytes(strcmd);
            }
        }
        public int GetSetPowerOnPacketSize()
        {
            string strcmd = "Stp1\r";
            return strcmd.Length;
        }
        public byte[] GetSetPowerOn(int On)
        {
            if ((On == 0) || (On == 1))
            {                
                string strcmd = "Stp";
                strcmd += On.ToString() + "\r";
                SetRequestedCommand(CommandMessage.STP);
                return Encoding.UTF8.GetBytes(strcmd);
            }
            else
                return null;
        }
        public byte[] GetSetPowerOn(bool On)
        {            
            if (On)
            {
                string strcmd = "Stp1\r";
                SetRequestedCommand(CommandMessage.STP);
                return Encoding.UTF8.GetBytes(strcmd);
            }
            else
            {
                string strcmd = "Stp0\r";
                SetRequestedCommand(CommandMessage.STP);
                return Encoding.UTF8.GetBytes(strcmd);
            }
        }
        public int GetSetErrorResetPacketSize()
        {
            string strcmd = "Str\r";
            return strcmd.Length;
        }
        public byte[] GetSetErrorReset()
        {            
            string strcmd = "Str\r";
            SetRequestedCommand(CommandMessage.STR);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public void ReceiveSetLaserOnTime(string data)
        {
            if (data != "")
            {
                string subdata = string.Empty;
                subdata = data.Substring(0, data.Length - 1);
                LaserOnTime = (int)(int.Parse(subdata, System.Globalization.NumberStyles.HexNumber) * MULTIPLY_TIME_VALUE);                
            }                
            else
                LaserOnTime = 0;
        }
        public void ReceiveSetLaserOnTime(int data)
        {
            LaserOnTime = data * MULTIPLY_TIME_VALUE;
        }
        public int GetLaserOnTimePacketSize()
        {
            string strcmd = "Trd\r";
            return strcmd.Length;
        }
        public byte[] GetLaserOnTime()
        {            
            string strcmd = "Trd\r";
            SetRequestedCommand(CommandMessage.TRD);
            return Encoding.UTF8.GetBytes(strcmd);
        }
        public int GetSetHeartbeatOnPacketSize()
        {
            string strcmd = "Wrh1\r";
            return strcmd.Length;
        }
        public byte[] GetSetHeartbeatOn(int On)
        {
            if ((On == 0) || (On == 1))
            {                
                string strcmd = "Wrh";
                strcmd += On.ToString() + "\r";
                SetRequestedCommand(CommandMessage.WRH);
                return Encoding.UTF8.GetBytes(strcmd);
            }
            else
                return null;
        }
        public byte[] GetSetHeartbeatOn(bool On)
        {            
            if (On)
            {
                string strcmd = "Wrh1\r";
                SetRequestedCommand(CommandMessage.WRH);
                return Encoding.UTF8.GetBytes(strcmd);
            }
            else
            {
                string strcmd = "Wrh0\r";
                SetRequestedCommand(CommandMessage.WRH);
                return Encoding.UTF8.GetBytes(strcmd);
            }
        }
        public int GetLaserCurrentSetPacketSize()
        {
            string strcmd = "Wrmc0\r";
            return strcmd.Length;
        }
        public byte[] GetLaserCurrentSet(int set)
        {
            if ((set == 0) || (set == 1))
            {                
                string strcmd = "Wrmc";
                strcmd += set.ToString() + "\r";
                SetRequestedCommand(CommandMessage.WRMC);
                return Encoding.UTF8.GetBytes(strcmd);
            }
            else
                return null;
        }
        public byte[] GetLaserCurrentSet(bool set)
        {            
            if (set)
            {
                string strcmd = "Wrmc1\r";          // external interface control
                SetRequestedCommand(CommandMessage.WRMC);
                return Encoding.UTF8.GetBytes(strcmd);
            }
            else
            {
                string strcmd = "Wrmc0\r";          // Internal control
                SetRequestedCommand(CommandMessage.WRMC);
                return Encoding.UTF8.GetBytes(strcmd);
            }
        }
        public int GetSetLaserControlPacketSize()
        {
            string strcmd = "Wrml1\r";
            return strcmd.Length;
        }
        public byte[] GetSetLaserControl(int set)
        {
            if ((set == 0) || (set == 1))
            {                
                string strcmd = "Wrml";
                strcmd += set.ToString() + "\r";
                SetRequestedCommand(CommandMessage.WRML);
                return Encoding.UTF8.GetBytes(strcmd);
            }
            else
                return null;
        }
        public byte[] GetSetLaserControl(bool set)
        {            
            if (set)
            {
                string strcmd = "Wrml1\r";          // external interface control
                SetRequestedCommand(CommandMessage.WRML);
                return Encoding.UTF8.GetBytes(strcmd);
            }
            else
            {
                string strcmd = "Wrml0\r";          // Internal control
                SetRequestedCommand(CommandMessage.WRML);
                return Encoding.UTF8.GetBytes(strcmd);
            }
        }
        public int GetSetLaserOperationModePacketSize()
        {
            string strcmd = "Wrmt0\r";
            return strcmd.Length;
        }
        public byte[] GetSetLaserOperationMode(int set)
        {
            if ((set == 0) || (set == 1))
            {                
                string strcmd = "Wrmt";
                strcmd += set.ToString() + "\r";    // CW/Pulse Mode Select
                SetRequestedCommand(CommandMessage.WRMT);
                return Encoding.UTF8.GetBytes(strcmd);
            }
            else
                return null;
        }
        public byte[] GetSetLaserOperationMode(bool set)
        {            
            if (set)
            {
                string strcmd = "Wrmt1\r";          // Pulse
                SetRequestedCommand(CommandMessage.WRMT);
                return Encoding.UTF8.GetBytes(strcmd);
            }
            else
            {
                string strcmd = "Wrmt0\r";          // CW
                SetRequestedCommand(CommandMessage.WRMT);
                return Encoding.UTF8.GetBytes(strcmd);
            }
        }
        public int GetSetPeltierTemperaturePacketSize(int set)
        {
            string strcmd = "Wrs";
            strcmd += set.ToString() + "\r";    // CW/Pulse Mode Select
            return strcmd.Length;
        }
        public byte[] GetSetPeltierTemperatureSet(int set)
        {
            if ((set >= DIODE_TEMPERATURE_MIN) && (set <= DIODE_TEMPERATURE_MAX))
            {                
                string strcmd = "Wrs";
                strcmd += set.ToString() + "\r";    // CW/Pulse Mode Select
                SetRequestedCommand(CommandMessage.WRS);
                return Encoding.UTF8.GetBytes(strcmd);
            }
            else
                return null;
        }
        public byte[] GetSetPeltierTemperatureSet(double set)
        {
            if ((set >= (DIODE_TEMPERATURE_MIN / D_10_MULTIFLY)) && (set <= (DIODE_TEMPERATURE_MAX / D_10_MULTIFLY)))
            {                
                string strcmd = "Wrs";
                int data = (int)(set * 10);
                strcmd += data.ToString() + "\r";
                SetRequestedCommand(CommandMessage.WRS);
                return Encoding.UTF8.GetBytes(strcmd);
            }
            else
                return null;
        }
        public int GetSetVT100ModePacketSize(int set)
        {
            string strcmd = "Wrv";
            strcmd += set.ToString() + "\r";    // VT100 Mode On/Off            
            return strcmd.Length;
        }
        public byte[] GetSetVT100Mode(int set)
        {
            if ((set == 0) || (set == 1))
            {                
                string strcmd = "Wrv";
                strcmd += set.ToString() + "\r";    // VT100 Mode On/Off
                SetRequestedCommand(CommandMessage.WRV);
                return Encoding.UTF8.GetBytes(strcmd);
            }
            else
                return null;
        }
        public byte[] GetSetVT100Mode(bool set)
        {            
            if (set)
            {
                string strcmd = "Wrv1\r";          // VT 100 Mode On
                SetRequestedCommand(CommandMessage.WRV);
                return Encoding.UTF8.GetBytes(strcmd);
            }
            else
            {
                string strcmd = "Wrv0\r";          // VT 100 Mode Off
                SetRequestedCommand(CommandMessage.WRV);
                return Encoding.UTF8.GetBytes(strcmd);
            }
        }
        public byte[] GetSetPasswordAccess(int set)
        {
            if ((set >= 0) && (set <= 9999))
            {                
                string strcmd = "Wrw";
                strcmd += set.ToString() + "\r";    // Password Access
                SetRequestedCommand(CommandMessage.WRW);
                return Encoding.UTF8.GetBytes(strcmd);
            }
            else
                return null;
        }
    }
}
