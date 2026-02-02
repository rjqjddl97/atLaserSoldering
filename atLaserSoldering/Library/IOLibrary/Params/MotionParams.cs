using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class MotionParams
    {
        public float MenualMoveVelocity { get; set; } = 100F;

        public int Motor_Direction_X { get; set; } = 1;
        public int Motor_Direction_Y { get; set; } = 1;
        public int Motor_Direction_Z { get; set; } = -1;
        public int Motor_DirectionR { get; set; } = -1;

        public int OneTurnResolutionX { get; set; } = 10000;
        public int OneTurnResolutionY { get; set; } = 1000;
        public int OneTurnResolutionZ { get; set; } = 3600;
        public int OneTurnResolutionR { get; set; } = 3600;

        public float GearRatioX { get; set; } = 1;
        public float GearRatioY { get; set; } = 1;
        public float GearRatioZ { get; set; } = 1/120F;
        public float GearRatioR { get; set; } = 1;
        public float BallLeadX { get; set; } = 10;
        public float BallLeadY { get; set; } = 1;
        public float BallLeadZ { get; set; } = 360;
        public float BallLeadR { get; set; } = 360;



        public float MoveVelocity { get; set; } = 200F;
        public float MoveAcceleration { get; set; } = 1000F;
        public float DefineSmallValue { get; set; } = 0.1F;
        public float DefineMiddleValue { get; set; } = 1F;
        public float DefineHighValue { get; set; } = 10F;
        public double realPositionX { get; set; } = 0;
        public double realPositionY { get; set; } = 0;
        public double realPositionZ { get; set; } = 0;
        public double realPositionR { get; set; } = 0;

        public double Pulse2MMRatioX { get; set; } = 0F;
        public double Pulse2MMRatioY { get; set; } = 0F;
        public double Pulse2MMRatioZ { get; set; } = 0F;
        public double Pulse2MMRatioR { get; set; } = 0F;

        public int MM2PulseRatioX { get; set; } = 0;
        public int MM2PulseRatioY { get; set; } = 0;
        public int MM2PulseRatioZ { get; set; } = 0;
        public int MM2PulseRatioR { get; set; } = 0;
        public MotionParams()
        {
            SetParameterInitial();
        }
        public void SetParameterInitial()
        {
            if (OneTurnResolutionX != 0)
                Pulse2MMRatioX = (double)((BallLeadX * GearRatioX) / OneTurnResolutionX);
            else
                Pulse2MMRatioX = (double)((BallLeadX * GearRatioX) / 10000);

            if (OneTurnResolutionY != 0)
                Pulse2MMRatioY = (double)((BallLeadY * GearRatioY) / OneTurnResolutionY);
            else
                Pulse2MMRatioY = (double)((BallLeadY * GearRatioY) / 1000);

            if (OneTurnResolutionZ != 0)
                Pulse2MMRatioZ = (double)((BallLeadZ * GearRatioZ) / OneTurnResolutionZ);
            else
                Pulse2MMRatioZ = (double)((BallLeadZ * GearRatioZ) / 1000);

            if (OneTurnResolutionR != 0)
                Pulse2MMRatioR = (double)((BallLeadR * GearRatioR) / OneTurnResolutionR);
            else
                Pulse2MMRatioR = (double)((BallLeadR * GearRatioR) / 3600);

            if (Pulse2MMRatioX != 0)
                MM2PulseRatioX = (int)(Math.Round(1 / (double)Pulse2MMRatioX));
            else
                MM2PulseRatioX = 1000;

            if (Pulse2MMRatioY != 0)
                MM2PulseRatioY = (int)(Math.Round(1 / (double)Pulse2MMRatioY));
            else
                MM2PulseRatioY = 1000;

            if (Pulse2MMRatioZ != 0)
                MM2PulseRatioZ = (int)(Math.Round(1 / (double)Pulse2MMRatioZ));
            else
                MM2PulseRatioZ = 333;

            if (Pulse2MMRatioR != 0)
                MM2PulseRatioR = (int)(Math.Round(1 / (double)Pulse2MMRatioR));
            else
                MM2PulseRatioR = 333;
        }
        public void GetTransPositions(int ix, int iy, int iz)
        {
            realPositionX = (ix * Pulse2MMRatioX);            
            realPositionY = (iy * Pulse2MMRatioY);
            realPositionZ = (iz * Pulse2MMRatioZ);
        }
    }
}
