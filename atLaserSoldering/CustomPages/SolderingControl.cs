using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.IO.Ports;
using FeederControlLibrary.SerialCommunication;
using FeederControlLibrary.SerialCommunication.Control;
using FeederControlLibrary.SerialCommunication.Data;
using FeederControlLibrary.SerialCommunication.DataProcessor;
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
        private FeederControlLibrary.SerialCommunication.Control.CommunicationManager _mFeederCommunicationManager = null;
        private FeedData _mFeederData = new FeedData();
        private CoherentCompactMini.SerialCommunication.Control.CommunicationManager _mLaserCommunicationManager = null;
        private CompactMiniData _mLaserData = new CompactMiniData();
        LaserSoldering.LaserSoderingProcess _mLaserSoldering = null;
        private RecipeManager.FeederParams _FeederParam = null;
        public LaserSolderParameter _mMenualSolderingJob = new LaserSolderParameter();

        public event Action<string> LogWriteEvent;

        public bool IsOpenStatus = false;
        public System.Timers.Timer UpdateTimer = new System.Timers.Timer();

        public SolderingControl()
        {
            InitializeComponent();
            InitialSystem();
        }
        public void InitialSystem()
        {
            try
            {
                textEditReadyTime.EditValue = 200;
                textEditPreHeatTime.EditValue = 1000;
                textEditPreHeatPowerRatio.EditValue = 10.0D;
                textEditHeatTime.EditValue = 1000;
                textEditHeatPowerRatio.EditValue = 15.0D;
                textEditForwardFeedLength.EditValue = 5.0D;
                textEditForwardFeedVelocity.EditValue = 10D;
                textEditReverseFeedLength.EditValue = 3.0D;
                textEditReverseFeedVelocity.EditValue = 10D;
                textEditMenualFeedLength.EditValue = 5.0D;
                textEditMenualPowerRatio.EditValue = 15;
            }
            catch (Exception ex)
            {
                ;
            }
        }
        public void ChangeSystemLanguage(bool _bsystemlanguage)
        {
            try
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
                layoutControlItemMenualPowerRatio.Text = "Power Ratio[%]";
            }
            catch (Exception ex)
            {
                ;
            }
        }        
        public void SetCommunicateManager(LaserSoderingProcess solder,ref FeederControlLibrary.SerialCommunication.Control.CommunicationManager feedmanager, ref CoherentCompactMini.SerialCommunication.Control.CommunicationManager lasermanager)
        {
            try
            {
                _mFeederCommunicationManager = feedmanager;
                _mLaserCommunicationManager = lasermanager;
                _mLaserSoldering = solder;
                _mLaserSoldering.InitialCommunication(ref lasermanager, ref feedmanager);
                //_mFeederCommunicationManager = _mLaserSoldering.FeederComm;
                //_mLaserCommunicationManager = _mLaserSoldering.CompactMiniComm;
            }
            catch (Exception ex)
            {
                ;
            }
        }
        public void SetCommunicateManager(LaserSoderingProcess solder, FeederControlLibrary.SerialCommunication.Control.CommunicationManager feedmanager, CoherentCompactMini.SerialCommunication.Control.CommunicationManager lasermanager)
        {
            try
            {
                _mFeederCommunicationManager = feedmanager;
                _mLaserCommunicationManager = lasermanager;
                _mLaserSoldering = solder;
                _mLaserSoldering.InitialCommunication(ref lasermanager, ref feedmanager);
            }
            catch (Exception ex)
            {
                ;
            }
        }
        public void SetCommunicationParams(CoherentCompactMini.SerialCommunication.Control.SerialPortSetData laser, FeederControlLibrary.SerialCommunication.Control.SerialPortSetData feeder, byte id)
        {
            try
            {
                //_mFeederCommunicationManager.mDrvCtrl.SetIDNumber(idnum, idarry);
                //_mFeederCommunicationManager.InitialPeriodData(idarry);            
                _mLaserSoldering.SetCommunicationParam(laser, feeder, id);
                _mFeederData = _mLaserSoldering.FeederData;
                _mLaserData = _mLaserSoldering.CompactMiniData;
            }
            catch (Exception ex)
            {
                ;
            }

        }
        public void ConnectionOpen(CoherentCompactMini.SerialCommunication.Control.SerialPortSetData setPortLaser, FeederControlLibrary.SerialCommunication.Control.SerialPortSetData setPortFeed, byte id)
        {
            try
            {
                byte[] ids = new byte[1];
                ids[0] = id;
                _mLaserSoldering.SetCommunicationParam(setPortLaser, setPortFeed, id);
                _mLaserSoldering.FeederComm.mFeedCtrl.SetIDNumber(1, ids);
                _mFeederData = _mLaserSoldering.FeederComm.mFeedCtrl;
                _mLaserData = _mLaserSoldering.CompactMiniComm.mLaserSourceCtrl;
                _mLaserSoldering.ConnectDeviceModule();
                IsOpenStatus = _mLaserSoldering.IsSolderingConnect;
                _mLaserSoldering.ReceiveDataLaserUpdateEvent += UpdateReceiveLaserData;
                _mLaserSoldering.ReceiveDataFeederUpdateEvent += UpdateReceiveFeederData;
                timerUpdateTime.Start();
                LogWriteEvent?.Invoke(string.Format("Feeder 제어 통신({0})과 레이저 제어 통신({1})이 연결 되었습니다", setPortFeed.PortName, setPortLaser.PortName));
            }
            catch (Exception ex)
            {
                ;
            }
        }
        public void ConnectionOpen(CoherentCompactMini.SerialCommunication.Control.SerialPortSetData setPortLaser, FeederControlLibrary.SerialCommunication.Control.SerialPortSetData setPortFeed)
        {
            try
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
            catch (Exception ex)
            {
                ;
            }
        }
        public void ConnectionClosed()
        {
            try
            {
                _mLaserSoldering.DisconnectDeviceModule();
                _mLaserSoldering.ReceiveDataLaserUpdateEvent -= UpdateReceiveLaserData;
                _mLaserSoldering.ReceiveDataFeederUpdateEvent -= UpdateReceiveFeederData;
                IsOpenStatus = _mLaserSoldering.IsSolderingConnect;
                timerUpdateTime.Stop();
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
            catch (Exception ex)
            {
                ;
            }
        }
        public void UpdateReceiveFeederData(FeedData update)
        {
            try
            {
                _mFeederData = update;
            }
            catch (Exception ex)
            {
                ;
            }            
        }
        public void UpdateReceiveLaserData(CompactMiniData update)
        {
            try
            {
                _mLaserData = update;
            }
            catch (Exception ex)
            {
                ;
            }            
        }
        public void SetFeederParam(RecipeManager.FeederParams _param)
        {
            try
            {
                _FeederParam = _param;
                _FeederParam.InitialParameter();
                _mLaserSoldering.FeederParam = _FeederParam;                
            }
            catch (Exception ex)
            {
                ;
            }            
        }
        private void simpleButtonLaserOn_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mLaserCommunicationManager.IsOpen())
                {
                    int datasize = 0;
                    datasize = _mLaserData.GetSetLaserOnPacketSize();
                    byte[] data = new byte[datasize];
                    //if (_mLaserCommunicationManager.IsReceiveAck())
                    {
                        if (_mLaserSoldering.IsLaserOn)
                        {
                            data = _mLaserData.GetSetLaserOn(false);
                            //simpleButtonLaserOn.Text = "Laser On";
                        }
                        else
                        {
                            data = _mLaserData.GetSetLaserOn(true);
                            //simpleButtonLaserOn.Text = "Laser Off";
                        }
                        _mLaserCommunicationManager.SendData(data);
                    }

                }
            }
            catch (Exception ex)
            {
                ;
            }
        }

        private void simpleButtonLaserPowerOn_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mLaserCommunicationManager.IsOpen())
                {
                    int datasize = 0;
                    datasize = _mLaserData.GetSetPowerOnPacketSize();
                    byte[] data = new byte[datasize];
                    //if (_mLaserCommunicationManager.IsReceiveAck())
                    {
                        if (_mLaserSoldering.IsLaserPowerOn)
                        {
                            data = _mLaserData.GetSetPowerOn(false);
                            //simpleButtonLaserOn.Text = "Power On";
                        }
                        else
                        {
                            data = _mLaserData.GetSetPowerOn(true);
                            //simpleButtonLaserOn.Text = "Power Off";
                        }
                        _mLaserCommunicationManager.SendData(data);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void simpleButtonPilotOn_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mLaserCommunicationManager.IsOpen())
                {
                    int datasize = 0;
                    datasize = _mLaserData.GetSetPositionLaserOnPacketSize();
                    byte[] data = new byte[datasize];
                    //if (_mLaserCommunicationManager.IsReceiveAck())
                    {
                        if (_mLaserSoldering.IsLaserPilotOn)
                        {
                            data = _mLaserData.GetSetPositioningLaserOn(false);
                            //simpleButtonPilotOn.Text = "Piolt On";
                        }
                        else
                        {
                            data = _mLaserData.GetSetPositioningLaserOn(true);
                            //simpleButtonPilotOn.Text = "Piolt Off";
                        }
                        _mLaserCommunicationManager.SendData(data);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void simpleButtonLaserReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mLaserCommunicationManager.IsOpen())
                {
                    //int datasize = 0;
                    //datasize = _mLaserData.GetSetErrorResetPacketSize();
                    //byte[] data = new byte[datasize];
                    ////if (_mLaserCommunicationManager.IsReceiveAck())
                    //{
                    //    if (_mLaserSoldering.IsLaserError)
                    //    {
                    //        data = _mLaserData.GetSetErrorReset();
                    //        //simpleButtonLaserOn.Text = "Reset On";
                    //    }
                    //    else
                    //    {
                    //        data = _mLaserData.GetSetErrorReset();
                    //        //simpleButtonLaserOn.Text = "Reset Off";
                    //    }
                    //    _mLaserCommunicationManager.SendData(data);
                    //}
                    if (_mLaserSoldering.IsLaserError)
                    {
                        _mLaserSoldering.LaserReset();
                    }
                    if (_mLaserSoldering.IsFeederError)
                    {
                        _mLaserSoldering.FeederReset();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void simpleButtonSolderingStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mLaserSoldering.IsSolderingConnect)
                {
                    if ((_mLaserSoldering.IsAutoSolderEnd) && (!_mLaserSoldering.IsAutoSoldering))
                    {
                        _mMenualSolderingJob.ReadyTime = Convert.ToInt32(textEditReadyTime.EditValue);
                        _mMenualSolderingJob.PreHeatTime = Convert.ToInt32(textEditPreHeatTime.EditValue);
                        _mMenualSolderingJob.PreheatPowerRatio = Convert.ToInt32(textEditPreHeatPowerRatio.EditValue)*10;
                        _mMenualSolderingJob.HeatTime = Convert.ToInt32(textEditHeatTime.EditValue);
                        _mMenualSolderingJob.HeatPowerRatio = Convert.ToInt32(textEditHeatPowerRatio.EditValue)*10;
                        _mMenualSolderingJob.ForwordingWireLength = Convert.ToDouble(textEditForwardFeedLength.EditValue);
                        _mMenualSolderingJob.ForwordingVelocity = Convert.ToDouble(textEditForwardFeedVelocity.EditValue);
                        _mMenualSolderingJob.ForwordingAcceleration = 100D;
                        _mMenualSolderingJob.ReverseWireLength = Convert.ToDouble(textEditReverseFeedLength.EditValue);
                        _mMenualSolderingJob.ReverseVelocity = Convert.ToDouble(textEditReverseFeedVelocity.EditValue);
                        _mMenualSolderingJob.ReverseAcceleration = 100D;

                        _mLaserSoldering.LaserSolderParam = _mMenualSolderingJob;
                        _mLaserSoldering.LaserSolderingStart();
                        simpleButtonSolderingStart.Text = "Soldering Stop";
                    }
                    else if (_mLaserSoldering.IsAutoSoldering)
                    {
                        _mLaserSoldering.LaserSolderingStop();
                        simpleButtonSolderingStart.Text = "Soldering Start";
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }
        public void InitialCompactMiniControl()
        {
            try
            {
                // 초기화 backgroudworker(Thead) 구문으로 변경필요!
                int datasize = 0;
                if (_mLaserCommunicationManager.IsOpen())
                {
                    datasize = _mLaserData.GetSetVT100ModePacketSize(0);
                    byte[] data = new byte[datasize];
                    data = _mLaserData.GetSetVT100Mode(0);
                    if (_mLaserCommunicationManager.IsReceiveAck())
                    {
                        _mLaserCommunicationManager.SendData(data);
                    }
                    Thread.Sleep(500);
                    datasize = _mLaserData.GetLaserCurrentSetPacketSize();
                    byte[] subdata1 = new byte[datasize];
                    subdata1 = _mLaserData.GetLaserCurrentSet(0);
                    if (_mLaserCommunicationManager.IsReceiveAck())
                    {
                        _mLaserCommunicationManager.SendData(subdata1);
                    }
                    Thread.Sleep(500);
                    datasize = _mLaserData.GetSetLaserControlPacketSize();
                    byte[] subdata2 = new byte[datasize];
                    subdata2 = _mLaserData.GetSetLaserControl(0);
                    if (_mLaserCommunicationManager.IsReceiveAck())
                    {
                        _mLaserCommunicationManager.SendData(subdata2);
                    }
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }
        private void timerUpdateTime_Tick(object sender, EventArgs e)
        {
            try
            {
                _mLaserSoldering.UpdateCommuncationDatas();
                if (_mLaserSoldering.IsAutoSoldering)
                {
                    if (simpleButtonSolderingStart.InvokeRequired)
                    {
                        simpleButtonSolderingStart.Invoke(new MethodInvoker(delegate { simpleButtonSolderingStart.Text = "Soldering Stop"; }));
                    }
                    else
                        simpleButtonSolderingStart.Text = "Soldering Stop";
                }
                else
                {
                    if (simpleButtonSolderingStart.InvokeRequired)
                    {
                        simpleButtonSolderingStart.Invoke(new MethodInvoker(delegate { simpleButtonSolderingStart.Text = "Soldering Start"; }));
                    }
                    else
                        simpleButtonSolderingStart.Text = "Soldering Start";
                }
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
                if (_mLaserSoldering.IsFeederError)
                {
                    if (labelControlFeederStatus.InvokeRequired)
                    {
                        labelControlFeederStatus.Invoke(new MethodInvoker(delegate { labelControlFeederStatus.ImageOptions.Image = global::atLaserSoldering.Properties.Resources.IconSetSigns3_16x16; }));
                    }
                    else
                        labelControlFeederStatus.ImageOptions.Image = global::atLaserSoldering.Properties.Resources.IconSetSigns3_16x16;
                }
                else
                {
                    if (labelControlFeederStatus.InvokeRequired)
                    {
                        labelControlFeederStatus.Invoke(new MethodInvoker(delegate { labelControlFeederStatus.ImageOptions.Image = global::atLaserSoldering.Properties.Resources.iconsetredtoblack4_16x16; }));
                    }
                    else
                        labelControlFeederStatus.ImageOptions.Image = global::atLaserSoldering.Properties.Resources.iconsetredtoblack4_16x16;
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }

        private void simpleButtonFeedCommand_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mLaserSoldering.IsSolderingConnect)
                {
                    byte[] data = new byte[20];
                    data = _mFeederData.MoveTargetPositionSendData(_mFeederData.DrvID[0], (int)Math.Round(Convert.ToDouble(textEditMenualFeedLength.EditValue) * _mLaserSoldering._FeederParam.FeedermmToPulseRatio));
                    _mFeederCommunicationManager.SendData(data);
                    data = _mFeederData.MoveReleativeCommand(_mFeederData.DrvID[0]);
                    _mFeederCommunicationManager.SendData(data);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void simpleButtonFeedNegative_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mLaserSoldering.IsSolderingConnect)
                {
                    byte[] data = new byte[20];
                    data = _mFeederData.CCWJogCommand(_mFeederData.DrvID[0]);
                    _mFeederCommunicationManager.SendData(data);
                }                
            }
            catch (Exception ex)
            {

            }
        }

        private void simpleButtonFeedStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mLaserSoldering.IsSolderingConnect)
                {
                    byte[] data = new byte[20];
                    data = _mFeederData.MoveStopCommand(_mFeederData.DrvID[0]);
                    _mFeederCommunicationManager.SendData(data);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void simpleButtonFeedPositive_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mLaserSoldering.IsSolderingConnect)
                {
                    byte[] data = new byte[20];
                    data = _mFeederData.CWJogCommand(_mFeederData.DrvID[0]);
                    _mFeederCommunicationManager.SendData(data);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void textEditMenualPowerRatio_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (_mLaserSoldering.IsSolderingConnect)
                {
                    int datasize = 0;
                    int setdata = (int)(Convert.ToDouble(textEditMenualPowerRatio.Text) * 10);
                    if ((setdata >= 100) && (setdata <= 1000))
                    {
                        datasize = _mLaserData.GetSetLaserPowerPacketSize(setdata);
                        byte[] data = new byte[datasize];
                        data = _mLaserData.GetSetLaserPower(setdata);
                        _mLaserCommunicationManager.SendData(data);
                    }
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }
    }
}
