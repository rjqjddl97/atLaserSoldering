using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using RecipeManager;
using LogLibrary;

namespace atLaserSoldering
{
    public partial class SystemEditor : DevExpress.XtraEditors.XtraForm
    {
        SystemParams _systemParameters = new SystemParams();
        public Log _log = new Log();
        public SystemEditor()
        {
            InitializeComponent();
        }

        private void SystemEditor_Load(object sender, EventArgs e)
        {
            simpleButtonSystemFileSave.Enabled = false;

            InitializeSystemParameters();
            LoadSystemParameters();
            _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("System 설정 로드 완료"));
        }
        public SystemEditor(bool _bKoreaLanguage)
        {
            InitializeComponent();
            if (!_bKoreaLanguage)
            {
                categoryCameraParameters.Properties.Caption = "Camera Parameter";                
                rowCameraFriendlyName.Properties.Caption = "Camera Name";
                rowCameraHResolution.Properties.Caption = "Horizone Pixel(H)";
                rowCameraVResolution.Properties.Caption = "Vertical Pixel(V)";
                rowCameraOnePixelResolution.Properties.Caption = "PixelResolution(mm)";

                categoryCalibraitionParameters.Properties.Caption = "Calibration Parameter";
                rowCoordinateSwitch.Properties.Caption = "Use X,Y symmetry";
                rowImageToSystemX.Properties.Caption = "Image X Coordinate -> Sytem Y Coordiante Polarity";
                rowImageToSystemY.Properties.Caption = "Image Y Coordinate -> Sytem Y Coordiante Polarity";
                rowCoordinateCalibrationActive.Properties.Caption = "Use Coordinate Calibration";
                rowReference_X.Properties.Caption = "X Coordinate Reference";
                rowReference_Y.Properties.Caption = "Y Coordinate Reference";
                rowReference_Z.Properties.Caption = "Z Coordinate Reference";

                categoryMotionParameters.Properties.Caption = " Motion Parameter";
                rowMotionResolutionX.Properties.Caption = "X Axis Resolution[pulse]";
                rowMotionResolutionY.Properties.Caption = "Y Axis Resolution[pulse]";
                rowMotionResolutionZ.Properties.Caption = "Z Axis Resolution[pulse]";                
                rowMotionGearRatioX.Properties.Caption = "X Axis Gear Ratio[1:N]";
                rowMotionGearRatioY.Properties.Caption = "Y Axis Gear Ratio[1:N]";
                rowMotionGearRatioZ.Properties.Caption = "Z Axis Gear Ratio[1:N]";                
                rowMotionBallLeadX.Properties.Caption = "X Axis BallLead[mm]";
                rowMotionBallLeadY.Properties.Caption = "Y Axis BallLead[mm]";
                rowMotionBallLeadZ.Properties.Caption = "Z Axis BallLead[˚]";                
                rowMotionMoveVelocity.Properties.Caption = "Moving Velocity[mm/s]";
                rowMotionMoveAcceleration.Properties.Caption = "Acceleratoin Time[ms]";
                rowMotionMenaulVelocity.Properties.Caption = "Menual Moving Velocity[mm/s]";

                categoryAiCCommunicationParameters.Properties.Caption = "AiC Communication Parameter";
                rowAiCCommunicationPortName.Properties.Caption = "ComPort";
                rowAiCCommunicationBaudRate.Properties.Caption = "Baudrate[bps]";
                rowAiCCommunicationDatabit.Properties.Caption = "Data Bit";
                rowAiCCommunicationParity.Properties.Caption = "Parity";
                rowAiCCommunicationStopbit.Properties.Caption = "Stop Bit";
                rowAiCCommunicationHandshake.Properties.Caption = "Flow Control";
                rowAiCCommunicationCounter.Properties.Caption = "Connection Counter";
                categoryAiCCommunicationIDs.Properties.Caption = "AiC ID Setup";

                categoryRemoteIOCommunicationParameters.Properties.Caption = "Remote IO Communication Parameter";
                rowRemoteIOCommunicationPortName.Properties.Caption = "ComPort";
                rowRemoteIOCommunicationBaudRate.Properties.Caption = "Baudrate[bps]";
                rowRemoteIOCommunicationDatabit.Properties.Caption = "Data Bit";
                rowRemoteIOCommunicationParity.Properties.Caption = "Parity";
                rowRemoteIOCommunicationStopbit.Properties.Caption = "Stop Bit";
                rowRemoteIOCommunicationHandshake.Properties.Caption = "Flow Control";
                rowRemoteIOCommunicationCount.Properties.Caption = "Connection Counter";
                categoryRemoteIOCommunicationIDs.Properties.Caption = "Remote IO ID Setup";
                rowRemoteIOInputProperties1.Caption = "I/O Module1";
                rowRemoteIOOutputProperties1.Caption = "I/O Module2";

                categorySystemADMSInformation.Properties.Caption = "ADMS Information";
                rowSystemADMSUse.Properties.Caption = "Use DB";
                rowSystemJobWorkUse.Properties.Caption = "Use JobOrder DB";
                rowSystemADMSIPAddress.Properties.Caption = "IP Address";
                rowSystemADMSPort.Properties.Caption = "Port";
                rowSystemADMSUserID.Properties.Caption = "User ID";
                rowSystemADMSPassWD.Properties.Caption = "Password";
                rowSystemADMSEquipmentID.Properties.Caption = "Equipment ID";
                rowSystemADMSSchemaName.Properties.Caption = "Schema DB name";
                rowSystemADMSEquipmentDBName.Properties.Caption = "Equipment DB name";
                rowSystemADMSProductDBName.Properties.Caption = "Product DB name";

                categorySaveResult.Properties.Caption = "Inspection Result Parameter";
                rowSaveResultImageProcess.Properties.Caption = "Save Image File";
                rowSaveResultStatistics.Properties.Caption = "Save Chart Data";

                categorySystemLanguage.Properties.Caption = "System Language";
                rowSystemUseLanguage.Properties.Caption = "Use Korea Language";

                categoryLightParameter.Properties.Caption = "Light Communication Parameter";
                rowLightCommunicationPortName.Properties.Caption = "ComPort";
                rowLightCommunicationBaudRate.Properties.Caption = "Baudrate[bps]";
                rowLightCommunicationDataBit.Properties.Caption = "Data Bit";
                rowLightCommunicationParity.Properties.Caption = "Parity";
                rowLightCommunicationStopBit.Properties.Caption = "Stop Bit";
                rowLightCommunicationFlowControl.Properties.Caption = "Flow Control";

                categoryFeederParameter.Properties.Caption = "Wire Solder Communication Parameter";
                rowFeederGearRatio.Properties.Caption = "Gear Ratio[1:N]";
                rowFeederDiameter.Properties.Caption = "Poly Diameter[mm]";
                rowFeederResolution.Properties.Caption = "Motor Resolution[pulse]";
                rowFeederVelocity.Properties.Caption = "Feeder Velocity[mm/s]";
                rowFeederCommunicationPortName.Properties.Caption = "ComPort";
                rowFeederCommunicationBaudRate.Properties.Caption = "BaudRate[bps]";
                rowFeederCommunicationDataBit.Properties.Caption = "Data Bit";
                rowFeederCommunicationParity.Properties.Caption = "Parity";
                rowFeederCommunicationStopBit.Properties.Caption = "Stop Bit";
                rowFeederCommunicationFlowControl.Properties.Caption = "Flow Control";
                rowFeederCommunicationIDNumber.Properties.Caption = "Slave ID";

                categoryLaserParameter.Properties.Caption = "Laser Communication Parameter";
                rowLaserCommunicationPortName.Properties.Caption = "ComPort";
                rowLaserCommunicationBaudRate.Properties.Caption = "BaudRate[bps]";
                rowLaserCommunicationDataBit.Properties.Caption = "Data Bit";
                rowLaserCommunicationParity.Properties.Caption = "Parity";
                rowLaserCommunicationStopBit.Properties.Caption = "Stop Bit";
                rowLaserCommunicationFlowControl.Properties.Caption = "Flow Control";
            }
            else
            {
                categoryCameraParameters.Properties.Caption = "카메라 파라미터";                
                rowCameraFriendlyName.Properties.Caption = "카메라 이름";
                rowCameraHResolution.Properties.Caption = "가로 해상도(H)";
                rowCameraVResolution.Properties.Caption = "세로 해상도(V)";
                rowCameraOnePixelResolution.Properties.Caption = "한 픽셀 해상도(mm)";                

                categoryCalibraitionParameters.Properties.Caption = "보정 파라미터";
                rowCoordinateSwitch.Properties.Caption = "X,Y좌표계 대칭변환유무";
                rowImageToSystemX.Properties.Caption = "이미지 X 좌표계 -> 시스템 X 좌표계";
                rowImageToSystemY.Properties.Caption = "이미지 Y 좌표계 -> 시스템 Y 좌표계";
                rowCoordinateCalibrationActive.Properties.Caption = "좌표계 보정 활성화유무";
                rowReference_X.Properties.Caption = "X축 기준값";
                rowReference_Y.Properties.Caption = "Y축 기준값";
                rowReference_Z.Properties.Caption = "Z축 기준값";

                categoryMotionParameters.Properties.Caption = " 모션 파라미터";
                rowMotionResolutionX.Properties.Caption = "X축 1회전 분해능[pulse]";
                rowMotionResolutionY.Properties.Caption = "Y축 1회전 분해능[pulse]";
                rowMotionResolutionZ.Properties.Caption = "Z축 1회전 분해능[pulse]";                
                rowMotionGearRatioX.Properties.Caption = "X축 기어비[1:N]";
                rowMotionGearRatioY.Properties.Caption = "Y축 기어비[1:N]";
                rowMotionGearRatioZ.Properties.Caption = "Z축 기어비[1:N]";                
                rowMotionBallLeadX.Properties.Caption = "X축 볼리드[mm]";
                rowMotionBallLeadY.Properties.Caption = "Y출 볼리드[mm]";
                rowMotionBallLeadZ.Properties.Caption = "Z축 볼리드[mm]";                
                rowMotionMoveVelocity.Properties.Caption = "이동 속도[mm/s]";
                rowMotionMoveAcceleration.Properties.Caption = "이동 가감속시간[ms]";
                rowMotionMenaulVelocity.Properties.Caption = "수동 이동 속도[mm/s]";

                categoryAiCCommunicationParameters.Properties.Caption = "AiC 모션 통신 설정";
                rowAiCCommunicationPortName.Properties.Caption = "통신 포트";
                rowAiCCommunicationBaudRate.Properties.Caption = "통신속도[bps]";
                rowAiCCommunicationDatabit.Properties.Caption = "데이터 비트";
                rowAiCCommunicationParity.Properties.Caption = "패리티";
                rowAiCCommunicationStopbit.Properties.Caption = "정지 비트";
                rowAiCCommunicationHandshake.Properties.Caption = "흐름 제어";
                rowAiCCommunicationCounter.Properties.Caption = "연결 대수";
                categoryAiCCommunicationIDs.Properties.Caption = "AiC 모션 ID 설정";

                categoryRemoteIOCommunicationParameters.Properties.Caption = "Remote IO 통신 설정";
                rowRemoteIOCommunicationPortName.Properties.Caption = "통신 포트";
                rowRemoteIOCommunicationBaudRate.Properties.Caption = "통신속도[bps]";
                rowRemoteIOCommunicationDatabit.Properties.Caption = "데이터 비트";
                rowRemoteIOCommunicationParity.Properties.Caption = "패리티";
                rowRemoteIOCommunicationStopbit.Properties.Caption = "정지 비트";
                rowRemoteIOCommunicationHandshake.Properties.Caption = "흐름 제어";
                rowRemoteIOCommunicationCount.Properties.Caption = "연결 대수";
                categoryRemoteIOCommunicationIDs.Properties.Caption = "Remote IO ID설정";
                rowRemoteIOInputProperties1.Caption = "입출력 모듈1";
                rowRemoteIOOutputProperties1.Caption = "입출력 모듈2";

                categorySystemADMSInformation.Properties.Caption = "ADMS 정보";
                rowSystemADMSUse.Properties.Caption = "DB 사용";
                rowSystemJobWorkUse.Properties.Caption = "작업지시서 DB연계사용";
                rowSystemADMSIPAddress.Properties.Caption = "IP 주소";
                rowSystemADMSPort.Properties.Caption = "포트";
                rowSystemADMSUserID.Properties.Caption = "사용자 ID";
                rowSystemADMSPassWD.Properties.Caption = "비밀번호";
                rowSystemADMSEquipmentID.Properties.Caption = "Equipment ID";
                rowSystemADMSSchemaName.Properties.Caption = "Schema DB 이름";
                rowSystemADMSEquipmentDBName.Properties.Caption = "Equipment DB 이름";
                rowSystemADMSProductDBName.Properties.Caption = "Product DB 이름";

                categorySaveResult.Properties.Caption = "결과 저장 옵션";
                rowSaveResultImageProcess.Properties.Caption = "비젼 결과 이미지 저장";
                rowSaveResultStatistics.Properties.Caption = "통계 데이터 저장";

                categorySystemLanguage.Properties.Caption = "시스템 언어 설정";
                rowSystemUseLanguage.Properties.Caption = "한국어 사용";

                categoryLightParameter.Properties.Caption = "조명 통신 설정";
                rowLightCommunicationPortName.Properties.Caption = "통신 포트";
                rowLightCommunicationBaudRate.Properties.Caption = "통신속도[bps]";
                rowLightCommunicationDataBit.Properties.Caption = "데이터 비트";
                rowLightCommunicationParity.Properties.Caption = "패리티";
                rowLightCommunicationStopBit.Properties.Caption = "정지 비트";
                rowLightCommunicationFlowControl.Properties.Caption = "흐름 제어";                

                categoryFeederParameter.Properties.Caption = "실납 공급기 통신 설정";
                rowFeederGearRatio.Properties.Caption = "공급기 기어비[1:N]";
                rowFeederDiameter.Properties.Caption = "공급기 풀리지름[mm]";
                rowFeederResolution.Properties.Caption = "공급기모터 분해능[pulse]";
                rowFeederVelocity.Properties.Caption = "공급기 속도[mm/s]";
                rowFeederCommunicationPortName.Properties.Caption = "통신 포트";
                rowFeederCommunicationBaudRate.Properties.Caption = "통신속도[bps]";
                rowFeederCommunicationDataBit.Properties.Caption = "데이터 비트";
                rowFeederCommunicationParity.Properties.Caption = "패리티";
                rowFeederCommunicationStopBit.Properties.Caption = "정지 비트";
                rowFeederCommunicationFlowControl.Properties.Caption = "흐름 제어";
                rowFeederCommunicationIDNumber.Properties.Caption = "국번ID";

                categoryLaserParameter.Properties.Caption = "조명 통신 설정";
                rowLaserCommunicationPortName.Properties.Caption = "통신 포트";
                rowLaserCommunicationBaudRate.Properties.Caption = "통신속도[bps]";
                rowLaserCommunicationDataBit.Properties.Caption = "데이터 비트";
                rowLaserCommunicationParity.Properties.Caption = "패리티";
                rowLaserCommunicationStopBit.Properties.Caption = "정지 비트";
                rowLaserCommunicationFlowControl.Properties.Caption = "흐름 제어";

            }
        }

