using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IniParser;
using IniParser.Model;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using LogLibrary;
using System.Collections;

namespace RecipeManager
{
    public class RecipeFileIO
    {
        private Log _log = new Log();        
        static public string[] SerialPortName = new string[] { "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "COM10",
                                                        "COM11", "COM12", "COM13", "COM14", "COM15", "COM16", "COM17", "COM18", "COM19", "COM20" };

        static public string[] SerialBaudRates = new string[] { "9600", "19200", "38400", "57600", "115200" };
        static public string[] SerialDataBits = new string[] { "4", "5", "6", "7", "8" };
        static public string[] SerialParity = new string[] { "None", "Odd", "Even"};
        //static public string[] SerialEncoding = new string[] { "ASCII", "Unicode", "UTF8", "UTF32" };

        static string[] SystemParamSections = new string[] { "Camera Parameters","Calibration Parameters", "Motion Parameters", "AiC Parameters", "RemoteIO Parameters", "AMDS Parameters","Reflector Offset" ,"Save Results","Language" };
        static string[] WorkParamSections = new string[] { "Recipe Information", "Product Infomation", "Distance Inspection", "Inspection Positions" };

        static public string[] ProductSeries = new string[] {"BTS", "BTF", "BJ", "BJP", "BEN", "BPS" };        
        static public string[] ProductType = new string[] { "MirrorReflective", "FixedDistanceReflective", "DiffuseReflective", "BGSReflective", "LimitedReflective", "TransmitLight", "ReceiveLight" };
        static public string[] ProductOperationMode = new string[] { "LightON", "DarkON" };
        static public string[] ProductOutputType = new string[] { "NPN", "PNP" };
        static public string[] ProductDetectMeterial = new string[] { "None", "Mirror", "WhitePaper", "BlackPaper", "Glass" };//{ "NONE", "Mirror","WhitePage","BlackPage","Glass" };

        static public string[] InspectionPositionType = new string[] { "Inspection", "MaxDistance", "MinOrigin" ,"OpticalBase" ,"Ready"};
        static public string[] TransitionCoordinate = new string[] { "1","-1" };

