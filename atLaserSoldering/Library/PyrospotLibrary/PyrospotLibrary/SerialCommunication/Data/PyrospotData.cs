using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PyrospotControlLibrary.SerialCommunication.DataProcessor;
using System.Runtime.InteropServices;

namespace PyrospotControlLibrary.SerialCommunication.Data
{
    public class PyrospotData
    {       
        public enum OPERATION_COMMAND_MAP
        {
            // Base Address 11~29
            Reset = 10,
            EMG,
            AlarmReset,
            CommandPositionReset,
            ActualPositionReset,
            Step0RunpJogp,
            Step1RunnJogn,
            MoveAbsolute,
            MoveRelative,
            StartIndexMode,
            StartProgramMode,
            Home,
            Stop,
            SlowStop,
            Pause,
            ProgramStop,
            ProgramDelete,
            Reserved1,
            Reserved2,
            MoveOverride
        }
        public enum PRODUCT_INFORMATION_MAP
        {
            // Base Address 00001 ~ 00999
            HardwareStatus = 0,
            OrderNumber = 2,
            SerialNumber = 4,
            Description = 6,
            DeviceInsideTemperature = 22,
            MaxRegisterInsideTemperature = 23,
            BaudrateAndAddress = 24,
            Optics = 25,
            DiameterEyepieceParam = 26,
            FibercableType = 27,
            FibercableLength = 28
        }
        public enum MONITOR_DATA_MAP1
        {
            // Base Address 300256 ~ 300275
            MeasurementStatus = 256,
            MeasuremnetTemperature,
            Emissivity,
            ResponeTime,
            MaxMinStorageDetectionmode,
            TransmissionFactor,
            AmbientTemperature            
        }
        public enum MONITOR_DATA_MAP2
        {
            // Base Address 300275 ~ 300319
            RecalibrationFactor = 275,
            TemperatureRange,
            SubTemperatureRange,
            ModeAnalogueOutput
        }
        public enum MONITOR_DATA_MAP3
        {
            // Base Address 300320 ~ 
            SwitchOnTemperatureSWOut1 = 320,
            SwitchOffTemperatureSWOut1,
            SwitchOnTemperatureSWOut2,
            SwitchOffTemperatureSWOut2,
            StatusSwitchOutput
        }
        public byte DeviceIDCount { get; set; }
        public IO_16Bit OutputIO { get; set; }
        public string strSerialNumber { get; set; }
        public string HWVersion { get; set; }
        public string SWVersion { get; set; }
        public string ModelName { get; set; }
        public int OperationMode { get; set; }
        public int PresentTemperature { get; set; }

        public IO_16Bit[] AlarmError1 = null;
        public IO_16Bit[] AlarmError2 = null;
        public IO_16Bit[] InfoStatus1 = null;
        public IO_16Bit[] InfoStatus2 = null;
        public IO_16Bit[] OutputStaus = null;

        public ModbusRTU mMotionCommunication = new ModbusRTU();

        private int[] CurrentCommandData = null;
        private int[] CurrentProdInfomation = null;
        private int[] CurrentMonitor1Data = null;
        private int[] CurrentMonitor2Data = null;
        private int[] CurrentMonitor3Data = null;

        public byte[] DrvID = null;
        public int GetCurrentCommandData(OPERATION_COMMAND_MAP cmddata) => CurrentCommandData[(int)cmddata - Enum.GetValues(typeof(OPERATION_COMMAND_MAP)).Cast<int>().Min()];
        public int GetCurrentProdInfo(PRODUCT_INFORMATION_MAP prodinfo) => CurrentProdInfomation[(int)prodinfo - Enum.GetValues(typeof(PRODUCT_INFORMATION_MAP)).Cast<int>().Min()];
        public int GetCurrentMonitor1Data(MONITOR_DATA_MAP1 motdata) => CurrentMonitor1Data[(int)motdata - Enum.GetValues(typeof(MONITOR_DATA_MAP1)).Cast<int>().Min()];
        public int GetCurrentMonitor2Data(MONITOR_DATA_MAP2 motdata) => CurrentMonitor2Data[(int)motdata - Enum.GetValues(typeof(MONITOR_DATA_MAP2)).Cast<int>().Min()];
        public int GetCurrentMonitor3Data(MONITOR_DATA_MAP3 motdata) => CurrentMonitor3Data[(int)motdata - Enum.GetValues(typeof(MONITOR_DATA_MAP3)).Cast<int>().Min()];


