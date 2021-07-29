using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.DBContext;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.Repository.Interfaces;
using UTCAPPCMS.MVC.Models;

namespace UTCAPPCMS.MVC.Controllers
{//10 // Session and Privilage is Set
    public class SubscriptionController : Controller
    {
        private readonly string FormKey = "Subscription";

        private readonly IUnitOfWork<Subscription> _SubscriptionunitOfWork;
        private readonly IUnitOfWork<SubscriptionDurations> _SubDurationunitOfWork;
        private readonly IUnitOfWork<Parking> _ParkingUnitOfWork;
        private readonly ISubscriptionRepos _subscriptionRepos;

        private readonly IdefaultRepository _idefaultRepository;

        private readonly string ImgPath = "/Files/Images/SubscriptionIMG/";

        private readonly ISystemLogger _Logging;
        public SubscriptionController(ISubscriptionRepos _subscriptionRepos, IUnitOfWork<Subscription> _SubscriptionunitOfWork, IUnitOfWork<SubscriptionDurations> _SubDurationunitOfWork, IUnitOfWork<Parking> _ParkingUnitOfWork, IdefaultRepository _idefaultRepository, ISystemLogger _Logging)
        {
            this._subscriptionRepos = _subscriptionRepos;
            this._SubscriptionunitOfWork = _SubscriptionunitOfWork;
            this._SubDurationunitOfWork = _SubDurationunitOfWork;
            this._ParkingUnitOfWork = _ParkingUnitOfWork;
            this._idefaultRepository = _idefaultRepository;
            this._Logging = _Logging;
        }

