using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArioModbusLibrary.SerialCommunication.DataProcessor;
using System.Runtime.InteropServices;

namespace ArioModbusLibrary.SerialCommunication.Data
{
    public class ArioMRData
    {       
        public enum OUTPUT_CONTROL_MAP
        {
            // Base Address 402000 ~ 402256
            Output0 = 2000,
            Output1,
            Output2,
            Output3,
            Output4,
            Output5,
            Output6,
            Output7,
            Output8,
            Output9,
            Output10,
            Output11,
            Output12,
            Output13,
            Output14,
            Output15,
            Output16,
            Output17,
            Output18,
            Output19,
            Output20,
            Output21,
            Output22,
            Output23,
            Output24,
            Output25,
            Output26,
            Output27,
            Output28,
            Output29,
            Output30,
            Output31,
            Output32,
            Output33,
            Output34,
            Output35,
            Output36,
            Output37,
            Output38,
            Output39,
            Output40,
            Output41,
            Output42,
            Output43,
            Output44,
            Output45,
            Output46,
            Output47,
            Output48,
            Output49,
            Output50,
            Output51,
            Output52,
            Output53,
            Output54,
            Output55,
            Output56,
            Output57,
            Output58,
            Output59,
            Output60,
            Output61,
            Output62,
            Output63,
            Output64,
            Output65,
            Output66,
            Output67,
            Output68,
            Output69,
            Output70,
            Output71,
            Output72,
            Output73,
            Output74,
            Output75,
            Output76,
            Output77,
            Output78,
            Output79,
            Output80,
            Output81,
            Output82,
            Output83,
            Output84,
            Output85,
            Output86,
            Output87,
            Output88,
            Output89,
            Output90,
            Output91,
            Output92,
            Output93,
            Output94,
            Output95,
            Output96,
            Output97,
            Output98,
            Output99,
            Output100,
            Output101,
            Output102,
            Output103,
            Output104,
            Output105,
            Output106,
            Output107,
            Output108,
            Output109,
            Output110,
            Output111,
            Output112,
            Output113,
            Output114,
            Output115,
            Output116,
            Output117,
            Output118,
            Output119,
            Output120,
            Output121,
            Output122,
            Output123,
            Output124,
            Output125,
            Output126,
            Output127,
            Output128,
            Output129,
            Output130,
            Output131,
            Output132,
            Output133,
            Output134,
            Output135,
            Output136,
            Output137,
            Output138,
            Output139,
            Output140,
            Output141,
            Output142,
            Output143,
            Output144,
            Output145,
            Output146,
            Output147,
            Output148,
            Output149,
            Output150,
            Output151,
            Output152,
            Output153,
            Output154,
            Output155,
            Output156,
            Output157,
            Output158,
            Output159,
            Output160,
            Output161,
            Output162,
            Output163,
            Output164,
            Output165,
            Output166,
            Output167,
            Output168,
            Output169,
            Output170,
            Output171,
            Output172,
            Output173,
            Output174,
            Output175,
            Output176,
            Output177,
            Output178,
            Output179,
            Output180,
            Output181,
            Output182,
            Output183,
            Output184,
            Output185,
            Output186,
            Output187,
            Output188,
            Output189,
            Output190,
            Output191,
            Output192,
            Output193,
            Output194,
            Output195,
            Output196,
            Output197,
            Output198,
            Output199,
            Output200,
            Output201,
            Output202,
            Output203,
            Output204,
            Output205,
            Output206,
            Output207,
            Output208,
            Output209,
            Output210,
            Output211,
            Output212,
            Output213,
            Output214,
            Output215,
            Output216,
            Output217,
            Output218,
            Output219,
            Output220,
            Output221,
            Output222,
            Output223,
            Output224,
            Output225,
            Output226,
            Output227,
            Output228,
            Output229,
            Output230,
            Output231,
            Output232,
            Output233,
            Output234,
            Output235,
            Output236,
            Output237,
            Output238,
            Output239,
            Output240,
            Output241,
            Output242,
            Output243,
            Output244,
            Output245,
            Output246,
            Output247,
            Output248,
            Output249,
            Output250,
            Output251,
            Output252,
            Output253,
            Output254,
            Output255,
        }
        public enum INPUT_MAP
        {
            //Base Address 302001 ~ 302256.
            Input0 = 2000,
            Input1,
            Input2,
            Input3,
            Input4,
            Input5,
            Input6,
            Input7,
            Input8,
            Input9,
            Input10,
            Input11,
            Input12,
            Input13,
            Input14,
            Input15,
            Input16,
            Input17,
            Input18,
            Input19,
            Input20,
            Input21,
            Input22,
            Input23,
            Input24,
            Input25,
            Input26,
            Input27,
            Input28,
            Input29,
            Input30,
            Input31,
            Input32,
            Input33,
            Input34,
            Input35,
            Input36,
            Input37,
            Input38,
            Input39,
            Input40,
            Input41,
            Input42,
            Input43,
            Input44,
            Input45,
            Input46,
            Input47,
            Input48,
            Input49,
            Input50,
            Input51,
            Input52,
            Input53,
            Input54,
            Input55,
            Input56,
            Input57,
            Input58,
            Input59,
            Input60,
            Input61,
            Input62,
            Input63,
            Input64,
            Input65,
            Input66,
            Input67,
            Input68,
            Input69,            
            Input70,
            Input71,
            Input72,
            Input73,
            Input74,
            Input75,
            Input76,
            Input77,
            Input78,
            Input79,
            Input80,
            Input81,
            Input82,
            Input83,
            Input84,
            Input85,
            Input86,
            Input87,
            Input88,
            Input89,
            Input90,
            Input91,
            Input92,
            Input93,
            Input94,
            Input95,
            Input96,
            Input97,
            Input98,
            Input99,
            Input100,
            Input101,
            Input102,
            Input103,
            Input104,
            Input105,
            Input106,
            Input107,
            Input108,
            Input109,
            Input110,
            Input111,
            Input112,
            Input113,
            Input114,
            Input115,
            Input116,
            Input117,
            Input118,
            Input119,
            Input120,
            Input121,
            Input122,
            Input123,
            Input124,
            Input125,
            Input126,
            Input127,
            Input128,
            Input129,
            Input130,
            Input131,
            Input132,
            Input133,
            Input134,
            Input135,
            Input136,
            Input137,
            Input138,
            Input139,
            Input140,
            Input141,
            Input142,
            Input143,
            Input144,
            Input145,
            Input146,
            Input147,
            Input148,
            Input149,
            Input150,
            Input151,
            Input152,
            Input153,
            Input154,
            Input155,
            Input156,
            Input157,
            Input158,
            Input159,
            Input160,
            Input161,
            Input162,
            Input163,
            Input164,
            Input165,
            Input166,
            Input167,
            Input168,
            Input169,
            Input170,
            Input171,
            Input172,
            Input173,
            Input174,
            Input175,
            Input176,
            Input177,
            Input178,
            Input179,
            Input180,
            Input181,
            Input182,
            Input183,
            Input184,
            Input185,
            Input186,
            Input187,
            Input188,
            Input189,
            Input190,
            Input191,
            Input192,
            Input193,
            Input194,
            Input195,
            Input196,
            Input197,
            Input198,
            Input199,
            Input200,
            Input201,
            Input202,
            Input203,
            Input204,
            Input205,
            Input206,
            Input207,
            Input208,
            Input209,
            Input210,
            Input211,
            Input212,
            Input213,
            Input214,
            Input215,
            Input216,
            Input217,
            Input218,
            Input219,
            Input220,
            Input221,
            Input222,
            Input223,
            Input224,
            Input225,
            Input226,
            Input227,
            Input228,
            Input229,            
            Input230,
            Input231,
            Input232,
            Input233,
            Input234,
            Input235,
            Input236,
            Input237,
            Input238,
            Input239,
            Input240,
            Input241,
            Input242,
            Input243,
            Input244,
            Input245,
            Input246,
            Input247,
            Input248,
            Input249,
            Input250,
            Input251,
            Input252,
            Input253,
            Input254,
            Input255,
        }
        public enum PRODUCT_INFORMATION_MAP
        {
            // Base Address 300101 ~ 300129
            SerialNumberH = 100,
            SerialNumberL,
            HardwareVersion,
            SoftwareVersion,
            ModelName1,
            ModelName2,
            ModelName3,
            ModelName4,
            ModelName5,
            ModelName6,
            ModelName7,
            ModelName8,
            ModelName9,
            ModelName10
        }
        public enum PRODUCT_INFORMATION_CONNECT_NUMBER
        {
            // Base Address 300126
            ConnectedNumber = 125
        }
        public enum COUTER_DIAGONOSIS_MAP
        {
            // Base Address 301024
            DiagnosisData = 1023,
        }
        public enum OPERATION_COMMUNICATION_MAP
        {
            // Base Address 301030 ~ 301032
            BaudRate = 1029,
            ParityBit,
            StopBit,
        } 
        public enum OPERATION_INOUT_MAP
        {
            // Base Address 400001 ~ 400010
            Input1 = 0,
            Input2,
            Input3,
            Input4,
            Output1,
            Output2,
            Output3,
            Output4,
        }
        public enum CommandMassege
        {
            MSG_NONE = 0,
            MSG_OUTPUT,
            MSG_INPUT,
            MSG_PRODUCT_INFO,                        
            MSG_COMM_PARAM
        }
        public ModbusRTU mRemoteIOCommunication = new ModbusRTU();

