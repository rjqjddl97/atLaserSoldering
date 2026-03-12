using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.IO.Ports;
using AiCControlLibrary.SerialCommunication;
using AiCControlLibrary.SerialCommunication.Control;
using AiCControlLibrary.SerialCommunication.Data;
using AiCControlLibrary.SerialCommunication.DataProcessor;
using CoherentCompactMini.SerialCommunication.Control;
using CoherentCompactMini.SerialCommunication.Data;
using CoherentCompactMini.SerialCommunication.DataProcessor;
using RecipeManager;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using LogLibrary;
using LaserSoldering;

namespace CustomPages
{
    public partial class SolderingControl : DevExpress.XtraEditors.XtraUserControl
    {
        private AiCControlLibrary.SerialCommunication.Control.CommunicationManager _mFeederCommunicationManager = null;
        private AiCData _mFeederData = new AiCData();
        private CoherentCompactMini.SerialCommunication.Control.CommunicationManager _mLaserCommunicationManager = null;
        private CompactMiniData _mLaserData = new CompactMiniData();
        LaserSoldering.LaserSoderingProcess _mLaserSoldering = new LaserSoderingProcess();
        private RecipeManager.FeederParams _FeederParam = null;

        public event Action<string> LogWriteEvent;

        public bool IsOpenStatus = false;
        public System.Timers.Timer UpdateTimer = new System.Timers.Timer();

        public SolderingControl()
        {
            InitializeComponent();
            _mLaserSoldering = new LaserSoderingProcess();
        }
        public void ChangeSystemLanguage(bool _bsystemlanguage)
        {
            if (!_bsystemlanguage)
            {
                layoutControlItemReadyTime.Text = "Ready Time[ms]";
                layoutControlItemPreHeatTime.Text = "PreHeat Time[ms]";
                layoutControlItemPreHeatPower.Text = "PreHeat Power[%]";
                layoutControlItemHeatTime.Text = "Heat Time[ms]";
                layoutControlItemHeatPower.Text = "Heat Power[%]";
                layoutControlItemForwardFeedLength.Text = "F. FeedLength[mm]";
                layoutControlItemForwardFeedVelocity.Text = "F. FeedVelocity[mm/s]";
                layoutControlItemReverseFeedLength.Text = "R. FeedLength[mm]";
                layoutControlItemReverseFeedVelocity.Text = "R. FeedVelocity[mm/s]";
                simpleButtonSolderingStart.Text = "Soldering Start";
            }
            else
            {
                layoutControlItemReadyTime.Text = "대기 시간[ms]";
                layoutControlItemPreHeatTime.Text = "예열 시간[ms]";
                layoutControlItemPreHeatPower.Text = "예열 파워[%]";
                layoutControlItemHeatTime.Text = "납땜 시간[ms]";
                layoutControlItemHeatPower.Text = "납땜 파워[%]";
                layoutControlItemForwardFeedLength.Text = "납 공급길이[mm]";
                layoutControlItemForwardFeedVelocity.Text = "납 공급속도[mm/s]";
                layoutControlItemReverseFeedLength.Text = "납 후퇴길이[mm]";
                layoutControlItemReverseFeedVelocity.Text = "납 후퇴속도[mm/s]";
                simpleButtonSolderingStart.Text = "납땜 시작";                
            }            
            labelControlPowerStatus.Text = "Power";
            labelControlLaserStatus.Text = "Laser";
            labelControlErrorStatus.Text = "Error";
            labelControlFatalErrorStatus.Text = "Fatal Error";
            labelControlEmissionStatus.Text = "Emission";            
            simpleButtonLaserOn.Text = "Laser On";
            simpleButtonLaserPowerOn.Text = "Power On";
            simpleButtonPilotOn.Text = "Pilot On";
            simpleButtonLaserReset.Text = "Reset";
        }
        public void SetCommunicateManager(ref AiCControlLibrary.SerialCommunication.Control.CommunicationManager feedmanager, ref CoherentCompactMini.SerialCommunication.Control.CommunicationManager lasermanager)
        {
            //_mFeederCommunicationManager = feedmanager;
            //_mLaserCommunicationManager = lasermanager;
            _mLaserSoldering.InitialCommunication(lasermanager, feedmanager);            
        }
        public void SetCommunicationParams(CoherentCompactMini.SerialCommunication.Control.SerialPortSetData laser, AiCControlLibrary.SerialCommunication.Control.SerialPortSetData feeder, byte id)
        {
            //_mFeederCommunicationManager.mDrvCtrl.SetIDNumber(idnum, idarry);
            //_mFeederCommunicationManager.InitialPeriodData(idarry);            
            _mLaserSoldering.SetCommunicationParam(laser, feeder, id);
            _mFeederData = _mLaserSoldering.FeederData;
            _mLaserData = _mLaserSoldering.CompactMiniData;
            
        }
        public void ConnectionOpen(CoherentCompactMini.SerialCommunication.Control.SerialPortSetData setPortLaser,AiCControlLibrary.SerialCommunication.Control.SerialPortSetData setPortFeed, byte id)
        {
            _mLaserSoldering.SetCommunicationParam(setPortLaser, setPortFeed, id);
            _mFeederData = _mLaserSoldering.FeederData;
            _mLaserData = _mLaserSoldering.CompactMiniData;
            _mLaserSoldering.ConnectDeviceModule();
            LogWriteEvent?.Invoke(string.Format("Feeder 제어 통신({0})과 레이저 제어 통신({1})이 연결 되었습니다", setPortFeed.PortName, setPortLaser.PortName));
        }
        public void ConnectionOpen(CoherentCompactMini.SerialCommunication.Control.SerialPortSetData setPortLaser, AiCControlLibrary.SerialCommunication.Control.SerialPortSetData setPortFeed)
        {
            //_mFeederCommunicationManager.SetSerialData(setPortFeed);
            //_mLaserCommunicationManager.SetSerialData(setPortLaser);            

            //if ((!_mFeederCommunicationManager.IsOpen()) && (!_mLaserCommunicationManager.IsOpen()))
            //{
            //    _mFeederCommunicationManager.Connect();
            //    _mLaserCommunicationManager.Connect();
            //    if ( _mFeederCommunicationManager.IsOpen() && _mLaserCommunicationManager.IsOpen() )
            //    {
            //        IsOpenStatus = true;
            //        _mFeederCommunicationManager.ReceiveDataUpdateEvent += UpdateReceiveFeederData;
            //        _mLaserCommunicationManager.ReceiveDataUpdateEvent += UpdateReceiveLaserData;
            //        UpdateTimer.Start();

            //        LogWriteEvent?.Invoke(string.Format("Feeder 제어 통신({0})과 레이저 제어 통신({1})이 연결 되었습니다", setPortFeed.PortName,setPortLaser.PortName));
            //    }
            //}
        }
        public void ConnectionClosed()
        {
            _mLaserSoldering.DisconnectDeviceModule();
            LogWriteEvent?.Invoke(string.Format("Feeder 및 레이저 제어 통신이 연결해제 되었습니다"));
            //if (_mFeederCommunicationManager.IsOpen() && _mLaserCommunicationManager.IsOpen())
            //{
            //    _mFeederCommunicationManager.Disconnect();
            //    _mLaserCommunicationManager.Disconnect();
            //    IsOpenStatus = false;
            //    _mFeederCommunicationManager.ReceiveDataUpdateEvent -= UpdateReceiveFeederData;
            //    _mLaserCommunicationManager.ReceiveDataUpdateEvent -= UpdateReceiveLaserData;
            //    UpdateTimer.Stop();


            //    LogWriteEvent?.Invoke(string.Format("Feeder 및 레이저 제어 통신이 연결해제 되었습니다"));
            //}
        }
        public void UpdateReceiveFeederData(AiCData update)
        {
            _mFeederData = update;            
        }
        public void UpdateReceiveLaserData(CompactMiniData update)
        {
            _mLaserData = update;
        }
        public void SetMotionParam(ref RecipeManager.FeederParams _param)
        {
            _FeederParam = _param;
        }
        private void simpleButtonLaserOn_Click(object sender, EventArgs e)
        {

        }

