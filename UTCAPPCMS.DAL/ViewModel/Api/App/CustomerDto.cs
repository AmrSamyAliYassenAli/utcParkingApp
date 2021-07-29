using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using UTCAPPCMS.DAL.ViewModel.Api.App;

namespace UTCAPPCMS.DAL.Models
{
    public class CustomerDto : BaseHeaderRequest
    {
        public CustomerInfoDto data { get; set; }
    }
    public class CustomerInfoDto  
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string Derpartment { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string StafNo { get; set; }
        public string WorkPosition { get; set; }
        public string CivilID { get; set; }
        public string Password { get; set; }
        public string newPassword { get; set; }
        public string profileImage { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? NationalityFkID { get; set; }

    }

    public class CustomerPasswordDto : BaseHeaderRequest
    {
        public CustomerPassword data { get; set; }
    }
    public class CustomerPassword
    {
        public string Password { get; set; }
        public string newPassword { get; set; }

    }

    public class RegisterDto : BaseHeaderRequest
    {
        public Register data { get; set; }
       
    }
    public class Register 
    {
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ProfileImageDto : BaseHeaderRequest
    {
        public ProfileImage data { get; set; }
    }
    public class ProfileImage
    {
        public string profileImage { get; set; }

    }

    public class ApiLoginDto : BaseHeaderRequest
    {
        public ApiLogin data { get; set; }
        
        
    }
    public class ApiLogin  
    {
        public string userName { get; set; }
        public string password { get; set; }

    }
    public class APILoginReturn
    {
        public string token { get; set; }
        public Boolean isUpdate { get; set; }
        public Boolean isOptional { get; set; }
        public string messageAr { get; set; }
        public string messageEn { get; set; }
        public string iosLink { get; set; }
        public string androidLink { get; set; }
        
        public string currentVersionNo { get; set; }
       

    }
    public class ContactUsDto : BaseHeaderRequest
    {
        public ContactUsApi data { get; set; }

    }

    public class ContactUsApi
    {
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }

    public class PasswordForgetDto : BaseHeaderRequest
    {
        public PasswordForgetApi data { get; set; }

    }

    public class PasswordForgetApi
    {
        public string EmailOrMobile { get; set; }
    }

    public class TransactionDetailsSearchDto : BaseHeaderRequest
    {
        public TransactionDetailsSearch data { get; set; }
    }

    public class TransactionDetailsSearch
    {
        public string PlateNumber { get; set; }
    }

    public class CustomerActivationDto : BaseHeaderRequest
    {
        public CustomerActivation data { get; set; }
    }

    public class CustomerActivation
    {
        public string ActivationCode { get; set; }
    }

    public class TransactionDetailsReturnDto : BaseHeaderResponse
    {
        public TransactionDetailsDto data { get; set; }
    }
    public class TransactionDetailsDto
    {
        public int? TransactionId { get; set; }
        public long? SiteId { get; set; }
        public string SiteName { get; set; }
        public string TransactionTime { get; set; }
        public string PlateNumber { get; set; }
        public string PlateCity { get; set; }
        public string PlateCountry { get; set; }
        public string PlateType { get; set; }
        public string PlateCategory { get; set; }
        public string PlateColor { get; set; }
        public int? LaneNo { get; set; }
        public int? TimeLeave { get; set; }
        public string LaneName { get; set; }
        public string Duration { get; set; }
        public string Amount { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleModel { get; set; }
        public int? VehicleColor { get; set; }
        public int? VehicleClassification { get; set; }
        public DateTime? ReciveDate { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
    }

    public class PaymentProcessedDto : BaseHeaderResponse
    {
        public PaymentProcessed data { get; set; }

    }
    public class PaymentProcessed
    {
        public string URL { get; set; }
    }

    public class LoginDto : BaseHeaderRequest
    {
        public Login data { get; set; }
        
    }
    public class Login 
    {
        public string user { get; set; }
        public string password { get; set; }
    }


    public class LoginReturnDto : BaseHeaderResponse
    {
        public LoginReturn data { get; set; }
    }
    public class LoginReturn
    {

        public int Id { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string Derpartment { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string StafNo { get; set; }
        public string WorkPosition { get; set; }
        public string CivilID { get; set; }
        public string profileImage { get; set; }
        public DateTime? BirthDate { get; set; }

        public string SubName { get; set; }
        public string SubDuration { get; set; }
        public DateTime? SubActivatedDate { get; set; }
        public DateTime? SubExpireDate { get; set; }
        public double? SubPrice { get; set; }
        public bool? IsActivated { get; set; }
    }

    public class CustomerVehiclesReturnDto : BaseHeaderResponse
    {
        public List<CustomerVehiclesReturn> data { get; set; }
    }
    public class CustomerVehiclesReturn
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; }
        public string Image { get; set; }
        public string VechicleType { get; set; }
        public bool? IsVip { get; set; }
    }

    public class CustomerTransactionsReturnDto : BaseHeaderResponse
    {
        public List<CustomerTransactionsReturn> data { get; set; }
    }
    public class CustomerTransactionsReturn
    {
        public long? TransactionId { get; set; }
        public long? SiteId { get; set; }
        public string SiteName { get; set; }
        public string TransactionTime { get; set; }
        public string PlateNumber { get; set; }
        public string PlateCity { get; set; }
        public string PlateCountry { get; set; }
        public string PlateType { get; set; }
        public string PlateCategory { get; set; }
        public string PlateColor { get; set; }
        public int? LaneNo { get; set; }
        public int? TimeLeave { get; set; }
        public string LaneName { get; set; }
        public string Duration { get; set; }
        public string Amount { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleModel { get; set; }
        public int? VehicleColor { get; set; }
        public int? VehicleClassification { get; set; }
        public DateTime? ReciveDate { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
    }
}