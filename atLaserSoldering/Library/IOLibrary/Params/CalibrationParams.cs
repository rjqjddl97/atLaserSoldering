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

        public bool _CoordinateSwitchEnable { get; set; } = false;
        public float _imagetoSystemXcoordi { get; set; } = 1;
        public float _imagetoSystemYcoordi { get; set; } = 1;
        public bool _CoordinateCalibrationActive { get; set; } = false;
        public float _X_reference_Distance { get; set; } = 0;
        public float _X_DeltaX { get; set; } = 0;
        public float _X_DeltaZ { get; set; } = 0;
        public float _Z_reference_Distance { get; set; } = 0;
        public float _Z_DeltaX { get; set; } = 0;
        public float _Z_DeltaZ { get; set; } = 0;
        public float _Y_reference_Distance { get; set; } = 0;
        public float _Y_DeltaX { get; set; } = 0;
        public float _Y_DeltaZ { get; set; } = 0;

        public float _Y_OffsetX { get; set; } = 0;
        public float _Y_OffsetY { get; set; } = 0;
        public float _Y_OffsetZ { get; set; } = 0;
        public float _X_diff_X { get; set; } = 1;
        public float _X_diff_Z { get; set; } = 1;
        public float _Z_diff_X { get; set; } = 1;
        public float _Z_diff_Z { get; set; } = 1;
        public float _Y_diff_X { get; set; } = 1;
        public float _Y_Rotate_X { get; set; } = 1;
        public float _Y_Rotate_Z { get; set; } = 1;
        public float _Y_Rotate_ThetaX { get; set; } = 1;
        public float _Y_Rotate_ThetaZ { get; set; } = 1;
        public float _Y_diff_Z { get; set; } = 1;
        public class Position
        {
            public double X { get; set; } = 0;
            public double Y { get; set; } = 0;            
            public double Z { get; set; } = 0;
            public double R { get; set; } = 0;
        }
        public Position EmiterCalibratinoDeltaPosition(Position PresentPos)
        {
            Position DeltaPosition = new Position();            

            if (_CoordinateCalibrationActive)
            {
                double x_dx = 0, x_dz = 0, z_dx = 0, z_dz = 0, y_dx = 0, y_dz = 0;
                x_dx = (YAxis_Z_Delta_D_X * PresentPos.Z) + YAxis_Z_OFFSET_X + YAxis_X_Delta_D_X * (PresentPos.X - YAxis_X_Referense_Pos);
                x_dz = (YAxis_X_Delta_D_Z * PresentPos.X) + YAxis_X_OFFSET_Z + YAxis_Z_Delta_D_Z * (PresentPos.Z - YAxis_Z_Referense_Pos);
                y_dx = YAxis_Y_Delta_D_X * (PresentPos.Y - YAxis_Y_Referense_Pos) + YAxis_Y_Delta_Offset_X;
                y_dz = YAxis_Y_Delta_D_Z * (PresentPos.Y - YAxis_Y_Referense_Pos) + YAxis_Y_Delta_Offset_Z;

                //DeltaPosition.X = x_dx + y_dx;
                DeltaPosition.X = x_dx;
                DeltaPosition.Y = 0;
                //DeltaPosition.Z = x_dz + y_dz;
                DeltaPosition.Z = x_dz;
                DeltaPosition.R = 0;;

                _Y_OffsetX = (float)DeltaPosition.X;
                _Y_OffsetY = (float)DeltaPosition.Y;
                _Y_OffsetZ = (float)DeltaPosition.Z;
                
            }
            else
            {
                DeltaPosition.X = 0;
                DeltaPosition.Y = 0;
                DeltaPosition.Z = 0;                

                _Y_OffsetX = (float)DeltaPosition.X;
                _Y_OffsetY = (float)DeltaPosition.Y;
                _Y_OffsetZ = (float)DeltaPosition.Z;
            }

            return DeltaPosition;
        }
        public Position InspectCalibratinoDeltaPosition(Position PresentPos)
        {
            Position DeltaPosition = new Position();
            if (_CoordinateCalibrationActive)
            {
                double x_dx = 0, x_dz = 0, z_dx = 0, z_dz = 0, y_dx = 0, y_dz = 0;
                //x_dx = (Y2Axis_Z_Delta_D_X * PresentPos.Z) + Y2Axis_Z_OFFSET_X + Y2Axis_X_Delta_D_X * (PresentPos.X - Y2Axis_X_Referense_Pos);
                //x_dz = (Y2Axis_X_Delta_D_Z * PresentPos.X) + Y2Axis_X_OFFSET_Z + Y2Axis_Z_Delta_D_Z * (PresentPos.Z - Y2Axis_Z_Referense_Pos);
                //y_dx = Y2Axis_Y_Delta_D_X * (PresentPos.Y2 - Y2Axis_Y_Referense_Pos) + Y2Axis_Y_Delta_Offset_X;
                //y_dz = Y2Axis_Y_Delta_D_Z * (PresentPos.Y2 - Y2Axis_Y_Referense_Pos) + Y2Axis_Y_Delta_Offset_Z;

                ////DeltaPosition.X = x_dx + y_dx;
                //DeltaPosition.X = x_dx;
                //DeltaPosition.Y2 = 0;
                ////DeltaPosition.Z = x_dz + y_dz;
                //DeltaPosition.Z = x_dz;
                //DeltaPosition.Y2 = 0;
                //DeltaPosition.FZ = 0;
                //DeltaPosition.FR = 0;

                //_Y2_OffsetX = (float)DeltaPosition.X;
                //_Y2_OffsetY = (float)DeltaPosition.Y1;
                //_Y2_OffsetZ = (float)DeltaPosition.Z;
            }
            else
            {
                DeltaPosition.X = 0;
                DeltaPosition.Y = 0;
                DeltaPosition.Z = 0;
                DeltaPosition.R = 0;
            }
            return DeltaPosition;
        }
    }
}
