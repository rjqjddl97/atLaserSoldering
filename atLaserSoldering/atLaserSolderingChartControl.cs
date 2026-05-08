using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using RecipeManager;
using System.IO;
using System.IO.Ports;
using System.Timers;
using System.Threading;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Basler;
using CustomPages;
using LogLibrary;
using AiCControlLibrary;
using FeederControlLibrary;
using ArioModbusLibrary;
using LaserSoldering;
using atLaserSoldering;
using Cognex.VisionPro;
using Cognex.VisionPro.Implementation;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.QuickBuild;
using Cognex.VisionPro.ToolGroup;
using Cognex.VisionPro.ToolBlock;

namespace atLaserSoldering
{
    public partial class atLaserSoldering
    {
        public UInt32 _uiChartIndexCount = 0;
        public bool _IsUpdateChartEnable = false;
        public int _iPyrospotDataSaveSeq = 0;
        public bool _IsPyrospotDataSaveEnable = false;
        DateTime _PyrospotDataTime = new DateTime();
        public string _PyrospotDataFilePath = string.Empty;
        private void PyrospotDataSaveWriteEvent(int iSeq)
        {
            try
            {
                _iPyrospotDataSaveSeq = iSeq;
                if (_iPyrospotDataSaveSeq == 1)
                {
                    _IsPyrospotDataSaveEnable = true;
                    CreatPyrospotDataFile();
                }
                else if (_iPyrospotDataSaveSeq == 2)
                    _IsPyrospotDataSaveEnable = false;
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "Pyrospot Data 로그 이벤트에 오류가 있습니다.");
            }
        }
        public void CreatPyrospotDataFile()
        {
            try
            {
                _PyrospotDataTime = DateTime.Now;
                string strFilePath = string.Format(@"{0}\Pyrospot\{1:0000}{2:00}{3:00}",
                    SystemDirectoryParams.ResultFolderPath,
                    _PyrospotDataTime.Year, _PyrospotDataTime.Month, _PyrospotDataTime.Day);

                string strDataPath = "";

                strDataPath = strFilePath + string.Format(@"\{0:00}{1:00}{2:00}.csv", _PyrospotDataTime.Hour, _PyrospotDataTime.Minute, _PyrospotDataTime.Second);
                _PyrospotDataFilePath = strDataPath;
                if (!Directory.Exists(strFilePath))
                {
                    Directory.CreateDirectory(strFilePath);
                }
            }
            catch (Exception ex)
            {
                mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), ex.Message);
                mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), ex.StackTrace.ToString());
            }
        }

        public void UpdatePyrospotChart(UInt32 index, double dValue)
        {
            try
            {                
                if (_IsPyrospotDataSaveEnable)
                {
                    DateTime presettime = DateTime.Now;
                    using (StreamWriter sw = new StreamWriter(_PyrospotDataFilePath, true))
                    {
                        string strTemp = "";
                        strTemp = string.Format("{0}, {1:0000.0}", presettime.TimeOfDay.ToString(),dValue);
                        sw.WriteLine(strTemp);
                    }
                }
                if (index >= 100)
                {
                    _uiChartIndexCount = 0;
                    chartControlPyrospotData.Series[0].Points.RemoveAt(0);
                }
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        chartControlPyrospotData.Series[0].Points.Add(new DevExpress.XtraCharts.SeriesPoint(index, dValue));
                    }));
                }
                else
                {
                    chartControlPyrospotData.Series[0].Points.Add(new DevExpress.XtraCharts.SeriesPoint(index, dValue));
                }                
            }
            catch (Exception ex)
            {
                mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), string.Format("Message:{0}, StackTrace:{1}", ex.Message, ex.StackTrace));
            }
        }
        private void InitializeChart()
        {
            try
            {
                _uiChartIndexCount = 0;
                //chartControlPyrospotData.Series[0].Points.Add(new DevExpress.XtraCharts.SeriesPoint(_uiChartIndexCount, 0));
                chartControlPyrospotData.Series[0].Points.Clear();                                
                
            }
            catch (Exception ex)
            {
                mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), string.Format("Message:{0}, StackTrace:{1}", ex.Message, ex.StackTrace));
            }
        }
    }
}