        static public void ReadRecipeFile(WorkParams workParam, string strFilePath)
        {
            FileIniDataParser parser = new FileIniDataParser();
            IniData recipeData = new IniData();

            recipeData = parser.ReadFile(strFilePath);

            workParam.RecipeName = Convert.ToString(recipeData[WorkParamSections[0]]["RecipeName"]);
            workParam.RecipeCreateTime = Convert.ToDateTime(recipeData[WorkParamSections[0]]["RecipeCreateTime"]);
            workParam.RecipeCreatorName = Convert.ToString(recipeData[WorkParamSections[0]]["RecipeCreatorName"]);
                        
            workParam._ProductSeries = Convert.ToInt32(recipeData[WorkParamSections[1]]["ProductSeries"]);
            workParam._ProductModelName = Convert.ToString(recipeData[WorkParamSections[1]]["ProductModelName"]);
            workParam._ProductType = Convert.ToInt32(recipeData[WorkParamSections[1]]["ProductType"]);            
            workParam._ProductDistance = Convert.ToSingle(recipeData[WorkParamSections[1]]["ProductDistance"]);
            workParam._ProductOperatingMdoe = Convert.ToInt32(recipeData[WorkParamSections[1]]["ProductOperatingMode"]);
            workParam._ProductOutputType = Convert.ToInt32(recipeData[WorkParamSections[1]]["ProductOutputType"]);            
            workParam._ProductDetectMerterial = Convert.ToInt32(recipeData[WorkParamSections[1]]["ProductDetecMerterial"]);
            workParam._ProductDistanceMargin = Convert.ToSingle(recipeData[WorkParamSections[1]]["ProductDistaneMargin"]);

            workParam._InspectionShortDistanceEnable = Convert.ToBoolean(recipeData[WorkParamSections[2]]["InspectionShortDistanceEnable"]);
            workParam._InspectionPerformanceEnable = Convert.ToBoolean(recipeData[WorkParamSections[2]]["InspectionPerformanceEnable"]);
            workParam._InspectionShortDistance= Convert.ToDouble(recipeData[WorkParamSections[2]]["InspectionShortDistance"]);
            workParam._InspectionSignalDelaytime = Convert.ToInt32(recipeData[WorkParamSections[2]]["InspectionSignalWaitTime"]);
            workParam._InspectionDistance = Convert.ToDouble(recipeData[WorkParamSections[2]]["InspectionDistance"]);
            workParam._InspectionAngle = Convert.ToDouble(recipeData[WorkParamSections[2]]["InspectionAngle"]);
            workParam._InspectionAngleIncrement = Convert.ToDouble(recipeData[WorkParamSections[2]]["InspectionAngleIncrement"]);
            workParam._InspectionHeightPosition = Convert.ToDouble(recipeData[WorkParamSections[2]]["InspectionHeight"]);
            workParam._InspectionHeightIncrement = Convert.ToDouble(recipeData[WorkParamSections[2]]["InspectionHeightIncrement"]);
            workParam._InspectionMaxDistanceUseEnable = Convert.ToBoolean(recipeData[WorkParamSections[2]]["InspectionMaxDistanceEnable"]);

            workParam._WorkPositionsCount = Convert.ToInt32(recipeData[WorkParamSections[3]]["InspectionPositionCount"]);

            workParam.InspectionPositions.Clear();

            for (int i = 0; i < workParam._WorkPositionsCount; ++i)
            {
                InspectionPosition pos = new InspectionPosition();

                pos.Index = i + 1;
                pos.ePositionType = (INSPECTION_POSITION_MODE)Convert.ToInt32(recipeData[WorkParamSections[3]][string.Format("PositionType{0}", i)]);                
                pos.PositionX = Convert.ToSingle(recipeData[WorkParamSections[3]][string.Format("X{0}", i)]);
                pos.PositionZ = Convert.ToSingle(recipeData[WorkParamSections[3]][string.Format("Z{0}", i)]);                
                pos.PositionRz = Convert.ToSingle(recipeData[WorkParamSections[3]][string.Format("R{0}", i)]);
                pos.PositionNDFilter = Convert.ToSingle(recipeData[WorkParamSections[3]][string.Format("Theta{0}", i)]);
                workParam.InspectionPositions.Add(pos);
            }

            parser = null;
            recipeData = null;
        }
        static public void WriteRecipeFile(WorkParams workParam, string strFolderPath, string strFilePath)
        {
            FileIniDataParser parser = new FileIniDataParser();
            IniData recipeData = new IniData();

            string strSaveFilePath = string.Empty;

            recipeData.Sections.AddSection(WorkParamSections[0]);
            recipeData[WorkParamSections[0]].AddKey("RecipeName", workParam.RecipeName);
            recipeData[WorkParamSections[0]].AddKey("RecipeCreateTime", workParam.RecipeCreateTime.ToString());
            recipeData[WorkParamSections[0]].AddKey("RecipeCreatorName", workParam.RecipeCreatorName);

            recipeData.Sections.AddSection(WorkParamSections[1]);
            recipeData[WorkParamSections[1]].AddKey("ProductSeries", workParam._ProductSeries.ToString());
            recipeData[WorkParamSections[1]].AddKey("ProductModelName", workParam._ProductModelName);
            recipeData[WorkParamSections[1]].AddKey("ProductType", workParam._ProductType.ToString());
            recipeData[WorkParamSections[1]].AddKey("ProductDistance", workParam._ProductDistance.ToString());
            recipeData[WorkParamSections[1]].AddKey("ProductOperatingMode", workParam._ProductOperatingMdoe.ToString());
            recipeData[WorkParamSections[1]].AddKey("ProductOutputType", workParam._ProductOutputType.ToString());
            recipeData[WorkParamSections[1]].AddKey("ProductDetecMerterial", workParam._ProductDetectMerterial.ToString());
            recipeData[WorkParamSections[1]].AddKey("ProductDistaneMargin", workParam._ProductDistanceMargin.ToString());

            recipeData.Sections.AddSection(WorkParamSections[2]);
            recipeData[WorkParamSections[2]].AddKey("InspectionShortDistanceEnable", workParam._InspectionShortDistanceEnable.ToString());
            recipeData[WorkParamSections[2]].AddKey("InspectionPerformanceEnable", workParam._InspectionPerformanceEnable.ToString());            
            recipeData[WorkParamSections[2]].AddKey("InspectionShortDistance", workParam._InspectionShortDistance.ToString());
            recipeData[WorkParamSections[2]].AddKey("InspectionSignalWaitTime", workParam._InspectionSignalDelaytime.ToString());            
            recipeData[WorkParamSections[2]].AddKey("InspectionDistance", workParam._InspectionDistance.ToString());
            recipeData[WorkParamSections[2]].AddKey("InspectionAngle", workParam._InspectionAngle.ToString());
            recipeData[WorkParamSections[2]].AddKey("InspectionAngleIncrement", workParam._InspectionAngleIncrement.ToString());
            recipeData[WorkParamSections[2]].AddKey("InspectionHeight", workParam._InspectionHeightPosition.ToString());
            recipeData[WorkParamSections[2]].AddKey("InspectionHeightIncrement", workParam._InspectionHeightIncrement.ToString());
            recipeData[WorkParamSections[2]].AddKey("InspectionMaxDistanceEnable", workParam._InspectionMaxDistanceUseEnable.ToString());

            recipeData.Sections.AddSection(WorkParamSections[3]);
            recipeData[WorkParamSections[3]].AddKey("InspectionPositionCount", workParam.InspectionPositions.Count.ToString());

            for (int i = 0; i < workParam.InspectionPositions.Count; ++i)
            {
                int inspectionMode = (int)workParam.InspectionPositions[i].ePositionType;

                recipeData[WorkParamSections[3]].AddKey(string.Format("PositionType{0}", i), inspectionMode.ToString());
                recipeData[WorkParamSections[3]].AddKey(string.Format("X{0}", i), workParam.InspectionPositions[i].PositionX.ToString());
                recipeData[WorkParamSections[3]].AddKey(string.Format("Z{0}", i), workParam.InspectionPositions[i].PositionZ.ToString());
                recipeData[WorkParamSections[3]].AddKey(string.Format("R{0}", i), workParam.InspectionPositions[i].PositionRz.ToString());
                recipeData[WorkParamSections[3]].AddKey(string.Format("Theta{0}", i), workParam.InspectionPositions[i].PositionNDFilter.ToString());
            }

            parser.WriteFile(strFilePath, recipeData);

            parser = null;
            recipeData = null;
        }
        static public void ReadSystemFile(SystemParams systemParam, string strFilePath)
        {
            FileIniDataParser parser = new FileIniDataParser();
            IniData systemData = new IniData();

            systemData = parser.ReadFile(strFilePath);

            // Camera Parameters
            systemParam._cameraParams.CheckUsingCamera = Convert.ToBoolean(systemData[SystemParamSections[0]]["CameraUse"]);
            systemParam._cameraParams.FriendlyName = Convert.ToString(systemData[SystemParamSections[0]]["CameraFriendlyName"]);
            systemParam._cameraParams.HResolution = Convert.ToInt32(systemData[SystemParamSections[0]]["CameraHResolution"]);
            systemParam._cameraParams.VResolution = Convert.ToInt32(systemData[SystemParamSections[0]]["CameraVResolution"]);
            systemParam._cameraParams.FrameRate = Convert.ToInt32(systemData[SystemParamSections[0]]["CameraFrameRate"]);
            systemParam._cameraParams.ExposureTime = Convert.ToInt32(systemData[SystemParamSections[0]]["CameraExposureTime"]);
            systemParam._cameraParams.Gain = Convert.ToInt32(systemData[SystemParamSections[0]]["CameraGain"]);
            systemParam._cameraParams.OnePixelResolution = Convert.ToSingle(systemData[SystemParamSections[0]]["CameraOnePixelResolution"]);
            systemParam._cameraParams.ImageSensorHSize = Convert.ToSingle(systemData[SystemParamSections[0]]["CameraImageSensorHSize"]);
            systemParam._cameraParams.ImageSensorVSize = Convert.ToSingle(systemData[SystemParamSections[0]]["CameraImageSensorVSize"]);
            systemParam._cameraParams.LensFocusLength = Convert.ToSingle(systemData[SystemParamSections[0]]["CameraLensFocusLength"]);

            // Calibration Parameters
            systemParam._calibrationParams._CoordinateSwitchEnable = Convert.ToBoolean(systemData[SystemParamSections[1]]["CoordinateSwitchEnable"]);
            systemParam._calibrationParams._imagetoSystemXcoordi = Convert.ToSingle(systemData[SystemParamSections[1]]["ImageToSystemCoordinateX"]);
            systemParam._calibrationParams._imagetoSystemYcoordi = Convert.ToSingle(systemData[SystemParamSections[1]]["ImageToSystemCoordinateY"]);
            systemParam._calibrationParams._CoordinateCalibrationActive = Convert.ToBoolean(systemData[SystemParamSections[1]]["CoordinateCalibrationActive"]);
            systemParam._calibrationParams._X_reference_Distance = Convert.ToSingle(systemData[SystemParamSections[1]]["ReferenceXdistance"]);
            systemParam._calibrationParams._X_DeltaX = Convert.ToSingle(systemData[SystemParamSections[1]]["X_DeltaX"]);
            systemParam._calibrationParams._X_DeltaZ = Convert.ToSingle(systemData[SystemParamSections[1]]["X_DeltaZ"]);
            systemParam._calibrationParams._Z_reference_Distance = Convert.ToSingle(systemData[SystemParamSections[1]]["ReferenceZdistance"]);
            systemParam._calibrationParams._Z_DeltaX = Convert.ToSingle(systemData[SystemParamSections[1]]["Z_DeltaX"]);
            systemParam._calibrationParams._Z_DeltaZ = Convert.ToSingle(systemData[SystemParamSections[1]]["Z_DeltaZ"]);
            systemParam._calibrationParams._Y_reference_Distance = Convert.ToSingle(systemData[SystemParamSections[1]]["ReferenceYdistance"]);
            systemParam._calibrationParams._Y_DeltaX = Convert.ToSingle(systemData[SystemParamSections[1]]["Y_DeltaX"]);
            systemParam._calibrationParams._Y_DeltaZ = Convert.ToSingle(systemData[SystemParamSections[1]]["X_DeltaZ"]);

            // Motion Parameters
            systemParam._motionParams.MenualMoveVelocity = Convert.ToSingle(systemData[SystemParamSections[2]]["MenualMoveVelocity"]);
            systemParam._motionParams.MoveVelocity = Convert.ToSingle(systemData[SystemParamSections[2]]["MoveVelocity"]);
            systemParam._motionParams.MoveAcceleration = Convert.ToSingle(systemData[SystemParamSections[2]]["MoveAcceleration"]);
            systemParam._motionParams.OneTurnResolutionX  = Convert.ToInt32(systemData[SystemParamSections[2]]["OneTurnResolutionX"]);
            systemParam._motionParams.OneTurnResolutionY = Convert.ToInt32(systemData[SystemParamSections[2]]["OneTurnResolutionY"]);
            systemParam._motionParams.OneTurnResolutionZ = Convert.ToInt32(systemData[SystemParamSections[2]]["OneTurnResolutionZ"]);
            systemParam._motionParams.OneTurnResolutionR = Convert.ToInt32(systemData[SystemParamSections[2]]["OneTurnResolutionR"]);
            systemParam._motionParams.GearRatioX = Convert.ToSingle(systemData[SystemParamSections[2]]["GearRatioX"]);
            systemParam._motionParams.GearRatioY = Convert.ToSingle(systemData[SystemParamSections[2]]["GearRatioY"]);
            systemParam._motionParams.GearRatioZ = Convert.ToSingle(systemData[SystemParamSections[2]]["GearRatioZ"]);
            systemParam._motionParams.GearRatioR = Convert.ToSingle(systemData[SystemParamSections[2]]["GearRatioR"]);
            systemParam._motionParams.BallLeadX = Convert.ToSingle(systemData[SystemParamSections[2]]["BallLeadX"]);
            systemParam._motionParams.BallLeadY = Convert.ToSingle(systemData[SystemParamSections[2]]["BallLeadY"]);
            systemParam._motionParams.BallLeadZ = Convert.ToSingle(systemData[SystemParamSections[2]]["BallLeadZ"]);
            systemParam._motionParams.BallLeadR = Convert.ToSingle(systemData[SystemParamSections[2]]["BallLeadR"]);

            // AiC Parameters
            systemParam._AiCParams.SerialParameters.PortName = Convert.ToString(systemData[SystemParamSections[3]]["SerialPortName"]);
            systemParam._AiCParams.SerialParameters.BaudRates = Convert.ToInt32(systemData[SystemParamSections[3]]["SerialBaudRates"]);
            systemParam._AiCParams.SerialParameters.DataBits = Convert.ToInt32(systemData[SystemParamSections[3]]["SerialDataBits"]);               
            systemParam._AiCParams.SerialParameters.Parity = (Parity)Enum.Parse(typeof(Parity), Convert.ToString(systemData[SystemParamSections[3]]["SerialParity"]));
            systemParam._AiCParams.SerialParameters.StopBits = (StopBits)Enum.Parse(typeof(StopBits), Convert.ToString(systemData[SystemParamSections[3]]["SerialStopBits"]));
            systemParam._AiCParams.SerialParameters.Handshake = (Handshake)Enum.Parse(typeof(Handshake), Convert.ToString(systemData[SystemParamSections[3]]["SerialHandshake"]));
            systemParam._AiCParams.ConnectedNumber = Convert.ToInt32(systemData[SystemParamSections[3]]["ConnectedDeviceNumber"]);

            if (systemParam._AiCParams.ConnectedNumber == 3)
            {
                systemParam._AiCParams.SetInitialIDs(systemParam._AiCParams.ConnectedNumber);
                AiCParams._IDs tempList;
                tempList._devicename = Convert.ToString(systemData[SystemParamSections[3]]["DeviceIDsInfomation1_Name"]);
                tempList._idNumber = Convert.ToInt32(systemData[SystemParamSections[3]]["DeviceIDsInfomation1_ID"]);
                systemParam._AiCParams.IDs[0] = tempList;
                tempList._devicename = Convert.ToString(systemData[SystemParamSections[3]]["DeviceIDsInfomation2_Name"]);
                tempList._idNumber = Convert.ToInt32(systemData[SystemParamSections[3]]["DeviceIDsInfomation2_ID"]);
                systemParam._AiCParams.IDs[1] = tempList;
                tempList._devicename = Convert.ToString(systemData[SystemParamSections[3]]["DeviceIDsInfomation3_Name"]);
                tempList._idNumber = Convert.ToInt32(systemData[SystemParamSections[3]]["DeviceIDsInfomation3_ID"]);
                systemParam._AiCParams.IDs[2] = tempList;
            }
            else if (systemParam._AiCParams.ConnectedNumber == 4)
            {
                systemParam._AiCParams.SetInitialIDs(systemParam._AiCParams.ConnectedNumber);
                AiCParams._IDs tempList;
                tempList._devicename = Convert.ToString(systemData[SystemParamSections[3]]["DeviceIDsInfomation1_Name"]);
                tempList._idNumber = Convert.ToInt32(systemData[SystemParamSections[3]]["DeviceIDsInfomation1_ID"]);
                systemParam._AiCParams.IDs[0] = tempList;
                tempList._devicename = Convert.ToString(systemData[SystemParamSections[3]]["DeviceIDsInfomation2_Name"]);
                tempList._idNumber = Convert.ToInt32(systemData[SystemParamSections[3]]["DeviceIDsInfomation2_ID"]);
                systemParam._AiCParams.IDs[1] = tempList;
                tempList._devicename = Convert.ToString(systemData[SystemParamSections[3]]["DeviceIDsInfomation3_Name"]);
                tempList._idNumber = Convert.ToInt32(systemData[SystemParamSections[3]]["DeviceIDsInfomation3_ID"]);
                systemParam._AiCParams.IDs[2] = tempList;
                tempList._devicename = Convert.ToString(systemData[SystemParamSections[3]]["DeviceIDsInfomation4_Name"]);
                tempList._idNumber = Convert.ToInt32(systemData[SystemParamSections[3]]["DeviceIDsInfomation4_ID"]);
                systemParam._AiCParams.IDs[3] = tempList;
            }
            else
            {
                systemParam._AiCParams.SetInitialIDs(1);
                AiCParams._IDs tempList;
                tempList._devicename = Convert.ToString(systemData[SystemParamSections[3]]["DeviceIDsInfomation1_Name"]);
                tempList._idNumber = Convert.ToInt32(systemData[SystemParamSections[3]]["DeviceIDsInfomation1_ID"]);
                systemParam._AiCParams.IDs[0] = tempList;
            }

            // Remote IO Parameters
            systemParam._remoteIOParams.SerialParameters.PortName = Convert.ToString(systemData[SystemParamSections[4]]["SerialPortName"]);
            systemParam._remoteIOParams.SerialParameters.BaudRates = Convert.ToInt32(systemData[SystemParamSections[4]]["SerialBaudRates"]);
            systemParam._remoteIOParams.SerialParameters.DataBits = Convert.ToInt32(systemData[SystemParamSections[4]]["SerialDataBits"]);
            systemParam._remoteIOParams.SerialParameters.Parity = (Parity)Enum.Parse(typeof(Parity), Convert.ToString(systemData[SystemParamSections[4]]["SerialParity"]));
            systemParam._remoteIOParams.SerialParameters.StopBits = (StopBits)Enum.Parse(typeof(StopBits), Convert.ToString(systemData[SystemParamSections[4]]["SerialStopBits"]));
            systemParam._remoteIOParams.SerialParameters.Handshake = (Handshake)Enum.Parse(typeof(Handshake), Convert.ToString(systemData[SystemParamSections[4]]["SerialHandshake"]));
            systemParam._remoteIOParams.ConnectedNumber = Convert.ToInt32(systemData[SystemParamSections[4]]["ConnectedDeviceNumber"]);

            if (systemParam._remoteIOParams.ConnectedNumber != 0)
            {
                if (systemParam._remoteIOParams.ConnectedNumber == 1)
                {
                    systemParam._remoteIOParams.SetInitialIDs(systemParam._remoteIOParams.ConnectedNumber);
                    RemoteIOParams._IDs tempRemoteList;
                    tempRemoteList._devicename = Convert.ToString(systemData[SystemParamSections[4]]["DeviceIDsInfomation1_Name"]);
                    tempRemoteList._idNumber = Convert.ToInt32(systemData[SystemParamSections[4]]["DeviceIDsInfomation1_ID"]);
                    systemParam._remoteIOParams.IDs[0] = tempRemoteList;
                }
                else if (systemParam._remoteIOParams.ConnectedNumber == 2)
                {
                    systemParam._remoteIOParams.SetInitialIDs(systemParam._remoteIOParams.ConnectedNumber);
                    RemoteIOParams._IDs tempRemoteList;
                    tempRemoteList._devicename = Convert.ToString(systemData[SystemParamSections[4]]["DeviceIDsInfomation1_Name"]);
                    tempRemoteList._idNumber = Convert.ToInt32(systemData[SystemParamSections[4]]["DeviceIDsInfomation1_ID"]);
                    systemParam._remoteIOParams.IDs[0] = tempRemoteList;
                    tempRemoteList._devicename = Convert.ToString(systemData[SystemParamSections[4]]["DeviceIDsInfomation2_Name"]);
                    tempRemoteList._idNumber = Convert.ToInt32(systemData[SystemParamSections[4]]["DeviceIDsInfomation2_ID"]);
                    systemParam._remoteIOParams.IDs[1] = tempRemoteList;
                }
            }
            else
            {
                systemParam._remoteIOParams.ConnectedNumber = 1;
                systemParam._remoteIOParams.SetInitialIDs(systemParam._AiCParams.ConnectedNumber);
                RemoteIOParams._IDs tempRemoteList;
                tempRemoteList._devicename = "IO";
                tempRemoteList._idNumber = 1;
                systemParam._remoteIOParams.IDs[0] = tempRemoteList;
            }

            // ADMS Parameters
            systemParam._bJobWorkInfomationEnable = Convert.ToBoolean(systemData[SystemParamSections[5]]["JobInfomatoinEnableCheck"]);
            systemParam._admsParams._enableCheck = Convert.ToBoolean(systemData[SystemParamSections[5]]["EnableCheck"]);
            systemParam._admsParams._IpAddress = Convert.ToString(systemData[SystemParamSections[5]]["ConnectIPAddress"]);
            systemParam._admsParams._port = Convert.ToInt32(systemData[SystemParamSections[5]]["ConnectPort"]);
            systemParam._admsParams._userID = Convert.ToString(systemData[SystemParamSections[5]]["UserID"]);
            systemParam._admsParams._password = Convert.ToString(systemData[SystemParamSections[5]]["Password"]);
            systemParam._admsParams._eqpmentID = Convert.ToInt32(systemData[SystemParamSections[5]]["EquipmentID"]);
            systemParam._admsParams._dbschemaname = Convert.ToString(systemData[SystemParamSections[5]]["DBSchemaName"]);
            systemParam._admsParams._equipmentname = Convert.ToString(systemData[SystemParamSections[5]]["EquipmentDBName"]);
            systemParam._admsParams._productname = Convert.ToString(systemData[SystemParamSections[5]]["ProductName"]);

            // Inspection Reflector's Offset
            systemParam.InspectionReflectorsOffset = Convert.ToDouble(systemData[SystemParamSections[6]]["Reflector's Offset"]);
            systemParam.InspectionGlassOffset = Convert.ToDouble(systemData[SystemParamSections[6]]["Glass Offset"]);
            systemParam.InspectionProductsOffset = Convert.ToDouble(systemData[SystemParamSections[6]]["Product's Offset"]);

            // SaveResult Parameters
            systemParam._saveResultVisionProcessImage = Convert.ToBoolean(systemData[SystemParamSections[7]]["InspectionImageCheck"]);
            systemParam._saveResultStatistics = Convert.ToBoolean(systemData[SystemParamSections[7]]["SaveStatistics"]);
            // Language Parameters
            systemParam._SystemLanguageKoreaUse = Convert.ToBoolean(systemData[SystemParamSections[8]]["CheckUseKoreaLanguage"]);            

        }

