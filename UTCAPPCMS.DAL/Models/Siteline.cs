using System;
using System.Collections.Generic;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public partial class Siteline : BaseModel
    {
        public string LineName { get; set; }
        public string LineId { get; set; }
        public int? LineNumber { get; set; }
        public string LineType { get; set; }
        public string LineDpuid { get; set; }
        public string LineDpuname { get; set; }
        public int? ParkingLocationId { get; set; }

        public virtual ParkingLocations ParkingLocation { get; set; }
    }
}
