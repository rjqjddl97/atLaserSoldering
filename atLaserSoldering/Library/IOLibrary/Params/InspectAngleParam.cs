using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class InspectAngleParam
    {
        double _angleSum = 0;
        long _inspectionCount = 0;

        public double AngleSum
        {
            set { _angleSum = value; }
            get { return _angleSum; }
        }

        public long InspectionCount
        {
            set { _inspectionCount = value; }
            get { return _inspectionCount; }
        }

        public InspectAngleParam(double angleSum, long inspectionCount)
        {
            this._angleSum = angleSum;
            this._inspectionCount = inspectionCount;
        }
    }
}
