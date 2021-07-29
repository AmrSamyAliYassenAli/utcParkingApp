using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;

namespace UTCAPPCMS.MVC.Models
{
    public class Subscription_SubscriptionDurationVM 
    {
        // Subscription
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }

        public bool IsEnable { get; set; }

        public int? ParkingId { get; set; }
        public virtual Parking Sub_Parking { get; set; }

        // SubscriptionDuration
       /* public int? Dur_durationInDays { get; set; }
        public int? Dur_DaysCount { get; set; }
        public int? Dur_HoursCount { get; set; }
        public int? Dur_echicleCount { get; set; }

        public double? Dur_Price { get; set; }
        public double? Dur_PriceMulti { get; set; }

        public bool Dur_AllDays { get; set; }
        public bool Dur_AllTime { get; set; }
        public bool Dur_IsMullti { get; set; }*/

       public List<SubscriptionDurations> SubscriptionDurationslist { get; set; }
    }
}