        public async Task<IActionResult> Index()
        {
            var sessionUser = JsonConvert.DeserializeObject<CurrentLoginUser>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserImg = sessionUser.UserImg;
            ViewBag.UserName = sessionUser.UserName;
            var Priv = await _idefaultRepository.GetPrivilage(sessionUser.UserId, FormKey.Trim());
            try
            {
                if (Priv != null)
                {
                    if (Priv.User_View != null && Priv.User_View == true)
                    {
                        var subscriptionsList = _subscriptionRepos.GetALLSub(sessionUser.ParkingId);

                        if (subscriptionsList != null)
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "View", "Success Get index View", "");
                            return View(subscriptionsList);
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
                    if (id == null) // for Add
                    {
                        if (Priv.User_Add == true)
                        {
                            var ParkingList = _ParkingUnitOfWork.Repository.where(x => x.IsDeleted == false && x.IsEnable == true).ToList();
                            ViewBag.ParkingsList = new SelectList(ParkingList, "Id", "NameEn");
                            var SubscriptionDurationVM = new SubscriptionViewModel();
                            SubscriptionDurationVM.Durationlist = new List<SubscriptionDurations>();
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add", "Success Get Add View", "");
                            return View(SubscriptionDurationVM);
                        }
                        else
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add", "Not Authorized to View This Page", "");
                            return Redirect("/Home/ClientSideError404");
                        }
                    }
                    else    // For Edit
                    {
                        if (Priv.User_Edit == true)
                        {
                            var subscription = await _SubscriptionunitOfWork.Repository.GetById_FindAsync(id.Value);
                            var durationlist = await _SubDurationunitOfWork.Repository.where(x => x.SubscriptionId == subscription.Id).ToListAsync();
                            var ParkingList = _ParkingUnitOfWork.Repository.where(x => x.IsDeleted == false && x.IsEnable == true).ToList();
                            //ViewBag.ParkingsList = new SelectList(ParkingList, "Id", "NameEn");

                            var currentSubDur = await _subscriptionRepos.EditthisId(id.Value);

                            var SubscriptionDurationVM = new SubscriptionViewModel();
                            SubscriptionDurationVM.Durationlist = new List<SubscriptionDurations>();

                            SubscriptionDurationVM.Id = subscription.Id;
                            SubscriptionDurationVM.Name = subscription.Name;
                            SubscriptionDurationVM.Image = subscription.Image;
                            SubscriptionDurationVM.Description = subscription.Description;
                            SubscriptionDurationVM.IsEnable = subscription.IsEnable;
                            SubscriptionDurationVM.ParkingId = subscription.ParkingId;

                            foreach (var item in currentSubDur)
                            {
                                SubscriptionDurationVM.Durationlist.Add(new SubscriptionDurations
                                {
                                    Id = item.Id,
                                    durationInDays = item.durationInDays,
                                    DaysCount = item.DaysCount,
                                    HoursCount = item.HoursCount,
                                    VechicleCount = item.VechicleCount,
                                    Price = item.Price,
                                    PriceMulti = item.PriceMulti,
                                    AllDays = item.AllDays,
                                    AllTime = item.AllTime,
                                    IsMullti = item.IsMullti,
                                    SubscriptionId = subscription.Id,

                                    IsEnable = subscription.IsEnable,
                                    LastModifiedDate = DateTime.Now,
                                    ModifiedUserId = 1 // will set from session later
                                });
                            }
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit", "Success Get Update View", "");
                            return View(SubscriptionDurationVM);
                        }
                        else
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit", "Not Authorized to View This Page", "");
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
        public async Task<IActionResult> Createnew(SubscriptionViewModel_Ajax model)
        {
            var sessionUser = JsonConvert.DeserializeObject<CurrentLoginUser>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserImg = sessionUser.UserImg;
            ViewBag.UserName = sessionUser.UserName;
            var Priv = await _idefaultRepository.GetPrivilage(sessionUser.UserId, FormKey.Trim());

            try
            {
                if (Priv != null)
                {
                    if (model.Id == null)    // for Add
                    {
                        if (Priv.User_Add == true)
                        {
                            var subscription = new Subscription
                            {
                                Name = model.Name,
                                Image = "",
                                IsEnable = model.IsEnable,
                                Description = model.Description,
                                ParkingId = sessionUser.ParkingId,

                                IsDeleted = false,
                                CreatedDate = DateTime.Now,
                                LastModifiedDate = DateTime.Now,
                                ModifiedUserId = 0,
                                CreatedUserId = 1, // Id of Current loged in user      
                            };

                            await _SubscriptionunitOfWork.Repository.Add(subscription);
                            await _SubscriptionunitOfWork.Commit();

                            // Add Subscription Duration List
                            var DurationList = new List<SubscriptionDurations>();

                            if (model.Durationlist != null)
                            {
                                foreach (var item in model.Durationlist)
                                {
                                    DurationList.Add(new SubscriptionDurations
                                    {
                                        durationInDays = item.durationInDays,
                                        DaysCount = item.DaysCount,
                                        HoursCount = item.HoursCount,
                                        VechicleCount = item.VechicleCount,
                                        Price = item.Price,
                                        PriceMulti = item.PriceMulti,
                                        AllDays = item.AllDays,
                                        AllTime = item.AllTime,
                                        IsMullti = item.IsMullti,
                                        SubscriptionId = subscription.Id,

                                        IsEnable = model.IsEnable,
                                        IsDeleted = false,
                                        CreatedDate = DateTime.Now,
                                        LastModifiedDate = DateTime.Now,
                                        CreatedUserId = 0,
                                        ModifiedUserId = 1
                                    });
                                }
                            }

                            await _SubDurationunitOfWork.Repository.Add(DurationList);// Add only Durations without Subcription
                            await _SubDurationunitOfWork.Commit();
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", "Add Successed", "");
                            return RedirectToAction("Index", "Subscription");
                        }
                        else
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", "Not Authorized User to do this action", "");
                            return Redirect("/Home/ClientSideError404");
                        }
                    }
                    if (model.Id != null)    // For Edit
                    {
                        if (Priv.User_Edit == true)
                        {
                            var editedSubscription = await _SubscriptionunitOfWork.Repository.GetById_FirstOrDefAsync(model.Id.Value);
                            editedSubscription.Id = model.Id.Value;
                            editedSubscription.Name = model.Name;
                            editedSubscription.Image = model.Image;
                            editedSubscription.Description = model.Description;
                            editedSubscription.ParkingId = model.ParkingId;
                            editedSubscription.IsEnable = model.IsEnable;
                            editedSubscription.LastModifiedDate = DateTime.Now;
                            editedSubscription.ModifiedUserId = 1; // will be set from Session Later

                            _SubscriptionunitOfWork.Repository.Update(editedSubscription);
                            await _SubscriptionunitOfWork.Commit();

                            if (model.Durationlist != null)
                            {
                                var DurationList = new List<SubscriptionDurations>();
                                foreach (var item in model.Durationlist)
                                {
                                    DurationList.Add(new SubscriptionDurations
                                    {
                                        Id = item.Id.Value,
                                        durationInDays = item.durationInDays,
                                        DaysCount = item.DaysCount,
                                        HoursCount = item.HoursCount,
                                        VechicleCount = item.VechicleCount,
                                        Price = item.Price,
                                        PriceMulti = item.PriceMulti,
                                        AllDays = item.AllDays,
                                        AllTime = item.AllTime,
                                        IsMullti = item.IsMullti,
                                        SubscriptionId = model.Id.Value,

                                        IsEnable = model.IsEnable,
                                        LastModifiedDate = DateTime.Now,
                                        ModifiedUserId = 1 // will set from session later
                                    });
                                }

                                _SubDurationunitOfWork.Repository.Update(DurationList);
                                await _SubDurationunitOfWork.Commit();
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", "Update Successed", "");
                                return RedirectToAction("Index", "Subscription");
                            }
                            else
                            {
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", "Error List is Empty or null", "");
                                return Redirect("/Home/ClientSideError404");
                            }
                        }
                        else
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", "Id NotFound", "");
                            return Redirect("/Home/ClientSideError404");
                        }
                    }
                    else
                    {

                        return Redirect("/Home/ClientSideError404");
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
            var Priv = await _idefaultRepository.GetPrivilage(sessionUser.UserId, FormKey);
            try
            {
                if (Priv != null)
                {
                    if (Priv.User_Delete == true)
                    {
                        var subscription = await _SubscriptionunitOfWork.Repository.GetById_FindAsync(id);
                        subscription.LastModifiedDate = DateTime.Now;
                        subscription.IsEnable = false;
                        subscription.IsDeleted = true;

                        var Sub_durationslist = await _SubDurationunitOfWork.Repository.where(s => s.SubscriptionId == subscription.Id).ToListAsync();
                        if (Sub_durationslist != null)
                        {
                            foreach (var item in Sub_durationslist)
                            {
                                item.IsDeleted = true;
                                item.IsEnable = false;
                                item.LastModifiedDate = DateTime.Now;
                            }
                        }

                        await _SubscriptionunitOfWork.Commit();
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
            var Priv = await _idefaultRepository.GetPrivilage(sessionUser.UserId, FormKey);
            try
            {
                if (Priv != null)
                {
                    if (Priv.User_Print == true)
                    {
                        var parkings = _SubscriptionunitOfWork.Repository.All();
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Subscription");
                            var currentRow = 1;
                            worksheet.Cell(currentRow, 1).Value = "Id";
                            worksheet.Cell(currentRow, 2).Value = "CreatedUserId";
                            worksheet.Cell(currentRow, 3).Value = "ModifiedUserId";
                            worksheet.Cell(currentRow, 4).Value = "CreatedDate";
                            worksheet.Cell(currentRow, 5).Value = "LastModifiedDate";
                            worksheet.Cell(currentRow, 6).Value = "IsEnable";
                            worksheet.Cell(currentRow, 7).Value = "IsDeleted";
                            worksheet.Cell(currentRow, 8).Value = "Name";
                            worksheet.Cell(currentRow, 9).Value = "Image";
                            worksheet.Cell(currentRow, 10).Value = "Description";
                            worksheet.Cell(currentRow, 11).Value = "ParkingFkID";

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
                                worksheet.Cell(currentRow, 8).Value = item.Name;
                                worksheet.Cell(currentRow, 9).Value = item.Image;
                                worksheet.Cell(currentRow, 10).Value = item.Description;
                                worksheet.Cell(currentRow, 11).Value = item.ParkingId;
                            }
                            using (var stream = new MemoryStream())
                            {
                                workbook.SaveAs(stream);
                                var content = stream.ToArray();
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Excel", "Success to Download Excel file", "");
                                return File(content, "application/vnd.openxmlformats-officdocument.spreadsheetml.sheet", $"SubscriptionsList{DateTime.Now.Date.Hour}.xlsx");
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

        private bool SubscriptionExists(int id)
        {
            return _SubscriptionunitOfWork.Repository.Get_Any(id);
        }

    }
}
