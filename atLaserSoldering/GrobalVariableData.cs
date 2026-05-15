using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GrobalVariableData
{
    private static GrobalVariableData _grobaldata;
    public static GrobalVariableData GrobalData
    {
        get
        {
            if (_grobaldata == null)
                _grobaldata = new GrobalVariableData();
            return _grobaldata;
        }
    }
    private GrobalVariableData() { }

    public double PresentPositionX { get; set; }
    public double PresentPositionY { get; set; }
    public double PresentPositionZ { get; set; }
}

