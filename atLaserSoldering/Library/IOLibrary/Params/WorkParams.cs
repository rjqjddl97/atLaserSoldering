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

        public string _PCBModelName { get; set; } = "PCB260001";
        public string _PCBLayoutPath { get; set; } = string.Empty;
        public double _PCB_HorizontalSize { get; set; } = 200D;
        public double _PCB_VerticalSize { get; set; } = 200D;

        // 투광 편심 검사 파라미터
        public bool _PCBAlignVisionEnable { get; set; } = false;
        public int _AlignInspectionExposureTime { get; set; } = 7000;
        public int _SolderInspectionExposureTime { get; set; } = 7000;
        public int _ImageAcquisitionDelaytime { get; set;} = 1000;
        public string _InspectAlignVisionPath { get; set; } = string.Empty;
        public string _InspectSolderingVisionPath { get; set; } = string.Empty;
        public int _InspectionLightBright { get; set; } = 256;
        public int _SolderingInspectionLightBright { get; set; } = 256;
        public int _SolderingOutputWaitTime { get; set; } = 200;

        public double _FiducialMarkA_X { get; set; } = 0;
        public double _FiducialMarkA_Y { get; set; } = 0;

        public double _FiducialMarkB_X { get; set; } = 0;
        public double _FiducialMarkB_Y { get; set; } = 0;

        // 
        public bool _SolderingInspectVisionEnable { get; set; } = false;
        public bool _SolderingProcessEnable { get; set; } = false;
        public bool _UseLaserEnable { get; set; } = false;
        public bool _UseFeederEnable { get; set; } = false;
        public int _LaserOffWaitTime { get; set; } = 200;

        public int _WorkPositionsCount { get; set; } = 0;

        //public List<PositionParams> _WorkPositionParams = new List<PositionParams>();
        List<SolderingPosition> _listSolderPositionParams = new List<SolderingPosition>();
                
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
        public List<SolderingPosition> SolderPositionParams
        {
            get { return _listSolderPositionParams; }
            set { _listSolderPositionParams = value; }
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

            temp._PCBModelName = this._PCBModelName;
            temp._PCB_HorizontalSize = this._PCB_HorizontalSize;
            temp._PCB_VerticalSize = this._PCB_VerticalSize;

            temp._PCBAlignVisionEnable = this._PCBAlignVisionEnable;
            temp._AlignInspectionExposureTime = this._AlignInspectionExposureTime;
            temp._InspectionLightBright = this._InspectionLightBright;
            temp._InspectAlignVisionPath = this._InspectAlignVisionPath;
            temp._SolderInspectionExposureTime = this._SolderInspectionExposureTime;            
            temp._SolderingInspectionLightBright = this._SolderingInspectionLightBright;
            temp._InspectSolderingVisionPath = this._InspectSolderingVisionPath;
            temp._ImageAcquisitionDelaytime = this._ImageAcquisitionDelaytime;            

            temp._SolderingInspectVisionEnable = this._SolderingInspectVisionEnable;
            temp._SolderingProcessEnable = this._SolderingProcessEnable;
            temp._UseLaserEnable = this._UseLaserEnable;
            temp._UseFeederEnable = this._UseFeederEnable;
            temp._SolderingOutputWaitTime = this._SolderingOutputWaitTime;

            for (i = 0; i < this._listSolderPositionParams.Count; ++i)
                temp._listSolderPositionParams.Add(this._listSolderPositionParams[i]);

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
