using System;
using System.Collections.Generic;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public class ForgetPasswordAdminUser : BaseModel
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsSent { get; set; }
        public bool IsHold { get; set; }
        public string SentMessage { get; set; }
        public DateTime? SentDate { get; set; }
        public int AdminUsersId { get; set; }

    }
}
