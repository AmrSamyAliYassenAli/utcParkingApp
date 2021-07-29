using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public partial class ParkingLocations : BaseModel
    {
        public ParkingLocations()
        {
            Siteline = new HashSet<Siteline>();
            TableTariff = new HashSet<TableTariff>();
        }

        public string SiteName { get; set; }
        public string SiteNo { get; set; }
        public string SiteId { get; set; }
        public string Qrcode { get; set; }
       
        public string Block { get; set; }
        public string GpsLat { get; set; }
        public string GpsLong { get; set; }
        public bool IsPublic { get; set; }
        public int? CarCapacity { get; set; }
        public int? FloorsNo { get; set; }
        public string AddressEn { get; set; }
        public string AddressAr { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        
        public string OpenFromTime { get; set; }
       
        public string OpenToTime { get; set; }
        public int? Allowedtimeperminute { get; set; }
        public int? ParkingId { get; set; }
        public virtual Parking Parking { get; set; }
        [Range(1, 17000000000000000000,
        ErrorMessage = "! invalid area")]
        public int? AreaId { get; set; }
        public virtual Area Area { get; set; }
      

        public virtual ICollection<Siteline> Siteline { get; set; }
        public virtual ICollection<TableTariff> TableTariff { get; set; }
    }
}
