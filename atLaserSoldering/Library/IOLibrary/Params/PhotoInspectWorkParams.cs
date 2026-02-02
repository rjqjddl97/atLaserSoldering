using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class PhotoInspectWorkParams
    {
        SerialParams _SerialParam = new SerialParams();

        public SerialParams SerialParameters
        {
            get { return _SerialParam; }
            set { _SerialParam = value; }
        }
    }
}
