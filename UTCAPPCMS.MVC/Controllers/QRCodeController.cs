using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UTCAPPCMS.DAL.DBContext;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.Repository.Interfaces;
using UTCAPPCMS.DAL.ViewModel;
using UTCAPPCMS.MVC.Helpers;

namespace UTCAPPCMS.MVC.Controllers
{
    public class QRCodeController : Controller
    {
        private readonly IUnitOfWork<TableTariff> _unitOfWorkTariff;
        private readonly IUnitOfWork<TableTransactionDetail> _unitOfWorkTransaction;
        private readonly IdefaultRepository _idefaultRepository;
      //  private readonly UTCAPPCMS_DBContext _Context;

        public QRCodeController(IdefaultRepository idefaultRepository, IUnitOfWork<TableTransactionDetail> _unitOfWorkTransaction, IUnitOfWork<TableTariff> _unitOfWorkTariff)
        {
            this._unitOfWorkTariff = _unitOfWorkTariff;
            this._unitOfWorkTransaction = _unitOfWorkTransaction;
            this._idefaultRepository = idefaultRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int siteId)
        {
            var parkingLocation =await _idefaultRepository.GetParkingLocationById(siteId);// .ParkingLocations.Where(x => x.Id == siteId).Include(x => x.ParkingFk).AsNoTracking().FirstOrDefault();
            if(parkingLocation!=null)
            {
                return View(new ParkingLocationViewModel() { SiteId = siteId, LogoUrl = parkingLocation.Parking.Logo, Name = parkingLocation.SiteName });

            }else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Confirm(ParkingLocationViewModel parkingLocationViewModel)
        {
            var trans = _unitOfWorkTransaction.Repository.where(x => x.ParkingLocationId == parkingLocationViewModel.SiteId && x.PlateNumber == (parkingLocationViewModel.PlatePrefix + " " + parkingLocationViewModel.PlateNumber) && x.StatusFkId == 1).ToList().LastOrDefault();
            
            if(trans != null)
            {
                parkingLocationViewModel.NoTrans = false;

                parkingLocationViewModel.IsDetails = true;
                parkingLocationViewModel.transactionId = trans.TransactionId;
                parkingLocationViewModel.locationId = trans.ParkingLocationId;  
                parkingLocationViewModel.TimeIn = trans.TransactionTime?.ToString("dd/MM/yyyy h:mm tt") ??"-";

                QRCodeHelper qrCode = new QRCodeHelper(_unitOfWorkTariff);

                parkingLocationViewModel.calculate = qrCode.calculateduartion(trans.TransactionTime, parkingLocationViewModel.SiteId);
            }
            else
            {
                parkingLocationViewModel.NoTrans = true;
            }

            return View("~/Views/QRCode/Index.cshtml" , parkingLocationViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Pay(ParkingLocationViewModel parkingLocationViewModel)
        {
            //  string a = "";
            var trans = _unitOfWorkTransaction.Repository.where(x => x.ParkingLocationId == parkingLocationViewModel.locationId && x.TransactionId == parkingLocationViewModel.transactionId).ToList().LastOrDefault();

            var parkingLocation = await _idefaultRepository.GetParkingLocationById(Convert.ToInt32( trans.ParkingLocationId));

            trans.StatusFkId = 2;
            trans.PaymentTypeId = 2;
            trans.IsPaid = true;
            trans.IsSynch = false;
            trans.PaymentDate = DateTime.Now;
            trans.PaymentExpireDate = DateTime.Now.AddMinutes(Convert.ToDouble( parkingLocation.Allowedtimeperminute));
           await _unitOfWorkTransaction.Commit();
            return View("~/Views/QRCode/Successful.cshtml");
        }
        [HttpGet]
        public async Task<IActionResult> Pricing(int siteId)
        {
            var parkingLocation = await _idefaultRepository.GetParkingLocationById(siteId); 

            
            return View("~/Views/QRCode/Pricing.cshtml",new ParkingLocationViewModel() { SiteId = siteId, LogoUrl = parkingLocation.Parking.Logo, Name = parkingLocation.SiteName });
           
        }
    }
}
