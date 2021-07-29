using System;
using System.Collections.Generic;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public class UserPrivilage : BaseModel
    {
  

        public bool? View { get; set; }
        public bool? Add { get; set; }
        public bool? Edit { get; set; }
        public bool? Delete { get; set; }
        public bool? Search { get; set; }
        public bool? Print { get; set; }

        public int? FormPagesId { get; set; }
        public virtual FormPages FormPages { get; set; }

        public int? AdminUsersId { get; set; }
        public virtual AdminUsers AdminUsers { get; set; }
    }
}
