using System;
using System.Collections.Generic;
using System.Text;

namespace UTCAPPCMS.DAL.ViewModel.Api.App
{
    public class RequestHeader
    {
        public string ip { get; set; }
        public string deviceSerial { get; set; }
        public string notificationToken { get; set; }
        public string osversion { get; set; }
        public string appversion { get; set; }
        public int appLanguage { get; set; }
       
        public int deviceType { get; set; }
        public int userId { get; set; }
        public int parkingId { get; set; }
    }
    public class BaseHeaderRequest
    {
        public RequestHeader requestHeader { get; set; }
    }
}
