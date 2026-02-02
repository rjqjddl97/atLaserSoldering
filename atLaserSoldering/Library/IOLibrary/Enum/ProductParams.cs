using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace RecipeManager
{
    public enum ModelSeries
    {
        BTS = 0,
        BTF,
        BJ,
        BJP,
        BEN,
        BPS
    }
    public enum ModelType
    {
        MirrorReflective = 0,
        FixedDistanceReflective,
        DiffuseReflective,
        BGSReflective,
        LimitedReflective,
        TransmitLight,
        ReceiveLight
    }
    public enum OperationMode
    {         
        LightON = 0,         
        DarkON = 1
    }
    public enum OutPutType
    {
        NPN = 0,
        PNP
    }
    public enum DetectMeterial
    {
        None = 0,
        Mirror,
        WhitePaper,
        BlackPaper,
        Glass
    }
    
}