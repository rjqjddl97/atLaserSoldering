using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraBars;
using RecipeManager;
using LogLibrary;

namespace atLaserSoldering
{
    public partial class RecipeEditor : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        WorkParams _workParam = new WorkParams();
        SystemParams _systemParam = new SystemParams();
        public Log _log = new Log();

        int _gridRowIndex = -1;
        bool IsLoaded = false;

        string _strOldTitle = string.Empty;
        string _strNewTitle = string.Empty;

        public RecipeEditor()
        {
            InitializeComponent();            
            InitialRecipeParameters();
        }
        public RecipeEditor(bool _bsystemlanguage)
        {
            InitializeComponent();
            if (!_bsystemlanguage)
            {
                barButtonItemNewRecipe.Caption = "New Recipe";
                barButtonItemRecipeOpen.Caption = "Load";
                barButtonItemRecipeSave.Caption = "Save";
                ribbonPage1.Text = "File";
                ribbonPageGroup1.Text = "Recipe";

                categoryRecipeInformation.Properties.Caption = "Recipe Information";
                rowRecipeName.Properties.Caption = "Name";
                rowRecipeCreateTime.Properties.Caption = "Create Time";
                rowRecipeCreatorName.Properties.Caption = "Creator Name";

                categoryPCBInformation.Properties.Caption = "PCB Information Register";
                rowPCBModelName.Properties.Caption = "PCB Model Name";
                rowPCBHorizontalSize.Properties.Caption = "PCB Horizontal Size";
                rowPCBVerticalSize.Properties.Caption = "PCB Vertical Size";
                rowPCBLayoutFilePath.Properties.Caption = "PCB Layout File Path";

                categoryInspectionInformation.Properties.Caption = "Spot Inspection Information";
                rowReferenceInspectionLightBright.Properties.Caption = "Inspect Light Bright[0~1024]";
                rowReferenceInspectionExposureTime.Properties.Caption = "Inspect Exposure Time[us]";                
                rowReferenceInspectionVisionRecipeFilePath.Properties.Caption = "Inspect Vision Recipe FilePath";
                rowSolderingInspectionLightBright.Properties.Caption = "Sodering Light Bright[0~1024]";
                rowSolderingInspectionExposureTime.Properties.Caption = "Sodering Exposure Time[us]";
                rowSolderingInspectionVisionRecipeFilePath.Properties.Caption = "Sodering Vision Recipe FilePath";
                rowInspectionAcquisitionDelayTime.Properties.Caption = "Acquisition Delay Time[ms]";

                groupControl2.Text = "Setup Recipe";
                groupControl1.Text = "Inspection Information";

                simpleButtonInspectionPositionEdit.Text = "Edit";
                simpleButtonInspectionPositionDelete.Text = "Delete";
                simpleButtonInspectionPositionRegister.Text = "Register";

                layoutControlItem15.Text = "1. Insepction Mode";
                layoutControlItem29.Text = "2. Position X";
                layoutControlItem30.Text = "3. Position Y";
                layoutControlItem31.Text = "4. Position Z";
                layoutControlItem19.Text = "5. Ready Time[ms]";
                layoutControlItem20.Text = "7. PreHeat Time[ms]";
                layoutControlItem22.Text = "6. PreHeat P. Ratio[%]";
                layoutControlItem21.Text = "9. Heat Time[ms]";
                layoutControlItem23.Text = "8. Heat P. Ratio[%]";
                layoutControlItem24.Text = "10. F. Feed Length[mm]";
                layoutControlItem25.Text = "11. F. Feed Velocity[mm/s]";
                layoutControlItem26.Text = "12. R. Feed Length[mm]";
                layoutControlItem27.Text = "13. R. Feed Velocity[mm/s]";
            }
            else
            {
                barButtonItemNewRecipe.Caption = "새 레시피";
                barButtonItemRecipeOpen.Caption = "불러 오기";
                barButtonItemRecipeSave.Caption = "저장하기";
                ribbonPage1.Text = "파일";
                ribbonPageGroup1.Text = "레시피";

                categoryRecipeInformation.Properties.Caption = "레시피 정보";
                rowRecipeName.Properties.Caption = "레시피 이름";
                rowRecipeCreateTime.Properties.Caption = "레시피 생성 시간";
                rowRecipeCreatorName.Properties.Caption = "레시피 생성자 이름";

                categoryPCBInformation.Properties.Caption = "PCB 정보 등록";
                rowPCBModelName.Properties.Caption = "PCB 모델 이름";
                rowPCBHorizontalSize.Properties.Caption = "PCB 가로 크기[mm]";
                rowPCBVerticalSize.Properties.Caption = "PCB 세로 크기[mm]";
                rowPCBLayoutFilePath.Properties.Caption = "PCB 레이아웃 파일 경로";

                categoryInspectionInformation.Properties.Caption = "비젼 검사 정보";
                rowReferenceInspectionLightBright.Properties.Caption = "위치 보정 조명 밝기[0~1024]";
                rowReferenceInspectionExposureTime.Properties.Caption = "위치 보정 카메라 노출시간[us]";                
                rowReferenceInspectionVisionRecipeFilePath.Properties.Caption = "위치 보정 비젼레시피 파일경로";
                rowSolderingInspectionLightBright.Properties.Caption = "납땜 검사 조명 밝기[0~1024]";
                rowSolderingInspectionExposureTime.Properties.Caption = "납땜 검사 카메라 노출시간[us]";
                rowSolderingInspectionVisionRecipeFilePath.Properties.Caption = "납땜 검사 비젼레시피 파일경로";
                rowInspectionAcquisitionDelayTime.Properties.Caption = "이미지 취득 안정화 시간[ms]";

                groupControl2.Text = "거리검사 레시피 설정";
                groupControl1.Text = "검사 거리 정보";

                simpleButtonInspectionPositionEdit.Text = "수정";
                simpleButtonInspectionPositionDelete.Text = "삭제";
                simpleButtonInspectionPositionRegister.Text = "등록";

                layoutControlItem15.Text = "1. Insepction Mode";
                layoutControlItem29.Text = "2. Position X";
                layoutControlItem30.Text = "3. Position Y";
                layoutControlItem31.Text = "4. Position Z";
                layoutControlItem19.Text = "5. Ready Time[ms]";
                layoutControlItem20.Text = "7. PreHeat Time[ms]";
                layoutControlItem22.Text = "6. PreHeat P. Ratio[%]";
                layoutControlItem21.Text = "9. Heat Time[ms]";
                layoutControlItem23.Text = "8. Heat P. Ratio[%]";
                layoutControlItem24.Text = "10. F. Feed Length[mm]";
                layoutControlItem25.Text = "11. F. Feed Velocity[mm/s]";
                layoutControlItem26.Text = "12. R. Feed Length[mm]";
                layoutControlItem27.Text = "13. R. Feed Velocity[mm/s]";
            }
        }
        public void SetSystemParam(SystemParams sysParam)
        {
            _systemParam = sysParam;
        }
        public void InitialRecipeParameters()
        {
            //for (int i = 0; i < RecipeFileIO.ProductType.Length; ++i)
            //{
            //    repositoryItemComboBoxProductType.Items.Add(RecipeFileIO.ProductType[i]);
            //}

            //for (int i = 0; i < RecipeFileIO.ProductOperationMode.Length; ++i)
            //{
            //    repositoryItemComboBoxProductOpMode.Items.Add(RecipeFileIO.ProductOperationMode[i]);
            //}
            //for (int i = 0; i < RecipeFileIO.ProductDetectMeterial.Length; ++i)
            //{
            //    repositoryItemComboBoxProductDetectMeterial.Items.Add(RecipeFileIO.ProductDetectMeterial[i]);
            //}
            //for (int i = 0; i < RecipeFileIO.ProductOutputType.Length; ++i)
            //{
            //    repositoryItemComboBoxProductOutputType.Items.Add(RecipeFileIO.ProductOutputType[i]);
            //}
            for (int i = 0; i < RecipeFileIO.PositionType.Length; ++i)
            {
                comboBoxEditInspectionModeType.Properties.Items.Add(RecipeFileIO.PositionType[i]);
            }
            for (int i = 0; i < RecipeFileIO.AlignInspectionMode.Length; ++i)
            {
                repositoryItemComboBoxAlignInspectionMode.Items.Add(RecipeFileIO.AlignInspectionMode[i]);
            }
            comboBoxEditInspectionModeType.SelectedIndex = 0;
            comboBoxEditInspectionModeType.SelectedIndex = 0;
            rowReferenceInspectionMode.Properties.Value = repositoryItemComboBoxAlignInspectionMode.Items[0].ToString();
            // Recipe의 Recipe Infomation 초기화
            _workParam.RecipeName = Convert.ToString(rowRecipeName.Properties.Value);
            _workParam.RecipeCreatorName = Convert.ToString(rowRecipeCreatorName.Properties.Value);
            _workParam.RecipeCreateTime = Convert.ToDateTime(rowRecipeCreateTime.Properties.Value);

            // Recipe의 Product Infomation 초기화
            _workParam._PCBModelName = Convert.ToString(rowPCBModelName.Properties.Value);
            _workParam._PCB_HorizontalSize = Convert.ToDouble(rowPCBHorizontalSize.Properties.Value);
            _workParam._PCB_VerticalSize = Convert.ToDouble(rowPCBVerticalSize.Properties.Value);

            // Recipe의 Soldering 검사 Infomation 초기화
            _workParam._PCBAlignVisionEnable = Convert.ToBoolean(rowReferenceInspectionEnable.Properties.Value);
            _workParam._AlignInspectionMode = Convert.ToInt32(repositoryItemComboBoxAlignInspectionMode.Items.Contains(rowReferenceInspectionMode.Properties.Value)) - 1;
            _workParam._InspectionLightBright = Convert.ToInt32(rowReferenceInspectionLightBright.Properties.Value);
            _workParam._AlignInspectionExposureTime = Convert.ToInt32(rowReferenceInspectionExposureTime.Properties.Value);
            _workParam._InspectAlignVisionPath = Convert.ToString(rowReferenceInspectionVisionRecipeFilePath.Properties.Value);
            _workParam._SolderingInspectionLightBright = Convert.ToInt32(rowSolderingInspectionLightBright.Properties.Value);
            _workParam._SolderInspectionExposureTime = Convert.ToInt32(rowSolderingInspectionExposureTime.Properties.Value);
            _workParam._InspectSolderingVisionPath = Convert.ToString(rowSolderingInspectionVisionRecipeFilePath.Properties.Value);
            _workParam._ImageAcquisitionDelaytime = Convert.ToInt32(rowInspectionAcquisitionDelayTime.Properties.Value);
            //textEditPositionX.Text = _workParam.SolderPositionParams[_gridRowIndex].PositionX.ToString();
            //textEditPositionY.Text = _workParam.SolderPositionParams[_gridRowIndex].PositionY.ToString();
            //textEditPositionZ.Text = _workParam.SolderPositionParams[_gridRowIndex].PositionZ.ToString();
            //textEditReadyWaitTime.Text = _workParam.SolderPositionParams[_gridRowIndex].ReadyTime.ToString();
            //textEditPreHeatTime.Text = _workParam.SolderPositionParams[_gridRowIndex].PreHeatTime.ToString();
            //textEditPreHeatPowerRatio.Text = _workParam.SolderPositionParams[_gridRowIndex].PreheatPowerRatio.ToString();
            //textEditHeatTime.Text = _workParam.SolderPositionParams[_gridRowIndex].HeatTime.ToString();
            //textEditHeatPowerRatio.Text = _workParam.SolderPositionParams[_gridRowIndex].HeatPowerRatio.ToString();
            //textEditForwardFeedLength.Text = _workParam.SolderPositionParams[_gridRowIndex].ForwordingWireLength.ToString();
            //textEditForwardFeedVelocity.Text = _workParam.SolderPositionParams[_gridRowIndex].ForwordingVelocity.ToString();
            //textEditReverseFeedLength.Text = _workParam.SolderPositionParams[_gridRowIndex].ReverseWireLength.ToString();
            //textEditReverseFeedVelocity.Text = _workParam.SolderPositionParams[_gridRowIndex].ReverseVelocity.ToString();

            gridControlInspectionPosition.DataSource = _workParam.SolderPositionParams;
        }
        private void barButtonItemNewRecipe_ItemClick(object sender, ItemClickEventArgs e)
        {
            _workParam = new WorkParams();
            _workParam.SolderPositionParams.Clear();

            rowRecipeName.Properties.Value = _workParam.RecipeName;
            rowRecipeCreateTime.Properties.Value = _workParam.RecipeCreateTime;
            rowRecipeCreatorName.Properties.Value = _workParam.RecipeCreatorName;

            rowPCBModelName.Properties.Value = _workParam._PCBModelName;
            rowPCBHorizontalSize.Properties.Value = _workParam._PCB_HorizontalSize;
            rowPCBVerticalSize.Properties.Value = _workParam._PCB_VerticalSize;

            rowReferenceInspectionEnable.Properties.Value = _workParam._PCBAlignVisionEnable;
            rowReferenceInspectionExposureTime.Properties.Value = _workParam._AlignInspectionExposureTime;
            rowReferenceInspectionLightBright.Properties.Value = _workParam._InspectionLightBright;
            rowReferenceInspectionVisionRecipeFilePath.Properties.Value = _workParam._InspectAlignVisionPath;
            rowSoderingInspectionEnale.Properties.Value = _workParam._SolderingInspectVisionEnable;
            rowSolderingInspectionExposureTime.Properties.Value = _workParam._SolderInspectionExposureTime;
            rowSolderingInspectionLightBright.Properties.Value = _workParam._SolderingInspectionLightBright;
            rowSolderingInspectionVisionRecipeFilePath.Properties.Value = _workParam._InspectSolderingVisionPath;
            rowInspectionAcquisitionDelayTime.Properties.Value = _workParam._ImageAcquisitionDelaytime;

            rowLaserSolderingEnable.Properties.Value = _workParam._SolderingProcessEnable;
            rowLaserSolderingLaserEnable.Properties.Value = _workParam._UseLaserEnable;
            rowLaserSolderingFeedEnable.Properties.Value = _workParam._UseFeederEnable;
            rowLaserSolderingOutputWaitTime.Properties.Value = _workParam._LaserOffWaitTime;
                       
            pictureEditInspectImage.Image = null;

            _gridRowIndex = -1;

            gridControlInspectionPosition.DataSource = _workParam.SolderPositionParams;

            gridViewInspectionPositions.RefreshData();
            vGridControlInspectionParam.Refresh();

            barButtonItemRecipeSave.Enabled = false;

            this.Text = string.Format("{0} - {1}.rcp", _strOldTitle, "NewRecipe.rcp");

            //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("새로운 레시피 생성"));
        }

