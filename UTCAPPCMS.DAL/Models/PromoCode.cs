using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public class PromoCode : BaseModel
    {
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Code { get; set; }

        public int? NoOfUse { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public int? ParkingLocationsID { get; set; }
        public virtual ParkingLocations ParkingLocations { get; set; }
    }
}