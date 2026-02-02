using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiCControlLibrary.SerialCommunication.DataProcessor;
using System.Runtime.InteropServices;

namespace AiCControlLibrary.SerialCommunication.Data
{
    public class AiCData
    {       
        public enum OUTPUT_CONTROL_MAP
        {
            // Base Address 1~10
            Output0 = 0,
            Output1,
            Output2,
            Output3,
            Output4,
            Output5,
            Output6,
            Output7,
            Output8,
            Output9,
        }
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
            ProgramDelete
            //Reserved1,            // Version 2.0 over
            //Reserved2,
            //MoveOverride
        }
        public enum OPERATION_SETUP_MAP
        {
            // Base Address 31~100
            LimitStopMode = 30,
            Scurve,
            InputFilterTimeSelect,
            SoftwareLimitEnable,
            PowerOnHomming,
            PowerOnProgramStart,
            StopCurrentFixedMode,
            StartSignalLevel,
            Step0SignalLevel,
            Step1SignalLevel,
            Step2SignalLevel,
            Step3SignalLevel,
            Step4SignalLevel,
            Step5SignalLevel,
            MD0HMD0SignalLevel,
            MD1HMD1SignalLevel,
            PauseSignalLevel,
            StopSignalLevel,
            EMGSignalLevel,
            HomeSignalLevel,
            AlarmResetSignalLevel,
            ServoOnOffSignalLevel,
            LimitSignalLevel,
            Input0SignalLevel,
            Input1SignalLevel,
            Input2SignalLevel,
            Input3SignalLevel,
            Input4SignalLevel,
            Input5SignalLevel,
            Input6SignalLevel,
            Input7SignalLevel,
            Input8SignalLevel,
            SDSignalLevel,
            Reserved1,
            MotorDirection,
            OutputMode,
            Input8PinMode,            // Version 2.0 over
            ServoOff,
            BrakeOff
        }
        public enum EXTERNAL_INPUT_MAP
        {
            //Base Address 100001 ~ 100100.
            Start = 0,
            Step0,
            Step1,
            Step2,
            Step3,
            Step4,
            Step5,
            MD0HMD0,
            MD1HMD1,
            Pause,
            Stop,
            EMG,
            Home,
            ORG,
            AlarmReset,
            ServoOnOff,
            Limit_P,
            Limit_N,
            Input0,
            Input1,
            Input2,
            Input3,
            Input4,
            Input5,
            Input6,
            Input7,
            Input8,
            SD,
            OutputServoOn,
            OutputAlarm,
            OutputInPosition,
            OutputCompare1,
            OutputCompare2
        }
        public enum PRODUCT_INFORMATION_MAP
        {
            // Base Address 300001 ~ 300999
            SerialNumberL = 100,
            SerialNumberH,
            HardwareVersion,
            SoftwareVersion,
            ModelName1,
            ModelName2,
            ModelName3,
            ModelName4,
            ModelName5,
            ModelName6
        }
        public enum MONITOR_DATA_MAP
        {
            // Base Address 301000 ~ 301050
            OperationMode = 999,
            TargetPositionH,
            TargetPositionL,
            ActualPositionH,
            ActualPositionL,
            TargetVelocityH,
            TargetVelocityL,
            ActualVelocityH,
            ActualVelocityL,
            RPM,
            ActiveProgramStep,
            ErrorStatus,
            AlarmStatus,
            OperationStatus,
            DriveInputStatus,
            DriveOutputStatus
        }
        public enum OPERATION_SETUP1_MAP
        {
            // Base Address 400001 ~ 402601
            StartVelocityH = 2,
            StartVelocityL,
            MaxVelocityH,
            MaxVelocityL,
            AccelerationTime,
            DecelerationTime,
            TargetPositionH,
            TargetPositionL,
            ProgramStartAddress,
            SetHomeMode
        }
        public enum OPERATION_SETUP2_MAP
        {
            // Set Operation Group 405001 ~ 405050
            SystemStatus1 = 5000,
            SystemStatus2,
            SystemStatus3
        }
        public enum HOME_OPERATION_MAP
        {
            // Homing Parameter Group, 405051 ~ 405100
            HomingMaxVelocityH = 5050,
            HomingMaxVelocityL,
            HomingStartVelocityH,
            HomingStartVelocityL,
            HomingAccelationTime,
            HomingDeccelationTime,
            HomingDirection,
            HomingOffsetH,
            HomingOffsetL,
            HomingZeroPositionH,
            HomingZeroPositionL,
            HomingSensorSignalLevel,
            HomingTorqueRatio
            //HomingFinishedSet             // Version 2.0 over
        }
        public enum OPERATION_PARAMETER_MAP
        {
            // Operation Parameter Group, 405101 ~ 405200
            StartVelocity1H = 5102,
            StartVelocity1L,
            StartVelocity2H,
            StartVelocity2L,
            StartVelocity3H,
            StartVelocity3L,
            StartVelocity4H,
            StartVelocity4L,
            StartVelocity5H,
            StartVelocity5L,
            MaxVelocity1H,
            MaxVelocity1L,
            MaxVelocity2H,
            MaxVelocity2L,
            MaxVelocity3H,
            MaxVelocity3L,
            MaxVelocity4H,
            MaxVelocity4L,
            MaxVelocity5H,
            MaxVelocity5L,
            WaitTime1,
            WaitTime2,
            WaitTime3,
            WaitTime4,
            WaitTime5,
            AccelerationTime1,
            AccelerationTime2,
            AccelerationTime3,
            AccelerationTime4,
            AccelerationTime5,
            DeccelerationTime1,
            DeccelerationTime2,
            DeccelerationTime3,
            DeccelerationTime4,
            DeccelerationTime5,
            SCurveAccTime,
            SoftwarePLimitH,
            SoftwarePLimitL,
            SoftwareNLimitH,
            SoftwareNLimitL,
            OnTime1,
            OnTime2,
            OnTime3,
            OnTime4,
            OnTime5,
            Compare1Mode,
            Compare1Pulse,
            Compare1PriodH,
            Compare1PriodL,
            Compare1PositionH,
            Compare1PositionL,
            Compare2Mode,
            Compare2Pulse,
            Compare2PriodH,
            Compare2PriodL,
            Compare2PositionH,
            Compare2PositionL,
            Resolution,
            StopResponse,
            StopCurrent,
            MotorGain,
            Inposition,
            PGain,
            IGain
        }
        public enum COMM_PARAM_MAP
        {
            // Communication Parameter Group, 405201 ~ 405250
            BaudRate = 5200,
            Parity,
            StopBit,
            ResponseTime
        }
        public enum ErrorInfo
        {
            OverCurrent = 0,
            OverSpeed,
            PositionDeviation,
            OverLoad,
            OverHeat,
            MotorConn,
            EncoderConn,
            RegeneratedVoltage,
            MotorAline,
            InputSpeed,
            SupplyVoltage,
            Inposition,
            Memory,
            EmergencyStop,
            ProgramMode,
            IndexMode,
            HomeMode = 0,
            SoftwareLimitP,
            SoftwareLimitN,
            HardwareLimitP,
            HardwareLimitN,
            OverLoadAlarm,
            OverrideAlarm
        }
        public enum CommandMassege
        {
            MSG_NONE = 0,
            MSG_OUTPUT,
            MSG_OPERATI0N_CMD,
            MSG_OPERATION_SET,
            MSG_EXTERNAL_INPUT,
            MSG_PRODUCT_INFO,
            MSG_MONITOR_DATA,
            MSG_OPERATION_SETUP1,
            MSG_OPERATION_SETUP2,
            MSG_HOME_OPERATION,
            MSG_OPERATION_PARAM,
            MSG_COMM_PARAM            
        }
        public byte DeviceIDCount { get; set; }
        public bool Reset { get; set; }
        public bool EMG { get; set; }
        public bool AlarmReset { get; set; }
        public bool ClearTargetPosition { get; set; }
        public bool ClearActualPosition { get; set; }
        public bool Step0 { get; set; }
        public bool Step1 { get; set; }
        public bool MoveAbsolute { get; set; }
        public bool MoveRelative { get; set; }
        public bool StartIndex { get; set; }
        public bool StartProgram { get; set; }
        public bool StartHome { get; set; }
        public bool QuickStop { get; set; }
        public bool SlowStop { get; set; }
        public bool Pause { get; set; }
        public bool ProgramModeStop { get; set; }
        public bool ProgramDelete { get; set; }
        public bool StartOverride { get; set; }
        public bool LimitStopMode { get; set; }
        public bool SCurveActive { get; set; }
        public bool InputFilterSelect { get; set; }
        public bool SoftwareLimitActive { get; set; }
        public bool PowerOnHomingEnable { get; set; }
        public bool PowerOnProgramStartEnable { get; set; }
        public bool FixedStopCurrentEnable { get; set; }
        public bool StartSignalLevel { get; set; }
        public bool Step0SignalLevel { get; set; }
        public bool Step1SignalLevel { get; set; }
        public bool Step2SignalLevel { get; set; }
        public bool Step3SignalLevel { get; set; }
        public bool Step4SignalLevel { get; set; }
        public bool Step5SignalLevel { get; set; }
        public bool MD0SignalLevel { get; set; }
        public bool MD1SignalLevel { get; set; }
        public bool PauseSignalLevel { get; set; }
        public bool StopSignalLevel { get; set; }
        public bool EMGSignalLevel { get; set; }
        public bool HomeSignalLevel { get; set; }
        public bool AlarmResetSignalLevel { get; set; }
        public bool ServoOnSignalLevel { get; set; }
        public bool LimitSignalLevel { get; set; }
        public bool Input0SignalLevel { get; set; }
        public bool Input1SignalLevel { get; set; }
        public bool Input2SignalLevel { get; set; }
        public bool Input3SignalLevel { get; set; }
        public bool Input4SignalLevel { get; set; }
        public bool Input5SignalLevel { get; set; }
        public bool Input6SignalLevel { get; set; }
        public bool Input7SignalLevel { get; set; }
        public bool Input8SignalLevel { get; set; }
        public bool SDSignalLevel { get; set; }        
        public bool MotorDirection { get; set; }
        public bool OutputModeSet { get; set; }
        public bool Input8Set { get; set; }
        public bool ServoOff { get; set; }
        public bool BrakeOff { get; set; }
        public IO_16Bit OutputIO { get; set; }
        public string strSerialNumber { get; set; }
        public string HWVersion { get; set; }
        public string SWVersion { get; set; }
        public string ModelName { get; set; }
        public int OperationMode { get; set; }
        public int TargetPosition { get; set; }
        public int ActualPosition { get; set; }
        public int TargetVelocity { get; set; }
        public int InitialVelocity { get; set; }
        public int AccelerationTime { get; set; }
        public int DecelerationTime { get; set; }

