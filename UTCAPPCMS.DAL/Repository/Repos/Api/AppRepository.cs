using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.DBContext;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.Repository.Interfaces.Api;
using UTCAPPCMS.DAL.ViewModel.Api;
using AutoMapper;
using UTCAPPCMS.DAL.ViewModel.Api.App;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using UTCAPPCMS.DAL.Repository.Interfaces;
using UTCAPPCMS.DAL.ViewModel;

namespace UTCAPPCMS.DAL.Repository.Repos.Api
{
    public class AppRepository : IAppRepository
    {
        private UTCAPPCMS_DBContext _Context;
        private readonly IMapper _mapper;

        public AppRepository(UTCAPPCMS_DBContext context, IMapper mapper)
        {
            _Context = context;
            _mapper = mapper;
        }

        public async Task<AppSetting> GetAppSetting(SettingListDto settingListDto)
        {
            return await _Context.AppSettings.Where(a=>a.ParkingId== settingListDto.requestHeader.parkingId).FirstOrDefaultAsync();
        }

        public async Task<List<TableTransactionDetail>> GetTransactionList(int locationId)
        {
            return await _Context.TableTransactionDetail.Where(x => x.ParkingLocationId == locationId && x.IsSynch == false && x.IsPaid == true).AsNoTracking().ToListAsync();
        }

        public async Task<List<TableTransactionDetail>> AddTransactionList(List<TableTransactionDetail> TransDto)
        {
            using (var dbContextTransaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var trans in TransDto)
                    {
                        trans.CreatedDate = DateTime.Now;
                        if (trans.PaymentTypeId == 0)
                        {
                            trans.PaymentTypeId = null;
                        }
                        _Context.Add(trans);
                    }

                    if (await _Context.SaveChangesAsync() > 0)
                    {
                        dbContextTransaction.Commit();
                        return TransDto;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return null;
                }
            }
        }

