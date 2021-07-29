using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using UTCAPPCMS.DAL.DBContext;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.Repository.Interfaces;
using UTCAPPCMS.DAL.Repository.Repos;
using UTCAPPCMS.MVC.Helpers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using UTCAPPCMS.MVC.Models;

namespace UTCAPPCMS.MVC.Controllers
{//6 // Session and Privilage is Set
    public class ParkingLocationsController : Controller
    {
        private readonly string FormKey = "ParkingLocations";

        private readonly IUnitOfWork<ParkingLocations> _unitOfWork;
        private readonly IUnitOfWork<Parking> _parkingUnitOfWork;
        private readonly IUnitOfWork<Area> _AreaUnitOfWork;
        private readonly IdefaultRepository _idefaultRepository;
        private readonly QRCodeService _QRCodeGenerator;

        private readonly ISystemLogger _Logging;
        public ParkingLocationsController(IUnitOfWork<ParkingLocations> _unitOfWork, IUnitOfWork<Parking> _parkingUnitOfWork, IdefaultRepository _idefaultRepository, IUnitOfWork<Area> _AreaUnitOfWork, QRCodeService _QRCodeGenerator, ISystemLogger _Logging)
        {
            this._unitOfWork = _unitOfWork;
            this._parkingUnitOfWork = _parkingUnitOfWork;
            this._idefaultRepository = _idefaultRepository;
            this._AreaUnitOfWork = _AreaUnitOfWork;
            this._QRCodeGenerator = _QRCodeGenerator;
            this._Logging = _Logging;
        }

