using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class RobotInformation
    {
        private const double DECIMALPOINT = 0.001;
        private const double RECOVER_DECIMALPOINT = 1000;

        //public event Action<RobotInfomation> RobotInfomationUpdatedEvent;

        public double PositionX { get; set; }        
        public double PositionY { get; set; }
        public double PositionZ { get; set; }
        public double PositionR { get; set; }

        public DigitalOutputControl mInputData = new DigitalOutputControl();
        public DigitalOutputControl mOutputData = new DigitalOutputControl();
        public ulong mStatus;
        public ulong mError;
        public class DigitalOutputControl
        {
            public ulong Bit64 { get; set; }
            public bool B0 { get { return (Bit64 & (1U << 0)) != 0; } set { if (value) Bit64 |= (1U << 0); else Bit64 &= ~(1U << 0); } }
            public bool B1 { get { return (Bit64 & (1U << 1)) != 0; } set { if (value) Bit64 |= (1U << 1); else Bit64 &= ~(1U << 1); } }
            public bool B2 { get { return (Bit64 & (1U << 2)) != 0; } set { if (value) Bit64 |= (1U << 2); else Bit64 &= ~(1U << 2); } }
            public bool B3 { get { return (Bit64 & (1U << 3)) != 0; } set { if (value) Bit64 |= (1U << 3); else Bit64 &= ~(1U << 3); } }
            public bool B4 { get { return (Bit64 & (1U << 4)) != 0; } set { if (value) Bit64 |= (1U << 4); else Bit64 &= ~(1U << 4); } }
            public bool B5 { get { return (Bit64 & (1U << 5)) != 0; } set { if (value) Bit64 |= (1U << 5); else Bit64 &= ~(1U << 5); } }
            public bool B6 { get { return (Bit64 & (1U << 6)) != 0; } set { if (value) Bit64 |= (1U << 6); else Bit64 &= ~(1U << 6); } }
            public bool B7 { get { return (Bit64 & (1U << 7)) != 0; } set { if (value) Bit64 |= (1U << 7); else Bit64 &= ~(1U << 7); } }
            public bool B8 { get { return (Bit64 & (1U << 8)) != 0; } set { if (value) Bit64 |= (1U << 8); else Bit64 &= ~(1U << 8); } }
            public bool B9 { get { return (Bit64 & (1U << 9)) != 0; } set { if (value) Bit64 |= (1U << 9); else Bit64 &= ~(1U << 9); } }
            public bool B10 { get { return (Bit64 & (1U << 10)) != 0; } set { if (value) Bit64 |= (1U << 10); else Bit64 &= ~(1U << 10); } }
            public bool B11 { get { return (Bit64 & (1U << 11)) != 0; } set { if (value) Bit64 |= (1U << 11); else Bit64 &= ~(1U << 11); } }
            public bool B12 { get { return (Bit64 & (1U << 12)) != 0; } set { if (value) Bit64 |= (1U << 12); else Bit64 &= ~(1U << 12); } }
            public bool B13 { get { return (Bit64 & (1U << 13)) != 0; } set { if (value) Bit64 |= (1U << 13); else Bit64 &= ~(1U << 13); } }
            public bool B14 { get { return (Bit64 & (1U << 14)) != 0; } set { if (value) Bit64 |= (1U << 14); else Bit64 &= ~(1U << 14); } }
            public bool B15 { get { return (Bit64 & (1U << 15)) != 0; } set { if (value) Bit64 |= (1U << 15); else Bit64 &= ~(1U << 15); } }
            public bool B16 { get { return (Bit64 & (1U << 16)) != 0; } set { if (value) Bit64 |= (1U << 16); else Bit64 &= ~(1U << 16); } }
            public bool B17 { get { return (Bit64 & (1U << 17)) != 0; } set { if (value) Bit64 |= (1U << 17); else Bit64 &= ~(1U << 17); } }
            public bool B18 { get { return (Bit64 & (1U << 18)) != 0; } set { if (value) Bit64 |= (1U << 18); else Bit64 &= ~(1U << 18); } }
            public bool B19 { get { return (Bit64 & (1U << 19)) != 0; } set { if (value) Bit64 |= (1U << 19); else Bit64 &= ~(1U << 19); } }
            public bool B20 { get { return (Bit64 & (1U << 20)) != 0; } set { if (value) Bit64 |= (1U << 20); else Bit64 &= ~(1U << 20); } }
            public bool B21 { get { return (Bit64 & (1U << 21)) != 0; } set { if (value) Bit64 |= (1U << 21); else Bit64 &= ~(1U << 21); } }
            public bool B22 { get { return (Bit64 & (1U << 22)) != 0; } set { if (value) Bit64 |= (1U << 22); else Bit64 &= ~(1U << 22); } }
            public bool B23 { get { return (Bit64 & (1U << 23)) != 0; } set { if (value) Bit64 |= (1U << 23); else Bit64 &= ~(1U << 23); } }
            public bool B24 { get { return (Bit64 & (1U << 24)) != 0; } set { if (value) Bit64 |= (1U << 24); else Bit64 &= ~(1U << 24); } }
            public bool B25 { get { return (Bit64 & (1U << 25)) != 0; } set { if (value) Bit64 |= (1U << 25); else Bit64 &= ~(1U << 25); } }
            public bool B26 { get { return (Bit64 & (1U << 26)) != 0; } set { if (value) Bit64 |= (1U << 26); else Bit64 &= ~(1U << 26); } }
            public bool B27 { get { return (Bit64 & (1U << 27)) != 0; } set { if (value) Bit64 |= (1U << 27); else Bit64 &= ~(1U << 27); } }
            public bool B28 { get { return (Bit64 & (1U << 28)) != 0; } set { if (value) Bit64 |= (1U << 28); else Bit64 &= ~(1U << 28); } }
            public bool B29 { get { return (Bit64 & (1U << 29)) != 0; } set { if (value) Bit64 |= (1U << 29); else Bit64 &= ~(1U << 29); } }
            public bool B30 { get { return (Bit64 & (1U << 30)) != 0; } set { if (value) Bit64 |= (1U << 30); else Bit64 &= ~(1U << 30); } }
            public bool B31 { get { return (Bit64 & (1U << 31)) != 0; } set { if (value) Bit64 |= (1U << 31); else Bit64 &= ~(1U << 31); } }
            public bool B32 { get { return (Bit64 & (1U << 32)) != 0; } set { if (value) Bit64 |= (1U << 32); else Bit64 &= ~(1U << 32); } }
            public bool B33 { get { return (Bit64 & (1U << 33)) != 0; } set { if (value) Bit64 |= (1U << 33); else Bit64 &= ~(1U << 33); } }
            public bool B34 { get { return (Bit64 & (1U << 34)) != 0; } set { if (value) Bit64 |= (1U << 34); else Bit64 &= ~(1U << 34); } }
            public bool B35 { get { return (Bit64 & (1U << 35)) != 0; } set { if (value) Bit64 |= (1U << 35); else Bit64 &= ~(1U << 35); } }
            public bool B36 { get { return (Bit64 & (1U << 36)) != 0; } set { if (value) Bit64 |= (1U << 36); else Bit64 &= ~(1U << 36); } }
            public bool B37 { get { return (Bit64 & (1U << 37)) != 0; } set { if (value) Bit64 |= (1U << 37); else Bit64 &= ~(1U << 37); } }
            public bool B38 { get { return (Bit64 & (1U << 38)) != 0; } set { if (value) Bit64 |= (1U << 38); else Bit64 &= ~(1U << 38); } }
            public bool B39 { get { return (Bit64 & (1U << 39)) != 0; } set { if (value) Bit64 |= (1U << 39); else Bit64 &= ~(1U << 39); } }
            public bool B40 { get { return (Bit64 & (1U << 40)) != 0; } set { if (value) Bit64 |= (1U << 40); else Bit64 &= ~(1U << 40); } }
            public bool B41 { get { return (Bit64 & (1U << 41)) != 0; } set { if (value) Bit64 |= (1U << 41); else Bit64 &= ~(1U << 41); } }
            public bool B42 { get { return (Bit64 & (1U << 42)) != 0; } set { if (value) Bit64 |= (1U << 42); else Bit64 &= ~(1U << 42); } }
            public bool B43 { get { return (Bit64 & (1U << 43)) != 0; } set { if (value) Bit64 |= (1U << 43); else Bit64 &= ~(1U << 43); } }
            public bool B44 { get { return (Bit64 & (1U << 44)) != 0; } set { if (value) Bit64 |= (1U << 44); else Bit64 &= ~(1U << 44); } }
            public bool B45 { get { return (Bit64 & (1U << 45)) != 0; } set { if (value) Bit64 |= (1U << 45); else Bit64 &= ~(1U << 45); } }
            public bool B46 { get { return (Bit64 & (1U << 46)) != 0; } set { if (value) Bit64 |= (1U << 46); else Bit64 &= ~(1U << 46); } }
            public bool B47 { get { return (Bit64 & (1U << 47)) != 0; } set { if (value) Bit64 |= (1U << 47); else Bit64 &= ~(1U << 47); } }
            public bool B48 { get { return (Bit64 & (1U << 48)) != 0; } set { if (value) Bit64 |= (1U << 48); else Bit64 &= ~(1U << 48); } }
            public bool B49 { get { return (Bit64 & (1U << 49)) != 0; } set { if (value) Bit64 |= (1U << 49); else Bit64 &= ~(1U << 49); } }
            public bool B50 { get { return (Bit64 & (1U << 50)) != 0; } set { if (value) Bit64 |= (1U << 50); else Bit64 &= ~(1U << 50); } }
            public bool B51 { get { return (Bit64 & (1U << 51)) != 0; } set { if (value) Bit64 |= (1U << 51); else Bit64 &= ~(1U << 51); } }
            public bool B52 { get { return (Bit64 & (1U << 52)) != 0; } set { if (value) Bit64 |= (1U << 52); else Bit64 &= ~(1U << 52); } }
            public bool B53 { get { return (Bit64 & (1U << 53)) != 0; } set { if (value) Bit64 |= (1U << 53); else Bit64 &= ~(1U << 53); } }
            public bool B54 { get { return (Bit64 & (1U << 54)) != 0; } set { if (value) Bit64 |= (1U << 54); else Bit64 &= ~(1U << 54); } }
            public bool B55 { get { return (Bit64 & (1U << 55)) != 0; } set { if (value) Bit64 |= (1U << 55); else Bit64 &= ~(1U << 55); } }
            public bool B56 { get { return (Bit64 & (1U << 56)) != 0; } set { if (value) Bit64 |= (1U << 56); else Bit64 &= ~(1U << 56); } }
            public bool B57 { get { return (Bit64 & (1U << 57)) != 0; } set { if (value) Bit64 |= (1U << 57); else Bit64 &= ~(1U << 57); } }
            public bool B58 { get { return (Bit64 & (1U << 58)) != 0; } set { if (value) Bit64 |= (1U << 58); else Bit64 &= ~(1U << 58); } }
            public bool B59 { get { return (Bit64 & (1U << 59)) != 0; } set { if (value) Bit64 |= (1U << 59); else Bit64 &= ~(1U << 59); } }
            public bool B60 { get { return (Bit64 & (1U << 60)) != 0; } set { if (value) Bit64 |= (1U << 60); else Bit64 &= ~(1U << 60); } }
            public bool B61 { get { return (Bit64 & (1U << 61)) != 0; } set { if (value) Bit64 |= (1U << 61); else Bit64 &= ~(1U << 61); } }
            public bool B62 { get { return (Bit64 & (1U << 62)) != 0; } set { if (value) Bit64 |= (1U << 62); else Bit64 &= ~(1U << 62); } }
            public bool B63 { get { return (Bit64 & (1U << 63)) != 0; } set { if (value) Bit64 |= (1U << 63); else Bit64 &= ~(1U << 63); } }
            public byte[] GetData()
            {
                byte[] mRetValue = new byte[8];

                Buffer.BlockCopy(BitConverter.GetBytes(Bit64), 0, mRetValue, 0, sizeof(ulong));

                return mRetValue;
            }

            public void SetData(byte[] data)
            {
                if (data.Length < 8)
                    return;

                Bit64 = BitConverter.ToUInt64(data, 0);
            }
        }
        public enum RobotStatus
        {
            SystemReady = 0,
            ServoOn,
            Homming,
            OperationReady,
            MoveStop,
            Moving,
            Inposition,
            st7,
            OpMode,
            MenualMODE1,
            MenualMODE2,
            st11,
            st12,
            EmergencyStop,
            st16,
            Error,
            st17,
            st18,
            st19,
            st20,
            st21,
            st22,
            st23,
            st24,
            st25,
            st26,
            st27,
            st28,
            st29,
            st30,
            st31,
            st32
        }
        public enum ErrorStatus
        {
            EmergencyStop = 0,
            DrvError,
            ServoPower,
            Homming,
            OpReady,
            AxisReadStatus,
            AxisReadPosition,
            er7,
            MoveStop,
            EachAxisMove,
            MenualVelocityMove,
            MenualAngleMove,
            er11,
            er12,
            er13,
            er14,
            er15,
            er16,
            er17,
            er18,
            er19,
            er20,
            er21,
            er22,
            er23,
            er24,
            er25,
            er26,
            er27,
            er28,
            er29,
            er30,
            er31
        }
        public bool GetInputStatus(int i)
        {
            return (mInputData.Bit64 & (1U << i)) != 0;
        }

        public bool GetOutputStatus(int i)
        {
            return (mOutputData.Bit64 & (1U << i)) != 0;
        }

        public bool GetStatus(RobotStatus status)
        {
            return ((mStatus & (1U << (int)status)) != 0);
        }

        public bool GetStatus(ErrorStatus status)
        {
            return ((mError & (1U << (int)status)) != 0);
        }

        public void SetStatus(RobotStatus status, bool value)
        {
            if (value)
                mStatus |= (1U << (int)status);
            else
                mStatus &= ~(1U << (int)status);
        }
        public void SetError(ErrorStatus status, bool value)
        {
            if (value)
                mError |= (1U << (int)status);
            else
                mError &= ~(1U << (int)status);
        }
    }
}
