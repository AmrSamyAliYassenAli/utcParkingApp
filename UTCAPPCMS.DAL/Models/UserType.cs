using System;
using System.Collections.Generic;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public partial class UserType : BaseModel
    {
        public UserType()
        {
            AdminUsers = new HashSet<AdminUsers>();
        }

        public string NameEn { get; set; }
        public string NameAr { get; set; }

        public virtual ICollection<AdminUsers> AdminUsers { get; set; }
    }
}
