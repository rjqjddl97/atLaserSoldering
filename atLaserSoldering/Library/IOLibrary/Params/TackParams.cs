using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class TackParams
    {
        double _tackTimeSum = 0;
        long _inspectionCount = 0;

        public double TackTimeSum
        {
            set { _tackTimeSum = value; }
            get { return _tackTimeSum; }
        }

        public long InspectionCount
        {
            set { _inspectionCount = value; }
            get { return _inspectionCount; }
        }

        public TackParams(double tackTimeSum, long inspectionCount)
        {
            this._tackTimeSum = tackTimeSum;
            this._inspectionCount = inspectionCount;
        }
    }
}
