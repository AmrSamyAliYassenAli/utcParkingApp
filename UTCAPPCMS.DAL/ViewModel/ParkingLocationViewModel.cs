using System;
using System.Collections.Generic;
using System.Text;

namespace UTCAPPCMS.DAL.ViewModel
{
    public class ParkingLocationViewModel
    {
        public int SiteId { get; set; }
        public string LogoUrl { get; set; }
        public string Name { get; set; }
        public int? locationId { get; set; }
        public string PlateNumber { get; set; }
        public string PlatePrefix { get; set; }
        public long? transactionId { get; set; }
        public bool IsDetails { get; set; }

        public bool NoTrans { get; set; }

        public string TimeIn { get; set; }

        public CalculateDuartion calculate { get; set; }
    }

    public class CalculateDuartion
    {
        public string DurationHours { get; set; }

        public string Duration { get; set; }

        public string Price { get; set; }
    }
}
