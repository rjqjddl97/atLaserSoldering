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
            ProgramDelete,
            Reserved1,
            Reserved2,
            MoveOverride
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
            Input8PinMode,
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
            HomingTorqueRatio,
            HomingFinishedSet
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
        public byte DeviceID { get; set; }
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

        public int GetCurrentCommandData(OPERATION_COMMAND_MAP cmddata) => CurrentCommandData[(int)cmddata - Enum.GetValues(typeof(OPERATION_COMMAND_MAP)).Cast<int>().Min()];
        public int GetCurrentSetParam(OPERATION_SETUP_MAP setparam) => CurrentSetParam[(int)setparam - Enum.GetValues(typeof(OPERATION_SETUP_MAP)).Cast<int>().Min()];
        public int GetCurrentOutput(OUTPUT_CONTROL_MAP putput) => CurrentOutput[(int)putput - Enum.GetValues(typeof(OUTPUT_CONTROL_MAP)).Cast<int>().Min()];
        public int GetCurrentExternalIO(EXTERNAL_INPUT_MAP extIO) => CurrentExternalIO[(int)extIO - Enum.GetValues(typeof(EXTERNAL_INPUT_MAP)).Cast<int>().Min()];
        public int GetCurrentProdInfo(PRODUCT_INFORMATION_MAP prodinfo) => CurrentProdInfomation[(int)prodinfo - Enum.GetValues(typeof(PRODUCT_INFORMATION_MAP)).Cast<int>().Min()];
        public int GetCurrentMotionData(MONITOR_DATA_MAP motdata) => CurrentMotionData[(int)motdata - Enum.GetValues(typeof(MONITOR_DATA_MAP)).Cast<int>().Min()];
        public int GetCurrentOpParam(OPERATION_PARAMETER_MAP opparam) => CurrentOpParam[(int)opparam - Enum.GetValues(typeof(OPERATION_PARAMETER_MAP)).Cast<int>().Min()];
        public int GetCurrentHomeParam(HOME_OPERATION_MAP home) => CurrentHomeParam[(int)home - Enum.GetValues(typeof(HOME_OPERATION_MAP)).Cast<int>().Min()];
        public int GetCurrentSetParam1(OPERATION_SETUP1_MAP setup1) => CurrentSetParam1[(int)setup1 - Enum.GetValues(typeof(OPERATION_SETUP1_MAP)).Cast<int>().Min()];
        public int GetCurrentSetParam2(OPERATION_SETUP2_MAP setup2) => CurrentSetParam2[(int)setup2 - Enum.GetValues(typeof(OPERATION_SETUP2_MAP)).Cast<int>().Min()];
        public int GetCurrentCommParam(COMM_PARAM_MAP comm) => CurrentCommParam[(int)comm - Enum.GetValues(typeof(COMM_PARAM_MAP)).Cast<int>().Min()];


        public List<int[]> AiCElement = new List<int[]>();
        public Dictionary<int, List<int[]>> AiCProduct = new Dictionary<int, List<int[]>>();
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
        }
        public AiCData()
        {
            CurrentCommandData = new int[Enum.GetValues(typeof(OPERATION_COMMAND_MAP)).Length];
            CurrentSetParam = new int[Enum.GetValues(typeof(OPERATION_SETUP_MAP)).Length];
            CurrentOutput = new int[Enum.GetValues(typeof(OUTPUT_CONTROL_MAP)).Length];
            CurrentExternalIO = new int[Enum.GetValues(typeof(EXTERNAL_INPUT_MAP)).Length];
            CurrentProdInfomation = new int[Enum.GetValues(typeof(PRODUCT_INFORMATION_MAP)).Length];
            CurrentMotionData = new int[Enum.GetValues(typeof(MONITOR_DATA_MAP)).Length];
            CurrentOpParam = new int[Enum.GetValues(typeof(OPERATION_PARAMETER_MAP)).Length];        
            CurrentHomeParam = new int[Enum.GetValues(typeof(HOME_OPERATION_MAP)).Length];
            CurrentSetParam1 = new int[Enum.GetValues(typeof(OPERATION_SETUP1_MAP)).Length];
            CurrentSetParam2 = new int[Enum.GetValues(typeof(OPERATION_SETUP2_MAP)).Length];
            CurrentCommParam = new int[Enum.GetValues(typeof(COMM_PARAM_MAP)).Length];
            AiCElement.Clear();
            AiCProduct.Clear();
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
            if (IdNum > 0)
            {
                for (int i = 0; i < IdNum; i++)
                {
                    AiCProduct.Add(ID[i], AiCElement);
                }
            }
            else
                AiCProduct.Add(1, AiCElement);
        }
        public void SetSettingOutput(byte nID, OUTPUT_CONTROL_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);
            return mMotionCommunication.GetMessageForWrite(nID,
                DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleCoil,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingOutput(byte nID, OUTPUT_CONTROL_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadCoils,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingOutputs(byte nID)
        {
            ushort startAddr = Convert.ToUInt16((ushort)Enum.GetValues(typeof(OUTPUT_CONTROL_MAP)).Cast<int>().Min());
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadCoils,
                startAddr, (ushort)Enum.GetValues(typeof(OUTPUT_CONTROL_MAP)).Length);
        }
        public byte[] GetSettingOperationCommand(byte nID, OPERATION_COMMAND_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadCoils,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingOperationCommands(byte nID)
        {
            ushort startAddr = Convert.ToUInt16((ushort)Enum.GetValues(typeof(OPERATION_COMMAND_MAP)).Cast<int>().Min());
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadCoils,
                startAddr, (ushort)Enum.GetValues(typeof(OPERATION_COMMAND_MAP)).Length);
        }
        public byte[] GetSettingOperationSignal(byte nID, OPERATION_SETUP_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadCoils,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingOperationSignals(byte nID)
        {
            ushort startAddr = Convert.ToUInt16((ushort)Enum.GetValues(typeof(OPERATION_SETUP_MAP)).Cast<int>().Min());
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadCoils,
                startAddr, (ushort)Enum.GetValues(typeof(OPERATION_SETUP_MAP)).Length);
        }
        public byte[] GetSettingExternalIO(byte nID, EXTERNAL_INPUT_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputs,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingExternalIOs(byte nID)
        {
            ushort startAddr = Convert.ToUInt16((ushort)Enum.GetValues(typeof(EXTERNAL_INPUT_MAP)).Cast<int>().Min());
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputs,
                startAddr, (ushort)Enum.GetValues(typeof(EXTERNAL_INPUT_MAP)).Length);
        }
        public byte[] GetSettingProductInfo(byte nID, PRODUCT_INFORMATION_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingProductInfos(byte nID)
        {
            ushort startAddr = Convert.ToUInt16((ushort)Enum.GetValues(typeof(PRODUCT_INFORMATION_MAP)).Cast<int>().Min());
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters,
                startAddr, (ushort)Enum.GetValues(typeof(PRODUCT_INFORMATION_MAP)).Length);
        }
        public byte[] GetSettingMotionData(byte nID, MONITOR_DATA_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputRegisters,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingMotionDatas(byte nID)
        {
            ushort startAddr = Convert.ToUInt16((ushort)Enum.GetValues(typeof(MONITOR_DATA_MAP)).Cast<int>().Min());
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputRegisters,
                startAddr, (ushort)Enum.GetValues(typeof(MONITOR_DATA_MAP)).Length);
        }
        public byte[] GetSettingOperationData(byte nID, OPERATION_SETUP1_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);            
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputRegisters,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingOperationDatas(byte nID)
        {
            ushort startAddr = Convert.ToUInt16((ushort)Enum.GetValues(typeof(OPERATION_SETUP1_MAP)).Cast<int>().Min());
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputRegisters,
                startAddr, (ushort)Enum.GetValues(typeof(OPERATION_SETUP1_MAP)).Length);
        }
        public byte[] GetSettingHomeParameter(byte nID, HOME_OPERATION_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingHomeParameters(byte nID)
        {
            ushort startAddr = Convert.ToUInt16((ushort)Enum.GetValues(typeof(HOME_OPERATION_MAP)).Cast<int>().Min());
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters,
                startAddr, (ushort)Enum.GetValues(typeof(HOME_OPERATION_MAP)).Length);
        }
        public byte[] GetSettingOperationParam(byte nID, OPERATION_PARAMETER_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingOperationParams(byte nID)
        {
            ushort startAddr = Convert.ToUInt16((ushort)Enum.GetValues(typeof(OPERATION_PARAMETER_MAP)).Cast<int>().Min());
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters,
                startAddr, (ushort)Enum.GetValues(typeof(OPERATION_PARAMETER_MAP)).Length);
        }
        public byte[] GetSettingCommunicationData(byte nID, COMM_PARAM_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingCommunicationDatas(byte nID)
        {
            ushort startAddr = Convert.ToUInt16((ushort)Enum.GetValues(typeof(COMM_PARAM_MAP)).Cast<int>().Min());
            return mMotionCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters,
                startAddr, (ushort)Enum.GetValues(typeof(COMM_PARAM_MAP)).Length);
        }
    }
}
