using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiCControlLibrary.SerialCommunication.DataProcessor
{
    public class ModbusRTU
    {
        public const int MINIMUM_RESPONSE_SIZE = 6;

        public enum FunctionCodes
        {
            ReadCoils = 1,
            ReadInputs = 2,
            ReadHoldingRegisters = 3,
            ReadInputRegisters = 4,
            WriteSingleCoil = 5,
            WriteSingleRegister = 6,
            WriteMultipleCoils = 15,
            WriteMultipleRegisters = 16,
            Exception = 80,
        }

        public enum ReadFunctionCodes
        {
            ReadCoils = 1,
            ReadInputs = 2,
            ReadHoldingRegisters = 3,
            ReadInputRegisters = 4,
        }

        public enum WriteFunctionCodes
        {
            WriteSingleCoil = 5,
            WriteSingleRegister = 6,
        }

        public enum MultipleWriteFunctionCodes
        {
            WriteMultipleCoils = 15,
            WriteMultipleRegisters = 16,
        }
        public enum Errorcodes
        {
            Illegal_Function = 1,
            Illegal_Data_Address,
            Illegal_Data_Value,
            Slave_Device_Failure,
            Slave_Device_Busy
        }
        public byte[] GetMessageForRead(ushort id, ReadFunctionCodes code, ushort startAddr, ushort numberOfPoints)
        {
            try
            {
                byte[] data = new byte[6];
                data[0] = Convert.ToByte(id);
                data[1] = Convert.ToByte(code);
                data[2] = BitConverter.GetBytes(startAddr)[1];
                data[3] = BitConverter.GetBytes(startAddr)[0];
                data[4] = BitConverter.GetBytes(numberOfPoints)[1];
                data[5] = BitConverter.GetBytes(numberOfPoints)[0];

                return AddCRCValue(data);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public byte[] GetMessageForWrite(ushort id, WriteFunctionCodes code, ushort startAddr, ushort presetData)
        {
            try
            {
                byte[] data = new byte[6];
                data[0] = Convert.ToByte(id);
                if (id == 129)
                    data[1] = (byte)(Convert.ToByte(code) | 0x80);
                else
                    data[1] = Convert.ToByte(code);

                data[2] = BitConverter.GetBytes(startAddr)[1];
                data[3] = BitConverter.GetBytes(startAddr)[0];

                if (code == WriteFunctionCodes.WriteSingleCoil)
                {
                    data[4] = BitConverter.GetBytes(presetData)[1];
                    data[5] = BitConverter.GetBytes(presetData)[0];
                }
                else
                {
                    data[4] = BitConverter.GetBytes(presetData)[1];
                    data[5] = BitConverter.GetBytes(presetData)[0];
                }

                return AddCRCValue(data);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public byte[] GetMessageForMultipleWrite(ushort id, MultipleWriteFunctionCodes code, ushort startAddr, ushort numberOfRegister, byte[] presetData)
        {
            try
            {
                if (presetData == null) return null;

                byte[] data = new byte[7 + presetData.Length];
                data[0] = Convert.ToByte(id);
                data[1] = Convert.ToByte(code);
                data[2] = BitConverter.GetBytes(startAddr)[1];
                data[3] = BitConverter.GetBytes(startAddr)[0];
                data[4] = BitConverter.GetBytes(numberOfRegister)[1];
                data[5] = BitConverter.GetBytes(numberOfRegister)[0];
                data[6] = Convert.ToByte(presetData.Length);

                Buffer.BlockCopy(presetData, 0, data, 7, presetData.Length);

                return AddCRCValue(data);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public byte[] AddCRCValue(byte[] seedData)
        {
            if (seedData == null)
                return null;

            byte[] mRetValue = new byte[seedData.Length + 2];

            ushort crcValue = CRC.CRC16(seedData, seedData.Length);

            Buffer.BlockCopy(seedData, 0, mRetValue, 0, seedData.Length);

            mRetValue[mRetValue.Length - 2] = (byte)(crcValue & 0x00FF);
            mRetValue[mRetValue.Length - 1] = (byte)((crcValue >> 8) & 0xFF);

            return mRetValue;
        }

        public short GetShortValueFromTwoBytes(byte data1, byte data2, bool switchEndian = false)
        {
            if (switchEndian == false)
            {
                int retValue = 0x00ff & data1;
                return (short)((retValue << 8) | data2);
            }
            else
            {
                int retValue = 0x00ff & data2;
                return (short)((retValue << 8) | data1);
            }
        }

        public short GetShortValueFromTwoBytes(byte[] data, int startIndex, bool switchEndian = false)
        {
            if (data == null) throw new ArgumentNullException("data", "Data is null.");
            else if (data.Length < startIndex + 1) throw new ArgumentException("data", "Data length is too short.");
            else
            {
                if (switchEndian == false)
                {
                    int retValue = 0x00ff & data[startIndex];
                    return (short)((retValue << 8) | data[startIndex + 1]);
                }
                else
                {
                    int retValue = 0x00ff & data[startIndex + 1];
                    return (short)((retValue << 8) | data[startIndex]);
                }
            }
        }

        public int GetIntValueFromTwoShort(ushort data1, ushort data2, bool switchEndian = false)
        {
            if (switchEndian == false)
            {
                int retValue = 0xffff & data1;
                return (retValue << 16) | data2;
            }
            else
            {
                int retValue = 0xffff & data2;
                return ((retValue << 16) | data1);
            }
        }
    }
}