        public IO_16Bit[] AlarmError1 = null;
        public IO_16Bit[] AlarmError2 = null;
        public IO_16Bit[] InfoStatus1 = null;
        public IO_16Bit[] InfoStatus2 = null;
        public IO_16Bit[] OutputStaus = null;

        public ModbusRTU mMotionCommunication = new ModbusRTU();

        private int[] CurrentCommandData = null;
        private int[] CurrentSetParam = null;
        private int[] CurrentOutput = null;
        private int[] CurrentExternalIO = null;
        private int[] CurrentProdInfomation = null;
        private int[] CurrentMotionData = null;
        private int[] CurrentOpParam = null;
        private int[] CurrentHomeParam = null;
        private int[] CurrentSetParam1 = null;
        private int[] CurrentSetParam2 = null;
        private int[] CurrentCommParam = null;

        public byte[] DrvID = null;
        public int GetCurrentCommandData(OPERATION_COMMAND_MAP cmddata) => CurrentCommandData[(int)cmddata - Enum.GetValues(typeof(OPERATION_COMMAND_MAP)).Cast<int>().Min()];
        public int GetCurrentSetParam(OPERATION_SETUP_MAP setparam) => CurrentSetParam[(int)setparam - Enum.GetValues(typeof(OPERATION_SETUP_MAP)).Cast<int>().Min()];
        public int GetCurrentOutput(OUTPUT_CONTROL_MAP output) => CurrentOutput[((int)output - Enum.GetValues(typeof(OUTPUT_CONTROL_MAP)).Cast<int>().Min())/8];
        public int GetCurrentExternalIO(EXTERNAL_INPUT_MAP extIO) => CurrentExternalIO[((int)extIO - Enum.GetValues(typeof(EXTERNAL_INPUT_MAP)).Cast<int>().Min())/8];
        public int GetCurrentProdInfo(PRODUCT_INFORMATION_MAP prodinfo) => CurrentProdInfomation[(int)prodinfo - Enum.GetValues(typeof(PRODUCT_INFORMATION_MAP)).Cast<int>().Min()];
        public int GetCurrentMotionData(MONITOR_DATA_MAP motdata) => CurrentMotionData[(int)motdata - Enum.GetValues(typeof(MONITOR_DATA_MAP)).Cast<int>().Min()];
        public int GetCurrentOpParam(OPERATION_PARAMETER_MAP opparam) => CurrentOpParam[(int)opparam - Enum.GetValues(typeof(OPERATION_PARAMETER_MAP)).Cast<int>().Min()];
        public int GetCurrentHomeParam(HOME_OPERATION_MAP home) => CurrentHomeParam[(int)home - Enum.GetValues(typeof(HOME_OPERATION_MAP)).Cast<int>().Min()];
        public int GetCurrentSetParam1(OPERATION_SETUP1_MAP setup1) => CurrentSetParam1[(int)setup1 - Enum.GetValues(typeof(OPERATION_SETUP1_MAP)).Cast<int>().Min()];
        public int GetCurrentSetParam2(OPERATION_SETUP2_MAP setup2) => CurrentSetParam2[(int)setup2 - Enum.GetValues(typeof(OPERATION_SETUP2_MAP)).Cast<int>().Min()];
        public int GetCurrentCommParam(COMM_PARAM_MAP comm) => CurrentCommParam[(int)comm - Enum.GetValues(typeof(COMM_PARAM_MAP)).Cast<int>().Min()];

