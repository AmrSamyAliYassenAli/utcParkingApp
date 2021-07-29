using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.ViewModel.Api;
using UTCAPPCMS.DAL.ViewModel.Api.App;

namespace UTCAPPCMS.MVC.Helpers
{
    public class AutoMapperHelper : Profile
    {
        public AutoMapperHelper()
        {
            CreateMap<TransactionDetail,TableTransactionDetail>();
            CreateMap<TableTransactionDetail, TransactionDetail>();

            CreateMap<CustomerDto, Customer>();
            CreateMap<CustomerInfoDto, Customer>();
            CreateMap<RegisterDto, Customer>();
            CreateMap<Register, Customer>();
            
            CreateMap<CustomerSubscription, CustomerSubscriptions>();
            CreateMap<CustomerVehicleDto, CustomerVehicles>();
            CreateMap<VehicleDto, CustomerVehicles>();
            CreateMap<Customer, LoginReturn>();
            CreateMap<ContactUsApi, ContactUs>();

            CreateMap<PaymentType, SettingListReturn>();
            CreateMap<SubscriptionDurations, DurationsListReturn>();
            CreateMap<Subscription, SubscriptionsListReturn>();
            CreateMap<ParkingImages, SliderListReturn>();
            CreateMap<ParkingLocations, ParkingListReturn>();
            CreateMap<Parking, ParkingDto>();
            CreateMap<TableTariff, TableTariffDto>();

            CreateMap<CustomerVehicles, CustomerVehiclesReturn>();
            CreateMap<TableCollectionTransaction, CustomerTransactionsReturn>();
            CreateMap<AppSetting, AppSettingReturn>();
        }
    }
}