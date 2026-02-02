using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Basler
{
    public class GrabEndParam
    {
        private Bitmap _image = null;
        private ManualResetEvent _waitHandle = new ManualResetEvent(false);

        public Bitmap Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public ManualResetEvent WaitHandle
        {
            get { return _waitHandle; }
            set { _waitHandle = value; }
        }
    }
}
