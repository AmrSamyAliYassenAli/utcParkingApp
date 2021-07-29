using System;
using System.Collections.Generic;
using System.Text;
using UTCAPPCMS.DAL.Models;

namespace UTCAPPCMS.DAL.ViewModel.Api.App
{
    public class AppSettingReturnDto : BaseHeaderResponse
    {
        public AppSettingReturn data { get; set; }
    }

    public class AppSettingReturn
    {
        int Id { get; set; }
        public string shareLink { get; set; }
        public string linkedIn { get; set; }
        public string pinterest { get; set; }
        public string contactUsEmail { get; set; }
        public string contactUsPhone { get; set; }
        public string webSite { get; set; }
        public string facebook { get; set; }
        public string twitter { get; set; }
        public string instagram { get; set; }
        public string youtube { get; set; }

        public string aboutUsEn { get; set; }
        public string howItWorksEn { get; set; }
        public string termConditionEn { get; set; }
        public string shareMessageEn { get; set; }

        public string aboutUsAr { get; set; }
        public string howItWorksAr { get; set; }
        public string termConditionAr { get; set; }
        public string shareMessageAr { get; set; }

        public int ParkingId { get; set; }
    }
    public class SettingListDto :BaseHeaderRequest
    {
        public SettingDataDto data { get; set; }
    }

    public class SettingDataDto  
    {
        public int id { get; set; }
    }
    public class SettingListReturnDto : BaseHeaderResponse
    {
        public List<SettingListReturn> data { get; set; }
    }
    public class SettingListReturn
    {
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Image { get; set; }
        public string Key { get; set; }
        public int SortNo { get; set; }
        public Boolean IsPublic { get; set; }
    }

    public class DurationsListReturnDto : BaseHeaderResponse
    {
        public List<DurationsListReturn> data { get; set; }
    }
    public class DurationsListReturn
    {
        public int Id { get; set; }
        public int? durationInDays { get; set; }
        public int? DaysCount { get; set; }
        public int? HoursCount { get; set; }
        public int? VechicleCount { get; set; }

        public double? Price { get; set; }
        public double? PriceMulti { get; set; }

        public bool? AllDays { get; set; }
        public bool? AllTime { get; set; }
        public bool? IsMullti { get; set; }
    }

    public class SubscriptionsListReturnDto : BaseHeaderResponse
    {
        public List<SubscriptionsListReturn> data { get; set; }
    }
    public class SubscriptionsListReturn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }

    public class SliderListReturnDto : BaseHeaderResponse
    {
        public List<SliderListReturn> data { get; set; }
    }
    public class SliderListReturn
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int? OrderNo { get; set; }
        public bool? IsMain { get; set; }
    }

    public class ParkingListReturnDto : BaseHeaderResponse
    {
        public List<ParkingListReturn> data { get; set; }
    }
    public class ParkingListReturn
    {
        public int Id { get; set; }
        public string SiteName { get; set; }
        public string SiteNo { get; set; }
        public string Qrcode { get; set; }
        public string Block { get; set; }
        public string GpsLat { get; set; }
        public string GpsLong { get; set; }
        public bool? IsPublic { get; set; }
        public int? CarCapacity { get; set; }
        public int? FloorsNo { get; set; }
        public string AddressEn { get; set; }
        public string AddressAr { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public TimeSpan? OpenFromTime { get; set; }
        public TimeSpan? OpenToTime { get; set; }
        public int? Allowedtimeperminute { get; set; }

        public ICollection<TableTariffDto> tableTariff { get; set; }
        public ParkingDto parking { get; set; }
    }

    public class ParkingDto
    {
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string AddressEn { get; set; }
        public string AddressAr { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Invoicelogo { get; set; }
    }
    public class TableTariffDto
    {
        public string NameEn { get; set; }
        public double? HourPrice { get; set; }
    }
}