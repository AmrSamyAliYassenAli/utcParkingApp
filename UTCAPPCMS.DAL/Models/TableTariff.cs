using System;
using System.Collections.Generic;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public partial class TableTariff : BaseModel
    {
        public string NameEn { get; set; }
        public double? HourPrice { get; set; }
        public int? ParkingLocationId { get; set; }

        public virtual ParkingLocations ParkingLocation { get; set; }
    }
}
