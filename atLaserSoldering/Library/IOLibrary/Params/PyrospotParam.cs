using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class PyrospotParam
    {
        int _iConnectedNumber = 1;
        int _iIDNumber = 6;        
        SerialParams _SerialParam = new SerialParams();

        public int ConnectedNumber
        {
            get { return _iConnectedNumber; }
            set { _iConnectedNumber = value; }
        }
        public SerialParams SerialParameters
        {
            get { return _SerialParam; }
            set { _SerialParam = value; }
        }
        public int CommunicationID
        {
            get { return _iIDNumber; }
            set { _iIDNumber = value; }
        }
        public PyrospotParam()
        {

        }

    }
}
