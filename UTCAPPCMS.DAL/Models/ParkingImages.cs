using System;
using System.Collections.Generic;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public partial class ParkingImages : BaseModel
    {
        public string Path { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        
        public int? OrderNo { get; set; }
        public bool? IsMain { get; set; }
        public int? ParkingId { get; set; }
        public virtual Parking Parking { get; set; }
    }
}
