using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class ADMSParams
    {
        public bool _enableCheck { get; set; } = false;
        public string _IpAddress { get; set; } = "127.0.0.1";
        public int _port { get; set; } = 3306;
        public string _userID { get; set; } = "autoeqp";
        public string _password { get; set; } = "autoeqp1";
        public int _eqpmentID { get; set; } = 0;
        public string _dbschemaname { get; set; } = "dm";
        public string _equipmentname { get; set; } = "eqp_t00";
        public string _productname { get; set; } = "product_t00";
        public ADMSParams()
        {

        }
    }
}
