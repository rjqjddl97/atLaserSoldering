using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.IO.Ports;
using System.Timers;
using RecipeManager;
using LogLibrary;
using Basler;
using Cognex.VisionPro;
using Cognex.VisionPro.Implementation;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.ToolGroup;
using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro.PMAlign;

namespace CustomPages
{
    public partial class VisionControl : DevExpress.XtraEditors.XtraUserControl
    {
        public VisionControl()
        {
            InitializeComponent();
        }
    }
}
