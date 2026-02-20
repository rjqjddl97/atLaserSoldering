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
            //InitialRecipeParameters();
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

                categoryLEDInspectionInformation.Properties.Caption = "Spot Inspection Information";
                rowLEDInspectionUseEnable.Properties.Caption = "Use Short Distance";
                rowLEDInspectionShortDistance.Properties.Caption = "Short Distance[mm]";
                rowLedInspectionCameraMoveDistance.Properties.Caption = "Camera Move Distance[mm]";
                rowLEDInspectionExposureTime.Properties.Caption = "Exposure Time[us]";
                rowLEDInspectionAcquisitionDelayTime.Properties.Caption = "Acquisition Delay Time[ms]";
                rowLEDInspectionReferenceThresholdH.Properties.Caption = "Threshold Horizon[0~255]";
                rowLEDInspectionReferenceThresholdV.Properties.Caption = "Threshold Vertical[0~255]";
                rowLEDInspectionSpotMinSize.Properties.Caption = "Min Spot Size[mm]";
                rowLEDInspectionSpotMaxSize.Properties.Caption = "Max Spot Size[mm]";
                rowLEDInspectionAlignmentDistance.Properties.Caption = "Reference Eccentricity[mm]";
                rowLEDInspectionDivergenceHMinAngle.Properties.Caption = "Reference Eccentricity Horizon Min Angle[˚]";
                rowLEDInspectionDivergenceHMaxAngle.Properties.Caption = "Reference Eccentricity Horizon Max Angle[˚]";
                rowLEDInspectionDivergenceVMinAngle.Properties.Caption = "Reference Eccentricity Vertical Min Angle[˚]";
                rowLEDInspectionDivergenceVMaxAngle.Properties.Caption = "Reference Eccentricity Vertical Max Angle[˚]";
                rowLEDInspectionWorkAreaLeft.Properties.Caption = "ROI Left[Pixel]";
                rowLEDInspectionWorkAreaTop.Properties.Caption = "ROI Top[Pixel]";
                rowLEDInspectionWorkAreaWidth.Properties.Caption = "ROI Width[Pixel]";
                rowLEDInspectionWorkAreaHeight.Properties.Caption = "ROI Height[Pixel]";

                groupControl2.Text = "Setup Recipe";
                groupControl1.Text = "Inspection Information";

                simpleButtonInspectionPositionEdit.Text = "Edit";
                simpleButtonInspectionPositionDelete.Text = "Delete";
                simpleButtonInspectionPositionRegister.Text = "Register";

                layoutControlItem6.Text = "Pos(X)";
                layoutControlItem7.Text = "Pos(Y)";
                layoutControlItem9.Text = "Pos(Z)";
                layoutControlItem15.Text = "Type";
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

                categoryLEDInspectionInformation.Properties.Caption = "투광 소자 검사 정보";
                rowLEDInspectionUseEnable.Properties.Caption = "투광 단축 검사 유무";
                rowLEDInspectionShortDistance.Properties.Caption = "투광 단축 검사거리[mm]";
                rowLedInspectionCameraMoveDistance.Properties.Caption = "카메라 이동거리[mm]";
                rowLEDInspectionExposureTime.Properties.Caption = "카메라 노출시간[us]";
                rowLEDInspectionAcquisitionDelayTime.Properties.Caption = "이미지 취득지연 시간[ms]";
                rowLEDInspectionReferenceThresholdH.Properties.Caption = "수평 인식 임계값[0~255]";
                rowLEDInspectionReferenceThresholdV.Properties.Caption = "수직 인식 임계값[0~255]";
                rowLEDInspectionSpotMinSize.Properties.Caption = "광원 최소크기[mm]";
                rowLEDInspectionSpotMaxSize.Properties.Caption = "광원 최대크기[mm]";
                rowLEDInspectionAlignmentDistance.Properties.Caption = "편심 합격거리[mm]";
                rowLEDInspectionDivergenceHMinAngle.Properties.Caption = "편심각 합격범위 수평 최소각도[˚]";
                rowLEDInspectionDivergenceHMaxAngle.Properties.Caption = "편심각 합격범위 수평 최대각도[˚]";
                rowLEDInspectionDivergenceVMinAngle.Properties.Caption = "편심각 합격범위 수직 최소각도[˚]";
                rowLEDInspectionDivergenceVMaxAngle.Properties.Caption = "편심각 합격범위 수직 최대각도[˚]";
                rowLEDInspectionWorkAreaLeft.Properties.Caption = "검사 영역 X점[픽셀]";
                rowLEDInspectionWorkAreaTop.Properties.Caption = "검사 영역 Y점[픽셀]";
                rowLEDInspectionWorkAreaWidth.Properties.Caption = "검사 영역 폭[픽셀]";
                rowLEDInspectionWorkAreaHeight.Properties.Caption = "검사 영역 높이[픽셀]";


                groupControl2.Text = "거리검사 레시피 설정";
                groupControl1.Text = "검사 거리 정보";

                simpleButtonInspectionPositionEdit.Text = "수정";
                simpleButtonInspectionPositionDelete.Text = "삭제";
                simpleButtonInspectionPositionRegister.Text = "등록";

                layoutControlItem6.Text = "위치(X)";
                layoutControlItem7.Text = "위치(Y)";
                layoutControlItem9.Text = "위치(Z)";
                layoutControlItem15.Text = "위치형태";
            }
        }
        public void SetSystemParam(SystemParams sysParam)
        {
            _systemParam = sysParam;
        }
        public void InitialRecipeParameters()
        {
            //for (int i = 0; i < RecipeFileIO.ProductSeries.Length; ++i)
            //{
            //    repositoryItemComboBoxProductSeries.Items.Add(RecipeFileIO.ProductSeries[i]);
            //}

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
            //for (int i = 0; i < RecipeFileIO.InspectionPositionType.Length; ++i)
            //{
            //    comboBoxEditInspectionPositionType.Properties.Items.Add(RecipeFileIO.InspectionPositionType[i]);
            //}
            rowPCBModelName.Properties.Value = repositoryItemComboBoxProductSeries.Items[0].ToString();
            rowPCBVerticalSize.Properties.Value = repositoryItemComboBoxProductType.Items[0].ToString();
            
            comboBoxEditInspectionPositionType.SelectedIndex = 0;
            // Recipe의 Recipe Infomation 초기화
            _workParam.RecipeName = Convert.ToString(rowRecipeName.Properties.Value);
            _workParam.RecipeCreatorName = Convert.ToString(rowRecipeCreatorName.Properties.Value);
            _workParam.RecipeCreateTime = Convert.ToDateTime(rowRecipeCreateTime.Properties.Value);

            // Recipe의 Product Infomation 초기화
            _workParam._ProductSeries = Convert.ToInt32(repositoryItemComboBoxProductSeries.Items.Contains(rowPCBModelName.Properties.Value)) - 1;
            _workParam._ProductModelName = Convert.ToString(rowPCBHorizontalSize.Properties.Value);
            _workParam._ProductType = Convert.ToInt32(repositoryItemComboBoxProductType.Items.Contains(rowPCBVerticalSize.Properties.Value)) - 1;

            // Recipe의 투광LED 검사 Infomation 초기화

            //_workParam._LEDInspectionUseEnable = Convert.ToBoolean(rowLEDInspectionUseEnable.Properties.Value);
            //_workParam._LEDInspectionShortDistance = Convert.ToSingle(rowLEDInspectionShortDistance.Properties.Value);
            //_workParam._LEDInspectionExposureTime = Convert.ToInt32(rowLEDInspectionExposureTime.Properties.Value);
            //_workParam._LEDInspectionAcquisitionDelaytime = Convert.ToInt32(rowLEDInspectionAcquisitionDelayTime.Properties.Value);
            //_workParam._LEDInspectionReferenceThresholdH = Convert.ToInt32(rowLEDInspectionReferenceThresholdH.Properties.Value);
            //_workParam._LEDInspectionReferenceThresholdV = Convert.ToInt32(rowLEDInspectionReferenceThresholdV.Properties.Value);
            //_workParam._LEDInspectionAlignmentDistance = Convert.ToSingle(rowLEDInspectionAlignmentDistance.Properties.Value);
            //_workParam._LEDInspectionDivergenceHMinAngle = Convert.ToSingle(rowLEDInspectionDivergenceHMinAngle.Properties.Value);
            //_workParam._LEDInspectionDivergenceHMaxAngle = Convert.ToSingle(rowLEDInspectionDivergenceHMaxAngle.Properties.Value);
            //_workParam._LEDInspectionDivergenceVMinAngle = Convert.ToSingle(rowLEDInspectionDivergenceVMinAngle.Properties.Value);
            //_workParam._LEDInspectionDivergenceVMaxAngle = Convert.ToSingle(rowLEDInspectionDivergenceVMaxAngle.Properties.Value);
            //_workParam._LEDInspectionSpotMinSize = Convert.ToSingle(rowLEDInspectionSpotMinSize.Properties.Value);
            //_workParam._LEDInspectionSpotMaxSize = Convert.ToSingle(rowLEDInspectionSpotMaxSize.Properties.Value);
            //_workParam._LEDInspectionWorkAreaLeft = Convert.ToInt32(rowLEDInspectionWorkAreaLeft.Properties.Value);
            //_workParam._LEDInspectionWorkAreaTop = Convert.ToInt32(rowLEDInspectionWorkAreaTop.Properties.Value);
            //_workParam._LedInspectionWorkAreaWidth = Convert.ToInt32(rowLEDInspectionWorkAreaWidth.Properties.Value);
            //_workParam._LedInspectionWorkAreaHeight = Convert.ToInt32(rowLEDInspectionWorkAreaHeight.Properties.Value);
            //gridControlInspectionPosition.DataSource = _workParam.InspectionPositions;

            if (_workParam._LEDInspectionUseEnable)
                rowLEDInspectionShortDistance.Enabled = true;
            else
                rowLEDInspectionShortDistance.Enabled = false;

            rowLEDInspectionDivergenceHMinAngle.Enabled = true;
            rowLEDInspectionDivergenceHMaxAngle.Enabled = true;
            rowLEDInspectionDivergenceVMinAngle.Enabled = false;
            rowLEDInspectionDivergenceVMaxAngle.Enabled = false;
        }
        private void barButtonItemNewRecipe_ItemClick(object sender, ItemClickEventArgs e)
        {
            _workParam = new WorkParams();
            _workParam.InspectionPositions.Clear();

            rowRecipeName.Properties.Value = _workParam.RecipeName;
            rowRecipeCreateTime.Properties.Value = _workParam.RecipeCreateTime;
            rowRecipeCreatorName.Properties.Value = _workParam.RecipeCreatorName;

            rowPCBModelName.Properties.Value = _workParam._ProductSeries;
            rowPCBHorizontalSize.Properties.Value = _workParam._ProductModelName;
            rowPCBVerticalSize.Properties.Value = _workParam._ProductType;

            //rowLEDInspectionUseEnable.Properties.Value = _workParam._LEDInspectionUseEnable;
            //rowLEDInspectionShortDistance.Properties.Value = _workParam._LEDInspectionShortDistance;
            //rowLEDInspectionExposureTime.Properties.Value = _workParam._LEDInspectionExposureTime;
            //rowLEDInspectionAcquisitionDelayTime.Properties.Value = _workParam._LEDInspectionAcquisitionDelaytime;
            //rowLEDInspectionReferenceThresholdH.Properties.Value = _workParam._LEDInspectionReferenceThresholdH;
            //rowLEDInspectionReferenceThresholdV.Properties.Value = _workParam._LEDInspectionReferenceThresholdV;
            //rowLEDInspectionAlignmentDistance.Properties.Value = _workParam._LEDInspectionAlignmentDistance;
            //rowLEDInspectionDivergenceHMinAngle.Properties.Value = _workParam._LEDInspectionDivergenceHMinAngle;
            //rowLEDInspectionDivergenceHMaxAngle.Properties.Value = _workParam._LEDInspectionDivergenceHMaxAngle;
            //rowLEDInspectionDivergenceVMinAngle.Properties.Value = _workParam._LEDInspectionDivergenceVMinAngle;
            //rowLEDInspectionDivergenceVMaxAngle.Properties.Value = _workParam._LEDInspectionDivergenceVMaxAngle;
            //rowLEDInspectionSpotMinSize.Properties.Value = _workParam._LEDInspectionSpotMinSize;
            //rowLEDInspectionSpotMaxSize.Properties.Value = _workParam._LEDInspectionSpotMaxSize;
            //rowLEDInspectionWorkAreaLeft.Properties.Value = _workParam._LEDInspectionWorkAreaLeft;
            //rowLEDInspectionWorkAreaTop.Properties.Value = _workParam._LEDInspectionWorkAreaTop;
            //rowLEDInspectionWorkAreaWidth.Properties.Value = _workParam._LedInspectionWorkAreaWidth;
            //rowLEDInspectionWorkAreaHeight.Properties.Value = _workParam._LedInspectionWorkAreaHeight;
                       
            pictureEditInspectImage.Image = null;

            _gridRowIndex = -1;

            gridControlInspectionPosition.DataSource = _workParam.InspectionPositions;

            gridViewInspectionPositions.RefreshData();
            vGridControlInspectionParam.Refresh();

            barButtonItemRecipeSave.Enabled = false;

            rowLEDInspectionDivergenceHMinAngle.Enabled = true;
            rowLEDInspectionDivergenceHMaxAngle.Enabled = true;
            rowLEDInspectionDivergenceVMinAngle.Enabled = false;
            rowLEDInspectionDivergenceVMaxAngle.Enabled = false;

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

                    this.Text = string.Format("{0} - {1}.rcp", _strOldTitle, _workParam._ProductModelName);

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

                    this.Text = string.Format("{0} - {1}.rcp", _strOldTitle, _workParam._ProductModelName);

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

                bool IsValidate = false;

                for (int i = 0; i < repositoryItemComboBoxProductSeries.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxProductSeries.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(strTemp) || !IsValidate)
                {
                    MessageBox.Show(string.Format("제품 시리즈가 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowPCBModelName.Properties.Value = Enum.GetName(typeof(ModelSeries), (int)_workParam._ProductSeries);
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._ProductSeries = (int)Enum.Parse(typeof(ModelSeries), strTemp);

                barButtonItemRecipeSave.Enabled = true;
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format(" 제품 시리즈 이름이 {0}로 변경되었습니다.", _workParam._ProductSeries.ToString()));
            }
            else if (e.Row == rowPCBHorizontalSize)
            {
                strTemp = Convert.ToString(rowPCBHorizontalSize.Properties.Value);

                if (string.IsNullOrEmpty(strTemp))
                {
                    MessageBox.Show(string.Format("제품 모델명이 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowPCBHorizontalSize.Properties.Value = _workParam._ProductModelName;
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._ProductModelName = strTemp;

                barButtonItemRecipeSave.Enabled = true;
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("제품 모델 이름이 {0}로 변경되었습니다.", _workParam._ProductModelName));
            }
            else if (e.Row == rowPCBVerticalSize)
            {
                strTemp = Convert.ToString(rowPCBVerticalSize.Properties.Value);

                bool IsValidate = false;

                for (int i = 0; i < repositoryItemComboBoxProductType.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxProductType.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(strTemp) || !IsValidate)
                {
                    MessageBox.Show(string.Format("제품 유형이 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowPCBVerticalSize.Properties.Value = Enum.GetName(typeof(ModelSeries), (int)_workParam._ProductType);
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._ProductType = (int)Enum.Parse(typeof(ModelType), strTemp);

                if (_workParam._ProductType == (int)RecipeManager.ModelType.MirrorReflective)
                {
                    rowLEDInspectionDivergenceHMinAngle.Enabled = true;
                    rowLEDInspectionDivergenceHMaxAngle.Enabled = true;
                    rowLEDInspectionDivergenceVMinAngle.Enabled = true;
                    rowLEDInspectionDivergenceVMaxAngle.Enabled = true;
                }
                else
                {
                    rowLEDInspectionDivergenceVMinAngle.Enabled = false;
                    rowLEDInspectionDivergenceVMaxAngle.Enabled = false;
                }

                barButtonItemRecipeSave.Enabled = true;
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("모델 유형이 {0}로 변경되었습니다.", _workParam._ProductType.ToString()));
            } 
            else if (e.Row == rowLEDInspectionUseEnable)
            {
                bool check = Convert.ToBoolean(rowLEDInspectionUseEnable.Properties.Value);
                _workParam._LEDInspectionUseEnable = check;

                if (check)
                {
                    rowLEDInspectionShortDistance.Enabled = true;
                    MessageBox.Show("단축거리 검사 위치 X 좌표를 추가 하거나 수정하십시오.", "알람", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    rowLEDInspectionShortDistance.Enabled = false;

                barButtonItemRecipeSave.Enabled = true;
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("단축 거리 검사 유무가 {0}로 변경되었습니다.", _workParam._LEDInspectionUseEnable.ToString()));
            }
            else if (e.Row == rowLEDInspectionShortDistance)
            {
                fValue = Convert.ToSingle(rowLEDInspectionShortDistance.Properties.Value);

                if (fValue <= 0 || fValue > 900)
                {
                    MessageBox.Show("단축거리 설정이 잘못 입력되었습니다.\r\n단축거리의 최대 거리는 900mm입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLEDInspectionShortDistance.Properties.Value = _workParam._LEDInspectionShortDistance;
                    vGridControlInspectionParam.Refresh();

                    return;
                }
                _workParam._LEDInspectionShortDistance = fValue;
                barButtonItemRecipeSave.Enabled = true;
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("단축거리가 {0}로 설정이 변경되었습니다.", _workParam._LEDInspectionShortDistance));
            }
            else if (e.Row == rowLedInspectionCameraMoveDistance)
            {
                fValue = Convert.ToSingle(rowLedInspectionCameraMoveDistance.Properties.Value);

                if (fValue <= 0 || fValue > 200)
                {
                    MessageBox.Show("카메라 이동거리 설정이 잘못 입력되었습니다.\r\n카메라 거리의 최대 거리는 200mm입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLedInspectionCameraMoveDistance.Properties.Value = _workParam._LEDInspectionCameraDistance;
                    vGridControlInspectionParam.Refresh();

                    return;
                }
                _workParam._LEDInspectionCameraDistance = fValue;
                barButtonItemRecipeSave.Enabled = true;
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("카메라 이동거리가 {0}로 변경되었습니다.", _workParam._LEDInspectionCameraDistance));
            }
            else if (e.Row == rowLEDInspectionExposureTime)
            {
                value = Convert.ToInt32(rowLEDInspectionExposureTime.Properties.Value);

                if (value <= 0 || value > 1000000)
                {
                    MessageBox.Show(string.Format("카메라 노출 시간을 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLEDInspectionExposureTime.Properties.Value = _workParam._LEDInspectionExposureTime;
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._LEDInspectionExposureTime = value;

                barButtonItemRecipeSave.Enabled = true;
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("카메라 노출 시간이 {0}로 변경되었습니다.", _workParam._LEDInspectionExposureTime));
            }
            else if (e.Row == rowLEDInspectionAcquisitionDelayTime)
            {
                value = Convert.ToInt32(rowLEDInspectionAcquisitionDelayTime.Properties.Value);

                if (value <= 0 || value > 10000)
                {
                    MessageBox.Show(string.Format("이미지 취득 대기 시간을 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLEDInspectionAcquisitionDelayTime.Properties.Value = _workParam._LEDInspectionAcquisitionDelaytime;
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._LEDInspectionAcquisitionDelaytime = value;

                barButtonItemRecipeSave.Enabled = true;
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("이미지 취득 대기 시간이 {0}로 변경되었습니다.", _workParam._LEDInspectionAcquisitionDelaytime));
            }
            else if (e.Row == rowLEDInspectionReferenceThresholdH)
            {
                value = Convert.ToInt32(rowLEDInspectionReferenceThresholdH.Properties.Value);

                if (value < 0 || value > 255)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n임계치 값은 0~255사이의 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLEDInspectionReferenceThresholdH.Properties.Value = _workParam._LEDInspectionReferenceThresholdH;
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._LEDInspectionReferenceThresholdH = value;
                barButtonItemRecipeSave.Enabled = true;
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("이미지 처리 수평 임계값이  {0}로 변경되었습니다.", _workParam._LEDInspectionReferenceThresholdH));
            }
            else if (e.Row == rowLEDInspectionReferenceThresholdV)
            {
                value = Convert.ToInt32(rowLEDInspectionReferenceThresholdV.Properties.Value);

                if (value < 0 || value > 255)
                {
                    MessageBox.Show("잘못된 값을 입력했습니다.\r\n임계치 값은 0~255사이의 값입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLEDInspectionReferenceThresholdV.Properties.Value = _workParam._LEDInspectionReferenceThresholdV;
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._LEDInspectionReferenceThresholdV = value;
                barButtonItemRecipeSave.Enabled = true;
                _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("이미지 처리 수직 임계값이 {0}로 변경되었습니다.", _workParam._LEDInspectionReferenceThresholdV));
            }
 
        }
        private void UpdateRecipeControls()
        {
            _gridRowIndex = (_workParam.InspectionPositions.Count > 0) ? 0 : -1;

            rowRecipeName.Properties.Value = _workParam.RecipeName;
            rowRecipeCreateTime.Properties.Value = _workParam.RecipeCreateTime;
            rowRecipeCreatorName.Properties.Value = _workParam.RecipeCreatorName;

            rowPCBModelName.Properties.Value = Convert.ToString(repositoryItemComboBoxProductSeries.Items[_workParam._ProductSeries]);
            rowPCBHorizontalSize.Properties.Value = _workParam._ProductModelName;
            rowPCBVerticalSize.Properties.Value = Convert.ToString(repositoryItemComboBoxProductType.Items[_workParam._ProductType]);

            //rowLEDInspectionUseEnable.Properties.Value = _workParam._LEDInspectionUseEnable;
            //rowLEDInspectionShortDistance.Properties.Value = _workParam._LEDInspectionShortDistance;
            //rowLedInspectionCameraMoveDistance.Properties.Value = _workParam._LEDInspectionCameraDistance;
            //rowLEDInspectionExposureTime.Properties.Value = _workParam._LEDInspectionExposureTime;
            //rowLEDInspectionAcquisitionDelayTime.Properties.Value = _workParam._LEDInspectionAcquisitionDelaytime;
            //rowLEDInspectionAlignmentDistance.Properties.Value = _workParam._LEDInspectionAlignmentDistance;
            //rowLEDInspectionDivergenceHMinAngle.Properties.Value = _workParam._LEDInspectionDivergenceHMinAngle;
            //rowLEDInspectionDivergenceHMaxAngle.Properties.Value = _workParam._LEDInspectionDivergenceHMaxAngle;
            //rowLEDInspectionDivergenceVMinAngle.Properties.Value = _workParam._LEDInspectionDivergenceVMinAngle;
            //rowLEDInspectionDivergenceVMaxAngle.Properties.Value = _workParam._LEDInspectionDivergenceVMaxAngle;
            //rowLEDInspectionReferenceThresholdH.Properties.Value = _workParam._LEDInspectionReferenceThresholdH;
            //rowLEDInspectionReferenceThresholdV.Properties.Value = _workParam._LEDInspectionReferenceThresholdV;
            //rowLEDInspectionSpotMinSize.Properties.Value = _workParam._LEDInspectionSpotMinSize;
            //rowLEDInspectionSpotMaxSize.Properties.Value = _workParam._LEDInspectionSpotMaxSize;
            //rowLEDInspectionWorkAreaLeft.Properties.Value = _workParam._LEDInspectionWorkAreaLeft;
            //rowLEDInspectionWorkAreaTop.Properties.Value = _workParam._LEDInspectionWorkAreaTop;
            //rowLEDInspectionWorkAreaWidth.Properties.Value = _workParam._LedInspectionWorkAreaWidth;
            //rowLEDInspectionWorkAreaHeight.Properties.Value = _workParam._LedInspectionWorkAreaHeight;

            if (_workParam._LEDInspectionUseEnable)
                rowLEDInspectionShortDistance.Enabled = true;
            else
                rowLEDInspectionShortDistance.Enabled = false;

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
                rowPCBModelName.Properties.Value = Enum.GetName(typeof(ModelSeries), (int)_workParam._ProductSeries);
                vGridControlInspectionParam.Refresh();
                return;
            }

            if (!repositoryItemComboBoxProductSeries.Items[_workParam._ProductSeries].Equals(strTemp))
            {
                barButtonItemRecipeSave.Enabled = true;
            }
            _workParam._ProductSeries = (int)Enum.Parse(typeof(ModelSeries), strTemp);

            strTemp = Convert.ToString(rowPCBHorizontalSize.Properties.Value);

            if (string.IsNullOrEmpty(strTemp))
            {
                MessageBox.Show(string.Format("제품 모델명이 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowPCBHorizontalSize.Properties.Value = _workParam._ProductModelName;
                vGridControlInspectionParam.Refresh();
                return;
            }

            if (!_workParam._ProductModelName.Equals(strTemp))
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam._ProductModelName = Convert.ToString(rowPCBHorizontalSize.Properties.Value);

            strTemp = Convert.ToString(rowPCBVerticalSize.Properties.Value);

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
                rowPCBVerticalSize.Properties.Value = Enum.GetName(typeof(ModelType), (int)_workParam._ProductType);
                vGridControlInspectionParam.Refresh();
                return;
            }

            if (!repositoryItemComboBoxProductType.Items[_workParam._ProductType].Equals(strTemp))
            {
                barButtonItemRecipeSave.Enabled = true;
            }

            _workParam._ProductType = (int)Enum.Parse(typeof(ModelType), strTemp);

            if (_workParam._ProductType == (int)RecipeManager.ModelType.MirrorReflective)
            {
                rowLEDInspectionDivergenceHMinAngle.Enabled = true;
                rowLEDInspectionDivergenceHMaxAngle.Enabled = true;
                rowLEDInspectionDivergenceVMinAngle.Enabled = true;
                rowLEDInspectionDivergenceVMaxAngle.Enabled = true;
            }
            else
            {
                rowLEDInspectionDivergenceVMinAngle.Enabled = false;
                rowLEDInspectionDivergenceVMaxAngle.Enabled = false;
            }

            fValue = Convert.ToSingle(rowLEDInspectionAlignmentDistance.Properties.Value);

            if (fValue <= 0 || fValue > 50)
            {
                MessageBox.Show("편심 거리 설정이 잘못 입력되었습니다.\r\n편심의 최대 거리는 50mm입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rowLEDInspectionAlignmentDistance.Properties.Value = _workParam._LEDInspectionAlignmentDistance;
                vGridControlInspectionParam.Refresh();

                return;
            }
        }
        private void simpleButtonInspectionPositionRegister_Click(object sender, EventArgs e)
        {
            InspectionPosition inspectionPos = new InspectionPosition();

            float fResult;

            if (float.TryParse(textEditInspectionPositionX.Text, out fResult))
            {                
                if (fResult >= 15 && fResult <= 780)
                    inspectionPos.PositionX = fResult;
                else
                {
                    if (fResult < 15)
                        inspectionPos.PositionX = 15;
                    if (fResult > 780)
                        inspectionPos.PositionX = 780;
                }                
            }
            else
            {
                MessageBox.Show("잘못된 X 위치입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (float.TryParse(textEditInspectionPositionY.Text, out fResult))
            {
                if (fResult >= -20 && fResult <= 90)
                    inspectionPos.PositionY = fResult;
                else
                {
                    if (fResult < -20)
                        inspectionPos.PositionY = -20;
                    if (fResult > 90)
                        inspectionPos.PositionY = 90;
                }
            }
            else
            {
                MessageBox.Show("잘못된 Y 위치입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (float.TryParse(textEditInspectionPositionZ.Text, out fResult))
            {
                if (fResult >= -20 && fResult <= 90)
                    inspectionPos.PositionZ = fResult;
                else
                {
                    if (fResult < -20)
                        inspectionPos.PositionY = -20;
                    if (fResult > 90)
                        inspectionPos.PositionY = 90;
                }
            }
            else
            {
                MessageBox.Show("잘못된 Z 위치입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            //inspectionPos.ePositionType = (INSPECTION_POSITION_MODE)comboBoxEditInspectionPositionType.SelectedIndex;

            if (comboBoxEditInspectionPositionType.SelectedIndex == 0)
                inspectionPos.ePositionType = INSPECTION_POSITION_MODE.POSITION_READY_MODE;
            else if (comboBoxEditInspectionPositionType.SelectedIndex == 1)
                inspectionPos.ePositionType = INSPECTION_POSITION_MODE.POSITION_MAX_DISTANCE_MODE;
            else if (comboBoxEditInspectionPositionType.SelectedIndex == 2)
                inspectionPos.ePositionType = INSPECTION_POSITION_MODE.POSITION_MIN_ORIGIN_DISTANCE_MODE;
            else
            {
                inspectionPos.ePositionType = INSPECTION_POSITION_MODE.POSITION_OPTICAL_SPOT_MODE;
                if (_workParam._LEDInspectionUseEnable)
                {
                    inspectionPos.PositionX = _workParam._LEDInspectionShortDistance;                    
                }
            }

            for (int i = 0; i < _workParam.InspectionPositions.Count; ++i)
            {
                float fX = _workParam.InspectionPositions[i].PositionX;
                float fY = _workParam.InspectionPositions[i].PositionY;
                float fZ = _workParam.InspectionPositions[i].PositionZ;
                if (fX == inspectionPos.PositionX)
                {
                    if (fY == inspectionPos.PositionY)
                    {
                        if (fZ == inspectionPos.PositionZ)
                        {
                            if (inspectionPos.ePositionType == _workParam.InspectionPositions[i].ePositionType)
                            {
                                MessageBox.Show("동일한 위치 좌표가 이미 등록되어 있습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }
            }

            if ((inspectionPos.ePositionType == INSPECTION_POSITION_MODE.POSITION_OPTICAL_SPOT_MODE) || (inspectionPos.ePositionType == INSPECTION_POSITION_MODE.POSITION_READY_MODE))
            {
                if (inspectionPos.ePositionType == INSPECTION_POSITION_MODE.POSITION_OPTICAL_SPOT_MODE)
                {
                    if (_workParam.InspectionPositions.FindIndex(item => item.ePositionType.Equals(INSPECTION_POSITION_MODE.POSITION_OPTICAL_SPOT_MODE)) == -1)
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
                        _workParam.InspectionPositions.Add(inspectionPos);

                        gridViewInspectionPositions.FocusedRowHandle = _workParam.InspectionPositions.Count - 1;
                        _gridRowIndex = _workParam.InspectionPositions.Count - 1;

                        gridViewInspectionPositions.RefreshData();

                        barButtonItemRecipeSave.Enabled = true;                        
                    }
                    else
                    {
                        MessageBox.Show("동일 위치 형식이 있습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (_workParam.InspectionPositions.FindIndex(item => item.ePositionType.Equals(INSPECTION_POSITION_MODE.POSITION_READY_MODE)) == -1)
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
                        _workParam.InspectionPositions.Add(inspectionPos);

                        gridViewInspectionPositions.FocusedRowHandle = _workParam.InspectionPositions.Count - 1;
                        _gridRowIndex = _workParam.InspectionPositions.Count - 1;

                        gridViewInspectionPositions.RefreshData();

                        barButtonItemRecipeSave.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("동일 위치 형식이 있습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("지원하지 않은 위치 형식입니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(),
                string.Format("Type:{0} X:{1}, Y:{2}, Z:{3} 검사모드:{2}를 등록", inspectionPos.ePositionType.ToString(), inspectionPos.PositionX, inspectionPos.PositionY, inspectionPos.PositionZ));
        }
        private void simpleButtonInspectionPositionDelete_Click(object sender, EventArgs e)
        {
            int rowIndex = gridViewInspectionPositions.GetFocusedDataSourceRowIndex();

            if (rowIndex < 0)
                return;

            string strMessage = string.Format("Index:{0}, Type:{1}, X:{2}, Y:{3}, Z:{4} 값을 삭제하시겠습니까?",
                _workParam.InspectionPositions[rowIndex].Index,
                _workParam.InspectionPositions[rowIndex].ePositionType,
                _workParam.InspectionPositions[rowIndex].PositionX,
                _workParam.InspectionPositions[rowIndex].PositionY,
                _workParam.InspectionPositions[rowIndex].PositionZ);

            if (MessageBox.Show(strMessage, "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                return;

            _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(),
                string.Format("Type:{0} X:{1}, Y:{2}, Z:{3} 검사모드:{2}를 삭제", _workParam.InspectionPositions[rowIndex].ePositionType.ToString(), _workParam.InspectionPositions[rowIndex].PositionX, _workParam.InspectionPositions[rowIndex].PositionY, _workParam.InspectionPositions[rowIndex].PositionZ));

            if (rowIndex < _workParam.InspectionPositions.Count)
            {
                _workParam.InspectionPositions.RemoveAt(rowIndex);

                for (int i = 0; i < _workParam.InspectionPositions.Count; ++i)
                {
                    _workParam.InspectionPositions[i].Index = (i + 1);
                }

                if (_gridRowIndex == _workParam.InspectionPositions.Count)
                    _gridRowIndex = _workParam.InspectionPositions.Count - 1;

                gridViewInspectionPositions.FocusedRowHandle = _gridRowIndex;

                gridViewInspectionPositions.RefreshData();
                pictureEditInspectImage.Refresh();

                barButtonItemRecipeSave.Enabled = true;
            }
        }

        private void simpleButtonInspectionPositionEdit_Click(object sender, EventArgs e)
        {
            int rowIndex = gridViewInspectionPositions.GetFocusedDataSourceRowIndex();

            if (rowIndex < 0 || rowIndex >= _workParam.InspectionPositions.Count)
                return;

            float fResult;

            InspectionPosition inspectionPos = new InspectionPosition();

            inspectionPos.Index = rowIndex + 1;

            if (float.TryParse(textEditInspectionPositionX.Text, out fResult))
            {
                if (fResult >= 15 && fResult <= 780)
                    inspectionPos.PositionX = fResult;
                else
                {
                    if (fResult < 15)
                        inspectionPos.PositionX = 15;
                    if (fResult > 780)
                        inspectionPos.PositionX = 780;
                }
            }
            else
            {
                MessageBox.Show("잘못된 X 위치입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (float.TryParse(textEditInspectionPositionY.Text, out fResult))
            {
                if (fResult >= -20 && fResult <= 90)
                    inspectionPos.PositionY = fResult;
                else
                {
                    if (fResult < -20)
                        inspectionPos.PositionY = -20;
                    if (fResult > 90)
                        inspectionPos.PositionY = 90;
                }
            }
            else
            {
                MessageBox.Show("잘못된 Y1 위치입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (float.TryParse(textEditInspectionPositionZ.Text, out fResult))
            {
                if (fResult >= -20 && fResult <= 90)
                    inspectionPos.PositionZ = fResult;
                else
                {
                    if (fResult < -20)
                        inspectionPos.PositionZ = -20;
                    if (fResult > 90)
                        inspectionPos.PositionZ = 90;
                }
            }
            else
            {
                MessageBox.Show("잘못된 Z 위치입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (comboBoxEditInspectionPositionType.SelectedIndex == 0)
                inspectionPos.ePositionType = INSPECTION_POSITION_MODE.POSITION_READY_MODE;
            else if (comboBoxEditInspectionPositionType.SelectedIndex == 1)
                inspectionPos.ePositionType = INSPECTION_POSITION_MODE.POSITION_MAX_DISTANCE_MODE;
            else if (comboBoxEditInspectionPositionType.SelectedIndex == 2)
                inspectionPos.ePositionType = INSPECTION_POSITION_MODE.POSITION_MIN_ORIGIN_DISTANCE_MODE;
            else
            {
                inspectionPos.ePositionType = INSPECTION_POSITION_MODE.POSITION_OPTICAL_SPOT_MODE;
                if (_workParam._LEDInspectionUseEnable)
                {
                    inspectionPos.PositionX = _workParam._LEDInspectionShortDistance;                    
                }
            }
            if ((inspectionPos.ePositionType == INSPECTION_POSITION_MODE.POSITION_OPTICAL_SPOT_MODE) || (inspectionPos.ePositionType == INSPECTION_POSITION_MODE.POSITION_READY_MODE))
            {
                if (inspectionPos.ePositionType == INSPECTION_POSITION_MODE.POSITION_READY_MODE)
                {
                    int postypeindex = _workParam.InspectionPositions.FindIndex(item => item.ePositionType.Equals(INSPECTION_POSITION_MODE.POSITION_READY_MODE));
                    if (postypeindex != -1)
                    {                        
                        if (_workParam.InspectionPositions[rowIndex].ePositionType == _workParam.InspectionPositions[postypeindex].ePositionType)
                        {
                            string strMessage = string.Format("Index:{0}, Type:{1}, X:{2}, Y1:{3}, Z:{4} 값을 수정하시겠습니까?",
                                inspectionPos.Index,
                                inspectionPos.ePositionType,
                                inspectionPos.PositionX,
                                inspectionPos.PositionY,
                                inspectionPos.PositionZ);

                            if (MessageBox.Show(strMessage, "수정", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                                return;

                            _workParam.InspectionPositions[rowIndex] = inspectionPos;

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
                    }                    
                }
                else
                {
                    int postypeindex = _workParam.InspectionPositions.FindIndex(item => item.ePositionType.Equals(INSPECTION_POSITION_MODE.POSITION_OPTICAL_SPOT_MODE));
                    if (postypeindex != -1)
                    {                        
                        if (_workParam.InspectionPositions[rowIndex].ePositionType == _workParam.InspectionPositions[postypeindex].ePositionType)
                        {
                            string strMessage = string.Format("Index:{0}, Type:{1}, X:{2}, Y1:{3}, Z:{4} 값을 수정하시겠습니까?",
                                inspectionPos.Index,
                                inspectionPos.ePositionType,
                                inspectionPos.PositionX,
                                inspectionPos.PositionY,
                                inspectionPos.PositionZ);

                            if (MessageBox.Show(strMessage, "수정", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                                return;

                            _workParam.InspectionPositions[rowIndex] = inspectionPos;

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
                    }
                }
            }
            else
            {
                MessageBox.Show("지원하지 않은 위치 형식입니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(),
                string.Format("Type:{0} X:{1}, Y:{2}, Z:{3} 검사모드:{2}를 수정", inspectionPos.ePositionType.ToString(), inspectionPos.PositionX, inspectionPos.PositionY, inspectionPos.PositionZ));
        }

        private void simpleButtonReplaceDown_Click(object sender, EventArgs e)
        {
            if (_gridRowIndex < 0 || _gridRowIndex == _workParam.InspectionPositions.Count - 1)
                return;

            InspectionPosition tempPos1 = _workParam.InspectionPositions[_gridRowIndex];
            InspectionPosition tempPos2 = _workParam.InspectionPositions[_gridRowIndex + 1];
            int tempIndex = tempPos1.Index;

            tempPos1.Index = tempPos2.Index;
            tempPos2.Index = tempIndex;

            _workParam.InspectionPositions[_gridRowIndex] = tempPos2;
            _workParam.InspectionPositions[_gridRowIndex + 1] = tempPos1;

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
            if (_gridRowIndex <= 0 || _gridRowIndex > _workParam.InspectionPositions.Count - 1)
                return;

            InspectionPosition tempPos1 = _workParam.InspectionPositions[_gridRowIndex - 1];
            InspectionPosition tempPos2 = _workParam.InspectionPositions[_gridRowIndex];

            int tempIndex = tempPos1.Index;

            tempPos1.Index = tempPos2.Index;
            tempPos2.Index = tempIndex;

            _workParam.InspectionPositions[_gridRowIndex - 1] = tempPos2;
            _workParam.InspectionPositions[_gridRowIndex] = tempPos1;

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

            if (_gridRowIndex < 0 || _gridRowIndex >= _workParam.InspectionPositions.Count)
                return;

            textEditInspectionPositionX.Text = _workParam.InspectionPositions[_gridRowIndex].PositionX.ToString();
            textEditInspectionPositionY.Text = _workParam.InspectionPositions[_gridRowIndex].PositionY.ToString();
            textEditInspectionPositionZ.Text = _workParam.InspectionPositions[_gridRowIndex].PositionZ.ToString();
            comboBoxEditInspectionPositionType.SelectedIndex = (int)_workParam.InspectionPositions[_gridRowIndex].ePositionType;

            pictureEditInspectImage.Refresh();
        }
    }
}