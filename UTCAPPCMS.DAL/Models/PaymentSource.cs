using System;
using System.Collections.Generic;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public partial class PaymentSource : BaseModel
    {
        public PaymentSource()
        {
            TableCollectionTransaction = new HashSet<TableCollectionTransaction>();
        }

        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string IsoCode { get; set; }
        public bool? IsPublic { get; set; }

        public virtual ICollection<TableCollectionTransaction> TableCollectionTransaction { get; set; }
    }
}
