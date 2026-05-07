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
        public void UpdatePyrospotChart(UInt32 index, double dValue)
        {
            try
            {
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
                chartControlPyrospotData.Series[0].Points.Clear();

            }
            catch (Exception ex)
            {
                mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), string.Format("Message:{0}, StackTrace:{1}", ex.Message, ex.StackTrace));
            }
        }
    }
}
