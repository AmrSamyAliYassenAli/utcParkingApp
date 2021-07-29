using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;

namespace UTCAPPCMS.MVC.Models
{
    public class UserManagmentViewModa
    {
       
        public AdminUsers _AdminUsers { get; set; }
        
        public List<FormPageUserPrivilage> _FormPagePrivilagesList { get; set; }
        public bool IsEnable { get; set; }








        
    }
}