        public async Task<List<TableTransactionDetail>> UpdateTransactionList(List<TableTransactionDetail> TransDto)
        {
            using (var dbContextTransaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var trans in TransDto)
                    {
                        trans.IsSynch = true;

                        _Context.TableTransactionDetail.Update(trans);
                    }

                    if (await _Context.SaveChangesAsync() > 0)
                    {
                        dbContextTransaction.Commit();
                        return TransDto;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return null;
                }
            }
        }

        public async Task<Boolean> UpdateTransactionListSync(List<TransactionSynchUpdate> TransDto)
        {
            using (var dbContextTransaction = _Context.Database.BeginTransaction())
            {
                try
                {

                    foreach (var trans in TransDto)
                    {
                        var transdd = await _Context.TableTransactionDetail.Where(x => x.Id == trans.Id).FirstOrDefaultAsync();
                        if (transdd != null)
                        {
                            transdd.IsSynch = true;
                            transdd.SynchDate = DateTime.Now;
                        }
                    }

                    if (await _Context.SaveChangesAsync() > 0)
                    {
                        dbContextTransaction.Commit();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return false;
                }
            }
        }


        public async Task<Customer> RegisterCustomer(RegisterDto customerDto)
        {
            using (var dbContextTransaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    Customer customer = _mapper.Map<Customer>(customerDto.data);

                    if (customer.Id == 0)
                    {
                        var exist = _Context.Customer.Where(x => (x.Mobile == customerDto.data.Mobile || x.Email == customerDto.data.Email) && !x.IsDeleted).FirstOrDefault();

                        if (exist == null)
                        {
                            byte[] PasswordHash, PasswordSolt;
                            createPasswordHash(customerDto.data.Password, out PasswordHash, out PasswordSolt);
                            customer.passwordSalt = PasswordSolt;
                            customer.passwordHash = PasswordHash;
                            customer.ActivationCode = new Random().Next(1000, 9999).ToString();
                            customer.ParkingId = customerDto.requestHeader.parkingId;
                            customer.CreatedUserId = customerDto.requestHeader.userId;
                            customer.CreatedDate = DateTime.Now;
                            _Context.Add(customer);
                        }
                        else
                        {
                            customer.Id = -1;
                            return customer;
                        }
                    }

                    if (await _Context.SaveChangesAsync() > 0)
                    {
                        dbContextTransaction.Commit();
                        return customer;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return null;
                }
            }
        }

        public async Task<Customer> UpdateCustomer(CustomerDto customerDto)
        {
            using (var dbContextTransaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    Customer customer = new Customer();
                    var customerUpdate = _Context.Customer.Where(x => x.Id == customerDto.requestHeader.userId && !x.IsDeleted).FirstOrDefault();

                    if (customerUpdate == null)
                    {
                        customer.Id = -1;
                        return customer;
                    }
                    else
                    {
                        customerUpdate.FullName = customerDto.data.FullName;
                        customerUpdate.Email = customerDto.data.Email;
                        customerUpdate.Mobile = customerDto.data.Mobile;
                        customerUpdate.ModifiedUserId = customerDto.requestHeader.userId;
                        customerUpdate.LastModifiedDate = DateTime.Now;
                        _Context.Customer.Update(customerUpdate);
                    }

                    if (await _Context.SaveChangesAsync() > 0)
                    {
                        dbContextTransaction.Commit();
                        return customerUpdate;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return null;
                }
            }
        }

        public async Task<Customer> UpdateProfileImage(ProfileImageDto customerDto)
        {
            using (var dbContextTransaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    Customer customer = new Customer();
                    var customerUpdate = _Context.Customer.Where(x => x.Id == customerDto.requestHeader.userId && !x.IsDeleted).FirstOrDefault();

                    if (customerUpdate == null)
                    {
                        customer.Id = -1;
                        return customer;
                    }
                    else
                    {
                        if (customerDto.data.profileImage != null && customerDto.data.profileImage != "")
                        {
                            customerUpdate.profileImage = await SaveBasem64mobile("Customer", customerDto.data.profileImage);
                        }
                        customerUpdate.ModifiedUserId = customerDto.requestHeader.userId;
                        customerUpdate.LastModifiedDate = DateTime.Now;
                        _Context.Customer.Update(customerUpdate);
                    }

                    if (await _Context.SaveChangesAsync() > 0)
                    {
                        dbContextTransaction.Commit();
                        return customerUpdate;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return null;
                }
            }
        }

        public async Task<Customer> ActivateCustomer(CustomerActivationDto customerDto)
        {
            using (var dbContextTransaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    Customer customer = new Customer();
                    var customerUpdate = _Context.Customer.Where(x => x.Id == customerDto.requestHeader.userId && x.ActivationCode == customerDto.data.ActivationCode && !x.IsDeleted).FirstOrDefault();

                    if (customerUpdate == null)
                    {
                        customer.Id = -1;
                        return customer;
                    }
                    else
                    {
                        customerUpdate.IsActivated = true;
                        customerUpdate.ModifiedUserId = customerDto.requestHeader.userId;
                        customerUpdate.LastModifiedDate = DateTime.Now;
                        _Context.Customer.Update(customerUpdate);
                    }

                    if (await _Context.SaveChangesAsync() > 0)
                    {
                        dbContextTransaction.Commit();
                        return customerUpdate;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return null;
                }
            }
        }

        public async Task<Customer> UpdatePassword(CustomerPasswordDto customerDto)
        {
            using (var dbContextTransaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    Customer customer = new Customer();
                    var customerUpdate = _Context.Customer.Where(x => x.Id == customerDto.requestHeader.userId && !x.IsDeleted).FirstOrDefault();

                    if (customerUpdate == null)
                    {
                        customer.Id = -1;
                        return customer;
                    }
                    else
                    {
                        if (VerifyPasswordHash(customerDto.data.Password, customerUpdate.passwordSalt, customerUpdate.passwordHash))
                        {
                            byte[] PasswordHash, PasswordSolt;
                            createPasswordHash(customerDto.data.newPassword, out PasswordHash, out PasswordSolt);
                            customerUpdate.passwordSalt = PasswordSolt;
                            customerUpdate.passwordHash = PasswordHash;
                            customerUpdate.ModifiedUserId = customerDto.requestHeader.userId;
                            customerUpdate.LastModifiedDate = DateTime.Now;
                            _Context.Customer.Update(customerUpdate);
                        }
                    }

                    if (await _Context.SaveChangesAsync() > 0)
                    {
                        dbContextTransaction.Commit();
                        return customerUpdate;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return null;
                }
            }
        }

        public async Task<Customer> CustomerLogin(LoginDto login)
        {
            var customer = await _Context.Customer.FirstOrDefaultAsync(x => (x.Email == login.data.user || x.Mobile == login.data.user) && x.ParkingId == login.requestHeader.parkingId && !x.IsDeleted);
            if (customer == null)
            {
                return null;
            }
            else
            {
                if (!VerifyPasswordHash(login.data.password, customer.passwordSalt, customer.passwordHash))
                {
                    return null;
                }
                else
                {
                    return customer;
                }
            }
        }

        public async Task<ContactUs> ContactUs(ContactUsDto customerDto)
        {
            using (var dbContextTransaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    ContactUs customer = _mapper.Map<ContactUs>(customerDto.data);

                    if (customer.Id == 0)
                    {
                        customer.CreatedUserId = customerDto.requestHeader.userId;
                        customer.CreatedDate = DateTime.Now;
                        _Context.Add(customer);
                    }

                    if (await _Context.SaveChangesAsync() > 0)
                    {
                        dbContextTransaction.Commit();
                        return customer;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return null;
                }
            }
        }


        public async Task<PasswordForget> PasswordForget(PasswordForgetDto customerDto)
        {
            using (var dbContextTransaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    PasswordForget customer = new PasswordForget();

                    var exist = _Context.Customer.Where(x => (x.Mobile == customerDto.data.EmailOrMobile || x.Email == customerDto.data.EmailOrMobile) && !x.IsDeleted).FirstOrDefault();

                    if (exist != null)
                    {
                        customer.CustomerId = exist.Id;
                        customer.Email = exist.Email;
                        customer.CreatedUserId = customerDto.requestHeader.userId;
                        customer.CreatedDate = DateTime.Now;
                        _Context.Add(customer);
                    }
                    else
                    {
                        customer.Id = -1;
                        return customer;
                    }

                    if (await _Context.SaveChangesAsync() > 0)
                    {
                        dbContextTransaction.Commit();
                        return customer;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return null;
                }
            }
        }

        public async Task<CustomerSubscriptions> AddUpdateCustomerSubscription(CustomerSubscriptionDto customerSubscriptionDto)
        {
            using (var dbContextTransaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    CustomerSubscriptions customerSubscriptions = _mapper.Map<CustomerSubscriptions>(customerSubscriptionDto.data);

                    if (customerSubscriptions.Id == 0)
                    {
                        var active = _Context.CustomerSubscriptions.Where(x => x.CustomerId == customerSubscriptionDto.requestHeader.userId && x.IsActivated).FirstOrDefault();

                        if (active == null)
                        {
                            var duration = _Context.SubscriptionDurations.Where(x => x.Id == customerSubscriptionDto.data.SubscriptionDurationId).FirstOrDefault();

                            customerSubscriptions.ActivatedDate = DateTime.Now;
                            customerSubscriptions.FromDate = DateTime.Now;
                            customerSubscriptions.FromHours = DateTime.Now.TimeOfDay;
                            customerSubscriptions.IsActivated = true;
                            customerSubscriptions.IsEnable = true;
                            customerSubscriptions.IsPaid = true;
                            customerSubscriptions.PaymentDate = DateTime.Now;
                            customerSubscriptions.PaymentReference = "";
                            customerSubscriptions.Price = duration.Price;
                            customerSubscriptions.ToDate = DateTime.Now.AddDays((double)duration.durationInDays);
                            customerSubscriptions.ToHours = customerSubscriptions.ToDate.Value.TimeOfDay;
                            customerSubscriptions.CustomerId = customerSubscriptionDto.requestHeader.userId;

                            customerSubscriptions.CreatedUserId = customerSubscriptionDto.requestHeader.userId;
                            customerSubscriptions.CreatedDate = DateTime.Now;
                            _Context.Add(customerSubscriptions);
                        }
                        else
                        {
                            customerSubscriptions.Id = -1;
                            return customerSubscriptions;
                        }
                    }
                    else
                    {
                        customerSubscriptions.ModifiedUserId = customerSubscriptionDto.requestHeader.userId;
                        customerSubscriptions.LastModifiedDate = DateTime.Now;
                        _Context.CustomerSubscriptions.Update(customerSubscriptions);
                    }

                    if (await _Context.SaveChangesAsync() > 0)
                    {
                        dbContextTransaction.Commit();
                        return customerSubscriptions;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return null;
                }
            }
        }

        public async Task<CustomerVehicles> AddUpdateCustomerVehicle(CustomerVehicleDto customerVehicleDto)
        {
            using (var dbContextTransaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    CustomerVehicles customerVehicles = _mapper.Map<CustomerVehicles>(customerVehicleDto.data);

                    var custSub = _Context.CustomerSubscriptions.Include(x => x.SubscriptionDuration).Where(x => x.CustomerId == customerVehicleDto.requestHeader.userId && x.IsActivated).FirstOrDefault();

                    //if (custSub != null)
                    //{
                    //    var vehCount = _Context.CustomerVehicles.Where(x => x.CustomerId == customerVehicleDto.requestHeader.userId && !x.IsDeleted).Count();

                    //    if (vehCount < custSub.SubscriptionDuration.VechicleCount || customerVehicles.Id != 0)
                    //    {
                    if (customerVehicles.Id == 0)
                    {
                        var exist = _Context.CustomerVehicles.Where(x => x.CustomerId == customerVehicleDto.requestHeader.userId && x.PlateNumber == customerVehicleDto.data.PlateNumber && !x.IsDeleted).FirstOrDefault();

                        if (exist == null)
                        {
                            customerVehicles.CustomerId = customerVehicleDto.requestHeader.userId;
                            customerVehicles.IsEnable = true;

                            customerVehicles.CreatedUserId = customerVehicleDto.requestHeader.userId;
                            customerVehicles.CreatedDate = DateTime.Now;
                            _Context.Add(customerVehicles);
                        }
                        else
                        {
                            customerVehicles.Id = -1;
                            return customerVehicles;
                        }
                    }
                    else
                    {
                        var customerVehiclesUpdate = _Context.CustomerVehicles.Where(x => x.Id == customerVehicles.Id).FirstOrDefault();

                        if (customerVehicleDto.data.Image != null && customerVehicleDto.data.Image != "")
                        {
                            customerVehiclesUpdate.Image = await SaveBasem64mobile("CustomerVehicle", customerVehicleDto.data.Image);
                        }

                        customerVehiclesUpdate.IsVip = customerVehicleDto.data.IsVip;
                        customerVehiclesUpdate.PlateNumber = customerVehicleDto.data.PlateNumber;
                        customerVehiclesUpdate.VechicleType = customerVehicleDto.data.VechicleType;

                        customerVehiclesUpdate.ModifiedUserId = customerVehicleDto.requestHeader.userId;
                        customerVehiclesUpdate.LastModifiedDate = DateTime.Now;
                        _Context.CustomerVehicles.Update(customerVehiclesUpdate);
                    }

                    if (await _Context.SaveChangesAsync() > 0)
                    {
                        dbContextTransaction.Commit();
                        return customerVehicles;
                    }
                    else
                    {
                        return null;
                    }
                    //    }
                    //    else
                    //    {
                    //        customerVehicles.Id = -3;
                    //        return customerVehicles;
                    //    }
                    //}
                    //else
                    //{
                    //    customerVehicles.Id = -2;
                    //    return customerVehicles;
                    //}
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return null;
                }
            }
        }

        public async Task<CustomerVehicles> DeleteCustomerVehicle(SettingListDto deleteDto)
        {
            CustomerVehicles customerVehicles = await _Context.CustomerVehicles.Where(x => x.Id == deleteDto.data.id).FirstOrDefaultAsync();
            if (customerVehicles != null)
            {
                customerVehicles.IsDeleted = true;
                customerVehicles.LastModifiedDate = DateTime.Now;
                customerVehicles.ModifiedUserId = deleteDto.requestHeader.userId;
                if (await _Context.SaveChangesAsync() > 0)
                {
                    return customerVehicles;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }


        public async Task<LoginReturn> GetCustomerDetails(SettingListDto transactionDetailsDto)
        {
            LoginReturn custDetails = new LoginReturn();

            var customer = _Context.Customer.Where(x => x.ParkingId == transactionDetailsDto.requestHeader.parkingId && x.Id == transactionDetailsDto.requestHeader.userId && !x.IsDeleted).ToList().LastOrDefault();

            if (customer != null)
            {
                var customerSub = _Context.CustomerSubscriptions.Include(c => c.SubscriptionDuration.Subscription).Where(x => x.CustomerId == customer.Id && !x.IsDeleted).ToList().LastOrDefault();

                if (customerSub != null)
                {
                    custDetails = new LoginReturn
                    {
                        FullName = customer.FullName,
                        Email = customer.Email,
                        Mobile = customer.Mobile,
                        SubActivatedDate = customerSub.ActivatedDate,
                        SubExpireDate = customerSub.ToDate,
                        SubPrice = customerSub.Price,
                        SubName = customerSub.SubscriptionDuration.Subscription.Name,
                        SubDuration = customerSub.SubscriptionDuration.durationInDays.ToString()
                    };
                }
                else
                {
                    custDetails = new LoginReturn
                    {
                        FullName = customer.FullName,
                        Email = customer.Email,
                        Mobile = customer.Mobile
                    };
                }

                return custDetails;
            }
            else
            {
                return null;
            }
        }

        public async Task<TransactionDetailsDto> GetTransactionDetails(TransactionDetailsSearchDto transactionDetailsDto)
        {
            var trans = _Context.TableTransactionDetail.Include(x => x.ParkingLocation).Where(x => x.ParkingLocation.ParkingId == transactionDetailsDto.requestHeader.parkingId && x.PlateNumber == transactionDetailsDto.data.PlateNumber && x.StatusFkId == 1).ToList().LastOrDefault();

            if (trans != null)
            {
                var calculate = this.calculateduartion(trans.TransactionTime, (int)trans.ParkingLocationId);

                TransactionDetailsDto trxDetails = new TransactionDetailsDto
                {
                    TransactionId = trans.Id,
                    TransactionTime = trans.TransactionTime?.ToString("dd/MM/yyyy h:mm tt") ?? "-",
                    VehicleBrand = trans.VehicleBrand,
                    VehicleModel = trans.VehicleModel,
                    PlateNumber = trans.PlateNumber,
                    TimeLeave = trans.ParkingLocation.Allowedtimeperminute,
                    Duration = calculate.Duration,
                    Amount = calculate.Price
                };
                return trxDetails;
            }
            else
            {
                return null;
            }
        }

        public async Task<PaymentProcessed> PaymentProcessed(TransactionDetailsSearchDto transactionDetailsDto)
        {
            using (var dbContextTransaction = _Context.Database.BeginTransaction())
            {
                try
                {
                    var trans = _Context.TableTransactionDetail.Include(x => x.ParkingLocation).Where(x => x.ParkingLocation.ParkingId == transactionDetailsDto.requestHeader.parkingId && x.PlateNumber == transactionDetailsDto.data.PlateNumber && x.StatusFkId == 1).ToList().LastOrDefault();

                    if (trans != null)
                    {
                        //trans.StatusFkId = 3;
                        trans.IsPaid = true;
                        trans.CloseDate = DateTime.Now;
                        _Context.TableTransactionDetail.Update(trans);

                        var calculate = this.calculateduartion(trans.TransactionTime, (int)trans.ParkingLocationId);

                        TransactionDetailsDto trxDetails = new TransactionDetailsDto
                        {
                            TransactionId = trans.Id,
                            TransactionTime = trans.TransactionTime?.ToString("dd/MM/yyyy h:mm tt") ?? "-",
                            VehicleBrand = trans.VehicleBrand,
                            VehicleModel = trans.VehicleModel,
                            PlateNumber = trans.PlateNumber,
                            TimeLeave = trans.ParkingLocation.Allowedtimeperminute,
                            Duration = calculate.DurationHours,
                            Amount = calculate.Price
                        };

                        TableCollectionTransaction collectionTransaction = new TableCollectionTransaction();

                        collectionTransaction.PlateNumber = trxDetails.PlateNumber;
                        collectionTransaction.InTransactionId = trxDetails.TransactionId;
                        collectionTransaction.TimeIn = DateTime.Parse(trxDetails.TransactionTime);
                        collectionTransaction.Duration = int.Parse(trxDetails.Duration);
                        collectionTransaction.TotalMount = double.Parse(trxDetails.Amount);
                        collectionTransaction.CreatedDate = DateTime.Now;
                        collectionTransaction.PaymentTypeId = 3;

                        _Context.Add(collectionTransaction);

                        PaymentProcessed paymentProcessed = new PaymentProcessed();
                        paymentProcessed.URL = "https://www.google.com/";

                        if (await _Context.SaveChangesAsync() > 0)
                        {
                            dbContextTransaction.Commit();
                            return paymentProcessed;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return null;
                }
            }
        }

        public async Task<List<CustomerVehicles>> GetCustomerVehiclesList(SettingListDto settingListDto)
        {
            var list = await _Context.CustomerVehicles.Where(x => x.IsDeleted == false && x.CustomerId == settingListDto.requestHeader.userId).OrderBy(x => x.PlateNumber).ToListAsync();

            return list;
        }

        public async Task<List<TableCollectionTransaction>> GetCustomerTransactionsList(SettingListDto settingListDto)
        {
            var list = await _Context.TableCollectionTransaction.Where(x => x.IsDeleted == false && x.CustomerID == settingListDto.requestHeader.userId).OrderBy(x => x.CreatedDate).ToListAsync();

            return list;
        }


        public async Task<List<PaymentType>> GetPaymentTypeList(SettingListDto settingListDto)
        {
            var list = await _Context.PaymentTypes.Where(x => x.IsDeleted == false).OrderBy(x => x.NameEn).ToListAsync();

            return list;
        }

        public async Task<List<SubscriptionDurations>> GetSubscriptionDurationsList(SettingListDto settingListDto)
        {
            var list = await _Context.SubscriptionDurations.Where(x => x.IsDeleted == false && x.Subscription.ParkingId == settingListDto.requestHeader.parkingId && x.SubscriptionId == settingListDto.data.id).OrderBy(x => x.Price).ToListAsync();

            return list;
        }

        public async Task<List<Subscription>> GetSubscriptionsList(SettingListDto settingListDto)
        {
            var list = await _Context.Subscription.Where(x => x.IsDeleted == false && x.ParkingId == settingListDto.requestHeader.parkingId).OrderBy(x => x.Name).ToListAsync();

            return list;
        }

        public async Task<List<ParkingImages>> GetSliderList(SettingListDto settingListDto)
        {
            var list = await _Context.ParkingImages.Where(x => x.IsDeleted == false && x.ParkingId == settingListDto.requestHeader.parkingId).OrderBy(x => x.OrderNo).ToListAsync();

            return list;
        }

        public async Task<List<ParkingLocations>> GetParkingList(SettingListDto settingListDto)
        {
            var list = await _Context.ParkingLocations.Include(x => x.Parking).Include(x => x.TableTariff).Where(x => x.IsDeleted == false && x.ParkingId == settingListDto.requestHeader.parkingId).OrderBy(x => x.SiteName).ToListAsync();
            foreach (var item in list)
            {
                item.TableTariff = item.TableTariff.Where(x => x.IsDeleted == false && x.IsEnable == true).OrderBy(x => x.Id).Take(1).ToList();
            }
            return list;
        }


        public async Task<ResponseHeader> BuildErrorResponse(object Request, object Data, string controllername, string action, string messageen, string messagear, string developerremark, RequestHeader requestHeader)
        {
            try
            {
                ResponseHeader responseHeader = new ResponseHeader();
                responseHeader.ResponseCode = ResponseCodeHelper.ErrorCode;
                responseHeader.responseMessage = requestHeader.appLanguage == 1 ? messageen : messagear;

                responseHeader.ResponseRemark = developerremark;

                return responseHeader;
            }
            catch (Exception ex)
            {
                ResponseHeader responseHeader = new ResponseHeader();
                responseHeader.ResponseCode = ResponseCodeHelper.ExceptionCode;
                responseHeader.responseMessage = ex.Message;
                responseHeader.ResponseRemark = ex.Message + "-" + ex.StackTrace;

                return responseHeader;
            }
        }

        public async Task<ResponseHeader> BuildSuccesResponse(object Request, object Data, string controllername, string action, string messageen, string messagear, string developerremark, RequestHeader requestHeader)
        {
            try
            {
                ResponseHeader responseHeader = new ResponseHeader();
                responseHeader.ResponseCode = ResponseCodeHelper.SuccessCode;
                responseHeader.responseMessage = requestHeader.appLanguage == 1 ? messageen : messagear;

                responseHeader.ResponseRemark = developerremark;

                return responseHeader;
            }
            catch (Exception ex)
            {
                ResponseHeader responseHeader = new ResponseHeader();
                responseHeader.ResponseCode = ResponseCodeHelper.ExceptionCode;
                responseHeader.responseMessage = ex.Message;
                responseHeader.ResponseRemark = ex.Message + "-" + ex.StackTrace;

                return responseHeader;
            }
        }

        public async Task<ResponseHeader> BuildNullObjectResponse(object Request, object Data, string controllername, string action, string messageen, string messagear, string developerremark, RequestHeader requestHeader)
        {
            try
            {
                ResponseHeader responseHeader = new ResponseHeader();
                responseHeader.ResponseCode = ResponseCodeHelper.NullObjectCode;
                responseHeader.responseMessage = requestHeader.appLanguage == 1 ? messageen : messagear;

                return responseHeader;
            }
            catch (Exception exception)
            {
                ResponseHeader responseHeader = new ResponseHeader();
                responseHeader.ResponseCode = ResponseCodeHelper.ExceptionCode;
                responseHeader.responseMessage = "";

                responseHeader.ResponseRemark = exception.Message + "-" + exception.StackTrace;

                return responseHeader;
            }
        }

        public async Task<ResponseHeader> BuildExceptionResponse(object Request, object Data, string controllername, string action, string messageen, string messagear, Exception developerremark, RequestHeader requestHeader)
        {
            try
            {
                ResponseHeader responseHeader = new ResponseHeader();
                responseHeader.ResponseCode = ResponseCodeHelper.ExceptionCode;
                responseHeader.responseMessage = requestHeader.appLanguage == 1 ? messageen : messagear;

                responseHeader.ResponseRemark = developerremark.Message + "-" + developerremark.StackTrace;

                return responseHeader;
            }
            catch (Exception ex)
            {
                ResponseHeader responseHeader = new ResponseHeader();
                responseHeader.ResponseCode = ResponseCodeHelper.ExceptionCode;
                responseHeader.responseMessage = ex.Message;

                return responseHeader;
            }
        }


        public void Add<T>(T entity) where T : class
        {
            _Context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _Context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _Context.SaveChangesAsync() > 0;

        }


        public CalculateDuartion calculateduartion(DateTime? timein, int siteId)
        {
            try
            {
                var timeout = DateTime.Now;

                if (timein.ToString() != "" && timeout.ToString() != "")
                {
                    TimeSpan span = (timeout - timein.Value);
                    int duration = span.Hours;
                    string durationval = "";

                    if ((span.Seconds > 0) || (span.Minutes > 0) || (span.Minutes == 0 && span.Hours == 0))
                    {
                        duration = duration + 1;
                    }

                    if (span.Days > 0)
                    {
                        duration = duration + (span.Days * 24);
                    }

                    durationval = (span.Hours + (span.Days * 24)) + ":" + span.Minutes + ":" + span.Seconds;

                    double totalprice = 0;

                    var tariff = _Context.TableTariff.Where(x => x.ParkingLocationId == siteId).ToList();

                    for (int x = 0; x < duration; x++)
                    {
                        if (x <= tariff.Count - 1)
                        {
                            totalprice += Convert.ToDouble(tariff[x].HourPrice);
                        }
                        else
                        {
                            totalprice += Convert.ToDouble(tariff[tariff.Count() - 1].HourPrice);
                        }
                    }

                    return new CalculateDuartion() { DurationHours = duration.ToString(), Duration = durationval, Price = totalprice.ToString() };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {

                var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < ComputedHash.Length; i++)
                {
                    if (ComputedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private void createPasswordHash(string password, out byte[] passwordHash, out byte[] PasswordSolt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                PasswordSolt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        public async static Task<string> SaveBasem64mobile(string foldername, string base64)
        {
            try
            {
                if (base64 != null && base64 != "")
                {
                    string filebase64 = base64.Split('&')[1];
                    string extention = base64.Split('&')[0]; ;

                    string fname = (DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day + "" + DateTime.Now.Hour + "" + DateTime.Now.Minute + "" + DateTime.Now.Second + "" + DateTime.Now.Millisecond + "." + extention).ToString();
                    var path = Directory.GetCurrentDirectory();

                    string targetPath2 = path + "/wwwroot/Files/" + foldername;

                    if (!Directory.Exists(targetPath2))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(targetPath2);

                    }

                    await File.WriteAllBytesAsync(Path.Combine(targetPath2, fname), Convert.FromBase64String(filebase64));

                    string dbpath = "Files/" + foldername + "/" + fname;


                    return dbpath;
                }
                else
                {
                    return "";
                }

            }
            catch
            {
                return "";
            }
        }
    }
}