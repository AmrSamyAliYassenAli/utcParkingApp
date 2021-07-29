using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public class CustomerVehicles : BaseModel
    {
        public string PlateNumber { get; set; }
        public string Image { get; set; }
        public string VechicleType { get; set; }

        public bool? IsVip { get; set; }

        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}