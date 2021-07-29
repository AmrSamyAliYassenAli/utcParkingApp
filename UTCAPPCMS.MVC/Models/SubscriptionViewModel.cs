using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;

namespace UTCAPPCMS.MVC.Models
{
    public class SubscriptionViewModel// : BaseModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool IsEnable { get; set; }
        public string Description { get; set; }
        public int? ParkingId { get; set; }
        public List<SubscriptionDurations> Durationlist { get; set; }
    }
}
