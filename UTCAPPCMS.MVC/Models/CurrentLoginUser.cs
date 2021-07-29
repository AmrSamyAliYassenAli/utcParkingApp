using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTCAPPCMS.MVC.Models
{
    public class CurrentLoginUser
    {
        public int UserId { get; set; }
        public int? ParkingId { get; set; }
        public int UserTypeId { get; set; }
        public int? PrivilageGroupId { get; set; }
        public string UserName { get; set; }
        public string UserImg { get; set; }
    }
}
