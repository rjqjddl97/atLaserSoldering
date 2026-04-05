using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public enum INSPECTION_POSITION_MODE
    {
        POSITION_NOMAL_MODE,
        POSITION_INSPECTION_ALIGN_MODE,        
        POSITION_INSPECTION_MODE,
        POSITION_SOLDERING_MODE        
    }
    public enum INSPECTION_MODE
    {
        INSPECTION_MODE_NONE,
        INSPECTION_MODE_ALINE,
        INSPECTION_SOLDERING        
    }
    public enum ALIGNINSPECTMODE
    {
        None,
        TwoPoint,
        All
    }
}
