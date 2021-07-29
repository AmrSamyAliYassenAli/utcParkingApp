using System;
using System.Collections.Generic;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public class Nationality : BaseModel
    {
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public int? SortNo { get; set; }
    }
}