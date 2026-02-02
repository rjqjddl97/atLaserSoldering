using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiCControlLibrary.SerialCommunication.DataProcessor
{
    public class Base64
    {
        public int DataEncode(byte[] source, ref byte[] result, int nLength)
        {
            int i, nOdd, nResultLength, index;
            index = 0;
            nOdd = nLength % 3;
            nLength -= nOdd;
            nResultLength = 0;

            for (i = 0; i < nLength; i += 3)
            {
                result[index++] = (byte)(((source[i + 0] >> 2) & 0x3f) + ' ');
                result[index++] = (byte)((((source[i + 0] << 4) & 0x30) | ((source[i + 1] >> 4) & 0x0f)) + ' ');
                result[index++] = (byte)((((source[i + 1] << 2) & 0x3c) | ((source[i + 2] >> 6) & 0x03)) + ' ');
                result[index++] = (byte)((source[i + 2] & 0x3f) + ' ');
                nResultLength += 4;
            }

            switch (nOdd)
            {
                case 1:
                    result[index++] = (byte)(((source[i + 0] >> 2) & 0x3f) + ' ');
                    result[index++] = (byte)(((source[i + 0] << 4) & 0x30) + ' ');
                    nResultLength += 2;
                    break;
                case 2:
                    result[index++] = (byte)(((source[i + 0] >> 2) & 0x3f) + ' ');
                    result[index++] = (byte)((((source[i + 0] << 4) & 0x30) | ((source[i + 1] >> 4) & 0x0f)) + ' ');
                    result[index++] = (byte)(((source[i + 1] << 2) & 0x3c) + ' ');
                    nResultLength += 3;
                    break;
            }

            return nResultLength;
        }

        public int GetEncodeDataLength(int nLength)
        {
            int nOdd, i, nResultLength;
            nOdd = nLength % 3;
            nLength -= nOdd;

            nResultLength = 0;

            for (i = 0; i < nLength; i += 3)
                nResultLength += 4;

            switch (nOdd)
            {
                case 1:
                    nResultLength += 2;
                    break;
                case 2:
                    nResultLength += 3;
                    break;
            }
            return nResultLength;
        }

        public int DataDecode(byte[] source, ref byte[] result, int nLength)
        {
            int i, nOdd, nResultLength, index;
            index = 0;
            nOdd = nLength % 4;
            nLength -= nOdd;
            nResultLength = 0;

            for (i = 0; i < nLength; i += 4)
            {
                result[index++] = (byte)((((source[i + 0] - ' ') << 2) & 0xfc) | (((source[i + 1] - ' ') >> 4) & 0x03));
                result[index++] = (byte)((((source[i + 1] - ' ') << 4) & 0xf0) | (((source[i + 2] - ' ') >> 2) & 0x0f));
                result[index++] = (byte)((((source[i + 2] - ' ') << 6) & 0xc0) | ((source[i + 3] - ' ') & 0x3f));
                nResultLength += 3;
            }

            if (nOdd >= 2)
            {
                result[index++] = (byte)((((source[i + 0] - ' ') << 2) & 0xfc) | (((source[i + 1] - ' ') >> 4) & 0x03));
                nResultLength++;
            }

            if (nOdd >= 3)
            {
                result[index++] = (byte)((((source[i + 1] - ' ') << 4) & 0xf0) | (((source[i + 2] - ' ') >> 2) & 0x0f));
                nResultLength++;
            }

            return nResultLength;
        }

        public int GetDecodeDataLength(int nLength)
        {
            int nOdd;
            nOdd = nLength % 4;
            nLength -= nOdd;
            int nResultLength = 0;
            for (int i = 0; i < nLength; i += 4)
                nResultLength += 3;

            if (nOdd >= 2)
                nResultLength++;

            if (nOdd >= 3)
                nResultLength++;

            return nResultLength;
        }
    }
}