        static public void WriteSystemFile(SystemParams systemParam, string strFilePath)
        {
            FileIniDataParser parser = new FileIniDataParser();
            IniData systemData = new IniData();

            // Camera Parameters
            systemData.Sections.AddSection(SystemParamSections[0]);
            systemData[SystemParamSections[0]].AddKey("CameraUse", systemParam._cameraParams.CheckUsingCamera.ToString());
            systemData[SystemParamSections[0]].AddKey("CameraFriendlyName", systemParam._cameraParams.FriendlyName);
            systemData[SystemParamSections[0]].AddKey("CameraHResolution", systemParam._cameraParams.HResolution.ToString());
            systemData[SystemParamSections[0]].AddKey("CameraVResolution", systemParam._cameraParams.VResolution.ToString());
            systemData[SystemParamSections[0]].AddKey("CameraFrameRate", systemParam._cameraParams.FrameRate.ToString());
            systemData[SystemParamSections[0]].AddKey("CameraExposureTime", systemParam._cameraParams.ExposureTime.ToString());
            systemData[SystemParamSections[0]].AddKey("CameraGain", systemParam._cameraParams.Gain.ToString());
            systemData[SystemParamSections[0]].AddKey("CameraOnePixelResolution", systemParam._cameraParams.OnePixelResolution.ToString());
            systemData[SystemParamSections[0]].AddKey("CameraImageSensorHSize", systemParam._cameraParams.ImageSensorHSize.ToString());
            systemData[SystemParamSections[0]].AddKey("CameraImageSensorVSize", systemParam._cameraParams.ImageSensorVSize.ToString());
            systemData[SystemParamSections[0]].AddKey("CameraLensFocusLength", systemParam._cameraParams.LensFocusLength.ToString());

            // Calibration Coordinate Parameters
            systemData.Sections.AddSection(SystemParamSections[1]);            
            systemData[SystemParamSections[1]].AddKey("CoordinateSwitchEnable", systemParam._calibrationParams._CoordinateSwitchEnable.ToString());
            systemData[SystemParamSections[1]].AddKey("ImageToSystemCoordinateX", systemParam._calibrationParams._imagetoSystemXcoordi.ToString());
            systemData[SystemParamSections[1]].AddKey("ImageToSystemCoordinateY", systemParam._calibrationParams._imagetoSystemYcoordi.ToString());
            systemData[SystemParamSections[1]].AddKey("CoordinateCalibrationActive", systemParam._calibrationParams._CoordinateCalibrationActive.ToString());
            systemData[SystemParamSections[1]].AddKey("ReferenceXdistance", systemParam._calibrationParams._X_reference_Distance.ToString());
            systemData[SystemParamSections[1]].AddKey("X_DeltaX", systemParam._calibrationParams._X_DeltaX.ToString());
            systemData[SystemParamSections[1]].AddKey("X_DeltaZ", systemParam._calibrationParams._X_DeltaZ.ToString());
            systemData[SystemParamSections[1]].AddKey("ReferenceZdistance", systemParam._calibrationParams._X_reference_Distance.ToString());
            systemData[SystemParamSections[1]].AddKey("Z_DeltaX", systemParam._calibrationParams._X_DeltaX.ToString());
            systemData[SystemParamSections[1]].AddKey("Z_DeltaZ", systemParam._calibrationParams._X_DeltaZ.ToString());
            systemData[SystemParamSections[1]].AddKey("ReferenceY1distance", systemParam._calibrationParams._X_reference_Distance.ToString());
            systemData[SystemParamSections[1]].AddKey("Y1_DeltaX", systemParam._calibrationParams._X_DeltaX.ToString());
            systemData[SystemParamSections[1]].AddKey("Y1_DeltaZ", systemParam._calibrationParams._X_DeltaZ.ToString());
            systemData[SystemParamSections[1]].AddKey("ReferenceY2distance", systemParam._calibrationParams._X_reference_Distance.ToString());
            systemData[SystemParamSections[1]].AddKey("Y2_DeltaX", systemParam._calibrationParams._X_DeltaX.ToString());
            systemData[SystemParamSections[1]].AddKey("Y2_DeltaZ", systemParam._calibrationParams._X_DeltaZ.ToString());

            // Motion Parameters
            systemData.Sections.AddSection(SystemParamSections[2]);
            systemData[SystemParamSections[2]].AddKey("MenualMoveVelocity", systemParam._motionParams.MenualMoveVelocity.ToString());
            systemData[SystemParamSections[2]].AddKey("MoveVelocity", systemParam._motionParams.MoveVelocity.ToString());
            systemData[SystemParamSections[2]].AddKey("MoveAcceleration", systemParam._motionParams.MoveAcceleration.ToString());
            systemData[SystemParamSections[2]].AddKey("OneTurnResolutionX", systemParam._motionParams.OneTurnResolutionX.ToString());
            systemData[SystemParamSections[2]].AddKey("OneTurnResolutionY", systemParam._motionParams.OneTurnResolutionY.ToString());
            systemData[SystemParamSections[2]].AddKey("OneTurnResolutionZ", systemParam._motionParams.OneTurnResolutionZ.ToString());
            systemData[SystemParamSections[2]].AddKey("OneTurnResolutionR", systemParam._motionParams.OneTurnResolutionR.ToString());
            systemData[SystemParamSections[2]].AddKey("GearRatioX", systemParam._motionParams.GearRatioX.ToString());
            systemData[SystemParamSections[2]].AddKey("GearRatioY", systemParam._motionParams.GearRatioY.ToString());
            systemData[SystemParamSections[2]].AddKey("GearRatioZ", systemParam._motionParams.GearRatioZ.ToString());
            systemData[SystemParamSections[2]].AddKey("GearRatioR", systemParam._motionParams.GearRatioR.ToString());
            systemData[SystemParamSections[2]].AddKey("BallLeadX", systemParam._motionParams.BallLeadX.ToString());
            systemData[SystemParamSections[2]].AddKey("BallLeadY", systemParam._motionParams.BallLeadY.ToString());
            systemData[SystemParamSections[2]].AddKey("BallLeadZ", systemParam._motionParams.BallLeadZ.ToString());
            systemData[SystemParamSections[2]].AddKey("BallLeadR", systemParam._motionParams.BallLeadR.ToString());

            // AiC Parameters
            systemData.Sections.AddSection(SystemParamSections[3]);
            systemData[SystemParamSections[3]].AddKey("SerialPortName", systemParam._AiCParams.SerialParameters.PortName);
            systemData[SystemParamSections[3]].AddKey("SerialBaudRates", systemParam._AiCParams.SerialParameters.BaudRates.ToString());
            systemData[SystemParamSections[3]].AddKey("SerialDataBits", systemParam._AiCParams.SerialParameters.DataBits.ToString());
            systemData[SystemParamSections[3]].AddKey("SerialParity", Enum.GetName(typeof(Parity), (Parity)systemParam._AiCParams.SerialParameters.Parity));
            systemData[SystemParamSections[3]].AddKey("SerialStopBits", Enum.GetName(typeof(StopBits), (StopBits)systemParam._AiCParams.SerialParameters.StopBits));
            systemData[SystemParamSections[3]].AddKey("SerialHandshake", Enum.GetName(typeof(Handshake), (Handshake)systemParam._AiCParams.SerialParameters.Handshake));
            systemData[SystemParamSections[3]].AddKey("ConnectedDeviceNumber", systemParam._AiCParams.ConnectedNumber.ToString());
            systemData[SystemParamSections[3]].AddKey("DeviceIDsInfomation1_Name", systemParam._AiCParams.IDs[0]._devicename);
            systemData[SystemParamSections[3]].AddKey("DeviceIDsInfomation1_ID", systemParam._AiCParams.IDs[0]._idNumber.ToString());
            systemData[SystemParamSections[3]].AddKey("DeviceIDsInfomation2_Name", systemParam._AiCParams.IDs[1]._devicename);
            systemData[SystemParamSections[3]].AddKey("DeviceIDsInfomation2_ID", systemParam._AiCParams.IDs[1]._idNumber.ToString());
            systemData[SystemParamSections[3]].AddKey("DeviceIDsInfomation3_Name", systemParam._AiCParams.IDs[2]._devicename);
            systemData[SystemParamSections[3]].AddKey("DeviceIDsInfomation3_ID", systemParam._AiCParams.IDs[2]._idNumber.ToString());
            systemData[SystemParamSections[3]].AddKey("DeviceIDsInfomation4_Name", systemParam._AiCParams.IDs[3]._devicename);
            systemData[SystemParamSections[3]].AddKey("DeviceIDsInfomation4_ID", systemParam._AiCParams.IDs[3]._idNumber.ToString());

            // Remote IO Parameters
            systemData.Sections.AddSection(SystemParamSections[4]);
            systemData[SystemParamSections[4]].AddKey("SerialPortName", systemParam._remoteIOParams.SerialParameters.PortName);
            systemData[SystemParamSections[4]].AddKey("SerialBaudRates", systemParam._remoteIOParams.SerialParameters.BaudRates.ToString());
            systemData[SystemParamSections[4]].AddKey("SerialDataBits", systemParam._remoteIOParams.SerialParameters.DataBits.ToString());
            systemData[SystemParamSections[4]].AddKey("SerialParity", Enum.GetName(typeof(Parity), (Parity)systemParam._remoteIOParams.SerialParameters.Parity));
            systemData[SystemParamSections[4]].AddKey("SerialStopBits", Enum.GetName(typeof(StopBits), (StopBits)systemParam._remoteIOParams.SerialParameters.StopBits));
            systemData[SystemParamSections[4]].AddKey("SerialHandshake", Enum.GetName(typeof(Handshake), (Handshake)systemParam._remoteIOParams.SerialParameters.Handshake));
            systemData[SystemParamSections[4]].AddKey("ConnectedDeviceNumber", systemParam._remoteIOParams.ConnectedNumber.ToString());
            systemData[SystemParamSections[4]].AddKey("DeviceIDsInfomation1_Name", systemParam._remoteIOParams.IDs[0]._devicename);
            systemData[SystemParamSections[4]].AddKey("DeviceIDsInfomation1_ID", systemParam._remoteIOParams.IDs[0]._idNumber.ToString());
            systemData[SystemParamSections[4]].AddKey("DeviceIDsInfomation2_Name", systemParam._remoteIOParams.IDs[1]._devicename);
            systemData[SystemParamSections[4]].AddKey("DeviceIDsInfomation2_ID", systemParam._remoteIOParams.IDs[1]._idNumber.ToString());

            // ADMS Parameters
            systemData.Sections.AddSection(SystemParamSections[5]);
            systemData[SystemParamSections[5]].AddKey("EnableCheck", systemParam._admsParams._enableCheck.ToString());
            systemData[SystemParamSections[5]].AddKey("JobInfomatoinEnableCheck", systemParam._bJobWorkInfomationEnable.ToString());            
            systemData[SystemParamSections[5]].AddKey("ConnectIPAddress", systemParam._admsParams._IpAddress);
            systemData[SystemParamSections[5]].AddKey("ConnectPort", systemParam._admsParams._port.ToString());
            systemData[SystemParamSections[5]].AddKey("UserID", systemParam._admsParams._userID);
            systemData[SystemParamSections[5]].AddKey("Password", systemParam._admsParams._password);
            systemData[SystemParamSections[5]].AddKey("EquipmentID", systemParam._admsParams._eqpmentID.ToString());
            systemData[SystemParamSections[5]].AddKey("DBSchemaName", systemParam._admsParams._dbschemaname);
            systemData[SystemParamSections[5]].AddKey("EquipmentDBName", systemParam._admsParams._equipmentname);
            systemData[SystemParamSections[5]].AddKey("ProductName", systemParam._admsParams._productname);

            // Inspection Reflector's Offset
            systemData.Sections.AddSection(SystemParamSections[6]);
            systemData[SystemParamSections[6]].AddKey("Reflector's Offset", systemParam.InspectionReflectorsOffset.ToString());
            systemData[SystemParamSections[6]].AddKey("Glass Offset", systemParam.InspectionGlassOffset.ToString());
            systemData[SystemParamSections[6]].AddKey("Product's Offset", systemParam.InspectionProductsOffset.ToString());            

            // SaveResult Parameters
            systemData.Sections.AddSection(SystemParamSections[7]);
            systemData[SystemParamSections[7]].AddKey("InspectionImageCheck", systemParam._saveResultVisionProcessImage.ToString());
            systemData[SystemParamSections[7]].AddKey("SaveStatistics", systemParam._saveResultStatistics.ToString());

            // Language Parameters
            systemData.Sections.AddSection(SystemParamSections[8]);
            systemData[SystemParamSections[8]].AddKey("CheckUseKoreaLanguage", systemParam._SystemLanguageKoreaUse.ToString());


            parser.WriteFile(strFilePath, systemData);
        }
        static public StatisticParams ReadInspectionStatisticsFile(string strFilePath, int arrayCount)
        {
            StatisticParams statistics = new StatisticParams(arrayCount);

            FileIniDataParser parser = new FileIniDataParser();
            IniData systemData = new IniData();

            systemData = parser.ReadFile(strFilePath);

            statistics.TotalCount = Convert.ToInt32(systemData["Inspection Count"]["Total"]);
            statistics.PassCount = Convert.ToInt32(systemData["Inspection Count"]["Pass"]);
            statistics.FailCount = Convert.ToInt32(systemData["Inspection Count"]["Fail"]);

            statistics.Statistics.Add(Convert.ToString(systemData["Distance"]["15mm"]));
            statistics.Statistics.Add(Convert.ToString(systemData["Distance"]["30mm"]));
            statistics.Statistics.Add(Convert.ToString(systemData["Distance"]["50mm"]));
            statistics.Statistics.Add(Convert.ToString(systemData["Distance"]["100mm"]));
            statistics.Statistics.Add(Convert.ToString(systemData["Distance"]["200mm"]));
            statistics.Statistics.Add(Convert.ToString(systemData["Distance"]["500mm"]));
            statistics.Statistics.Add(Convert.ToString(systemData["Distance"]["1M"]));
            statistics.Statistics.Add(Convert.ToString(systemData["Distance"]["2M"]));
            statistics.Statistics.Add(Convert.ToString(systemData["Distance"]["3M"]));
            statistics.Statistics.Add(Convert.ToString(systemData["Distance"]["7M"]));
            statistics.Statistics.Add(Convert.ToString(systemData["Distance"]["10M"]));
            statistics.Statistics.Add(Convert.ToString(systemData["Distance"]["15M"]));
            statistics.Statistics.Add(Convert.ToString(systemData["Distance"]["30M"]));
            statistics.Statistics.Add(Convert.ToString(systemData["Distance"]["50M"]));

            statistics.Statistics.Add(Convert.ToString(systemData["Angle"]["±2˚"]));
            statistics.Statistics.Add(Convert.ToString(systemData["Angle"]["±4˚"]));
            statistics.Statistics.Add(Convert.ToString(systemData["Angle"]["±6˚"]));
            statistics.Statistics.Add(Convert.ToString(systemData["Angle"]["±8˚"]));
            statistics.Statistics.Add(Convert.ToString(systemData["Angle"]["±10˚"]));
            statistics.Statistics.Add(Convert.ToString(systemData["Angle"]["±12˚"]));
            statistics.Statistics.Add(Convert.ToString(systemData["Angle"]["±14˚"]));
            statistics.Statistics.Add(Convert.ToString(systemData["Angle"]["±16˚"]));
            statistics.Statistics.Add(Convert.ToString(systemData["Angle"]["±18˚"]));
            statistics.Statistics.Add(Convert.ToString(systemData["Angle"]["±20˚"]));

            return statistics;
        }
        static public void WriteInspectionStatisticsFile(string strFilePath, StatisticParams statistics)
        {
            FileIniDataParser parser = new FileIniDataParser();
            IniData systemData = new IniData();

            // Camera Parameters
            systemData.Sections.AddSection("Inspection Count");
            systemData["Inspection Count"].AddKey("Total", Convert.ToString(statistics.TotalCount));
            systemData["Inspection Count"].AddKey("Pass", Convert.ToString(statistics.PassCount));
            systemData["Inspection Count"].AddKey("Fail", Convert.ToString(statistics.FailCount));

            systemData.Sections.AddSection("Statistics");
            systemData["Distance"].AddKey("15mm", Convert.ToString(statistics.Statistics[0]));
            systemData["Distance"].AddKey("30mm", Convert.ToString(statistics.Statistics[1]));
            systemData["Distance"].AddKey("50mm", Convert.ToString(statistics.Statistics[2]));
            systemData["Distance"].AddKey("100mm", Convert.ToString(statistics.Statistics[3]));
            systemData["Distance"].AddKey("200mm", Convert.ToString(statistics.Statistics[4]));
            systemData["Distance"].AddKey("500mm", Convert.ToString(statistics.Statistics[5]));
            systemData["Distance"].AddKey("1M", Convert.ToString(statistics.Statistics[6]));
            systemData["Distance"].AddKey("2M", Convert.ToString(statistics.Statistics[7]));
            systemData["Distance"].AddKey("3M", Convert.ToString(statistics.Statistics[8]));
            systemData["Distance"].AddKey("7M", Convert.ToString(statistics.Statistics[9]));
            systemData["Distance"].AddKey("10M", Convert.ToString(statistics.Statistics[10]));
            systemData["Distance"].AddKey("15M", Convert.ToString(statistics.Statistics[11]));
            systemData["Distance"].AddKey("30M", Convert.ToString(statistics.Statistics[12]));
            systemData["Distance"].AddKey("50M", Convert.ToString(statistics.Statistics[13]));

            systemData["Angle"].AddKey("±2˚", Convert.ToString(statistics.Statistics[14]));
            systemData["Angle"].AddKey("±4˚", Convert.ToString(statistics.Statistics[15]));
            systemData["Angle"].AddKey("±6˚", Convert.ToString(statistics.Statistics[16]));
            systemData["Angle"].AddKey("±8˚", Convert.ToString(statistics.Statistics[17]));
            systemData["Angle"].AddKey("±10˚", Convert.ToString(statistics.Statistics[18]));
            systemData["Angle"].AddKey("±12˚", Convert.ToString(statistics.Statistics[19]));
            systemData["Angle"].AddKey("±14˚", Convert.ToString(statistics.Statistics[20]));
            systemData["Angle"].AddKey("±16˚", Convert.ToString(statistics.Statistics[21]));
            systemData["Angle"].AddKey("±18˚", Convert.ToString(statistics.Statistics[22]));
            systemData["Angle"].AddKey("±20˚", Convert.ToString(statistics.Statistics[23]));

            parser.WriteFile(strFilePath, systemData);
        }
        static public Dictionary<string, TackParams> ReadTackTimeFile(string strFilePath)
        {
            Dictionary<string, TackParams> tempTack = new Dictionary<string, TackParams>();

            FileIniDataParser parser = new FileIniDataParser();
            IniData data = new IniData();

            data = parser.ReadFile(strFilePath);

            foreach (SectionData section in data.Sections)
            {
                if (section.SectionName == "TackTimeSum")
                {
                    foreach (KeyData key in section.Keys)
                    {
                        tempTack.Add(key.KeyName, new TackParams(Convert.ToDouble(key.Value), 0));
                    }
                }
                else if (section.SectionName == "InspectionCount")
                {
                    foreach (KeyData key in section.Keys)
                    {
                        tempTack[key.KeyName].InspectionCount = Convert.ToInt64(key.Value);
                    }
                }
            }

            return tempTack;
        }

