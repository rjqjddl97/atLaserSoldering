using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using NLog.Targets;

namespace LogLibrary
{
    public class Log 
    {
        public delegate void EventWriteLogViewer(object sender, LogEventArgs e);
        public event EventWriteLogViewer WriteLogViewer;

        Logger _logger = LogManager.GetCurrentClassLogger();
        MemoryTarget _memTarget = LogManager.Configuration.AllTargets[1] as MemoryTarget;
        MethodCallTarget _methodTarget = LogManager.Configuration.AllTargets[2] as MethodCallTarget;

        public void SetLogPath(string strPath)
        {
            if (_logger.Factory.Configuration.Variables.ContainsKey("runtime"))
            {
                _logger.Factory.Configuration.Variables["runtime"] = string.Format(@"{0}", strPath);
            }
            else
            {
                _logger.Factory.Configuration.Variables.Add("runtime", string.Format(@"{0}", strPath));
            }

            _logger.Factory.SuspendLogging();
            _logger.Factory.ReconfigExistingLoggers();
            _logger.Factory.ResumeLogging();
        }

        public Log()
        {
            ValidateLogMesaage();
        }

        public void WriteLog(LogLevel level, string strClass, string strMessage)
        {
            string strTemp = strClass + "|" + strMessage;
            switch (level)
            {
                case LogLevel.Debug:
                    _logger.Debug(strTemp);
                    break;

                case LogLevel.Error:
                    _logger.Error(strTemp);
                    break;

                case LogLevel.Fatal:
                    _logger.Fatal(strTemp);
                    break;

                case LogLevel.Info:
                    _logger.Info(strTemp);
                    break;

                case LogLevel.Trace:
                    _logger.Trace(strTemp);
                    break;

                case LogLevel.Warn:
                default:
                    _logger.Warn(strTemp);
                    break;
            }

            while (_memTarget.Logs.Count > 0)
            {
                string str = _memTarget.Logs[0];

                if (str != null)
                {
                    string[] strSplit = str.Split('|');

                    if (strSplit.Length > 0 && strSplit.Length < 6)
                    {
                        LogData logData = new LogData(strSplit[0], strSplit[1], strSplit[3], strSplit[4]);

                        OnWriteLogViewer(new LogEventArgs(logData));
                    }

                    if(_memTarget.Logs.Count > 0)
                        _memTarget.Logs.RemoveAt(0);
                }
            }
        }
            
        public static void WriteLogCallBack(string level, string longdate, string logger, string message)
        {
            
        }

        private void ValidateLogMesaage()
        {
            //ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object obj)
            //{
            //    while (true)
            //    {
            //        System.Threading.Monitor.Enter(_memTarget);
            //        try
            //        {
            //            if (_memTarget.Logs.Count > 0)
            //            {
            //                string str = _memTarget.Logs[0];

            //                if (str != null)
            //                {
            //                    string[] strTemp = str.Split('|');

            //                    if (strTemp.Length > 0 && strTemp.Length < 6)
            //                    {
            //                        //LogLevel level = (LogLevel)Enum.Parse(typeof(LogLevel), strTemp[0]);
            //                        LogData logData = new LogData(strTemp[0], strTemp[1], strTemp[3], strTemp[4]);

            //                        OnWriteLogViewer(new LogEventArgs(logData));
            //                    }

            //                    if (_memTarget.Logs.Count > 0)
            //                        _memTarget.Logs.RemoveAt(0);
            //                }
            //            }
            //        }
            //        finally
            //        {
            //            System.Threading.Monitor.Exit(_memTarget);
            //        }

            //        Thread.Sleep(1);
            //    }
            //}));
        }

        public void OnWriteLogViewer(LogEventArgs data)
        {
            if(this.WriteLogViewer != null)
            {
                WriteLogViewer(this, data);
            }
        }
    }
}
