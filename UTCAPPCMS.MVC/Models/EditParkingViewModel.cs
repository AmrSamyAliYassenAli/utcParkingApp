using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;

namespace UTCAPPCMS.MVC.Models
{
    public class EditParkingViewModel : BaseModel
    {
        public string ReferanceKey { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string AddressEn { get; set; }
        public string AddressAr { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyPassword { get; set; }
        public string Description { get; set; }
        public IFormFile Logo { get; set; }
        public string LogoPath { get; set; }
        public IFormFile Invoicelogo { get; set; }
        public string InvoiceLogoPath { get; set; }
    }
}
