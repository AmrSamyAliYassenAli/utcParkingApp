using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public partial class Parking : BaseModel
    {
        public Parking()
        {
            AdminUsers = new HashSet<AdminUsers>();
            ParkingImages = new HashSet<ParkingImages>();
            ParkingLocations = new HashSet<ParkingLocations>();
        }
        public string ReferanceKey { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string AddressEn { get; set; }
        public string AddressAr { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        [NotMapped]
        public string CompanyEmail { get; set; }
        [NotMapped]
        public string CompanyPassword { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Invoicelogo { get; set; }

        public virtual ICollection<AdminUsers> AdminUsers { get; set; }
        public virtual ICollection<ParkingImages> ParkingImages { get; set; }
        public virtual ICollection<ParkingLocations> ParkingLocations { get; set; }
        public virtual ICollection<GroupPrivilage> GroupPrivilages { get; set; }
    }
}