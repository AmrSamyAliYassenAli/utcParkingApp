using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTCAPPCMS.MVC.Models
{
    public class GroupPrivilageViewModel_ajax
    {
        public int? Id { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public bool IsEnable { get; set; }
        public List<FormPageUserPrivilage> Priv_tableData { get; set; }

        
    }
}
