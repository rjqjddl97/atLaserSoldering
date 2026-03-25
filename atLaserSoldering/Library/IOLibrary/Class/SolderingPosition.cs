using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class SolderingPosition
    {
        int _isResult = InspectionResult.Ready;

        int _index = 0;

        INSPECTION_POSITION_MODE _positiontype = INSPECTION_POSITION_MODE.POSITION_INSPECTION_MODE;
        //INSPECTION_POSITION_MODE _eInspectionPositionMode = INSPECTION_POSITION_MODE.INSPECTION_POSITION_BASE_MODE;

        private double _PreheatPowerRatio = 0;
        private double _HeatPowerRatio = 0;
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
        
        private double _positionX = 0;
        private double _positionY = 0;
        private double _positionZ = 0;
        private double _positionRx = 0;
        private double _positionRy = 0;
        private double _positionRz = 0;

        public INSPECTION_POSITION_MODE ePositionType { get { return _positiontype; } set { _positiontype = value; } }
        public double PositionX { get { return _positionX; } set { _positionX = value; } }
        public double PositionY { get { return _positionY; } set { _positionY = value; } }
        public double PositionZ { get { return _positionZ; } set { _positionZ = value; } }
        public double PositionRx { get { return _positionRx; } set { _positionRx = value; } }
        public double PositionRy { get { return _positionRy; } set { _positionRy = value; } }
        public double PositionRz { get { return _positionRz; } set { _positionRz = value; } }
        public double PreHeatPowerRatio { get { return _PreheatPowerRatio; } set { _PreheatPowerRatio = value; } }
        public double HeatPowerRatio { get { return _HeatPowerRatio; } set { _HeatPowerRatio = value; } }
        public int ReadyTime { get { return _ReadyTime; } set { _ReadyTime = value; } }
        public int PreHeatTime { get { return _PreHeatTime; } set { _PreHeatTime = value; } }
        public int HeatTime { get { return _HeatTime; } set { _HeatTime = value; } }
        public double ForwardFeedLength { get { return _ForwordingWireLength; } set { _ForwordingWireLength = value; } }
        public double ForwardFeedVelocity { get { return _ForwordingVelocity; } set { _ForwordingVelocity = value; } }
        public double ForwordingAcceleration { get { return _ForwordingAcceleration; } set { _ForwordingAcceleration = value; } }
        public double ReverseFeedLength { get { return _ReverseWireLength; } set { _ReverseWireLength = value; } }
        public double ReverseFeedVelocity { get { return _ReverseVelocity; } set { _ReverseVelocity = value; } }
        public double ReverseAcceleration { get { return _ReverseAcceleration; } set { _ReverseAcceleration = value; } }
        public int LaserOnTime { get { return _LaserOnTime; } set { _LaserOnTime = value; } }
        public int IsResult { get { return _isResult; } set { _isResult = value; }}
        public int Index { get { return _index; } set { _index = value; } }            

        public void InitializeSolderParameters()
        {
            // X, Y, Z 좌표 이외 파라미터 초기화
            _PreheatPowerRatio = 0;
            _HeatPowerRatio = 0;
            _ReadyTime = 0;
            _PreHeatTime = 0;
            _HeatTime = 0;
            _ForwordingWireLength = 0;
            _ForwordingVelocity = 0;
            _ForwordingAcceleration = 0;
            _ReverseWireLength = 0;
            _ReverseVelocity = 0;
            _ReverseAcceleration = 0;
            _LaserOnTime = 0;
            _positionX = 0;
            _positionY = 0;
            _positionZ = 0;
            _positionRx = 0;
            _positionRy = 0;
            _positionRz = 0;
            _isResult = InspectionResult.Ready;           
        }
    }
}
