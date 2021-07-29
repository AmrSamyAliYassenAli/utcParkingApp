using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public class CustomerSubscriptionLocation : BaseModel
    {
        public int? CustomerSubscriptionID { get; set; }
        public virtual CustomerSubscriptions CustomerSubscription { get; set; }

        public int? ParkingLocationsID { get; set; }
        public virtual ParkingLocations ParkingLocations { get; set; }
    }
}