        static public void WriteTackTimeFile(string strFilePath, Dictionary<string, TackParams> tackTime)
        {
            if (tackTime.Count <= 0)
                return;

            FileIniDataParser parser = new FileIniDataParser();
            IniData data = new IniData();

            data.Sections.AddSection("TackTimeSum");

            foreach (KeyValuePair<string, TackParams> items in tackTime)
            {
                data["TackTimeSum"].AddKey(items.Key, Convert.ToString(items.Value.TackTimeSum));
            }

            data.Sections.AddSection("InspectionCount");

            foreach (KeyValuePair<string, TackParams> items in tackTime)
            {
                data["InspectionCount"].AddKey(items.Key, Convert.ToString(items.Value.InspectionCount));
            }

            parser.WriteFile(strFilePath, data);
        }
        static public Dictionary<string, InspectDistanceParam> ReadDistanceFile(string strFilePath)
        {
            Dictionary<string, InspectDistanceParam> tempDistance = new Dictionary<string, InspectDistanceParam>();

            FileIniDataParser parser = new FileIniDataParser();
            IniData data = new IniData();

            data = parser.ReadFile(strFilePath);

            foreach (SectionData section in data.Sections)
            {
                if (section.SectionName == "DistanceSum")
                {
                    foreach (KeyData key in section.Keys)
                    {
                        tempDistance.Add(key.KeyName, new InspectDistanceParam(Convert.ToDouble(key.Value), 0));
                    }
                }
                else if (section.SectionName == "InspectionCount")
                {
                    foreach (KeyData key in section.Keys)
                    {
                        tempDistance[key.KeyName].InspectionCount = Convert.ToInt64(key.Value);
                    }
                }
            }

            return tempDistance;
        }
        static public void WriteDistanceFile(string strFilePath, Dictionary<string, InspectDistanceParam> distanceresult)
        {
            if (distanceresult.Count <= 0)
                return;

            FileIniDataParser parser = new FileIniDataParser();
            IniData data = new IniData();

            data.Sections.AddSection("DistanceSum");

            foreach (KeyValuePair<string, InspectDistanceParam> items in distanceresult)
            {
                data["TackTimeSum"].AddKey(items.Key, Convert.ToString(items.Value.DistanceSum));
            }

            data.Sections.AddSection("InspectionCount");

            foreach (KeyValuePair<string, InspectDistanceParam> items in distanceresult)
            {
                data["InspectionCount"].AddKey(items.Key, Convert.ToString(items.Value.InspectionCount));
            }

            parser.WriteFile(strFilePath, data);
        }

