using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public class PasswordForget : BaseModel
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsSent { get; set; }
        public bool IsHold { get; set; }
        public DateTime? SentDate { get; set; }
        public string MessageResponse { get; set; }
        public int CustomerId { get; set; }
    }
}