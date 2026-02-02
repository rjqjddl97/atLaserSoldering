using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class PositionParams
    {
        public int _index { get; set; } = 0;
        public string _PositionName { get; set; } = "BasePosition";
        public float _X { get; set; } = 0F;
        public float _Y { get; set; } = 0F;        
        public float _Z { get; set; } = 0F;
        public PositionParams()
        {

        }
    }
}
