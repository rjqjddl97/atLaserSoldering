using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace LFineLightLibrary.SerialCommunication.Data
{
    public class LFineData
    {
        public const byte STX = 0x02;
        public const byte ETX = 0x03;
        

        public byte[] SetLightControl(byte ch, byte cmd)
        {
            byte[] data = new byte[4];
            data[0] = STX;
            data[1] = ch;
            data[2] = cmd;
            data[3] = ETX;
            return data;            
        }
        public byte[] SetLightControl(int ch, byte cmd)
        {
            byte[] data = new byte[4];
            data[0] = STX;
            data[1] = (byte)(ch + '0');
            data[2] = cmd;
            data[3] = ETX;
            return data;
        }
        public byte[] SetLightControl(int ch, bool cmd)
        {
            byte[] data = new byte[4];
            data[0] = STX;
            data[1] = (byte)(ch + '0');
            if(cmd)
                data[2] = (byte)'o';
            else
                data[2] = (byte)'f';
            data[3] = ETX;
            return data;
        }
        public byte[] SetBrightControl(int ch, string value)
        {
            byte[] data = new byte[8];
            byte[] byData = Encoding.ASCII.GetBytes(value);
            int index = 0;
            data[index++] = STX;
            data[index++] = (byte)(ch + '0');
            data[index++] = (byte)'w';
            Buffer.BlockCopy(byData, 0, data, index, 4);
            data[index+4] = ETX;
            return data;
        }
        public byte[] SetBrightControl(int ch, byte[] value)
        {
            byte[] data = new byte[8];            
            int index = 0;
            data[index++] = STX;
            data[index++] = (byte)(ch + '0');
            data[index++] = (byte)'w';
            Buffer.BlockCopy(value, 0, data, index, 4);
            data[index + 4] = ETX;
            return data;
        }
    }
}
