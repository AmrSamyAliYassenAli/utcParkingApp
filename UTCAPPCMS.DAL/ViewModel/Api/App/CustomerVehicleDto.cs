using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using UTCAPPCMS.DAL.ViewModel.Api.App;

namespace UTCAPPCMS.DAL.Models
{
    public class CustomerVehicleDto : BaseHeaderRequest
    {
        public VehicleDto data { get; set; }
        
    }

    public class VehicleDto  
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; }
        public string Image { get; set; }
        public string VechicleType { get; set; }
        public bool? IsVip { get; set; }
    }


}