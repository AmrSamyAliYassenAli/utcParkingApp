using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public class Customer : BaseModel
    {
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string Derpartment { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string StafNo { get; set; }
        public string WorkPosition { get; set; }
        public string CivilID { get; set; }
        public string ActivationCode { get; set; }
        public string profileImage { get; set; }

        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }

        public DateTime? BirthDate { get; set; }
        public DateTime? ExpireDate { get; set; }

        public bool? IsActivated { get; set; }

        public int? NationalityId { get; set; }
        public virtual Nationality Nationality { get; set; }

        public int? ParkingId { get; set; }
        public virtual Parking Parking { get; set; }

        public int? SubscriptionDurationId { get; set; }
        public virtual SubscriptionDurations SubscriptionDuration { get; set; }
    }
}