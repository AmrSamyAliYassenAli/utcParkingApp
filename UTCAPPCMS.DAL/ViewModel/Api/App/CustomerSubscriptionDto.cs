using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using UTCAPPCMS.DAL.ViewModel.Api.App;

namespace UTCAPPCMS.DAL.Models
{
    public class CustomerSubscriptionDto : BaseHeaderRequest
    {
        public CustomerSubscription data { get; set; }
    }

    public class CustomerSubscription
    {
        public int? SubscriptionDurationId { get; set; }
        public int? PaymentTypeId { get; set; }
    }
}