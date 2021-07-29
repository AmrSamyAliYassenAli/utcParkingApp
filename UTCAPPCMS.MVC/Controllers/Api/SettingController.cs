using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.Repository.Interfaces.Api;
using UTCAPPCMS.DAL.ViewModel.Api;
using UTCAPPCMS.DAL.ViewModel.Api.App;

namespace UTCAPPCMS.MVC.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly IAppRepository _appRepository;
        private readonly IMapper _mapper;
        string _correntdomainname = "";

        public SettingController(IAppRepository appRepository, IMapper mapper, IConfiguration config)
        {
            _appRepository = appRepository;
            _mapper = mapper;
            _correntdomainname = config.GetSection("AppSettings:CurrentDomain").Value;
        }
        [HttpPost("GetAppSetting")]
        public async Task<AppSettingReturnDto> GetAppSetting([FromBody] SettingListDto requestBody)
        {
     
            AppSettingReturnDto appSettingReturnDto = new AppSettingReturnDto();
            try
            {
                var returnData = await _appRepository.GetAppSetting(requestBody);
                var returnData2 = _mapper.Map<AppSettingReturn>(returnData);

                appSettingReturnDto.data = returnData2;
                appSettingReturnDto.responseHeader = await _appRepository.BuildSuccesResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.SuccessgetDataMessageEn, ResponseCodeHelper.SuccessgetDataMessageAr, ResponseCodeHelper.SuccessMessageEn, requestBody.requestHeader);

                return appSettingReturnDto;
            }
            catch (Exception exception)
            {

                appSettingReturnDto.data = null;
                appSettingReturnDto.responseHeader= await _appRepository.BuildExceptionResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, exception, requestBody.requestHeader);
                return appSettingReturnDto;

            }
            
        }
       
        [HttpPost("GetPaymentTypeList")]
        public async Task<SettingListReturnDto> GetPaymentTypeList([FromBody] SettingListDto requestBody)
        {
            SettingListReturnDto settingListReturnDto = new SettingListReturnDto();
            try
            {
                var returnData = await _appRepository.GetPaymentTypeList(requestBody);
                var returnData2 = _mapper.Map<List<SettingListReturn>>(returnData);

                foreach (SettingListReturn x in returnData2)
                {
                    x.Image = _correntdomainname + x.Image;
                }

                settingListReturnDto.data = returnData2;
                settingListReturnDto.responseHeader = await _appRepository.BuildSuccesResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.SuccessgetDataMessageEn, ResponseCodeHelper.SuccessgetDataMessageAr, ResponseCodeHelper.SuccessMessageEn, requestBody.requestHeader);
                return settingListReturnDto;
            }
            catch (Exception exception)
            {
                settingListReturnDto.data = null;
                settingListReturnDto.responseHeader = await _appRepository.BuildExceptionResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, exception, requestBody.requestHeader);
                return settingListReturnDto;
            }
        }

        [HttpPost("GetSubscriptionDurationsList")]
        public async Task<DurationsListReturnDto> GetSubscriptionDurationsList([FromBody] SettingListDto requestBody)
        {
            DurationsListReturnDto settingListReturnDto = new DurationsListReturnDto();
            try
            {
                var returnData = await _appRepository.GetSubscriptionDurationsList(requestBody);
                var returnData2 = _mapper.Map<List<DurationsListReturn>>(returnData);
                settingListReturnDto.data = returnData2;
                settingListReturnDto.responseHeader = await _appRepository.BuildSuccesResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.SuccessgetDataMessageEn, ResponseCodeHelper.SuccessgetDataMessageAr, ResponseCodeHelper.SuccessMessageEn, requestBody.requestHeader);
                return settingListReturnDto;
            }
            catch (Exception exception)
            {
                settingListReturnDto.data = null;
                settingListReturnDto.responseHeader = await _appRepository.BuildExceptionResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, exception, requestBody.requestHeader);
                return settingListReturnDto;
            }
        }

        [HttpPost("GetSubscriptionsList")]
        public async Task<SubscriptionsListReturnDto> GetSubscriptionsList([FromBody] SettingListDto requestBody)
        {
            SubscriptionsListReturnDto settingListReturnDto = new SubscriptionsListReturnDto();
            try
            {
                var returnData = await _appRepository.GetSubscriptionsList(requestBody);
                var returnData2 = _mapper.Map<List<SubscriptionsListReturn>>(returnData);

                foreach (SubscriptionsListReturn x in returnData2)
                {
                    x.Image = _correntdomainname + x.Image;
                }

                settingListReturnDto.data = returnData2;
                settingListReturnDto.responseHeader = await _appRepository.BuildSuccesResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.SuccessgetDataMessageEn, ResponseCodeHelper.SuccessgetDataMessageAr, ResponseCodeHelper.SuccessMessageEn, requestBody.requestHeader);
                return settingListReturnDto;
            }
            catch (Exception exception)
            {
                settingListReturnDto.data = null;
                settingListReturnDto.responseHeader = await _appRepository.BuildExceptionResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, exception, requestBody.requestHeader);
                return settingListReturnDto;
            }
        }

        [HttpPost("GetSliderList")]
        public async Task<SliderListReturnDto> GetSliderList([FromBody] SettingListDto requestBody)
        {
            SliderListReturnDto settingListReturnDto = new SliderListReturnDto();
            try
            {
                var returnData = await _appRepository.GetSliderList(requestBody);
                var returnData2 = _mapper.Map<List<SliderListReturn>>(returnData);

                foreach (SliderListReturn x in returnData2)
                {
                    x.Path = _correntdomainname + x.Path;
                }

                settingListReturnDto.data = returnData2;
                settingListReturnDto.responseHeader = await _appRepository.BuildSuccesResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.SuccessgetDataMessageEn, ResponseCodeHelper.SuccessgetDataMessageAr, ResponseCodeHelper.SuccessMessageEn, requestBody.requestHeader);
                return settingListReturnDto;
            }
            catch (Exception exception)
            {
                settingListReturnDto.data = null;
                settingListReturnDto.responseHeader = await _appRepository.BuildExceptionResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, exception, requestBody.requestHeader);
                return settingListReturnDto;
            }
        }

        [HttpPost("GetParkingLocationList")]
        public async Task<ParkingListReturnDto> GetParkingLocationList([FromBody] SettingListDto requestBody)
        {
            ParkingListReturnDto settingListReturnDto = new ParkingListReturnDto();
            try
            {
                var returnData = await _appRepository.GetParkingList(requestBody);
                var returnData2 = _mapper.Map<List<ParkingListReturn>>(returnData);

                foreach (ParkingListReturn x in returnData2)
                {
                    x.parking.Logo = _correntdomainname + x.parking.Logo;
                    x.parking.Invoicelogo = _correntdomainname + x.parking.Invoicelogo;
                }

                settingListReturnDto.data = returnData2;
                settingListReturnDto.responseHeader = await _appRepository.BuildSuccesResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.SuccessgetDataMessageEn, ResponseCodeHelper.SuccessgetDataMessageAr, ResponseCodeHelper.SuccessMessageEn, requestBody.requestHeader);
                return settingListReturnDto;
            }
            catch (Exception exception)
            {
                settingListReturnDto.data = null;
                settingListReturnDto.responseHeader = await _appRepository.BuildExceptionResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, exception, requestBody.requestHeader);
                return settingListReturnDto;
            }
        }
    }
}