        public List<int[]> PyrospotElement = new List<int[]>();
        public Dictionary<int, List<int[]>> PyrospotProduct = new Dictionary<int, List<int[]>>();        
        public PyrospotDatas _mPyrospotDatas = null;

        public event Action<PyrospotDatas> PyrospotMonitorEvent;

        public class PyrospotDatas
        {
            public int _Id { get; set; }
            public int[] _CurrentMotion1Datas = new int[Enum.GetValues(typeof(MONITOR_DATA_MAP1)).Length];
            public int[] _CurrentMotion2Datas = new int[Enum.GetValues(typeof(MONITOR_DATA_MAP2)).Length];
            public int[] _CurrentMotion3Datas = new int[Enum.GetValues(typeof(MONITOR_DATA_MAP3)).Length];
        }
        public class IO_8Bit
        {
            public byte Bit8;
            public int B0 { get { return Bit8 & (1 << 0); } set { Bit8 |= (1 << 0); } }
            public int B1 { get { return Bit8 & (1 << 1); } set { Bit8 |= (1 << 1); } }
            public int B2 { get { return Bit8 & (1 << 2); } set { Bit8 |= (1 << 2); } }
            public int B3 { get { return Bit8 & (1 << 3); } set { Bit8 |= (1 << 3); } }
            public int B4 { get { return Bit8 & (1 << 4); } set { Bit8 |= (1 << 4); } }
            public int B5 { get { return Bit8 & (1 << 5); } set { Bit8 |= (1 << 5); } }
            public int B6 { get { return Bit8 & (1 << 6); } set { Bit8 |= (1 << 6); } }
            public int B7 { get { return Bit8 & (1 << 7); } set { Bit8 |= (1 << 7); } }
            public void SetData(byte data)
            {
                Bit8 = data;
            }
        }
        public class IO_16Bit
        {
            public UInt16 Bit16;
            public int B0 { get { return Bit16 & (1 << 0); } set { Bit16 |= (1 << 0); } }
            public int B1 { get { return Bit16 & (1 << 1); } set { Bit16 |= (1 << 1); } }
            public int B2 { get { return Bit16 & (1 << 2); } set { Bit16 |= (1 << 2); } }
            public int B3 { get { return Bit16 & (1 << 3); } set { Bit16 |= (1 << 3); } }
            public int B4 { get { return Bit16 & (1 << 4); } set { Bit16 |= (1 << 4); } }
            public int B5 { get { return Bit16 & (1 << 5); } set { Bit16 |= (1 << 5); } }
            public int B6 { get { return Bit16 & (1 << 6); } set { Bit16 |= (1 << 6); } }
            public int B7 { get { return Bit16 & (1 << 7); } set { Bit16 |= (1 << 7); } }
            public int B8 { get { return Bit16 & (1 << 8); } set { Bit16 |= (1 << 8); } }
            public int B9 { get { return Bit16 & (1 << 9); } set { Bit16 |= (1 << 9); } }
            public int B10 { get { return Bit16 & (1 << 10); } set { Bit16 |= (1 << 10); } }
            public int B11 { get { return Bit16 & (1 << 11); } set { Bit16 |= (1 << 11); } }
            public int B12 { get { return Bit16 & (1 << 12); } set { Bit16 |= (1 << 12); } }
            public int B13 { get { return Bit16 & (1 << 13); } set { Bit16 |= (1 << 13); } }
            public int B14 { get { return Bit16 & (1 << 14); } set { Bit16 |= (1 << 14); } }
            public int B15 { get { return Bit16 & (1 << 15); } set { Bit16 |= (1 << 15); } }