        private void barButtonItemRecipeOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            xtraFolderBrowserDialog.Title = "불러올 레시피 폴더를 선택하세요.";
            xtraFolderBrowserDialog.SelectedPath = SystemDirectoryParams.RecipeFolderPath;

            if (xtraFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string[] strTemp = null;
                string strRecipeName = string.Empty;

                if (!string.IsNullOrEmpty(xtraFolderBrowserDialog.SelectedPath))
                {
                    strTemp = xtraFolderBrowserDialog.SelectedPath.Split('\\');

                    if (strTemp.Length > 0)
                    {
                        strRecipeName = strTemp[strTemp.Length - 1];
                    }

                    string strRecipeFilePath = string.Format(@"{0}\{1}.rcp", xtraFolderBrowserDialog.SelectedPath, strRecipeName);

                    if (!File.Exists(strRecipeFilePath))
                    {
                        MessageBox.Show("레시피 파일을 불러올 수 없습니다. 경로를 확인해 주십시오.", "불러오기 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Recipe File 읽기
                    RecipeFileIO.ReadRecipeFile(_workParam, strRecipeFilePath);
                    UpdateRecipeControls();

                    // 초기 Save 버튼은 Disable 상태, 편집 후, Enable 상태로 변경
                    barButtonItemRecipeSave.Enabled = false;

                    this.Text = string.Format("{0} - {1}.rcp", _strOldTitle, _workParam.RecipeName);

                    _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("레시피 파일 읽기:{0}", strRecipeFilePath));
                }
            }
        }

