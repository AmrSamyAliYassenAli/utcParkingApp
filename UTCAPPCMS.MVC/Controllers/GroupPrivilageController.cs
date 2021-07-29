using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
{   // Session and Privilage is Set
    public class GroupPrivilageController : Controller
    {
        private readonly string FormKey = "GroupPrivilage";

        private readonly IUnitOfWork<FormPages> _FormPagesUnitOfWork;
        private readonly IUnitOfWork<GroupPrivilage> _GroupPrivilageUnitOfWork;
        private readonly IUnitOfWork<UserGroup> _UserGroupUnitOfWork;

        private readonly IGroupPrivilageRepos _groupPrivilageRepos;

        private readonly IdefaultRepository _idefaultRepository;

        private readonly ISystemLogger _Logging;
        public GroupPrivilageController(IGroupPrivilageRepos _groupPrivilageRepos, IUnitOfWork<UserGroup> _UserGroupUnitOfWork,IUnitOfWork<GroupPrivilage> _GroupPrivilageUnitOfWork, IUnitOfWork<FormPages> _FormPagesUnitOfWork, IdefaultRepository _idefaultRepository, ISystemLogger _Logging)
        {
            this._groupPrivilageRepos = _groupPrivilageRepos;
            this._UserGroupUnitOfWork = _UserGroupUnitOfWork;
            this._GroupPrivilageUnitOfWork = _GroupPrivilageUnitOfWork;
            this._FormPagesUnitOfWork = _FormPagesUnitOfWork;
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
                        var UserGroupList = _UserGroupUnitOfWork.Repository.where(x => x.IsDeleted == false && x.IsEnable == true&&(x.ParkingId==sessionUser.ParkingId ||(sessionUser.ParkingId==null&&x.ParkingId==null))).ToList();
                        if (UserGroupList != null)
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "View", "Success Get index View", "");
                            return View(UserGroupList);
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
                    if (id != null) // for edit
                    {
                        if (Priv.User_Edit == true)
                        {
                            var _CurrentGroupPrivilage = await _groupPrivilageRepos.EditthisId(id);

                            var GroupPrivilageVM = new GroupPrivilageViewModel();
                            GroupPrivilageVM._UserGroup = new UserGroup();
                            GroupPrivilageVM._FormPagePrivilagesList = new List<FormPageUserPrivilage>();


                            foreach (var item in _CurrentGroupPrivilage)
                            {
                                GroupPrivilageVM.Id = id;
                                GroupPrivilageVM._UserGroup.NameEn = item.UserGroup.NameEn;
                                GroupPrivilageVM._UserGroup.NameAr = item.UserGroup.NameAr;

                                GroupPrivilageVM._FormPagePrivilagesList.Add(new FormPageUserPrivilage
                                {
                                    Id = item.Id,
                                    FormKey = item.FormPages.FormKey,
                                    View = item.View.Value,
                                    Add = item.Add.Value,
                                    Edit = item.Edit.Value,
                                    Delete = item.Delete.Value,
                                    Search = item.Search.Value,
                                    Print = item.Print.Value
                                });
                            }
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit", "Success Get Update View", "");
                            return View(GroupPrivilageVM);
                        }
                        else
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit", "Not Authorized to View This Page", "");
                            return Redirect("/Home/ClientSideError404");
                        }
                    }
                    else    // for Add
                    {
                        if (Priv.User_Add == true)
                        {
                            var GroupPrivilageVM = new GroupPrivilageViewModel();
                            GroupPrivilageVM._UserGroup = new UserGroup();
                            GroupPrivilageVM._FormPagePrivilagesList = new List<FormPageUserPrivilage>();

                            var formPages = await _groupPrivilageRepos.GetAllFormKeys();
                            if (formPages != null)
                            {
                                foreach (var item in formPages)
                                {
                                    GroupPrivilageVM._UserGroup.NameEn = string.Empty;
                                    GroupPrivilageVM._UserGroup.NameAr = string.Empty;
                                    GroupPrivilageVM._UserGroup.ParkingId = sessionUser.ParkingId;
                                    GroupPrivilageVM._FormPagePrivilagesList.Add(new FormPageUserPrivilage
                                    {
                                        FormKey = item.FormKey,
                                        View = false,
                                        Add = false,
                                        Delete = false,
                                        Search = false,
                                        Edit = false,
                                        Print = false
                                    });
                                }
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add", "Success Get Add View", "");
                                return View(GroupPrivilageVM);
                            }
                            else
                            {
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add", "Error Can't Access FormPages Table Data", "");
                                return Redirect("/Home/ClientSideError404");
                            }
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
        [AllowAnonymous]
        public async Task<IActionResult> Save(GroupPrivilageViewModel_ajax model)
        {
            var sessionUser = JsonConvert.DeserializeObject<CurrentLoginUser>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserImg = sessionUser.UserImg;
            ViewBag.UserName = sessionUser.UserName;
            var Priv = await _idefaultRepository.GetPrivilage(sessionUser.UserId, FormKey.Trim());
            try
            {
                if (Priv != null)
                {

                    if (model.Id == null)    // For Add
                    {
                        if (Priv.User_Add == true)
                        {
                            if (ModelState.IsValid)
                            {
                                var UserGroup = new UserGroup
                                {
                                    CreatedDate = DateTime.Now,
                                    CreatedUserId = 0,
                                    ModifiedUserId = 0,
                                    LastModifiedDate = DateTime.Now,
                                    IsDeleted = false,
                                    IsEnable = model.IsEnable,
                                    NameAr = model.ArabicName,
                                    NameEn = model.EnglishName,
                                    ParkingId=sessionUser.ParkingId
                                };

                                await _UserGroupUnitOfWork.Repository.Add(UserGroup);
                                await _UserGroupUnitOfWork.Commit();

                                var currentUserGroup = _UserGroupUnitOfWork.Repository.where(x => x.NameEn == UserGroup.NameEn && x.NameAr == UserGroup.NameAr).SingleOrDefault();
                                var GroupPrivilage = new List<GroupPrivilage>(); // Group Privilage
                                try
                                {
                                    if (model.Priv_tableData != null)
                                    {
                                        foreach (var item in model.Priv_tableData)
                                        {
                                            var formPageId = _FormPagesUnitOfWork.Repository.where(x => x.FormKey == item.FormKey.Trim()).FirstOrDefault();
                                            GroupPrivilage.Add(new GroupPrivilage
                                            {
                                                Add = item.Add,
                                                Edit = item.Edit,
                                                View = item.View,
                                                Search = item.Search,
                                                Delete = item.Delete,
                                                Print = item.Print,
                                                IsEnable = model.IsEnable,
                                                IsDeleted = false,
                                                CreatedDate = DateTime.Now,
                                                LastModifiedDate = DateTime.Now,
                                                CreatedUserId = 1,
                                                ModifiedUserId = 8,
                                                UserGroupId = currentUserGroup.Id,
                                                ParkingId = 27, // will be Parking ID From Logged in Session
                                                FormPagesId = formPageId.Id
                                            });
                                        }
                                    }

                                    await _GroupPrivilageUnitOfWork.Repository.Add(GroupPrivilage);
                                    await _GroupPrivilageUnitOfWork.Commit();
                                    await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", "Add Successed", "");
                                }
                                catch (Exception ex)
                                {
                                    await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add Post Method", ex.Message, "");
                                    return Redirect("/Home/ServerSideError500");
                                }


                                return RedirectToAction("Index");
                            }
                            else
                            {
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", "Model State is InValid", "");
                            }
                        }
                        else
                        {
                            return Redirect("/Home/ClientSideError404");
                        }
                    }
                    if (model.Id != null) // Edit Apply
                    {
                        if (Priv.User_Edit == true)
                        {
                            var editedUserGroup = await _UserGroupUnitOfWork.Repository.GetById_FirstOrDefAsync(model.Id.Value);
                            editedUserGroup.ModifiedUserId = 1; // will assign from session log in
                            editedUserGroup.LastModifiedDate = DateTime.Now;
                            editedUserGroup.NameAr = model.ArabicName;
                            editedUserGroup.NameEn = model.EnglishName;
                            editedUserGroup.IsEnable = model.IsEnable;

                            _UserGroupUnitOfWork.Repository.Update(editedUserGroup);
                            await _UserGroupUnitOfWork.Commit();

                            var currentUserGroup = _UserGroupUnitOfWork.Repository.where(x => x.NameEn == editedUserGroup.NameEn && x.NameAr == editedUserGroup.NameAr).SingleOrDefault();
                            var GroupPrivilage = new List<GroupPrivilage>(); // Group Privilage
                            try
                            {
                                if (model.Priv_tableData != null)
                                {
                                    foreach (var item in model.Priv_tableData)
                                    {
                                        var formPageId = _FormPagesUnitOfWork.Repository.where(x => x.FormKey == item.FormKey.Trim()).FirstOrDefault();
                                        GroupPrivilage.Add(new GroupPrivilage
                                        {
                                            Id = item.Id.Value,
                                            Add = item.Add,
                                            Edit = item.Edit,
                                            View = item.View,
                                            Search = item.Search,
                                            Delete = item.Delete,
                                            Print = item.Print,
                                            IsEnable = model.IsEnable,
                                            IsDeleted = false,
                                            LastModifiedDate = DateTime.Now,
                                            CreatedUserId = 1,
                                            ModifiedUserId = 8,
                                            UserGroupId = currentUserGroup.Id,
                                            ParkingId = 27, // will be Parking ID From Logged in Session
                                            FormPagesId = formPageId.Id
                                        });
                                    }
                                }
                                else
                                {
                                    await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", $"Error Null Refrance :Privilage Table is Null From Ajax Model", "");
                                }
                                _GroupPrivilageUnitOfWork.Repository.Update(GroupPrivilage);
                                await _GroupPrivilageUnitOfWork.Commit();
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", "Update Successed", "");
                                return RedirectToAction("Index");
                            }
                            catch (Exception ex)
                            {
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add Post Method", ex.Message, "");
                                return Redirect("/Home/ServerSideError500");
                            }
                        }
                        else
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", "Not Authorized User to do this action", "");
                            return Redirect("/Home/ClientSideError404");
                        }
                    }
                    else
                    {
                        await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", $"Error Id is NofFound {model.Id}", "");
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
            var Priv = await _idefaultRepository.GetPrivilage(sessionUser.UserId, FormKey.Trim());
            try { 
                if (Priv != null)
                {
                    if (Priv.User_Delete == true)
                    {
                        var User = await _UserGroupUnitOfWork.Repository.GetById_FindAsync(id);

                        User.IsEnable = false;
                        User.IsDeleted = true;
                        User.ModifiedUserId = sessionUser.UserId;    // Id of Logged in from Session
                        User.LastModifiedDate = DateTime.Now;

                        _UserGroupUnitOfWork.Repository.Update(User);
                        await _UserGroupUnitOfWork.Commit();

                        var GroupPrivilageList = _GroupPrivilageUnitOfWork.Repository.where(x => x.UserGroupId == id).ToList();
                        if (GroupPrivilageList != null)
                        {
                            foreach (var item in GroupPrivilageList)
                            {
                                item.ModifiedUserId = sessionUser.UserId;
                                item.LastModifiedDate = DateTime.Now;
                                item.IsEnable = false;
                                item.IsDeleted = true;
                            }
                            _GroupPrivilageUnitOfWork.Repository.Update(GroupPrivilageList);
                            await _GroupPrivilageUnitOfWork.Commit();
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Delete", $"Success Deleting ID {id}", "");
                        }
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
                        var UserGroupList = _UserGroupUnitOfWork.Repository.where(x => x.IsDeleted == false && x.IsEnable == true).ToList();
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("AdminUserList");
                            var currentRow = 1;
                            worksheet.Cell(currentRow, 1).Value = "Id";
                            worksheet.Cell(currentRow, 2).Value = "CreatedUserId";
                            worksheet.Cell(currentRow, 3).Value = "ModifiedUserId";
                            worksheet.Cell(currentRow, 4).Value = "CreatedDate";
                            worksheet.Cell(currentRow, 5).Value = "LastModifiedDate";
                            worksheet.Cell(currentRow, 6).Value = "IsEnable";
                            worksheet.Cell(currentRow, 7).Value = "IsDeleted";
                            worksheet.Cell(currentRow, 8).Value = "English Name";

                            foreach (var item in UserGroupList)
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

                            }
                            using (var stream = new MemoryStream())
                            {
                                workbook.SaveAs(stream);
                                var content = stream.ToArray();
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Excel", "Success to Download Excel file", "");
                                return File(content, "application/vnd.openxmlformats-officdocument.spreadsheetml.sheet", $"GroupList{DateTime.Now.Date.Hour}.xlsx");
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
    }
}
