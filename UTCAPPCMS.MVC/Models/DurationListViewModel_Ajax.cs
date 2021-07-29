using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTCAPPCMS.MVC.Models
{
    public class DurationListViewModel_Ajax
    {
        public int? Id { get; set; }
        public int durationInDays { get; set; }
        public int DaysCount { get; set; }
        public int HoursCount { get; set; }
        public int VechicleCount { get; set; }
        public int Price { get; set; }
        public int PriceMulti { get; set; }
        public bool AllDays { get; set; }
        public bool AllTime { get; set; }
        public bool IsMullti { get; set; }
    }
}
