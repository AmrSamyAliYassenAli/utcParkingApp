using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.Repository.Interfaces.Api;
using UTCAPPCMS.DAL.ViewModel.Api;
using UTCAPPCMS.DAL.ViewModel.Api.App;
using UTCAPPCMS.MVC.Helpers.Api;

namespace UTCAPPCMS.MVC.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly IAppRepository _appRepository;
        private readonly IMapper _mapper;
        string _correntdomainname = "";
        private IConfiguration _config;
        public AppController(IAppRepository appRepository, IMapper mapper, IConfiguration config)
        {
            _appRepository = appRepository;
            _config = config;
            _mapper = mapper;
            _correntdomainname = _config.GetSection("AppSettings:CurrentDomain").Value;
        }

        //[HttpPost("GetTransactionList")]
        //public async Task<List<TableTransactionDetail>> GetTransactionList ([FromBody] int requestBody)
        //{
        //    try
        //    {
        //        return await _appRepository.GetTransactionList(requestBody);
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        //[HttpPost("AddTransactionList")]
        //public async Task<Response> AddTransactionList ([FromBody] List<TransactionDetail> requestBody)
        //{
        //    Response transReturn = new Response();

        //    try
        //    {
        //        List<TableTransactionDetail> transDetail = _mapper.Map<List<TableTransactionDetail>>(requestBody);

        //        var list = await _appRepository.AddTransactionList(transDetail);

        //        if (list != null)
        //        {
        //            transReturn.responseCode = ResponseCode.SuccessCode;
        //            transReturn.responseMessage = "Success";

        //            return transReturn;

        //        }
        //        else
        //        {
        //            transReturn.responseCode = ResponseCode.ErrorCode;
        //            transReturn.responseMessage = "Error";

        //            return transReturn;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        transReturn.responseCode = ResponseCode.ExceptionCode;
        //        transReturn.responseMessage = exception.Message;

        //        return transReturn;
        //    }
        //}

        //[HttpPost("UpdateTransactionList")]
        //public async Task<Response> UpdateTransactionList([FromBody] List<TransactionDetail> requestBody)
        //{
        //    Response transReturn = new Response();

        //    try
        //    {
        //        List<TableTransactionDetail> transDetail = _mapper.Map<List<TableTransactionDetail>>(requestBody);

        //        var list = await _appRepository.UpdateTransactionList(transDetail);

        //        if (list != null)
        //        {
        //            transReturn.responseCode = ResponseCode.SuccessCode;
        //            transReturn.responseMessage = "Success";

        //            return transReturn;
        //        }
        //        else
        //        {
        //            transReturn.responseCode = ResponseCode.ErrorCode;
        //            transReturn.responseMessage = "Error";

        //            return transReturn;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        transReturn.responseCode = ResponseCode.ExceptionCode;
        //        transReturn.responseMessage = exception.Message;

        //        return transReturn;
        //    }
        //}

        [HttpPost("ApiLogin")]
        public async Task<IActionResult> ApiLogin([FromBody] ApiLoginDto requestBody)
        {
            APILoginReturn loginReturnDto = new APILoginReturn();
            try
            {
                if(requestBody.data.userName=="admin"&& requestBody.data.password=="123456")
                {
                    var calims = new[] {
                                    new Claim(ClaimTypes.NameIdentifier, "1"),
                                     new Claim(ClaimTypes.Name, "admin") };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
                    var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(calims),
                        Expires = DateTime.Now.AddDays(1),
                        SigningCredentials = cred
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    loginReturnDto.token = tokenHandler.WriteToken(token);
                    loginReturnDto.isUpdate = false;
                    loginReturnDto.isOptional = true;


                    return Ok(loginReturnDto);
                }
               else
                {
                    return Unauthorized();
                }
            }
            catch (Exception exception)
            {
                return Unauthorized();
            }
        }


        [HttpPost("RegisterCustomer")]
        public async Task<LoginReturnDto> RegisterCustomer([FromBody] RegisterDto requestBody)
        {
            LoginReturnDto loginReturnDto = new LoginReturnDto();
            try
            {

                var cust = await _appRepository.RegisterCustomer(requestBody);

                if (cust != null)
                {
                    if (cust.Id > 0)
                    {
                        var loginReturn = _mapper.Map<LoginReturn>(cust);

                        loginReturn.profileImage = _correntdomainname + loginReturn.profileImage;

                        loginReturnDto.data = loginReturn;
                        loginReturnDto.responseHeader = await _appRepository.BuildSuccesResponse(requestBody, loginReturn, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.SuccessgetDataMessageEn, ResponseCodeHelper.SuccessgetDataMessageAr, ResponseCodeHelper.SuccessMessageEn, requestBody.requestHeader);
                        return loginReturnDto;
                    }
                    else
                    {
                        if (cust.Id == -1)
                        {
                            loginReturnDto.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ExistMessageEn, ResponseCodeHelper.ExistMessageAr, ResponseCodeHelper.ExistMessageEn, requestBody.requestHeader);
                            return loginReturnDto;
                        }
                        else
                        {
                            loginReturnDto.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, ResponseCodeHelper.ErrorMessageEn, requestBody.requestHeader);
                            return loginReturnDto;
                        }
                    }
                }
                else
                {
                    loginReturnDto.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, ResponseCodeHelper.ErrorMessageEn, requestBody.requestHeader);
                    return loginReturnDto;
                }
            }
            catch (Exception exception)
            {
                loginReturnDto.responseHeader = await _appRepository.BuildExceptionResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, exception, requestBody.requestHeader);
                return loginReturnDto;
            }
        }

        [HttpPost("UpdateCustomer")]
        public async Task<BaseHeaderResponse> UpdateCustomer([FromBody] CustomerDto requestBody)
        {
            BaseHeaderResponse customerReturn = new BaseHeaderResponse();

            try
            {
                var cust = await _appRepository.UpdateCustomer(requestBody);

                if (cust != null)
                {
                    if (cust.Id > 0)
                    {
                        customerReturn.responseHeader = await _appRepository.BuildSuccesResponse(requestBody, customerReturn, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.SuccessgetDataMessageEn, ResponseCodeHelper.SuccessgetDataMessageAr, ResponseCodeHelper.SuccessMessageEn, requestBody.requestHeader);
                        return customerReturn;
                    }
                    else
                    {
                        if (cust.Id == -1)
                        {
                            customerReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.NotExistMessageEn, ResponseCodeHelper.NotExistMessageAr, ResponseCodeHelper.NotExistMessageEn, requestBody.requestHeader);
                            return customerReturn;
                        }
                        else
                        {
                            customerReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, ResponseCodeHelper.ErrorMessageEn, requestBody.requestHeader);
                            return customerReturn;
                        }
                    }
                }
                else
                {
                    customerReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, ResponseCodeHelper.ErrorMessageEn, requestBody.requestHeader);
                    return customerReturn;
                }
            }
            catch (Exception exception)
            {
                customerReturn.responseHeader = await _appRepository.BuildExceptionResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, exception, requestBody.requestHeader);
                return customerReturn;
            }
        }

        [HttpPost("UpdateProfileImage")]
        public async Task<BaseHeaderResponse> UpdateProfileImage([FromBody] ProfileImageDto requestBody)
        {
            BaseHeaderResponse customerReturn = new BaseHeaderResponse();

            try
            {
                var cust = await _appRepository.UpdateProfileImage(requestBody);

                if (cust != null)
                {
                    if (cust.Id > 0)
                    {
                        customerReturn.responseHeader = await _appRepository.BuildSuccesResponse(requestBody, customerReturn, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.SuccessgetDataMessageEn, ResponseCodeHelper.SuccessgetDataMessageAr, ResponseCodeHelper.SuccessMessageEn, requestBody.requestHeader);
                        return customerReturn;
                    }
                    else
                    {
                        if (cust.Id == -1)
                        {
                            customerReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.NotExistMessageEn, ResponseCodeHelper.NotExistMessageAr, ResponseCodeHelper.NotExistMessageEn, requestBody.requestHeader);
                            return customerReturn;
                        }
                        else
                        {
                            customerReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, ResponseCodeHelper.ErrorMessageEn, requestBody.requestHeader);
                            return customerReturn;
                        }
                    }
                }
                else
                {
                    customerReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, ResponseCodeHelper.ErrorMessageEn, requestBody.requestHeader);
                    return customerReturn;
                }
            }
            catch (Exception exception)
            {
                customerReturn.responseHeader = await _appRepository.BuildExceptionResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, exception, requestBody.requestHeader);
                return customerReturn;
            }
        }

        [HttpPost("ActivateCustomer")]
        public async Task<BaseHeaderResponse> ActivateCustomer([FromBody] CustomerActivationDto requestBody)
        {
            BaseHeaderResponse customerReturn = new BaseHeaderResponse();

            try
            {
                var cust = await _appRepository.ActivateCustomer(requestBody);

                if (cust != null)
                {
                    if (cust.Id > 0)
                    {
                        customerReturn.responseHeader = await _appRepository.BuildSuccesResponse(requestBody, customerReturn, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.SuccessgetDataMessageEn, ResponseCodeHelper.SuccessgetDataMessageAr, ResponseCodeHelper.SuccessMessageEn, requestBody.requestHeader);
                        return customerReturn;
                    }
                    else
                    {
                        if (cust.Id == -1)
                        {
                            customerReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.NotExistMessageEn, ResponseCodeHelper.NotExistMessageAr, ResponseCodeHelper.NotExistMessageEn, requestBody.requestHeader);
                            return customerReturn;
                        }
                        else
                        {
                            customerReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, ResponseCodeHelper.ErrorMessageEn, requestBody.requestHeader);
                            return customerReturn;
                        }
                    }
                }
                else
                {
                    customerReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, ResponseCodeHelper.ErrorMessageEn, requestBody.requestHeader);
                    return customerReturn;
                }
            }
            catch (Exception exception)
            {
                customerReturn.responseHeader = await _appRepository.BuildExceptionResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, exception, requestBody.requestHeader);
                return customerReturn;
            }
        }

        [HttpPost("UpdatePassword")]
        public async Task<BaseHeaderResponse> UpdatePassword([FromBody] CustomerPasswordDto requestBody)
        {
            BaseHeaderResponse customerReturn = new BaseHeaderResponse();

            try
            {
                var cust = await _appRepository.UpdatePassword(requestBody);

                if (cust != null)
                {
                    if (cust.Id > 0)
                    {
                        customerReturn.responseHeader = await _appRepository.BuildSuccesResponse(requestBody, customerReturn, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.SuccessgetDataMessageEn, ResponseCodeHelper.SuccessgetDataMessageAr, ResponseCodeHelper.SuccessMessageEn, requestBody.requestHeader);
                        return customerReturn;
                    }
                    else
                    {
                        customerReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, ResponseCodeHelper.ErrorMessageEn, requestBody.requestHeader);
                        return customerReturn;
                    }
                }
                else
                {
                    customerReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, ResponseCodeHelper.ErrorMessageEn, requestBody.requestHeader);
                    return customerReturn;
                }

            }
            catch (Exception exception)
            {
                customerReturn.responseHeader = await _appRepository.BuildExceptionResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, exception, requestBody.requestHeader);
                return customerReturn;
            }
        }

        [HttpPost("CustomerLogin")]
        public async Task<LoginReturnDto> CustomerLogin([FromBody] LoginDto requestBody)
        {
            LoginReturnDto loginReturnDto = new LoginReturnDto();
            try
            {
                Customer CustomerInfo = await _appRepository.CustomerLogin(requestBody);
                if (CustomerInfo != null)
                {
                    var loginReturn = _mapper.Map<LoginReturn>(CustomerInfo);

                    loginReturn.profileImage = _correntdomainname + loginReturn.profileImage;

                    loginReturnDto.data = loginReturn;
                    loginReturnDto.responseHeader = await _appRepository.BuildSuccesResponse(requestBody, loginReturn, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.SuccessgetDataMessageEn, ResponseCodeHelper.SuccessgetDataMessageAr, ResponseCodeHelper.SuccessMessageEn, requestBody.requestHeader);
                    return loginReturnDto;
                }
                else
                {
                    loginReturnDto.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, ResponseCodeHelper.ErrorMessageEn, requestBody.requestHeader);
                    return loginReturnDto;
                }
            }
            catch (Exception exception)
            {
                loginReturnDto.responseHeader = await _appRepository.BuildExceptionResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, exception, requestBody.requestHeader);
                return loginReturnDto;
            }
        }

        [HttpPost("ContactUs")]
        public async Task<BaseHeaderResponse> ContactUs([FromBody] ContactUsDto requestBody)
        {
            BaseHeaderResponse customerReturn = new BaseHeaderResponse();

            try
            {
                var cust = await _appRepository.ContactUs(requestBody);

                if (cust != null)
                {
                    if (cust.Id > 0)
                    {
                        customerReturn.responseHeader = await _appRepository.BuildSuccesResponse(requestBody, customerReturn, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.SuccessgetDataMessageEn, ResponseCodeHelper.SuccessgetDataMessageAr, ResponseCodeHelper.SuccessMessageEn, requestBody.requestHeader);
                        return customerReturn;
                    }
                    else
                    {
                        customerReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, ResponseCodeHelper.ErrorMessageEn, requestBody.requestHeader);
                        return customerReturn;
                    }
                }
                else
                {
                    customerReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, ResponseCodeHelper.ErrorMessageEn, requestBody.requestHeader);
                    return customerReturn;
                }
            }
            catch (Exception exception)
            {
                customerReturn.responseHeader = await _appRepository.BuildExceptionResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, exception, requestBody.requestHeader);
                return customerReturn;
            }
        }

        [HttpPost("PasswordForget")]
        public async Task<BaseHeaderResponse> PasswordForget([FromBody] PasswordForgetDto requestBody)
        {
            BaseHeaderResponse customerReturn = new BaseHeaderResponse();

            try
            {
                var cust = await _appRepository.PasswordForget(requestBody);

                if (cust != null)
                {
                    if (cust.Id > 0)
                    {
                        customerReturn.responseHeader = await _appRepository.BuildSuccesResponse(requestBody, customerReturn, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.SuccessgetDataMessageEn, ResponseCodeHelper.SuccessgetDataMessageAr, ResponseCodeHelper.SuccessMessageEn, requestBody.requestHeader);
                        return customerReturn;
                    }
                    else
                    {
                        customerReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.NotExistMessageEn, ResponseCodeHelper.NotExistMessageAr, ResponseCodeHelper.NotExistMessageEn, requestBody.requestHeader);
                        return customerReturn;
                    }
                }
                else
                {
                    customerReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, ResponseCodeHelper.ErrorMessageEn, requestBody.requestHeader);
                    return customerReturn;
                }
            }
            catch (Exception exception)
            {
                customerReturn.responseHeader = await _appRepository.BuildExceptionResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, exception, requestBody.requestHeader);
                return customerReturn;
            }
        }


        [HttpPost("AddUpdateCustomerSubscription")]
        public async Task<PaymentProcessedDto> AddUpdateCustomerSubscription([FromBody] CustomerSubscriptionDto requestBody)
        {
            PaymentProcessedDto customerSubscriptionReturn = new PaymentProcessedDto();

            try
            {
                var custSub = await _appRepository.AddUpdateCustomerSubscription(requestBody);

                if (custSub != null)
                {
                    if (custSub.Id > 0)
                    {
                        PaymentProcessed paymentProcessed = new PaymentProcessed();
                        paymentProcessed.URL = "https://www.google.com/";
                        customerSubscriptionReturn.data = paymentProcessed;
                        customerSubscriptionReturn.responseHeader = await _appRepository.BuildSuccesResponse(requestBody, customerSubscriptionReturn, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.SuccessgetDataMessageEn, ResponseCodeHelper.SuccessgetDataMessageAr, ResponseCodeHelper.SuccessMessageEn, requestBody.requestHeader);
                        return customerSubscriptionReturn;
                    }
                    if (custSub.Id == -1)
                    {
                        customerSubscriptionReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.SubMessageEn, ResponseCodeHelper.SubMessageAr, ResponseCodeHelper.SubMessageEn, requestBody.requestHeader);
                        return customerSubscriptionReturn;
                    }
                    else
                    {
                        customerSubscriptionReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, ResponseCodeHelper.ErrorMessageEn, requestBody.requestHeader);
                        return customerSubscriptionReturn;
                    }
                }
                else
                {
                    customerSubscriptionReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, ResponseCodeHelper.ErrorMessageEn, requestBody.requestHeader);
                    return customerSubscriptionReturn;
                }
            }
            catch (Exception exception)
            {
                customerSubscriptionReturn.responseHeader = await _appRepository.BuildExceptionResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, exception, requestBody.requestHeader);
                return customerSubscriptionReturn;
            }
        }

        [HttpPost("AddUpdateCustomerVehicle")]
        public async Task<BaseHeaderResponse> AddUpdateCustomerVehicle([FromBody] CustomerVehicleDto requestBody)
        {
            BaseHeaderResponse customerVehicleReturn = new BaseHeaderResponse();

            try
            {
                var custVeh = await _appRepository.AddUpdateCustomerVehicle(requestBody);

                if (custVeh != null)
                {
                    if (custVeh.Id > 0)
                    {
                        customerVehicleReturn.responseHeader = await _appRepository.BuildSuccesResponse(requestBody, customerVehicleReturn, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.SuccessgetDataMessageEn, ResponseCodeHelper.SuccessgetDataMessageAr, ResponseCodeHelper.SuccessMessageEn, requestBody.requestHeader);
                        return customerVehicleReturn;
                    }
                    else
                    {
                        if (custVeh.Id == -1)
                        {
                            customerVehicleReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ExistMessageEn, ResponseCodeHelper.ExistMessageAr, ResponseCodeHelper.ExistMessageEn, requestBody.requestHeader);
                            return customerVehicleReturn;
                        }
                        if (custVeh.Id == -2)
                        {
                            customerVehicleReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.NoSubMessageEn, ResponseCodeHelper.NoSubMessageAr, ResponseCodeHelper.NoSubMessageEn, requestBody.requestHeader);
                            return customerVehicleReturn;
                        }
                        if (custVeh.Id == -3)
                        {
                            customerVehicleReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ExceedMessageEn, ResponseCodeHelper.ExceedMessageAr, ResponseCodeHelper.ExceedMessageEn, requestBody.requestHeader);
                            return customerVehicleReturn;
                        }
                        else
                        {
                            customerVehicleReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, ResponseCodeHelper.ErrorMessageEn, requestBody.requestHeader);
                            return customerVehicleReturn;
                        }
                        
                    }
                }
                else
                {
                    customerVehicleReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, ResponseCodeHelper.ErrorMessageEn, requestBody.requestHeader);
                    return customerVehicleReturn;
                }
            }
            catch (Exception exception)
            {
                customerVehicleReturn.responseHeader = await _appRepository.BuildExceptionResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, exception, requestBody.requestHeader);
                return customerVehicleReturn;
            }
        }

        [HttpPost("DeleteCustomerVehicle")]
        public async Task<BaseHeaderResponse> DeleteCustomerVehicle([FromBody] SettingListDto requestBody)
        {
            BaseHeaderResponse settingReturn = new BaseHeaderResponse();

            try
            {

                var custVeh = await _appRepository.DeleteCustomerVehicle(requestBody); ;
                if (custVeh != null)
                {

                    settingReturn.responseHeader = await _appRepository.BuildSuccesResponse(requestBody, settingReturn, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.SuccessgetDataMessageEn, ResponseCodeHelper.SuccessgetDataMessageAr, ResponseCodeHelper.SuccessMessageEn, requestBody.requestHeader);
                    return settingReturn;

                }
                else
                {
                    settingReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, ResponseCodeHelper.ErrorMessageEn, requestBody.requestHeader);
                    return settingReturn;
                }

            }
            catch (Exception exception)
            {
                settingReturn.responseHeader = await _appRepository.BuildExceptionResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, exception, requestBody.requestHeader);
                return settingReturn;
            }
        }


        [HttpPost("GetCustomerDetails")]
        public async Task<LoginReturnDto> GetCustomerDetails([FromBody] SettingListDto requestBody)
        {
            LoginReturnDto loginReturn = new LoginReturnDto();
            try
            {
                var returnData = await _appRepository.GetCustomerDetails(requestBody);
                if (returnData != null)
                {
                    returnData.profileImage = _correntdomainname + returnData.profileImage;
                    loginReturn.data = returnData;
                    loginReturn.responseHeader = await _appRepository.BuildSuccesResponse(requestBody, loginReturn, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.SuccessgetDataMessageEn, ResponseCodeHelper.SuccessgetDataMessageAr, ResponseCodeHelper.SuccessMessageEn, requestBody.requestHeader);
                    return loginReturn;
                }
                else
                {
                    loginReturn.responseHeader = await _appRepository.BuildErrorResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, ResponseCodeHelper.ErrorMessageEn, requestBody.requestHeader);
                    return loginReturn;
                }
            }
            catch (Exception exception)
            {
                loginReturn.responseHeader = await _appRepository.BuildExceptionResponse(requestBody, null, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ResponseCodeHelper.ErrorMessageEn, ResponseCodeHelper.ErrorMessageAr, exception, requestBody.requestHeader);
                return loginReturn;
            }
        }

        [HttpPost("GetTransactionDetails")]
        public async Task<TransactionDetailsReturnDto> GetTransactionDetails([FromBody] TransactionDetailsSearchDto requestBody)
        {
            TransactionDetailsReturnDto settingListReturnDto = new TransactionDetailsReturnDto();

            try
            {
                var returnData = await _appRepository.GetTransactionDetails(requestBody);
                settingListReturnDto.data = returnData;
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

        [HttpPost("PaymentProcessed")]
        public async Task<PaymentProcessedDto> PaymentProcessed([FromBody] TransactionDetailsSearchDto requestBody)
        {
            PaymentProcessedDto settingListReturnDto = new PaymentProcessedDto();

            try
            {
                var returnData = await _appRepository.PaymentProcessed(requestBody);
                settingListReturnDto.data = returnData;
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

        [HttpPost("GetCustomerVehiclesList")]
        public async Task<CustomerVehiclesReturnDto> GetCustomerVehiclesList([FromBody] SettingListDto requestBody)
        {
            CustomerVehiclesReturnDto settingListReturnDto = new CustomerVehiclesReturnDto();

            try
            {
                var returnData = await _appRepository.GetCustomerVehiclesList(requestBody);
                var returnData2 = _mapper.Map<List<CustomerVehiclesReturn>>(returnData);

                foreach (CustomerVehiclesReturn x in returnData2)
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

        [HttpPost("GetCustomerTransactionsList")]
        public async Task<CustomerTransactionsReturnDto> GetCustomerTransactionsList([FromBody] SettingListDto requestBody)
        {
            CustomerTransactionsReturnDto settingListReturnDto = new CustomerTransactionsReturnDto();

            try
            {
                var returnData = await _appRepository.GetCustomerTransactionsList(requestBody);
                var returnData2 = _mapper.Map<List<CustomerTransactionsReturn>>(returnData);
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