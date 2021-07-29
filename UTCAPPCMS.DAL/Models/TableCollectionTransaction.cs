using System;
using System.Collections.Generic;
using System.Text;

namespace UTCAPPCMS.DAL.Models
{
    public partial class TableCollectionTransaction : BaseModel
    {
        public string PlateNumber { get; set; }
        public int? InTransactionId { get; set; }
        public int? OutTransactionId { get; set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public int? Duration { get; set; }
        public string DurationText { get; set; }
        public double? TotalMount { get; set; }
        public double? Paid { get; set; }
        public double? Remaining { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? ClosedUserId { get; set; }
        public int? LineNumber { get; set; }
        public bool? IsCollected { get; set; }
        public int? ShiftId { get; set; }
        public int? PaymentTypeId { get; set; }
        public int? CustomerID { get; set; }
    }
}
