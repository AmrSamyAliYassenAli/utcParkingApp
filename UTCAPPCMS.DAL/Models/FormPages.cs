using System;
using System.Collections.Generic;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public class FormPages : BaseModel
    {
        public FormPages()
        {
            GroupPrivilage = new HashSet<GroupPrivilage>();
            UserPrivilage = new HashSet<UserPrivilage>();
        }
        public string FormKey { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public virtual ICollection<GroupPrivilage> GroupPrivilage { get; set; }
        public virtual ICollection<UserPrivilage> UserPrivilage { get; set; }
    }
}