        public byte DeviceIDCount { get; set; }
        public int DiagnosisStatus { get; set; } = 0;

        private int[] CurrentOutput = null;
        private int[] CurrentInput = null;
        private int[] CurrentProdInfomation = null;
        private int[] CurrentCommParam = null;
        public byte[] DrvID = null;
        public int GetCurrentOutput(OUTPUT_CONTROL_MAP putput) => CurrentOutput[((int)putput - Enum.GetValues(typeof(OUTPUT_CONTROL_MAP)).Cast<int>().Min()) / 8];
        public int GetCurrentInput(INPUT_MAP input) => CurrentInput[((int)input - Enum.GetValues(typeof(INPUT_MAP)).Cast<int>().Min()) / 8];        
        public int GetCurrentProdInfo(PRODUCT_INFORMATION_MAP prodinfo) => CurrentProdInfomation[(int)prodinfo - Enum.GetValues(typeof(PRODUCT_INFORMATION_MAP)).Cast<int>().Min()];
        public int GetCurrentCommParam(OPERATION_COMMUNICATION_MAP comm) => CurrentCommParam[(int)comm - Enum.GetValues(typeof(OPERATION_COMMUNICATION_MAP)).Cast<int>().Min()];

        public Queue<CommandMassege> RequestedCommandQueue = new Queue<CommandMassege>();
        public List<int[]> ArioElement = new List<int[]>();
        public Dictionary<int, List<int[]>> ArioProduct = new Dictionary<int, List<int[]>>();
        public RemoteIODatas _mRemoteIODatas = new RemoteIODatas();                

