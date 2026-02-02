using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class ADMSEquipmentInfo
    {
        public string EquipmentID { get; set; } = "0";

        public DateTime Time { get; set; } = DateTime.Now;
        public ADMSEquipmentEvent Event { get; set; } = ADMSEquipmentEvent.STATE;
        public ADMSEquipmentEventSubState EventSubState { get; set; } = ADMSEquipmentEventSubState.START;

        public string WorkerID { get; set; } = string.Empty;
        public string WorkerName { get; set; } = string.Empty;
        public string EventSubCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string JobInformation { get; set; } = "00000000-0000";
        public string RecipeName { get; set; } = string.Empty;
        public string ItemNo { get; set; } = string.Empty;
        public string CountOrder { get; set; } = string.Empty;
        public string CountOK { get; set; } = string.Empty;
        public string CountNG { get; set; } = string.Empty;
        public string CountDisuse { get; set; } = string.Empty;
        public string Temperature { get; set; } = string.Empty;
        public string Humidity { get; set; } = string.Empty;
        public string Vibrate { get; set; } = string.Empty;

        public ADMSEquipmentInfo()
        {

        }
    }
}
