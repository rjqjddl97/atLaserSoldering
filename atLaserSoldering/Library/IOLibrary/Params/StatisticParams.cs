using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class StatisticParams
    {
        int _totalCount = 0;
        int _failCount = 0;
        int _passCount = 0;

        ArrayList _statistics = new ArrayList();

        public int TotalCount
        {
            get { return _totalCount; }
            set { _totalCount = value; }
        }

        public int FailCount
        {
            get { return _failCount; }
            set { _failCount = value; }
        }

        public int PassCount
        {
            get { return _passCount; }
            set { _passCount = value; }
        }

        public ArrayList Statistics
        {
            get { return _statistics; }
            set { _statistics = value; }
        }

        public StatisticParams(int arrayCount)
        {
            this._totalCount = 0;
            this._failCount = 0;
            this._passCount = 0;

            this._statistics = new ArrayList(arrayCount);
        }
    }
}