        public class RemoteIODatas
        {
            public int _Id;
            public int[] _CurrentOutputs = new int[(Enum.GetValues(typeof(OUTPUT_CONTROL_MAP)).Length / 8) + 1];
            public int[] _CurrentInputs = new int[(Enum.GetValues(typeof(INPUT_MAP)).Length / 8) + 1];
        }
        public class IO_8Bit
        {
            public byte Bit8 { get; set; } = 0;
            //public int B0 { get { return Bit8 & (1 << 0); } set { Bit8 |= (1 << 0); } }
            public bool B0 { get { return Convert.ToBoolean((Bit8 >> 0) & 0x01); } set { Bit8 |= (1 << 0); } }
            public bool B1 { get { return Convert.ToBoolean((Bit8 >> 1) & 0x01); } set { Bit8 |= (1 << 1); } }
            public bool B2 { get { return Convert.ToBoolean((Bit8 >> 2) & 0x01); } set { Bit8 |= (1 << 2); } }
            public bool B3 { get { return Convert.ToBoolean((Bit8 >> 3) & 0x01); } set { Bit8 |= (1 << 3); } }
            public bool B4 { get { return Convert.ToBoolean((Bit8 >> 4) & 0x01); } set { Bit8 |= (1 << 4); } }
            public bool B5 { get { return Convert.ToBoolean((Bit8 >> 5) & 0x01); } set { Bit8 |= (1 << 5); } }
            public bool B6 { get { return Convert.ToBoolean((Bit8 >> 6) & 0x01); } set { Bit8 |= (1 << 6); } }
            public bool B7 { get { return Convert.ToBoolean((Bit8 >> 7) & 0x01); } set { Bit8 |= (1 << 7); } }
        }
        public class IO_16Bit
        {
            public UInt16 Bit16 { get; set; } = 0;
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
        }
        public ArioMRData()
        {
            CurrentOutput = new int[(Enum.GetValues(typeof(OUTPUT_CONTROL_MAP)).Length/8) + 1];
            CurrentInput = new int[(Enum.GetValues(typeof(INPUT_MAP)).Length/8) + 1];
            CurrentProdInfomation = new int[Enum.GetValues(typeof(PRODUCT_INFORMATION_MAP)).Length];
            CurrentCommParam = new int[Enum.GetValues(typeof(OPERATION_COMMUNICATION_MAP)).Length];
            ArioElement.Clear();
            ArioProduct.Clear();
        }
        ~ArioMRData()
        {
            CurrentOutput = null;
            CurrentInput = null;
            CurrentProdInfomation = null;
            CurrentCommParam = null;
            if (ArioElement != null)
                ArioElement.Clear();
            if (ArioProduct != null)
                ArioProduct.Clear();
        }
        public void SetIDNumber(int IdNum,byte[] ID)
        {
            if ((CurrentOutput == null) && (CurrentInput == null) &&  (CurrentProdInfomation == null) &&  (CurrentCommParam == null)) return;

            List<int[]> listElement = new List<int[]>();

            ArioElement.Add(CurrentOutput);
            ArioElement.Add(CurrentInput);
            ArioElement.Add(CurrentProdInfomation);
            ArioElement.Add(CurrentCommParam);

            DeviceIDCount = (byte)IdNum;
            DrvID = new byte[DeviceIDCount];
            if (IdNum > 0)
            {
                for (int i = 0; i < IdNum; i++)
                {
                    ArioProduct.Add(ID[i], ArioElement);
                    DrvID[i] = ID[i];
                }
            }
            else
                ArioProduct.Add(1, ArioElement);
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

                byte nID = data[0];
                if (ArioProduct.ContainsKey((int)nID))
                {
                    short count = data[2];
                    for (int i = 0; i < count; i++)
                        CurrentOutput[i] = (data[3 + i] & 0x0000ffff);
                    
                    _mRemoteIODatas._Id = nID;
                    Array.Copy(CurrentOutput, 0, _mRemoteIODatas._CurrentOutputs, 0, CurrentOutput.Length);
                    //if (data[1] == (byte)DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleCoil)
                    //{
                    //    short addr = BitConverter.ToInt16(data, 2);
                    //    if ((addr <= ((ushort)Enum.GetValues(typeof(OUTPUT_CONTROL_MAP)).Length)))
                    //    {
                    //        //CurrentOutput[addr] = BitConverter.ToInt16(data, 4);
                    //    }
                    //}
                    //else if (data[1] == (byte)DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters)
                    //{
                    //    short count = data[2];
                    //    //if (count == CurrentOutput.Length)
                    //    {
                    //        for (int i = 0; i < count; i++)
                    //            CurrentOutput[i] = (data[3 + i] & 0x0000ffff);
                    //    }
                    //    _mRemoteIODatas._Id = nID;
                    //    Array.Copy(CurrentOutput, 0, _mRemoteIODatas._CurrentOutputs, 0, CurrentOutput.Length);
                    //}
                }
            }
            catch (Exception)
            {
                ;
            }
        }
        public byte[] SetSettingOutput(byte nID, OUTPUT_CONTROL_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);            
            return mRemoteIOCommunication.GetMessageForWrite(nID,
                DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleRegister,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingOutput(byte nID, OUTPUT_CONTROL_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);
            
            return mRemoteIOCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingOutputs(byte nID)
        {            
            
            return mRemoteIOCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters,
                (ushort)OUTPUT_CONTROL_MAP.Output0, (ushort)(ushort)(Enum.GetValues(typeof(OUTPUT_CONTROL_MAP)).Length / 8));
        }
        public byte[] GetSettingOutputs(byte nID, ushort numberOfPoints)
        {

            return mRemoteIOCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters,
                (ushort)OUTPUT_CONTROL_MAP.Output0, numberOfPoints);
        }
        public byte[] Output1byteCommand(byte nID, OUTPUT_CONTROL_MAP addr, ushort data)
        {
            return mRemoteIOCommunication.GetMessageForWrite(nID, DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleCoil, (ushort)addr, (ushort) data);
        }
        public void ReceiveSetInput(byte[] data)
        {
            try
            {
                if (data == null) return;
                if (data.Length < DataProcessor.ModbusRTU.MINIMUM_RESPONSE_SIZE) return;

                byte nID = data[0];
                if (ArioProduct.ContainsKey((int)nID))
                {
                    short count = data[2];
                        for (int i = 0; i < count; i++)
                            CurrentInput[i] = (data[3 + i] & 0x0000ffff);
                        
                    _mRemoteIODatas._Id = nID;
                    Buffer.BlockCopy(CurrentInput, 0, _mRemoteIODatas._CurrentInputs, 0, CurrentInput.Length);
                    //if (data[1] == (byte)DataProcessor.ModbusRTU.WriteFunctionCodes.WriteSingleCoil)
                    //{
                    //    short addr = BitConverter.ToInt16(data, 2);
                    //    if ((addr <= ((ushort)Enum.GetValues(typeof(INPUT_MAP)).Length)))
                    //    {
                    //        //CurrentInput[addr] = BitConverter.ToInt16(data, 4);
                    //    }
                    //}
                    //else if (data[1] == (byte)DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputRegisters)
                    //{
                    //    short count = data[2];
                    //    //if (count == CurrentInput.Length)
                    //    {
                    //        for (int i = 0; i < count; i++)                            
                    //            CurrentInput[i] = (data[3 + i] & 0x0000ffff);

                    //    }
                    //    _mRemoteIODatas._Id = nID;
                    //    Buffer.BlockCopy(CurrentInput, 0, _mRemoteIODatas._CurrentInputs, 0, CurrentInput.Length);
                    //}
                }
            }
            catch (Exception)
            {
                ;
            }
        }
        public byte[] GetSettingInput(byte nID, INPUT_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);
            //SetRequestedCommand(CommandMassege.MSG_INPUT);
            return mRemoteIOCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputRegisters,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingInputs(byte nID)
        {            
            //SetRequestedCommand(CommandMassege.MSG_INPUT);
            return mRemoteIOCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputRegisters,
                (ushort)INPUT_MAP.Input0, (ushort)(8));              //(Enum.GetValues(typeof(INPUT_MAP)).Length / 8) = (64 / 8)
        }
        public byte[] GetSettingInputs(byte nID,ushort numberOfPoints)
        {
            //SetRequestedCommand(CommandMassege.MSG_INPUT);
            return mRemoteIOCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputRegisters,
                (ushort)INPUT_MAP.Input0, (ushort)numberOfPoints);              //(Enum.GetValues(typeof(INPUT_MAP)).Length / 8) = (64 / 8)
        }
        public void ReceiveSetProductInfo(byte[] data)
        {
            try
            {
                if (data == null) return;
                if (data.Length < DataProcessor.ModbusRTU.MINIMUM_RESPONSE_SIZE) return;

                byte nID = data[0];

                if (ArioProduct.ContainsKey((int)nID))
                {
                    if (data[1] == (byte)DataProcessor.ModbusRTU.ReadFunctionCodes.ReadInputRegisters)
                    {
                        short count = data[2];
                        if (count != 0) count /= 2;
                        for (int i = 0; i < count; i++)
                            CurrentProdInfomation[i] = (mRemoteIOCommunication.GetShortValueFromTwoBytes(data, 3 + (i * 2)) & 0x0000ffff);
                        Buffer.BlockCopy(CurrentProdInfomation, 0, ArioElement[2], 0, CurrentProdInfomation.Length);

                        ArioProduct[nID] = ArioElement;
                    }
                }
            }
            catch (Exception)
            {
                ;
            }
        }
        public void ReceiveSetCommunicationData(byte[] data)
        {
            try
            {
                if (data == null) return;
                if (data.Length < DataProcessor.ModbusRTU.MINIMUM_RESPONSE_SIZE) return;

                byte nID = data[0];

                if (ArioProduct.ContainsKey((int)nID))
                {
                    if (data[1] == (byte)DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters)
                    {
                        short count = data[2];
                        if (count != 0) count /= 2;
                        for (int i = 0; i < count; i++)
                            CurrentCommParam[i] = (mRemoteIOCommunication.GetShortValueFromTwoBytes(data, 3 + (i * 2)) & 0x0000ffff);
                        Buffer.BlockCopy(CurrentCommParam, 0, ArioElement[7], 0, CurrentCommParam.Length);

                        ArioProduct[nID] = ArioElement;
                    }
                }
            }
            catch (Exception)
            {
                ;
            }
        }        public byte[] GetSettingCommunicationData(byte nID, OPERATION_COMMUNICATION_MAP func, ushort numberOfPoints)
        {
            ushort startAddr = Convert.ToUInt16((ushort)func);
            //SetRequestedCommand(CommandMassege.MSG_COMM_PARAM);
            return mRemoteIOCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters,
                startAddr,
                numberOfPoints);
        }
        public byte[] GetSettingCommunicationDatas(byte nID)
        {
            ushort startAddr = Convert.ToUInt16((ushort)Enum.GetValues(typeof(OPERATION_COMMUNICATION_MAP)).Cast<int>().Min());
            //SetRequestedCommand(CommandMassege.MSG_COMM_PARAM);
            return mRemoteIOCommunication.GetMessageForRead(nID,
                DataProcessor.ModbusRTU.ReadFunctionCodes.ReadHoldingRegisters,
                startAddr, (ushort)Enum.GetValues(typeof(OPERATION_COMMUNICATION_MAP)).Length);
        }
    }
}
