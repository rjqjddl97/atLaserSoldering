using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class CalibrationParams
    {
        public const double YAxis_X_Referense_Pos = -48.39;
        public const double YAxis_Y_Referense_Pos = 100;
        public const double YAxis_Z_Referense_Pos = 42.53;
        public const double YAxis_X_Delta_D_X = -0.001213;
        public const double YAxis_X_OFFSET_X = 0;
        public const double YAxis_X_Delta_D_Z = -0.003775;        
        public const double YAxis_X_OFFSET_Z = -0.15448;
        public const double YAxis_Z_Delta_D_X = 0.009253;
        public const double YAxis_Z_OFFSET_X = -0.24929;
        public const double YAxis_Z_Delta_D_Z = -0.000425;        
        public const double YAxis_Z_OFFSET_Z = 0;        
        public const double YAxis_Y_Delta_D_X = -0.0082;
        public const double YAxis_Y_Delta_D_Z = -0.012;
        public const double YAxis_Y_Delta_Offset_X = 0.81999;
        public const double YAxis_Y_Delta_Offset_Z = 1.1999;


        private double _X_reference_Distance = 0;
        private double _X_Delta = 0;
        private double _X_DeltaX = 0;
        private double _X_DeltaY = 0;
        private double _X_DeltaZ = 0;
        private double _Y_reference_Distance = 0;
        private double _Y_Delta = 0;
        private double _Y_DeltaX = 0;
        private double _Y_DeltaY = 0;
        private double _Y_DeltaZ = 0;
        private double _Z_reference_Distance = 0;
        private double _Z_Delta = 0;
        private double _Z_DeltaX = 0;
        private double _Z_DeltaY = 0;
        private double _Z_DeltaZ = 0;
        private double _OffsetX = 0;
        private double _OffsetY = 0;
        private double _OffsetZ = 0;
        private double _diff_X = 0;
        private double _diff_Y = 0;
        private double _diff_Z = 0;
        private double _intercept_X = 0;
        private double _intercept_Y = 0;
        private double _intercept_Z = 0;
        private double _Rotate_Theta = 0;
        private int _CalibrationMode = 0;    // 0: None, 1: All, 2: Two Point


        public bool _CoordinateSwitchEnable { get; set; } = false;
        public double _imagetoSystemXcoordi { get; set; } = 1;
        public double _imagetoSystemYcoordi { get; set; } = 1;
        public bool _CoordinateCalibrationActive { get; set; } = false;
        public double X_reference_Distance { get { return _X_reference_Distance; } set { _X_reference_Distance = value; } }
        public double X_Delta { get { return _X_Delta; } set { _X_Delta = value; } }
        public double X_DeltaX { get { return _X_DeltaX; } set { _X_DeltaX = value; } }
        public double X_DeltaY { get { return _X_DeltaY; } set { _X_DeltaY = value; } }
        public double X_DeltaZ { get { return _X_DeltaZ; } set { _X_DeltaZ = value; } }
        public double Y_reference_Distance { get { return _Y_reference_Distance; } set { _Y_reference_Distance = value; } }
        public double Y_Delta { get { return _Y_Delta; } set { _Y_Delta = value; } }
        public double Y_DeltaX { get { return _Y_DeltaX; } set { _Y_DeltaX = value; } }
        public double Y_DeltaY { get { return _Y_DeltaY; } set { _Y_DeltaY = value; } }
        public double Y_DeltaZ { get { return _Y_DeltaZ; } set { _Y_DeltaZ = value; } }
        public double Z_reference_Distance { get { return _Z_reference_Distance; } set { _Z_reference_Distance = value; } }
        public double Z_Delta { get { return _Z_Delta; } set { _Z_Delta = value; } }

        public double OffsetX { get { return _OffsetX; } set { _OffsetX = value; } }
        public double OffsetY { get { return _OffsetY; } set { _OffsetY = value; } }
        public double OffsetZ { get { return _OffsetZ; } set { _OffsetZ = value; } }

        public double Diff_X { get { return _diff_X; } set { _diff_X = value; } }
        public double Diff_Y { get { return _diff_Y; } set { _diff_Y = value; } }
        public double Diff_Z { get { return _diff_Z; } set { _diff_Z = value; } }

        public double intercept_X { get { return _intercept_X; } set { _intercept_X = value; } }
        public double intercept_Y { get { return _intercept_Y; } set { _intercept_Y = value; } }
        public double intercept_Z { get { return _intercept_Z; } set { _intercept_Z = value; } }

        public double Rotate_Theta { get { return _Rotate_Theta; } set { _Rotate_Theta = value; } }
        public int CalibrationMode { get { return _CalibrationMode; } set { _CalibrationMode = value; } }
        

        public class Position
        {
            public double X { get; set; } = 0;
            public double Y { get; set; } = 0;            
            public double Z { get; set; } = 0;
            public double R { get; set; } = 0;
        }
        public class Calibration_Position
        {
            public double X { get; set; } = 0;
            public double Y { get; set; } = 0;
            public double Delta_X { get; set; } = 0;
            public double Delta_Y { get; set; } = 0;
        }
        public void Calibration_TwoPoint(Calibration_Position p1, Calibration_Position p2)
        {
            if (Math.Abs((p2.X + p2.Delta_X) - (p1.X + p1.Delta_X)) > double.Epsilon)
            {
                _diff_X = (p2.X - p1.X) / ((p2.X + p2.Delta_X) - (p1.X + p1.Delta_X));
                _intercept_X = p1.X - (_diff_X * (p1.X + p1.Delta_X));
            }
            else
                _OffsetX = 0;

            if (Math.Abs((p2.Y + p2.Delta_Y) - (p1.Y + p1.Delta_Y)) > double.Epsilon)
            {
                _diff_Y = (p2.Y - p1.Y) / ((p2.Y + p2.Delta_Y) - (p1.Y + p1.Delta_Y));
                _intercept_Y = p1.Y - (_diff_Y * (p1.Y + p1.Delta_Y));
            }
            else
            {
                _OffsetX = 0;
                _OffsetY = 0;
            }
        }
        public void CalibrationOffset_TwoPoint(Calibration_Position p)
        {
            if (_CalibrationMode == 2)
            {
                _OffsetX = (_diff_X * p.X) + _intercept_X;
                _OffsetY = (_diff_Y * p.Y) + _intercept_Y;
            }            
        }
    }
}
