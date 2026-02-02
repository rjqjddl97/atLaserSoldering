using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class ADMSProductInfo
    {
        public int EquipmentID { get; } = 57;
        public DateTime Time { get; set; } = DateTime.Now;
        public string ElapsedTime { get; set; } = string.Empty;

        public string JobInformation { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Result { get; set; } = "OK";
        public string ErrorStep { get; set; } = string.Empty;
        public string ErrorCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;        

        public ADMSProductInfo()
        {

        }
    }
}
