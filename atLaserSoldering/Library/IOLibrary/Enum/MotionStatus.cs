using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    [Flags]
    public enum MOTION_STATUS
    {
        MOTION_STATUS_DRIVE = 1,
        MOTION_STATUS_ERROR = 2,
        MOTION_STATUS_HOME = 4,
        MOTION_STATUS_PLUS_LIMIT = 8,
        MOTION_STATUS_MINUS_LIMIT = 16,
        MOTION_STATUS_EMG = 32,
        MOTION_STATUS_INPUT0 = 64,
        MOTION_STATUS_INPUT1 = 128
    }
}
