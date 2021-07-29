using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;

namespace UTCAPPCMS.MVC.Models
{
    public class FilterModel
    {
        public string Phone { get; set; }
        public DateTime? FromCreateDate { get; set; }
        public DateTime? ToCreateDate { get; set; }
        public DateTime? FromExpireDate { get; set; }
        public DateTime? ToExpireDate { get; set; }
        public int SubscriptionType { get; set; }
        public int SubscriptionDurationID { get; set; }
        public List <Customer> Customers { get; set; }
    }
}