        public async Task<IActionResult> Index()
        {
            var sessionUser = JsonConvert.DeserializeObject<CurrentLoginUser>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserImg = sessionUser.UserImg;
            ViewBag.UserName = sessionUser.UserName;
            var Priv =await _idefaultRepository.GetPrivilage(sessionUser.UserId, FormKey.Trim());
            try
            {
                if (Priv != null)
                {
                    if (Priv.User_View != null && Priv.User_View == true)
                    {
                        var parkinglocationList = new List<ParkingLocations>();
                        if (sessionUser.ParkingId == null || sessionUser.ParkingId == 0)    // Admin Account
                        {
                            parkinglocationList = await _unitOfWork.Repository.where(x => x.IsDeleted == false && x.IsEnable == true).ToListAsync(); 
                        }
                        else // Company Account
                        {
                            parkinglocationList = await _unitOfWork.Repository.where(x => x.IsDeleted == false && x.IsEnable == true && x.ParkingId == sessionUser.ParkingId).ToListAsync();
                        }                                                                   
                        if (parkinglocationList != null)
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "View", "Success Get index View", "");
                            return View(parkinglocationList);
                        }
                        else
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "View", "Data NotFound in Table", "");
                            return Redirect("/Home/ClientSideError404");
                        }
                    }
                    else
                    {
                        await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "View", "Not Authorized to View This Page", "");
                        return Redirect("/Home/ClientSideError404");
                    }
                }
                else
                {
                    await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "View", "Privilage Returns Null : User Don't Assign to Privilage Group or Custom Privilage ", "");
                    return Redirect("/Home/ClientSideError404");
                }
            }
            catch (Exception ex)
            {
                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "View", ex.Message, "");
                return Redirect("/Home/ServerSideError500");
            }      
        }

        public async Task<IActionResult> Create(int? id)
        {
            var sessionUser = JsonConvert.DeserializeObject<CurrentLoginUser>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserImg = sessionUser.UserImg;
            ViewBag.UserName = sessionUser.UserName;
            var Priv = await _idefaultRepository.GetPrivilage(sessionUser.UserId, FormKey.Trim());
            try
            {
                if (Priv != null)
                {
                    if (id != null)  // For Edit
                    {
                        if (Priv.User_Edit == true)
                        {
                            var parkingLocations = await _unitOfWork.Repository.GetById_FindAsync(id.Value);
                            var areas = await _AreaUnitOfWork.Repository.where(x => x.IsDeleted == false && x.IsEnable == true).ToListAsync();
                            ViewBag.AreasList = new SelectList(areas, "Id", "NameEn");
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit", "Success Get Update View", "");
                            return View(parkingLocations);
                        }
                        else
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit", "Not Authorized to View This Page", "");
                            return Redirect("/Home/ClientSideError404");
                        }
                    }
                    else // For Create
                    {
                        if (Priv.User_Add == true)
                        {
                            var areas = await _AreaUnitOfWork.Repository.where(x => x.IsDeleted == false && x.IsEnable == true).ToListAsync();
                            ViewBag.AreasList = new SelectList(areas, "Id", "NameEn");
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add", "Success Get Add View", "");
                            return View();
                        }
                        else
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add", "Not Authorized to View This Page", "");
                            return Redirect("/Home/ClientSideError404");
                        }
                    }
                }
                else
                {
                    await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add", "Privilage Returns Null : User Don't Assign to Privilage Group or Custom Privilage ", "");
                    return Redirect("/Home/ClientSideError404");
                }
            }
            catch (Exception ex)
            {
                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add", ex.Message, "");
                return Redirect("/Home/ServerSideError500");
            }             
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ParkingLocations parkingLocations)
        {
            var sessionUser = JsonConvert.DeserializeObject<CurrentLoginUser>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserImg = sessionUser.UserImg;
            ViewBag.UserName = sessionUser.UserName;
            var Priv = await _idefaultRepository.GetPrivilage(sessionUser.UserId, FormKey.Trim());
            try
            {
                if (Priv != null)
                {
                    if (parkingLocations.Id == 0)   // For Create
                    {
                        if (Priv.User_Add == true)
                        {
                            if (ModelState.IsValid)
                            {
                                parkingLocations.IsDeleted = false;
                                parkingLocations.CreatedDate = DateTime.Now;
                                parkingLocations.LastModifiedDate = DateTime.Now;
                                parkingLocations.ModifiedUserId = 0;
                                parkingLocations.CreatedUserId = sessionUser.UserId; // Id of Current loged in user
                                parkingLocations.Qrcode = _QRCodeGenerator.SaveQRCodeAsImg(parkingLocations.Id.ToString(), parkingLocations.Id);
                                ViewBag.QRCodeIMG = parkingLocations.Qrcode;
                                parkingLocations.ParkingId = sessionUser.ParkingId;
                                await _unitOfWork.Repository.Add(parkingLocations);
                                await _unitOfWork.Commit();
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", "Add Successed", "");
                                return RedirectToAction(nameof(Index));
                            }
                            else
                            {
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", "Model State is InValid", "");
                            }
                            ViewData["ParkingFkId"] = new SelectList(_parkingUnitOfWork.Repository.All(), "Id", "Id", parkingLocations.ParkingId);
                            return View(parkingLocations);
                        }
                        else
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", "Not Authorized User to do this action", "");
                            return Redirect("/Home/ClientSideError404");
                        }
                    }
                    else // For Edit
                    {
                        if (Priv.User_Edit == true)
                        {

                            if (ModelState.IsValid)
                            {
                                try
                                {
                                    var oldparkingLoc = await _unitOfWork.Repository.GetById_FirstOrDefAsync(parkingLocations.Id);
                                    parkingLocations.IsDeleted = false;
                                    parkingLocations.CreatedDate = DateTime.Now;
                                    parkingLocations.LastModifiedDate = DateTime.Now;
                                    parkingLocations.ModifiedUserId = sessionUser.UserId;
                                    parkingLocations.CreatedUserId = oldparkingLoc.CreatedUserId; // Id of Current loged in user
                                    parkingLocations.Qrcode = oldparkingLoc.Qrcode;
                                    ViewBag.QRCodeIMG = parkingLocations.Qrcode;
                                    parkingLocations.ParkingId = sessionUser.ParkingId;
                                    _unitOfWork.Repository.Update(parkingLocations);
                                    await _unitOfWork.Commit();
                                }
                                catch (DbUpdateConcurrencyException ex)
                                {
                                    if (!ParkingLocationsExists(parkingLocations.Id))
                                    {
                                        await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", $"ERROR: Id NotFound {parkingLocations.Id}", "");
                                        return Redirect("/Home/ClientSideError404");
                                    }
                                    else
                                    {
                                        await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", ex.Message, "");
                                        return Redirect("/Home/ServerSideError500");
                                    }
                                }
                                return RedirectToAction(nameof(Index));
                            }
                            else
                            {
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", "Model State is InValid", "");
                            }
                            ViewData["ParkingFkId"] = new SelectList(_parkingUnitOfWork.Repository.All(), "Id", "Id", parkingLocations.ParkingId);
                            return View(parkingLocations);
                        }
                        else
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", "Not Authorized User to do this action", "");
                            return Redirect("/Home/ClientSideError404");
                        }

                    }
                }
                else
                {
                    await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add Post Method", "Privilage Returns Null : User Don't Assign to Privilage Group or Custom Privilage ", "");
                    return Redirect("/Home/ClientSideError404");
                }
            }
            catch (Exception ex)
            {
                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add Post Method", ex.Message, "");
                return Redirect("/Home/ServerSideError500");
            }
        }

        
        public JsonResult GrtArea(string an)
        {
            
            if (an == null)
            {
                return Json(_AreaUnitOfWork.Repository.where(x=> x.IsDeleted == false && x.IsEnable == true).Select(x => (new { label = x.NameEn, val = x.Id })).ToList());
            }
            else
            {
                return Json(_AreaUnitOfWork.Repository.where(x => (x.IsDeleted == false && x.IsEnable == true) && (x.NameEn.ToUpper().Contains(an.ToUpper()))).Select(x => (new { label = x.NameEn, val = x.Id })).ToList());
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sessionUser = JsonConvert.DeserializeObject<CurrentLoginUser>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserImg = sessionUser.UserImg;
            ViewBag.UserName = sessionUser.UserName;
            var Priv = await _idefaultRepository.GetPrivilage(sessionUser.UserId, FormKey.Trim());
            try
            {
                if (Priv != null)
                {
                    if (Priv.User_Delete == true)
                    {
                        var parkingLocations = await _unitOfWork.Repository.GetById_FindAsync(id);
                        parkingLocations.LastModifiedDate = DateTime.Now;
                        parkingLocations.CreatedDate = parkingLocations.CreatedDate;
                        parkingLocations.ModifiedUserId = sessionUser.UserId;
                        parkingLocations.IsEnable = false;
                        parkingLocations.IsDeleted = true;
                        await _unitOfWork.Commit();
                        await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Delete", $"Success Deleting ID {id}", "");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Delete", "Not Authorized to do this Action", "");
                        return Redirect("/Home/ClientSideError404");
                    }
                }
                else
                {
                    await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Delete", "Privilage Returns Null : User Don't Assign to Privilage Group or Custom Privilage ", "");
                    return Redirect("/Home/ClientSideError404");
                }
            }
            catch (Exception ex)
            {
                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Delete", ex.Message, "");
                return Redirect("/Home/ServerSideError500");
            }
        }

        public async Task<IActionResult> Excel()
        {
            var sessionUser = JsonConvert.DeserializeObject<CurrentLoginUser>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserImg = sessionUser.UserImg;
            ViewBag.UserName = sessionUser.UserName;
            var Priv = await _idefaultRepository.GetPrivilage(sessionUser.UserId, FormKey.Trim());
            try
            {
                if (Priv != null)
                {
                    if (Priv.User_Print == true)
                    {
                        var parkings = _unitOfWork.Repository.All();
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("ParkingLocations");
                            var currentRow = 1;
                            worksheet.Cell(currentRow, 1).Value = "Id";
                            worksheet.Cell(currentRow, 2).Value = "CreatedUserId";
                            worksheet.Cell(currentRow, 3).Value = "ModifiedUserId";
                            worksheet.Cell(currentRow, 4).Value = "CreatedDate";
                            worksheet.Cell(currentRow, 5).Value = "LastModifiedDate";
                            worksheet.Cell(currentRow, 6).Value = "IsEnable";
                            worksheet.Cell(currentRow, 7).Value = "IsDeleted";
                            worksheet.Cell(currentRow, 8).Value = "SiteName";
                            worksheet.Cell(currentRow, 9).Value = "SiteNo";
                            worksheet.Cell(currentRow, 10).Value = "SiteId";
                            worksheet.Cell(currentRow, 11).Value = "QrCode";
                            worksheet.Cell(currentRow, 12).Value = "Parking_FK_ID";
                            worksheet.Cell(currentRow, 13).Value = "Area_FK_ID";
                            worksheet.Cell(currentRow, 14).Value = "Block";
                            worksheet.Cell(currentRow, 15).Value = "GPS_Lat";
                            worksheet.Cell(currentRow, 16).Value = "GPS_Long";
                            worksheet.Cell(currentRow, 17).Value = "IsPublic";
                            worksheet.Cell(currentRow, 18).Value = "CarCapacity";
                            worksheet.Cell(currentRow, 19).Value = "FloorsNo";
                            worksheet.Cell(currentRow, 20).Value = "AddressEn";
                            worksheet.Cell(currentRow, 21).Value = "AddressAr";
                            worksheet.Cell(currentRow, 22).Value = "ContactName";
                            worksheet.Cell(currentRow, 23).Value = "ContactEmail";
                            worksheet.Cell(currentRow, 24).Value = "ContactPhone";
                            worksheet.Cell(currentRow, 25).Value = "OpenFromTime";
                            worksheet.Cell(currentRow, 26).Value = "OpenToTime";
                            worksheet.Cell(currentRow, 27).Value = "AllowedTimePerMinute";

                            foreach (var item in parkings)
                            {
                                currentRow++;
                                worksheet.Cell(currentRow, 1).Value = item.Id;
                                worksheet.Cell(currentRow, 2).Value = item.CreatedUserId;
                                worksheet.Cell(currentRow, 3).Value = item.ModifiedUserId;
                                worksheet.Cell(currentRow, 4).Value = item.CreatedDate;
                                worksheet.Cell(currentRow, 5).Value = item.LastModifiedDate;
                                worksheet.Cell(currentRow, 6).Value = item.IsEnable;
                                worksheet.Cell(currentRow, 7).Value = item.IsDeleted;
                                worksheet.Cell(currentRow, 8).Value = item.SiteName;
                                worksheet.Cell(currentRow, 9).Value = item.SiteNo;
                                worksheet.Cell(currentRow, 10).Value = item.SiteId;
                                worksheet.Cell(currentRow, 11).Value = item.Qrcode;
                                worksheet.Cell(currentRow, 12).Value = item.ParkingId;
                                worksheet.Cell(currentRow, 13).Value = item.AreaId;
                                worksheet.Cell(currentRow, 14).Value = item.Block;
                                worksheet.Cell(currentRow, 15).Value = item.GpsLat;
                                worksheet.Cell(currentRow, 16).Value = item.GpsLong;
                                worksheet.Cell(currentRow, 17).Value = item.IsPublic;
                                worksheet.Cell(currentRow, 18).Value = item.CarCapacity;
                                worksheet.Cell(currentRow, 19).Value = item.FloorsNo;
                                worksheet.Cell(currentRow, 20).Value = item.AddressEn;
                                worksheet.Cell(currentRow, 21).Value = item.AddressAr;
                                worksheet.Cell(currentRow, 22).Value = item.ContactName;
                                worksheet.Cell(currentRow, 23).Value = item.ContactEmail;
                                worksheet.Cell(currentRow, 24).Value = item.ContactPhone;
                                worksheet.Cell(currentRow, 25).Value = item.OpenFromTime;
                                worksheet.Cell(currentRow, 26).Value = item.OpenToTime;
                                worksheet.Cell(currentRow, 27).Value = item.Allowedtimeperminute;
                            }
                            using (var stream = new MemoryStream())
                            {
                                workbook.SaveAs(stream);
                                var content = stream.ToArray();
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Excel", "Success to Download Excel file", "");
                                return File(content, "application/vnd.openxmlformats-officdocument.spreadsheetml.sheet", $"ParkingLocationsList{DateTime.Now.Date.Hour}.xlsx");
                            }
                        }
                    }
                    else
                    {
                        await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Excel", "Not Authorized to do this Action", "");
                        return Redirect("/Home/ClientSideError404");
                    }
                }
                else
                {
                    await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Excel", "Privilage Returns Null : User Don't Assign to Privilage Group or Custom Privilage ", "");
                    return Redirect("/Home/ClientSideError404");
                }
            }
            catch (Exception ex)
            {
                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Delete", ex.Message, "");
                return Redirect("/Home/ServerSideError500");
            }
        }

        private bool ParkingLocationsExists(int id)
        {
            return _unitOfWork.Repository.Get_Any(id);
        }
    }
}
