using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace RecipeManager
{
    public class AiCParams
    {
        int _iConnectedNumber = 4;
        //List<string> devidename = new List<string> { "Photo Voltage", "Photo Ampere", "Residual Voltage", "Load Current" };
        //List<int> id = new List<int> { 1, 2, 3, 4 };
        //List<List<string, int>> IDs = new List<System.Collections.Generic.List<string, int>>;
        //string[] _devicename = new string[]{"Photo Voltage","Photo Ampere","Residual Voltage","Load Current"};
        //int[] _idNumber = new int[] {1,2,3,4 };
        public struct _IDs
        {
            public string _devicename;    // { "Photo Voltage", "Photo Ampere", "Residual Voltage", "Load Current" };
            public int _idNumber;         // { 1, 2, 3, 4 };
        }
        public List<_IDs> IDs = new List<_IDs>();
        SerialParams _SerialParam = new SerialParams();
        
        public int ConnectedNumber
        {
            get { return _iConnectedNumber; }
            set { _iConnectedNumber = value; }
        }        
        public SerialParams SerialParameters
        {
            get { return _SerialParam; }
            set { _SerialParam = value; }
        }
        public void SetInitialIDs(int num)
        {
            _IDs stIDs = new _IDs();
            for (int i = 0; i < num; i++)
            {
                stIDs._devicename = "";
                stIDs._idNumber = 1;
                IDs.Add(stIDs);
            }
        }
        public AiCParams()
        {
            
        }
    }
}
