using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class SystemParams
    {
        public CameraParams _cameraParams = new CameraParams();
        public CalibrationParams _calibrationParams = new CalibrationParams();
        public MotionParams _motionParams = new MotionParams();
        public AiCParams _AiCParams = new AiCParams();
        public RemoteIOParams _remoteIOParams = new RemoteIOParams();
        public ADMSParams _admsParams = new ADMSParams();
        public string LatestUsedRecipe = string.Empty;

        public bool _saveResultLEDMeasurement { get; set; } = false;
        public bool _saveResultStatistics { get; set; } = true;

        public bool _SystemLanguageKoreaUse { get; set; } = true;
        public int InspectionBinaryThreshold { get; set; } = 70;
        public int InspectionBlobSizeMinimum { get; set; } = 10000;
        public int InspectionBlobSizeMaximum { get; set; } = 200000;
        public int InspectionOpticalSpotCenterX { get; set; } = 813;
        public int InspectionOpticalSpotCenterY { get; set; } = 615;
        public float InspectionDistanceThreshold { get; set; } = 500F;
        public Rectangle InspectionPassArea { get; set; } = new Rectangle(250, 100, 480, 860);
        public bool _bJobWorkInfomationEnable { get; set; } = false;
        public double InspectionProductsOffset { get; set; } = 229D;
        public double InspectionGlassOffset { get; set; } = 54D;
        public double InspectionReflectorsOffset { get; set; } = 80D;
        public SystemParams()
        {

        }
    }
}
