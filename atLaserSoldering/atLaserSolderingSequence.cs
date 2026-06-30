using System;
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
using CustomPages;
using LogLibrary;
using AiCControlLibrary;
using ArioModbusLibrary;
using LaserSoldering;
using atLaserSoldering;

namespace atLaserSoldering
{
    public partial class atLaserSoldering
    {
        double[] _RobotTargetPosition = new double[3];
        public bool _IsRequestAutomovingCommand = false;
        public bool _IsAutoSequenceCancleRequest = false; 
        private void OnLaserSolderingRecieveEnd(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {

                }
            }
            catch (Exception ex)
            {
                ;
            }
        }
        private void backgroundWorkerAutoSoldering_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {                
                if (sender is BackgroundWorker worker)
                {
                    LaserSolderParameter _mSolderingJob = new LaserSolderParameter();

                    if (_workParams._PCBAlignVisionEnable)
                    {
                        if (_workParams._AlignInspectionMode == 1)      // 0: None, 1: 2Point, 2: All
                        {
                            MotionParams _motParams = new MotionParams();
                            AiCControlLibrary.SerialCommunication.Data.AiCData _mDrvData = new AiCControlLibrary.SerialCommunication.Data.AiCData();
                            _mDrvData = _mMotionControlCommManager.mDrvCtrl;
                            _motParams = _systemParams._motionParams;
                            for (int i = 0; i < _workParams.SolderPositionParams.Count; i++)
                            {
                                if (mRobotInformation.mError != 0)
                                {
                                    e.Cancel = true;
                                    mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), "모션 에러에 의한 시퀀스 종료...");
                                }
                                //if (!e.Cancel)
                                if (!_IsAutoSequenceCancleRequest) 
                                {
                                    if (i == 0 && (_workParams.SolderPositionParams[i].ePositionType == INSPECTION_POSITION_MODE.POSITION_INSPECTION_ALIGN_MODE))
                                    {
                                        byte[] data = new byte[100];
                                        while ((mRobotInformation.mStatus & 0x00000052) != 0x00000052) ;
                                        for (int j = 0; j < _mMotionControlCommManager.mDrvCtrl.DeviceIDCount; j++)
                                        {
                                            if (j == 0)
                                            {
                                                data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionX * _motParams.MM2PulseRatioX));
                                                _mMotionControlCommManager.SendData(data);
                                                _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionX;
                                            }
                                            else if (j == 1)
                                            {
                                                data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionY * _motParams.MM2PulseRatioY));
                                                _mMotionControlCommManager.SendData(data);
                                                _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionY;
                                            }
                                            else if (j == 2)
                                            {
                                                data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionZ * _motParams.MM2PulseRatioZ));
                                                _mMotionControlCommManager.SendData(data);
                                                _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionZ;
                                            }
                                            Thread.Sleep(50);
                                        }
                                        data = _mDrvData.MoveAbsoluteCommand(129);
                                        _mMotionControlCommManager.SendData(data);
                                        Thread.Sleep(1000);
                                        _IsRequestAutomovingCommand = true;
                                        _waitHandle.Reset();
                                        _waitHandle.WaitOne();

                                        while ((mRobotInformation.mStatus & 0x00000052) != 0x00000052) ;
                                        Thread.Sleep(_workParams._ImageAcquisitionDelaytime);
                                        _Camera.OneShot(_waitHandle);
                                        _waitHandle.Reset();
                                        _waitHandle.WaitOne(_workParams._ImageAcquisitionDelaytime);

                                        if (_AlignToolBlock != null)
                                        {
                                            // "InputImage"는 Job의 입력 터미널 이름입니다.
                                            _AlignToolBlock.Inputs["InputImage"].Value = _sourceImage;
                                         
                                        }
                                        _AlignToolBlock.Run();

                                        if (_AlignToolBlock.RunStatus.Result == Cognex.VisionPro.CogToolResultConstants.Accept)
                                        {
                                            //textBoxResult.Text = "Pass";
                                             
                                        }
                                        else
                                        {
                                            //textBoxResult.Text = "Fail";
                                        }

                                        /*      // Insert PCB Align Vision Inspect Result Point1 Offset X,Y
                                         
                                         */

                                        GC.Collect();
                                    }
                                    else if (i == 1 && (_workParams.SolderPositionParams[i].ePositionType == INSPECTION_POSITION_MODE.POSITION_INSPECTION_ALIGN_MODE))
                                    {
                                        byte[] data = new byte[100];
                                        while ((mRobotInformation.mStatus & 0x00000052) != 0x00000052) ;
                                        for (int j = 0; j < _mMotionControlCommManager.mDrvCtrl.DeviceIDCount; j++)
                                        {
                                            if (j == 0)
                                            {
                                                data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionX * _motParams.MM2PulseRatioX));
                                                _mMotionControlCommManager.SendData(data);
                                                _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionX;
                                            }
                                            else if (j == 1)
                                            {
                                                data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionY * _motParams.MM2PulseRatioY));
                                                _mMotionControlCommManager.SendData(data);
                                                _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionY;
                                            }
                                            else if (j == 2)
                                            {
                                                data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionZ * _motParams.MM2PulseRatioZ));
                                                _mMotionControlCommManager.SendData(data);
                                                _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionZ;
                                            }
                                            Thread.Sleep(50);
                                        }
                                        data = _mDrvData.MoveAbsoluteCommand(129);
                                        _mMotionControlCommManager.SendData(data);
                                        Thread.Sleep(1000);
                                        _IsRequestAutomovingCommand = true;
                                        _waitHandle.Reset();
                                        _waitHandle.WaitOne();
                                        while ((mRobotInformation.mStatus & 0x00000052) != 0x00000052) ;
                                        Thread.Sleep(_workParams._ImageAcquisitionDelaytime);
                                        _Camera.OneShot(_waitHandle);
                                        _waitHandle.Reset();
                                        _waitHandle.WaitOne(_workParams._ImageAcquisitionDelaytime);

                                        if (_AlignToolBlock != null)
                                        {
                                            // "InputImage"는 Job의 입력 터미널 이름입니다.
                                            _AlignToolBlock.Inputs["InputImage"].Value = _sourceImage;

                                        }
                                        _AlignToolBlock.Run();

                                        //if (_AlignToolBlock.RunStatus.Result == Cognex.VisionPro.CogToolResultConstants.Accept)
                                        //{
                                        //    textBoxResult.Text = "Pass";
                                        //}
                                        //else
                                        //{
                                        //    textBoxResult.Text = "Fail";
                                        //}

                                        /*      // Insert PCB Align Vision Inspect Result Point2 Offset X,Y
                                         
                                         */

                                        GC.Collect();
                                    }
                                    else
                                    {
                                        byte[] data = new byte[100];
                                        while ((mRobotInformation.mStatus & 0x00000052) != 0x00000052) ;
                                        // Insert Align Offset Position !!
                                        for (int j = 0; j < _mMotionControlCommManager.mDrvCtrl.DeviceIDCount; j++)
                                        {
                                            if (j == 0)
                                            {
                                                data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionX * _motParams.MM2PulseRatioX));
                                                _mMotionControlCommManager.SendData(data);
                                                _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionX;
                                            }
                                            else if (j == 1)
                                            {
                                                data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionY * _motParams.MM2PulseRatioY));
                                                _mMotionControlCommManager.SendData(data);
                                                _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionY;
                                            }
                                            else if (j == 2)
                                            {
                                                data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionZ * _motParams.MM2PulseRatioZ));
                                                _mMotionControlCommManager.SendData(data);
                                                _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionZ;
                                            }
                                            Thread.Sleep(50);
                                        }
                                        data = _mDrvData.MoveAbsoluteCommand(129);
                                        _mMotionControlCommManager.SendData(data);
                                        Thread.Sleep(1000);
                                        _IsRequestAutomovingCommand = true;
                                        _waitHandle.Reset();
                                        _waitHandle.WaitOne();
                                        if (!_IsAutoSequenceCancleRequest) //if (!e.Cancel)
                                        {
                                            ///*
                                            // Insert Laser soldering Sequence Start                                    
                                            if (_workParams._SolderingProcessEnable && _workParams._UseLaserEnable && _workParams._UseFeederEnable)
                                            {
                                                while ((mRobotInformation.mStatus & 0x00000052) != 0x00000052) ;
                                                if (!_mLaserSoldering.IsAutoSolderError)
                                                {
                                                    _mSolderingJob.ReadyTime = _workParams.SolderPositionParams[i].ReadyTime;
                                                    _mSolderingJob.PreheatPowerRatio = (int)_workParams.SolderPositionParams[i].PreHeatPowerRatio;
                                                    _mSolderingJob.PreHeatTime = _workParams.SolderPositionParams[i].PreHeatTime;
                                                    _mSolderingJob.MeltingPowerRatio = (int)_workParams.SolderPositionParams[i].MeltingPowerRatio;
                                                    _mSolderingJob.MeltingTime = _workParams.SolderPositionParams[i].MeltingTime;
                                                    _mSolderingJob.HeatPowerRatio = (int)_workParams.SolderPositionParams[i].HeatPowerRatio;
                                                    _mSolderingJob.HeatTime = _workParams.SolderPositionParams[i].HeatTime;
                                                    _mSolderingJob.ForwordingWireLength = _workParams.SolderPositionParams[i].ForwardFeedLength;
                                                    _mSolderingJob.ForwordingVelocity = _workParams.SolderPositionParams[i].ForwardFeedVelocity;
                                                    _mSolderingJob.ReverseWireLength = _workParams.SolderPositionParams[i].ReverseFeedLength;
                                                    _mSolderingJob.ReverseVelocity = _workParams.SolderPositionParams[i].ReverseFeedVelocity;
                                                    _mSolderingJob.LaserProfileEnable = _workParams._SolderingProfileEnable;
                                                    _mSolderingJob.FeederEnable = _workParams._UseFeederEnable;
                                                    _mSolderingJob.LaserEnable = _workParams._UseLaserEnable;
                                                    _mLaserSoldering.LaserSolderParam = _mSolderingJob;
                                                    _mLaserSoldering.LaserSolderingStart();
                                                    _waitHandle.Reset();
                                                    _waitHandle.WaitOne();
                                                }
                                                else
                                                {
                                                    e.Cancel = true;
                                                    mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), "레이저 에러에 의한 시퀀스 종료...");
                                                }
                                            }

                                            //*/
                                        }
                                        else
                                        {
                                            _IsAutoSolderingRunning = false;
                                            _IsAutoSolderingEnd = false;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    _IsAutoSolderingRunning = false;
                                    _IsAutoSolderingEnd = false;
                                    break;
                                }
                                _backgroundWorkerAutoSoldering.ReportProgress(((i + 1) * 100) / (2*_workParams.SolderPositionParams.Count - 2));
                            }
                            // 납땜 검사 위치 이동.
                            
                            for (int i = 0; i < _workParams.SolderPositionParams.Count; i++)
                            {
                                if (mRobotInformation.mError != 0)
                                {
                                    e.Cancel = true;
                                    mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), "모션 에러에 의한 시퀀스 종료...");
                                }
                                if (!_IsAutoSequenceCancleRequest) //if (!e.Cancel)
                                {
                                    if (_workParams._SolderingInspectVisionEnable)
                                    {
                                        if (_workParams.SolderPositionParams[i].ePositionType == INSPECTION_POSITION_MODE.POSITION_SOLDERING_MODE)
                                        {
                                            byte[] data = new byte[100];
                                            while ((mRobotInformation.mStatus & 0x00000052) != 0x00000052) ;
                                            // Insert Align Offset Position !!
                                            for (int j = 0; j < _mMotionControlCommManager.mDrvCtrl.DeviceIDCount; j++)
                                            {
                                                if (j == 0)
                                                {
                                                    data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionX * _motParams.MM2PulseRatioX));
                                                    _mMotionControlCommManager.SendData(data);
                                                    _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionX;
                                                }
                                                else if (j == 1)
                                                {
                                                    data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionY * _motParams.MM2PulseRatioY));
                                                    _mMotionControlCommManager.SendData(data);
                                                    _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionY;
                                                }
                                                else if (j == 2)
                                                {
                                                    data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionZ * _motParams.MM2PulseRatioZ));
                                                    _mMotionControlCommManager.SendData(data);
                                                    _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionZ;
                                                }
                                                Thread.Sleep(50);
                                            }
                                            data = _mDrvData.MoveAbsoluteCommand(129);
                                            _mMotionControlCommManager.SendData(data);
                                            Thread.Sleep(1000);
                                            _IsRequestAutomovingCommand = true;
                                            _waitHandle.Reset();
                                            _waitHandle.WaitOne();
                                            while ((mRobotInformation.mStatus & 0x00000052) != 0x00000052) ;
                                            Thread.Sleep(_workParams._ImageAcquisitionDelaytime);
                                            _Camera.OneShot(_waitHandle);
                                            _waitHandle.Reset();
                                            _waitHandle.WaitOne(_workParams._ImageAcquisitionDelaytime);

                                            if (_InspectToolBlock != null)
                                            {
                                                // "InputImage"는 Job의 입력 터미널 이름입니다.
                                                _InspectToolBlock.Inputs["InputImage"].Value = _sourceImage;

                                            }
                                            _InspectToolBlock.Run();

                                            //if (_InspectToolBlock.RunStatus.Result == Cognex.VisionPro.CogToolResultConstants.Accept)
                                            //{
                                            //    textBoxResult.Text = "Pass";
                                            //}
                                            //else
                                            //{
                                            //    textBoxResult.Text = "Fail";
                                            //}

                                            /*      // Insert Soldering Vision Inspect Result

                                             */
                                            GC.Collect(); 

                                        }
                                    }
                                }
                                else
                                {
                                    _IsAutoSolderingRunning = false;
                                    _IsAutoSolderingEnd = false;
                                    break;
                                }
                                _backgroundWorkerAutoSoldering.ReportProgress(((i + 1) * 100) / (2 * _workParams.SolderPositionParams.Count - 2));
                            }
                        }
                        else if (_workParams._AlignInspectionMode == 2)
                        {
                            if (mRobotInformation.mError != 0)
                            {
                                e.Cancel = true;
                                mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), "모션 에러에 의한 시퀀스 종료...");
                            }
                            if (!_IsAutoSequenceCancleRequest) //if (!e.Cancel)
                            {
                                MotionParams _motParams = new MotionParams();
                                AiCControlLibrary.SerialCommunication.Data.AiCData _mDrvData = new AiCControlLibrary.SerialCommunication.Data.AiCData();
                                _mDrvData = _mMotionControlCommManager.mDrvCtrl;
                                _motParams = _systemParams._motionParams;
                                for (int i = 0; i < _workParams.SolderPositionParams.Count; i++)
                                {
                                    if (_workParams.SolderPositionParams[i].ePositionType == INSPECTION_POSITION_MODE.POSITION_SOLDERING_MODE)
                                    {
                                        byte[] data = new byte[100];
                                        while ((mRobotInformation.mStatus & 0x00000052) != 0x00000052) ;
                                        for (int j = 0; j < _mMotionControlCommManager.mDrvCtrl.DeviceIDCount; j++)
                                        {
                                            if (j == 0)
                                            {
                                                data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionX * _motParams.MM2PulseRatioX));
                                                _mMotionControlCommManager.SendData(data);
                                                _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionX;
                                            }
                                            else if (j == 1)
                                            {
                                                data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionY * _motParams.MM2PulseRatioY));
                                                _mMotionControlCommManager.SendData(data);
                                                _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionY;
                                            }
                                            else if (j == 2)
                                            {
                                                data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionZ * _motParams.MM2PulseRatioZ));
                                                _mMotionControlCommManager.SendData(data);
                                                _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionZ;
                                            }
                                            Thread.Sleep(50);
                                        }
                                        data = _mDrvData.MoveAbsoluteCommand(129);
                                        _mMotionControlCommManager.SendData(data);
                                        Thread.Sleep(1000);
                                        _IsRequestAutomovingCommand = true;
                                        _waitHandle.Reset();
                                        _waitHandle.WaitOne();
                                        while ((mRobotInformation.mStatus & 0x00000052) != 0x00000052) ;
                                        Thread.Sleep(_workParams._ImageAcquisitionDelaytime);
                                        _Camera.OneShot(_waitHandle);
                                        _waitHandle.Reset();
                                        _waitHandle.WaitOne(_workParams._ImageAcquisitionDelaytime);

                                        if (_AlignToolBlock != null)
                                        {
                                            // "InputImage"는 Job의 입력 터미널 이름입니다.
                                            _AlignToolBlock.Inputs["InputImage"].Value = _sourceImage;

                                        }
                                        _AlignToolBlock.Run();

                                        // Insert Align Offset Position !!
                                        for (int j = 0; j < _mMotionControlCommManager.mDrvCtrl.DeviceIDCount; j++)
                                        {
                                            if (j == 0)
                                            {
                                                data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionX * _motParams.MM2PulseRatioX));
                                                _mMotionControlCommManager.SendData(data);
                                                _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionX;
                                            }
                                            else if (j == 1)
                                            {
                                                data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionY * _motParams.MM2PulseRatioY));
                                                _mMotionControlCommManager.SendData(data);
                                                _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionY;
                                            }
                                            else if (j == 2)
                                            {
                                                data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionZ * _motParams.MM2PulseRatioZ));
                                                _mMotionControlCommManager.SendData(data);
                                                _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionZ;
                                            }
                                            Thread.Sleep(50);
                                        }
                                        data = _mDrvData.MoveAbsoluteCommand(129);
                                        _mMotionControlCommManager.SendData(data);
                                        Thread.Sleep(1000);
                                        _IsRequestAutomovingCommand = true;
                                        _waitHandle.Reset();
                                        _waitHandle.WaitOne();
                                        if (!_IsAutoSequenceCancleRequest) //if (!e.Cancel)
                                        {
                                            ///*
                                            // Insert Laser soldering Sequence Start                                    
                                            if (_workParams._SolderingProcessEnable && _workParams._UseLaserEnable && _workParams._UseFeederEnable)
                                            {
                                                while ((mRobotInformation.mStatus & 0x00000052) != 0x00000052) ;
                                                if (!_mLaserSoldering.IsAutoSolderError)
                                                {
                                                    _mSolderingJob.ReadyTime = _workParams.SolderPositionParams[i].ReadyTime;
                                                    _mSolderingJob.PreheatPowerRatio = (int)_workParams.SolderPositionParams[i].PreHeatPowerRatio;
                                                    _mSolderingJob.PreHeatTime = _workParams.SolderPositionParams[i].PreHeatTime;
                                                    _mSolderingJob.MeltingPowerRatio = (int)_workParams.SolderPositionParams[i].MeltingPowerRatio;
                                                    _mSolderingJob.MeltingTime = _workParams.SolderPositionParams[i].MeltingTime;
                                                    _mSolderingJob.HeatPowerRatio = (int)_workParams.SolderPositionParams[i].HeatPowerRatio;
                                                    _mSolderingJob.HeatTime = _workParams.SolderPositionParams[i].HeatTime;
                                                    _mSolderingJob.ForwordingWireLength = _workParams.SolderPositionParams[i].ForwardFeedLength;
                                                    _mSolderingJob.ForwordingVelocity = _workParams.SolderPositionParams[i].ForwardFeedVelocity;
                                                    _mSolderingJob.ReverseWireLength = _workParams.SolderPositionParams[i].ReverseFeedLength;
                                                    _mSolderingJob.ReverseVelocity = _workParams.SolderPositionParams[i].ReverseFeedVelocity;
                                                    _mSolderingJob.LaserProfileEnable = _workParams._SolderingProfileEnable;
                                                    _mSolderingJob.FeederEnable = _workParams._UseFeederEnable;
                                                    _mSolderingJob.LaserEnable = _workParams._UseLaserEnable;
                                                    _mLaserSoldering.LaserSolderParam = _mSolderingJob;
                                                    _mLaserSoldering.LaserSolderingStart();
                                                    _waitHandle.Reset();
                                                    _waitHandle.WaitOne();
                                                }
                                                else
                                                {
                                                    e.Cancel = true;
                                                    mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), "레이저 에러에 의한 시퀀스 종료...");
                                                }
                                            }
                                            //*/
                                        }
                                        else
                                        {
                                            _IsAutoSolderingRunning = false;
                                            _IsAutoSolderingEnd = false;
                                            break;
                                        }

                                        while ((mRobotInformation.mStatus & 0x00000052) != 0x00000052) ;
                                        // Insert Align Offset Position !!
                                        for (int j = 0; j < _mMotionControlCommManager.mDrvCtrl.DeviceIDCount; j++)
                                        {
                                            if (j == 0)
                                            {
                                                data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionX * _motParams.MM2PulseRatioX));
                                                _mMotionControlCommManager.SendData(data);
                                                _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionX;
                                            }
                                            else if (j == 1)
                                            {
                                                data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionY * _motParams.MM2PulseRatioY));
                                                _mMotionControlCommManager.SendData(data);
                                                _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionY;
                                            }
                                            else if (j == 2)
                                            {
                                                data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionZ * _motParams.MM2PulseRatioZ));
                                                _mMotionControlCommManager.SendData(data);
                                                _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionZ;
                                            }
                                            Thread.Sleep(50);
                                        }
                                        data = _mDrvData.MoveAbsoluteCommand(129);
                                        _mMotionControlCommManager.SendData(data);
                                        Thread.Sleep(1000);
                                        _IsRequestAutomovingCommand = true;
                                        _waitHandle.Reset();
                                        _waitHandle.WaitOne();
                                        while ((mRobotInformation.mStatus & 0x00000052) != 0x00000052) ;
                                        Thread.Sleep(_workParams._ImageAcquisitionDelaytime);
                                        _Camera.OneShot(_waitHandle);
                                        _waitHandle.Reset();
                                        _waitHandle.WaitOne(_workParams._ImageAcquisitionDelaytime);

                                        if (_InspectToolBlock != null)
                                        {
                                            // "InputImage"는 Job의 입력 터미널 이름입니다.
                                            _InspectToolBlock.Inputs["InputImage"].Value = _sourceImage;

                                        }
                                        _InspectToolBlock.Run();

                                        //if (_InspectToolBlock.RunStatus.Result == Cognex.VisionPro.CogToolResultConstants.Accept)
                                        //{
                                        //    textBoxResult.Text = "Pass";
                                        //}
                                        //else
                                        //{
                                        //    textBoxResult.Text = "Fail";
                                        //}

                                        /*      // Insert Soldering Vision Inspect Result

                                         */
                                        GC.Collect();

                                    }
                                    _backgroundWorkerAutoSoldering.ReportProgress(((i + 1) * 100) / (2*_workParams.SolderPositionParams.Count));
                                }
                            }
                            else
                            {
                                _IsAutoSolderingRunning = false;
                                _IsAutoSolderingEnd = false;
                            }
                        }
                        else
                        {
                            MotionParams _motParams = new MotionParams();
                            AiCControlLibrary.SerialCommunication.Data.AiCData _mDrvData = new AiCControlLibrary.SerialCommunication.Data.AiCData();
                            _mDrvData = _mMotionControlCommManager.mDrvCtrl;
                            _motParams = _systemParams._motionParams;
                            for (int i = 0; i < _workParams.SolderPositionParams.Count; i++)
                            {  
                                if (mRobotInformation.mError != 0)
                                {
                                    e.Cancel = true;
                                    mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), "모션 에러에 의한 시퀀스 종료...");
                                }
                                if (_workParams.SolderPositionParams[i].ePositionType == INSPECTION_POSITION_MODE.POSITION_NOMAL_MODE)
                                {
                                    if (!_IsAutoSequenceCancleRequest) //if (!e.Cancel)
                                    {
                                        byte[] data = new byte[100];
                                        while ((mRobotInformation.mStatus & 0x00000052) != 0x00000052) ;
                                        for (int j = 0; j < _mMotionControlCommManager.mDrvCtrl.DeviceIDCount; j++)
                                        {
                                            if (j == 0)
                                            {
                                                data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionX * _motParams.MM2PulseRatioX));
                                                _mMotionControlCommManager.SendData(data);
                                                _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionX;
                                            }
                                            else if (j == 1)
                                            {
                                                data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionY * _motParams.MM2PulseRatioY));
                                                _mMotionControlCommManager.SendData(data);
                                                _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionY;
                                            }
                                            else if (j == 2)
                                            {
                                                data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionZ * _motParams.MM2PulseRatioZ));
                                                _mMotionControlCommManager.SendData(data);
                                                _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionZ;
                                            }
                                            Thread.Sleep(50);
                                        }
                                        data = _mDrvData.MoveAbsoluteCommand(129);
                                        _mMotionControlCommManager.SendData(data);
                                        Thread.Sleep(1000);
                                        _IsRequestAutomovingCommand = true;
                                        _waitHandle.Reset();
                                        _waitHandle.WaitOne();
                                    }
                                    else
                                    {
                                        _IsAutoSolderingRunning = false;
                                        _IsAutoSolderingEnd = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (_workParams.SolderPositionParams[i].ePositionType == INSPECTION_POSITION_MODE.POSITION_SOLDERING_MODE)
                                    {
                                        if (!_IsAutoSequenceCancleRequest) //if (!e.Cancel)
                                        {
                                            byte[] data = new byte[100];
                                            while ((mRobotInformation.mStatus & 0x00000052) != 0x00000052) ;
                                            for (int j = 0; j < _mMotionControlCommManager.mDrvCtrl.DeviceIDCount; j++)
                                            {
                                                if (j == 0)
                                                {
                                                    data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionX * _motParams.MM2PulseRatioX));
                                                    _mMotionControlCommManager.SendData(data);
                                                    _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionX;
                                                }
                                                else if (j == 1)
                                                {
                                                    data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionY * _motParams.MM2PulseRatioY));
                                                    _mMotionControlCommManager.SendData(data);
                                                    _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionY;
                                                }
                                                else if (j == 2)
                                                {
                                                    data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionZ * _motParams.MM2PulseRatioZ));
                                                    _mMotionControlCommManager.SendData(data);
                                                    _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionZ;
                                                }
                                                Thread.Sleep(50);
                                            }
                                            data = _mDrvData.MoveAbsoluteCommand(129);
                                            _mMotionControlCommManager.SendData(data);
                                            Thread.Sleep(1000);
                                            _IsRequestAutomovingCommand = true;
                                            _waitHandle.Reset();
                                            _waitHandle.WaitOne();

                                            if (!_IsAutoSequenceCancleRequest) //if (!e.Cancel)
                                            {
                                                ///*
                                                // Insert Laser soldering Sequence Start                                    
                                                if (_workParams._SolderingProcessEnable && _workParams._UseLaserEnable && _workParams._UseFeederEnable)
                                                {
                                                    while ((mRobotInformation.mStatus & 0x00000052) != 0x00000052) ;
                                                    if (!_mLaserSoldering.IsAutoSolderError)
                                                    {
                                                        _mSolderingJob.ReadyTime = _workParams.SolderPositionParams[i].ReadyTime;
                                                        _mSolderingJob.PreheatPowerRatio = (int)_workParams.SolderPositionParams[i].PreHeatPowerRatio;
                                                        _mSolderingJob.PreHeatTime = _workParams.SolderPositionParams[i].PreHeatTime;
                                                        _mSolderingJob.MeltingPowerRatio = (int)_workParams.SolderPositionParams[i].MeltingPowerRatio;
                                                        _mSolderingJob.MeltingTime = _workParams.SolderPositionParams[i].MeltingTime;
                                                        _mSolderingJob.HeatPowerRatio = (int)_workParams.SolderPositionParams[i].HeatPowerRatio;
                                                        _mSolderingJob.HeatTime = _workParams.SolderPositionParams[i].HeatTime;
                                                        _mSolderingJob.ForwordingWireLength = _workParams.SolderPositionParams[i].ForwardFeedLength;
                                                        _mSolderingJob.ForwordingVelocity = _workParams.SolderPositionParams[i].ForwardFeedVelocity;
                                                        _mSolderingJob.ReverseWireLength = _workParams.SolderPositionParams[i].ReverseFeedLength;
                                                        _mSolderingJob.ReverseVelocity = _workParams.SolderPositionParams[i].ReverseFeedVelocity;
                                                        _mSolderingJob.LaserProfileEnable = _workParams._SolderingProfileEnable;
                                                        _mSolderingJob.FeederEnable = _workParams._UseFeederEnable;
                                                        _mSolderingJob.LaserEnable = _workParams._UseLaserEnable;
                                                        _mLaserSoldering.LaserSolderParam = _mSolderingJob;
                                                        _mLaserSoldering.LaserSolderingStart();
                                                        _waitHandle.Reset();
                                                        _waitHandle.WaitOne();
                                                    }
                                                    else
                                                    {
                                                        e.Cancel = true;
                                                        mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), "레이저 에러에 의한 시퀀스 종료...");
                                                    }
                                                }
                                                //*/
                                            }
                                            else
                                            {
                                                _IsAutoSolderingRunning = false;
                                                _IsAutoSolderingEnd = false;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            _IsAutoSolderingRunning = false;
                                            _IsAutoSolderingEnd = false;
                                            break;
                                        }
                                    }
                                } 
                                _backgroundWorkerAutoSoldering.ReportProgress(((i + 1) * 100) / _workParams.SolderPositionParams.Count);
                            }
                        }
                    }
                    else
                    {
                        MotionParams _motParams = new MotionParams();
                        AiCControlLibrary.SerialCommunication.Data.AiCData _mDrvData = new AiCControlLibrary.SerialCommunication.Data.AiCData();
                        _mDrvData = _mMotionControlCommManager.mDrvCtrl;
                        _motParams = _systemParams._motionParams;
                        for (int i = 0; i < _workParams.SolderPositionParams.Count; i++)
                        {
                            if (mRobotInformation.mError != 0)
                            {
                                e.Cancel = true;
                                mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), "모션 에러에 의한 시퀀스 종료...");
                            }
                            if (!_IsAutoSequenceCancleRequest) //if (!e.Cancel)
                            {
                                byte[] data = new byte[100];
                                //while (!Convert.ToBoolean((mRobotInformation.mStatus >> 6) & 0x01)) ;
                                while ((mRobotInformation.mStatus & 0x00000052) != 0x00000052) ;
                                for (int j = 0; j < _mMotionControlCommManager.mDrvCtrl.DeviceIDCount; j++)
                                {
                                    if (j == 0)
                                    {
                                        data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionX * _motParams.MM2PulseRatioX));
                                        _mMotionControlCommManager.SendData(data);
                                        _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionX;
                                    }
                                    else if (j == 1)
                                    {
                                        data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionY * _motParams.MM2PulseRatioY));
                                        _mMotionControlCommManager.SendData(data);
                                        _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionY;
                                    }
                                    else if (j == 2)
                                    {
                                        data = _mDrvData.MoveTargetPositionSendData((byte)_mDrvData.DrvID[j], Convert.ToInt32(_workParams.SolderPositionParams[i].PositionZ * _motParams.MM2PulseRatioZ));
                                        _mMotionControlCommManager.SendData(data);
                                        _RobotTargetPosition[j] = _workParams.SolderPositionParams[i].PositionZ;
                                    }
                                    Thread.Sleep(50);
                                }
                                data = _mDrvData.MoveAbsoluteCommand(129);
                                _mMotionControlCommManager.SendData(data);
                                Thread.Sleep(1000);
                                _IsRequestAutomovingCommand = true;
                                _waitHandle.Reset();
                                _waitHandle.WaitOne();

                                if (!_IsAutoSequenceCancleRequest) //if (!e.Cancel)
                                {
                                    ///*
                                    // Insert Laser soldering Sequence Start                                    
                                    if (_workParams._SolderingProcessEnable && _workParams._UseLaserEnable && _workParams._UseFeederEnable)
                                    {
                                        //while (!Convert.ToBoolean((mRobotInformation.mStatus >> 6) & 0x01)) ;
                                        while ((mRobotInformation.mStatus & 0x00000052) != 0x00000052) ;
                                        if (!_mLaserSoldering.IsAutoSolderError)
                                        {
                                            _mSolderingJob.ReadyTime = _workParams.SolderPositionParams[i].ReadyTime;
                                            _mSolderingJob.PreheatPowerRatio = (int)_workParams.SolderPositionParams[i].PreHeatPowerRatio;
                                            _mSolderingJob.PreHeatTime = _workParams.SolderPositionParams[i].PreHeatTime;
                                            _mSolderingJob.MeltingPowerRatio = (int)_workParams.SolderPositionParams[i].MeltingPowerRatio;
                                            _mSolderingJob.MeltingTime = _workParams.SolderPositionParams[i].MeltingTime;
                                            _mSolderingJob.HeatPowerRatio = (int)_workParams.SolderPositionParams[i].HeatPowerRatio;
                                            _mSolderingJob.HeatTime = _workParams.SolderPositionParams[i].HeatTime;
                                            _mSolderingJob.ForwordingWireLength = _workParams.SolderPositionParams[i].ForwardFeedLength;
                                            _mSolderingJob.ForwordingVelocity = _workParams.SolderPositionParams[i].ForwardFeedVelocity;
                                            _mSolderingJob.ReverseWireLength = _workParams.SolderPositionParams[i].ReverseFeedLength;
                                            _mSolderingJob.ReverseVelocity = _workParams.SolderPositionParams[i].ReverseFeedVelocity;
                                            _mSolderingJob.LaserProfileEnable = _workParams._SolderingProfileEnable;
                                            _mSolderingJob.FeederEnable = _workParams._UseFeederEnable;
                                            _mSolderingJob.LaserEnable = _workParams._UseLaserEnable;
                                            _mLaserSoldering.LaserSolderParam = _mSolderingJob;
                                            _mLaserSoldering.LaserSolderingStart();
                                            _waitHandle.Reset();
                                            _waitHandle.WaitOne();
                                        }
                                        else
                                        {
                                            e.Cancel = true;
                                            mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), "레이저 에러에 의한 시퀀스 종료...");
                                        }
                                    }
                                    //*/
                                }
                                else
                                {
                                    _IsAutoSolderingRunning = false;
                                    _IsAutoSolderingEnd = false;
                                    break;
                                }
                            }
                            else
                            {
                                _IsAutoSolderingRunning = false;
                                _IsAutoSolderingEnd = false;
                                break;
                            }
                            _backgroundWorkerAutoSoldering.ReportProgress(((i + 1) * 100) / _workParams.SolderPositionParams.Count);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                mLog.WriteLog(LogLevel.Fatal, LogClass.atLaser.ToString(), string.Format("{0}\r\n{1}", ex.Message, ex.StackTrace));
            }
        }
        private void backgroundWorkerAutoSoldering_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                _IsAutoSolderingRunning = false;
                _IsRequestAutomovingCommand = false;
                _IsAutoSolderingEnd = true;
                _IsAutoSequenceCancleRequest = false;
                _IsAutoSequencePyrospotDataSaveEnable = false;
                //_isInspecting = false;
                //_InspectionWorking = false;
                //UpdateProcessTime(false);
                CheckTackTime.Stop();
                barCheckItemLaserSolderingStart.Caption = string.Format("작업 시작");
                barStaticItemAutoSolderingStatus.Caption = string.Format("진행: 작업 완료");
                //if ((!_isInspectError) && (!_isInspectCancel))
                //{
                //    InpsectResultUpdate();
                //    UpdateRadarChartResult();
                //    CreateResultFile(mResultData.bTotalResult);
                //    //UpdateChartAngle();
                //    //UpdateChartDistance();
                //    UpdateStaticsData();
                //    System.Console.WriteLine("bacground work Photo Inspection run worker completed");
                //}
                //_isInspectCancel = false;
                //barEditItemInspectionProgress.EditValue = 100;
                AutoStartButtonRelease();
                //barCheckItemInspectionStart.Checked = false;
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "포토 센서 자동 검사 완료작업을 하지 못햇습니다.");
            }
        }
        private void backgroundWorkerAutoSoldering_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                TimeSpan ts = CheckTackTime.Elapsed;
                barStaticItemAutoSolderingStatus.Caption = "진행: 납땜 작업중";
                barStaticAutoSolderingTime.Caption = string.Format("작업 시간: {0:00:}{1:00}.{2:000} sec",ts.TotalMinutes,ts.TotalSeconds,ts.TotalMilliseconds);
                barEditItemAutoSolderingProgress.EditValue = e.ProgressPercentage;
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "작업 상태 갱신 실패.");
            }
        }
        private void backgroundWorkerMotionHome_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                RobotInformation info = e.Argument as RobotInformation;
                int _homestep = 0;
                bool _motionflag = false;
                if (sender is BackgroundWorker worker)
                {
                    _IsHommingCancle = false;
                    if (_mMotionControlCommManager.IsOpen())
                    {
                        if (!_IsHommingFinished)
                        {
                            byte[] SeData = new byte[8];
                            if (MessageBox.Show("원점복귀를 진행을 합니다.", "원점복귀", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.Yes)
                            {
                                _HommingProcess = true;
                                _IsHommingCancle = false;                                
                            }
                            else
                                _IsHommingCancle = true;

                            while (_HommingProcess)
                            {
                                switch (_homestep)
                                {
                                    case 0:
                                        // Z축 -Limit 방향으로 이동.
                                        if ((mRobotInformation.DrvStatus & 0x00000040) == 0x00000040)
                                        {
                                            _homestep = 1;
                                            _motionflag = false;
                                        }
                                        else
                                        {
                                            if (!_motionflag)
                                            {
                                                SeData = _mMotionControlCommManager.mDrvCtrl.CCWJogCommand((byte)3);             // Z Axis -Limit Direction Command
                                                _mMotionControlCommManager.SendData(SeData);
                                                _motionflag = true;
                                            }
                                        }
                                        break;
                                    case 1:
                                        if ((mRobotInformation.DrvStatus & 0x00000080) == 0x00000080)
                                        {
                                            _homestep = 2;
                                            _motionflag = false;
                                        }
                                        else
                                        {
                                            if (!_motionflag)
                                            {
                                                SeData = _mMotionControlCommManager.mDrvCtrl.HomeStartCommand((byte)3);             // Z Axis Homing Command
                                                _mMotionControlCommManager.SendData(SeData);
                                                _motionflag = true;
                                            }
                                        }
                                        break;
                                    case 2:
                                        //if ((mRobotInformation.mStatus & 0x00000052) == 0x00000052)
                                        {
                                            for (int i = 0; i < _mMotionControlCommManager.mDrvCtrl.DeviceIDCount; i++)
                                            {
                                                if (i != 2)
                                                {
                                                    SeData = _mMotionControlCommManager.mDrvCtrl.HomeStartCommand((byte)_mMotionControlCommManager.mDrvCtrl.DrvID[i]);      // X,Y Axis Homing Command
                                                    _mMotionControlCommManager.SendData(SeData);
                                                }
                                            }
                                            _homestep = 3;
                                        }
                                        break;
                                    case 3:
                                        if ((mRobotInformation.mStatus & 0x00000052) == 0x00000052)
                                        {
                                            _HommingProcess = false;
                                            _IsHommingFinished = true;
                                            mRobotInformation.SetStatus(RobotInformation.RobotStatus.OperationReady, _IsHommingFinished);                                            
                                            _homestep = 0;
                                            mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), "로봇 원점 복귀 진행을 끝났습니다.");
                                        }
                                        break;
                                    default:
                                        _HommingProcess = false;
                                        _IsHommingCancle = true;
                                        mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), "실린더 센서 점검 및 모션 원점 복귀 진행을 하지 못하였습니다.");
                                        break;
                                }
                                Thread.Sleep(500);
                            }
                        }
                    }
                    else
                    {
                        _IsHommingFinished = false;
                        _IsHommingCancle = true;
                    }
                }
                //RobotInformation info = e.Argument as RobotInformation;
                //int _homestep = 0;
                //bool _motionflag = false;
                //if (sender is BackgroundWorker worker)
                //{
                //    _IsHommingCancle = false;
                //    if (_mMotionControlCommManager.IsOpen())
                //    {
                //        if (!_IsHommingFinished)
                //        {
                //            byte[] SeData = new byte[8];
                //            if (MessageBox.Show("원점복귀를 진행을 합니다.", "원점복귀", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.Yes)
                //            {
                //                //SeData = _mRemteIOCommManager.mRemoteIOCtrl.Output1byteCommand(_mRemteIOCommManager.mRemoteIOCtrl.DrvID[0], ARMLibrary.SerialCommunication.Data.ARMData.OUTPUT_CONTROL_MAP.Output3, (ushort)0x0000);
                //                _mRemteIOCommManager.SendData(SeData);
                //                _HommingProcess = true;
                //                _IsHommingCancle = false;
                //                //mRobotInformation.SetStatus(RobotInformation.RobotStatus.OperationReady, _IsHommingFinished);
                //                mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), "실린더3 초기화");
                //            }
                //            else
                //                _IsHommingCancle = true;

                    //            while (_HommingProcess)
                    //            {
                    //                switch (_homestep)
                    //                {
                    //                    case 0:
                    //                        if (mRobotInformation.mInputData.B12)
                    //                        {
                    //                            SeData = _mRemteIOCommManager.mRemoteIOCtrl.Output1byteCommand(_mRemteIOCommManager.mRemoteIOCtrl.DrvID[0], ARMLibrary.SerialCommunication.Data.ARMData.OUTPUT_CONTROL_MAP.Output1, (ushort)0x0000);
                    //                            _mRemteIOCommManager.SendData(SeData);
                    //                            SeData = _mRemteIOCommManager.mRemoteIOCtrl.Output1byteCommand(_mRemteIOCommManager.mRemoteIOCtrl.DrvID[0], ARMLibrary.SerialCommunication.Data.ARMData.OUTPUT_CONTROL_MAP.Output5, (ushort)0x0000);
                    //                            _mRemteIOCommManager.SendData(SeData);
                    //                            mLog.WriteLog(LogLevel.Info, LogClass.atPhoto.ToString(), "실린더3 센서 점검완료 및 실린더1,5 초기화");
                    //                            _homestep = 1;
                    //                        }
                    //                        break;
                    //                    case 1:
                    //                        if (mRobotInformation.mInputData.B8)
                    //                        {
                    //                            SeData = _mRemteIOCommManager.mRemoteIOCtrl.Output1byteCommand(_mRemteIOCommManager.mRemoteIOCtrl.DrvID[0], ARMLibrary.SerialCommunication.Data.ARMData.OUTPUT_CONTROL_MAP.Output2, (ushort)0x0000);
                    //                            _mRemteIOCommManager.SendData(SeData);
                    //                            _homestep = 2;
                    //                            mLog.WriteLog(LogLevel.Info, LogClass.atPhoto.ToString(), "실린더1,5 센서 점검완료 및 실린더2 초기화");
                    //                        }
                    //                        break;
                    //                    case 2:
                    //                        if (mRobotInformation.mInputData.B10)
                    //                        {
                    //                            SeData = _mRemteIOCommManager.mRemoteIOCtrl.Output1byteCommand(_mRemteIOCommManager.mRemoteIOCtrl.DrvID[0], ARMLibrary.SerialCommunication.Data.ARMData.OUTPUT_CONTROL_MAP.Output4, (ushort)0x0000);
                    //                            _mRemteIOCommManager.SendData(SeData);
                    //                            _homestep = 3;
                    //                            mLog.WriteLog(LogLevel.Info, LogClass.atPhoto.ToString(), "실린더2 센서 점검완료 및 실린더4 초기화");
                    //                        }
                    //                        break;
                    //                    case 3:
                    //                        if (mRobotInformation.mInputData.B14)
                    //                        {
                    //                            for (int i = 0; i < _mMotionControlCommManager.mDrvCtrl.DeviceIDCount; i++)
                    //                            {
                    //                                SeData = _mMotionControlCommManager.mDrvCtrl.HomeStartCommand((byte)_mMotionControlCommManager.mDrvCtrl.DrvID[i]);
                    //                                _mMotionControlCommManager.SendData(SeData);
                    //                            }
                    //                            _homestep = 4;
                    //                            mLog.WriteLog(LogLevel.Info, LogClass.atPhoto.ToString(), "실린더4 센서 점검완료 및 모션 원점 복귀 시작");
                    //                        }
                    //                        break;
                    //                    case 4:
                    //                        if (!_motionflag)
                    //                        {
                    //                            _motionflag = true;
                    //                        }
                    //                        else
                    //                        {
                    //                            _homestep = 5;
                    //                        }
                    //                        break;
                    //                    case 5:
                    //                        if ((mRobotInformation.mStatus & 0x00000052) == 0x00000052)
                    //                        {
                    //                            _HommingProcess = false;
                    //                            _IsHommingFinished = true;
                    //                            mRobotInformation.SetStatus(RobotInformation.RobotStatus.OperationReady, _IsHommingFinished);
                    //                        }
                    //                        break;
                    //                    default:
                    //                        _HommingProcess = false;
                    //                        _IsHommingCancle = true;
                    //                        mLog.WriteLog(LogLevel.Info, LogClass.atPhoto.ToString(), "실린더 센서 점검 및 모션 원점 복귀 진행을 하지 못하였습니다.");
                    //                        break;
                    //                }
                    //                Thread.Sleep(500);
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        _IsHommingFinished = false;
                    //        _IsHommingCancle = true;
                    //    }
                    //}
            }
            catch (Exception ex)
            {
                mLog.WriteLog(LogLevel.Warn, LogClass.atLaser.ToString(), string.Format("{0}\r\n{1}", ex.Message, ex.StackTrace));
            }
        }
        private void backgroundWorkerMotionHome_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                AutoStartButtonRelease();
                if (_IsHommingCancle)
                {
                    if (_mMotionControlCommManager.IsOpen())
                    {
                        byte[] SeData = new byte[8];
                        for (int i = 0; i < _mMotionControlCommManager.mDrvCtrl.DeviceIDCount; i++)
                        {
                            SeData = _mMotionControlCommManager.mDrvCtrl.MoveStopCommand((byte)_mMotionControlCommManager.mDrvCtrl.DrvID[i]);
                            _mMotionControlCommManager.SendData(SeData);
                        }
                    }
                    mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), "원점 복귀 진행 취소");
                    _IsHommingCancle = false;
                }
                else
                    mLog.WriteLog(LogLevel.Info, LogClass.atLaser.ToString(), "원점 복귀 진행 완료");
            }
            catch (Exception)
            {
                mLog.WriteLog(LogLevel.Error, LogClass.atLaser.ToString(), "원점 복귀 시퀀스를 완료하지 못햇습니다.");
            }
        }        
    }
}
