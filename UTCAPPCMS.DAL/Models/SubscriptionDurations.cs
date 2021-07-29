using System;
using System.Collections.Generic;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public partial class SubscriptionDurations : BaseModel
    {
        public int? durationInDays { get; set; }
        public int? DaysCount { get; set; }
        public int? HoursCount { get; set; }
        public int? VechicleCount { get; set; }

        public double? Price { get; set; }
        public double? PriceMulti { get; set; }

        public bool AllDays { get; set; }
        public bool AllTime { get; set; }
        public bool IsMullti { get; set; }
        
        public int? SubscriptionId { get; set; }
        public virtual Subscription Subscription { get; set; }
    }
}