using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;

namespace UTCAPPCMS.MVC.Models
{
    public class CustomerInfo
    {
        public List<CustomerVehicles> VehiclesList { get; set; }
        public Customer customer { get; set; }
        public string SubName { get; set; }
    }
}
