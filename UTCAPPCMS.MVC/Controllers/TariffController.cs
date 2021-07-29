using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.Repository.Interfaces;
using UTCAPPCMS.MVC.Models;

namespace UTCAPPCMS.MVC.Controllers
{//9 // Session and Privilage is Set
    public class TariffController : Controller
    {
        private readonly string FormKey = "Tariff";

        private readonly IUnitOfWork<TableTariff> _unitOfWork;
        private readonly IUnitOfWork<ParkingLocations> _ParkingLocationsunitOfWork;

        private readonly IdefaultRepository _idefaultRepository;

        private readonly ITariffRepos _tariffRepos;
        private readonly ISystemLogger _Logging;

        public TariffController(IUnitOfWork<TableTariff> _unitOfWork, IUnitOfWork<ParkingLocations> _ParkingLocationsunitOfWork, IdefaultRepository _idefaultRepository, ITariffRepos _tariffRepos, ISystemLogger _Logging)
        {
            this._unitOfWork = _unitOfWork;
            this._ParkingLocationsunitOfWork = _ParkingLocationsunitOfWork;
            this._idefaultRepository = _idefaultRepository;
            this._tariffRepos = _tariffRepos;
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
                        //var tariffList = _unitOfWork.Repository.where(x => x.IsDeleted == false && x.IsEnable == true).ToList();
                        var tariffList = await _tariffRepos.GetAllTariff(sessionUser.ParkingId);
                        if (tariffList != null)
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "View", "Success Get index View", "");
                            return View(tariffList);
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
                    if (id != null) //For Edit
                    {
                        if (Priv.User_Edit == true)
                        {
                            var tariff = await _unitOfWork.Repository.GetById_FindAsync(id.Value);
                            var parkinglocations_List = _ParkingLocationsunitOfWork.Repository.where(l => l.IsDeleted == false && l.IsEnable == true&&l.ParkingId==sessionUser.ParkingId).ToList();
                            ViewBag.ParkingLocationsList = new SelectList(parkinglocations_List, "Id", "SiteName");

                            if (tariff == null)
                            {
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit", $"Error Null Refrance : Id ({id.Value}) is Equal to null", "");
                                return Redirect("/Home/ClientSideError404");
                            }
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit", "Success Get Update View", "");
                            return View(tariff);
                        }
                        else
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit", "Not Authorized to View This Page", "");
                            return Redirect("/Home/ClientSideError404");
                        }
                    }
                    else // For Add
                    {
                        if (Priv.User_Add == true)
                        {
                            var parkinglocations_List = _ParkingLocationsunitOfWork.Repository.where(l => l.IsDeleted == false && l.IsEnable == true).ToList();
                            ViewBag.ParkingLocationsList = new SelectList(parkinglocations_List, "Id", "SiteName");
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
        public async Task<IActionResult> Create(TableTariff tariff)
        {
            var sessionUser = JsonConvert.DeserializeObject<CurrentLoginUser>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserImg = sessionUser.UserImg;
            ViewBag.UserName = sessionUser.UserName;
            var Priv =await _idefaultRepository.GetPrivilage(sessionUser.UserId, FormKey);
            try
            {
                if (Priv != null)
                {
                    if (tariff.Id != 0) //For Edit
                    {
                        if (Priv.User_Edit == true)
                        {
                            if (ModelState.IsValid)
                            {
                                try
                                {
                                    tariff.LastModifiedDate = DateTime.Now;
                                    tariff.ModifiedUserId = sessionUser.UserId;

                                    _unitOfWork.Repository.Update(tariff);
                                    await _unitOfWork.Commit();
                                    await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", "Update Successed", "");
                                }
                                catch (DbUpdateConcurrencyException ex)
                                {
                                    if (!TariffExists(tariff.Id))
                                    {
                                        await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", $"ERROR : Not Exists Siteleine Id : {tariff.Id}", "");
                                        return Redirect("/Home/ClientSideError404");
                                    }
                                    else
                                    {
                                        await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", ex.Message, "");
                                    }
                                }
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", "Update Successed", "");
                                return RedirectToAction(nameof(Index));
                            }
                            else
                            {
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", "Model State is InValid", "");
                            }
                            return View(tariff);
                        }
                        else
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", "Not Authorized User to do this action", "");
                            return Redirect("/Home/ClientSideError404");
                        }
                    }
                    else // For Add
                    {
                        if (Priv.User_Add == true)
                        {
                            if (ModelState.IsValid)
                            {
                                tariff.IsDeleted = false;
                                tariff.CreatedDate = DateTime.Now;
                                tariff.LastModifiedDate = DateTime.Now;
                                tariff.ModifiedUserId = 0;
                                tariff.CreatedUserId = sessionUser.UserId;

                                await _unitOfWork.Repository.Add(tariff);
                                await _unitOfWork.Commit();
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", "Add Successed", "");
                                return RedirectToAction(nameof(Index));
                            }
                            else
                            {
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", "Model State is InValid", "");
                            }
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", "Add Successed", "");
                            return View(tariff);
                        }
                        else
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", "Not Authorized User to do this action", "");
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

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sessionUser = JsonConvert.DeserializeObject<CurrentLoginUser>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserImg = sessionUser.UserImg;
            ViewBag.UserName = sessionUser.UserName;
            var Priv =await _idefaultRepository.GetPrivilage(sessionUser.UserId, FormKey.Trim());
            try
            {
                if (Priv != null)
                {
                    if (Priv.User_Delete == true)
                    {
                        var tariff = await _unitOfWork.Repository.GetById_FindAsync(id);
                        tariff.LastModifiedDate = DateTime.Now;
                        tariff.CreatedUserId = sessionUser.UserId;
                        tariff.IsEnable = false;
                        tariff.IsDeleted = true;
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
            var Priv =await _idefaultRepository.GetPrivilage(sessionUser.UserId, FormKey.Trim());
            try
            {
                if (Priv != null)
                {
                    if (Priv.User_Print == true)
                    {
                        var parkings = _unitOfWork.Repository.All();
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Tariff");
                            var currentRow = 1;
                            worksheet.Cell(currentRow, 1).Value = "Id";
                            worksheet.Cell(currentRow, 2).Value = "CreatedUserId";
                            worksheet.Cell(currentRow, 3).Value = "ModifiedUserId";
                            worksheet.Cell(currentRow, 4).Value = "CreatedDate";
                            worksheet.Cell(currentRow, 5).Value = "LastModifiedDate";
                            worksheet.Cell(currentRow, 6).Value = "IsEnable";
                            worksheet.Cell(currentRow, 7).Value = "IsDeleted";
                            worksheet.Cell(currentRow, 8).Value = "EnglishName";
                            worksheet.Cell(currentRow, 9).Value = "HourPrice";
                            worksheet.Cell(currentRow, 10).Value = "ParkingLocationFkId";

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
                                worksheet.Cell(currentRow, 8).Value = item.NameEn;
                                worksheet.Cell(currentRow, 9).Value = item.HourPrice;
                                worksheet.Cell(currentRow, 10).Value = item.ParkingLocationId;
                            }
                            using (var stream = new MemoryStream())
                            {
                                workbook.SaveAs(stream);
                                var content = stream.ToArray();
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Excel", "Success to Download Excel file", "");
                                return File(content, "application/vnd.openxmlformats-officdocument.spreadsheetml.sheet", $"TariffList{DateTime.Now.Date.Hour}.xlsx");
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

        private bool TariffExists(int id)
        {
            return _unitOfWork.Repository.Get_Any(id);
        }

    }
}
