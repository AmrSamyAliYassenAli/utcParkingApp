using System;
using System.Collections.Generic;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public class AppSetting : BaseModel
    {
        public string shareLink { get; set; }
        public string linkedIn { get; set; }
        public string pinterest { get; set; }
        public string contactUsEmail { get; set; }
        public string contactUsPhone { get; set; }
        public string webSite { get; set; }
        public string facebook { get; set; }
        public string twitter { get; set; }
        public string instagram { get; set; }
        public string youtube { get; set; }

        public string aboutUsEn { get; set; }
        public string howItWorksEn { get; set; }
        public string termConditionEn { get; set; }
        public string shareMessageEn { get; set; }

        public string aboutUsAr { get; set; }
        public string howItWorksAr { get; set; }
        public string termConditionAr { get; set; }
        public string shareMessageAr { get; set; }

        public int ParkingId { get; set; }
    }
}
