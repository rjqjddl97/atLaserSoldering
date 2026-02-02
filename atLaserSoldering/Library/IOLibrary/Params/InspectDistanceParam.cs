using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class InspectDistanceParam
    {
        double _distanceSum = 0;
        long _inspectionCount = 0;

        public double DistanceSum
        {
            set { _distanceSum = value; }
            get { return _distanceSum; }
        }

        public long InspectionCount
        {
            set { _inspectionCount = value; }
            get { return _inspectionCount; }
        }

        public InspectDistanceParam(double distanceSum, long inspectionCount)
        {
            this._distanceSum = distanceSum;
            this._inspectionCount = inspectionCount;
        }
    }
}