        private void vGridControlSystemParameters_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            SetCellValue(e.Row);
        }
        private void InitializeSystemParameters()
        {
            for (int i = 0; i < RecipeFileIO.SerialPortName.Length; ++i)
            {
                repositoryItemComboBoxAiCCommunicationPortName.Items.Add(RecipeFileIO.SerialPortName[i]);
                repositoryItemComboBoxRemoteIOCommunicationPortName.Items.Add(RecipeFileIO.SerialPortName[i]);                
                repositoryItemComboBoxLaserCommunicationPortName.Items.Add(RecipeFileIO.SerialPortName[i]);
                repositoryItemComboBoxFeederCommunicationPortName.Items.Add(RecipeFileIO.SerialPortName[i]);
                repositoryItemComboBoxLightCommunicationPortName.Items.Add(RecipeFileIO.SerialPortName[i]);
            }

            for (int i = 0; i < RecipeFileIO.SerialBaudRates.Length; ++i)
            {
                repositoryItemComboBoxAiCCommunicationBaudRate.Items.Add(RecipeFileIO.SerialBaudRates[i]);
                repositoryItemComboBoxRemoteIOCommunicationBaudRate.Items.Add(RecipeFileIO.SerialBaudRates[i]);
                repositoryItemComboBoxLaserCommunicationBaudRate.Items.Add(RecipeFileIO.SerialBaudRates[i]);
                repositoryItemComboBoxFeederCommunicationBaudRate.Items.Add(RecipeFileIO.SerialBaudRates[i]);
                repositoryItemComboBoxLightCommunicationBaudRate.Items.Add(RecipeFileIO.SerialBaudRates[i]);
            }

            for (int i = 0; i < RecipeFileIO.SerialDataBits.Length; ++i)
            {
                repositoryItemComboBoxAiCCommunicationDatabit.Items.Add(RecipeFileIO.SerialDataBits[i]);
                repositoryItemComboBoxRemoteIOCommunicationDatabit.Items.Add(RecipeFileIO.SerialDataBits[i]);
                repositoryItemComboBoxLaserCommunicationDataBit.Items.Add(RecipeFileIO.SerialDataBits[i]);
                repositoryItemComboBoxFeederCommunicationDataBit.Items.Add(RecipeFileIO.SerialDataBits[i]);
                repositoryItemComboBoxLightCommunicationDataBit.Items.Add(RecipeFileIO.SerialDataBits[i]);
            }

            foreach (string strParity in Enum.GetNames(typeof(Parity)))
            {
                repositoryItemComboBoxAiCCommunicationParity.Items.Add(strParity);
                repositoryItemComboBoxRemoteIOCommunicationParity.Items.Add(strParity);
                repositoryItemComboBoxLaserCommunicationParity.Items.Add(strParity);
                repositoryItemComboBoxFeederCommunicationParity.Items.Add(strParity);
                repositoryItemComboBoxLightCommunicationParity.Items.Add(strParity);
            }

            foreach (string strStopbits in Enum.GetNames(typeof(StopBits)))
            {
                repositoryItemComboBoxAiCCommunicationStopbit.Items.Add(strStopbits);
                repositoryItemComboBoxRemoteIOCommunicationStopbit.Items.Add(strStopbits);
                repositoryItemComboBoxLaserCommunicationStopBit.Items.Add(strStopbits);
                repositoryItemComboBoxFeederCommunicationStopBit.Items.Add(strStopbits);
                repositoryItemComboBoxLightCommunicationStopBit.Items.Add(strStopbits);
            }

            foreach (string strHandshake in Enum.GetNames(typeof(Handshake)))
            {
                repositoryItemComboBoxAiCCommunicationHandshake.Items.Add(strHandshake);
                repositoryItemComboBoxRemoteIOCommunicationHandshake.Items.Add(strHandshake);
                repositoryItemComboBoxLaserCommunicationFlowControl.Items.Add(strHandshake);
                repositoryItemComboBoxFeederCommunicationFlowControl.Items.Add(strHandshake);
                repositoryItemComboBoxLightCommunicationFlowControl.Items.Add(strHandshake);
            }
            for (int i = 0; i < RecipeFileIO.TransitionCoordinate.Length; i++)
            {
                repositoryItemComboBoxCalibrationImageX.Items.Add(RecipeFileIO.TransitionCoordinate[i]);
                repositoryItemComboBoxCalibrationImageY.Items.Add(RecipeFileIO.TransitionCoordinate[i]);
            }
            // System Parameter의 Light Serial 초기화
            rowAiCCommunicationPortName.Properties.Value = repositoryItemComboBoxAiCCommunicationPortName.Items[0].ToString();
            rowAiCCommunicationBaudRate.Properties.Value = repositoryItemComboBoxAiCCommunicationBaudRate.Items[4].ToString();
            rowAiCCommunicationDatabit.Properties.Value = repositoryItemComboBoxAiCCommunicationDatabit.Items[4].ToString();
            rowAiCCommunicationParity.Properties.Value = repositoryItemComboBoxAiCCommunicationParity.Items[0].ToString();
            rowAiCCommunicationStopbit.Properties.Value = repositoryItemComboBoxAiCCommunicationStopbit.Items[0].ToString();
            rowAiCCommunicationHandshake.Properties.Value = repositoryItemComboBoxAiCCommunicationHandshake.Items[0].ToString();

            rowRemoteIOCommunicationPortName.Properties.Value = repositoryItemComboBoxRemoteIOCommunicationPortName.Items[1].ToString();
            rowRemoteIOCommunicationBaudRate.Properties.Value = repositoryItemComboBoxRemoteIOCommunicationBaudRate.Items[4].ToString();
            rowRemoteIOCommunicationDatabit.Properties.Value = repositoryItemComboBoxRemoteIOCommunicationDatabit.Items[4].ToString();
            rowRemoteIOCommunicationParity.Properties.Value = repositoryItemComboBoxRemoteIOCommunicationParity.Items[0].ToString();
            rowRemoteIOCommunicationStopbit.Properties.Value = repositoryItemComboBoxRemoteIOCommunicationStopbit.Items[0].ToString();
            rowRemoteIOCommunicationHandshake.Properties.Value = repositoryItemComboBoxRemoteIOCommunicationHandshake.Items[0].ToString();

            // System Parameter의 Camera 파라미터 초기화
            _systemParameters._cameraParams.CheckUsingCamera = true;
            _systemParameters._cameraParams.HResolution = Convert.ToInt32(rowCameraHResolution.Properties.Value);
            _systemParameters._cameraParams.VResolution = Convert.ToInt32(rowCameraVResolution.Properties.Value);
            _systemParameters._cameraParams.OnePixelResolution = Convert.ToSingle(rowCameraOnePixelResolution.Properties.Value);                        

            // Calibration Coordinate 파라미터 초기화
            _systemParameters._calibrationParams._CoordinateSwitchEnable = Convert.ToBoolean(rowCoordinateSwitch.Properties.Value);
            rowImageToSystemX.Properties.Value = repositoryItemComboBoxCalibrationImageX.Items[0].ToString();
            rowImageToSystemY.Properties.Value = repositoryItemComboBoxCalibrationImageY.Items[0].ToString();
            _systemParameters._calibrationParams._imagetoSystemXcoordi = Convert.ToSingle(rowImageToSystemX.Properties.Value);
            _systemParameters._calibrationParams._imagetoSystemYcoordi = Convert.ToSingle(rowImageToSystemY.Properties.Value);
            _systemParameters._calibrationParams._CoordinateCalibrationActive = Convert.ToBoolean(rowCoordinateCalibrationActive.Properties.Value);
            _systemParameters._calibrationParams._X_reference_Distance = Convert.ToSingle(rowReference_X.Properties.Value);            
            _systemParameters._calibrationParams._Y_reference_Distance = Convert.ToSingle(rowReference_Y.Properties.Value);
            _systemParameters._calibrationParams._Z_reference_Distance = Convert.ToSingle(rowReference_Z.Properties.Value);            
            // System Parameter의 Motion 파라미터 초기화
            _systemParameters._motionParams.MenualMoveVelocity = Convert.ToSingle(rowMotionMenaulVelocity.Properties.Value);
            _systemParameters._motionParams.MoveVelocity = Convert.ToSingle(rowMotionMoveVelocity.Properties.Value);
            _systemParameters._motionParams.MoveAcceleration = Convert.ToSingle(rowMotionMoveAcceleration.Properties.Value);
            _systemParameters._motionParams.OneTurnResolutionX = Convert.ToInt32(rowMotionResolutionX.Properties.Value);
            _systemParameters._motionParams.OneTurnResolutionY = Convert.ToInt32(rowMotionResolutionY.Properties.Value);
            _systemParameters._motionParams.OneTurnResolutionZ = Convert.ToInt32(rowMotionResolutionZ.Properties.Value);            
            _systemParameters._motionParams.GearRatioX = Convert.ToSingle(rowMotionGearRatioX.Properties.Value);
            _systemParameters._motionParams.GearRatioY = Convert.ToSingle(rowMotionGearRatioY.Properties.Value);
            _systemParameters._motionParams.GearRatioZ = Convert.ToSingle(rowMotionGearRatioZ.Properties.Value);            
            _systemParameters._motionParams.BallLeadX = Convert.ToSingle(rowMotionBallLeadX.Properties.Value);
            _systemParameters._motionParams.BallLeadY = Convert.ToSingle(rowMotionBallLeadY.Properties.Value);
            _systemParameters._motionParams.BallLeadZ = Convert.ToSingle(rowMotionBallLeadZ.Properties.Value);
            

            // System Parameter의 AiC 파라미터 초기화
            _systemParameters._AiCParams.ConnectedNumber = Convert.ToInt32(rowAiCCommunicationCounter.Properties.Value);
            _systemParameters._AiCParams.SerialParameters.PortName = Convert.ToString(rowAiCCommunicationPortName.Properties.Value);
            _systemParameters._AiCParams.SerialParameters.BaudRates = Convert.ToInt32(rowAiCCommunicationBaudRate.Properties.Value);
            _systemParameters._AiCParams.SerialParameters.Parity = (Parity)Enum.Parse(typeof(Parity), Convert.ToString(rowAiCCommunicationParity.Properties.Value));
            _systemParameters._AiCParams.SerialParameters.StopBits = (StopBits)Enum.Parse(typeof(StopBits), Convert.ToString(rowAiCCommunicationStopbit.Properties.Value));
            _systemParameters._AiCParams.SerialParameters.DataBits = Convert.ToInt32(rowAiCCommunicationDatabit.Properties.Value);
            _systemParameters._AiCParams.SerialParameters.Handshake = (Handshake)Enum.Parse(typeof(Handshake), Convert.ToString(rowAiCCommunicationHandshake.Properties.Value));
            _systemParameters._AiCParams.ConnectedNumber = Convert.ToInt32(rowAiCCommunicationCounter.Properties.Value);
            AiCParams._IDs items = new AiCParams._IDs();
            items._devicename = Convert.ToString(rowAiC1Properties1.Value);
            items._idNumber = Convert.ToInt32(rowAiC1Properties2.Value);
            _systemParameters._AiCParams.IDs.Add(items);
            items._devicename = Convert.ToString(rowAiC2Properties1.Value);
            items._idNumber = Convert.ToInt32(rowAiC2Properties2.Value);
            _systemParameters._AiCParams.IDs.Add(items);
            items._devicename = Convert.ToString(rowAiC3Properties1.Value);
            items._idNumber = Convert.ToInt32(rowAiC3Properties2.Value);
            _systemParameters._AiCParams.IDs.Add(items);
            items._devicename = Convert.ToString(rowAiC4Properties1.Value);
            items._idNumber = Convert.ToInt32(rowAiC4Properties2.Value);
            _systemParameters._AiCParams.IDs.Add(items);

            // System Parameter의 Remote I/O Serial 초기화
            _systemParameters._remoteIOParams.SerialParameters.PortName = Convert.ToString(rowRemoteIOCommunicationPortName.Properties.Value);
            _systemParameters._remoteIOParams.SerialParameters.BaudRates = Convert.ToInt32(rowRemoteIOCommunicationBaudRate.Properties.Value);
            _systemParameters._remoteIOParams.SerialParameters.Parity = (Parity)Enum.Parse(typeof(Parity), Convert.ToString(rowRemoteIOCommunicationParity.Properties.Value));
            _systemParameters._remoteIOParams.SerialParameters.StopBits = (StopBits)Enum.Parse(typeof(StopBits), Convert.ToString(rowRemoteIOCommunicationStopbit.Properties.Value));
            _systemParameters._remoteIOParams.SerialParameters.DataBits = Convert.ToInt32(rowRemoteIOCommunicationDatabit.Properties.Value);
            _systemParameters._remoteIOParams.SerialParameters.Handshake = (Handshake)Enum.Parse(typeof(Handshake), Convert.ToString(rowRemoteIOCommunicationHandshake.Properties.Value));
            _systemParameters._remoteIOParams.ConnectedNumber = Convert.ToInt32(rowRemoteIOCommunicationCount.Properties.Value);
            RemoteIOParams._IDs remoteitems = new RemoteIOParams._IDs();
            remoteitems._devicename = Convert.ToString(rowRemoteIOInputProperties1.Value);
            remoteitems._idNumber = Convert.ToInt32(rowRemoteIOInputProperties2.Value);
            _systemParameters._remoteIOParams.IDs.Add(remoteitems);
            remoteitems._devicename = Convert.ToString(rowRemoteIOOutputProperties1.Value);
            remoteitems._idNumber = Convert.ToInt32(rowRemoteIOOutputProperties2.Value);
            _systemParameters._remoteIOParams.IDs.Add(remoteitems);
            // System Parameter의 ADMS 파라미터 초기화
            _systemParameters._admsParams._enableCheck = Convert.ToBoolean(rowSystemADMSUse.Properties.Value);
            _systemParameters._bJobWorkInfomationEnable = Convert.ToBoolean(rowSystemJobWorkUse.Properties.Value);
            _systemParameters._admsParams._IpAddress = Convert.ToString(rowSystemADMSIPAddress.Properties.Value);
            _systemParameters._admsParams._port = Convert.ToInt32(rowSystemADMSPort.Properties.Value);
            _systemParameters._admsParams._userID = Convert.ToString(rowSystemADMSUserID.Properties.Value);
            _systemParameters._admsParams._password = Convert.ToString(rowSystemADMSPassWD.Properties.Value);
            _systemParameters._admsParams._equipmentname = Convert.ToString(rowSystemADMSEquipmentDBName.Properties.Value);
            _systemParameters._admsParams._eqpmentID = Convert.ToInt32(rowSystemADMSEquipmentID.Properties.Value);
            _systemParameters._admsParams._dbschemaname = Convert.ToString(rowSystemADMSSchemaName.Properties.Value);
            _systemParameters._admsParams._productname = Convert.ToString(rowSystemADMSProductDBName.Properties.Value);

            // System Parameter의 Light 파라미터 초기화            
            rowLightCommunicationPortName.Properties.Value = repositoryItemComboBoxLightCommunicationPortName.Items[2].ToString();
            rowLightCommunicationBaudRate.Properties.Value = repositoryItemComboBoxLightCommunicationBaudRate.Items[0].ToString();
            rowLightCommunicationDataBit.Properties.Value = repositoryItemComboBoxLightCommunicationDataBit.Items[4].ToString();
            rowLightCommunicationParity.Properties.Value = repositoryItemComboBoxLightCommunicationParity.Items[0].ToString();
            rowLightCommunicationStopBit.Properties.Value = repositoryItemComboBoxLightCommunicationStopBit.Items[0].ToString();
            rowLightCommunicationFlowControl.Properties.Value = repositoryItemComboBoxLightCommunicationFlowControl.Items[0].ToString();

            _systemParameters._LightParams.SerialParameters.PortName = Convert.ToString(rowLightCommunicationPortName.Properties.Value);
            _systemParameters._LightParams.SerialParameters.BaudRates = Convert.ToInt32(rowLightCommunicationBaudRate.Properties.Value);
            _systemParameters._LightParams.SerialParameters.Parity = (Parity)Enum.Parse(typeof(Parity), Convert.ToString(rowLightCommunicationParity.Properties.Value));
            _systemParameters._LightParams.SerialParameters.StopBits = (StopBits)Enum.Parse(typeof(StopBits), Convert.ToString(rowLightCommunicationStopBit.Properties.Value));
            _systemParameters._LightParams.SerialParameters.DataBits = Convert.ToInt32(rowLightCommunicationDataBit.Properties.Value);
            _systemParameters._LightParams.SerialParameters.Handshake = (Handshake)Enum.Parse(typeof(Handshake), Convert.ToString(rowLightCommunicationFlowControl.Properties.Value));

            // System Parameter의 Feeder 파라미터 초기화            
            rowFeederCommunicationPortName.Properties.Value = repositoryItemComboBoxFeederCommunicationPortName.Items[3].ToString();
            rowFeederCommunicationBaudRate.Properties.Value = repositoryItemComboBoxFeederCommunicationBaudRate.Items[4].ToString();
            rowFeederCommunicationDataBit.Properties.Value = repositoryItemComboBoxFeederCommunicationDataBit.Items[4].ToString();
            rowFeederCommunicationParity.Properties.Value = repositoryItemComboBoxFeederCommunicationParity.Items[0].ToString();
            rowFeederCommunicationStopBit.Properties.Value = repositoryItemComboBoxFeederCommunicationStopBit.Items[0].ToString();
            rowFeederCommunicationFlowControl.Properties.Value = repositoryItemComboBoxFeederCommunicationFlowControl.Items[0].ToString();

            _systemParameters._FeederParams.SerialParameters.PortName = Convert.ToString(rowFeederCommunicationPortName.Properties.Value);
            _systemParameters._FeederParams.SerialParameters.BaudRates = Convert.ToInt32(rowFeederCommunicationBaudRate.Properties.Value);
            _systemParameters._FeederParams.SerialParameters.Parity = (Parity)Enum.Parse(typeof(Parity), Convert.ToString(rowFeederCommunicationParity.Properties.Value));
            _systemParameters._FeederParams.SerialParameters.StopBits = (StopBits)Enum.Parse(typeof(StopBits), Convert.ToString(rowFeederCommunicationStopBit.Properties.Value));
            _systemParameters._FeederParams.SerialParameters.DataBits = Convert.ToInt32(rowFeederCommunicationDataBit.Properties.Value);
            _systemParameters._FeederParams.SerialParameters.Handshake = (Handshake)Enum.Parse(typeof(Handshake), Convert.ToString(rowFeederCommunicationFlowControl.Properties.Value));
            _systemParameters._FeederParams.FeederCommunicationID = Convert.ToInt32(rowFeederCommunicationIDNumber.Properties.Value);
            _systemParameters._FeederParams.FeederGearRatio = Convert.ToDouble(rowFeederGearRatio.Properties.Value);
            _systemParameters._FeederParams.FeederDiameter = Convert.ToDouble(rowFeederDiameter.Properties.Value);
            _systemParameters._FeederParams.FeederResolution = Convert.ToDouble(rowFeederResolution.Properties.Value);
            _systemParameters._FeederParams.FeederMoveVelocity = Convert.ToDouble(rowFeederVelocity.Properties.Value);

            // System Parameter의 Laser 파라미터 초기화        
            rowLaserCommunicationPortName.Properties.Value = repositoryItemComboBoxLaserCommunicationPortName.Items[4].ToString();
            rowLaserCommunicationBaudRate.Properties.Value = repositoryItemComboBoxLaserCommunicationBaudRate.Items[4].ToString();
            rowLaserCommunicationDataBit.Properties.Value = repositoryItemComboBoxLaserCommunicationDataBit.Items[4].ToString();
            rowLaserCommunicationParity.Properties.Value = repositoryItemComboBoxLaserCommunicationParity.Items[0].ToString();
            rowLaserCommunicationStopBit.Properties.Value = repositoryItemComboBoxLaserCommunicationStopBit.Items[0].ToString();
            rowLaserCommunicationFlowControl.Properties.Value = repositoryItemComboBoxLaserCommunicationFlowControl.Items[0].ToString();

            _systemParameters._LaserParams.SerialParameters.PortName = Convert.ToString(rowLaserCommunicationPortName.Properties.Value);
            _systemParameters._LaserParams.SerialParameters.BaudRates = Convert.ToInt32(rowLaserCommunicationBaudRate.Properties.Value);
            _systemParameters._LaserParams.SerialParameters.Parity = (Parity)Enum.Parse(typeof(Parity), Convert.ToString(rowLaserCommunicationParity.Properties.Value));
            _systemParameters._LaserParams.SerialParameters.StopBits = (StopBits)Enum.Parse(typeof(StopBits), Convert.ToString(rowLaserCommunicationStopBit.Properties.Value));
            _systemParameters._LaserParams.SerialParameters.DataBits = Convert.ToInt32(rowLaserCommunicationDataBit.Properties.Value);
            _systemParameters._LaserParams.SerialParameters.Handshake = (Handshake)Enum.Parse(typeof(Handshake), Convert.ToString(rowLaserCommunicationFlowControl.Properties.Value));

            // Save Result
            _systemParameters._saveResultVisionProcessImage = Convert.ToBoolean(rowSaveResultImageProcess.Properties.Value);
            _systemParameters._saveResultStatistics = Convert.ToBoolean(rowSaveResultStatistics.Properties.Value);

            _systemParameters._SystemLanguageKoreaUse = Convert.ToBoolean(rowSystemUseLanguage.Properties.Value);
        }
        private void LoadSystemParameters()
        {
            string strSystemFilePath = string.Format(@"{0}\{1}", SystemDirectoryParams.SystemFolderPath, SystemDirectoryParams.SystemFileName);

            if (File.Exists(strSystemFilePath))
            {
                RecipeFileIO.ReadSystemFile(_systemParameters, strSystemFilePath);
            }
            else
            {
                MessageBox.Show(
                    string.Format(string.Format("시스템 파일이 존재하지 않습니다. 기본 파일을 생성합니다.\r\n{0}", strSystemFilePath), "시스템 파일 생성",
                    MessageBoxButtons.OK, MessageBoxIcon.Information));

                RecipeFileIO.WriteSystemFile(_systemParameters, strSystemFilePath);
            }
            _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("System 설정 로드 완료"));
            UpdateSystemControls();
        }
        private void UpdateSystemControls()
        {
            // Camera Parameters            
            rowCameraFriendlyName.Properties.Value = _systemParameters._cameraParams.FriendlyName;
            rowCameraHResolution.Properties.Value = _systemParameters._cameraParams.HResolution;
            rowCameraVResolution.Properties.Value = _systemParameters._cameraParams.VResolution;
            rowCameraOnePixelResolution.Properties.Value = _systemParameters._cameraParams.OnePixelResolution;            

            // Calibration Coordinate Parameters
            rowCoordinateSwitch.Properties.Value = _systemParameters._calibrationParams._CoordinateSwitchEnable;
            rowImageToSystemX.Properties.Value = _systemParameters._calibrationParams._imagetoSystemXcoordi;
            rowImageToSystemY.Properties.Value = _systemParameters._calibrationParams._imagetoSystemYcoordi;
            rowCoordinateCalibrationActive.Properties.Value = _systemParameters._calibrationParams._CoordinateCalibrationActive;
            rowReference_X.Properties.Value = _systemParameters._calibrationParams._X_reference_Distance;
            rowReference_Y.Properties.Value = _systemParameters._calibrationParams._Y_reference_Distance;
            rowReference_Z.Properties.Value = _systemParameters._calibrationParams._Z_reference_Distance;

            // Motion Parameters
            rowMotionMenaulVelocity.Properties.Value = _systemParameters._motionParams.MenualMoveVelocity;
            rowMotionMoveVelocity.Properties.Value = _systemParameters._motionParams.MoveVelocity;
            rowMotionMoveAcceleration.Properties.Value = _systemParameters._motionParams.MoveAcceleration;
            rowMotionResolutionX.Properties.Value = _systemParameters._motionParams.OneTurnResolutionX;
            rowMotionResolutionY.Properties.Value = _systemParameters._motionParams.OneTurnResolutionY;
            rowMotionResolutionZ.Properties.Value = _systemParameters._motionParams.OneTurnResolutionZ;            
            rowMotionGearRatioX.Properties.Value = _systemParameters._motionParams.GearRatioX;
            rowMotionGearRatioY.Properties.Value = _systemParameters._motionParams.GearRatioY;
            rowMotionGearRatioZ.Properties.Value = _systemParameters._motionParams.GearRatioZ;            
            rowMotionBallLeadX.Properties.Value = _systemParameters._motionParams.BallLeadX;
            rowMotionBallLeadY.Properties.Value = _systemParameters._motionParams.BallLeadY;
            rowMotionBallLeadZ.Properties.Value = _systemParameters._motionParams.BallLeadZ;            

            // AiC Parameters                        
            rowAiCCommunicationPortName.Properties.Value = _systemParameters._AiCParams.SerialParameters.PortName;
            rowAiCCommunicationBaudRate.Properties.Value = _systemParameters._AiCParams.SerialParameters.BaudRates;
            rowAiCCommunicationDatabit.Properties.Value = _systemParameters._AiCParams.SerialParameters.DataBits;
            rowAiCCommunicationStopbit.Properties.Value = _systemParameters._AiCParams.SerialParameters.StopBits;
            rowAiCCommunicationParity.Properties.Value = _systemParameters._AiCParams.SerialParameters.Parity;
            rowAiCCommunicationHandshake.Properties.Value = _systemParameters._AiCParams.SerialParameters.Handshake;
            rowAiCCommunicationCounter.Properties.Value = _systemParameters._AiCParams.ConnectedNumber;
            if (_systemParameters._AiCParams.IDs.Count > 0)
            {
                rowAiC1Properties1.Value = _systemParameters._AiCParams.IDs[0]._devicename;
                rowAiC1Properties2.Value = _systemParameters._AiCParams.IDs[0]._idNumber;
            }
            if (_systemParameters._AiCParams.IDs.Count > 1)
            {
                rowAiC2Properties1.Value = _systemParameters._AiCParams.IDs[1]._devicename;
                rowAiC2Properties2.Value = _systemParameters._AiCParams.IDs[1]._idNumber;
            }
            if (_systemParameters._AiCParams.IDs.Count > 2)
            {
                rowAiC3Properties1.Value = _systemParameters._AiCParams.IDs[2]._devicename;
                rowAiC3Properties2.Value = _systemParameters._AiCParams.IDs[2]._idNumber;
            }
            if (_systemParameters._AiCParams.IDs.Count > 3)
            {
                rowAiC4Properties1.Value = _systemParameters._AiCParams.IDs[3]._devicename;
                rowAiC4Properties2.Value = _systemParameters._AiCParams.IDs[3]._idNumber;
            }

            // Remote I/O Parameters
            rowRemoteIOCommunicationPortName.Properties.Value = _systemParameters._remoteIOParams.SerialParameters.PortName;
            rowRemoteIOCommunicationBaudRate.Properties.Value = _systemParameters._remoteIOParams.SerialParameters.BaudRates;
            rowRemoteIOCommunicationDatabit.Properties.Value = _systemParameters._remoteIOParams.SerialParameters.DataBits;
            rowRemoteIOCommunicationStopbit.Properties.Value = _systemParameters._remoteIOParams.SerialParameters.StopBits;
            rowRemoteIOCommunicationParity.Properties.Value = _systemParameters._remoteIOParams.SerialParameters.Parity;
            rowRemoteIOCommunicationHandshake.Properties.Value = _systemParameters._remoteIOParams.SerialParameters.Handshake;
            rowRemoteIOCommunicationCount.Properties.Value = _systemParameters._remoteIOParams.ConnectedNumber;
            if (_systemParameters._remoteIOParams.IDs.Count > 0)
            {
                rowRemoteIOInputProperties1.Value = _systemParameters._remoteIOParams.IDs[0]._devicename;
                rowRemoteIOInputProperties2.Value = _systemParameters._remoteIOParams.IDs[0]._idNumber;
            }
            if (_systemParameters._remoteIOParams.IDs.Count > 1)
            {
                rowRemoteIOOutputProperties1.Value = _systemParameters._remoteIOParams.IDs[1]._devicename;
                rowRemoteIOOutputProperties2.Value = _systemParameters._remoteIOParams.IDs[1]._idNumber;
            }

            // ADMS Parameters
            rowSystemADMSUse.Properties.Value = _systemParameters._admsParams._enableCheck;
            rowSystemJobWorkUse.Properties.Value = _systemParameters._bJobWorkInfomationEnable;
            rowSystemADMSIPAddress.Properties.Value = _systemParameters._admsParams._IpAddress;
            rowSystemADMSPort.Properties.Value = _systemParameters._admsParams._port;
            rowSystemADMSUserID.Properties.Value = _systemParameters._admsParams._userID;
            rowSystemADMSPassWD.Properties.Value = _systemParameters._admsParams._password;
            rowSystemADMSEquipmentDBName.Properties.Value = _systemParameters._admsParams._equipmentname;
            rowSystemADMSEquipmentID.Properties.Value = _systemParameters._admsParams._eqpmentID;
            rowSystemADMSSchemaName.Properties.Value = _systemParameters._admsParams._dbschemaname;
            rowSystemADMSProductDBName.Properties.Value = _systemParameters._admsParams._productname;

            // System Light 파라미터 초기화
            rowLightCommunicationPortName.Properties.Value = _systemParameters._LightParams.SerialParameters.PortName;
            rowLightCommunicationBaudRate.Properties.Value = _systemParameters._LightParams.SerialParameters.BaudRates;
            rowLightCommunicationDataBit.Properties.Value = _systemParameters._LightParams.SerialParameters.DataBits;
            rowLightCommunicationStopBit.Properties.Value = _systemParameters._LightParams.SerialParameters.StopBits;
            rowLightCommunicationParity.Properties.Value = _systemParameters._LightParams.SerialParameters.Parity;
            rowLightCommunicationFlowControl.Properties.Value = _systemParameters._LightParams.SerialParameters.Handshake;

            // System Feeder 파라미터 초기화
            rowFeederCommunicationPortName.Properties.Value = _systemParameters._FeederParams.SerialParameters.PortName;
            rowFeederCommunicationBaudRate.Properties.Value = _systemParameters._FeederParams.SerialParameters.BaudRates;
            rowFeederCommunicationDataBit.Properties.Value = _systemParameters._FeederParams.SerialParameters.DataBits;
            rowFeederCommunicationStopBit.Properties.Value = _systemParameters._FeederParams.SerialParameters.StopBits;
            rowFeederCommunicationParity.Properties.Value = _systemParameters._FeederParams.SerialParameters.Parity;
            rowFeederCommunicationFlowControl.Properties.Value = _systemParameters._FeederParams.SerialParameters.Handshake;
            rowFeederCommunicationIDNumber.Properties.Value = _systemParameters._FeederParams.FeederCommunicationID;
            rowFeederGearRatio.Properties.Value = _systemParameters._FeederParams.FeederGearRatio;
            rowFeederDiameter.Properties.Value = _systemParameters._FeederParams.FeederDiameter;
            rowFeederResolution.Properties.Value = _systemParameters._FeederParams.FeederResolution;
            rowFeederVelocity.Properties.Value = _systemParameters._FeederParams.FeederMoveVelocity;

            // System Laser 파라미터 초기화
            rowLaserCommunicationPortName.Properties.Value = _systemParameters._LaserParams.SerialParameters.PortName;
            rowLaserCommunicationBaudRate.Properties.Value = _systemParameters._LaserParams.SerialParameters.BaudRates;
            rowLaserCommunicationDataBit.Properties.Value = _systemParameters._LaserParams.SerialParameters.DataBits;
            rowLaserCommunicationStopBit.Properties.Value = _systemParameters._LaserParams.SerialParameters.StopBits;
            rowLaserCommunicationParity.Properties.Value = _systemParameters._LaserParams.SerialParameters.Parity;
            rowLaserCommunicationFlowControl.Properties.Value = _systemParameters._LaserParams.SerialParameters.Handshake;

            // Save Result

            rowSaveResultImageProcess.Properties.Value = _systemParameters._saveResultVisionProcessImage;
            rowSaveResultStatistics.Properties.Value = _systemParameters._saveResultStatistics;

            // System Language Check
            rowSystemUseLanguage.Properties.Value = _systemParameters._SystemLanguageKoreaUse;
        }
        private void vGridControlSystemParameters_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DevExpress.XtraVerticalGrid.VGridControl vGrid = sender as DevExpress.XtraVerticalGrid.VGridControl;
                DevExpress.XtraVerticalGrid.Rows.BaseRow currentRow = vGrid.FocusedRow as DevExpress.XtraVerticalGrid.Rows.BaseRow;

                SetCellValue(currentRow);
            }
        }
        private void SetCellValue(DevExpress.XtraVerticalGrid.Rows.BaseRow currentRow)
        {
            int value = 0;
            float fValue = 0;
            string strTemp = string.Empty;

            if (currentRow == rowCameraFriendlyName)
            {
                strTemp = Convert.ToString(rowCameraFriendlyName.Properties.Value);

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._cameraParams.FriendlyName = strTemp;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("카메라 이름이 {0}로 변경되었습니다.", _systemParameters._cameraParams.FriendlyName));
            }
            else if (currentRow == rowCameraHResolution)
            {
                value = Convert.ToInt32(rowCameraHResolution.Properties.Value);

                if (value <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n카메라의 가로 해상도는 0보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowCameraHResolution.Properties.Value = _systemParameters._cameraParams.HResolution;
                    vGridControlSystemParameters.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._cameraParams.HResolution = value;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("카메라 가로 해상도가 {0}로 변경되었습니다.", _systemParameters._cameraParams.HResolution.ToString()));
            }
            else if (currentRow == rowCameraVResolution)
            {
                value = Convert.ToInt32(rowCameraVResolution.Properties.Value);

                if (value <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n카메라의 세로 해상도는 0보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowCameraVResolution.Properties.Value = _systemParameters._cameraParams.VResolution;
                    vGridControlSystemParameters.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._cameraParams.VResolution = value;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("카메라 세로 해상도가 {0}로 변경되었습니다.", _systemParameters._cameraParams.VResolution.ToString()));
            }
            else if (currentRow == rowCameraOnePixelResolution)
            {
                fValue = Convert.ToSingle(rowCameraOnePixelResolution.Properties.Value);

                if (fValue <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n카메라의 한 픽셀 크기는 0보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowCameraOnePixelResolution.Properties.Value = _systemParameters._cameraParams.OnePixelResolution;
                    vGridControlSystemParameters.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._cameraParams.OnePixelResolution = fValue;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("카메라 픽셀 해상도가 {0}로 변경되었습니다.", _systemParameters._cameraParams.OnePixelResolution.ToString()));
            }
            else if (currentRow == rowCoordinateSwitch)
            {
                bool check = Convert.ToBoolean(rowCoordinateSwitch.Properties.Value);
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._calibrationParams._CoordinateSwitchEnable = check;
            }
            else if (currentRow == rowImageToSystemX)
            {
                fValue = Convert.ToSingle(rowImageToSystemX.Properties.Value);

                if (fValue == 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n좌표계 보정값은 0보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowImageToSystemX.Properties.Value = _systemParameters._calibrationParams._imagetoSystemXcoordi;
                    vGridControlSystemParameters.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._calibrationParams._imagetoSystemXcoordi = fValue;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("이미지 X 좌표계 -> X 실좌표계 극성값이 {0}로 변경되었습니다.", _systemParameters._calibrationParams._imagetoSystemXcoordi.ToString()));
            }
            else if (currentRow == rowImageToSystemY)
            {
                fValue = Convert.ToSingle(rowImageToSystemY.Properties.Value);

                if (fValue == 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n좌표계 보정값은 0보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowImageToSystemY.Properties.Value = _systemParameters._calibrationParams._imagetoSystemYcoordi;
                    vGridControlSystemParameters.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._calibrationParams._imagetoSystemYcoordi = fValue;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("이미지 Y 좌표계 -> Z 실좌표계 극성값이 {0}로 변경되었습니다.", _systemParameters._calibrationParams._imagetoSystemYcoordi.ToString()));
            }
            else if (currentRow == rowCoordinateCalibrationActive)
            {
                bool check = Convert.ToBoolean(rowCoordinateCalibrationActive.Properties.Value);
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._calibrationParams._CoordinateCalibrationActive = check;
            }
            else if (currentRow == rowReference_X)
            {
                fValue = Convert.ToSingle(rowReference_X.Properties.Value);
                if (fValue <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nX축 이동 기준값은 0보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowReference_X.Properties.Value = _systemParameters._calibrationParams._X_reference_Distance;
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._calibrationParams._X_reference_Distance = fValue;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("X축 이동 기준값이 {0}로 변경되었습니다.", _systemParameters._calibrationParams._X_reference_Distance.ToString()));
            }
            else if (currentRow == rowReference_Y)
            {
                fValue = Convert.ToSingle(rowReference_Y.Properties.Value);
                if (fValue <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nY축 이동 기준값은 0보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowReference_Y.Properties.Value = _systemParameters._calibrationParams._Y_reference_Distance;
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._calibrationParams._Y_reference_Distance = fValue;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Y축 이동 기본 값이 {0}로 변경되었습니다.", _systemParameters._calibrationParams._Y_reference_Distance.ToString()));
            }
            else if (currentRow == rowReference_Z)
            {
                fValue = Convert.ToSingle(rowReference_Z.Properties.Value);
                if (fValue <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nZ축 이동 기준값은 0보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowReference_Z.Properties.Value = _systemParameters._calibrationParams._Z_reference_Distance;
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._calibrationParams._Z_reference_Distance = fValue;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Z축 이동 기준 값이 {0}로 변경되었습니다.", _systemParameters._calibrationParams._Z_reference_Distance.ToString()));
            }
            else if (currentRow == rowMotionMoveVelocity)
            {
                fValue = Convert.ToSingle(rowMotionMoveVelocity.Properties.Value);
                if (fValue <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n이동 속도 값은 0보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowMotionMoveVelocity.Properties.Value = _systemParameters._motionParams.MoveVelocity;
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._motionParams.MoveVelocity = fValue;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("이동 속도 값이 {0}로 변경되었습니다.", _systemParameters._motionParams.MoveVelocity.ToString()));
            }
            else if (currentRow == rowMotionMenaulVelocity)
            {
                fValue = Convert.ToSingle(rowMotionMenaulVelocity.Properties.Value);
                if (fValue <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n수동 이동 속도 값은 0보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowMotionMenaulVelocity.Properties.Value = _systemParameters._motionParams.MenualMoveVelocity;
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._motionParams.MenualMoveVelocity = fValue;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("수동이동 속도 값이 {0}로 변경되었습니다.", _systemParameters._motionParams.MenualMoveVelocity.ToString()));
            }
            else if (currentRow == rowMotionMoveAcceleration)
            {
                fValue = Convert.ToSingle(rowMotionMoveAcceleration.Properties.Value);
                if (fValue <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n이동 가속도 값은 0보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowMotionMoveAcceleration.Properties.Value = _systemParameters._motionParams.MoveAcceleration;
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._motionParams.MoveAcceleration = fValue;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("이동 가속도 값이 {0}로 변경되었습니다.", _systemParameters._motionParams.MoveAcceleration.ToString()));
            }
            else if (currentRow == rowMotionResolutionX)
            {
                value = Convert.ToInt32(rowMotionResolutionX.Properties.Value);
                if (value <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nX축 분해능 값은 0보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowMotionResolutionX.Properties.Value = _systemParameters._motionParams.OneTurnResolutionX;
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._motionParams.OneTurnResolutionX = value;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("X축 분해능 값이 {0}로 변경되었습니다.", _systemParameters._motionParams.OneTurnResolutionX.ToString()));
            }
            else if (currentRow == rowMotionResolutionY)
            {
                value = Convert.ToInt32(rowMotionResolutionY.Properties.Value);
                if (value <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nR축 분해능 값은 0보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowMotionResolutionY.Properties.Value = _systemParameters._motionParams.OneTurnResolutionY;
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._motionParams.OneTurnResolutionY = value;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("R축 분해능 값이 {0}로 변경되었습니다.", _systemParameters._motionParams.OneTurnResolutionY.ToString()));
            }
            else if (currentRow == rowMotionResolutionZ)
            {
                value = Convert.ToInt32(rowMotionResolutionZ.Properties.Value);
                if (value <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nZ축 분해능 값은 0보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowMotionResolutionZ.Properties.Value = _systemParameters._motionParams.OneTurnResolutionZ;
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._motionParams.OneTurnResolutionZ = value;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Z축 분해능 값이 {0}로 변경되었습니다.", _systemParameters._motionParams.OneTurnResolutionZ.ToString()));
            }
            else if (currentRow == rowMotionGearRatioX)
            {
                fValue = Convert.ToSingle(rowMotionGearRatioX.Properties.Value);
                if (fValue <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nX축 기어비 값은 1보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowMotionGearRatioX.Properties.Value = _systemParameters._motionParams.GearRatioX;
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._motionParams.GearRatioX = fValue;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("X축 기어비 값이 {0}로 변경되었습니다.", _systemParameters._motionParams.GearRatioX.ToString()));
            }
            else if (currentRow == rowMotionGearRatioY)
            {
                fValue = Convert.ToSingle(rowMotionGearRatioY.Properties.Value);
                if (fValue <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nY축 기어비 값은 1보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowMotionGearRatioY.Properties.Value = _systemParameters._motionParams.GearRatioY;
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._motionParams.GearRatioY = fValue;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("R축 기어비 값이 {0}로 변경되었습니다.", _systemParameters._motionParams.GearRatioY.ToString()));
            }
            else if (currentRow == rowMotionGearRatioZ)
            {
                fValue = Convert.ToSingle(rowMotionGearRatioZ.Properties.Value);
                if (fValue <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nZ축 기어비 값은 1보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowMotionGearRatioZ.Properties.Value = _systemParameters._motionParams.GearRatioZ;
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._motionParams.GearRatioZ = fValue;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Z축 기어비 값이 {0}로 변경되었습니다.", _systemParameters._motionParams.GearRatioZ.ToString()));
            }
            else if (currentRow == rowMotionBallLeadX)
            {
                fValue = Convert.ToSingle(rowMotionBallLeadX.Properties.Value);
                if (fValue <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nX축 Ball Lead 값은 1보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowMotionBallLeadX.Properties.Value = _systemParameters._motionParams.BallLeadX;
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._motionParams.BallLeadX = fValue;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("X축 볼리드 값이 {0}로 변경되었습니다.", _systemParameters._motionParams.BallLeadX.ToString()));
            }
            else if (currentRow == rowMotionBallLeadY)
            {
                fValue = Convert.ToSingle(rowMotionBallLeadY.Properties.Value);
                if (fValue <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nY축 Ball Lead 값은 1보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowMotionBallLeadY.Properties.Value = _systemParameters._motionParams.BallLeadY;
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._motionParams.BallLeadY = fValue;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("R축 볼리드 값이 {0}로 변경되었습니다.", _systemParameters._motionParams.BallLeadY.ToString()));
            }
            else if (currentRow == rowMotionBallLeadZ)
            {
                fValue = Convert.ToSingle(rowMotionBallLeadZ.Properties.Value);
                if (fValue <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nZ축 Ball Lead 값은 1보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowMotionBallLeadZ.Properties.Value = _systemParameters._motionParams.BallLeadZ;
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._motionParams.BallLeadZ = fValue;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Z축 볼리드 값이 {0}로 변경되었습니다.", _systemParameters._motionParams.BallLeadZ.ToString()));
            }
            else if (currentRow == rowAiCCommunicationPortName)
            {
                bool IsValidate = false;
                strTemp = Convert.ToString(rowAiCCommunicationPortName.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxAiCCommunicationPortName.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxAiCCommunicationPortName.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n통신 포트 값은 COM1~20 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowAiCCommunicationPortName.Properties.Value = _systemParameters._AiCParams.SerialParameters.PortName;
                    vGridControlSystemParameters.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._AiCParams.SerialParameters.PortName = strTemp;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("AiC 통신 포트를 {0}로 변경되었습니다.", _systemParameters._AiCParams.SerialParameters.PortName));
            }
            else if (currentRow == rowAiCCommunicationBaudRate)
            {
                bool IsValidate = false;
                value = Convert.ToInt32(rowAiCCommunicationBaudRate.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxAiCCommunicationBaudRate.Items.Count; ++i)
                {
                    if (value == Convert.ToInt32(repositoryItemComboBoxAiCCommunicationBaudRate.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nBaudRates 값은 9600, 19200, 38400, 57600, 115200 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowAiCCommunicationBaudRate.Properties.Value = _systemParameters._AiCParams.SerialParameters.BaudRates;
                    vGridControlSystemParameters.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._AiCParams.SerialParameters.BaudRates = value;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("AiC 통신 속도를 {0}로 변경되었습니다.", _systemParameters._AiCParams.SerialParameters.BaudRates.ToString()));
            }
            else if (currentRow == rowAiCCommunicationStopbit)
            {
                bool IsValidate = false;
                value = Convert.ToInt32(rowAiCCommunicationStopbit.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxAiCCommunicationStopbit.Items.Count; ++i)
                {
                    if (value == Convert.ToInt32(repositoryItemComboBoxAiCCommunicationStopbit.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n정지 비트 값은 None, One, Two, One5 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //rowPanelMeterCommunicationStopbit.Properties.Value = _systemParameters._panelmeterParams.SerialParameters.StopBits;
                    rowAiCCommunicationStopbit.Properties.Value = Enum.GetName(typeof(StopBits), (StopBits)_systemParameters._AiCParams.SerialParameters.StopBits);
                    vGridControlSystemParameters.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                //_systemParameters._panelmeterParams.SerialParameters.StopBits = value;
                _systemParameters._AiCParams.SerialParameters.StopBits = (StopBits)Enum.Parse(typeof(StopBits), strTemp);
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("AiC 통신 정지 비트를 {0}로 변경되었습니다.", _systemParameters._AiCParams.SerialParameters.StopBits.ToString()));
            }
            else if (currentRow == rowAiCCommunicationDatabit)
            {
                bool IsValidate = false;
                value = Convert.ToInt32(rowAiCCommunicationDatabit.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxAiCCommunicationDatabit.Items.Count; ++i)
                {
                    if (value == Convert.ToInt32(repositoryItemComboBoxAiCCommunicationDatabit.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nDataBits 값은 4, 5, 6, 7, 8 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowAiCCommunicationDatabit.Properties.Value = _systemParameters._AiCParams.SerialParameters.DataBits;
                    vGridControlSystemParameters.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._AiCParams.SerialParameters.DataBits = value;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("AiC 통신 데이터 비트를 {0}로 변경되었습니다.", _systemParameters._AiCParams.SerialParameters.DataBits.ToString()));
            }
            else if (currentRow == rowAiCCommunicationHandshake)
            {
                bool IsValidate = false;
                strTemp = Convert.ToString(rowAiCCommunicationHandshake.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxAiCCommunicationHandshake.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxAiCCommunicationHandshake.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n흐름 제어 값은 None, XonXoff, RequestToSend, RequestToSendXonXoff 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //rowPanelMeterCommunicationHandshake.Properties.Value = _systemParameters._panelmeterParams.SerialParameters.Handshake;
                    rowAiCCommunicationHandshake.Properties.Value = Enum.GetName(typeof(Handshake), (Handshake)_systemParameters._AiCParams.SerialParameters.Handshake);
                    vGridControlSystemParameters.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                //_systemParameters._panelmeterParams.SerialParameters.Handshake = strTemp;
                _systemParameters._AiCParams.SerialParameters.Handshake = (Handshake)Enum.Parse(typeof(Handshake), strTemp);
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("AiC 통신 흐름 제어를 {0}로 변경되었습니다.", _systemParameters._AiCParams.SerialParameters.Handshake.ToString()));
            }
            else if (currentRow == rowAiCCommunicationParity)
            {
                bool IsValidate = false;
                strTemp = Convert.ToString(rowAiCCommunicationParity.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxAiCCommunicationParity.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxAiCCommunicationParity.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n패리티 값은 None, Odd, Even, Mark, Space 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //rowPanelMeterCommunicationParity.Properties.Value = _systemParameters._panelmeterParams.SerialParameters.Parity;
                    rowAiCCommunicationParity.Properties.Value = Enum.GetName(typeof(Parity), (Parity)_systemParameters._AiCParams.SerialParameters.Parity);
                    vGridControlSystemParameters.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                //_systemParameters._panelmeterParams.SerialParameters.Parity = strTemp;
                _systemParameters._AiCParams.SerialParameters.Parity = (Parity)Enum.Parse(typeof(Parity), strTemp);
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("AiC 통신 parity를 {0}로 변경되었습니다.", _systemParameters._AiCParams.SerialParameters.Parity.ToString()));
            }
            else if (currentRow == rowAiCCommunicationCounter)
            {
                ///*
                value = Convert.ToInt32(rowAiCCommunicationCounter.Properties.Value);
                if (value < 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n AiC 연결대수는 0보다 크거나 같은 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowAiCCommunicationCounter.Properties.Value = _systemParameters._AiCParams.ConnectedNumber;
                    vGridControlSystemParameters.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._AiCParams.ConnectedNumber = value;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("AiC 통신 연결 대수를 {0}로 변경되었습니다.", _systemParameters._AiCParams.ConnectedNumber.ToString()));
                //*/
            }
            else if (currentRow == rowAiCXAxis)
            {
                AiCParams._IDs tempIDs = new AiCParams._IDs();
                tempIDs._devicename = Convert.ToString(rowAiC1Properties1.Value);
                tempIDs._idNumber = Convert.ToInt32(rowAiC1Properties2.Value);

                if (tempIDs._devicename == string.Empty)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nAiC 모션 이름 설정을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //rowPanelMeterCommunicationPortName.Properties.Value = _systemParameters._panelmeterParams.;
                    if (_systemParameters._AiCParams.IDs.Count > 0)
                    {
                        rowAiC1Properties1.Value = _systemParameters._AiCParams.IDs[0]._devicename;
                        rowAiC1Properties2.Value = _systemParameters._AiCParams.IDs[0]._idNumber;
                    }
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                if (_systemParameters._AiCParams.IDs.Count > 0)
                {
                    simpleButtonSystemFileSave.Enabled = true;
                    _systemParameters._AiCParams.IDs[0] = tempIDs;
                }
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("AiC X축 ID를 {0}로 변경되었습니다.", _systemParameters._AiCParams.IDs[0].ToString()));
            }
            else if (currentRow == rowAiCYAxis)
            {
                AiCParams._IDs tempIDs = new AiCParams._IDs();
                tempIDs._devicename = Convert.ToString(rowAiC2Properties1.Value);
                tempIDs._idNumber = Convert.ToInt32(rowAiC2Properties2.Value);

                if (tempIDs._devicename == string.Empty)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nAiC 모션 이름 설정을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //rowPanelMeterCommunicationPortName.Properties.Value = _systemParameters._panelmeterParams.;
                    if (_systemParameters._AiCParams.IDs.Count > 1)
                    {
                        rowAiC2Properties1.Value = _systemParameters._AiCParams.IDs[1]._devicename;
                        rowAiC2Properties2.Value = _systemParameters._AiCParams.IDs[1]._idNumber;
                    }
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                if (_systemParameters._AiCParams.IDs.Count > 1)
                {
                    simpleButtonSystemFileSave.Enabled = true;
                    _systemParameters._AiCParams.IDs[1] = tempIDs;
                }
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("AiC Y축 ID를 {0}로 변경되었습니다.", _systemParameters._AiCParams.IDs[1].ToString()));
            }
            else if (currentRow == rowAiCZAxis)
            {
                AiCParams._IDs tempIDs = new AiCParams._IDs();
                tempIDs._devicename = Convert.ToString(rowAiC3Properties1.Value);
                tempIDs._idNumber = Convert.ToInt32(rowAiC3Properties2.Value);

                if (tempIDs._devicename == string.Empty)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nAiC 모션 이름 설정을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //rowPanelMeterCommunicationPortName.Properties.Value = _systemParameters._panelmeterParams.;
                    if (_systemParameters._AiCParams.IDs.Count > 2)
                    {
                        rowAiC3Properties1.Value = _systemParameters._AiCParams.IDs[2]._devicename;
                        rowAiC3Properties2.Value = _systemParameters._AiCParams.IDs[2]._idNumber;
                    }
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                if (_systemParameters._AiCParams.IDs.Count > 2)
                {
                    simpleButtonSystemFileSave.Enabled = true;
                    _systemParameters._AiCParams.IDs[2] = tempIDs;
                }
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("AiC Z축 ID를 {0}로 변경되었습니다.", _systemParameters._AiCParams.IDs[2].ToString()));
            }
            else if (currentRow == rowAiCLoaderAxis)
            {
                AiCParams._IDs tempIDs = new AiCParams._IDs();
                tempIDs._devicename = Convert.ToString(rowAiC4Properties1.Value);
                tempIDs._idNumber = Convert.ToInt32(rowAiC4Properties2.Value);

                if (tempIDs._devicename == string.Empty)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nAiC 모션 이름 설정을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //rowPanelMeterCommunicationPortName.Properties.Value = _systemParameters._panelmeterParams.;
                    if (_systemParameters._AiCParams.IDs.Count > 3)
                    {
                        rowAiC4Properties1.Value = _systemParameters._AiCParams.IDs[3]._devicename;
                        rowAiC4Properties2.Value = _systemParameters._AiCParams.IDs[3]._idNumber;
                    }
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                if (_systemParameters._AiCParams.IDs.Count > 3)
                {
                    simpleButtonSystemFileSave.Enabled = true;
                    _systemParameters._AiCParams.IDs[3] = tempIDs;
                }
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("AiC θ축 ID를 {0}로 변경되었습니다.", _systemParameters._AiCParams.IDs[3].ToString()));
            }
            else if (currentRow == rowRemoteIOCommunicationStopbit)
            {
                bool IsValidate = false;
                value = Convert.ToInt32(rowRemoteIOCommunicationStopbit.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxRemoteIOCommunicationStopbit.Items.Count; ++i)
                {
                    if (value == Convert.ToInt32(repositoryItemComboBoxRemoteIOCommunicationStopbit.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n정지 비트 값은 None, One, Two, One5 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //rowPhotoCommunicationStopbit.Properties.Value = _systemParameters._photoParams.SerialParameters.StopBits;
                    rowRemoteIOCommunicationStopbit.Properties.Value = Enum.GetName(typeof(StopBits), (StopBits)_systemParameters._remoteIOParams.SerialParameters.StopBits);
                    vGridControlSystemParameters.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                //_systemParameters._photoParams.SerialParameters.StopBits = value;
                _systemParameters._remoteIOParams.SerialParameters.StopBits = (StopBits)Enum.Parse(typeof(StopBits), strTemp);
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("RemoteIO 통신 정지 비트를 {0}로 변경되었습니다.", _systemParameters._remoteIOParams.SerialParameters.StopBits.ToString()));
            }
            else if (currentRow == rowRemoteIOCommunicationBaudRate)
            {
                bool IsValidate = false;
                value = Convert.ToInt32(rowRemoteIOCommunicationBaudRate.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxRemoteIOCommunicationBaudRate.Items.Count; ++i)
                {
                    if (value == Convert.ToInt32(repositoryItemComboBoxRemoteIOCommunicationBaudRate.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nBaudRates 값은 9600, 19200, 38400, 57600, 115200 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowRemoteIOCommunicationBaudRate.Properties.Value = _systemParameters._remoteIOParams.SerialParameters.BaudRates;
                    vGridControlSystemParameters.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._remoteIOParams.SerialParameters.BaudRates = value;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("RemoteIO 통신 속도를 {0}로 변경되었습니다.", _systemParameters._remoteIOParams.SerialParameters.BaudRates.ToString()));
            }
            else if (currentRow == rowRemoteIOCommunicationDatabit)
            {
                bool IsValidate = false;
                value = Convert.ToInt32(rowRemoteIOCommunicationDatabit.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxRemoteIOCommunicationDatabit.Items.Count; ++i)
                {
                    if (value == Convert.ToInt32(repositoryItemComboBoxRemoteIOCommunicationDatabit.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nDataBits 값은 4, 5, 6, 7, 8 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowRemoteIOCommunicationDatabit.Properties.Value = _systemParameters._remoteIOParams.SerialParameters.DataBits;
                    vGridControlSystemParameters.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._remoteIOParams.SerialParameters.DataBits = value;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("RemoteIO 통신 데이터 비트를 {0}로 변경되었습니다.", _systemParameters._remoteIOParams.SerialParameters.DataBits.ToString()));
            }
            else if (currentRow == rowRemoteIOCommunicationHandshake)
            {
                bool IsValidate = false;
                strTemp = Convert.ToString(rowRemoteIOCommunicationHandshake.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxRemoteIOCommunicationHandshake.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxRemoteIOCommunicationHandshake.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n흐름 제어 값은 None, XonXoff, RequestToSend, RequestToSendXonXoff 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //rowPhotoCommunicationHandshake.Properties.Value = _systemParameters._photoParams.SerialParameters.Handshake;
                    rowRemoteIOCommunicationHandshake.Properties.Value = Enum.GetName(typeof(Handshake), (Handshake)_systemParameters._remoteIOParams.SerialParameters.Handshake);
                    vGridControlSystemParameters.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                //_systemParameters._photoParams.SerialParameters.Handshake = strTemp;
                _systemParameters._remoteIOParams.SerialParameters.Handshake = (Handshake)Enum.Parse(typeof(Handshake), strTemp);
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("RemoteIO 통신 흐름제어를 {0}로 변경되었습니다.", _systemParameters._remoteIOParams.SerialParameters.Handshake.ToString()));
            }
            else if (currentRow == rowRemoteIOCommunicationParity)
            {
                bool IsValidate = false;
                strTemp = Convert.ToString(rowRemoteIOCommunicationParity.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxRemoteIOCommunicationParity.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxRemoteIOCommunicationParity.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n패리티 값은 None, Odd, Even, Mark, Space 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //rowPhotoCommunicationParity.Properties.Value = _systemParameters._photoParams.SerialParameters.Parity;
                    rowRemoteIOCommunicationParity.Properties.Value = Enum.GetName(typeof(Parity), (Parity)_systemParameters._remoteIOParams.SerialParameters.Parity);
                    vGridControlSystemParameters.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._remoteIOParams.SerialParameters.Parity = (Parity)Enum.Parse(typeof(Parity), strTemp);
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("RemoteIO 통신 Parity를 {0}로 변경되었습니다.", _systemParameters._remoteIOParams.SerialParameters.Parity.ToString()));
            }
            else if (currentRow == rowRemoteIOCommunicationPortName)
            {
                bool IsValidate = false;
                strTemp = Convert.ToString(rowRemoteIOCommunicationPortName.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxRemoteIOCommunicationPortName.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxRemoteIOCommunicationPortName.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n통신 포트 값은 COM1~20 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowRemoteIOCommunicationPortName.Properties.Value = _systemParameters._remoteIOParams.SerialParameters.PortName;
                    vGridControlSystemParameters.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._remoteIOParams.SerialParameters.PortName = strTemp;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("RemoteIO 통신 포트를 {0}로 변경되었습니다.", _systemParameters._remoteIOParams.SerialParameters.PortName));
            }
            else if (currentRow == rowRemoteIOCommunicationStopbit)
            {
                bool IsValidate = false;
                strTemp = Convert.ToString(rowRemoteIOCommunicationStopbit.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxRemoteIOCommunicationStopbit.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxRemoteIOCommunicationStopbit.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n정지 비트 값은 None, One, Two, OnePointFive 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowRemoteIOCommunicationStopbit.Properties.Value = Enum.GetName(typeof(StopBits), (StopBits)_systemParameters._remoteIOParams.SerialParameters.StopBits);
                    vGridControlSystemParameters.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._remoteIOParams.SerialParameters.StopBits = (StopBits)Enum.Parse(typeof(StopBits), strTemp);
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("RemoteIO 통신 정지 비트를 {0}로 변경되었습니다.", _systemParameters._remoteIOParams.SerialParameters.StopBits.ToString()));
            }
            else if (currentRow == rowRemoteIOCommunicationCount)
            {
                ///*
                value = Convert.ToInt32(rowRemoteIOCommunicationCount.Properties.Value);
                if (value < 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n 리모트I/O 연결대수는 0보다 크거나 같은 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowRemoteIOCommunicationCount.Properties.Value = _systemParameters._remoteIOParams.ConnectedNumber;
                    vGridControlSystemParameters.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._remoteIOParams.ConnectedNumber = value;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("RemoteIO 통신 연결 대수를 {0}로 변경되었습니다.", _systemParameters._remoteIOParams.ConnectedNumber.ToString()));
                //*/
            }
            else if (currentRow == rowRemoteIOInputIDs)
            {
                ///*
                RemoteIOParams._IDs tempIDs = new RemoteIOParams._IDs();
                tempIDs._devicename = Convert.ToString(rowRemoteIOInputProperties1.Value);
                tempIDs._idNumber = Convert.ToInt32(rowRemoteIOInputProperties2.Value);

                if (tempIDs._devicename == string.Empty)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n판넬메타 이름 설정을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //rowPanelMeterCommunicationPortName.Properties.Value = _systemParameters._panelmeterParams.;
                    if (_systemParameters._remoteIOParams.IDs.Count > 0)
                    {
                        rowRemoteIOInputProperties1.Value = _systemParameters._remoteIOParams.IDs[0]._devicename;
                        rowRemoteIOInputProperties2.Value = _systemParameters._remoteIOParams.IDs[0]._idNumber;
                    }
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                if (_systemParameters._remoteIOParams.IDs.Count > 0)
                {
                    simpleButtonSystemFileSave.Enabled = true;
                    _systemParameters._remoteIOParams.IDs[0] = tempIDs;
                }
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("RemoteIO 모듈 1 ID를 {0}로 변경되었습니다.", _systemParameters._remoteIOParams.IDs[0].ToString()));
                //*/
            }
            else if (currentRow == rowRemoteIOOutputIDs)
            {
                ///*
                RemoteIOParams._IDs tempIDs = new RemoteIOParams._IDs();
                tempIDs._devicename = Convert.ToString(rowRemoteIOOutputProperties1.Value);
                tempIDs._idNumber = Convert.ToInt32(rowRemoteIOOutputProperties2.Value);

                if (tempIDs._devicename == string.Empty)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n판넬메타 이름 설정을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //rowPanelMeterCommunicationPortName.Properties.Value = _systemParameters._panelmeterParams.;
                    if (_systemParameters._remoteIOParams.IDs.Count > 1)
                    {
                        rowRemoteIOOutputProperties1.Value = _systemParameters._remoteIOParams.IDs[1]._devicename;
                        rowRemoteIOOutputProperties2.Value = _systemParameters._remoteIOParams.IDs[1]._idNumber;
                    }
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                if (_systemParameters._remoteIOParams.IDs.Count > 1)
                {
                    simpleButtonSystemFileSave.Enabled = true;
                    _systemParameters._remoteIOParams.IDs[1] = tempIDs;
                }
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("RemoteIO 모듈 2 ID를 {0}로 변경되었습니다.", _systemParameters._remoteIOParams.IDs[1].ToString()));
                //*/
            }
            else if (currentRow == rowSystemADMSUse)
            {
                bool check = Convert.ToBoolean(rowSystemADMSUse.Properties.Value);
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._admsParams._enableCheck = check;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("ADMS 사용여부가 {0}로 변경되었습니다.", _systemParameters._admsParams._enableCheck.ToString()));
            }
            else if (currentRow == rowSystemJobWorkUse)
            {
                bool check = Convert.ToBoolean(rowSystemJobWorkUse.Properties.Value);
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._bJobWorkInfomationEnable = check;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("작업지시서 DB 사용여부가 {0}로 변경되었습니다.", _systemParameters._bJobWorkInfomationEnable.ToString()));
            }
            else if (currentRow == rowSystemADMSIPAddress)
            {
                strTemp = Convert.ToString(rowSystemADMSIPAddress.Properties.Value);
                if (strTemp == string.Empty)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nDB Ip주소를 입력하세요", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowSystemADMSIPAddress.Properties.Value = _systemParameters._admsParams._IpAddress;
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._admsParams._IpAddress = strTemp;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("ADMS IP 주소가 {0}로 변경되었습니다.", _systemParameters._admsParams._IpAddress));
            }
            else if (currentRow == rowSystemADMSPort)
            {
                value = Convert.ToInt32(rowSystemADMSPort.Properties.Value);
                if (value <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nDB 포트를 입력하세요", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowSystemADMSPort.Properties.Value = _systemParameters._admsParams._port;
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._admsParams._port = value;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("ADMS DB 포트가 {0}로 변경되었습니다.", _systemParameters._admsParams._port.ToString()));
            }
            else if (currentRow == rowSystemADMSUserID)
            {
                strTemp = Convert.ToString(rowSystemADMSUserID.Properties.Value);
                if (strTemp == string.Empty)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nDB User ID를 입력하세요", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowSystemADMSUserID.Properties.Value = _systemParameters._admsParams._userID;
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._admsParams._userID = strTemp;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("ADMS DB User ID가 {0}로 변경되었습니다.", _systemParameters._admsParams._userID));
            }
            else if (currentRow == rowSystemADMSPassWD)
            {
                strTemp = Convert.ToString(rowSystemADMSPassWD.Properties.Value);
                if (strTemp == string.Empty)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nDB User ID를 입력하세요", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowSystemADMSPassWD.Properties.Value = _systemParameters._admsParams._password;
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._admsParams._password = strTemp;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("ADMS DB User 비밀번호가 {0}로 변경되었습니다.", _systemParameters._admsParams._password));
            }
            else if (currentRow == rowSystemADMSEquipmentID)
            {
                value = Convert.ToInt32(rowSystemADMSEquipmentID.Properties.Value);
                if (value <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nEquipmentID를 입력하세요", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowSystemADMSEquipmentID.Properties.Value = _systemParameters._admsParams._eqpmentID;
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._admsParams._eqpmentID = value;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("ADMS DB eqpmentID가 {0}로 변경되었습니다.", _systemParameters._admsParams._eqpmentID.ToString()));
            }
            else if (currentRow == rowSystemADMSEquipmentDBName)
            {
                strTemp = Convert.ToString(rowSystemADMSEquipmentDBName.Properties.Value);
                if (strTemp == string.Empty)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nEquipmentDB 이름 입력하세요", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowSystemADMSEquipmentDBName.Properties.Value = _systemParameters._admsParams._equipmentname;
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._admsParams._equipmentname = strTemp;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("ADMS DB eqpment name이 {0}로 변경되었습니다.", _systemParameters._admsParams._equipmentname));
            }
            else if (currentRow == rowSystemADMSSchemaName)
            {
                strTemp = Convert.ToString(rowSystemADMSSchemaName.Properties.Value);
                if (strTemp == string.Empty)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nDB schema 이름 입력하세요", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowSystemADMSSchemaName.Properties.Value = _systemParameters._admsParams._dbschemaname;
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._admsParams._dbschemaname = strTemp;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("ADMS DB schema name이 {0}로 변경되었습니다.", _systemParameters._admsParams._dbschemaname));
            }
            else if (currentRow == rowSystemADMSProductDBName)
            {
                strTemp = Convert.ToString(rowSystemADMSProductDBName.Properties.Value);
                if (strTemp == string.Empty)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nProduct DB 이름 입력하세요", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowSystemADMSProductDBName.Properties.Value = _systemParameters._admsParams._productname;
                    vGridControlSystemParameters.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._admsParams._productname = strTemp;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("ADMS DB product name이 {0}로 변경되었습니다.", _systemParameters._admsParams._productname));
            }
            else if (currentRow == rowSaveResultImageProcess)
            {
                bool bvalue = false;
                bvalue = Convert.ToBoolean(rowSaveResultImageProcess.Properties.Value);

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._saveResultVisionProcessImage = bvalue;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("검사 결과 데이터 저장 유무를 {0}로 변경되었습니다.", _systemParameters._saveResultVisionProcessImage.ToString()));
            }
            else if (currentRow == rowSaveResultStatistics)
            {
                bool bvalue = false;
                bvalue = Convert.ToBoolean(rowSaveResultStatistics.Properties.Value);

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._saveResultStatistics = bvalue;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("통계 데이터 저장 유무를 {0}로 변경되었습니다.", _systemParameters._saveResultStatistics.ToString()));
            }
            else if (currentRow == rowSystemUseLanguage)
            {
                bool bvalue = false;
                bvalue = Convert.ToBoolean(rowSystemUseLanguage.Properties.Value);

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._SystemLanguageKoreaUse = bvalue;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("한국어 언어 사용 유무를 {0}로 변경되었습니다.", _systemParameters._SystemLanguageKoreaUse.ToString()));
                if (!_systemParameters._SystemLanguageKoreaUse)
                    MessageBox.Show("언어가 변경되었습니다.\r\n프로그램을 재시작이 필요합니다.", "알람", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("System Language changed now.\r\nMust restart program.", "Alarm", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (currentRow == rowLightCommunicationPortName)
            {
                bool IsValidate = false;
                strTemp = Convert.ToString(rowLightCommunicationPortName.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxLightCommunicationPortName.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxLightCommunicationPortName.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n통신 포트 값은 COM1~20 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLightCommunicationPortName.Properties.Value = _systemParameters._LightParams.SerialParameters.PortName;
                    vGridControlSystemParametersSecond.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._LightParams.SerialParameters.PortName = strTemp;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Light 통신 포트를 {0}로 변경되었습니다.", _systemParameters._LightParams.SerialParameters.PortName));
            }
            else if (currentRow == rowLightCommunicationBaudRate)
            {
                bool IsValidate = false;
                value = Convert.ToInt32(rowLightCommunicationBaudRate.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxLightCommunicationBaudRate.Items.Count; ++i)
                {
                    if (value == Convert.ToInt32(repositoryItemComboBoxLightCommunicationBaudRate.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nBaudRates 값은 9600, 19200, 38400, 57600, 115200 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLightCommunicationBaudRate.Properties.Value = _systemParameters._LightParams.SerialParameters.BaudRates;
                    vGridControlSystemParametersSecond.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._LightParams.SerialParameters.BaudRates = value;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Light 통신 속도를 {0}로 변경되었습니다.", _systemParameters._LightParams.SerialParameters.BaudRates.ToString()));
            }
            else if (currentRow == rowLightCommunicationStopBit)
            {
                bool IsValidate = false;
                strTemp = Convert.ToString(rowLightCommunicationStopBit.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxLightCommunicationStopBit.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxLightCommunicationStopBit.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n정지 비트 값은 None, One, Two, One5 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLightCommunicationStopBit.Properties.Value = Enum.GetName(typeof(StopBits), (StopBits)_systemParameters._LightParams.SerialParameters.StopBits);
                    vGridControlSystemParametersSecond.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._LightParams.SerialParameters.StopBits = (StopBits)Enum.Parse(typeof(StopBits), strTemp);
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Light 통신 정지 비트를 {0}로 변경되었습니다.", _systemParameters._LightParams.SerialParameters.StopBits.ToString()));
            }
            else if (currentRow == rowLightCommunicationDataBit)
            {
                bool IsValidate = false;
                value = Convert.ToInt32(rowLightCommunicationDataBit.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxLightCommunicationDataBit.Items.Count; ++i)
                {
                    if (value == Convert.ToInt32(repositoryItemComboBoxLightCommunicationDataBit.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nDataBits 값은 4, 5, 6, 7, 8 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLightCommunicationDataBit.Properties.Value = _systemParameters._LightParams.SerialParameters.DataBits;
                    vGridControlSystemParametersSecond.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._LightParams.SerialParameters.DataBits = value;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Light 통신 데이터 비트를 {0}로 변경되었습니다.", _systemParameters._LightParams.SerialParameters.DataBits.ToString()));
            }
            else if (currentRow == rowLightCommunicationFlowControl)
            {
                bool IsValidate = false;
                strTemp = Convert.ToString(rowLightCommunicationFlowControl.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxLightCommunicationFlowControl.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxLightCommunicationFlowControl.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n흐름 제어 값은 None, XonXoff, RequestToSend, RequestToSendXonXoff 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLightCommunicationFlowControl.Properties.Value = Enum.GetName(typeof(Handshake), (Handshake)_systemParameters._LightParams.SerialParameters.Handshake);
                    vGridControlSystemParametersSecond.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;

                _systemParameters._LightParams.SerialParameters.Handshake = (Handshake)Enum.Parse(typeof(Handshake), strTemp);
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Light 통신 흐름 제어를 {0}로 변경되었습니다.", _systemParameters._LightParams.SerialParameters.Handshake.ToString()));
            }
            else if (currentRow == rowLightCommunicationParity)
            {
                bool IsValidate = false;
                strTemp = Convert.ToString(rowLightCommunicationParity.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxLightCommunicationParity.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxLightCommunicationParity.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n패리티 값은 None, Odd, Even, Mark, Space 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //rowPanelMeterCommunicationParity.Properties.Value = _systemParameters._panelmeterParams.SerialParameters.Parity;
                    rowLightCommunicationParity.Properties.Value = Enum.GetName(typeof(Parity), (Parity)_systemParameters._LightParams.SerialParameters.Parity);
                    vGridControlSystemParametersSecond.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;

                _systemParameters._LightParams.SerialParameters.Parity = (Parity)Enum.Parse(typeof(Parity), strTemp);
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Light 통신 parity를 {0}로 변경되었습니다.", _systemParameters._LightParams.SerialParameters.Parity.ToString()));
            }
            else if (currentRow == rowFeederGearRatio)
            {                
                fValue = Convert.ToSingle(rowFeederGearRatio.Properties.Value);
                if (fValue <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nFeeder 기어비 값은 1보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowFeederGearRatio.Properties.Value = _systemParameters._FeederParams.FeederGearRatio;
                    vGridControlSystemParametersSecond.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;

                _systemParameters._FeederParams.FeederGearRatio = (double)fValue;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Feeder 기어비를 {0}로 변경되었습니다.", _systemParameters._FeederParams.FeederGearRatio.ToString()));
            }
            else if (currentRow == rowFeederDiameter)
            {
                fValue = Convert.ToSingle(rowFeederDiameter.Properties.Value);
                if (fValue <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nFeeder 풀리지름 값은 1보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowFeederDiameter.Properties.Value = _systemParameters._FeederParams.FeederDiameter;
                    vGridControlSystemParametersSecond.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;

                _systemParameters._FeederParams.FeederDiameter = (double)fValue;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Feeder 풀리지름이 {0}로 변경되었습니다.", _systemParameters._FeederParams.FeederDiameter.ToString()));
            }
            else if (currentRow == rowFeederResolution)
            {
                fValue = Convert.ToSingle(rowFeederResolution.Properties.Value);
                if (fValue <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nFeeder 분해능 값은 1보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowFeederResolution.Properties.Value = _systemParameters._FeederParams.FeederResolution;
                    vGridControlSystemParametersSecond.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;

                _systemParameters._FeederParams.FeederResolution = (double)fValue;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Feeder 분해능이 {0}로 변경되었습니다.", _systemParameters._FeederParams.FeederResolution.ToString()));
            }
            else if (currentRow == rowFeederVelocity)
            {
                fValue = Convert.ToSingle(rowFeederVelocity.Properties.Value);
                if (fValue <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nFeeder 이동속도 값은 1보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowFeederVelocity.Properties.Value = _systemParameters._FeederParams.FeederMoveVelocity;
                    vGridControlSystemParametersSecond.Refresh();
                    return;
                }
                simpleButtonSystemFileSave.Enabled = true;

                _systemParameters._FeederParams.FeederMoveVelocity = (double)fValue;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Feeder 이동속도가 {0}로 변경되었습니다.", _systemParameters._FeederParams.FeederMoveVelocity.ToString()));
            }
            else if (currentRow == rowFeederCommunicationPortName)
            {
                bool IsValidate = false;
                strTemp = Convert.ToString(rowFeederCommunicationPortName.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxFeederCommunicationPortName.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxFeederCommunicationPortName.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n통신 포트 값은 COM1~20 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowFeederCommunicationPortName.Properties.Value = _systemParameters._FeederParams.SerialParameters.PortName;
                    vGridControlSystemParametersSecond.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._FeederParams.SerialParameters.PortName = strTemp;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Feeder 통신 포트를 {0}로 변경되었습니다.", _systemParameters._FeederParams.SerialParameters.PortName));
            }
            else if (currentRow == rowFeederCommunicationBaudRate)
            {
                bool IsValidate = false;
                value = Convert.ToInt32(rowFeederCommunicationBaudRate.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxFeederCommunicationBaudRate.Items.Count; ++i)
                {
                    if (value == Convert.ToInt32(repositoryItemComboBoxFeederCommunicationBaudRate.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nBaudRates 값은 9600, 19200, 38400, 57600, 115200 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowFeederCommunicationBaudRate.Properties.Value = _systemParameters._FeederParams.SerialParameters.BaudRates;
                    vGridControlSystemParametersSecond.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._FeederParams.SerialParameters.BaudRates = value;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Feeder 통신 속도를 {0}로 변경되었습니다.", _systemParameters._FeederParams.SerialParameters.BaudRates.ToString()));
            }
            else if (currentRow == rowFeederCommunicationStopBit)
            {
                bool IsValidate = false;
                strTemp = Convert.ToString(rowFeederCommunicationStopBit.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxFeederCommunicationStopBit.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxFeederCommunicationStopBit.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n정지 비트 값은 None, One, Two, One5 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowFeederCommunicationStopBit.Properties.Value = Enum.GetName(typeof(StopBits), (StopBits)_systemParameters._FeederParams.SerialParameters.StopBits);
                    vGridControlSystemParametersSecond.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._FeederParams.SerialParameters.StopBits = (StopBits)Enum.Parse(typeof(StopBits), strTemp);
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Feeder 통신 정지 비트를 {0}로 변경되었습니다.", _systemParameters._FeederParams.SerialParameters.StopBits.ToString()));
            }
            else if (currentRow == rowFeederCommunicationDataBit)
            {
                bool IsValidate = false;
                value = Convert.ToInt32(rowFeederCommunicationDataBit.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxFeederCommunicationDataBit.Items.Count; ++i)
                {
                    if (value == Convert.ToInt32(repositoryItemComboBoxFeederCommunicationDataBit.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nDataBits 값은 4, 5, 6, 7, 8 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowFeederCommunicationDataBit.Properties.Value = _systemParameters._FeederParams.SerialParameters.DataBits;
                    vGridControlSystemParametersSecond.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._FeederParams.SerialParameters.DataBits = value;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Feeder 통신 데이터 비트를 {0}로 변경되었습니다.", _systemParameters._FeederParams.SerialParameters.DataBits.ToString()));
            }
            else if (currentRow == rowFeederCommunicationFlowControl)
            {
                bool IsValidate = false;
                strTemp = Convert.ToString(rowFeederCommunicationFlowControl.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxFeederCommunicationFlowControl.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxFeederCommunicationFlowControl.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n흐름 제어 값은 None, XonXoff, RequestToSend, RequestToSendXonXoff 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowFeederCommunicationFlowControl.Properties.Value = Enum.GetName(typeof(Handshake), (Handshake)_systemParameters._FeederParams.SerialParameters.Handshake);
                    vGridControlSystemParametersSecond.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;

                _systemParameters._FeederParams.SerialParameters.Handshake = (Handshake)Enum.Parse(typeof(Handshake), strTemp);
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Feeder 통신 흐름 제어를 {0}로 변경되었습니다.", _systemParameters._FeederParams.SerialParameters.Handshake.ToString()));
            }
            else if (currentRow == rowFeederCommunicationParity)
            {
                bool IsValidate = false;
                strTemp = Convert.ToString(rowFeederCommunicationParity.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxFeederCommunicationParity.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxFeederCommunicationParity.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n패리티 값은 None, Odd, Even, Mark, Space 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //rowPanelMeterCommunicationParity.Properties.Value = _systemParameters._panelmeterParams.SerialParameters.Parity;
                    rowFeederCommunicationParity.Properties.Value = Enum.GetName(typeof(Parity), (Parity)_systemParameters._FeederParams.SerialParameters.Parity);
                    vGridControlSystemParametersSecond.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;

                _systemParameters._FeederParams.SerialParameters.Parity = (Parity)Enum.Parse(typeof(Parity), strTemp);
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Feeder 통신 parity를 {0}로 변경되었습니다.", _systemParameters._FeederParams.SerialParameters.Parity.ToString()));
            }
            else if (currentRow == rowFeederCommunicationIDNumber)
            {                
                value = Convert.ToInt32(rowFeederCommunicationIDNumber.Properties.Value);

                if (value <= 0)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n국번ID 값은 1보다 큰 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                    rowFeederCommunicationIDNumber.Properties.Value = _systemParameters._FeederParams.FeederCommunicationID;
                    vGridControlSystemParametersSecond.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._FeederParams.FeederCommunicationID = value;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Feeder 통신 ID를 {0}로 변경되었습니다.", _systemParameters._FeederParams.FeederCommunicationID.ToString()));
            }
            else if (currentRow == rowLaserCommunicationPortName)
            {
                bool IsValidate = false;
                strTemp = Convert.ToString(rowLaserCommunicationPortName.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxLaserCommunicationPortName.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxLaserCommunicationPortName.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n통신 포트 값은 COM1~20 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLaserCommunicationPortName.Properties.Value = _systemParameters._LaserParams.SerialParameters.PortName;
                    vGridControlSystemParametersSecond.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._LaserParams.SerialParameters.PortName = strTemp;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Laser 통신 포트를 {0}로 변경되었습니다.", _systemParameters._LaserParams.SerialParameters.PortName));
            }
            else if (currentRow == rowLaserCommunicationBaudRate)
            {
                bool IsValidate = false;
                value = Convert.ToInt32(rowLaserCommunicationBaudRate.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxLaserCommunicationBaudRate.Items.Count; ++i)
                {
                    if (value == Convert.ToInt32(repositoryItemComboBoxLaserCommunicationBaudRate.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nBaudRates 값은 9600, 19200, 38400, 57600, 115200 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLaserCommunicationBaudRate.Properties.Value = _systemParameters._LaserParams.SerialParameters.BaudRates;
                    vGridControlSystemParametersSecond.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._LaserParams.SerialParameters.BaudRates = value;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Laser 통신 속도를 {0}로 변경되었습니다.", _systemParameters._LaserParams.SerialParameters.BaudRates.ToString()));
            }
            else if (currentRow == rowLaserCommunicationStopBit)
            {
                bool IsValidate = false;
                strTemp = Convert.ToString(rowLaserCommunicationStopBit.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxLaserCommunicationStopBit.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxLaserCommunicationStopBit.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n정지 비트 값은 None, One, Two, One5 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLaserCommunicationStopBit.Properties.Value = Enum.GetName(typeof(StopBits), (StopBits)_systemParameters._LaserParams.SerialParameters.StopBits);
                    vGridControlSystemParametersSecond.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._LaserParams.SerialParameters.StopBits = (StopBits)Enum.Parse(typeof(StopBits), strTemp);
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Laser 통신 정지 비트를 {0}로 변경되었습니다.", _systemParameters._LaserParams.SerialParameters.StopBits.ToString()));
            }
            else if (currentRow == rowLaserCommunicationDataBit)
            {
                bool IsValidate = false;
                value = Convert.ToInt32(rowLaserCommunicationDataBit.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxLaserCommunicationDataBit.Items.Count; ++i)
                {
                    if (value == Convert.ToInt32(repositoryItemComboBoxLaserCommunicationDataBit.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\nDataBits 값은 4, 5, 6, 7, 8 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLaserCommunicationDataBit.Properties.Value = _systemParameters._LaserParams.SerialParameters.DataBits;
                    vGridControlSystemParametersSecond.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;
                _systemParameters._LaserParams.SerialParameters.DataBits = value;
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Laser 통신 데이터 비트를 {0}로 변경되었습니다.", _systemParameters._LaserParams.SerialParameters.DataBits.ToString()));
            }
            else if (currentRow == rowLaserCommunicationFlowControl)
            {
                bool IsValidate = false;
                strTemp = Convert.ToString(rowLaserCommunicationFlowControl.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxLaserCommunicationFlowControl.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxLaserCommunicationFlowControl.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n흐름 제어 값은 None, XonXoff, RequestToSend, RequestToSendXonXoff 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLaserCommunicationFlowControl.Properties.Value = Enum.GetName(typeof(Handshake), (Handshake)_systemParameters._LaserParams.SerialParameters.Handshake);
                    vGridControlSystemParametersSecond.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;

                _systemParameters._LaserParams.SerialParameters.Handshake = (Handshake)Enum.Parse(typeof(Handshake), strTemp);
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Laser 통신 흐름 제어를 {0}로 변경되었습니다.", _systemParameters._LaserParams.SerialParameters.Handshake.ToString()));
            }
            else if (currentRow == rowLaserCommunicationParity)
            {
                bool IsValidate = false;
                strTemp = Convert.ToString(rowLaserCommunicationParity.Properties.Value);

                for (int i = 0; i < repositoryItemComboBoxLaserCommunicationParity.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxLaserCommunicationParity.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n패리티 값은 None, Odd, Even, Mark, Space 중 하나입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //rowPanelMeterCommunicationParity.Properties.Value = _systemParameters._panelmeterParams.SerialParameters.Parity;
                    rowLaserCommunicationParity.Properties.Value = Enum.GetName(typeof(Parity), (Parity)_systemParameters._LaserParams.SerialParameters.Parity);
                    vGridControlSystemParametersSecond.Refresh();
                    return;
                }

                simpleButtonSystemFileSave.Enabled = true;

                _systemParameters._LaserParams.SerialParameters.Parity = (Parity)Enum.Parse(typeof(Parity), strTemp);
                _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("Laser 통신 parity를 {0}로 변경되었습니다.", _systemParameters._LaserParams.SerialParameters.Parity.ToString()));
            }
        }

        private void simpleButtonSystemFileSave_Click(object sender, EventArgs e)
        {
            string strSavePath = string.Format(@"{0}\{1}", SystemDirectoryParams.SystemFolderPath, SystemDirectoryParams.SystemFileName);

            if (MessageBox.Show(string.Format("시스템 파일을 저장하시겠습니까?\r\n저장 위치:{0}", strSavePath), "시스템파일 저장", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                try
                {
                    if (!Directory.Exists(SystemDirectoryParams.SystemFolderPath))
                    {
                        Directory.CreateDirectory(strSavePath);
                    }

                    // Recipe File
                    RecipeFileIO.WriteSystemFile(_systemParameters, strSavePath);
                    _log.WriteLog(LogLevel.Info, LogClass.SystemEditor.ToString(), string.Format("시스템 파라미터가 저장되었습니다."));
                }
                catch (Exception ex)
                {
                    ;
                }
            }
            else
            {
                this.DialogResult = DialogResult.None;
            }
        }
    }
}
