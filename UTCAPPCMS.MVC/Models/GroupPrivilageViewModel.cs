using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;

namespace UTCAPPCMS.MVC.Models
{
    public class GroupPrivilageViewModel
    {
        public int? Id { get; set; }
        public UserGroup _UserGroup { get; set; }
        public List<FormPageUserPrivilage> _FormPagePrivilagesList { get; set; }
    }
}
