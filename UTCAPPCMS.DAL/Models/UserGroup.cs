using System;
using System.Collections.Generic;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public class UserGroup : BaseModel
    {
        public UserGroup()
        {
            GroupPrivilage = new HashSet<GroupPrivilage>();
        }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public int? ParkingId { get; set; }
         public virtual Parking Parking { get; set; }

        public virtual ICollection<GroupPrivilage> GroupPrivilage { get; set; }
    }
}