        private void barButtonItemRecipeSave_ItemClick(object sender, ItemClickEventArgs e)
        {
             string strSavePath = string.Format(@"{0}\{1}", SystemDirectoryParams.RecipeFolderPath, _workParam.RecipeName);

            if (MessageBox.Show(string.Format("{0}을 저장하시겠습니까?\r\n저장 위치:{1}", _workParam.RecipeName, strSavePath), "레시피 저장", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                try
                {
                    if (!Directory.Exists(strSavePath))
                    {
                        Directory.CreateDirectory(strSavePath);
                    }

                    string strRecipeSaveFileName = string.Format(@"{0}\{1}.rcp", strSavePath, _workParam.RecipeName);
                    // Recipe File
                    RecipeFileIO.WriteRecipeFile(_workParam, strSavePath, strRecipeSaveFileName);

                    vGridControlInspectionParam.Refresh();

                    this.Text = string.Format("{0} - {1}.rcp", _strOldTitle, _workParam.RecipeName);

                    _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("레시피 파일을 저장합니다.{0}", strRecipeSaveFileName));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("{0}\r\n{1}", ex.Message, ex.StackTrace));
                }
            }
        } 
        /*
        private void vGridControlInspectionParam_Leave(object sender, EventArgs e)
        {
            float fValue = 0f;
            int value = 0;
            string strTemp = string.Empty;

            vGridControlInspectionParam.Refresh();

            strTemp = Convert.ToString(rowRecipeName.Properties.Value);

            if (string.IsNullOrEmpty(strTemp))
            {
                MessageBox.Show(string.Format("레시피의 이름이 잘못 입력되었습니다.\r\n{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowRecipeName.Properties.Value = _workParam.RecipeName;
                vGridControlInspectionParam.Refresh();

                return;
            }

            if (!_workParam.RecipeName.Equals(strTemp))
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam.RecipeName = strTemp;
            _workParam.RecipeCreateTime = Convert.ToDateTime(rowRecipeCreateTime.Properties.Value);

            strTemp = Convert.ToString(rowRecipeCreatorName.Properties.Value);

            if (string.IsNullOrEmpty(strTemp))
            {
                MessageBox.Show(string.Format("레시피 생성자의 이름이 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowRecipeCreatorName.Properties.Value = _workParam.RecipeCreatorName;
                vGridControlInspectionParam.Refresh();

                return;
            }

            if (!_workParam.RecipeCreatorName.Equals(strTemp))
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam.RecipeCreatorName = strTemp;
                        
            strTemp = Convert.ToString(rowProductSeries.Properties.Value);

            bool IsValidate = false;

            for (int i = 0; i < repositoryItemComboBoxProductSeries.Items.Count; ++i)
            {
                if (strTemp == Convert.ToString(repositoryItemComboBoxProductSeries.Items[i]))
                {
                    IsValidate = true;
                    break;
                }
            }

            if (!IsValidate)
            {
                MessageBox.Show(string.Format("제품 시리즈가 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowProductSeries.Properties.Value = Enum.GetName(typeof(ModelSeries), (int)_workParam._ProductSeries);                
                vGridControlInspectionParam.Refresh();
                return;
            }

            if (!repositoryItemComboBoxProductSeries.Items[_workParam._ProductSeries].Equals(strTemp))
            {
                barButtonItemRecipeSave.Enabled = true;
            }
            _workParam._ProductSeries = (int) Enum.Parse(typeof(ModelSeries), strTemp);

            strTemp = Convert.ToString(rowProductModelName.Properties.Value);

            if (string.IsNullOrEmpty(strTemp))
            {
                MessageBox.Show(string.Format("제품 모델명이 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowProductModelName.Properties.Value =_workParam._ProductModelName;
                vGridControlInspectionParam.Refresh();
                return;
            }

            if (!_workParam._ProductModelName.Equals(strTemp))
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam._ProductModelName = Convert.ToString(rowProductModelName.Properties.Value);

            strTemp = Convert.ToString(rowProductType.Properties.Value);

            for (int i = 0; i < repositoryItemComboBoxProductType.Items.Count; ++i)
            {
                if (strTemp == Convert.ToString(repositoryItemComboBoxProductType.Items[i]))
                {
                    IsValidate = true;
                    break;
                }
            }
            if (!IsValidate)
            {
                MessageBox.Show(string.Format("제품 형태가 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowProductType.Properties.Value = Enum.GetName(typeof(ModelType), (int)_workParam._ProductType);
                vGridControlInspectionParam.Refresh();
                return;
            }

            if (!repositoryItemComboBoxProductType.Items[_workParam._ProductType].Equals(strTemp))
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam._ProductType = (int) Enum.Parse(typeof(ModelType), strTemp);

            fValue = Convert.ToSingle(rowProductDistance.Properties.Value);

            if (fValue <= 0 || fValue > 50000)
            {
                MessageBox.Show("제품 거리가 잘못 입력되었습니다.\r\nPCB의 최대 가로 크기는 240mm입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowProductDistance.Properties.Value = _workParam._ProductDistance;
                vGridControlInspectionParam.Refresh();

                return;
            }

            if (_workParam._ProductDistance != fValue)
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam._ProductDistance = fValue;

            strTemp = Convert.ToString(rowProductOpMode.Properties.Value);

            for (int i = 0; i < repositoryItemComboBoxProductOpMode.Items.Count; ++i)
            {
                if (strTemp == Convert.ToString(repositoryItemComboBoxProductOpMode.Items[i]))
                {
                    IsValidate = true;
                    break;
                }
            }

            if (!IsValidate)
            {
                MessageBox.Show(string.Format("제품 동작 모드가 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowProductOpMode.Properties.Value = Enum.GetName(typeof(ModelType), (int)_workParam._ProductOperatingMdoe);
                vGridControlInspectionParam.Refresh();
                return;
            }

            if (!repositoryItemComboBoxProductOpMode.Items[_workParam._ProductOperatingMdoe].Equals(strTemp))
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam._ProductOperatingMdoe = (int) Enum.Parse(typeof(OperationMode), strTemp);

            strTemp = Convert.ToString(rowProductOutputType.Properties.Value);

            for (int i = 0; i < repositoryItemComboBoxProductOutputType.Items.Count; ++i)
            {
                if (strTemp == Convert.ToString(repositoryItemComboBoxProductOutputType.Items[i]))
                {
                    IsValidate = true;
                    break;
                }
            }

            if (!IsValidate)
            {
                MessageBox.Show(string.Format("제품 출력 형태가 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowProductOutputType.Properties.Value = repositoryItemComboBoxProductOutputType.Items[_workParam._ProductOutputType].ToString();
                vGridControlInspectionParam.Refresh();
                return;
            }

            if (!repositoryItemComboBoxProductOutputType.Items[_workParam._ProductOutputType].Equals(strTemp))
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam._ProductOutputType = (int) Enum.Parse(typeof(OutPutType), strTemp);

            strTemp = Convert.ToString(rowProductDetectMeterial.Properties.Value);

            for (int i = 0; i < repositoryItemComboBoxProductDetectMeterial.Items.Count; ++i)
            {
                if (strTemp == Convert.ToString(repositoryItemComboBoxProductDetectMeterial.Items[i]))
                {
                    IsValidate = true;
                    break;
                }
            }

            if (!IsValidate)
            {
                MessageBox.Show(string.Format("검사 검출체가 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowProductDetectMeterial.Properties.Value = repositoryItemComboBoxProductDetectMeterial.Items[_workParam._ProductDetectMerterial].ToString();
                vGridControlInspectionParam.Refresh();
                return;
            }

            if (!repositoryItemComboBoxProductDetectMeterial.Items[_workParam._ProductDetectMerterial].Equals(strTemp))
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam._ProductDetectMerterial = (int) Enum.Parse(typeof(DetectMeterial), strTemp);

            fValue = Convert.ToSingle(rowLEDInspectionAlignmentDistance.Properties.Value);

            if (fValue <= 0 || fValue > 50)
            {
                MessageBox.Show("편심 거리 설정이 잘못 입력되었습니다.\r\n편심의 최대 거리는 50mm입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowLEDInspectionAlignmentDistance.Properties.Value = _workParam._LEDInspectionAlignmentDistance;
                vGridControlInspectionParam.Refresh();

                return;
            }

            if (_workParam._LEDInspectionAlignmentDistance != fValue)
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam._LEDInspectionAlignmentDistance = fValue;

            fValue = Convert.ToSingle(rowLEDInspectionDivergenceAngle.Properties.Value);

            if (fValue <= 0 || fValue > 20)
            {
                MessageBox.Show("발산각 설정이 잘못 입력되었습니다.\r\n발산각의 최대 가로 크기는 20mm입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowLEDInspectionDivergenceAngle.Properties.Value = _workParam._LEDInspectionDivergenceAngle;
                vGridControlInspectionParam.Refresh();

                return;
            }

            if (_workParam._LEDInspectionDivergenceAngle != fValue)
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam._LEDInspectionDivergenceAngle = fValue;

            fValue = Convert.ToSingle(rowLEDInspectionShortDistance.Properties.Value);

            if (fValue <= 0 || fValue > 900)
            {
                MessageBox.Show("단축거리 설정이 잘못 입력되었습니다.\r\n단축거리의 최대 거리는 900mm입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowLEDInspectionShortDistance.Properties.Value = _workParam._LEDInspectionShortDistance;
                vGridControlInspectionParam.Refresh();

                return;
            }

            if (_workParam._LEDInspectionShortDistance != fValue)
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam._LEDInspectionShortDistance = fValue;

            fValue = Convert.ToSingle(rowLedInspectionCameraMoveDistance.Properties.Value);

            if (fValue <= 0 || fValue > 200)
            {
                MessageBox.Show("카메라 이동거리 설정이 잘못 입력되었습니다.\r\n카메라 이동거리의 최대 거리는 200mm입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowLedInspectionCameraMoveDistance.Properties.Value = _workParam._LEDInspectionCameraDistance;
                vGridControlInspectionParam.Refresh();

                return;
            }

            if (_workParam._LEDInspectionCameraDistance != fValue)
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam._LEDInspectionCameraDistance = fValue;

            value = Convert.ToInt32(rowLEDInspectionExposureTime.Properties.Value);

            if (value < 0 || value > 1000000)
            {
                MessageBox.Show("잘못된 값을 입력했습니다.\r\n카메라 노출시간은는 0~1000000사이의 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowLEDInspectionExposureTime.Properties.Value = _workParam._LEDInspectionExposureTime;
                vGridControlInspectionParam.Refresh();
                return;
            }

            if (_workParam._LEDInspectionExposureTime != value)
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam._LEDInspectionExposureTime = value;

            value = Convert.ToInt32(rowLEDInspectionAcquisitionDelayTime.Properties.Value);

            if (value < 0 || value > 10000)
            {
                MessageBox.Show("잘못된 값을 입력했습니다.\r\n카메라 대기시간은는 0~10000사이의 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowLEDInspectionAcquisitionDelayTime.Properties.Value = _workParam._LEDInspectionAcquisitionDelaytime;
                vGridControlInspectionParam.Refresh();
                return;
            }

            if (_workParam._LEDInspectionAcquisitionDelaytime != value)
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam._LEDInspectionAcquisitionDelaytime = value;

            value = Convert.ToInt32(rowLEDInspectionReferenceThresholdH.Properties.Value);

            if (value < 0 || value > 255)
            {
                MessageBox.Show("잘못된 값을 입력했습니다.\r\n임계치 값은 0~255사이의 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowLEDInspectionReferenceThresholdH.Properties.Value = _workParam._LEDInspectionReferenceThresholdH;
                vGridControlInspectionParam.Refresh();
                return;
            }

            if (_workParam._LEDInspectionReferenceThresholdH != value)
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam._LEDInspectionReferenceThresholdH = value;

            value = Convert.ToInt32(rowLEDInspectionReferenceThresholdV.Properties.Value);

            if (value < 0 || value > 255)
            {
                MessageBox.Show("잘못된 값을 입력했습니다.\r\n임계치 값은 0~255사이의 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowLEDInspectionReferenceThresholdV.Properties.Value = _workParam._LEDInspectionReferenceThresholdV;
                vGridControlInspectionParam.Refresh();
                return;
            }

            if (_workParam._LEDInspectionReferenceThresholdV != value)
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam._LEDInspectionReferenceThresholdV = value;

            fValue = Convert.ToSingle(rowLEDInspectionSpotMinSize.Properties.Value);

            if (fValue < 0 || fValue > 250)
            {
                MessageBox.Show("잘못된 값을 입력했습니다.\r\n스팟 수평 최소크기 값은 0~250사이의 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowLEDInspectionSpotMinSize.Properties.Value = _workParam._LEDInspectionSpotMinSize;
                vGridControlInspectionParam.Refresh();
                return;
            }

            if (_workParam._LEDInspectionSpotMinSize != fValue)
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam._LEDInspectionSpotMinSize = fValue;

            fValue = Convert.ToSingle(rowLEDInspectionSpotMaxSize.Properties.Value);

            if (fValue < 1 || fValue > 500)
            {
                MessageBox.Show("잘못된 값을 입력했습니다.\r\n스팟 수평 최대크기 값은 1~500사이의 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowLEDInspectionSpotMaxSize.Properties.Value = _workParam._LEDInspectionSpotMaxSize;
                vGridControlInspectionParam.Refresh();
                return;
            }

            if (_workParam._LEDInspectionSpotMaxSize != fValue)
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam._LEDInspectionSpotMaxSize = fValue;
           
            value = Convert.ToInt32(rowLEDInspectionWorkAreaLeft.Properties.Value);

            if (value < 0 || value > (_systemParam._cameraParams.HResolution - 1))
            {
                MessageBox.Show("잘못된 값을 입력했습니다.\r\n작업영역 왼쪽 시작점은 0 ~ 카메라 H 해상도 사이의 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowLEDInspectionWorkAreaLeft.Properties.Value = _workParam._LEDInspectionWorkAreaLeft;
                vGridControlInspectionParam.Refresh();
                return;
            }

            if (_workParam._LEDInspectionWorkAreaLeft != value)
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam._LEDInspectionWorkAreaLeft = value;

            value = Convert.ToInt32(rowLEDInspectionWorkAreaTop.Properties.Value);

            if (value < 0 || value > (_systemParam._cameraParams.VResolution - 1))
            {
                MessageBox.Show("잘못된 값을 입력했습니다.\r\n작업영역 위쪽 시작점은 0~ 카메라 H 해상도 사이의 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowLEDInspectionWorkAreaTop.Properties.Value = _workParam._LEDInspectionWorkAreaTop;
                vGridControlInspectionParam.Refresh();
                return;
            }

            if (_workParam._LEDInspectionWorkAreaTop != value)
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam._LEDInspectionWorkAreaTop = value;

            value = Convert.ToInt32(rowLEDInspectionWorkAreaWidth.Properties.Value);

            if ((_workParam._LEDInspectionWorkAreaLeft + value) < 1 || (_workParam._LEDInspectionWorkAreaLeft + value) > _systemParam._cameraParams.HResolution)
            {
                MessageBox.Show("잘못된 값을 입력했습니다.\r\n작업 영역 넓이 값은 1 ~ 카메라 H 해상도 사이의 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowLEDInspectionWorkAreaWidth.Properties.Value = _workParam._LedInspectionWorkAreaWidth;
                vGridControlInspectionParam.Refresh();
                return;
            }

            if (_workParam._LedInspectionWorkAreaWidth != value)
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam._LedInspectionWorkAreaWidth = value;

            value = Convert.ToInt32(rowLEDInspectionWorkAreaHeight.Properties.Value);

            if ((_workParam._LEDInspectionWorkAreaTop + value) < 1 || (_workParam._LEDInspectionWorkAreaTop + value) > _systemParam._cameraParams.VResolution)
            {
                MessageBox.Show("잘못된 값을 입력했습니다.\r\n작업영역 높이 값은 1 ~ 카메라 V 해상도 사이의 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowLEDInspectionWorkAreaHeight.Properties.Value = _workParam._LedInspectionWorkAreaHeight;
                vGridControlInspectionParam.Refresh();
                return;
            }

            if (_workParam._LedInspectionWorkAreaHeight != value)
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam._LedInspectionWorkAreaHeight = value;
        }
        */
        private void vGridControlInspectionParam_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            float fValue = 0f;
            int value = 0;
            string strTemp = string.Empty;

            if (e.Row == rowRecipeName)
            {
                strTemp = Convert.ToString(rowRecipeName.Properties.Value);

                if (string.IsNullOrEmpty(strTemp))
                {
                    MessageBox.Show(string.Format("레시피의 이름이 잘못 입력되었습니다.\r\n{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowRecipeName.Properties.Value = _workParam.RecipeName;
                    vGridControlInspectionParam.Refresh();

                    return;
                }

                _workParam.RecipeName = strTemp;

                barButtonItemRecipeSave.Enabled = true;

                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("레시피 이름이 {0}로 변경되었습니다.", _workParam.RecipeName));
            }
            else if (e.Row == rowRecipeCreateTime)
            {
                DateTime time = Convert.ToDateTime(rowRecipeCreateTime.Properties.Value);

                _workParam.RecipeCreateTime = time;

                barButtonItemRecipeSave.Enabled = true;

                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("레시피 저장 시간이 {0}로 변경되었습니다.", _workParam.RecipeCreateTime));
            }
            else if (e.Row == rowRecipeCreatorName)
            {
                strTemp = Convert.ToString(rowRecipeCreatorName.Properties.Value);

                if (string.IsNullOrEmpty(strTemp))
                {
                    MessageBox.Show(string.Format("레시피 생성자의 이름이 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowRecipeCreatorName.Properties.Value = _workParam.RecipeCreatorName;
                    vGridControlInspectionParam.Refresh();

                    return;
                }

                _workParam.RecipeCreatorName = strTemp;

                barButtonItemRecipeSave.Enabled = true;

                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("레시피 생성자가 {0}로 변경되었습니다.", _workParam.RecipeCreatorName));
            }
            else if (e.Row == rowPCBModelName)
            {
                strTemp = Convert.ToString(rowPCBModelName.Properties.Value);

                if (string.IsNullOrEmpty(strTemp))
                {
                    MessageBox.Show(string.Format("제품 시리즈가 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowPCBModelName.Properties.Value = _workParam._PCBModelName;
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._PCBModelName = strTemp;

                barButtonItemRecipeSave.Enabled = true;
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format(" 제품 시리즈 이름이 {0}로 변경되었습니다.", _workParam._PCBModelName));
            }
            else if (e.Row == rowPCBHorizontalSize)
            {
                //strTemp = Convert.ToString(rowPCBHorizontalSize.Properties.Value);

                //if (string.IsNullOrEmpty(strTemp))
                //{
                //    MessageBox.Show(string.Format("제품 모델명이 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    rowPCBHorizontalSize.Properties.Value = _workParam._ProductModelName;
                //    vGridControlInspectionParam.Refresh();
                //    return;
                //}

                //_workParam._ProductModelName = strTemp;

                //barButtonItemRecipeSave.Enabled = true;
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("제품 모델 이름이 {0}로 변경되었습니다.", _workParam._ProductModelName));
            }
            else if (e.Row == rowPCBVerticalSize)
            {
                //strTemp = Convert.ToString(rowPCBVerticalSize.Properties.Value);

                //bool IsValidate = false;

                //for (int i = 0; i < repositoryItemComboBoxProductType.Items.Count; ++i)
                //{
                //    if (strTemp == Convert.ToString(repositoryItemComboBoxProductType.Items[i]))
                //    {
                //        IsValidate = true;
                //        break;
                //    }
                //}

                //if (string.IsNullOrEmpty(strTemp) || !IsValidate)
                //{
                //    MessageBox.Show(string.Format("제품 유형이 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    rowPCBVerticalSize.Properties.Value = Enum.GetName(typeof(ModelSeries), (int)_workParam._ProductType);
                //    vGridControlInspectionParam.Refresh();
                //    return;
                //}

                //_workParam._ProductType = (int)Enum.Parse(typeof(ModelType), strTemp);

                //barButtonItemRecipeSave.Enabled = true;
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("모델 유형이 {0}로 변경되었습니다.", _workParam._ProductType.ToString()));
            }
            else if (e.Row == rowReferenceInspectionEnable)
            {
                bool check = Convert.ToBoolean(rowReferenceInspectionEnable.Properties.Value); ;
                _workParam._PCBAlignVisionEnable = check;

                if (check)
                {
                    rowReferenceInspectionEnable.Enabled = true;
                }
                else
                {
                    rowReferenceInspectionEnable.Enabled = false;
                }
                barButtonItemRecipeSave.Enabled = true;
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("PCB Align 검사 유무가 {0}로 변경되었습니다.", _workParam._PCBAlignVisionEnable.ToString()));
            }
            else if (e.Row == rowReferenceInspectionMode)
            {
                strTemp = Convert.ToString(rowReferenceInspectionMode.Properties.Value);

                bool IsValidate = false;

                for (int i = 0; i < repositoryItemComboBoxAlignInspectionMode.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxAlignInspectionMode.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (!IsValidate)
                {
                    MessageBox.Show(string.Format("Align 검사 모드가 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowReferenceInspectionMode.Properties.Value = Enum.GetName(typeof(ALIGNINSPECTMODE), (int)_workParam._AlignInspectionMode);
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                if (!repositoryItemComboBoxAlignInspectionMode.Items[_workParam._AlignInspectionMode].Equals(strTemp))
                {
                    barButtonItemRecipeSave.Enabled = true;
                }
                _workParam._AlignInspectionMode = (int)Enum.Parse(typeof(ALIGNINSPECTMODE), strTemp);
            }
            else if (e.Row == rowReferenceInspectionLightBright)
            {
                value = Convert.ToInt32(rowReferenceInspectionLightBright.Properties.Value);

                if (value <= 0 || value > 1024)
                {
                    MessageBox.Show("조명 밝기 설정이 잘못 입력되었습니다.\r\n조명 밝기의 최대 값은 1024[digit]입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowReferenceInspectionLightBright.Properties.Value = _workParam._InspectionLightBright;
                    vGridControlInspectionParam.Refresh();

                    return;
                }
                _workParam._InspectionLightBright = value;
                barButtonItemRecipeSave.Enabled = true;
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("조명 밝기가 {0}로 변경되었습니다.", _workParam._InspectionLightBright));
            }
            else if (e.Row == rowReferenceInspectionExposureTime)
            {
                value = Convert.ToInt32(rowReferenceInspectionExposureTime.Properties.Value);

                if (value <= 0 || value > 10000000)
                {
                    MessageBox.Show(string.Format("카메라 노출 시간을 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowReferenceInspectionExposureTime.Properties.Value = _workParam._AlignInspectionExposureTime;
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._AlignInspectionExposureTime = value;

                barButtonItemRecipeSave.Enabled = true;
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("카메라 노출 시간이 {0}로 변경되었습니다.", _workParam._AlignInspectionExposureTime));
            }
            else if (e.Row == rowReferenceInspectionVisionRecipeFilePath)
            {
                strTemp = Convert.ToString(rowReferenceInspectionVisionRecipeFilePath.Properties.Value);

                if (string.IsNullOrEmpty(strTemp))
                {
                    MessageBox.Show("PCB Align 비젼 레시피 경로가 입력되지 않았습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowReferenceInspectionVisionRecipeFilePath.Properties.Value = _workParam._InspectAlignVisionPath;
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._InspectAlignVisionPath = strTemp;
                barButtonItemRecipeSave.Enabled = true;
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("PCB Align 비젼 레시피 경로가 {0}로 변경되었습니다.", _workParam._InspectAlignVisionPath));
            }
            else if (e.Row == rowSoderingInspectionEnale)
            {
                bool check = Convert.ToBoolean(rowSoderingInspectionEnale.Properties.Value); ;
                _workParam._SolderingInspectVisionEnable = check;

                if (check)
                {
                    rowSoderingInspectionEnale.Enabled = true;
                }
                else
                {
                    rowSoderingInspectionEnale.Enabled = false;
                }
                barButtonItemRecipeSave.Enabled = true;
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("납땜 검사 유무가 {0}로 변경되었습니다.", _workParam._SolderingInspectVisionEnable.ToString()));
            }
            else if (e.Row == rowSolderingInspectionLightBright)
            {
                value = Convert.ToInt32(rowSolderingInspectionLightBright.Properties.Value);

                if (value < 0 || value > 1024)
                {
                    MessageBox.Show("조명 밝기 설정이 잘못 입력되었습니다.\r\n조명 밝기의 최대 값은 1024[digit]입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowSolderingInspectionLightBright.Properties.Value = _workParam._SolderingInspectionLightBright;
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._SolderingInspectionLightBright = value;
                barButtonItemRecipeSave.Enabled = true;
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("조명 밝기가  {0}로 변경되었습니다.", _workParam._SolderingInspectionLightBright));
            }
            else if (e.Row == rowSolderingInspectionExposureTime)
            {
                value = Convert.ToInt32(rowSolderingInspectionExposureTime.Properties.Value);

                if (value <= 0 || value > 10000000)
                {
                    MessageBox.Show(string.Format("카메라 노출 시간을 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowSolderingInspectionExposureTime.Properties.Value = _workParam._SolderInspectionExposureTime;
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._SolderInspectionExposureTime = value;

                barButtonItemRecipeSave.Enabled = true;
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("카메라 노출 시간이 {0}로 변경되었습니다.", _workParam._SolderInspectionExposureTime));
            }
            else if (e.Row == rowSolderingInspectionVisionRecipeFilePath)
            {
                strTemp = Convert.ToString(rowSolderingInspectionVisionRecipeFilePath.Properties.Value);

                if (string.IsNullOrEmpty(strTemp))
                {
                    MessageBox.Show("Soldering 비젼 레시피 경로가 입력되지 않았습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowSolderingInspectionVisionRecipeFilePath.Properties.Value = _workParam._InspectSolderingVisionPath;
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._InspectSolderingVisionPath = strTemp;
                barButtonItemRecipeSave.Enabled = true;
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("Soldering 비젼 레시피 경로가 {0}로 변경되었습니다.", _workParam._InspectSolderingVisionPath));
            }
            else if (e.Row == rowInspectionAcquisitionDelayTime)
            {
                value = Convert.ToInt32(rowInspectionAcquisitionDelayTime.Properties.Value);

                if (value <= 0 || value > 10000)
                {
                    MessageBox.Show(string.Format("이미지 취득 대기 시간을 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowInspectionAcquisitionDelayTime.Properties.Value = _workParam._ImageAcquisitionDelaytime;
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._ImageAcquisitionDelaytime = value;

                barButtonItemRecipeSave.Enabled = true;
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("이미지 취득 대기 시간이 {0}로 변경되었습니다.", _workParam._ImageAcquisitionDelaytime));
            }
            else if (e.Row == rowLaserSolderingEnable)
            {
                bool check = Convert.ToBoolean(rowLaserSolderingEnable.Properties.Value); ;
                _workParam._SolderingProcessEnable = check;

                if (check)
                {
                    rowLaserSolderingEnable.Enabled = true;
                }
                else
                {
                    rowLaserSolderingEnable.Enabled = false;
                }
                barButtonItemRecipeSave.Enabled = true;
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("레이저 납땜 작업 유무가 {0}로 변경되었습니다.", _workParam._SolderingProcessEnable.ToString()));
            }
            else if (e.Row == rowLaserSolderingLaserEnable)
            {
                bool check = Convert.ToBoolean(rowLaserSolderingLaserEnable.Properties.Value); ;
                _workParam._UseLaserEnable = check;

                if (check)
                {
                    rowLaserSolderingLaserEnable.Enabled = true;
                }
                else
                {
                    rowLaserSolderingLaserEnable.Enabled = false;
                }
                barButtonItemRecipeSave.Enabled = true;
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("레이저 사용 유무가 {0}로 변경되었습니다.", _workParam._UseLaserEnable.ToString()));
            }
            else if (e.Row == rowLaserSolderingFeedEnable)
            {
                bool check = Convert.ToBoolean(rowLaserSolderingFeedEnable.Properties.Value); ;
                _workParam._UseFeederEnable = check;

                if (check)
                {
                    rowLaserSolderingFeedEnable.Enabled = true;
                }
                else
                {
                    rowLaserSolderingFeedEnable.Enabled = false;
                }
                barButtonItemRecipeSave.Enabled = true;
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("실납 공급기 유무가 {0}로 변경되었습니다.", _workParam._UseFeederEnable.ToString()));
            }
            else if (e.Row == rowLaserSolderingOutputWaitTime)
            {
                value = Convert.ToInt32(rowLaserSolderingOutputWaitTime.Properties.Value);

                if (value <= 0 || value > 10000)
                {
                    MessageBox.Show(string.Format("Laser Off 대기 시간을 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLaserSolderingOutputWaitTime.Properties.Value = _workParam._LaserOffWaitTime;
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._ImageAcquisitionDelaytime = value;

                barButtonItemRecipeSave.Enabled = true;
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("Laser Off 대기 시간이 {0}로 변경되었습니다.", _workParam._LaserOffWaitTime));
            }
        }
        private void UpdateRecipeControls()
        {
            _gridRowIndex = (_workParam.SolderPositionParams.Count > 0) ? 0 : -1;

            rowRecipeName.Properties.Value = _workParam.RecipeName;
            rowRecipeCreateTime.Properties.Value = _workParam.RecipeCreateTime;
            rowRecipeCreatorName.Properties.Value = _workParam.RecipeCreatorName;

            rowPCBModelName.Properties.Value = _workParam._PCBModelName;
            //rowPCBHorizontalSize.Properties.Value = _workParam;
            //rowPCBVerticalSize.Properties.Value = Convert.ToString(repositoryItemComboBoxProductType.Items[_workParam._ProductType]);
            rowPCBLayoutFilePath.Properties.Value = _workParam._PCBLayoutPath;

            rowReferenceInspectionEnable.Properties.Value = _workParam._PCBAlignVisionEnable;
            rowReferenceInspectionLightBright.Properties.Value = _workParam._InspectionLightBright;
            rowReferenceInspectionExposureTime.Properties.Value = _workParam._AlignInspectionExposureTime;
            rowReferenceInspectionVisionRecipeFilePath.Properties.Value = _workParam._InspectAlignVisionPath;
            rowSoderingInspectionEnale.Properties.Value = _workParam._SolderingInspectVisionEnable;
            rowSolderingInspectionLightBright.Properties.Value = _workParam._SolderingInspectionLightBright;
            rowSolderingInspectionExposureTime.Properties.Value = _workParam._SolderInspectionExposureTime;
            rowSolderingInspectionVisionRecipeFilePath.Properties.Value = _workParam._InspectSolderingVisionPath;
            rowInspectionAcquisitionDelayTime.Properties.Value = _workParam._ImageAcquisitionDelaytime;

            rowLaserSolderingEnable.Properties.Value = _workParam._SolderingProcessEnable;
            rowLaserSolderingLaserEnable.Properties.Value = _workParam._UseLaserEnable;
            rowLaserSolderingFeedEnable.Properties.Value = _workParam._UseFeederEnable;

            //comboBoxEditInspectionModeType.SelectedIndex = _workParam.SolderPositionParams[_gridRowIndex].Index;
            //textEditPositionX.Text = _workParam.SolderPositionParams[_gridRowIndex].PositionX.ToString();
            //textEditPositionY.Text = _workParam.SolderPositionParams[_gridRowIndex].PositionY.ToString();
            //textEditPositionZ.Text = _workParam.SolderPositionParams[_gridRowIndex].PositionZ.ToString();
            //textEditReadyWaitTime.Text = _workParam.SolderPositionParams[_gridRowIndex].ReadyTime.ToString();
            //textEditPreHeatTime.Text = _workParam.SolderPositionParams[_gridRowIndex].PreHeatTime.ToString();
            //textEditPreHeatPowerRatio.Text = _workParam.SolderPositionParams[_gridRowIndex].PreheatPowerRatio.ToString();
            //textEditHeatTime.Text = _workParam.SolderPositionParams[_gridRowIndex].HeatTime.ToString();
            //textEditHeatPowerRatio.Text = _workParam.SolderPositionParams[_gridRowIndex].HeatPowerRatio.ToString();
            //textEditForwardFeedLength.Text = _workParam.SolderPositionParams[_gridRowIndex].ForwordingWireLength.ToString();
            //textEditForwardFeedVelocity.Text = _workParam.SolderPositionParams[_gridRowIndex].ForwordingVelocity.ToString();
            //textEditReverseFeedLength.Text = _workParam.SolderPositionParams[_gridRowIndex].ReverseWireLength.ToString();
            //textEditReverseFeedVelocity.Text = _workParam.SolderPositionParams[_gridRowIndex].ReverseVelocity.ToString(); 

            gridViewInspectionPositions.RefreshData();
            vGridControlInspectionParam.Refresh();
            pictureEditInspectImage.Refresh();
        }
        private void RecipeEditor_Load(object sender, EventArgs e)
        {
            // 최대 크기로 Loading
            this.WindowState = FormWindowState.Maximized;
            _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("레시피 편집기를 최대화합니다."));

            InitialRecipeParameters();
            _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("검사 파라미터를 초기화합니다."));

            IsLoaded = true;
            _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("레시피 편집기 로딩 성공"));

            // 초기 Save 버튼은 Disable 상태, 편집 후, Enable 상태로 변경
            barButtonItemRecipeSave.Enabled = false;

            // Title Backup
            _strOldTitle = this.Text;
        }

        private void vGridControlInspectionParam_Leave(object sender, EventArgs e)
        {
            float fValue = 0f;
            int value = 0;
            string strTemp = string.Empty;
            bool _isvalue = false;

            vGridControlInspectionParam.Refresh();

            strTemp = Convert.ToString(rowRecipeName.Properties.Value);

            if (string.IsNullOrEmpty(strTemp))
            {
                MessageBox.Show(string.Format("레시피의 이름이 잘못 입력되었습니다.\r\n{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowRecipeName.Properties.Value = _workParam.RecipeName;
                vGridControlInspectionParam.Refresh();

                return;
            }

            if (!_workParam.RecipeName.Equals(strTemp))
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam.RecipeName = strTemp;
            _workParam.RecipeCreateTime = Convert.ToDateTime(rowRecipeCreateTime.Properties.Value);

            strTemp = Convert.ToString(rowRecipeCreatorName.Properties.Value);

            if (string.IsNullOrEmpty(strTemp))
            {
                MessageBox.Show(string.Format("레시피 생성자의 이름이 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowRecipeCreatorName.Properties.Value = _workParam.RecipeCreatorName;
                vGridControlInspectionParam.Refresh();

                return;
            }

            if (!_workParam.RecipeCreatorName.Equals(strTemp))
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam.RecipeCreatorName = strTemp;

            strTemp = Convert.ToString(rowPCBModelName.Properties.Value);
            
            if (string.IsNullOrEmpty(strTemp))
            {
                MessageBox.Show(string.Format("제품 시리즈가 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowPCBModelName.Properties.Value = _workParam._PCBModelName;
                vGridControlInspectionParam.Refresh();
                return;
            }

            if (!_workParam._PCBModelName.Equals(strTemp))
            {
                barButtonItemRecipeSave.Enabled = true;
            }
            _workParam._PCBModelName = strTemp;

            _isvalue = Convert.ToBoolean(rowReferenceInspectionEnable.Properties.Value);

            if(_workParam._PCBAlignVisionEnable != _isvalue)
            {
                barButtonItemRecipeSave.Enabled = true;
            }
            _workParam._PCBAlignVisionEnable = _isvalue;

            value = Convert.ToInt32(rowReferenceInspectionLightBright.Properties.Value);

            if (value <= 0 || value > 1024)
            {
                MessageBox.Show("조명 밝기 설정이 잘못 입력되었습니다.\r\n카메라 밝기의 최대 값은 1024[digit]입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowReferenceInspectionLightBright.Properties.Value = _workParam._InspectionLightBright;
                vGridControlInspectionParam.Refresh();

                return;
            }

            if (_workParam._InspectionLightBright != value)
            {
                barButtonItemRecipeSave.Enabled = true;
            }
            _workParam._InspectionLightBright = value;

            value = Convert.ToInt32(rowReferenceInspectionExposureTime.Properties.Value);

            if (value <= 0 || value > 10000000)
            {
                MessageBox.Show(string.Format("카메라 노출 시간을 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowReferenceInspectionExposureTime.Properties.Value = _workParam._AlignInspectionExposureTime;
                vGridControlInspectionParam.Refresh();
                return;
            }

            if (_workParam._AlignInspectionExposureTime != value)
            {
                barButtonItemRecipeSave.Enabled = true;
            }
            _workParam._AlignInspectionExposureTime = value;

            value = Convert.ToInt32(rowInspectionAcquisitionDelayTime.Properties.Value);

            if (value <= 0 || value > 10000)
            {
                MessageBox.Show(string.Format("이미지 취득 대기 시간을 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowInspectionAcquisitionDelayTime.Properties.Value = _workParam._ImageAcquisitionDelaytime;
                vGridControlInspectionParam.Refresh();
                return;
            }

            if (_workParam._ImageAcquisitionDelaytime != value)
            {
                barButtonItemRecipeSave.Enabled = true;
            }
            _workParam._ImageAcquisitionDelaytime = value;

            //strTemp = Convert.ToString(rowReferenceInspectionVisionRecipeFilePath.Properties.Value);

            //if (string.IsNullOrEmpty(strTemp))
            //{
            //    MessageBox.Show(string.Format("PCB Align 비젼 레시피 경로가 입력되지 않았습니다."), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    rowReferenceInspectionVisionRecipeFilePath.Properties.Value = _workParam._InspectAlignVisionPath;
            //    vGridControlInspectionParam.Refresh();
            //    return;
            //}

            //if (!_workParam._InspectAlignVisionPath.Equals(strTemp))
            //{
            //    barButtonItemRecipeSave.Enabled = true;
            //}
            //_workParam._InspectAlignVisionPath = strTemp;

            _isvalue = Convert.ToBoolean(rowSoderingInspectionEnale.Properties.Value);

            if (_workParam._SolderingInspectVisionEnable != _isvalue)
            {
                barButtonItemRecipeSave.Enabled = true;
            }
            _workParam._SolderingInspectVisionEnable = _isvalue;

            value = Convert.ToInt32(rowSolderingInspectionLightBright.Properties.Value);

            if (value <= 0 || value > 1024)
            {
                MessageBox.Show("조명 밝기 설정이 잘못 입력되었습니다.\r\n카메라 밝기의 최대 값은 1024[digit]입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowSolderingInspectionLightBright.Properties.Value = _workParam._SolderingInspectionLightBright;
                vGridControlInspectionParam.Refresh();

                return;
            }

            if (_workParam._SolderingInspectionLightBright != value)
            {
                barButtonItemRecipeSave.Enabled = true;
            }
            _workParam._SolderingInspectionLightBright = value;

            value = Convert.ToInt32(rowSolderingInspectionExposureTime.Properties.Value);

            if (value <= 0 || value > 10000000)
            {
                MessageBox.Show(string.Format("카메라 노출 시간을 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowSolderingInspectionExposureTime.Properties.Value = _workParam;
                vGridControlInspectionParam.Refresh();
                return;
            }

            if (_workParam._InspectionLightBright != value)
            {
                barButtonItemRecipeSave.Enabled = true;
            }
            _workParam._InspectionLightBright = value;

            //strTemp = Convert.ToString(rowSolderingInspectionVisionRecipeFilePath.Properties.Value);

            //if (string.IsNullOrEmpty(strTemp))
            //{
            //    MessageBox.Show(string.Format("PCB Align 비젼 레시피 경로가 입력되지 않았습니다."), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    rowSolderingInspectionVisionRecipeFilePath.Properties.Value = _workParam._InspectSolderingVisionPath;
            //    vGridControlInspectionParam.Refresh();
            //    return;
            //}

            //if (!_workParam._InspectSolderingVisionPath.Equals(strTemp))
            //{
            //    barButtonItemRecipeSave.Enabled = true;
            //}
            //_workParam._InspectSolderingVisionPath = strTemp;

            _isvalue = Convert.ToBoolean(rowLaserSolderingEnable.Properties.Value);

            if (_workParam._SolderingProcessEnable != _isvalue)
            {
                barButtonItemRecipeSave.Enabled = true;
            }
            _workParam._SolderingProcessEnable = _isvalue;

            _isvalue = Convert.ToBoolean(rowLaserSolderingLaserEnable.Properties.Value);

            if (_workParam._UseLaserEnable != _isvalue)
            {
                barButtonItemRecipeSave.Enabled = true;
            }
            _workParam._UseLaserEnable = _isvalue;

            _isvalue = Convert.ToBoolean(rowLaserSolderingFeedEnable.Properties.Value);

            if (_workParam._UseFeederEnable != _isvalue)
            {
                barButtonItemRecipeSave.Enabled = true;
            }
            _workParam._UseFeederEnable = _isvalue;

            value = Convert.ToInt32(rowLaserSolderingOutputWaitTime.Properties.Value);
            
            if (value <= 0 || value > 1000)
            {
                MessageBox.Show(string.Format("Laser Off 대기 시간을 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowLaserSolderingOutputWaitTime.Properties.Value = _workParam._LaserOffWaitTime;
                vGridControlInspectionParam.Refresh();
                return;
            }
            _workParam._LaserOffWaitTime = value;

             
        }
        private void simpleButtonInspectionPositionRegister_Click(object sender, EventArgs e)
        {
            SolderingPosition inspectionPos = new SolderingPosition();            

            if (comboBoxEditInspectionModeType.SelectedIndex == 0)
                inspectionPos.ePositionType = INSPECTION_POSITION_MODE.POSITION_NOMAL_MODE;
            else if (comboBoxEditInspectionModeType.SelectedIndex == 1)
                inspectionPos.ePositionType = INSPECTION_POSITION_MODE.POSITION_INSPECTION_ALIGN_MODE;
            else if (comboBoxEditInspectionModeType.SelectedIndex == 2)
                inspectionPos.ePositionType = INSPECTION_POSITION_MODE.POSITION_INSPECTION_MODE;
            else
                inspectionPos.ePositionType = INSPECTION_POSITION_MODE.POSITION_SOLDERING_MODE;

            double dResult = 0;
            int iResult = 0;
            if (double.TryParse(textEditPositionX.Text, out dResult))
            {
                if (dResult >= 0 && dResult <= 400)
                {
                    inspectionPos.PositionX = dResult;
                }
                else
                {
                    MessageBox.Show("잘못된 X 위치입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (double.TryParse(textEditPositionY.Text, out dResult))
            {
                if (dResult >= 0 && dResult <= 400)
                {
                    inspectionPos.PositionY = dResult;
                }
                else
                {
                    MessageBox.Show("잘못된 Y 위치입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (double.TryParse(textEditPositionZ.Text, out dResult))
            {
                if (dResult >= 0 && dResult <= 150)
                {
                    inspectionPos.PositionZ = dResult;
                }
                else
                {
                    MessageBox.Show("잘못된 Z 위치입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if ((inspectionPos.ePositionType == INSPECTION_POSITION_MODE.POSITION_NOMAL_MODE) || (inspectionPos.ePositionType == INSPECTION_POSITION_MODE.POSITION_INSPECTION_ALIGN_MODE)
                || (inspectionPos.ePositionType == INSPECTION_POSITION_MODE.POSITION_INSPECTION_MODE) || (inspectionPos.ePositionType == INSPECTION_POSITION_MODE.POSITION_SOLDERING_MODE))
            {
                if ((inspectionPos.ePositionType == INSPECTION_POSITION_MODE.POSITION_NOMAL_MODE) || (inspectionPos.ePositionType == INSPECTION_POSITION_MODE.POSITION_INSPECTION_ALIGN_MODE) || (inspectionPos.ePositionType == INSPECTION_POSITION_MODE.POSITION_INSPECTION_MODE))
                {
                    string strMessage = string.Format("Index:{0}, Type:{1}, X:{2}, Y:{3}, Z:{4} 값을 등록하시겠습니까?",
                                            gridViewInspectionPositions.RowCount + 1,
                                            inspectionPos.ePositionType,
                                            inspectionPos.PositionX,
                                            inspectionPos.PositionY,
                                            inspectionPos.PositionZ);

                    if (MessageBox.Show(strMessage, "등록", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                        return;

                    inspectionPos.Index = gridViewInspectionPositions.RowCount + 1;
                    _workParam.SolderPositionParams.Add(inspectionPos);

                    gridViewInspectionPositions.FocusedRowHandle = _workParam.SolderPositionParams.Count - 1;
                    _gridRowIndex = _workParam.SolderPositionParams.Count - 1;

                    gridViewInspectionPositions.RefreshData();

                    barButtonItemRecipeSave.Enabled = true;

                    _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(),
                        string.Format("Type:{0} X:{1}, Y:{2}, Z:{3} 위치 데이터를 등록", inspectionPos.ePositionType.ToString(), inspectionPos.PositionX, inspectionPos.PositionY, inspectionPos.PositionZ));

                }
                else
                {
                    if (int.TryParse(textEditReadyWaitTime.Text, out iResult))
                    {
                        if (iResult >= 0)
                        {
                            inspectionPos.ReadyTime = iResult;
                        }
                        else
                        {
                            MessageBox.Show("잘못된 ReadyTime 입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    if (int.TryParse(textEditPreHeatTime.Text, out iResult))
                    {
                        if (iResult >= 0)
                        {
                            inspectionPos.PreHeatTime = iResult;
                        }
                        else
                        {
                            MessageBox.Show("잘못된 PreHeat Time 입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    if (int.TryParse(textEditPreHeatPowerRatio.Text, out iResult))
                    {
                        if (iResult >= 0)
                        {
                            inspectionPos.PreHeatPowerRatio = iResult;
                        }
                        else
                        {
                            MessageBox.Show("잘못된 PreHeat Power Ratio 입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    if (int.TryParse(textEditHeatTime.Text, out iResult))
                    {
                        if (iResult >= 0)
                        {
                            inspectionPos.HeatTime = iResult;
                        }
                        else
                        {
                            MessageBox.Show("잘못된 Heat Time 입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    if (int.TryParse(textEditHeatPowerRatio.Text, out iResult))
                    {
                        if (iResult >= 0)
                        {
                            inspectionPos.HeatPowerRatio = iResult;
                        }
                        else
                        {
                            MessageBox.Show("잘못된 Heat Power Ratio 입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    if (double.TryParse(textEditForwardFeedLength.Text, out dResult))
                    {
                        if (dResult >= 0 && dResult <= 50)
                        {
                            inspectionPos.ForwardFeedLength = dResult;
                        }
                        else
                        {
                            MessageBox.Show("잘못된 정방향 Feeding 양입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    if (double.TryParse(textEditForwardFeedVelocity.Text, out dResult))
                    {
                        if (dResult >= 0 && dResult <= 50)
                        {
                            inspectionPos.ForwardFeedVelocity = dResult;
                        }
                        else
                        {
                            MessageBox.Show("잘못된 정방향 Feeding 속도입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    if (double.TryParse(textEditReverseFeedLength.Text, out dResult))
                    {
                        if (dResult >= 0 && dResult <= 50)
                        {
                            inspectionPos.ReverseFeedLength = dResult;
                        }
                        else
                        {
                            MessageBox.Show("잘못된 역방향 Feeding 양입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    if (double.TryParse(textEditReverseFeedVelocity.Text, out dResult))
                    {
                        if (dResult >= 0 && dResult <= 50)
                        {
                            inspectionPos.ReverseFeedVelocity = dResult;
                        }
                        else
                        {
                            MessageBox.Show("잘못된 역방향 Feeding 속도입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    
                    string strMessage = string.Format("Index:{0}, Type:{1}, X:{2}, Y:{3}, Z:{4}, ReadyTime:{5}, PreHeatTime:{6}, PreHeatPower:{7}" +
                        "HeatTime:{8}, HeatPower:{9}, F FeedLength:{10}, F FeedVelocity:{11}, R FeedLength:{12}, R FeedVelocity:{13} 값을 등록하시겠습니까?",
                        gridViewInspectionPositions.RowCount + 1,
                        inspectionPos.ePositionType,
                        inspectionPos.PositionX,
                        inspectionPos.PositionY,
                        inspectionPos.PositionZ,
                        inspectionPos.ReadyTime,
                        inspectionPos.PreHeatTime,
                        inspectionPos.PreHeatPowerRatio,
                        inspectionPos.HeatTime,
                        inspectionPos.HeatPowerRatio,
                        inspectionPos.ForwardFeedLength,
                        inspectionPos.ForwardFeedVelocity,
                        inspectionPos.ReverseFeedLength,
                        inspectionPos.ReverseFeedVelocity
                        );

                    if (MessageBox.Show(strMessage, "등록", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                        return;

                    inspectionPos.Index = gridViewInspectionPositions.RowCount + 1;
                    _workParam.SolderPositionParams.Add(inspectionPos);

                    gridViewInspectionPositions.FocusedRowHandle = _workParam.SolderPositionParams.Count - 1;
                    _gridRowIndex = _workParam.SolderPositionParams.Count - 1;

                    gridViewInspectionPositions.RefreshData();

                    barButtonItemRecipeSave.Enabled = true;

                    _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(),
                        string.Format("Type:{0}, X:{1}, Y:{2}, Z:{3}, ReadyTime:{4}, PreHeatTime:{5}, PreHeatPower:{6}" +
                                    "HeatTime:{7}, HeatPower:{8}, F FeedLength:{9}, F FeedVelocity:{10}, R FeedLength:{11}, R FeedVelocity:{12}를 등록",
                                    inspectionPos.ePositionType.ToString(), inspectionPos.PositionX, inspectionPos.PositionY, inspectionPos.PositionZ,
                                    inspectionPos.ReadyTime, inspectionPos.PreHeatTime, inspectionPos.PreHeatPowerRatio, inspectionPos.HeatTime, inspectionPos.HeatPowerRatio,
                                    inspectionPos.ForwardFeedLength, inspectionPos.ForwardFeedVelocity, inspectionPos.ReverseFeedLength, inspectionPos.ReverseFeedVelocity));
                    
                }
            }
            else
            {
                MessageBox.Show("지원하지 않은 위치 형식입니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("레시피 등록 중 지원하지 않는 위치 형식입니다."));
            }
        }
        private void simpleButtonInspectionPositionDelete_Click(object sender, EventArgs e)
        {
            int rowIndex = gridViewInspectionPositions.GetFocusedDataSourceRowIndex();

            if (rowIndex < 0)
                return;

            string strMessage = string.Format("Index:{0} 값을 삭제하시겠습니까?", _workParam.SolderPositionParams[rowIndex].Index);

            if (MessageBox.Show(strMessage, "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                return;

            _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("레시피 위치 데이터를 삭제하였습니다."));

            if (rowIndex < _workParam.SolderPositionParams.Count)
            {
                _workParam.SolderPositionParams.RemoveAt(rowIndex);

                for (int i = 0; i < _workParam.SolderPositionParams.Count; ++i)
                {
                    _workParam.SolderPositionParams[i].Index = (i + 1);
                }

                if (_gridRowIndex == _workParam.SolderPositionParams.Count)
                    _gridRowIndex = _workParam.SolderPositionParams.Count - 1;

                gridViewInspectionPositions.FocusedRowHandle = _gridRowIndex;

                gridViewInspectionPositions.RefreshData();
                pictureEditInspectImage.Refresh();

                barButtonItemRecipeSave.Enabled = true;
            }
        }

        private void simpleButtonInspectionPositionEdit_Click(object sender, EventArgs e)
        {
            int rowIndex = gridViewInspectionPositions.GetFocusedDataSourceRowIndex();

            if (rowIndex < 0 || rowIndex >= _workParam.SolderPositionParams.Count)
                return;

            double dResult = 0;
            int iResult = 0;

            SolderingPosition inspectionPos = new SolderingPosition();

            inspectionPos.Index = rowIndex + 1;

            if (double.TryParse(textEditPositionX.Text, out dResult))
            {
                if (dResult >= 0 && dResult <= 400)
                {
                    inspectionPos.PositionX = dResult;
                }
                else
                {
                    MessageBox.Show("잘못된 X 위치입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (double.TryParse(textEditPositionY.Text, out dResult))
            {
                if (dResult >= 0 && dResult <= 400)
                {
                    inspectionPos.PositionY = dResult;
                }
                else
                {
                    MessageBox.Show("잘못된 Y 위치입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (double.TryParse(textEditPositionZ.Text, out dResult))
            {
                if (dResult >= 0 && dResult <= 150)
                {
                    inspectionPos.PositionZ = dResult;
                }
                else
                {
                    MessageBox.Show("잘못된 Z 위치입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (comboBoxEditInspectionModeType.SelectedIndex == 0)
                inspectionPos.ePositionType = INSPECTION_POSITION_MODE.POSITION_NOMAL_MODE;
            else if (comboBoxEditInspectionModeType.SelectedIndex == 1)
                inspectionPos.ePositionType = INSPECTION_POSITION_MODE.POSITION_INSPECTION_ALIGN_MODE;
            else if (comboBoxEditInspectionModeType.SelectedIndex == 2)
                inspectionPos.ePositionType = INSPECTION_POSITION_MODE.POSITION_INSPECTION_MODE;
            else
            {
                inspectionPos.ePositionType = INSPECTION_POSITION_MODE.POSITION_SOLDERING_MODE;
            }
            if ((inspectionPos.ePositionType == INSPECTION_POSITION_MODE.POSITION_NOMAL_MODE) || (inspectionPos.ePositionType == INSPECTION_POSITION_MODE.POSITION_INSPECTION_ALIGN_MODE)
                || (inspectionPos.ePositionType == INSPECTION_POSITION_MODE.POSITION_INSPECTION_MODE) || (inspectionPos.ePositionType == INSPECTION_POSITION_MODE.POSITION_SOLDERING_MODE))
            {
                if ((inspectionPos.ePositionType == INSPECTION_POSITION_MODE.POSITION_NOMAL_MODE) || (inspectionPos.ePositionType == INSPECTION_POSITION_MODE.POSITION_INSPECTION_ALIGN_MODE) || (inspectionPos.ePositionType == INSPECTION_POSITION_MODE.POSITION_INSPECTION_MODE))
                {
                    int postypeindex = 0;
                    if (inspectionPos.ePositionType == INSPECTION_POSITION_MODE.POSITION_NOMAL_MODE)
                        postypeindex = _workParam.SolderPositionParams.FindIndex(item => item.ePositionType.Equals(INSPECTION_POSITION_MODE.POSITION_NOMAL_MODE));
                    else if (inspectionPos.ePositionType == INSPECTION_POSITION_MODE.POSITION_INSPECTION_ALIGN_MODE)
                        postypeindex = _workParam.SolderPositionParams.FindIndex(item => item.ePositionType.Equals(INSPECTION_POSITION_MODE.POSITION_INSPECTION_ALIGN_MODE));
                    else if (inspectionPos.ePositionType == INSPECTION_POSITION_MODE.POSITION_INSPECTION_MODE)
                        postypeindex = _workParam.SolderPositionParams.FindIndex(item => item.ePositionType.Equals(INSPECTION_POSITION_MODE.POSITION_INSPECTION_MODE));

                    if (postypeindex != -1)
                    {                        
                        if (_workParam.SolderPositionParams[rowIndex].ePositionType == _workParam.SolderPositionParams[postypeindex].ePositionType)
                        {
                            string strMessage = string.Format("Index:{0}, Type:{1}, X:{2}, Y1:{3}, Z:{4} 값을 수정하시겠습니까?",
                                inspectionPos.Index,
                                inspectionPos.ePositionType,
                                inspectionPos.PositionX,
                                inspectionPos.PositionY,
                                inspectionPos.PositionZ);

                            if (MessageBox.Show(strMessage, "수정", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                                return;

                            _workParam.SolderPositionParams[rowIndex] = inspectionPos;

                            gridViewInspectionPositions.RefreshData();
                            pictureEditInspectImage.Refresh();

                            barButtonItemRecipeSave.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("동일 위치 형식이 있습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("위치 데이터가 없습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("레시피 수정 중 위치 형식 데이터가 없습니다."));
                    }                    
                }
                else
                {
                    int postypeindex = _workParam.SolderPositionParams.FindIndex(item => item.ePositionType.Equals(INSPECTION_POSITION_MODE.POSITION_SOLDERING_MODE));
                    if (postypeindex != -1)
                    {                        
                        if (_workParam.SolderPositionParams[rowIndex].ePositionType == _workParam.SolderPositionParams[postypeindex].ePositionType)
                        {
                            if (int.TryParse(textEditReadyWaitTime.Text, out iResult))
                            {
                                if (iResult >= 0)
                                {
                                    inspectionPos.ReadyTime = iResult;
                                }
                                else
                                {
                                    MessageBox.Show("잘못된 ReadyTime 입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }

                            if (int.TryParse(textEditPreHeatTime.Text, out iResult))
                            {
                                if (iResult >= 0)
                                {
                                    inspectionPos.PreHeatTime = iResult;
                                }
                                else
                                {
                                    MessageBox.Show("잘못된 PreHeat Time 입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }

                            if (int.TryParse(textEditPreHeatPowerRatio.Text, out iResult))
                            {
                                if (iResult >= 0)
                                {
                                    inspectionPos.PreHeatPowerRatio = iResult;
                                }
                                else
                                {
                                    MessageBox.Show("잘못된 PreHeat Power Ratio 입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }

                            if (int.TryParse(textEditHeatTime.Text, out iResult))
                            {
                                if (iResult >= 0)
                                {
                                    inspectionPos.HeatTime = iResult;
                                }
                                else
                                {
                                    MessageBox.Show("잘못된 Heat Time 입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }

                            if (int.TryParse(textEditHeatPowerRatio.Text, out iResult))
                            {
                                if (iResult >= 0)
                                {
                                    inspectionPos.HeatPowerRatio = iResult;
                                }
                                else
                                {
                                    MessageBox.Show("잘못된 Heat Power Ratio 입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            if (double.TryParse(textEditForwardFeedLength.Text, out dResult))
                            {
                                if (dResult >= 0 && dResult <= 50)
                                {
                                    inspectionPos.ForwardFeedLength = dResult;
                                }
                                else
                                {
                                    MessageBox.Show("잘못된 정방향 Feeding 양입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            if (double.TryParse(textEditForwardFeedVelocity.Text, out dResult))
                            {
                                if (dResult >= 0 && dResult <= 50)
                                {
                                    inspectionPos.ForwardFeedVelocity = dResult;
                                }
                                else
                                {
                                    MessageBox.Show("잘못된 정방향 Feeding 속도입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            if (double.TryParse(textEditReverseFeedLength.Text, out dResult))
                            {
                                if (dResult >= 0 && dResult <= 50)
                                {
                                    inspectionPos.ReverseFeedLength = dResult;
                                }
                                else
                                {
                                    MessageBox.Show("잘못된 역방향 Feeding 양입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            if (double.TryParse(textEditReverseFeedVelocity.Text, out dResult))
                            {
                                if (dResult >= 0 && dResult <= 50)
                                {
                                    inspectionPos.ReverseFeedVelocity = dResult;
                                }
                                else
                                {
                                    MessageBox.Show("잘못된 역방향 Feeding 속도입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }

                            string strMessage = string.Format("Index:{0}, Type:{1}, X:{2}, Y:{3}, Z:{4}, ReadyTime:{5}, PreHeatTime:{6}, PreHeatPower:{7}" +
                            "HeatTime:{8}, HeatPower:{9}, F FeedLength:{10}, F FeedVelocity:{11}, R FeedLength:{12}, R FeedVelocity:{13} 값을 수정하시겠습니까?",
                                inspectionPos.Index,
                                inspectionPos.ePositionType,
                                inspectionPos.PositionX,
                                inspectionPos.PositionY,
                                inspectionPos.PositionZ,
                                inspectionPos.ReadyTime,
                                inspectionPos.PreHeatTime,
                                inspectionPos.PreHeatPowerRatio,
                                inspectionPos.HeatTime,
                                inspectionPos.HeatPowerRatio,
                                inspectionPos.ForwardFeedLength,
                                inspectionPos.ForwardFeedVelocity,
                                inspectionPos.ReverseFeedLength,
                                inspectionPos.ReverseFeedVelocity
                                );

                            if (MessageBox.Show(strMessage, "수정", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                                return;

                            _workParam.SolderPositionParams[rowIndex] = inspectionPos;

                            gridViewInspectionPositions.RefreshData();
                            pictureEditInspectImage.Refresh();
                            
                            barButtonItemRecipeSave.Enabled = true;

                            _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(),string.Format("Type:{0}, X:{1}, Y:{2}, Z:{3}, ReadyTime:{4}, PreHeatTime:{5}, PreHeatPower:{6}" +
                                "HeatTime:{7}, HeatPower:{8}, F FeedLength:{9}, F FeedVelocity:{10}, R FeedLength:{11}, R FeedVelocity:{12}를 수정",
                                inspectionPos.ePositionType.ToString(), inspectionPos.PositionX, inspectionPos.PositionY, inspectionPos.PositionZ,
                                inspectionPos.ReadyTime, inspectionPos.PreHeatTime, inspectionPos.PreHeatPowerRatio, inspectionPos.HeatTime, inspectionPos.HeatPowerRatio,
                                inspectionPos.ForwardFeedLength, inspectionPos.ForwardFeedVelocity, inspectionPos.ReverseFeedLength, inspectionPos.ReverseFeedVelocity));
                        }
                        else
                        {
                            MessageBox.Show("동일 위치 형식이 있습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("위치 데이터가 없습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("레시피 수정 중 위치 형식 데이터가 없습니다."));
                    }
                }
            }
            else
            {
                MessageBox.Show("지원하지 않은 위치 형식입니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("레시피 수정 중 위치 형식 데이터가 없습니다."));
            }
        }

        private void simpleButtonReplaceDown_Click(object sender, EventArgs e)
        {
            if (_gridRowIndex < 0 || _gridRowIndex == _workParam.SolderPositionParams.Count - 1)
                return;

            SolderingPosition tempPos1 = _workParam.SolderPositionParams[_gridRowIndex];
            SolderingPosition tempPos2 = _workParam.SolderPositionParams[_gridRowIndex + 1];
            int tempIndex = tempPos1.Index;

            tempPos1.Index = tempPos2.Index;
            tempPos2.Index = tempIndex;

            _workParam.SolderPositionParams[_gridRowIndex] = tempPos2;
            _workParam.SolderPositionParams[_gridRowIndex + 1] = tempPos1;

            _gridRowIndex += 1;
            gridViewInspectionPositions.FocusedRowHandle = _gridRowIndex;
            gridViewInspectionPositions.RefreshData();
            vGridControlInspectionParam.Refresh();
            pictureEditInspectImage.Refresh();

            barButtonItemRecipeSave.Enabled = true;
            _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("위치 정보를 한행을 내립니다."));
        }

        private void simpleButtonReplaceUp_Click(object sender, EventArgs e)
        {
            if (_gridRowIndex <= 0 || _gridRowIndex > _workParam.SolderPositionParams.Count - 1)
                return;

            SolderingPosition tempPos1 = _workParam.SolderPositionParams[_gridRowIndex - 1];
            SolderingPosition tempPos2 = _workParam.SolderPositionParams[_gridRowIndex];

            int tempIndex = tempPos1.Index;

            tempPos1.Index = tempPos2.Index;
            tempPos2.Index = tempIndex;

            _workParam.SolderPositionParams[_gridRowIndex - 1] = tempPos2;
            _workParam.SolderPositionParams[_gridRowIndex] = tempPos1;

            _gridRowIndex -= 1;
            gridViewInspectionPositions.FocusedRowHandle = _gridRowIndex;
            gridViewInspectionPositions.RefreshData();
            vGridControlInspectionParam.Refresh();
            pictureEditInspectImage.Refresh();

            barButtonItemRecipeSave.Enabled = true;
            _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("위치 정보를 한행을 올림니다."));
        }

        private void gridViewInspectionPositions_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            _gridRowIndex = e.RowHandle;

            if (_gridRowIndex < 0 || _gridRowIndex >= _workParam.SolderPositionParams.Count)
                return;

            textEditPositionX.Text = _workParam.SolderPositionParams[_gridRowIndex].PositionX.ToString();
            textEditPositionY.Text = _workParam.SolderPositionParams[_gridRowIndex].PositionY.ToString();
            textEditPositionZ.Text = _workParam.SolderPositionParams[_gridRowIndex].PositionZ.ToString();
            textEditReadyWaitTime.Text = _workParam.SolderPositionParams[_gridRowIndex].ReadyTime.ToString();
            textEditPreHeatTime.Text = _workParam.SolderPositionParams[_gridRowIndex].PreHeatTime.ToString();
            textEditPreHeatPowerRatio.Text = _workParam.SolderPositionParams[_gridRowIndex].PreHeatPowerRatio.ToString();
            textEditHeatTime.Text = _workParam.SolderPositionParams[_gridRowIndex].HeatTime.ToString();
            textEditHeatPowerRatio.Text = _workParam.SolderPositionParams[_gridRowIndex].HeatPowerRatio.ToString();
            textEditForwardFeedLength.Text = _workParam.SolderPositionParams[_gridRowIndex].ForwardFeedLength.ToString();
            textEditForwardFeedVelocity.Text = _workParam.SolderPositionParams[_gridRowIndex].ForwardFeedVelocity.ToString();
            textEditReverseFeedLength.Text = _workParam.SolderPositionParams[_gridRowIndex].ReverseFeedLength.ToString();
            textEditReverseFeedVelocity.Text = _workParam.SolderPositionParams[_gridRowIndex].ReverseFeedVelocity.ToString();                        
            comboBoxEditInspectionModeType.SelectedIndex = (int)_workParam.SolderPositionParams[_gridRowIndex].ePositionType;

            pictureEditInspectImage.Refresh();
        }

        private void buttonEditPCBLayoutFilePath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (openFileDialogTemplateImage.ShowDialog() == DialogResult.OK)
                {
                    OpenPCBOpenLayoutImageFile(openFileDialogTemplateImage.FileName);

                    barButtonItemRecipeSave.Enabled = true;

                    //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("PCB Layout 경로 이미지를 설정합니다.{0}", _workParam.PCBLayoutFilePath));
                    //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("PCB Layout Zoom 배율:{0}", pictureEditPCBImage.Properties.ZoomPercent));
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }
        private void OpenPCBOpenLayoutImageFile(string strFilePath)
        {
            //buttonEditPCBLayoutFilePath.Text = strFilePath;
            //rowPCBLayoutFilePath.Properties.Value = strFilePath;
            //_workParam.PCBLayoutFilePath = strFilePath;

            //using (FileStream fs = new FileStream(strFilePath, FileMode.Open, FileAccess.Read))
            //{
            //    if (pictureEditInspectImage.Image != null)
            //    {
            //        pictureEditInspectImage.Image.Dispose();
            //        pictureEditInspectImage.Image = null;
            //    }

            //    pictureEditPCBImage.Image = Image.FromStream(fs);
            //}

            //PictureEdit edit = pictureEditPCBImage as PictureEdit;
            //PictureEditViewInfo vi = edit.GetViewInfo() as PictureEditViewInfo;

            //float fVScale = (float)vi.ClientRect.Height / edit.Image.Size.Height;
            //float fHScale = (float)vi.ClientRect.Width / edit.Image.Size.Width;

            //_fScale = Math.Min(fHScale, fVScale);

            //pictureEditInspectImage.Properties.ZoomPercent = _fScale * 100;

            //pictureEditInspectImage.Refresh();

            //barButtonItemRecipeSave.Enabled = true;
        }
    }
}