using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public class CustomerDays : BaseModel
    {
        public int? CustomerSubscriptionID { get; set; }
        public virtual CustomerSubscriptions CustomerSubscription { get; set; }

        public int? DayID { get; set; }
        public virtual Day Day { get; set; }
    }
}