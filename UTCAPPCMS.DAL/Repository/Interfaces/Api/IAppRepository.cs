using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.ViewModel.Api;
using UTCAPPCMS.DAL.ViewModel.Api.App;

namespace UTCAPPCMS.DAL.Repository.Interfaces.Api
{
    public interface IAppRepository
    {
        Task<AppSetting> GetAppSetting(SettingListDto settingListDto);
        Task<List<TableTransactionDetail>> GetTransactionList (int locationId);
        Task<List<TableTransactionDetail>> AddTransactionList (List<TableTransactionDetail> TransDto);
        Task<List<TableTransactionDetail>> UpdateTransactionList (List<TableTransactionDetail> TransDto);
        Task<Boolean> UpdateTransactionListSync(List<TransactionSynchUpdate> TransDto);

        Task<Customer> RegisterCustomer(RegisterDto requestBody);
        Task<Customer> UpdateCustomer(CustomerDto requestBody);
        Task<Customer> UpdateProfileImage(ProfileImageDto requestBody);
        Task<Customer> ActivateCustomer(CustomerActivationDto requestBody);
        Task<Customer> UpdatePassword(CustomerPasswordDto requestBody);
        Task<Customer> CustomerLogin(LoginDto requestBody);
        Task<ContactUs> ContactUs(ContactUsDto requestBody);
        Task<PasswordForget> PasswordForget(PasswordForgetDto requestBody);

        Task<CustomerSubscriptions> AddUpdateCustomerSubscription(CustomerSubscriptionDto requestBody);
        Task<CustomerVehicles> AddUpdateCustomerVehicle(CustomerVehicleDto requestBody);
        Task<CustomerVehicles> DeleteCustomerVehicle(SettingListDto requestBody);

        Task<LoginReturn> GetCustomerDetails(SettingListDto requestBody);
        Task<TransactionDetailsDto> GetTransactionDetails(TransactionDetailsSearchDto requestBody);
        Task<PaymentProcessed> PaymentProcessed(TransactionDetailsSearchDto requestBody);
        Task<List<CustomerVehicles>> GetCustomerVehiclesList(SettingListDto requestBody);
        Task<List<TableCollectionTransaction>> GetCustomerTransactionsList(SettingListDto requestBody);

        Task<List<PaymentType>> GetPaymentTypeList(SettingListDto requestBody);
        Task<List<SubscriptionDurations>> GetSubscriptionDurationsList(SettingListDto requestBody);
        Task<List<Subscription>> GetSubscriptionsList(SettingListDto requestBody);
        Task<List<ParkingImages>> GetSliderList(SettingListDto requestBody);
        Task<List<ParkingLocations>> GetParkingList(SettingListDto requestBody);

        Task<ResponseHeader> BuildErrorResponse(object Request, object Data, string controllername, string action, string messageen, string messagear, string developerremark, RequestHeader requestHeader);
        Task<ResponseHeader> BuildSuccesResponse(object Request, object Data, string controllername, string action, string messageen, string messagear, string developerremark, RequestHeader requestHeader);
        Task<ResponseHeader> BuildNullObjectResponse(object Request, object Data, string controllername, string action, string messageen, string messagear, string developerremark, RequestHeader requestHeader);
        Task<ResponseHeader> BuildExceptionResponse(object Request, object Data, string controllername, string action, string messageen, string messagear, Exception developerremark, RequestHeader requestHeader);

        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
    }
}