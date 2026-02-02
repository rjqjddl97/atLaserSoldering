using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basler
{
    public class CameraParameters
    {
        public double MaxValue { get; set; }
        public double MinValue { get; set; }
        public double Value { get; set; }

        public CameraParameters()
        {
            this.MaxValue = 0;
            this.MinValue = 0;
            this.Value = 0;
        }
    }
}
