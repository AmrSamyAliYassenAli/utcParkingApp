using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.Repository.Interfaces.Api;
using UTCAPPCMS.DAL.ViewModel.Api;
using UTCAPPCMS.MVC.Helpers.Api;
using UTCAPPCMS.MVC.Models.API;

namespace UTCAPPCMS.MVC.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRCodeOperationController : ControllerBase
    {
        private readonly IAppRepository _appRepository;
        private readonly IMapper _mapper;

        public QRCodeOperationController(IAppRepository appRepository, IMapper mapper)
        {
            _appRepository = appRepository;
            _mapper = mapper;
        }

        [HttpPost("GetPaidTransactionList")]
        public async Task<List<TransactionDetail>> GetPaidTransactionList([FromBody] transactionlistdto requestbody)
        {
            try
            {
                var list =await _appRepository.GetTransactionList(requestbody.locationId);
                List <TransactionDetail> transDetail = _mapper.Map<List<TransactionDetail>>(list);

                return transDetail;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost("AddTransactionList")]
        public async Task<Response> AddTransactionList([FromBody] List<TransactionDetail> requestBody)
        {
            Response transReturn = new Response();

            try
            {
                List<TableTransactionDetail> transDetail = _mapper.Map<List<TableTransactionDetail>>(requestBody);

                var list = await _appRepository.AddTransactionList(transDetail);

                if (list != null)
                {
                    transReturn.responseCode = ResponseCode.SuccessCode;
                    transReturn.responseMessage = "Success";

                    return transReturn;

                }
                else
                {
                    transReturn.responseCode = ResponseCode.ErrorCode;
                    transReturn.responseMessage = "Error";

                    return transReturn;
                }
            }
            catch (Exception exception)
            {
                transReturn.responseCode = ResponseCode.ExceptionCode;
                transReturn.responseMessage = exception.Message;

                return transReturn;
            }
        }

        [HttpPost("UpdateTransactionList")]
        public async Task<Response> UpdateTransactionList([FromBody] List<TransactionSynchUpdate> requestBody)
        {
            Response transReturn = new Response();

            try
            {
                

                var list = await _appRepository.UpdateTransactionListSync(requestBody);

                if (list ==true)
                {
                    transReturn.responseCode = ResponseCode.SuccessCode;
                    transReturn.responseMessage = "Success";

                    return transReturn;
                }
                else
                {
                    transReturn.responseCode = ResponseCode.ErrorCode;
                    transReturn.responseMessage = "Error";

                    return transReturn;
                }
            }
            catch (Exception exception)
            {
                transReturn.responseCode = ResponseCode.ExceptionCode;
                transReturn.responseMessage = exception.Message;

                return transReturn;
            }
        }


    }
}