        private void simpleButtonLaserPowerOn_Click(object sender, EventArgs e)
        {

        }

        private void simpleButtonPilotOn_Click(object sender, EventArgs e)
        {

        }

        private void simpleButtonLaserReset_Click(object sender, EventArgs e)
        {

        }

        private void simpleButtonSolderingStart_Click(object sender, EventArgs e)
        {

        }

        private void timerUpdateTime_Tick(object sender, EventArgs e)
        {
            if (_mLaserData.Status.B0)
            {
                if (labelControlPowerStatus.InvokeRequired)
                {
                    labelControlPowerStatus.Invoke(new MethodInvoker(delegate { labelControlPowerStatus.ImageOptions.Image = global::atLaserSoldering.Properties.Resources.IconSetSigns3_16x16; }));
                }
                else
                    labelControlPowerStatus.ImageOptions.Image = global::atLaserSoldering.Properties.Resources.IconSetSigns3_16x16;            //IconSetSigns3_16x16(on), iconsetredtoblack4_16x16(off)

                if (simpleButtonLaserPowerOn.InvokeRequired)
                {
                    simpleButtonLaserPowerOn.Invoke(new MethodInvoker(delegate { simpleButtonLaserPowerOn.Text = "Power Off"; }));
                }
                else
                    simpleButtonLaserPowerOn.Text = "Power Off";
            }
            else
            {
                if (labelControlPowerStatus.InvokeRequired)
                {
                    labelControlPowerStatus.Invoke(new MethodInvoker(delegate { labelControlPowerStatus.ImageOptions.Image = global::atLaserSoldering.Properties.Resources.iconsetredtoblack4_16x16; }));
                }
                else
                    labelControlPowerStatus.ImageOptions.Image = global::atLaserSoldering.Properties.Resources.iconsetredtoblack4_16x16;

                if (simpleButtonLaserPowerOn.InvokeRequired)
                {
                    simpleButtonLaserPowerOn.Invoke(new MethodInvoker(delegate { simpleButtonLaserPowerOn.Text = "Power On"; }));
                }
                else
                    simpleButtonLaserPowerOn.Text = "Power On";

            }

            if (_mLaserData.Status.B1)
            {
                if (labelControlLaserStatus.InvokeRequired)
                {
                    labelControlLaserStatus.Invoke(new MethodInvoker(delegate { labelControlLaserStatus.ImageOptions.Image = global::atLaserSoldering.Properties.Resources.IconSetSigns3_16x16; }));
                }
                else
                    labelControlLaserStatus.ImageOptions.Image = global::atLaserSoldering.Properties.Resources.IconSetSigns3_16x16;

                if (simpleButtonLaserOn.InvokeRequired)
                {
                    simpleButtonLaserOn.Invoke(new MethodInvoker(delegate { simpleButtonLaserOn.Text = "Laser Off"; }));
                }
                else
                    simpleButtonLaserOn.Text = "Laser Off";

            }
            else
            {
                if (labelControlLaserStatus.InvokeRequired)
                {
                    labelControlLaserStatus.Invoke(new MethodInvoker(delegate { labelControlLaserStatus.ImageOptions.Image = global::atLaserSoldering.Properties.Resources.iconsetredtoblack4_16x16; }));
                }
                else
                    labelControlLaserStatus.ImageOptions.Image = global::atLaserSoldering.Properties.Resources.iconsetredtoblack4_16x16;

                if (simpleButtonLaserOn.InvokeRequired)
                {
                    simpleButtonLaserOn.Invoke(new MethodInvoker(delegate { simpleButtonLaserOn.Text = "Laser On"; }));
                }
                else
                    simpleButtonLaserOn.Text = "Laser On";
            }

            if (_mLaserData.Status.B2)
            {
                if (labelControlErrorStatus.InvokeRequired)
                {
                    labelControlErrorStatus.Invoke(new MethodInvoker(delegate { labelControlErrorStatus.ImageOptions.Image = global::atLaserSoldering.Properties.Resources.IconSetSigns3_16x16; }));
                }
                else
                    labelControlErrorStatus.ImageOptions.Image = global::atLaserSoldering.Properties.Resources.IconSetSigns3_16x16;
            }
            else
            {
                if (labelControlErrorStatus.InvokeRequired)
                {
                    labelControlErrorStatus.Invoke(new MethodInvoker(delegate { labelControlErrorStatus.ImageOptions.Image = global::atLaserSoldering.Properties.Resources.iconsetredtoblack4_16x16; }));
                }
                else
                    labelControlErrorStatus.ImageOptions.Image = global::atLaserSoldering.Properties.Resources.iconsetredtoblack4_16x16;
            }

            if (_mLaserData.Status.B3)
            {
                if (labelControlFatalErrorStatus.InvokeRequired)
                {
                    labelControlFatalErrorStatus.Invoke(new MethodInvoker(delegate { labelControlFatalErrorStatus.ImageOptions.Image = global::atLaserSoldering.Properties.Resources.IconSetSigns3_16x16; }));
                }
                else
                    labelControlFatalErrorStatus.ImageOptions.Image = global::atLaserSoldering.Properties.Resources.IconSetSigns3_16x16;
            }
            else
            {
                if (labelControlFatalErrorStatus.InvokeRequired)
                {
                    labelControlFatalErrorStatus.Invoke(new MethodInvoker(delegate { labelControlFatalErrorStatus.ImageOptions.Image = global::atLaserSoldering.Properties.Resources.iconsetredtoblack4_16x16; }));
                }
                else
                    labelControlFatalErrorStatus.ImageOptions.Image = global::atLaserSoldering.Properties.Resources.iconsetredtoblack4_16x16;
            }

            if (_mLaserData.Status.B4)
            {
                if (labelControlEmissionStatus.InvokeRequired)
                {
                    labelControlEmissionStatus.Invoke(new MethodInvoker(delegate { labelControlEmissionStatus.ImageOptions.Image = global::atLaserSoldering.Properties.Resources.IconSetSigns3_16x16; }));
                }
                else
                    labelControlEmissionStatus.ImageOptions.Image = global::atLaserSoldering.Properties.Resources.IconSetSigns3_16x16;
            }
            else
            {
                if (labelControlEmissionStatus.InvokeRequired)
                {
                    labelControlEmissionStatus.Invoke(new MethodInvoker(delegate { labelControlEmissionStatus.ImageOptions.Image = global::atLaserSoldering.Properties.Resources.iconsetredtoblack4_16x16; }));
                }
                else
                    labelControlEmissionStatus.ImageOptions.Image = global::atLaserSoldering.Properties.Resources.iconsetredtoblack4_16x16;
            }
        }
    }
}
