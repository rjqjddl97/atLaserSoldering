using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiCControlLibrary.SerialCommunication.DataProcessor
{
    public class Modulo256
    {
        public byte[] GetMessageForCommand(byte Opcode, byte cmd, byte Length)
        {
            try
            {
                byte[] data = new byte[4];
                data[0] = Opcode;
                data[1] = cmd;
                data[2] = Length;
                data[3] = Checksum(data, 0, 3);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public byte[] GetMessageForWrite(byte Opcode, byte cmd, byte Length, byte data)
        {
            try
            {
                byte[] retdata = new byte[5];
                retdata[0] = Opcode;
                retdata[1] = cmd;
                retdata[2] = Length;
                retdata[3] = data;
                retdata[4] = Checksum(retdata, 0, 4);

                return retdata;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public byte[] GetMessageForMultipleWrite(byte Opcode, byte cmd, byte Length, byte[] Data)
        {
            try
            {
                if (Data == null) return null;

                byte[] retdata = new byte[4+Data.Length];
                retdata[0] = Opcode;
                retdata[1] = cmd;
                retdata[2] = Length;
                Buffer.BlockCopy(Data, 0, retdata, 3, Data.Length);
                retdata[Data.Length+3] = Checksum(retdata, 0, Data.Length+3);

                return retdata;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public byte Checksum(byte[] arr, int offset, int size)
        {
            try
            {
                byte result = 0;
                int sum = 0;

                for (int i = 0; i < size; i++)
                {
                    sum += arr[offset + i];
                }
                result = (byte)(sum % 256);

                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
