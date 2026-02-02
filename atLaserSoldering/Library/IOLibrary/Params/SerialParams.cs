using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace RecipeManager
{
    public class SerialParams
    {
        string _strPortName = "COM1";

        int _baudRates = 9600;
        int _databits = 8;

        Parity _eParity = Parity.None;
        StopBits _eStopBits = StopBits.One;
        Handshake _eHandShake = Handshake.None;
        Encoding _cEncoding = Encoding.ASCII;


        public string PortName
        {
            get { return _strPortName; }
            set { _strPortName = value; }
        }

        public int BaudRates
        {
            get { return _baudRates; }
            set { _baudRates = value; }
        }

        public int DataBits
        {
            get { return _databits; }
            set { _databits = value; }
        }

        public Parity Parity
        {
            get { return _eParity; }
            set { _eParity = value; }
        }

        public StopBits StopBits
        {
            get { return _eStopBits; }
            set { _eStopBits = value; }
        }

        public Handshake Handshake
        {
            get { return _eHandShake; }
            set { _eHandShake = value; }
        }

        public Encoding Encodeing
        {
            get { return _cEncoding; }
            set { _cEncoding = value; }
        }
    }
}
