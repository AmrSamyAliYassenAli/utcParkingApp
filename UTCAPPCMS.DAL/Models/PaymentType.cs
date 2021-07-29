using System;
using System.Collections.Generic;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public class PaymentType : BaseModel
    {
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Image { get; set; }
        public string Key { get; set; }
        public int SortNo { get; set; }
        public Boolean IsPublic { get; set; }
    }
}
