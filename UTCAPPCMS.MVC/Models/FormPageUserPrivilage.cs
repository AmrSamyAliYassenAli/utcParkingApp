using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;

namespace UTCAPPCMS.MVC.Models
{
    public class FormPageUserPrivilage
    {
        public int? Id { get; set; }
        public string FormKey { get; set; }
        public bool View { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Search { get; set; }
        public bool Print { get; set; }

        /*

        public int? FormPagesId { get; set; }
        public virtual FormPages FormPages { get; set; }
        */
    }
}
