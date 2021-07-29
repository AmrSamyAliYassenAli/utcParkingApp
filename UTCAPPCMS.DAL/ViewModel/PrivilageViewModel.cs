using System;
using System.Collections.Generic;
using System.Text;

namespace UTCAPPCMS.DAL.ViewModel
{
    public class PrivilageViewModel
    {
        public bool? User_View { get; set; }
        public bool? User_Add { get; set; }
        public bool? User_Edit { get; set; }
        public bool? User_Delete { get; set; }
        public bool? User_Search { get; set; }
        public bool? User_Print { get; set; }
    }
}
