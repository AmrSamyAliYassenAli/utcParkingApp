using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public class CustomerSubscriptions : BaseModel
    {
        public string PaymentReference { get; set; }

        public DateTime? ActivatedDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public TimeSpan? FromHours { get; set; }
        public TimeSpan? ToHours { get; set; }

        public double? Price { get; set; }

        public bool IsActivated { get; set; }
        public bool? IsPaid { get; set; }

        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int? SubscriptionDurationId { get; set; }
        public virtual SubscriptionDurations SubscriptionDuration { get; set; }

        public int? PaymentTypeId { get; set; }
        public virtual PaymentType PaymentType { get; set; }
    }
}