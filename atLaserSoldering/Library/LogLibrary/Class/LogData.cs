using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogLibrary
{
    public class LogData
    {
        string _logLevel;
        string _dateTime;
        string _logger;
        string _message;

        public string Level
        {
            set { _logLevel = value; }
            get { return _logLevel; }
        }

        public string DateTime
        {
            set { _dateTime = value; }
            get { return _dateTime; }
        }

        public string Logger
        {
            set { _logger = value; }
            get { return _logger; }
        }

        public string Message
        {
            set { _message = value; }
            get { return _message; }
        }

        public LogData()
        {

        }

        public LogData(string level, string dateTime, string logger, string message)
        {
            this._logLevel = level;
            this._dateTime = dateTime;
            this._logger = logger;
            this._message = message;
        }
    }
}
