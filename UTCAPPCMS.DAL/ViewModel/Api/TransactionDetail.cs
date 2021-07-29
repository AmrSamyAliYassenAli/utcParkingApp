using System;
using System.Collections.Generic;
using System.Text;

namespace UTCAPPCMS.DAL.ViewModel.Api
{
    public class TransactionDetail
    {
        public int Id { get; set; }
        public long? TransactionId { get; set; }
        public long? RecordVersionId { get; set; }
        public long? SiteId { get; set; }
        public string SiteName { get; set; }
        public long? Dpuid { get; set; }
        public string Dpuname { get; set; }
        public DateTime? TransactionTime { get; set; }
        public string PlateNumber { get; set; }
        public string PlatePrefix { get; set; }
        public string PlateNumberOnly { get; set; }
        public string PlateCity { get; set; }
        public string PlateCountry { get; set; }
        public string Confidence { get; set; }
        public string PlateType { get; set; }
        public string PlateCategory { get; set; }
        public string PlateColor { get; set; }
        public string AlarmStatusCode { get; set; }
        public string AlarmStatusTitle { get; set; }
        public int? LaneNo { get; set; }
        public string LaneName { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleModel { get; set; }
        public int? VehicleColor { get; set; }
        public int? VehicleClassification { get; set; }
        public int? StatusFkId { get; set; }
        public DateTime? ReciveDate { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public Boolean IsPaid { get; set; }

        public DateTime? PaymentDate { get; set; }
        public Boolean IsSynch { get; set; }
        public DateTime? SynchDate { get; set; }
        public int? ParkingLocationId { get; set; }
        public int? PaymentTypeId { get; set; }

       
        public DateTime? PaymentExpireDate { get; set; }
       
         
    }
}
