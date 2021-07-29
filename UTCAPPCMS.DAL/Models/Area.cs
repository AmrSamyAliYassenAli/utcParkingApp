using System;
using System.Collections.Generic;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public partial class Area :BaseModel
    {
        public Area()
        {
            ParkingLocations = new HashSet<ParkingLocations>();
        }

        public string NameEn { get; set; }
        public string NameAr { get; set; }

        public virtual ICollection<ParkingLocations> ParkingLocations { get; set; }
    }
}
