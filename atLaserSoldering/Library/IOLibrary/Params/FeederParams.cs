using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class FeederParams
    {
        
        int _iIDNumber = 1;
        double _FeederGearRatio = 0.416667;
        double _FeederDiameter = 22;
        double _FeederResolution = 1000;
        double _FeederMoveVelocity = 20;
        SerialParams _SerialParam = new SerialParams();
        public SerialParams SerialParameters
        {
            get { return _SerialParam; }
            set { _SerialParam = value; }
        }
        public double FeederGearRatio
        {
            get { return _FeederGearRatio; }
            set { _FeederGearRatio = value; }
        }
        public double FeederDiameter
        {
            get { return _FeederDiameter; }
            set { _FeederDiameter = value; }
        }
        public double FeederResolution
        {
            get { return _FeederResolution; }
            set { _FeederResolution = value; }
        }
        public double FeederMoveVelocity
        {
            get { return _FeederMoveVelocity; }
            set { _FeederMoveVelocity = value; }
        }
        public int FeederCommunicationID
        {
            get { return _iIDNumber; }
            set { _iIDNumber = value; }
        }
        public FeederParams()
        {

        }
    }
}
