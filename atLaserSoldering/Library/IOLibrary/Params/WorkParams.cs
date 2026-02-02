using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class WorkParams : ICloneable
    {
        string _strRecipeName = "Recipe001";
        DateTime _createDateTime = DateTime.Now;
        string _strRecipeCreatorName = "소속-이름";

        public int _ProductSeries { get; set; } = 0;                // 0: BTS, 1: BTF, 2: BJ, 3: BJP, 4: BEN,  5: BSP
        public string _ProductModelName { get; set; } = "BTS200-PDTL";
        public int _ProductType { get; set; } = 0;                  // 0: 미러반사형, 1: 한정거리반사, 2: 확산반사, 3: BGS반사, 4: 협시계반사, 5: 투광, 6: 수광
        public float _ProductDistance { get; set; } = 200F;
        public int _ProductOperatingMdoe { get; set; } = 0;         //  0: Light ON, 1: Dark ON
        public int _ProductOutputType { get; set; } = 0;            //  0: NPN, 1: PNP 
        public int _ProductDetectMerterial { get; set; } = 2;       // 0: 없음, 1: 거울, 2: 백색지, 3: 흑색지, 4: 유리체 
        public float _ProductDistanceMargin { get; set; } = 0.2F;
        public bool _ProductMaxDistanceProcessEnable { get; set; } = false;
        public bool _ProductOriginDistanceProcessEnable { get; set; } = false;
        public bool _ProductMinObjectDetectProcessEnable { get; set; } = false;

        // 투광 편심 검사 파라미터
        public bool _LEDInspectionUseEnable { get; set; } = false;
        public float _LEDInspectionShortDistance { get; set; } = 600F;
        public float _LEDInspectionCameraDistance { get; set; } = 150F;
        public int _LEDInspectionExposureTime { get; set; } = 7000;
        public int _LEDInspectionAcquisitionDelaytime { get; set;} = 1000;
        public int _LEDInspectionReferenceThresholdH { get; set; } = 128;
        public int _LEDInspectionReferenceThresholdV { get; set; } = 128;
        public float _LEDInspectionSpotMinSize { get; set; } = 20F;
        public float _LEDInspectionSpotMaxSize { get; set; } = 100F;
        public float _LEDInspectionAlignmentDistance { get; set; } = 2F;
        public float _LEDInspectionDivergenceAngle { get; set; } = 2F;
        public int _LEDInspectionWorkAreaLeft { get; set; } = 200;
        public int _LEDInspectionWorkAreaTop { get; set; } = 300;
        public int _LedInspectionWorkAreaWidth { get; set; } = 600;
        public int _LedInspectionWorkAreaHeight { get; set; } = 600;

        // 단축 거리 검사 파라미터
        public bool _InspectionShortDistanceEnable { get; set; } = false;
        public bool _InspectionPerformanceEnable { get; set; } = false;
        public double _InspectionShortDistance { get; set; } = 500D;
        public int _InspectionSignalDelaytime { get; set; } = 1000;
        public double _InspectionDistance { get; set; } = 50D;
        public double _InspectionAngle { get; set; } = 20D;
        public double _InspectionAngleIncrement { get; set; } = 1D;
        public double _InspectionHeightPosition { get; set; } = 10D;
        public double _InspectionHeightIncrement { get; set; } = 1D;
        public bool _InspectionMaxDistanceUseEnable { get; set; } = false;

        public int _WorkPositionsCount { get; set; } = 0;
        //public List<PositionParams> _WorkPositionParams = new List<PositionParams>();
        List<InspectionPosition> _listInspectionPositions = new List<InspectionPosition>();
        List<Blob> _blobs = new List<Blob>();

        public int ImageCenterX = 800;
        public int ImageCenterY = 600;


        public bool _isBinaryInverse { get; set; } = false;
        int _opticalSpotMultipleInspectionTryNumber = 3;
        int _numberOfOpticalSpot = 0;
        int _referencePositionX = 813;
        int _referencePositionY = 618;


        PointF _fptAreaStart = new PointF(0, 0);
        PointF _fptAreaEnd = new PointF(0, 0);
        PointF _fptAreaCenter = new PointF(0, 0);
        PointF _fptMatchStart = new PointF(0, 0);
        PointF _fptMatchEnd = new PointF(0, 0);

        string _MatchingImagePath = string.Empty;
        int _MatchingSimilarityThreshold = 70;
        public int NumberOfOpticalSpot
        {
            get { return _numberOfOpticalSpot; }
            set { _numberOfOpticalSpot = value; }
        }
        
        public string RecipeName
        {
            get { return _strRecipeName; }
            set { _strRecipeName = value; }
        }

        public DateTime RecipeCreateTime
        {
            get { return _createDateTime; }
            set { _createDateTime = value; }
        }

        public string RecipeCreatorName
        {
            get { return _strRecipeCreatorName; }
            set { _strRecipeCreatorName = value; }
        }

          public List<Blob> Blobs
        {
            get { return _blobs; }
            set { _blobs = value; }
        }
        
        public List<InspectionPosition> InspectionPositions
        {
            get { return _listInspectionPositions; }
            set { _listInspectionPositions = value; }
        }
   
        public PointF AreaStart
        {
            get { return _fptAreaStart; }
            set { _fptAreaStart = value; }
        }

        public PointF AreaEnd
        {
            get { return _fptAreaEnd; }
            set { _fptAreaEnd = value; }
        }
        public PointF MatchStart
        {
            get { return _fptMatchStart; }
            set { _fptMatchStart = value; }
        }

        public PointF MatchEnd
        {
            get { return _fptMatchEnd; }
            set { _fptMatchEnd = value; }
        }
        public PointF AreaCenter
        {
            get { return _fptAreaCenter; }
            set { _fptAreaCenter = value; }
        }
        public int ReferencePositionX
        {
            get { return _referencePositionX; }
            set { _referencePositionX = value; }
        }
        public int ReferencePositionY
        {
            get { return _referencePositionY; }
            set { _referencePositionY = value; }
        }
        public string MatchingImagePath
        {
            get { return _MatchingImagePath; }
            set { _MatchingImagePath = value; }
        }
        public int MatchingSimilarityThreshold
        {
            get { return _MatchingSimilarityThreshold; }
            set { _MatchingSimilarityThreshold = value; }
        }
        public WorkParams()
        {

        }

        public object Clone()
        {
            int i = 0;
            WorkParams temp = new WorkParams();

            temp._strRecipeName = this._strRecipeName;
            temp._createDateTime = this._createDateTime;
            temp._strRecipeCreatorName = this._strRecipeCreatorName;

            temp._ProductSeries = this._ProductSeries;
            temp._ProductModelName = this._ProductModelName;
            temp._ProductDistance = this._ProductDistance;
            temp._ProductOperatingMdoe = this._ProductOperatingMdoe;
            temp._ProductType = this._ProductType;            
            temp._ProductDetectMerterial = this._ProductDetectMerterial;
            temp._ProductDistanceMargin = this._ProductDistanceMargin;
            temp._ProductMaxDistanceProcessEnable = this._ProductMaxDistanceProcessEnable;
            temp._ProductOriginDistanceProcessEnable = this._ProductOriginDistanceProcessEnable;
            temp._ProductMinObjectDetectProcessEnable = this._ProductMinObjectDetectProcessEnable;

            temp._LEDInspectionUseEnable = this._LEDInspectionUseEnable;
            temp._LEDInspectionShortDistance = this._LEDInspectionShortDistance;
            temp._LEDInspectionExposureTime = this._LEDInspectionExposureTime;
            temp._LEDInspectionAcquisitionDelaytime = this._LEDInspectionAcquisitionDelaytime;
            temp._LEDInspectionReferenceThresholdH = this._LEDInspectionReferenceThresholdH;
            temp._LEDInspectionReferenceThresholdV = this._LEDInspectionReferenceThresholdV;
            temp._LEDInspectionSpotMinSize = this._LEDInspectionSpotMinSize;
            temp._LEDInspectionSpotMaxSize = this._LEDInspectionSpotMaxSize;
            temp._LEDInspectionAlignmentDistance = this._LEDInspectionAlignmentDistance;
            temp._LEDInspectionDivergenceAngle = this._LEDInspectionDivergenceAngle;
            temp._LEDInspectionWorkAreaLeft = this._LEDInspectionWorkAreaLeft;
            temp._LEDInspectionWorkAreaTop = this._LEDInspectionWorkAreaTop;
            temp._LedInspectionWorkAreaWidth = this._LedInspectionWorkAreaWidth;
            temp._LedInspectionWorkAreaHeight = this._LedInspectionWorkAreaHeight;
         
            for (i = 0; i < this._listInspectionPositions.Count; ++i)
                temp._listInspectionPositions.Add(this._listInspectionPositions[i]);

            for (i = 0; i < this._blobs.Count; ++i)
                temp._blobs.Add(this._blobs[i]);

            temp._isBinaryInverse = this._isBinaryInverse;

            temp._opticalSpotMultipleInspectionTryNumber = this._opticalSpotMultipleInspectionTryNumber;
            temp._numberOfOpticalSpot = this._numberOfOpticalSpot;
            temp._referencePositionX = this._referencePositionX;
            temp._referencePositionY = this._referencePositionY;

            temp._MatchingImagePath = this._MatchingImagePath;
            return (object)temp;
        }
    }
}
