using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class FeederParams
    {

        private int _iIDNumber = 1;
        private double _FeederGearRatio = 0.416667;
        private double _FeederDiameter = 22;
        private double _FeederBallLead = 2 * Math.PI * (22 / 2D);     // _dFeederDiameter
        private double _FeederResolution = 1000;
        private double _FeederMoveVelocity = 20;
        private double _FeederPulseTommRatio = 1;
        private UInt32 _FeedermmToPulseRatio = 1;

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
        public double FeederBallLead
        {
            get { return _FeederBallLead; }
            set { _FeederBallLead = value; }
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
        public double FeederPulseTommRatio
        {
            get { return _FeederPulseTommRatio; }
            set { _FeederPulseTommRatio = value; }
        }
        public UInt32 FeedermmToPulseRatio
        {
            get { return _FeedermmToPulseRatio; }
            set { _FeedermmToPulseRatio = value; }
        }
        public int FeederCommunicationID
        {
            get { return _iIDNumber; }
            set { _iIDNumber = value; }
        }
        public FeederParams()
        {
            ;
        }
        public void InitialParameter()
        {

            if (_FeederDiameter != 0)
                _FeederBallLead = 2 * Math.PI * (_FeederDiameter / 2D);

            if (_FeederResolution == 0)
                _FeederResolution = 1;

            _FeederPulseTommRatio = (double)((_FeederBallLead * _FeederGearRatio) / _FeederResolution);
            _FeedermmToPulseRatio = (UInt32)Math.Round(1D / _FeederPulseTommRatio);
        }
    }
}
