using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public enum ADMSEquipmentEventSubState
    {
        START,
        RUN,
        IDLE,
        STOP,
        END,
        ALARM,
        CHANGE
    }

    public enum ADMSEquipmentEvent
    {
        STATE,
        JOB
    }
}

