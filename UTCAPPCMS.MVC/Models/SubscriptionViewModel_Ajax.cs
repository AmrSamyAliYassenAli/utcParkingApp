using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTCAPPCMS.MVC.Models
{
    public class SubscriptionViewModel_Ajax
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool IsEnable { get; set; }
        public string Description { get; set; }
        public int? ParkingId { get; set; }
        public List<DurationListViewModel_Ajax> Durationlist { get; set; }
    }
}
