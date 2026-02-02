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
using ArioModbusLibrary.SerialCommunication;
using ArioModbusLibrary.SerialCommunication.Control;
using ArioModbusLibrary.SerialCommunication.Data;
using ArioModbusLibrary.SerialCommunication.DataProcessor;
using System.IO;
using System.IO.Ports;
using System.Timers;
using RecipeManager;
using LogLibrary;
namespace CustomPages
{
    public partial class RemoteIOControl : DevExpress.XtraEditors.XtraUserControl
    {
        private CommunicationManager _mArioCommunicationManager = null;
        private ArioMRData _mArioData;        

        RecipeManager.RobotInformation _mRobotInfomation = new RecipeManager.RobotInformation();
        public event Action<RecipeManager.RobotInformation> RobotInfomationUpdatedEvent;
        public event Action<string> LogWriteEvent;

        public bool IsOpenStatus = false;
        public string _SerialPortName = "COM5";
        public int _iBaudrate = 6;
        public int _iDataBit = 1;
        public int _iStopBit = 0;
        public int _iParity = 0;
        public int _iFlowControl = 0;

        public System.Timers.Timer UpdateTimer = new System.Timers.Timer();
        public RemoteIOControl()
        {
            InitializeComponent();
            initialSystemDefineValue();
            LogWriteEvent?.Invoke(string.Format("원격IO 초기화 완료."));
        }
        public void ChangeSystemLanguage(bool _bsystemlanguage)
        {
            if (!_bsystemlanguage)
            {                
                groupControl2.Text = "Remote I/O Information";
                groupControl6.Text = "Digital Input";
                labelControlDIn32.Text = "32";
                labelControlDIn31.Text = "31";
                labelControlDIn30.Text = "30";
                labelControlDIn29.Text = "29";
                labelControlDIn28.Text = "28";
                labelControlDIn27.Text = "27";
                labelControlDIn26.Text = "26";
                labelControlDIn25.Text = "25";
                labelControlDIn24.Text = "24";
                labelControlDIn23.Text = "23";
                labelControlDIn22.Text = "22";
                labelControlDIn21.Text = "21";
                labelControlDIn20.Text = "20";
                labelControlDIn19.Text = "19";
                labelControlDIn18.Text = "18";
                labelControlDIn17.Text = "17";
                labelControlDIn16.Text = "16";
                labelControlDIn15.Text = "15";
                labelControlDIn14.Text = "14";
                labelControlDIn13.Text = "13";
                labelControlDIn12.Text = "12";
                labelControlDIn11.Text = "11";
                labelControlDIn10.Text = "10";
                labelControlDIn9.Text = "9";
                labelControlDIn8.Text = "8";
                labelControlDIn7.Text = "7";
                labelControlDIn6.Text = "6";
                labelControlDIn5.Text = "5";
                labelControlDIn4.Text = "Jig";
                labelControlDIn3.Text = "Reset";
                labelControlDIn2.Text = "Start";
                labelControlDIn1.Text = "EMG";

                groupControl5.Text = "Digital Output";
                labelControlDOut32.Text = "32";
                labelControlDOut31.Text = "31";
                labelControlDOut30.Text = "30";
                labelControlDOut29.Text = "29";
                labelControlDOut28.Text = "28";
                labelControlDOut27.Text = "27";
                labelControlDOut26.Text = "26";
                labelControlDOut25.Text = "25";
                labelControlDOut24.Text = "24";
                labelControlDOut23.Text = "23";
                labelControlDOut22.Text = "22";
                labelControlDOut21.Text = "21";
                labelControlDOut20.Text = "20";
                labelControlDOut19.Text = "19";
                labelControlDOut18.Text = "18";
                labelControlDOut17.Text = "17";
                labelControlDOut16.Text = "16";
                labelControlDOut15.Text = "15";
                labelControlDOut14.Text = "14";
                labelControlDOut13.Text = "13";
                labelControlDOut12.Text = "12";
                labelControlDOut11.Text = "11";
                labelControlDOut10.Text = "10";
                labelControlDOut9.Text = "9";
                labelControlDOut8.Text = "8";
                labelControlDOut7.Text = "7";
                labelControlDOut6.Text = "6";
                labelControlDOut5.Text = "5";
                labelControlDOut4.Text = "4";
                labelControlDOut3.Text = "3";
                labelControlDOut2.Text = "2";
                labelControlDOut1.Text = "S PWR";
            }
            else
            {                
                groupControl2.Text = "Remote I/O 정보";
                groupControl6.Text = "Digital Input";
                labelControlDIn32.Text = "32";
                labelControlDIn31.Text = "31";
                labelControlDIn30.Text = "30";
                labelControlDIn29.Text = "29";
                labelControlDIn28.Text = "28";
                labelControlDIn27.Text = "27";
                labelControlDIn26.Text = "26";
                labelControlDIn25.Text = "25";
                labelControlDIn24.Text = "24";
                labelControlDIn23.Text = "23";
                labelControlDIn22.Text = "22";
                labelControlDIn21.Text = "21";
                labelControlDIn20.Text = "20";
                labelControlDIn19.Text = "19";
                labelControlDIn18.Text = "18";
                labelControlDIn17.Text = "17";
                labelControlDIn16.Text = "16";
                labelControlDIn15.Text = "15";
                labelControlDIn14.Text = "14";
                labelControlDIn13.Text = "13";
                labelControlDIn12.Text = "12";
                labelControlDIn11.Text = "11";
                labelControlDIn10.Text = "10";
                labelControlDIn9.Text = "9";
                labelControlDIn8.Text = "8";
                labelControlDIn7.Text = "7";
                labelControlDIn6.Text = "6";
                labelControlDIn5.Text = "5";
                labelControlDIn4.Text = "Jig";
                labelControlDIn3.Text = "Reset";
                labelControlDIn2.Text = "Start";
                labelControlDIn1.Text = "EMG";

                groupControl5.Text = "Digital Output";
                labelControlDOut32.Text = "32";
                labelControlDOut31.Text = "31";
                labelControlDOut30.Text = "30";
                labelControlDOut29.Text = "29";
                labelControlDOut28.Text = "28";
                labelControlDOut27.Text = "27";
                labelControlDOut26.Text = "26";
                labelControlDOut25.Text = "25";
                labelControlDOut24.Text = "24";
                labelControlDOut23.Text = "23";
                labelControlDOut22.Text = "22";
                labelControlDOut21.Text = "21";
                labelControlDOut20.Text = "20";
                labelControlDOut19.Text = "19";
                labelControlDOut18.Text = "18";
                labelControlDOut17.Text = "17";
                labelControlDOut16.Text = "16";
                labelControlDOut15.Text = "15";
                labelControlDOut14.Text = "14";
                labelControlDOut13.Text = "13";
                labelControlDOut12.Text = "12";
                labelControlDOut11.Text = "11";
                labelControlDOut10.Text = "10";
                labelControlDOut9.Text = "9";
                labelControlDOut8.Text = "8";
                labelControlDOut7.Text = "7";
                labelControlDOut6.Text = "6";
                labelControlDOut5.Text = "5";
                labelControlDOut4.Text = "4";
                labelControlDOut3.Text = "3";
                labelControlDOut2.Text = "2";
                labelControlDOut1.Text = "S PWR";

            }
        }
        public void initialSystemDefineValue()
        {
            UpdateTimer.Interval = 100;
            UpdateTimer.Elapsed += new ElapsedEventHandler(UpdateRemoteIOData);
        }
        public void SetCommunicateManager(ref CommunicationManager manager)
        {
            _mArioCommunicationManager = manager;            
        }
        public void SetCommunicationData(int idnum, byte[] idarry)
        {
            _mArioCommunicationManager.mRemoteIOCtrl.SetIDNumber(idnum, idarry);
            _mArioData = _mArioCommunicationManager.mRemoteIOCtrl;
        }
        private void OutputControl_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mArioCommunicationManager != null)
                {
                    if (_mArioCommunicationManager.IsOpen())
                    {
                        ///*
                        if (!(sender is DevExpress.XtraEditors.LabelControl label)) return;
                        byte[] data = new byte[8];
                        int outputstatus = 0;
                        ushort OutputData = 0;
                        switch (label.Name)
                        {
                            case "labelControlDOut1":
                                outputstatus = _mArioData._mRemoteIODatas._CurrentOutputs[0];
                                if (Convert.ToBoolean(outputstatus & (1 << 0)))
                                {
                                    OutputData = (ushort)((outputstatus << 8) & 0xfe00);
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력1 Off."));
                                }
                                else
                                {
                                    OutputData = (ushort)((outputstatus << 8) | (1 << 8));
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력1 On."));
                                }
                                data = _mArioData.Output1byteCommand(_mArioCommunicationManager.mRemoteIOCtrl.DrvID[0], ArioMRData.OUTPUT_CONTROL_MAP.Output0, OutputData);
                                _mArioCommunicationManager.SendData(data);                                
                                break;
                            case "labelControlDOut2":
                                outputstatus = _mArioData._mRemoteIODatas._CurrentOutputs[0];

                                if (Convert.ToBoolean(outputstatus & (1 << 1)))
                                {
                                    OutputData = (ushort)((outputstatus << 8) & 0xfd00);
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력2 Off."));
                                }
                                else
                                {
                                    OutputData = (ushort)((outputstatus << 8) | (1 << 9));
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력2 On."));
                                }
                                data = _mArioData.Output1byteCommand(_mArioCommunicationManager.mRemoteIOCtrl.DrvID[0], ArioMRData.OUTPUT_CONTROL_MAP.Output0, OutputData);
                                _mArioCommunicationManager.SendData(data);
                                break;
                            case "labelControlDOut3":

                                outputstatus = _mArioData._mRemoteIODatas._CurrentOutputs[0];

                                if (Convert.ToBoolean(OutputData & (1 << 2)))
                                {
                                    OutputData = (ushort)((outputstatus << 8) & 0xfb00);
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력3 Off."));
                                }
                                else
                                {
                                    OutputData = (ushort)((outputstatus << 8) | (1 << 10));
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력3 On."));
                                }
                                data = _mArioData.Output1byteCommand(_mArioCommunicationManager.mRemoteIOCtrl.DrvID[0], ArioMRData.OUTPUT_CONTROL_MAP.Output0, OutputData);
                                _mArioCommunicationManager.SendData(data);
                                break;
                            case "labelControlDOut4":

                                outputstatus = _mArioData._mRemoteIODatas._CurrentOutputs[0];

                                if (Convert.ToBoolean(OutputData & (1 << 3)))
                                {
                                    OutputData = (ushort)((outputstatus << 8) & 0xf700);
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력4 Off."));
                                }
                                else
                                {
                                    OutputData = (ushort)((outputstatus << 8) | (1 << 11));
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력4 On."));
                                }
                                data = _mArioData.Output1byteCommand(_mArioCommunicationManager.mRemoteIOCtrl.DrvID[0], ArioMRData.OUTPUT_CONTROL_MAP.Output0, OutputData);
                                _mArioCommunicationManager.SendData(data);
                                break;
                            case "labelControlDOut5":

                                outputstatus = _mArioData._mRemoteIODatas._CurrentOutputs[0];

                                if (Convert.ToBoolean(OutputData & (1 << 4)))
                                {
                                    OutputData = (ushort)((outputstatus << 8) & 0xef00);
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력5 Off."));
                                }
                                else
                                {
                                    OutputData = (ushort)((outputstatus << 8) | (1 << 12));
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력5 On."));
                                }
                                data = _mArioData.Output1byteCommand(_mArioCommunicationManager.mRemoteIOCtrl.DrvID[0], ArioMRData.OUTPUT_CONTROL_MAP.Output0, OutputData);
                                _mArioCommunicationManager.SendData(data);
                                break;
                            case "labelControlDOut6":

                                outputstatus = _mArioData._mRemoteIODatas._CurrentOutputs[0];

                                if (Convert.ToBoolean(OutputData & (1 << 5)))
                                {
                                    OutputData = (ushort)((outputstatus << 8) & 0xdf00);
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력6 Off."));
                                }
                                else
                                {
                                    OutputData = (ushort)((outputstatus << 8) | (1 << 13));
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력6 On."));
                                }
                                data = _mArioData.Output1byteCommand(_mArioCommunicationManager.mRemoteIOCtrl.DrvID[0], ArioMRData.OUTPUT_CONTROL_MAP.Output0, OutputData);
                                _mArioCommunicationManager.SendData(data);
                                break;
                            case "labelControlDOut7":

                                outputstatus = _mArioData._mRemoteIODatas._CurrentOutputs[0];

                                if (Convert.ToBoolean(OutputData & (1 << 6)))
                                {
                                    OutputData = (ushort)((outputstatus << 8) & 0xbf00);
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력7 Off."));
                                }
                                else
                                {
                                    OutputData = (ushort)((outputstatus << 8) | (1 << 14));
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력7 On."));
                                }
                                data = _mArioData.Output1byteCommand(_mArioCommunicationManager.mRemoteIOCtrl.DrvID[0], ArioMRData.OUTPUT_CONTROL_MAP.Output0, OutputData);
                                _mArioCommunicationManager.SendData(data);
                                break;
                            case "labelControlDOut8":

                                outputstatus = _mArioData._mRemoteIODatas._CurrentOutputs[0];

                                if (Convert.ToBoolean(OutputData & (1 << 7)))
                                {
                                    OutputData = (ushort)((outputstatus << 8) & 0x7f00);
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력8 Off."));
                                }
                                else
                                {
                                    OutputData = (ushort)((outputstatus << 8) | (1 << 15));
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력8 On."));
                                }
                                data = _mArioData.Output1byteCommand(_mArioCommunicationManager.mRemoteIOCtrl.DrvID[0], ArioMRData.OUTPUT_CONTROL_MAP.Output0, OutputData);
                                _mArioCommunicationManager.SendData(data);
                                break;
                            case "labelControlDOut9":

                                outputstatus = _mArioData._mRemoteIODatas._CurrentOutputs[0];

                                if (Convert.ToBoolean(OutputData & (1 << 0)))
                                {
                                    OutputData = (ushort)((outputstatus << 8) & 0xfe00);
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력9 Off."));
                                }
                                else
                                {
                                    OutputData = (ushort)((outputstatus << 8) | (1 << 8));
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력9 On."));
                                }
                                data = _mArioData.Output1byteCommand(_mArioCommunicationManager.mRemoteIOCtrl.DrvID[0], ArioMRData.OUTPUT_CONTROL_MAP.Output1, OutputData);
                                _mArioCommunicationManager.SendData(data);
                                break;
                            case "labelControlDOut10":

                                outputstatus = _mArioData._mRemoteIODatas._CurrentOutputs[0];

                                if (Convert.ToBoolean(OutputData & (1 << 1)))
                                {
                                    OutputData = (ushort)((outputstatus << 8) & 0xfd00);
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력10 Off."));
                                }
                                else
                                {
                                    OutputData = (ushort)((outputstatus << 8) | (1 << 9));
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력10 On."));
                                }
                                data = _mArioData.Output1byteCommand(_mArioCommunicationManager.mRemoteIOCtrl.DrvID[0], ArioMRData.OUTPUT_CONTROL_MAP.Output1, OutputData);
                                _mArioCommunicationManager.SendData(data);
                                break;
                            case "labelControlDOut11":

                                outputstatus = _mArioData._mRemoteIODatas._CurrentOutputs[0];

                                if (Convert.ToBoolean(OutputData & (1 << 2)))
                                {
                                    OutputData = (ushort)((outputstatus << 8) & 0xfb00);
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력11 Off."));
                                }
                                else
                                {
                                    OutputData = (ushort)((outputstatus << 8) | (1 << 10));
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력11 On."));
                                }
                                data = _mArioData.Output1byteCommand(_mArioCommunicationManager.mRemoteIOCtrl.DrvID[0], ArioMRData.OUTPUT_CONTROL_MAP.Output1, OutputData);
                                _mArioCommunicationManager.SendData(data);
                                break;
                            case "labelControlDOut12":

                                outputstatus = _mArioData._mRemoteIODatas._CurrentOutputs[0];

                                if (Convert.ToBoolean(OutputData & (1 << 3)))
                                {
                                    OutputData = (ushort)((outputstatus << 8) & 0xf700);
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력12 Off."));
                                }
                                else
                                {
                                    OutputData = (ushort)((outputstatus << 8) | (1 << 11));
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력12 On."));
                                }
                                data = _mArioData.Output1byteCommand(_mArioCommunicationManager.mRemoteIOCtrl.DrvID[0], ArioMRData.OUTPUT_CONTROL_MAP.Output1, OutputData);
                                _mArioCommunicationManager.SendData(data);
                                break;
                            case "labelControlDOut13":

                                outputstatus = _mArioData._mRemoteIODatas._CurrentOutputs[0];

                                if (Convert.ToBoolean(OutputData & (1 << 4)))
                                {
                                    OutputData = (ushort)((outputstatus << 8) & 0xef00);
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력13 Off."));
                                }
                                else
                                {
                                    OutputData = (ushort)((outputstatus << 8) | (1 << 12));
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력13 On."));
                                }
                                data = _mArioData.Output1byteCommand(_mArioCommunicationManager.mRemoteIOCtrl.DrvID[0], ArioMRData.OUTPUT_CONTROL_MAP.Output1, OutputData);
                                _mArioCommunicationManager.SendData(data);
                                break;
                            case "labelControlDOut14":

                                outputstatus = _mArioData._mRemoteIODatas._CurrentOutputs[0];

                                if (Convert.ToBoolean(OutputData & (1 << 5)))
                                {
                                    OutputData = (ushort)((outputstatus << 8) & 0xdf00);
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력14 Off."));
                                }
                                else
                                {
                                    OutputData = (ushort)((outputstatus << 8) | (1 << 13));
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력14 On."));
                                }
                                data = _mArioData.Output1byteCommand(_mArioCommunicationManager.mRemoteIOCtrl.DrvID[0], ArioMRData.OUTPUT_CONTROL_MAP.Output1, OutputData);
                                _mArioCommunicationManager.SendData(data);
                                break;
                            case "labelControlDOut15":

                                outputstatus = _mArioData._mRemoteIODatas._CurrentOutputs[0];

                                if (Convert.ToBoolean(OutputData & (1 << 6)))
                                {
                                    OutputData = (ushort)((outputstatus << 8) & 0xbf00);
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력15 Off."));
                                }
                                else
                                {
                                    OutputData = (ushort)((outputstatus << 8) | (1 << 14));
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력15 On."));
                                }
                                data = _mArioData.Output1byteCommand(_mArioCommunicationManager.mRemoteIOCtrl.DrvID[0], ArioMRData.OUTPUT_CONTROL_MAP.Output1, OutputData);
                                _mArioCommunicationManager.SendData(data);
                                break;
                            case "labelControlDOut16":

                                outputstatus = _mArioData._mRemoteIODatas._CurrentOutputs[0];

                                if (Convert.ToBoolean(OutputData & (1 << 7)))
                                {
                                    OutputData = (ushort)((outputstatus << 8) & 0x7f00);
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력16 Off."));
                                }
                                else
                                {
                                    OutputData = (ushort)((outputstatus << 8) | (1 << 15));
                                    LogWriteEvent?.Invoke(string.Format("원격IO 출력16 On."));
                                }
                                data = _mArioData.Output1byteCommand(_mArioCommunicationManager.mRemoteIOCtrl.DrvID[0], ArioMRData.OUTPUT_CONTROL_MAP.Output1, OutputData);
                                _mArioCommunicationManager.SendData(data);
                                break;
                            default:
                                break;
                        }
                        //*/
                    }
                }
            }
            catch (Exception)
            {
                ;
            }
        }
        public void ConnectionOpen(SerialPortSetData setPort)
        {
            _mArioCommunicationManager.SetSerialData(setPort);
            if (!_mArioCommunicationManager.IsOpen())
            {
                _mArioCommunicationManager.Connect();
                if (_mArioCommunicationManager.IsOpen())
                {
                    IsOpenStatus = true;                    
                    _mArioCommunicationManager.ReceiveDataUpdateEvent += UpdateReceiveData;
                    UpdateTimer.Start();
                    LogWriteEvent?.Invoke(string.Format("원격 I/O 통신({0})이 연결 되었습니다", setPort.PortName));
                }
            }
        }
        public void ConnectionClosed()
        {
            if (_mArioCommunicationManager.IsOpen())
            {
                _mArioCommunicationManager.Disconnect();
                IsOpenStatus = false;
                _mArioCommunicationManager.ReceiveDataUpdateEvent -= UpdateReceiveData;                
                UpdateTimer.Stop();
                LogWriteEvent?.Invoke(string.Format("원격 I/O 통신이 연결해제 되었습니다"));
            }
        }
        public void UpdateReceiveData(ArioMRData update)
        {
            _mArioData = update;

            SetIOStatus(_mArioData);
        }
        private void UpdateRemoteIOData(object sender, ElapsedEventArgs e)
        {
            if (_mArioCommunicationManager.IsOpen())
            {
                //SetIOStatus( _mArioData._mRemoteIODatas);     
                RobotInfomationUpdatedEvent?.Invoke(_mRobotInfomation);
            }
        }
        public void SetIOStatus(ArioMRData update)
        {
            ArioMRData.IO_16Bit InData = new ArioMRData.IO_16Bit();
            ulong inputs = 0;

            InData.Bit16 = Convert.ToUInt16(update._mRemoteIODatas._CurrentInputs[0]);
            ShowStatus(labelControlDIn1, Convert.ToBoolean(InData.B0));
            ShowStatus(labelControlDIn2, Convert.ToBoolean(InData.B1));
            ShowStatus(labelControlDIn3, Convert.ToBoolean(InData.B2));
            ShowStatus(labelControlDIn4, Convert.ToBoolean(InData.B3));
            ShowStatus(labelControlDIn5, Convert.ToBoolean(InData.B4));
            ShowStatus(labelControlDIn6, Convert.ToBoolean(InData.B5));
            ShowStatus(labelControlDIn7, Convert.ToBoolean(InData.B6));
            ShowStatus(labelControlDIn8, Convert.ToBoolean(InData.B7));
            InData.Bit16 = Convert.ToUInt16(update._mRemoteIODatas._CurrentInputs[1]);
            ShowStatus(labelControlDIn9, Convert.ToBoolean(InData.B0));
            ShowStatus(labelControlDIn10, Convert.ToBoolean(InData.B1));
            ShowStatus(labelControlDIn11, Convert.ToBoolean(InData.B2));
            ShowStatus(labelControlDIn12, Convert.ToBoolean(InData.B3));
            ShowStatus(labelControlDIn13, Convert.ToBoolean(InData.B4));
            ShowStatus(labelControlDIn14, Convert.ToBoolean(InData.B5));
            ShowStatus(labelControlDIn15, Convert.ToBoolean(InData.B6));
            ShowStatus(labelControlDIn16, Convert.ToBoolean(InData.B7));
            InData.Bit16 = Convert.ToUInt16(update._mRemoteIODatas._CurrentInputs[2]);
            ShowStatus(labelControlDIn17, Convert.ToBoolean(InData.B0));
            ShowStatus(labelControlDIn18, Convert.ToBoolean(InData.B1));
            ShowStatus(labelControlDIn19, Convert.ToBoolean(InData.B2));
            ShowStatus(labelControlDIn20, Convert.ToBoolean(InData.B3));
            ShowStatus(labelControlDIn21, Convert.ToBoolean(InData.B4));
            ShowStatus(labelControlDIn22, Convert.ToBoolean(InData.B5));
            ShowStatus(labelControlDIn23, Convert.ToBoolean(InData.B6));
            ShowStatus(labelControlDIn24, Convert.ToBoolean(InData.B7));
            InData.Bit16 = Convert.ToUInt16(update._mRemoteIODatas._CurrentInputs[3]);
            ShowStatus(labelControlDIn25, Convert.ToBoolean(InData.B0));
            ShowStatus(labelControlDIn26, Convert.ToBoolean(InData.B1));
            ShowStatus(labelControlDIn27, Convert.ToBoolean(InData.B2));
            ShowStatus(labelControlDIn28, Convert.ToBoolean(InData.B3));
            ShowStatus(labelControlDIn29, Convert.ToBoolean(InData.B4));
            ShowStatus(labelControlDIn30, Convert.ToBoolean(InData.B5));
            ShowStatus(labelControlDIn31, Convert.ToBoolean(InData.B6));
            ShowStatus(labelControlDIn32, Convert.ToBoolean(InData.B7));

            InData.Bit16 = Convert.ToUInt16(update._mRemoteIODatas._CurrentOutputs[0]);
            ShowStatus(labelControlDOut1, Convert.ToBoolean(InData.B0));
            ShowStatus(labelControlDOut2, Convert.ToBoolean(InData.B1));
            ShowStatus(labelControlDOut3, Convert.ToBoolean(InData.B2));
            ShowStatus(labelControlDOut4, Convert.ToBoolean(InData.B3));
            ShowStatus(labelControlDOut5, Convert.ToBoolean(InData.B4));
            ShowStatus(labelControlDOut6, Convert.ToBoolean(InData.B5));
            ShowStatus(labelControlDOut7, Convert.ToBoolean(InData.B6));
            ShowStatus(labelControlDOut8, Convert.ToBoolean(InData.B7));
            InData.Bit16 = Convert.ToUInt16(update._mRemoteIODatas._CurrentOutputs[1]);
            ShowStatus(labelControlDOut9, Convert.ToBoolean(InData.B0));
            ShowStatus(labelControlDOut10, Convert.ToBoolean(InData.B1));
            ShowStatus(labelControlDOut11, Convert.ToBoolean(InData.B2));
            ShowStatus(labelControlDOut12, Convert.ToBoolean(InData.B3));
            ShowStatus(labelControlDOut13, Convert.ToBoolean(InData.B4));
            ShowStatus(labelControlDOut14, Convert.ToBoolean(InData.B5));
            ShowStatus(labelControlDOut15, Convert.ToBoolean(InData.B6));
            ShowStatus(labelControlDOut16, Convert.ToBoolean(InData.B7));
            InData.Bit16 = Convert.ToUInt16(update._mRemoteIODatas._CurrentOutputs[2]);
            ShowStatus(labelControlDOut17, Convert.ToBoolean(InData.B0));
            ShowStatus(labelControlDOut18, Convert.ToBoolean(InData.B1));
            ShowStatus(labelControlDOut19, Convert.ToBoolean(InData.B2));
            ShowStatus(labelControlDOut20, Convert.ToBoolean(InData.B3));
            ShowStatus(labelControlDOut21, Convert.ToBoolean(InData.B4));
            ShowStatus(labelControlDOut22, Convert.ToBoolean(InData.B5));
            ShowStatus(labelControlDOut23, Convert.ToBoolean(InData.B6));
            ShowStatus(labelControlDOut24, Convert.ToBoolean(InData.B7));
            InData.Bit16 = Convert.ToUInt16(update._mRemoteIODatas._CurrentOutputs[3]);
            ShowStatus(labelControlDOut25, Convert.ToBoolean(InData.B0));
            ShowStatus(labelControlDOut26, Convert.ToBoolean(InData.B1));
            ShowStatus(labelControlDOut27, Convert.ToBoolean(InData.B2));
            ShowStatus(labelControlDOut28, Convert.ToBoolean(InData.B3));
            ShowStatus(labelControlDOut29, Convert.ToBoolean(InData.B4));
            ShowStatus(labelControlDOut30, Convert.ToBoolean(InData.B5));
            ShowStatus(labelControlDOut31, Convert.ToBoolean(InData.B6));
            ShowStatus(labelControlDOut32, Convert.ToBoolean(InData.B7));
        }
        public void ShowStatus(DevExpress.XtraEditors.LabelControl label, bool status)
        {
            try
            {
                if (status)
                {
                    if (label.InvokeRequired)
                    {
                        label.Invoke(new MethodInvoker(delegate { label.ForeColor = Color.FromArgb(255, 255, 255, 255); label.BackColor = Color.FromArgb(255, 20, 200, 129); }));
                    }
                    else
                    {
                        label.ForeColor = Color.FromArgb(255, 255, 255, 255);
                        label.BackColor = Color.FromArgb(255, 20, 200, 129);
                    }
                }
                else
                {
                    if (label.InvokeRequired)
                    {
                        label.Invoke(new MethodInvoker(delegate { label.ForeColor = Color.FromArgb(255, 37, 37, 37); label.BackColor = Color.FromArgb(255, 224, 224, 224); }));
                    }
                    else
                    {
                        label.ForeColor = Color.FromArgb(255, 37, 37, 37);
                        label.BackColor = Color.FromArgb(255, 224, 224, 224);
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