        static public Dictionary<string, InspectAngleParam> ReadAngleFile(string strFilePath)
        {
            Dictionary<string, InspectAngleParam> tempAngle = new Dictionary<string, InspectAngleParam>();

            FileIniDataParser parser = new FileIniDataParser();
            IniData data = new IniData();

            data = parser.ReadFile(strFilePath);

            foreach (SectionData section in data.Sections)
            {
                if (section.SectionName == "AngleSum")
                {
                    foreach (KeyData key in section.Keys)
                    {
                        tempAngle.Add(key.KeyName, new InspectAngleParam(Convert.ToDouble(key.Value), 0));
                    }
                }
                else if (section.SectionName == "InspectionCount")
                {
                    foreach (KeyData key in section.Keys)
                    {
                        tempAngle[key.KeyName].InspectionCount = Convert.ToInt64(key.Value);
                    }
                }
            }

            return tempAngle;
        }
        static public void WriteAngleFile(string strFilePath, Dictionary<string, InspectAngleParam> angleresult)
        {
            if (angleresult.Count <= 0)
                return;

            FileIniDataParser parser = new FileIniDataParser();
            IniData data = new IniData();

            data.Sections.AddSection("AngleSum");

            foreach (KeyValuePair<string, InspectAngleParam> items in angleresult)
            {
                data["AngleSum"].AddKey(items.Key, Convert.ToString(items.Value.AngleSum));
            }

            data.Sections.AddSection("InspectionCount");

            foreach (KeyValuePair<string, InspectAngleParam> items in angleresult)
            {
                data["InspectionCount"].AddKey(items.Key, Convert.ToString(items.Value.InspectionCount));
            }

            parser.WriteFile(strFilePath, data);
        }
    }
}
