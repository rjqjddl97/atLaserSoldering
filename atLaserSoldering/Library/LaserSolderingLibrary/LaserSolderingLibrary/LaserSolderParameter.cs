using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserSoldering
{
    public class LaserSolderParameter
    {
        private int _PreheatPowerRatio = 0;
        private int _HeatPowerRatio = 0;
        private int _ReadyTime = 0;
        private int _PreHeatTime = 0;
        private int _HeatTime = 0;
        private double _ForwordingWireLength = 0;
        private double _ForwordingVelocity = 0;
        private double _ForwordingAcceleration = 0;
        private double _ReverseWireLength = 0;
        private double _ReverseVelocity = 0;
        private double _ReverseAcceleration = 0;
        private int _LaserOnTime = 0;

        public int PreheatPowerRatio { get { return _PreheatPowerRatio; } set { _PreheatPowerRatio = value; } }
        public int HeatPowerRatio { get { return _HeatPowerRatio; } set { _HeatPowerRatio = value; } }
        public int ReadyTime { get { return _ReadyTime; } set { _ReadyTime = value; } }
        public int PreHeatTime { get { return _PreHeatTime; } set { _PreHeatTime = value; } }
        public int HeatTime { get { return _HeatTime; } set { _HeatTime = value; } }
        public double ForwordingWireLength { get { return _ForwordingWireLength; } set { _ForwordingWireLength = value; } }
        public double ForwordingVelocity { get { return _ForwordingVelocity; } set { _ForwordingVelocity = value; } }
        public double ForwordingAcceleration { get { return _ForwordingAcceleration; } set { _ForwordingAcceleration = value; } }
        public double ReverseWireLength { get { return _ReverseWireLength; } set { _ReverseWireLength = value; } }
        public double ReverseVelocity { get { return _ReverseVelocity; } set { _ReverseVelocity = value; } }
        public double ReverseAcceleration { get { return _ReverseAcceleration; } set { _ReverseAcceleration = value; } }
        public int LaserOnTime { get { return _LaserOnTime; } set { _LaserOnTime = value; } }
    }
}
