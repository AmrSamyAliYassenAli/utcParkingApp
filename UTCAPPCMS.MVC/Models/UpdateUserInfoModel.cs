using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;

namespace UTCAPPCMS.MVC.Models
{
    public class UpdateUserInfoModel
    {
        public IFormFile File   { get; set; }
        public AdminUsers Admin { set; get; }
    }
}
