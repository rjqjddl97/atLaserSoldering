using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompactSECommunication.Communication.Control
{
    public class TcpIpSetData
    {
        private string mIpAddress = "192.168.0.1";
        public string IpAddress
        {
            get { return mIpAddress; }
            set { mIpAddress = value; }
        }
        private int mPort = 1000;
        public int Port
        {
            get { return mPort; }
            set
            {
                if (value > 0)
                    mPort = value;
            }
        }

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
    }
}
