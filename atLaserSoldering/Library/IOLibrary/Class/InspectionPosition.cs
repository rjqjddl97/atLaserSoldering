using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class InspectionPosition
    {
        int _isResult = InspectionResult.Ready;

        int _index = 0;

        RectangleF _fOpticalSpotROI = new RectangleF(0, 0, 0, 0);
        PointF _fptBlobCenter = new PointF(-100f, -100f);
        PointF _fptOpticalSpotCenter = new PointF(-100f, -100f);


        int _similarity = 0;
        int _similarity1 = 0;
        int _similarity2 = 0;
        int _similarity3 = 0;
        
        float _fAverageBrightness = 0.0f;
        float _fHorizontalBrightnessA = 0.0f;
        float _fHorizontalBrightnessB = 0.0f;
        float _fVerticalBrightnessA = 0.0f;
        float _fVerticalBrightnessB = 0.0f;
        float _fDistance = 0.0f;
        float _fDistanceX = 0.0f;
        float _fDistanceY = 0.0f;
        float _fHorizontalDeviation = 0.0f;
        float _fVerticalDeviation = 0.0f;
        float _fOpticalSpotSizeX = 0.0f;
        float _fOpticalSpotSizeY = 0.0f;

        INSPECTION_POSITION_MODE _positiontype = INSPECTION_POSITION_MODE.POSITION_INSPECTION_MODE;
        float _positionX = 0F;
        float _positionY = 0F;        
        float _positionZ = 0F;
        float _positionRx = 0F;
        float _positionRy = 0F;
        float _positionRz = 0F;

        float _ND_filter = 0F;

        //INSPECTION_POSITION_MODE _eInspectionPositionMode = INSPECTION_POSITION_MODE.INSPECTION_POSITION_BASE_MODE;

        public INSPECTION_POSITION_MODE ePositionType
        {
            get { return _positiontype; }
            set { _positiontype = value; }
        }
        public float PositionX
        {
            get { return _positionX; }
            set { _positionX = value; }
        }
        public float PositionY
        {
            get { return _positionY; }
            set { _positionY = value; }
        }
        public float PositionZ
        {
            get { return _positionZ; }
            set { _positionZ = value; }
        }
        public float PositionRx
        {
            get { return _positionRx; }
            set { _positionRx = value; }
        }
        public float PositionRy
        {
            get { return _positionRy; }
            set { _positionRy = value; }
        }
        public float PositionRz
        {
            get { return _positionRz; }
            set { _positionRz = value; }
        }
        public float PositionNDFilter
        {
            get { return _ND_filter; }
            set { _ND_filter = value; }
        }
        public RectangleF OpticalSpotROI
        {
            get { return _fOpticalSpotROI; }
            set { _fOpticalSpotROI = value; }
        }

        public PointF OpticalSpotCenter
        {
            get { return _fptOpticalSpotCenter; }
            set { _fptOpticalSpotCenter = value; }
        }

        public PointF BlobCenter
        {
            get { return _fptBlobCenter; }
            set { _fptBlobCenter = value; }
        }        

        public int IsResult
        {
            get { return _isResult; }
            set { _isResult = value; }
        }
                

        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }    

        public int Similarity
        {
            get { return _similarity; }
            set { _similarity = value; }
        }
        public int Similarity1
        {
            get { return _similarity1; }
            set { _similarity1 = value; }
        }

        public int Similarity2
        {
            get { return _similarity2; }
            set { _similarity2 = value; }
        }

        public int Similarity3
        {
            get { return _similarity3; }
            set { _similarity3 = value; }
        }

        public float HorizontalBrightnessA
        {
            get { return _fHorizontalBrightnessA; }
            set { _fHorizontalBrightnessA = value; }
        }

        public float HorizontalBrightnessB
        {
            get { return _fHorizontalBrightnessB; }
            set { _fHorizontalBrightnessB = value; }
        }

        public float VerticalBrightnessA
        {
            get { return _fVerticalBrightnessA; }
            set { _fVerticalBrightnessA = value; }
        }

        public float VerticalBrightnessB
        {
            get { return _fVerticalBrightnessB; }
            set { _fVerticalBrightnessB = value; }
        }

        public float HorizontalDeviation
        {
            get { return _fHorizontalDeviation; }
            set { _fHorizontalDeviation = value; }
        }

        public float VerticalDeviation
        {
            get { return _fVerticalDeviation; }
            set { _fVerticalDeviation = value; }
        }

        public float AverageBrightness
        {
            get { return _fAverageBrightness; }
            set { _fAverageBrightness = value; }
        }
        public float Distance
        {
            get { return _fDistance; }
            set { _fDistance = value; }
        }
        public float DistanceX
        {
            get { return _fDistanceX; }
            set { _fDistanceX = value; }
        }

        public float DistanceY
        {
            get { return _fDistanceY; }
            set { _fDistanceY = value; }
        }


        public float OpticalSpotSizeX
        {
            get { return _fOpticalSpotSizeX; }
            set { _fOpticalSpotSizeX = value; }
        }

        public float OpticalSpotSizeY
        {
            get { return _fOpticalSpotSizeY; }
            set { _fOpticalSpotSizeY = value; }
        }

        public void InitializeInspectionParameters()
        {
            // X, Y, Z 좌표 이외 파라미터 초기화
            _fOpticalSpotROI = new RectangleF(0, 0, 0, 0);
            _fptBlobCenter = new PointF(-100f, -100f);
            _fptOpticalSpotCenter = new PointF(-100f, -100f);

            _isResult = InspectionResult.Ready;
            _fHorizontalBrightnessA = 0;
            _fHorizontalBrightnessB = 0;
            _fHorizontalDeviation = 0;
            _fVerticalBrightnessA = 0;
            _fVerticalBrightnessB = 0;
            _fVerticalDeviation = 0;
            _fAverageBrightness = 0;
            _fOpticalSpotSizeX = 0;
            _fOpticalSpotSizeY = 0;
            _fDistanceX = 0;
            _fDistanceY = 0;
            _similarity1 = 0;
            _similarity2 = 0;
            _similarity3 = 0;
        }
    }
}
