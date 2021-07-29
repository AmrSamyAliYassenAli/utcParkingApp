using System;
using System.Collections.Generic;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public partial class TransactionStatus : BaseModel
    {
        public TransactionStatus()
        {
            TableTransactionDetail = new HashSet<TableTransactionDetail>();
        }

        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string IsoCode { get; set; }
        public bool? IsPublic { get; set; }

        public virtual ICollection<TableTransactionDetail> TableTransactionDetail { get; set; }
    }
}
