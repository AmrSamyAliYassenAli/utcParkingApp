using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTCAPPCMS.MVC.Models
{
    public class ResetAdminPassword
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public DateTime ExpDate { get; set; }
    }
}

