using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public class Subscription : BaseModel
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        public int? ParkingId { get; set; }
        public virtual Parking Parking { get; set; }
    }
}