using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
{//2 // Session and Privilage is Set
    public class SiteLineController : Controller
    {
        private readonly string FormKey = "SiteLine";

        private readonly IUnitOfWork<Siteline> _unitOfWork;
        private readonly IUnitOfWork<ParkingLocations> _ParkingLocationsunitOfWork;

        private readonly IdefaultRepository _idefaultRepository;

        private readonly ISystemLogger _Logging;
        public SiteLineController(IUnitOfWork<Siteline> _unitOfWork, IUnitOfWork<ParkingLocations> _ParkingLocationsunitOfWork, IdefaultRepository _idefaultRepository, ISystemLogger _Logging)
        {
            this._unitOfWork = _unitOfWork;
            this._ParkingLocationsunitOfWork = _ParkingLocationsunitOfWork;
            this._idefaultRepository = _idefaultRepository;
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
                        var sitelinesList = new List<Siteline>();
                        if (sessionUser.ParkingId == null || sessionUser.ParkingId == 0)    // Admin Account
                        {
                            sitelinesList = _unitOfWork.Repository.where(x => x.IsDeleted == false && x.IsEnable == true).ToList();
                        }
                        else    // Company Account
                        {
                            var parkinglocationList = await _ParkingLocationsunitOfWork.Repository.where(x => x.IsDeleted == false && x.IsEnable == true && x.ParkingId == sessionUser.ParkingId).ToListAsync();
                            foreach (var item in parkinglocationList)
                            {
                                sitelinesList.AddRange ( _unitOfWork.Repository.where(x => x.IsDeleted == false && x.IsEnable == true && x.ParkingLocationId == item.Id).ToList());
                            }
                        } 
                        
                        if (sitelinesList != null)
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "View", "Success Get index View", "");
                            return View(sitelinesList);
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
                            var siteline = await _unitOfWork.Repository.GetById_FindAsync(id.Value);
                            var parkinglocations_List = _ParkingLocationsunitOfWork.Repository.where(l => l.IsDeleted == false && l.IsEnable == true&&l.ParkingId==sessionUser.ParkingId).ToList();
                            ViewBag.ParkingLocationsList = new SelectList(parkinglocations_List, "Id", "SiteName");

                            if (siteline == null)
                            {
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit", $"Error Null Refrance : Id ({id.Value}) is Equal to null", "");
                                return Redirect("/Home/ClientSideError404");
                            }
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit", "Success Get Update View", "");
                            return View(siteline);
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
        public async Task<IActionResult> Create(Siteline siteline)
        {
            var sessionUser = JsonConvert.DeserializeObject<CurrentLoginUser>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserImg = sessionUser.UserImg;
            ViewBag.UserName = sessionUser.UserName;
            var Priv =await _idefaultRepository.GetPrivilage(sessionUser.UserId, FormKey.Trim());
            try
            {
                if (Priv != null)
                {
                    if (siteline.Id != 0) //For Edit
                    {
                        if (Priv.User_Edit == true)
                        {
                            if (ModelState.IsValid)
                            {
                                try
                                {
                                    siteline.IsDeleted = false;
                                    siteline.LastModifiedDate = DateTime.Now;
                                    siteline.ModifiedUserId = sessionUser.UserId;
                                    _unitOfWork.Repository.Update(siteline);
                                    await _unitOfWork.Commit();
                                    await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", "Update Successed", "");
                                }
                                catch (DbUpdateConcurrencyException ex)
                                {
                                    if (!SiteLineExists(siteline.Id))
                                    {
                                        await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", $"ERROR : Not Exists Siteleine Id : {siteline.Id}", "");
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
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", "Update Successed", "");
                            return View(siteline);
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
                                siteline.IsDeleted = false;
                                siteline.CreatedDate = DateTime.Now;
                                siteline.LastModifiedDate = DateTime.Now;
                                siteline.ModifiedUserId = 0;
                                siteline.CreatedUserId = sessionUser.UserId; // Id of Current loged in user    

                                await _unitOfWork.Repository.Add(siteline);
                                await _unitOfWork.Commit();
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", "Add Successed", "");
                                return RedirectToAction(nameof(Index));
                            }
                            else
                            {
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", "Model State is InValid", "");
                            }
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", "Add Successed", "");
                            return View(siteline);
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
            var Priv = await _idefaultRepository.GetPrivilage(sessionUser.UserId, FormKey.Trim());
            try
            {
                if (Priv != null)
                {
                    if (Priv.User_Delete == true)
                    {
                        var siteline = await _unitOfWork.Repository.GetById_FindAsync(id);

                        siteline.LastModifiedDate = DateTime.Now;
                        siteline.ModifiedUserId = sessionUser.UserId;
                        siteline.IsEnable = false;
                        siteline.IsDeleted = true;
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
                            var worksheet = workbook.Worksheets.Add("SiteLines");
                            var currentRow = 1;
                            worksheet.Cell(currentRow, 1).Value = "Id";
                            worksheet.Cell(currentRow, 2).Value = "CreatedUserId";
                            worksheet.Cell(currentRow, 3).Value = "ModifiedUserId";
                            worksheet.Cell(currentRow, 4).Value = "CreatedDate";
                            worksheet.Cell(currentRow, 5).Value = "LastModifiedDate";
                            worksheet.Cell(currentRow, 6).Value = "IsEnable";
                            worksheet.Cell(currentRow, 7).Value = "IsDeleted";
                            worksheet.Cell(currentRow, 8).Value = "LineId";
                            worksheet.Cell(currentRow, 9).Value = "LineName";
                            worksheet.Cell(currentRow, 10).Value = "LineNumber";
                            worksheet.Cell(currentRow, 11).Value = "LineType";
                            worksheet.Cell(currentRow, 12).Value = "LineDpuid";
                            worksheet.Cell(currentRow, 13).Value = "LineDpuname";
                            worksheet.Cell(currentRow, 14).Value = "ParkingLocationFkId";


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
                                worksheet.Cell(currentRow, 8).Value = item.LineId;
                                worksheet.Cell(currentRow, 9).Value = item.LineName;
                                worksheet.Cell(currentRow, 10).Value = item.LineNumber;
                                worksheet.Cell(currentRow, 11).Value = item.LineType;
                                worksheet.Cell(currentRow, 12).Value = item.LineDpuid;
                                worksheet.Cell(currentRow, 13).Value = item.LineDpuname;
                                worksheet.Cell(currentRow, 14).Value = item.ParkingLocationId;
                            }
                            using (var stream = new MemoryStream())
                            {
                                workbook.SaveAs(stream);
                                var content = stream.ToArray();
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Excel", "Success to Download Excel file", "");
                                return File(content, "application/vnd.openxmlformats-officdocument.spreadsheetml.sheet", $"SiteLineList{DateTime.Now.Date.Hour}.xlsx");
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

        private bool SiteLineExists(int id)
        {
            return _unitOfWork.Repository.Get_Any(id);
        }
    }
}