            public void SetData(UInt16 data)
            {
                //if (data.Length < 2)
                //    return;

                //Bit16 = BitConverter.ToUInt16(data, 0);
                Bit16 = data;
            }
        }
        public PyrospotData()
        {
            CurrentCommandData = new int[Enum.GetValues(typeof(OPERATION_COMMAND_MAP)).Length];
            CurrentProdInfomation = new int[Enum.GetValues(typeof(PRODUCT_INFORMATION_MAP)).Length];
            CurrentMonitor1Data = new int[Enum.GetValues(typeof(MONITOR_DATA_MAP1)).Length];
            CurrentMonitor2Data = new int[Enum.GetValues(typeof(MONITOR_DATA_MAP2)).Length];
            CurrentMonitor3Data = new int[Enum.GetValues(typeof(MONITOR_DATA_MAP3)).Length];
            PyrospotElement.Clear();
            PyrospotProduct.Clear();
            _mPyrospotDatas = new PyrospotDatas();
        }
        ~PyrospotData()
        {
            CurrentCommandData = null;
            CurrentProdInfomation = null;
            CurrentMonitor1Data = null;
            CurrentMonitor2Data = null;
            CurrentMonitor3Data = null;
            if (PyrospotElement != null)
                PyrospotElement.Clear();
            if (PyrospotProduct != null)
                PyrospotProduct.Clear();
        }
        public void SetIDNumber(int IdNum, byte[] ID)
        {
            if ((CurrentCommandData == null) && (CurrentMonitor1Data == null) && (CurrentMonitor2Data == null) && (CurrentMonitor3Data == null) && (CurrentProdInfomation == null)) return;

            List<int[]> listElement = new List<int[]>();

            PyrospotElement.Add(CurrentCommandData);            
            PyrospotElement.Add(CurrentMonitor1Data);
            PyrospotElement.Add(CurrentMonitor2Data);
            PyrospotElement.Add(CurrentMonitor3Data);
            PyrospotElement.Add(CurrentProdInfomation);

            if (IdNum > 0)
            {
                AlarmError1 = new IO_16Bit[IdNum];
                AlarmError2 = new IO_16Bit[IdNum];
                InfoStatus1 = new IO_16Bit[IdNum];
                InfoStatus2 = new IO_16Bit[IdNum];
                OutputStaus = new IO_16Bit[IdNum];
                DeviceIDCount = (byte)IdNum;
                DrvID = new byte[DeviceIDCount];
                for (int i = 0; i < IdNum; i++)
                {
                    listElement.Clear();
                    listElement.Add(new int[Enum.GetValues(typeof(OPERATION_COMMAND_MAP)).Length]);                    
                    listElement.Add(new int[Enum.GetValues(typeof(MONITOR_DATA_MAP1)).Length]);
                    listElement.Add(new int[Enum.GetValues(typeof(MONITOR_DATA_MAP2)).Length]);
                    listElement.Add(new int[Enum.GetValues(typeof(MONITOR_DATA_MAP3)).Length]);
                    listElement.Add(new int[Enum.GetValues(typeof(PRODUCT_INFORMATION_MAP)).Length]);                    
                    PyrospotProduct.Add(ID[i], listElement);

                }
            }
            else
            {
                DeviceIDCount = 1;
                DrvID = new byte[1];
                PyrospotProduct.Add(1, PyrospotElement);
            }
        }
        //public void SetRequestedCommand(CommandMassege cmd)
        //{
        //    RequestedCommandQueue.Enqueue(cmd);
        //}
        //public CommandMassege GetRequestedCommand()
        //{
        //    if (RequestedCommandQueue.Count > 0)
        //        return RequestedCommandQueue.Dequeue();
        //    else
        //        return CommandMassege.MSG_NONE;
        //}
        //public void ClearRequestedCommand()
        //{
        //    RequestedCommandQueue.Clear();
        //}
         public void ReceiveSetOperationCommand(byte[] data)
        {
            try
            {
                if (data == null) return;
                if (data.Length < DataProcessor.ModbusRTU.MINIMUM_RESPONSE_SIZE) return;

                byte nID = data[0];
                if (PyrospotProduct.ContainsKey((int)nID))
                {
                    if (data[1] == (byte)DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleCoil)
                    {
                        short addr = BitConverter.ToInt16(data, 2);
                        if ((addr <= ((ushort)Enum.GetValues(typeof(OPERATION_COMMAND_MAP)).Length)))
                        {
                            CurrentCommandData[addr] = BitConverter.ToInt16(data, 4);
                        }
                    }
                    else if (data[1] == (byte)DataProcessor.ModbusRTU.ReadFunctionCodes.ReadCoils)
                    {
                        short count = data[2];
                        for (int i = 0; i < count; i++)
                            CurrentCommandData[i] = (data[3 + i] & 0x0000ffff);
                    }
                    Array.Copy(CurrentCommandData, 0, PyrospotElement[0], 0, CurrentCommandData.Length);

                    PyrospotProduct[nID] = PyrospotElement;
                }
            }
            catch (Exception)
            {
                ;
            }
        }
        public byte[] GetSettingOperationCommand(byte nID, OPERATION_COMMAND_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);
            //SetRequestedCommand(CommandMassege.MSG_OPERATI0N_CMD);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadCoils,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingOperationCommands(byte nID)
        {
            ushort startAddr = Convert.ToUInt16((ushort)Enum.GetValues(typeof(OPERATION_COMMAND_MAP)).Cast<int>().Min());
            //SetRequestedCommand(CommandMassege.MSG_OPERATI0N_CMD);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadCoils,
                startAddr, (ushort)((Enum.GetValues(typeof(OPERATION_COMMAND_MAP)).Length / 1) + 0));
        }
  
