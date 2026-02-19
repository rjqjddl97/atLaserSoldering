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

namespace atLaserSoldering
{
    public partial class RecipeEditor : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        WorkParams _workParam = new WorkParams();
        SystemParams _systemParam = new SystemParams();
        int _gridRowIndex = -1;
        bool IsLoaded = false;

        string _strOldTitle = string.Empty;
        string _strNewTitle = string.Empty;
        public RecipeEditor()
        {
            InitializeComponent();
            //InitialRecipeParameters();
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
            rowProductSeries.Properties.Value = repositoryItemComboBoxProductSeries.Items[0].ToString();
            rowProductType.Properties.Value = repositoryItemComboBoxProductType.Items[0].ToString();
            rowProductOpMode.Properties.Value = repositoryItemComboBoxProductOpMode.Items[0].ToString();
            rowProductDetectMeterial.Properties.Value = repositoryItemComboBoxProductDetectMeterial.Items[2].ToString();
            rowProductOutputType.Properties.Value = repositoryItemComboBoxProductOutputType.Items[0].ToString();
            comboBoxEditInspectionPositionType.SelectedIndex = 0;
            // Recipe의 Recipe Infomation 초기화
            _workParam.RecipeName = Convert.ToString(rowRecipeName.Properties.Value);
            _workParam.RecipeCreatorName = Convert.ToString(rowRecipeCreatorName.Properties.Value);
            _workParam.RecipeCreateTime = Convert.ToDateTime(rowRecipeCreateTime.Properties.Value);

            // Recipe의 Product Infomation 초기화
            _workParam._ProductSeries = Convert.ToInt32(repositoryItemComboBoxProductSeries.Items.Contains(rowProductSeries.Properties.Value)) - 1;
            _workParam._ProductModelName = Convert.ToString(rowProductModelName.Properties.Value);
            _workParam._ProductType = Convert.ToInt32(repositoryItemComboBoxProductType.Items.Contains(rowProductType.Properties.Value)) - 1;
            _workParam._ProductDistance = Convert.ToSingle(rowProductDistance.Properties.Value);
            _workParam._ProductOperatingMdoe = Convert.ToInt32(repositoryItemComboBoxProductOpMode.Items.Contains(rowProductOpMode.Properties.Value)) - 1;
            _workParam._ProductOutputType = Convert.ToInt32(repositoryItemComboBoxProductOutputType.Items.Contains(rowProductOutputType.Properties.Value)) - 1;
            _workParam._ProductDetectMerterial = Convert.ToInt32(repositoryItemComboBoxProductDetectMeterial.Items.Contains(rowProductDetectMeterial.Properties.Value)) - 1;
            _workParam._ProductDistanceMargin = Convert.ToSingle(rowProductDistanceMargin.Properties.Value);
            _workParam._ProductMaxDistanceProcessEnable = Convert.ToBoolean(rowProductMaxDistanceProcessEnable.Properties.Value);
            _workParam._ProductOriginDistanceProcessEnable = Convert.ToBoolean(rowProductOriginDistanceProcessEnable.Properties.Value);
            _workParam._ProductMinObjectDetectProcessEnable = Convert.ToBoolean(rowProductMinObjectDetectProcessEnable.Properties.Value);

            // Recipe의 투광LED 검사 Infomation 초기화

            _workParam._LEDInspectionUseEnable = Convert.ToBoolean(rowLEDInspectionUseEnable.Properties.Value);
            _workParam._LEDInspectionShortDistance = Convert.ToSingle(rowLEDInspectionShortDistance.Properties.Value);
            _workParam._LEDInspectionExposureTime = Convert.ToInt32(rowLEDInspectionExposureTime.Properties.Value);
            _workParam._LEDInspectionAcquisitionDelaytime = Convert.ToInt32(rowLEDInspectionAcquisitionDelayTime.Properties.Value);
            _workParam._LEDInspectionReferenceThresholdH = Convert.ToInt32(rowLEDInspectionReferenceThresholdH.Properties.Value);
            _workParam._LEDInspectionReferenceThresholdV = Convert.ToInt32(rowLEDInspectionReferenceThresholdV.Properties.Value);
            _workParam._LEDInspectionAlignmentDistance = Convert.ToSingle(rowLEDInspectionAlignmentDistance.Properties.Value);
            _workParam._LEDInspectionDivergenceAngle = Convert.ToSingle(rowLEDInspectionDivergenceAngle.Properties.Value);
            _workParam._LEDInspectionSpotMinSize = Convert.ToSingle(rowLEDInspectionSpotMinSize.Properties.Value);
            _workParam._LEDInspectionSpotMaxSize = Convert.ToSingle(rowLEDInspectionSpotMaxSize.Properties.Value);
            _workParam._LEDInspectionWorkAreaLeft = Convert.ToInt32(rowLEDInspectionWorkAreaLeft.Properties.Value);
            _workParam._LEDInspectionWorkAreaTop = Convert.ToInt32(rowLEDInspectionWorkAreaTop.Properties.Value);
            _workParam._LedInspectionWorkAreaWidth = Convert.ToInt32(rowLEDInspectionWorkAreaWidth.Properties.Value);
            _workParam._LedInspectionWorkAreaHeight = Convert.ToInt32(rowLEDInspectionWorkAreaHeight.Properties.Value);
            gridControlInspectionPosition.DataSource = _workParam.InspectionPositions;
        }
        private void barButtonItemNewRecipe_ItemClick(object sender, ItemClickEventArgs e)
        {
            _workParam = new WorkParams();
            _workParam.InspectionPositions.Clear();

            rowRecipeName.Properties.Value = _workParam.RecipeName;
            rowRecipeCreateTime.Properties.Value = _workParam.RecipeCreateTime;
            rowRecipeCreatorName.Properties.Value = _workParam.RecipeCreatorName;

            rowProductSeries.Properties.Value = _workParam._ProductSeries;
            rowProductModelName.Properties.Value = _workParam._ProductModelName;
            rowProductType.Properties.Value = _workParam._ProductType;
            rowProductOpMode.Properties.Value = _workParam._ProductOperatingMdoe;
            rowProductDistance.Properties.Value = _workParam._ProductDistance;
            rowProductOutputType.Properties.Value = _workParam._ProductOutputType;            
            rowProductDetectMeterial.Properties.Value = _workParam._ProductDetectMerterial;
            rowProductDistanceMargin.Properties.Value = _workParam._ProductDistanceMargin;
            rowProductMaxDistanceProcessEnable.Properties.Value = _workParam._ProductMaxDistanceProcessEnable;
            rowProductOriginDistanceProcessEnable.Properties.Value = _workParam._ProductOriginDistanceProcessEnable;
            rowProductMinObjectDetectProcessEnable.Properties.Value = _workParam._ProductMinObjectDetectProcessEnable;

            rowLEDInspectionUseEnable.Properties.Value = _workParam._LEDInspectionUseEnable;
            rowLEDInspectionShortDistance.Properties.Value = _workParam._LEDInspectionShortDistance;
            rowLEDInspectionExposureTime.Properties.Value = _workParam._LEDInspectionExposureTime;
            rowLEDInspectionAcquisitionDelayTime.Properties.Value = _workParam._LEDInspectionAcquisitionDelaytime;
            rowLEDInspectionReferenceThresholdH.Properties.Value = _workParam._LEDInspectionReferenceThresholdH;
            rowLEDInspectionReferenceThresholdV.Properties.Value = _workParam._LEDInspectionReferenceThresholdV;
            rowLEDInspectionAlignmentDistance.Properties.Value = _workParam._LEDInspectionAlignmentDistance;
            rowLEDInspectionDivergenceAngle.Properties.Value = _workParam._LEDInspectionDivergenceAngle;
            rowLEDInspectionSpotMinSize.Properties.Value = _workParam._LEDInspectionSpotMinSize;
            rowLEDInspectionSpotMaxSize.Properties.Value = _workParam._LEDInspectionSpotMaxSize;
            rowLEDInspectionWorkAreaLeft.Properties.Value = _workParam._LEDInspectionWorkAreaLeft;
            rowLEDInspectionWorkAreaTop.Properties.Value = _workParam._LEDInspectionWorkAreaTop;
            rowLEDInspectionWorkAreaWidth.Properties.Value = _workParam._LedInspectionWorkAreaWidth;
            rowLEDInspectionWorkAreaHeight.Properties.Value = _workParam._LedInspectionWorkAreaHeight;
                       
            pictureEditInspectImage.Image = null;

            _gridRowIndex = -1;

            gridControlInspectionPosition.DataSource = _workParam.InspectionPositions;

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

                    this.Text = string.Format("{0} - {1}.rcp", _strOldTitle, _workParam._ProductModelName);


                    //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("레시피 파일 읽기:{0}", strRecipeFilePath));
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

                    //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("레시피 파일을 저장합니다.{0}", strRecipeSaveFileName));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("{0}\r\n{1}", ex.Message, ex.StackTrace));
                }
            }
        }

        private void simpleButtonInspectionPositionRegister_Click(object sender, EventArgs e)
        {

        }

        private void simpleButtonInspectionPositionDelete_Click(object sender, EventArgs e)
        {

        }

        private void simpleButtonInspectionPositionEdit_Click(object sender, EventArgs e)
        {

        }

        private void simpleButtonReplaceDown_Click(object sender, EventArgs e)
        {

        }

        private void simpleButtonReplaceUp_Click(object sender, EventArgs e)
        {

        }
        private void gridViewInspectionPositions_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

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

                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("레시피 이름이 변경되었습니다.{0}", _workParam.RecipeName));
            }
            else if (e.Row == rowRecipeCreateTime)
            {
                DateTime time = Convert.ToDateTime(rowRecipeCreateTime.Properties.Value);

                _workParam.RecipeCreateTime = time;

                barButtonItemRecipeSave.Enabled = true;

                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("레시피 저장 시간이 변경되었습니다.{0}", _workParam.RecipeCreateTime));
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

                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("레시피 생성자가 변경되었습니다.{0}", _workParam.RecipeCreatorName));
            }
            else if (e.Row == rowProductSeries)
            {
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

                if (string.IsNullOrEmpty(strTemp) || !IsValidate)
                {
                    MessageBox.Show(string.Format("제품 시리즈가 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowProductSeries.Properties.Value = Enum.GetName(typeof(ModelSeries), (int)_workParam._ProductSeries);
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._ProductSeries = (int)Enum.Parse(typeof(ModelSeries), strTemp);

                barButtonItemRecipeSave.Enabled = true;
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("PCB 이름이 변경되었습니다.{0}", _workParam.PCBName));
            }
            else if (e.Row == rowProductModelName)
            {
                strTemp = Convert.ToString(rowProductModelName.Properties.Value);

                if (string.IsNullOrEmpty(strTemp))
                {
                    MessageBox.Show(string.Format("제품 모델명이 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowProductModelName.Properties.Value = _workParam._ProductModelName;
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._ProductModelName = strTemp;

                barButtonItemRecipeSave.Enabled = true;
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("제품 모델 이름이 변경되었습니다.{0}", _workParam._ProductModelName));
            }
            else if (e.Row == rowProductType)
            {
                strTemp = Convert.ToString(rowProductType.Properties.Value);

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
                    rowProductType.Properties.Value = Enum.GetName(typeof(ModelSeries), (int)_workParam._ProductType);
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._ProductType = (int)Enum.Parse(typeof(ModelType), strTemp);

                barButtonItemRecipeSave.Enabled = true;
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("모델 유형이 변경되었습니다.{0}", _workParam._ProductType));
            }
            else if (e.Row == rowProductDistance)
            {
                fValue = Convert.ToSingle(rowProductDistance.Properties.Value);

                if (fValue <= 0 || fValue > 300000)
                {
                    MessageBox.Show(string.Format("제품 거리가 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowProductDistance.Properties.Value = _workParam._ProductDistance;
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._ProductDistance = fValue;

                barButtonItemRecipeSave.Enabled = true;
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("모델 유형이 변경되었습니다.{0}", _workParam._ProductDistance));
            }
            else if (e.Row == rowProductOpMode)
            {
                strTemp = Convert.ToString(rowProductOpMode.Properties.Value);

                bool IsValidate = false;

                for (int i = 0; i < repositoryItemComboBoxProductOpMode.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxProductOpMode.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(strTemp) || !IsValidate)
                {
                    MessageBox.Show(string.Format("제품 동작을 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowProductOpMode.Properties.Value = Enum.GetName(typeof(OperationMode), (int)_workParam._ProductOperatingMdoe);
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._ProductOperatingMdoe = (int)Enum.Parse(typeof(OperationMode), strTemp);

                barButtonItemRecipeSave.Enabled = true;
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("모델 유형이 변경되었습니다.{0}", _workParam._ProductOperatingMdoe));
            }
            else if (e.Row == rowProductOutputType)
            {
                strTemp = Convert.ToString(rowProductOutputType.Properties.Value);

                bool IsValidate = false;

                for (int i = 0; i < repositoryItemComboBoxProductOutputType.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxProductOutputType.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(strTemp) || !IsValidate)
                {
                    MessageBox.Show(string.Format("제품 출력 유형을 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowProductOutputType.Properties.Value = Enum.GetName(typeof(OutPutType), (int)_workParam._ProductOutputType);
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._ProductOutputType = (int)Enum.Parse(typeof(OutPutType), strTemp);

                barButtonItemRecipeSave.Enabled = true;
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("모델 유형이 변경되었습니다.{0}", _workParam._ProductOutputType));
            }
            else if (e.Row == rowProductDetectMeterial)
            {
                strTemp = Convert.ToString(rowProductDetectMeterial.Properties.Value);

                bool IsValidate = false;

                for (int i = 0; i < repositoryItemComboBoxProductDetectMeterial.Items.Count; ++i)
                {
                    if (strTemp == Convert.ToString(repositoryItemComboBoxProductDetectMeterial.Items[i]))
                    {
                        IsValidate = true;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(strTemp) || !IsValidate)
                {
                    MessageBox.Show(string.Format("제품인식 검출체 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowProductDetectMeterial.Properties.Value = Enum.GetName(typeof(DetectMeterial), (int)_workParam._ProductDetectMerterial);
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._ProductDetectMerterial = (int)Enum.Parse(typeof(DetectMeterial), strTemp);

                barButtonItemRecipeSave.Enabled = true;
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("모델 유형이 변경되었습니다.{0}", _workParam._ProductDetectMerterial));
            }
            else if (e.Row == rowProductDistanceMargin)
            {
                fValue = Convert.ToSingle(rowProductDistanceMargin.Properties.Value);

                if (fValue <= 0 || fValue > 100)
                {
                    MessageBox.Show(string.Format("거리마진율 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowProductDistanceMargin.Properties.Value = _workParam._ProductDistanceMargin;
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._ProductDistanceMargin = fValue;

                barButtonItemRecipeSave.Enabled = true;
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("거리 마진값이 변경되었습니다.{0}", _workParam._ProductDistanceMargin));
            }
            else if (e.Row == rowProductMaxDistanceProcessEnable)
            {
                bool check = Convert.ToBoolean(rowProductMaxDistanceProcessEnable.Properties.Value);
                _workParam._ProductMaxDistanceProcessEnable = check;

                barButtonItemRecipeSave.Enabled = true;
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("최대 거리 검사 유무가 변경되었습니다.{0}", _workParam._ProductMaxDistanceProcessEnable));
            }
            else if (e.Row == rowProductOriginDistanceProcessEnable)
            {
                bool check = Convert.ToBoolean(rowProductOriginDistanceProcessEnable.Properties.Value);
                _workParam._ProductOriginDistanceProcessEnable = check;

                barButtonItemRecipeSave.Enabled = true;
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("동작 거리 검사 유무가 변경되었습니다.{0}", _workParam._ProductOriginDistanceProcessEnable));
            }
            else if (e.Row == rowProductMinObjectDetectProcessEnable)
            {
                bool check = Convert.ToBoolean(rowProductMinObjectDetectProcessEnable.Properties.Value);
                _workParam._ProductMinObjectDetectProcessEnable = check;

                barButtonItemRecipeSave.Enabled = true;
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("최소 물체 인식 검사 유무가 변경되었습니다.{0}", _workParam._ProductMinObjectDetectProcessEnable));
            }
            else if (e.Row == rowLEDInspectionUseEnable)
            {
                bool check = Convert.ToBoolean(rowLEDInspectionUseEnable.Properties.Value);
                _workParam._LEDInspectionUseEnable = check;

                barButtonItemRecipeSave.Enabled = true;
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("투광 검사 유무가 변경되었습니다.{0}", _workParam._LEDInspectionUseEnable));
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
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("카메라 노출 시간이 변경되었습니다.{0}", _workParam._LEDInspectionExposureTime));
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
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("이미지 취득 대기 시간이 변경되었습니다.{0}", _workParam._LEDInspectionAcquisitionDelaytime));
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
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("이미지 취득 대기 시간이 변경되었습니다.{0}", _workParam._LEDInspectionAcquisitionDelaytime));
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
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("이미지 취득 대기 시간이 변경되었습니다.{0}", _workParam._LEDInspectionAcquisitionDelaytime));
            }
            else if (e.Row == rowLEDInspectionAlignmentDistance)
            {
                fValue = Convert.ToInt32(rowLEDInspectionAlignmentDistance.Properties.Value);

                if (fValue <= 0 || fValue > 50)
                {
                    MessageBox.Show(string.Format("편심 합격 기준거리를 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLEDInspectionAlignmentDistance.Properties.Value = _workParam._LEDInspectionAlignmentDistance;
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._LEDInspectionAlignmentDistance = fValue;

                barButtonItemRecipeSave.Enabled = true;
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("편심 합격 기준 거리가 변경되었습니다.{0}", _workParam._LEDInspectionAlignmentDistance));
            }
            else if (e.Row == rowLEDInspectionDivergenceAngle)
            {
                fValue = Convert.ToInt32(rowLEDInspectionDivergenceAngle.Properties.Value);

                if (fValue <= 0 || fValue > 20)
                {
                    MessageBox.Show(string.Format("발산각 합격 각도를 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLEDInspectionDivergenceAngle.Properties.Value = _workParam._LEDInspectionDivergenceAngle;
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._LEDInspectionDivergenceAngle = fValue;

                barButtonItemRecipeSave.Enabled = true;
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("발산각 합격 기준 각도가 변경되었습니다.{0}", _workParam._LEDInspectionDivergenceAngle));
            }
            else if (e.Row == rowLEDInspectionSpotMinSize)
            {
                fValue = Convert.ToSingle(rowLEDInspectionSpotMinSize.Properties.Value);

                if (fValue <= 0 || fValue > 250)
                {
                    MessageBox.Show(string.Format("투광 소자 수평 최소크기를 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLEDInspectionSpotMinSize.Properties.Value = _workParam._LEDInspectionSpotMinSize;
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._LEDInspectionSpotMinSize = fValue;

                barButtonItemRecipeSave.Enabled = true;
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("투광 소자 수평 최소크기 기준이 변경되었습니다.{0}", _workParam._LEDInspectionSpotHorizonMinSize));
            }
            else if (e.Row == rowLEDInspectionSpotMaxSize)
            {
                fValue = Convert.ToSingle(rowLEDInspectionSpotMaxSize.Properties.Value);

                if (fValue <= 1 || fValue > 500)
                {
                    MessageBox.Show(string.Format("투광 소자 수평 최대 크기를 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLEDInspectionSpotMaxSize.Properties.Value = _workParam._LEDInspectionSpotMaxSize;
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._LEDInspectionSpotMaxSize = fValue;

                barButtonItemRecipeSave.Enabled = true;
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("투광 소자 수평 최대 크기 기준이 변경되었습니다.{0}", _workParam._LEDInspectionSpotHorizonMaxSize));
            }            
            else if (e.Row == rowLEDInspectionWorkAreaLeft)
            {
                value = Convert.ToInt32(rowLEDInspectionWorkAreaLeft.Properties.Value);

                if (value < 0 || value > (_systemParam._cameraParams.HResolution - 1))
                {
                    MessageBox.Show(string.Format("작업영역 왼쪽 시작점을 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLEDInspectionWorkAreaLeft.Properties.Value = _workParam._LEDInspectionWorkAreaLeft;
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._LEDInspectionWorkAreaLeft = value;

                barButtonItemRecipeSave.Enabled = true;
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("작업 영역 왼쪽 시작점이 변경되었습니다.{0}", _workParam._LEDInspectionWorkAreaLeft));
            }
            else if (e.Row == rowLEDInspectionWorkAreaTop)
            {
                value = Convert.ToInt32(rowLEDInspectionWorkAreaTop.Properties.Value);

                if (value < 0 || value > (_systemParam._cameraParams.VResolution - 1))
                {
                    MessageBox.Show(string.Format("작업영역 위쪽 시작점를 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLEDInspectionWorkAreaTop.Properties.Value = _workParam._LEDInspectionWorkAreaTop;
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._LEDInspectionWorkAreaTop = value;

                barButtonItemRecipeSave.Enabled = true;
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("작업 영역 위쪽 시작점이 변경되었습니다.{0}", _workParam._LEDInspectionWorkAreaTop));
            }
            else if (e.Row == rowLEDInspectionWorkAreaWidth)
            {
                value = Convert.ToInt32(rowLEDInspectionWorkAreaWidth.Properties.Value);

                if (value < 1 || value > _systemParam._cameraParams.HResolution)
                {
                    MessageBox.Show(string.Format("작업영역 폭을 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLEDInspectionWorkAreaWidth.Properties.Value = _workParam._LedInspectionWorkAreaWidth;
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._LedInspectionWorkAreaWidth = value;

                barButtonItemRecipeSave.Enabled = true;
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("작업 영역 폭이 변경되었습니다.{0}", _workParam._LedInspectionWorkAreaWidth));
            }
            else if (e.Row == rowLEDInspectionWorkAreaHeight)
            {
                value = Convert.ToInt32(rowLEDInspectionWorkAreaHeight.Properties.Value);

                if (value < 1 || value > _systemParam._cameraParams.VResolution)
                {
                    MessageBox.Show(string.Format("작업영역 높이를 잘못 입력되었습니다.{0}", strTemp), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rowLEDInspectionWorkAreaHeight.Properties.Value = _workParam._LedInspectionWorkAreaHeight;
                    vGridControlInspectionParam.Refresh();
                    return;
                }

                _workParam._LedInspectionWorkAreaHeight = value;

                barButtonItemRecipeSave.Enabled = true;
                //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("작업 영역 높이가 변경되었습니다.{0}", _workParam._LedInspectionWorkAreaHeight));
            }
        }
        private void UpdateRecipeControls()
        {
            _gridRowIndex = (_workParam.InspectionPositions.Count > 0) ? 0 : -1;

            rowRecipeName.Properties.Value = _workParam.RecipeName;
            rowRecipeCreateTime.Properties.Value = _workParam.RecipeCreateTime;
            rowRecipeCreatorName.Properties.Value = _workParam.RecipeCreatorName;

            rowProductSeries.Properties.Value = Convert.ToString(repositoryItemComboBoxProductSeries.Items[_workParam._ProductSeries]);
            rowProductModelName.Properties.Value = _workParam._ProductModelName;
            rowProductType.Properties.Value = Convert.ToString(repositoryItemComboBoxProductType.Items[_workParam._ProductType]);
            rowProductDistance.Properties.Value = _workParam._ProductDistance;
            rowProductOpMode.Properties.Value = Convert.ToString(repositoryItemComboBoxProductOpMode.Items[_workParam._ProductOperatingMdoe]);
            rowProductOutputType.Properties.Value = Convert.ToString(repositoryItemComboBoxProductOutputType.Items[_workParam._ProductOutputType]);
            rowProductDetectMeterial.Properties.Value = Convert.ToString(repositoryItemComboBoxProductDetectMeterial.Items[_workParam._ProductDetectMerterial]);
            rowProductDistanceMargin.Properties.Value = _workParam._ProductDistanceMargin;
            rowProductMaxDistanceProcessEnable.Properties.Value = _workParam._ProductMaxDistanceProcessEnable;
            rowProductOriginDistanceProcessEnable.Properties.Value = _workParam._ProductOriginDistanceProcessEnable;
            rowProductMinObjectDetectProcessEnable.Properties.Value = _workParam._ProductMinObjectDetectProcessEnable;

            rowLEDInspectionUseEnable.Properties.Value = _workParam._LEDInspectionUseEnable;
            rowLEDInspectionShortDistance.Properties.Value = _workParam._LEDInspectionShortDistance;
            rowLedInspectionCameraMoveDistance.Properties.Value = _workParam._LEDInspectionCameraDistance;
            rowLEDInspectionExposureTime.Properties.Value = _workParam._LEDInspectionExposureTime;
            rowLEDInspectionAcquisitionDelayTime.Properties.Value = _workParam._LEDInspectionAcquisitionDelaytime;
            rowLEDInspectionAlignmentDistance.Properties.Value = _workParam._LEDInspectionAlignmentDistance;
            rowLEDInspectionDivergenceAngle.Properties.Value = _workParam._LEDInspectionDivergenceAngle;
            rowLEDInspectionReferenceThresholdH.Properties.Value = _workParam._LEDInspectionReferenceThresholdH;
            rowLEDInspectionReferenceThresholdV.Properties.Value = _workParam._LEDInspectionReferenceThresholdV;
            rowLEDInspectionSpotMinSize.Properties.Value = _workParam._LEDInspectionSpotMinSize;
            rowLEDInspectionSpotMaxSize.Properties.Value = _workParam._LEDInspectionSpotMaxSize;
            rowLEDInspectionWorkAreaLeft.Properties.Value = _workParam._LEDInspectionWorkAreaLeft;
            rowLEDInspectionWorkAreaTop.Properties.Value = _workParam._LEDInspectionWorkAreaTop;
            rowLEDInspectionWorkAreaWidth.Properties.Value = _workParam._LedInspectionWorkAreaWidth;
            rowLEDInspectionWorkAreaHeight.Properties.Value = _workParam._LedInspectionWorkAreaHeight;

            gridViewInspectionPositions.RefreshData();
            vGridControlInspectionParam.Refresh();
            pictureEditInspectImage.Refresh();
        }
        private void RecipeEditor_Load(object sender, EventArgs e)
        {
            // 최대 크기로 Loading
            this.WindowState = FormWindowState.Maximized;
            //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("레시피 편집기를 최대화합니다."));

            InitialRecipeParameters();
            //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("검사 파라미터를 초기화합니다."));

            IsLoaded = true;
            //_log.WriteLog(LogLevel.Info, LogClass.RecipeEditor.ToString(), string.Format("레시피 편집기 로딩 성공"));

            // 초기 Save 버튼은 Disable 상태, 편집 후, Enable 상태로 변경
            barButtonItemRecipeSave.Enabled = false;

            // Title Backup
            _strOldTitle = this.Text;
        }
    }
}