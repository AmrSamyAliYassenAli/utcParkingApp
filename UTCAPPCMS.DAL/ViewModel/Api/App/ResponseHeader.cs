using System;
using System.Collections.Generic;
using System.Text;

namespace UTCAPPCMS.DAL.ViewModel.Api.App
{
    public class ResponseHeader
    {
        public int ResponseCode { get; set; }

        public string responseMessage { get; set; }

        public string ResponseRemark { get; set; }

    }
    public class BaseHeaderResponse
    {
        public ResponseHeader responseHeader { get; set; }
    }
}