        public void ReceiveSetProductInfo(byte[] data)
        {
            try
            {
                if (data == null) return;
                if (data.Length < DataProcessor.ModbusRTU.MINIMUM_RESPONSE_SIZE) return;

                byte nID = data[0];

                if (PyrospotProduct.ContainsKey((int)nID))
                {
                    if (data[1] == (byte)DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputRegisters)
                    {
                        short count = data[2];
                        if (count != 0) count /= 2;
                        for (int i = 0; i < count; i++)
                            CurrentProdInfomation[i] = (mMotionCommunication.GetShortValueFromTwoBytes(data, 3 + (i * 2)) & 0x0000ffff);
                        Array.Copy(CurrentProdInfomation, 0, PyrospotElement[4], 0, CurrentProdInfomation.Length);

                        PyrospotProduct[nID] = PyrospotElement;
                    }
                }
            }
            catch (Exception)
            {
                ;
            }
        }
        public byte[] GetSettingProductInfo(byte nID, PRODUCT_INFORMATION_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);
            //SetRequestedCommand(CommandMassege.MSG_PRODUCT_INFO);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputRegisters,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingProductInfos(byte nID)
        {
            ushort startAddr = Convert.ToUInt16((ushort)Enum.GetValues(typeof(PRODUCT_INFORMATION_MAP)).Cast<int>().Min());
            //SetRequestedCommand(CommandMassege.MSG_PRODUCT_INFO);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputRegisters,
                startAddr, (ushort)Enum.GetValues(typeof(PRODUCT_INFORMATION_MAP)).Length);
        }
        public void ReceiveSetMonitor1Data(byte[] data)
        {
            try
            {
                if (data == null) return;
                //if (data.Length < DataProcessor.ModbusRTU.MINIMUM_RESPONSE_SIZE) return;

                byte nID = data[0];

                if (PyrospotProduct.ContainsKey((int)nID))
                {
                    //Array.Clear(AiCElement[2], 0, CurrentMotionData.Length);
                    if (data[1] == (byte)DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputRegisters)
                    {
                        short count = data[2];
                        if (count != 0) count /= 2;
                        if (count == CurrentMonitor1Data.Length)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                CurrentMonitor1Data[i] = mMotionCommunication.GetShortValueFromTwoBytes(data, 3 + (i * 2), false) & 0x0000ffff;
                            }
                            Array.Copy(CurrentMonitor1Data, 0, _mPyrospotDatas._CurrentMotion1Datas, 0, CurrentMonitor1Data.Length);

                            _mPyrospotDatas._Id = nID;
                            //MotionMonitorEvent?.Invoke(_mAiCMotionDatas);                            
                        }
                    }
                }
            }
            catch (Exception)
            {
                ;
            }
        }
        public byte[] GetSettingMonitor1Data(byte nID, MONITOR_DATA_MAP1 func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);
            //SetRequestedCommand(CommandMassege.MSG_MONITOR_DATA);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingMonitor1Datas(byte nID)
        {
            ushort startAddr = Convert.ToUInt16((ushort)Enum.GetValues(typeof(MONITOR_DATA_MAP1)).Cast<int>().Min());
            //SetRequestedCommand(CommandMassege.MSG_MONITOR_DATA);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters,
                startAddr, (ushort)Enum.GetValues(typeof(MONITOR_DATA_MAP1)).Length);
        } 
    }
}