        public Queue<CommandMassege> RequestedCommandQueue = new Queue<CommandMassege>();
        public List<int[]> AiCElement = new List<int[]>();
        public Dictionary<int, List<int[]>> AiCProduct = new Dictionary<int, List<int[]>>();
        public AiCMotionDatas _mAiCMotionDatas = null;

        public event Action<AiCMotionDatas> MotionMonitorEvent;

        public class AiCMotionDatas
        {
            public int _Id { get; set; }
            public int[] _CurrentDatas = new int[Enum.GetValues(typeof(MONITOR_DATA_MAP)).Length];
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
        public AiCData()
        {
            CurrentCommandData = new int[Enum.GetValues(typeof(OPERATION_COMMAND_MAP)).Length];
            CurrentSetParam = new int[(Enum.GetValues(typeof(OPERATION_SETUP_MAP)).Length / 8) + 1];
            CurrentOutput = new int[(Enum.GetValues(typeof(OUTPUT_CONTROL_MAP)).Length / 8) + 1];
            CurrentExternalIO = new int[(Enum.GetValues(typeof(EXTERNAL_INPUT_MAP)).Length / 8) + 1];
            CurrentProdInfomation = new int[Enum.GetValues(typeof(PRODUCT_INFORMATION_MAP)).Length];
            CurrentMotionData = new int[Enum.GetValues(typeof(MONITOR_DATA_MAP)).Length];
            CurrentOpParam = new int[Enum.GetValues(typeof(OPERATION_PARAMETER_MAP)).Length];        
            CurrentHomeParam = new int[Enum.GetValues(typeof(HOME_OPERATION_MAP)).Length];
            CurrentSetParam1 = new int[Enum.GetValues(typeof(OPERATION_SETUP1_MAP)).Length];
            CurrentSetParam2 = new int[Enum.GetValues(typeof(OPERATION_SETUP2_MAP)).Length];
            CurrentCommParam = new int[Enum.GetValues(typeof(COMM_PARAM_MAP)).Length];
            AiCElement.Clear();
            AiCProduct.Clear();
            _mAiCMotionDatas = new AiCMotionDatas();
        }
        ~AiCData()
        {
            CurrentCommandData = null;
            CurrentSetParam = null;
            CurrentOutput = null;
            CurrentExternalIO = null;
            CurrentProdInfomation = null;
            CurrentMotionData = null;
            CurrentHomeParam = null;
            CurrentSetParam1 = null;
            CurrentSetParam2 = null;
            CurrentCommParam = null;
            if (AiCElement != null)
                AiCElement.Clear();
            if (AiCProduct != null)
                AiCProduct.Clear();
        }
        public void SetIDNumber(int IdNum,byte[] ID)
        {
            if ((CurrentCommandData == null) && (CurrentExternalIO == null) && (CurrentMotionData == null) && (CurrentOutput == null) && (CurrentProdInfomation == null) && (CurrentSetParam == null) && 
                (CurrentHomeParam == null) && (CurrentSetParam1 == null) && (CurrentSetParam2 == null) && (CurrentCommParam == null)) return;

            List<int[]> listElement = new List<int[]>();
            
            AiCElement.Add(CurrentCommandData);
            AiCElement.Add(CurrentExternalIO);
            AiCElement.Add(CurrentMotionData);
            AiCElement.Add(CurrentOutput);
            AiCElement.Add(CurrentProdInfomation);
            AiCElement.Add(CurrentSetParam);
            AiCElement.Add(CurrentHomeParam);
            AiCElement.Add(CurrentSetParam1);
            AiCElement.Add(CurrentSetParam2);
            AiCElement.Add(CurrentCommParam);
            AiCElement.Add(CurrentOpParam);

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
                    listElement.Add(new int[(Enum.GetValues(typeof(EXTERNAL_INPUT_MAP)).Length / 8) + 1]);
                    listElement.Add(new int[Enum.GetValues(typeof(MONITOR_DATA_MAP)).Length]);
                    listElement.Add(new int[(Enum.GetValues(typeof(OUTPUT_CONTROL_MAP)).Length / 8) + 1]);
                    listElement.Add(new int[Enum.GetValues(typeof(PRODUCT_INFORMATION_MAP)).Length]);
                    listElement.Add(new int[(Enum.GetValues(typeof(OPERATION_SETUP_MAP)).Length / 8) + 1]);
                    listElement.Add(new int[Enum.GetValues(typeof(HOME_OPERATION_MAP)).Length]);
                    listElement.Add(new int[Enum.GetValues(typeof(OPERATION_SETUP1_MAP)).Length]);
                    listElement.Add(new int[Enum.GetValues(typeof(OPERATION_SETUP2_MAP)).Length]);
                    listElement.Add(new int[Enum.GetValues(typeof(COMM_PARAM_MAP)).Length]);
                    listElement.Add(new int[Enum.GetValues(typeof(OPERATION_PARAMETER_MAP)).Length]);

                    AiCProduct.Add(ID[i], listElement);
                    AlarmError1[i] = new IO_16Bit();
                    AlarmError2[i] = new IO_16Bit();
                    InfoStatus1[i] = new IO_16Bit();
                    InfoStatus2[i] = new IO_16Bit();
                    OutputStaus[i] = new IO_16Bit();
                    DrvID[i] = ID[i];
                }
            }
            else
            {
                DeviceIDCount = 1;
                DrvID = new byte[1];
                AiCProduct.Add(1, AiCElement);
            }
        }
        public void SetRequestedCommand(CommandMassege cmd)
        {
            RequestedCommandQueue.Enqueue(cmd);            
        }
        public CommandMassege GetRequestedCommand()
        { 
            if (RequestedCommandQueue.Count > 0)
                return RequestedCommandQueue.Dequeue();
            else
                return CommandMassege.MSG_NONE; 
        }
        public void ClearRequestedCommand()
        {
            RequestedCommandQueue.Clear();            
        }
        public void ReceiveSetOutput(byte[] data)
        {
            try
            {
                if (data == null) return;
                if (data.Length < DataProcessor.ModbusRTU.MINIMUM_RESPONSE_SIZE) return;

                byte nID = data[0];
                
                if (AiCProduct.ContainsKey((int)nID))
                {
                    if (data[1] == (byte)DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleCoil)
                    {
                        short addr = BitConverter.ToInt16(data, 2);
                        if ((addr <= ((ushort)Enum.GetValues(typeof(OUTPUT_CONTROL_MAP)).Length)))
                        {
                            CurrentOutput[addr] = BitConverter.ToInt16(data, 4);
                        }
                    }
                    else if (data[1] == (byte)DataProcessor.ModbusRTU.ReadFunctionCodes.ReadCoils)
                    {
                        short count = data[2];
                        if (count == CurrentOutput.Length)
                        {
                            for (int i = 0; i < count; i++)
                                CurrentOutput[i] = (data[3 + i] & 0x0000ffff);
                        }                        
                    }
                    Array.Copy(CurrentOutput, 0, AiCElement[3], 0, CurrentOutput.Length);

                    AiCProduct[nID] = AiCElement;
                }
            }
            catch (Exception)
            {
                ;
            }
        }
        public byte[] GetSettingOutput(byte nID, OUTPUT_CONTROL_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);
            //SetRequestedCommand(CommandMassege.MSG_OUTPUT);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadCoils,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingOutputs(byte nID)
        {
            ushort startAddr = Convert.ToUInt16((ushort)Enum.GetValues(typeof(OUTPUT_CONTROL_MAP)).Cast<int>().Min());
            //SetRequestedCommand(CommandMassege.MSG_OUTPUT);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadCoils,
                startAddr, (ushort)((Enum.GetValues(typeof(OUTPUT_CONTROL_MAP)).Length / 1) + 0));
        }
        public void ReceiveSetOperationCommand(byte[] data)
        {
            try
            {
                if (data == null) return;
                if (data.Length < DataProcessor.ModbusRTU.MINIMUM_RESPONSE_SIZE) return;

                byte nID = data[0];
                if (AiCProduct.ContainsKey((int)nID))
                {
                    if (data[1] == (byte)DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleCoil)
                    {
                        short addr = BitConverter.ToInt16(data, 2);
                        if ((addr <= ((ushort)Enum.GetValues(typeof(OUTPUT_CONTROL_MAP)).Length)))
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
                    Array.Copy(CurrentCommandData, 0, AiCElement[0], 0, CurrentCommandData.Length);

                    AiCProduct[nID] = AiCElement;
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
        public void ReceiveSetOperationSignal(byte[] data)
        {
            try
            {
                if (data == null) return;
                if (data.Length < DataProcessor.ModbusRTU.MINIMUM_RESPONSE_SIZE) return;

                byte nID = data[0];
                if (AiCProduct.ContainsKey((int)nID))
                {
                    if (data[1] == (byte)DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleCoil)
                    {
                        short addr = BitConverter.ToInt16(data, 2);
                        if ((addr <= ((ushort)Enum.GetValues(typeof(OUTPUT_CONTROL_MAP)).Length)))
                        {
                            CurrentSetParam[addr] = BitConverter.ToInt16(data, 4);
                        }
                    }
                    else if (data[1] == (byte)DataProcessor.ModbusRTU.ReadFunctionCodes.ReadCoils)
                    {
                        short count = data[2];
                        for (int i = 0; i < count; i++)
                            CurrentSetParam[i] = (data[3 + i] & 0x0000ffff);
                    }
                    Array.Copy(CurrentSetParam, 0, AiCElement[5], 0, CurrentSetParam.Length);

                    AiCProduct[nID] = AiCElement;
                }
            }
            catch (Exception)
            {
                ;
            }
        }
        public byte[] GetSettingOperationSignal(byte nID, OPERATION_SETUP_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);
            //SetRequestedCommand(CommandMassege.MSG_OPERATION_SET);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadCoils,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingOperationSignals(byte nID)
        {
            ushort startAddr = Convert.ToUInt16((ushort)Enum.GetValues(typeof(OPERATION_SETUP_MAP)).Cast<int>().Min());
            //SetRequestedCommand(CommandMassege.MSG_OPERATION_SET);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadCoils,
                startAddr, (ushort)((Enum.GetValues(typeof(OPERATION_SETUP_MAP)).Length / 1) + 0));
        }
        public void ReceiveSetExternalIO(byte[] data)
        {
            try
            {
                if (data == null) return;
                if (data.Length < DataProcessor.ModbusRTU.MINIMUM_RESPONSE_SIZE) return;

                byte nID = data[0];
                if (AiCProduct.ContainsKey((int)nID))
                {
                    if (data[1] == (byte)DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputs)
                    {
                        short count = data[2];
                        for (int i = 0; i < count; i++)
                            CurrentExternalIO[i] = (data[3 + i] & 0x0000ffff);
                    }
                    Array.Copy(CurrentExternalIO, 0, AiCElement[1], 0, CurrentExternalIO.Length);

                    AiCProduct[nID] = AiCElement;
                    //MotionMonitorEvent?.Invoke(this);
                }
            }
            catch (Exception)
            {
                ;
            }
        }
        public byte[] GetSettingExternalIO(byte nID, EXTERNAL_INPUT_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);
            //SetRequestedCommand(CommandMassege.MSG_EXTERNAL_INPUT);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputs,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingExternalIOs(byte nID)
        {
            ushort startAddr = Convert.ToUInt16((ushort)Enum.GetValues(typeof(EXTERNAL_INPUT_MAP)).Cast<int>().Min());
            //SetRequestedCommand(CommandMassege.MSG_EXTERNAL_INPUT);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputs,
                startAddr, (ushort)((Enum.GetValues(typeof(EXTERNAL_INPUT_MAP)).Length / 1) + 0));
        }
        public void ReceiveSetProductInfo(byte[] data)
        {
            try
            {
                if (data == null) return;
                if (data.Length < DataProcessor.ModbusRTU.MINIMUM_RESPONSE_SIZE) return;

                byte nID = data[0];

                if (AiCProduct.ContainsKey((int)nID))
                {
                    if (data[1] == (byte)DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputRegisters)
                    {
                        short count = data[2];
                        if (count != 0) count /= 2;
                        for (int i = 0; i < count; i++)
                            CurrentProdInfomation[i] = (mMotionCommunication.GetShortValueFromTwoBytes(data, 3 + (i * 2)) & 0x0000ffff);
                        Array.Copy(CurrentProdInfomation, 0, AiCElement[4], 0, CurrentProdInfomation.Length);

                        AiCProduct[nID] = AiCElement;
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
        public void ReceiveSetMotionData(byte[] data)
        {
            try
            {
                if (data == null) return;
                //if (data.Length < DataProcessor.ModbusRTU.MINIMUM_RESPONSE_SIZE) return;

                byte nID = data[0];

                if (AiCProduct.ContainsKey((int)nID))
                {
                    //Array.Clear(AiCElement[2], 0, CurrentMotionData.Length);
                    if (data[1] == (byte)DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputRegisters)
                    {
                        short count = data[2];
                        if (count != 0) count /= 2;
                        if (count == CurrentMotionData.Length)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                CurrentMotionData[i] = mMotionCommunication.GetShortValueFromTwoBytes(data, 3 + (i * 2), false) & 0x0000ffff;
                            }
                            Array.Copy(CurrentMotionData, 0, _mAiCMotionDatas._CurrentDatas, 0, CurrentMotionData.Length);

                            _mAiCMotionDatas._Id = nID;
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
        public byte[] GetSettingMotionData(byte nID, MONITOR_DATA_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);
            //SetRequestedCommand(CommandMassege.MSG_MONITOR_DATA);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputRegisters,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingMotionDatas(byte nID)
        {
            ushort startAddr = Convert.ToUInt16((ushort)Enum.GetValues(typeof(MONITOR_DATA_MAP)).Cast<int>().Min());
            //SetRequestedCommand(CommandMassege.MSG_MONITOR_DATA);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputRegisters,
                startAddr, (ushort)Enum.GetValues(typeof(MONITOR_DATA_MAP)).Length);
        }
        public byte[] GetSettingMotionDatasLowVersion(byte nID)
        {
            ushort startAddr = Convert.ToUInt16((ushort)Enum.GetValues(typeof(MONITOR_DATA_MAP)).Cast<int>().Min());
            //SetRequestedCommand(CommandMassege.MSG_MONITOR_DATA);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputRegisters,
                startAddr, (ushort)(Enum.GetValues(typeof(MONITOR_DATA_MAP)).Length - 1));
        }

        public void ReceiveSetOperationData(byte[] data)
        {
            try
            {
                if (data == null) return;
                if (data.Length < DataProcessor.ModbusRTU.MINIMUM_RESPONSE_SIZE) return;

                byte nID = data[0];

                if (AiCProduct.ContainsKey((int)nID))
                {
                    if (data[1] == (byte)DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters)
                    {
                        short count = data[2];
                        if (count != 0) count /= 2;
                        for (int i = 0; i < count; i++)
                            CurrentSetParam1[i] = (mMotionCommunication.GetShortValueFromTwoBytes(data, 3 + (i * 2)) & 0x0000ffff);
                        Array.Copy(CurrentSetParam1, 0, AiCElement[7], 0, CurrentSetParam1.Length);

                        AiCProduct[nID] = AiCElement;                        
                    }
                }
            }
            catch (Exception)
            {
                ;
            }
        }
        public byte[] GetSettingOperationData(byte nID, OPERATION_SETUP1_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);
            //SetRequestedCommand(CommandMassege.MSG_OPERATION_SETUP1);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingOperationDatas(byte nID)
        {
            ushort startAddr = Convert.ToUInt16((ushort)Enum.GetValues(typeof(OPERATION_SETUP1_MAP)).Cast<int>().Min());
            //SetRequestedCommand(CommandMassege.MSG_OPERATION_SETUP1);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters,
                startAddr, (ushort)Enum.GetValues(typeof(OPERATION_SETUP1_MAP)).Length);
        }
        public void ReceiveSetHomeParameter(byte[] data)
        {
            try
            {
                if (data == null) return;
                if (data.Length < DataProcessor.ModbusRTU.MINIMUM_RESPONSE_SIZE) return;

                byte nID = data[0];

                if (AiCProduct.ContainsKey((int)nID))
                {
                    if (data[1] == (byte)DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters)
                    {
                        short count = data[2];
                        if (count != 0) count /= 2;
                        for (int i = 0; i < count; i++)
                            CurrentHomeParam[i] = (mMotionCommunication.GetShortValueFromTwoBytes(data, 3 + (i * 2)) & 0x0000ffff);
                        Array.Copy(CurrentHomeParam, 0, AiCElement[6], 0, CurrentHomeParam.Length);

                        AiCProduct[nID] = AiCElement;
                        //MotionMonitorEvent?.Invoke(this);
                    }
                }
            }
            catch (Exception)
            {
                ;
            }
        }
        public byte[] GetSettingHomeParameter(byte nID, HOME_OPERATION_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);
            //SetRequestedCommand(CommandMassege.MSG_HOME_OPERATION);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingHomeParameters(byte nID)
        {
            ushort startAddr = Convert.ToUInt16((ushort)Enum.GetValues(typeof(HOME_OPERATION_MAP)).Cast<int>().Min());
            //SetRequestedCommand(CommandMassege.MSG_HOME_OPERATION);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters,
                startAddr, (ushort)Enum.GetValues(typeof(HOME_OPERATION_MAP)).Length);
        }
        public void ReceiveSetOperationParam(byte[] data)
        {
            try
            {
                if (data == null) return;
                if (data.Length < DataProcessor.ModbusRTU.MINIMUM_RESPONSE_SIZE) return;

                byte nID = data[0];

                if (AiCProduct.ContainsKey((int)nID))
                {
                    if (data[1] == (byte)DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters)
                    {
                        short count = data[2];
                        if (count != 0) count /= 2;
                        for (int i = 0; i < count; i++)
                            CurrentOpParam[i] = (mMotionCommunication.GetShortValueFromTwoBytes(data, 3 + (i * 2)) & 0x0000ffff);
                        Array.Copy(CurrentOpParam, 0, AiCElement[10], 0, CurrentOpParam.Length);

                        AiCProduct[nID] = AiCElement;
                    }
                }
            }
            catch (Exception)
            {
                ;
            }
        }
        public byte[] GetSettingOperationParam(byte nID, OPERATION_PARAMETER_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);
            //SetRequestedCommand(CommandMassege.MSG_OPERATION_PARAM);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingOperationParams(byte nID)
        {
            ushort startAddr = Convert.ToUInt16((ushort)Enum.GetValues(typeof(OPERATION_PARAMETER_MAP)).Cast<int>().Min());
            //SetRequestedCommand(CommandMassege.MSG_OPERATION_PARAM);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters,
                startAddr, (ushort)Enum.GetValues(typeof(OPERATION_PARAMETER_MAP)).Length);
        }
        public void ReceiveSetCommunicationData(byte[] data)
        {
            try
            {
                if (data == null) return;
                if (data.Length < DataProcessor.ModbusRTU.MINIMUM_RESPONSE_SIZE) return;

                byte nID = data[0];

                if (AiCProduct.ContainsKey((int)nID))
                {
                    if (data[1] == (byte)DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters)
                    {
                        short count = data[2];
                        if (count != 0) count /= 2;
                        for (int i = 0; i < count; i++)
                            CurrentCommParam[i] = (mMotionCommunication.GetShortValueFromTwoBytes(data, 3 + (i * 2)) & 0x0000ffff);
                        Array.Copy(CurrentCommParam, 0, AiCElement[9], 0, CurrentCommParam.Length);

                        AiCProduct[nID] = AiCElement;
                    }
                }
            }
            catch (Exception)
            {
                ;
            }
        }
        public byte[] GetSettingCommunicationData(byte nID, COMM_PARAM_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);
            //SetRequestedCommand(CommandMassege.MSG_COMM_PARAM);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingCommunicationDatas(byte nID)
        {
            ushort startAddr = Convert.ToUInt16((ushort)Enum.GetValues(typeof(COMM_PARAM_MAP)).Cast<int>().Min());
            //SetRequestedCommand(CommandMassege.MSG_COMM_PARAM);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters,
                startAddr, (ushort)Enum.GetValues(typeof(COMM_PARAM_MAP)).Length);
        }
        public byte[] MoveTargetPositionSendData(byte nID, int TargetPos)
        {
            byte[] pos = new byte[4];
            pos[0] = (byte)((TargetPos >> 24) & 0xff);
            pos[1] = (byte)((TargetPos >> 16) & 0xff);
            pos[2] = (byte)((TargetPos >> 8) & 0xff);
            pos[3] = (byte)(TargetPos & 0xff);
            return mMotionCommunication.GetMessageForMultipleWrite(nID,
                DataProcessor.ModbusRTU.MultipleWriteFunctionCodes.WriteMultipleRegisters,
                (ushort)OPERATION_SETUP1_MAP.TargetPositionH,sizeof(short),pos);
        }
        public byte[] ServoOnOffControl(byte nID,bool control)
        {
            if (control)
            {
                return mMotionCommunication.GetMessageForWrite(nID,
                    DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleCoil,
                    (ushort)OPERATION_SETUP_MAP.ServoOff, (ushort)0x0000);
            }
            else
            {
                return mMotionCommunication.GetMessageForWrite(nID,
                    DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleCoil,
                    (ushort)OPERATION_SETUP_MAP.ServoOff, (ushort)0xff00);
            }
        }
        public byte[] HomeStartCommand(byte nID)
        {
            return mMotionCommunication.GetMessageForWrite(nID,
                DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleCoil,
                (ushort)OPERATION_COMMAND_MAP.Home, (ushort)0xff00);
        }
        public byte[] MoveAbsoluteCommand(byte nID)
        {
            return mMotionCommunication.GetMessageForWrite(nID,
                DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleCoil,
                (ushort)OPERATION_COMMAND_MAP.MoveAbsolute, (ushort)0xff00);
        }
        public byte[] MoveReleativeCommand(byte nID)
        {
            return mMotionCommunication.GetMessageForWrite(nID,
                DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleCoil,
                (ushort)OPERATION_COMMAND_MAP.MoveRelative, (ushort)0xff00);
        }
        public byte[] MoveStopCommand(byte nID)
        {
            return mMotionCommunication.GetMessageForWrite(nID,
                DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleCoil,
                (ushort)OPERATION_COMMAND_MAP.Stop, (ushort)0xff00);
        }
        public byte[] MoveSlowStopCommand(byte nID)
        {
            return mMotionCommunication.GetMessageForWrite(nID,
                DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleCoil,
                (ushort)OPERATION_COMMAND_MAP.SlowStop, (ushort)0xff00);
        }
        public byte[] EmergencyCommand(byte nID)
        {
            return mMotionCommunication.GetMessageForWrite(nID,
                DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleCoil,
                (ushort)OPERATION_COMMAND_MAP.EMG, (ushort)0xff00);
        }
        public byte[] AlarmResetCommand(byte nID)
        {
            return mMotionCommunication.GetMessageForWrite(nID,
                DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleCoil,
                (ushort)OPERATION_COMMAND_MAP.AlarmReset, (ushort)0xff00);
        }
        public byte[] ResetCommand(byte nID)
        {
            return mMotionCommunication.GetMessageForWrite(nID,
                DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleCoil,
                (ushort)OPERATION_COMMAND_MAP.Reset, (ushort)0xff00);
        }
        public byte[] ResetCommandPosition(byte nID)
        {
            return mMotionCommunication.GetMessageForWrite(nID,
                DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleCoil,
                (ushort)OPERATION_COMMAND_MAP.CommandPositionReset, (ushort)0xff00);
        }
        public byte[] ResetActualPosition(byte nID)
        {
            return mMotionCommunication.GetMessageForWrite(nID,
                DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleCoil,
                (ushort)OPERATION_COMMAND_MAP.ActualPositionReset, (ushort)0xff00);
        }
        public byte[] CWJogCommand(byte nID)
        {
            return mMotionCommunication.GetMessageForWrite(nID,
                DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleCoil,
                (ushort)OPERATION_COMMAND_MAP.Step0RunpJogp, (ushort)0xff00);
        }
        public byte[] CCWJogCommand(byte nID)
        {
            return mMotionCommunication.GetMessageForWrite(nID,
                DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleCoil,
                (ushort)OPERATION_COMMAND_MAP.Step1RunnJogn, (ushort)0xff00);
        }
        public byte[] TargetPosClearCommand(byte nID)
        {
            return mMotionCommunication.GetMessageForWrite(nID,
                DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleCoil,
                (ushort)OPERATION_COMMAND_MAP.CommandPositionReset, (ushort)0xff00);
        }
        public byte[] ActualPosClearCommand(byte nID)
        {
            return mMotionCommunication.GetMessageForWrite(nID,
                DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleCoil,
                (ushort)OPERATION_COMMAND_MAP.ActualPositionReset, (ushort)0xff00);
        }
        public byte[] SetEncoderResolution(byte nID, short data)
        {
            return mMotionCommunication.GetMessageForWrite(nID,
                DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleRegister,
                (ushort)OPERATION_PARAMETER_MAP.Resolution, (ushort)data);
        }
        public byte[] SetMoveTargetVelocity(byte nID, int iMaxSpeed)
        {
            byte[] initdata = new byte[4];
            initdata[0] = (byte)((iMaxSpeed >> 24) & 0xff);
            initdata[1] = (byte)((iMaxSpeed >> 16) & 0xff);
            initdata[2] = (byte)((iMaxSpeed >> 8) & 0xff);
            initdata[3] = (byte)((iMaxSpeed & 0xff));
            return mMotionCommunication.GetMessageForMultipleWrite(nID,
                DataProcessor.ModbusRTU.MultipleWriteFunctionCodes.WriteMultipleRegisters,
                (ushort)OPERATION_SETUP1_MAP.MaxVelocityH, (ushort)(initdata.Length / 2), initdata);
        }
        public byte[] SetMoveTargetAccel(byte nID, int iMaxAccel)
        {
            byte[] initdata = new byte[4];
            initdata[0] = (byte)((iMaxAccel >> 24) & 0xff);
            initdata[1] = (byte)((iMaxAccel >> 16) & 0xff);
            initdata[2] = (byte)((iMaxAccel >> 8) & 0xff);
            initdata[3] = (byte)((iMaxAccel & 0xff));
            return mMotionCommunication.GetMessageForMultipleWrite(nID,
                DataProcessor.ModbusRTU.MultipleWriteFunctionCodes.WriteMultipleRegisters,
                (ushort)OPERATION_SETUP1_MAP.AccelerationTime, (ushort)(initdata.Length / 2), initdata);
        }
        public byte[] DriveInitialSetting(byte nID,int iStartSpeed, int iMaxSpeed,short iAccTime, short iDecTime)
        {
            byte[] initdata = new byte[12];
            initdata[0] = (byte)((iStartSpeed >> 24) & 0xff);
            initdata[1] = (byte)((iStartSpeed >> 16) & 0xff);
            initdata[2] = (byte)((iStartSpeed >> 8) & 0xff);
            initdata[3] = (byte)((iStartSpeed & 0xff));
            initdata[4] = (byte)((iMaxSpeed >> 24) & 0xff);
            initdata[5] = (byte)((iMaxSpeed >> 16) & 0xff);
            initdata[6] = (byte)((iMaxSpeed >> 8) & 0xff);
            initdata[7] = (byte)((iMaxSpeed & 0xff));
            initdata[8] = (byte)((iAccTime >> 8) & 0xff);
            initdata[9] = (byte)((iAccTime & 0xff));
            initdata[10] = (byte)((iDecTime >> 8) & 0xff);
            initdata[11] = (byte)((iDecTime & 0xff));
            return mMotionCommunication.GetMessageForMultipleWrite(nID,
                DataProcessor.ModbusRTU.MultipleWriteFunctionCodes.WriteMultipleRegisters,
                (ushort)OPERATION_SETUP1_MAP.StartVelocityH, (ushort)(initdata.Length/2), initdata);
        }
    }
}
