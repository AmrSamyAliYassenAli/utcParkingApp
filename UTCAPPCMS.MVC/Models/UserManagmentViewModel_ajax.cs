using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;

namespace UTCAPPCMS.MVC.Models
{
    public class UserManagmentViewModel_ajax
    {
        public int? Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }     
        public string Password { get; set; }
        public string Image { get; set; }
        public bool IsEnable { get; set; }
        public int? UserTypeId { get; set; }
        public int? PrivilageGroupId { get; set; }
        public List<FormPageUserPrivilage> Priv_tableData { get; set; }
    }
}
