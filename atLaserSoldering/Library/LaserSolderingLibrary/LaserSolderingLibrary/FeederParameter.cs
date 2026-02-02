using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserSoldering
{
    public class FeederParameter
    {
        private double _dGearRatioA = 48;
        private double _dGearRatioB = 20;
        private double _dGearRatio = 1;
        private double _dFeederDiameter = 22;
        private double _dMotorResolution = 1000;
        public double _dFeederBallLead = 2 * Math.PI * (22 / 2D);     // _dFeederDiameter
        private double _dFeederVelocity = 10;
        private double _dFeederAcceleration = 0.3;
        public double _dFeederPulseTommRatio = 1;
        public UInt32 _dFeedermmToPulseRatio = 1;

        private double _dPreIronForwardLength = 1;
        private double _dPreIronForwardVelocity = 10;
        private double _dPreIronForwardAccereration = 0.3;
        private double _dPreIronReversedLength = 1;
        private double _dPreIronReverseVelocity = 10;
        private double _dPreIronReverseAccereration = 0.3;
        private double _dIronForwardLength = 1;
        private double _dIronForwardVelocity = 10;
        private double _dIronForwardAccereration = 0.3;
        private double _dIronReversedLength = 1;
        private double _dIronReverseVelocity = 10;
        private double _dIronReverseAccereration = 0.3;
        private UInt32 _uReadyTime = 0;
        private UInt32 _uPreHeatTime = 0;
        private UInt32 _uHeatTime = 0;
        public double GearRatioA { get { return _dGearRatioA; } set { _dGearRatioA = value; } }
        public double GearRatioB { get { return _dGearRatioB; } set { _dGearRatioB = value; } }
        public double GearRatio { get { return _dGearRatio; } set { _dGearRatio = value; } }
        public double FeederDiameter { get { return _dFeederDiameter; } set { _dFeederDiameter = value; } }
        public double MotorResolution { get { return _dMotorResolution; } set { _dMotorResolution = value; } }
        public double FeederVelocity { get { return _dFeederVelocity; } set { _dFeederVelocity = value; } }
        public double FeederAcceleration { get { return _dFeederAcceleration; } set { _dFeederAcceleration = value; } }
        public double PreIronForwardLength { get { return _dPreIronForwardLength; } set { _dPreIronForwardLength = value; } }
        public double PreIronForwardVelocity { get { return _dPreIronForwardVelocity; } set { _dPreIronForwardVelocity = value; } }
        public double PreIronForwardAccereration { get { return _dPreIronForwardAccereration; } set { _dPreIronForwardAccereration = value; } }
        public double PreIronReversedLength { get { return _dPreIronReversedLength; } set { _dPreIronReversedLength = value; } }
        public double PreIronReverseVelocity { get { return _dPreIronReverseVelocity; } set { _dPreIronReverseVelocity = value; } }
        public double PreIronReverseAccereration { get { return _dPreIronReverseAccereration; } set { _dPreIronReverseAccereration = value; } }
        public double IronForwardLength { get { return _dIronForwardLength; } set { _dIronForwardLength = value; } }
        public double IronForwardVelocity { get { return _dIronForwardVelocity; } set { _dIronForwardVelocity = value; } }
        public double IronForwardAccereration { get { return _dIronForwardAccereration; } set { _dIronForwardAccereration = value; } }
        public double IronReversedLength { get { return _dIronReversedLength; } set { _dIronReversedLength = value; } }
        public double IronReverseVelocity { get { return _dIronReverseVelocity; } set { _dIronReverseVelocity = value; } }
        public double IronReverseAccereration { get { return _dIronReverseAccereration; } set { _dIronReverseAccereration = value; } }
        public UInt32 ReadyTime { get { return _uReadyTime; } set { _uReadyTime = value; } }
        public UInt32 PreHeatTime { get { return _uPreHeatTime; } set { _uPreHeatTime = value; } }
        public UInt32 HeatTime { get { return _uHeatTime; } set { _uHeatTime = value; } }

        public void InitialParameter()
        {
            if (_dGearRatioA != 0)
                _dGearRatio = _dGearRatioB / _dGearRatioA;
            else
                _dGearRatio = 1D;

            if (_dFeederDiameter != 0)
                _dFeederBallLead = 2 * Math.PI * (_dFeederDiameter / 2D);
            
            if (_dMotorResolution == 0)
                _dMotorResolution = 1;

            _dFeederPulseTommRatio = (double)((_dFeederBallLead * _dGearRatio) / _dMotorResolution);
            _dFeedermmToPulseRatio = (UInt32)Math.Round(1D / _dFeederPulseTommRatio);
        }
    }
}
