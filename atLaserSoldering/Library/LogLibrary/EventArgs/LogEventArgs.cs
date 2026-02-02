using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLibrary
{
    public class LogEventArgs : EventArgs
    {
        private LogData _logData;

        public LogData Data
        {
            set { _logData = value; }
            get { return _logData; }
        }

        public LogEventArgs()
        {

        }

        public LogEventArgs(LogData data)
        {
            _logData = data;
        }
    }
}
