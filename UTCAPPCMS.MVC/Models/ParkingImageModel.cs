using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;

namespace UTCAPPCMS.MVC.Models
{
    public class ParkingImageModel
    {
        public IFormFile File { set; get; }
        public ParkingImages Parking_Images { set; get; }
    }
}
