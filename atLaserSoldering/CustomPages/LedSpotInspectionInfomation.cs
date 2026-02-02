using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomPages
{
    public partial class LedSpotInspectionInfomation : UserControl
    {
        public double _InspectLedBlobHsize { get { return Convert.ToDouble(rowLedSpotBlobInspectHSize.Properties.Value); } set { rowLedSpotBlobInspectHSize.Properties.Value = value; } }
        public double _InspectLedBlobVsize { get { return Convert.ToDouble(rowLedSpotBlobInspectVSize.Properties.Value); } set { rowLedSpotBlobInspectVSize.Properties.Value = value; } }

        public string _InspectProductModelName { get { return rowProductModelName.Properties.Value.ToString(); } set { rowProductModelName.Properties.Value = value; } }
        public string _InspectProductSeries { get { return rowProductSeries.Properties.Value.ToString(); } set { rowProductSeries.Properties.Value = value; } }
        public double _InspectLedBlobBright { get { return Convert.ToDouble(rowLedSpotImageBright.Properties.Value); } set { rowLedSpotImageBright.Properties.Value = value; } }
        public double _InspectLedOpticalEccentricity { get { return Convert.ToDouble(rowAlignmentDistance.Properties.Value); } set { rowAlignmentDistance.Properties.Value = value; } }        
        public double _InspectLedOpticalEccentricAngle { get { return Convert.ToDouble(rowOpticalEccentricAngle.Properties.Value); } set { rowOpticalEccentricAngle.Properties.Value = value; } }
        public double _InspectLedOpticalEccentricHorizonAngle { get { return Convert.ToDouble(rowOpticalEccentricHorizonAngle.Properties.Value); } set { rowOpticalEccentricHorizonAngle.Properties.Value = value; } }
        public double _InspectLedOpticalEccentricVirticalAngle { get { return Convert.ToDouble(rowOpticalEccentricVirticalAngle.Properties.Value); } set { rowOpticalEccentricVirticalAngle.Properties.Value = value; } }
        public double _InspectLedOpticalEmiterAngle { get { return Convert.ToDouble(rowDivergenceAngle.Properties.Value); } set { rowDivergenceAngle.Properties.Value = value; } }
        public double _InspectLedODFilterReduce { get { return Convert.ToDouble(rowNDReduceRatio.Properties.Value); } set { rowNDReduceRatio.Properties.Value = value; } }
        public double _InspectLedND_FilterAngle { get { return Convert.ToDouble(rowNDFilterAngle.Properties.Value); } set { rowNDFilterAngle.Properties.Value = value; } }
        public double _InspectOperateMax_Distance { get { return Convert.ToDouble(rowOperateMaxDistance.Properties.Value); } set { rowOperateMaxDistance.Properties.Value = value; } }
        public double _InspectOperateMin_Distance { get { return Convert.ToDouble(rowOperateMinDistance.Properties.Value); } set { rowOperateMinDistance.Properties.Value = value; } }
        public string _InspectOpticalResult { get { return rowInspectResult.Properties.Value.ToString(); } set { rowInspectResult.Properties.Value = value; } }
        public LedSpotInspectionInfomation()
        {
            InitializeComponent();
        }
        public void ChangeSystemLanguage(bool _bsystemlanguage)
        {
            if (!_bsystemlanguage)
            {
                categoryImageProcessResult.Properties.Caption = "Inspection Result";
                rowProductSeries.Properties.Caption = "Product Series";
                rowProductModelName.Properties.Caption = "Product Model Name";
                rowLedSpotBlobInspectHSize.Properties.Caption = "Spot 1 Size[mm]";
                rowLedSpotBlobInspectVSize.Properties.Caption = "Spot 2 Size[mm]";
                rowLedSpotImageBright.Properties.Caption = "Spot Image Bright[˚]";
                rowAlignmentDistance.Properties.Caption = "Spot Eccentric Distance[mm]";
                rowOpticalEccentricAngle.Properties.Caption = "Spot Eccentric Angle[˚]";
                rowOpticalEccentricHorizonAngle.Properties.Caption = "Spot Eccentric Horizon Angle[˚]";
                rowOpticalEccentricVirticalAngle.Properties.Caption = "Spot Eccentric Virtiacl Angle[˚]";
                rowDivergenceAngle.Properties.Caption = "Divergence Angle[˚]";
                rowNDReduceRatio.Properties.Caption = "ND Filter Reduction Ratio";
                rowNDFilterAngle.Properties.Caption = "ND 필터 Estimate Angle";
                rowOperateMinDistance.Properties.Caption = "Min Distance[ND Filter Angle]";
                rowOperateMaxDistance.Properties.Caption = "Max Distance[ND Filter Angle]";
                rowInspectResult.Properties.Caption = "Inspection Result";
            }
            else
            {
                categoryImageProcessResult.Properties.Caption = "투광 검사 결과";
                rowProductSeries.Properties.Caption = "제품 시리즈";
                rowProductModelName.Properties.Caption = "제품 모델명";
                rowLedSpotBlobInspectHSize.Properties.Caption = "Spot 1 크기[mm]";
                rowLedSpotBlobInspectVSize.Properties.Caption = "Spot 2 크기[mm]";
                rowLedSpotImageBright.Properties.Caption = "광원 이미지 밝기";
                rowAlignmentDistance.Properties.Caption = "편심 거리[mm]";
                rowOpticalEccentricAngle.Properties.Caption = "편심 각도[˚]";
                rowOpticalEccentricHorizonAngle.Properties.Caption = "편심 수평각도[˚]";
                rowOpticalEccentricVirticalAngle.Properties.Caption = "편심 수직각도[˚]";
                rowDivergenceAngle.Properties.Caption = "광원 발산 각도[˚]";
                rowNDReduceRatio.Properties.Caption = "ND 필터감쇄율";
                rowNDFilterAngle.Properties.Caption = "ND 필터 예측각도";
                rowOperateMinDistance.Properties.Caption = "동작 거리[ND 필터 각도]";
                rowOperateMaxDistance.Properties.Caption = "최대 거리[ND 필터 각도]";
                rowInspectResult.Properties.Caption = "투광 검사 결과";
            }            
        }
        //public void ChangeResultFontColor(bool _result)
        //{
        //    if (_result)
        //    {                
        //        rowOpticalEccentricAngle.Appearance.BackColor = System.Drawing.Color.Yellow;
        //    }
        //    else
        //    {
        //        rowOpticalEccentricAngle.Appearance.BackColor = System.Drawing.Color.Red;
        //    }
        //    rowOpticalEccentricAngle.Appearance.BackColor2 = System.Drawing.Color.White;
            
        //}
    }
}
