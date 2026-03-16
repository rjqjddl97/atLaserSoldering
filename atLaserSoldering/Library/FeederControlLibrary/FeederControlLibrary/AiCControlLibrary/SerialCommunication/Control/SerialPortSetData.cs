using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace AiCControlLibrary.SerialCommunication.Control
{
    public class SerialPortSetData
    {
        private string mPortName = "COM1";
        public string PortName
        {
            get { return mPortName; }
            set
            {
                if (value.Contains("COM"))
                {
                    uint retNumber = 0;

                    if (UInt32.TryParse(value.Replace("COM", ""), out retNumber))
                        mPortName = "COM" + retNumber.ToString();
                }
                else
                {
                    //for combobox index.
                    uint retNumber = 0;
                    if (UInt32.TryParse(value, out retNumber))
                        mPortName = "COM" + (retNumber + 1).ToString();
                }
            }
        }
        public int BaudRate { get; set; } = 9600;
        public Parity Parity { get; set; } = Parity.Even;
        public StopBits StopBits { get; set; } = StopBits.Two;
        public int DataBits { get; set; } = 7;

        private int mReadTimeout = 1000;
        public int ReadTimeout
        {
            get { return mReadTimeout; }
            set
            {
                if (value > 0)
                    mReadTimeout = value;
            }
        }

        private int mWriteTimeout = 1000;
        public int WriteTimeout
        {
            get { return mWriteTimeout; }
            set
            {
                if (value > 0)
                    mWriteTimeout = value;
            }
        }

        public Handshake Handshake { get; } = Handshake.None;
    }
}
