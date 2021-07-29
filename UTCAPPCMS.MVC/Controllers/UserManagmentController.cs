using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.Repository.Interfaces;
using UTCAPPCMS.MVC.Helpers;
using UTCAPPCMS.MVC.Models;

namespace UTCAPPCMS.MVC.Controllers
{
    public class UserManagmentController : Controller
    {
        private readonly string FormKey = "UserManagment";
        private readonly string ImgPath = "/Files/AdmistratorImg/";

        private readonly IUnitOfWork<AdminUsers> _AdminUsersUnitOfWork;
        private readonly IUnitOfWork<UserPrivilage> _UserPrivilageUnitOfWork;
        private readonly IUnitOfWork<UserType> _UserTypeUnitOfWork;
        private readonly IUnitOfWork<GroupPrivilage> _GroupPrivilageUnitOfWork;
        private readonly IUnitOfWork<FormPages> _FormPagesUnitOfWork;
        private readonly IUnitOfWork<UserGroup> _UserGroupUnitOfWork;

        private readonly IUserManagmentRepos _userManagmentRepos;

        private readonly IdefaultRepository _idefaultRepository;
        private readonly ISystemLogger _Logging;
        public UserManagmentController(IUnitOfWork<FormPages> _FormPagesUnitOfWork, IUserManagmentRepos _userManagmentRepos, IUnitOfWork<AdminUsers> _AdminUsersUnitOfWork, IUnitOfWork<UserPrivilage> _UserPrivilageUnitOfWork, IUnitOfWork<UserType> _UserTypeUnitOfWork, IUnitOfWork<GroupPrivilage> _GroupPrivilageUnitOfWork, IUnitOfWork<UserGroup> _UserGroupUnitOfWork, IdefaultRepository _idefaultRepository, ISystemLogger _Logging)
        {
            this._FormPagesUnitOfWork = _FormPagesUnitOfWork;
            this._userManagmentRepos = _userManagmentRepos;
            this._AdminUsersUnitOfWork = _AdminUsersUnitOfWork;
            this._UserPrivilageUnitOfWork = _UserPrivilageUnitOfWork;
            this._UserTypeUnitOfWork = _UserTypeUnitOfWork;
            this._GroupPrivilageUnitOfWork = _GroupPrivilageUnitOfWork;
            this._UserGroupUnitOfWork = _UserGroupUnitOfWork;
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
                        var AdminUserList = _AdminUsersUnitOfWork.Repository.where(x => x.IsDeleted == false && x.IsEnable == true&&x.ParkingId== sessionUser.ParkingId).ToList();
                        if (AdminUserList != null)
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "View", "Success Get index View", "");
                            return View(AdminUserList);
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
            var Priv = await _idefaultRepository.GetPrivilage(sessionUser.UserId, FormKey);

            try
            {
                if (Priv != null)
                {
                    if (id == null)    // For Add
                    {
                        if (Priv.User_Add == true)
                        {
                            var UserTypeList = _UserTypeUnitOfWork.Repository.where(x => x.IsDeleted == false && x.IsEnable == true).ToList();
                            ViewBag.UserType = new SelectList(UserTypeList, "Id", "NameEn");

                            var UserGroupList = _UserGroupUnitOfWork.Repository.where(x => x.IsDeleted == false && x.IsEnable == true).ToList();
                            ViewBag.Privileges = new SelectList(UserGroupList, "Id", "NameEn");

                            var UserManagmentVM = new UserManagmentViewModa();
                            UserManagmentVM._AdminUsers = new AdminUsers();
                            UserManagmentVM._FormPagePrivilagesList = new List<FormPageUserPrivilage>();

                            var Input_UserPrivIncludeFormPageList = await _FormPagesUnitOfWork.Repository.AllAsync();

                            foreach (var item in Input_UserPrivIncludeFormPageList)
                            {
                                UserManagmentVM._AdminUsers.Fullname = string.Empty;
                                UserManagmentVM._AdminUsers.Email = string.Empty;
                                UserManagmentVM._AdminUsers.Mobile = string.Empty;
                                UserManagmentVM._AdminUsers.Username = string.Empty;
                                UserManagmentVM._AdminUsers.Password = string.Empty;
                                UserManagmentVM._AdminUsers.Image = string.Empty;
                                UserManagmentVM._AdminUsers.ParkingId = null;
                                UserManagmentVM._AdminUsers.UserTypeId = null;
                                UserManagmentVM._AdminUsers.PrivilageGroupId = null;

                                UserManagmentVM._FormPagePrivilagesList.Add(new FormPageUserPrivilage
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
                            return View(UserManagmentVM);
                        }
                        {
                            return Redirect("/Home/ClientSideError404");
                        }
                    }
                    else    // For Edit
                    {
                        if (Priv.User_Edit == true)
                        {
                            var UserManagmentVM = new UserManagmentViewModa();
                            UserManagmentVM._AdminUsers = new AdminUsers();
                            UserManagmentVM._FormPagePrivilagesList = new List<FormPageUserPrivilage>();

                            var _CurrentAdminUser = await _userManagmentRepos.EditthisId(id);
                            var _CurrentUser = await _userManagmentRepos.GetUserById(id);


                            var UserTypeList = _UserTypeUnitOfWork.Repository.where(x => x.IsDeleted == false && x.IsEnable == true).ToList();
                            ViewBag.UserType = new SelectList(UserTypeList, "Id", "NameEn");

                            var UserGroupList = _UserGroupUnitOfWork.Repository.where(x => x.IsDeleted == false && x.IsEnable == true).ToList();
                            ViewBag.Privileges = new SelectList(UserGroupList, "Id", "NameEn");
                            UserManagmentVM._AdminUsers.Id = id.Value;
                            UserManagmentVM._AdminUsers.Fullname = _CurrentUser.Fullname;
                            UserManagmentVM._AdminUsers.Email = _CurrentUser.Email;
                            UserManagmentVM._AdminUsers.Mobile = _CurrentUser.Mobile;
                            UserManagmentVM._AdminUsers.Username = _CurrentUser.Username;
                            UserManagmentVM._AdminUsers.Password = _CurrentUser.Password;
                            UserManagmentVM._AdminUsers.Image = _CurrentUser.Image;
                            UserManagmentVM._AdminUsers.ParkingId = _CurrentUser.ParkingId;
                            UserManagmentVM._AdminUsers.UserTypeId = _CurrentUser.UserTypeId;
                            UserManagmentVM._AdminUsers.PrivilageGroupId = _CurrentUser.PrivilageGroupId;

                            UserManagmentVM.IsEnable = _CurrentUser.IsEnable;
                            foreach (var item in _CurrentAdminUser)
                            {


                                UserManagmentVM._FormPagePrivilagesList.Add(new FormPageUserPrivilage
                                {
                                    Id = item.Id,
                                    FormKey = item.FormPages.FormKey,
                                    View = item.View.Value,
                                    Add = item.Add.Value,
                                    Edit = item.Edit.Value,
                                    Delete = item.Delete.Value,
                                    Search = item.Search.Value,
                                    Print = item.Print.Value,
                                });
                            }
                            
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit", "Success Get Update View", "");
                            return View(UserManagmentVM);
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
        [AllowAnonymous]
        public async Task<IActionResult> Save(UserManagmentViewModel_ajax model)// if model.Email is in database don't add it again
        {
            var sessionUser = JsonConvert.DeserializeObject<CurrentLoginUser>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserImg = sessionUser.UserImg;
            ViewBag.UserName = sessionUser.UserName;
            var Priv = await _idefaultRepository.GetPrivilage(sessionUser.UserId, FormKey);
            try
            {
                if (Priv != null)
                {
                    if (model.Id == null || model.Id == 0)    // For Add
                    {
                        if (Priv.User_Add == true)
                        {
                            var AdminUser = new AdminUsers
                            {
                                CreatedDate = DateTime.Now,
                                CreatedUserId = sessionUser.UserId,
                                ModifiedUserId = 0,
                                LastModifiedDate = DateTime.Now,
                                IsDeleted = false,
                                IsEnable = model.IsEnable,
                                Fullname = model.Fullname,
                                Email = model.Email,
                                Mobile = model.Mobile,
                                Username = model.Username,
                                Password = HashingHelperMD5.CreateMD5(model.Password),// will be Hashed
                                Image = model.Image,
                                ParkingId = sessionUser.ParkingId, // Set From Session logged in User
                                UserTypeId = model.UserTypeId,
                                PrivilageGroupId = model.PrivilageGroupId
                            };

                            await _AdminUsersUnitOfWork.Repository.Add(AdminUser);
                            await _AdminUsersUnitOfWork.Commit();

                            if (model.Priv_tableData != null)
                            {
                                var currentAdminUser = _AdminUsersUnitOfWork.Repository.where(x => x.IsEnable == true && x.IsDeleted == false && x.Email == model.Email && x.Username == model.Username).FirstOrDefault();
                                var UserPrivilageList = new List<UserPrivilage>();
                                foreach (var item in model.Priv_tableData)
                                {
                                    var formPage = _FormPagesUnitOfWork.Repository.where(x => x.IsDeleted == false && x.IsEnable == true && x.FormKey == item.FormKey.Trim()).FirstOrDefault();
                                    UserPrivilageList.Add(new UserPrivilage
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
                                        AdminUsersId = currentAdminUser.Id,
                                        FormPagesId = formPage.Id
                                    });
                                }
                                await _UserPrivilageUnitOfWork.Repository.Add(UserPrivilageList);
                                await _UserPrivilageUnitOfWork.Commit();
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", "Update Successed", "");
                            }
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", "Update Successed", "");
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", "Not Authorized User to do this action", "");
                            return Redirect("/Home/ClientSideError404");
                        }
                    }
                    if (model.Id != null || model.Id != 0)    // For Edit
                    {
                        if (Priv.User_Edit == true)
                        {
                            var editedAdminUser = await _AdminUsersUnitOfWork.Repository.GetById_FirstOrDefAsync(model.Id.Value);

                            editedAdminUser.Id = model.Id.Value;
                            editedAdminUser.ModifiedUserId = sessionUser.UserId; // Will assign from session
                            editedAdminUser.LastModifiedDate = DateTime.Now;
                            editedAdminUser.Fullname = model.Fullname;
                            editedAdminUser.Username = model.Username;
                            editedAdminUser.Email = model.Email;
                            editedAdminUser.Password = model.Password;
                            editedAdminUser.IsEnable = model.IsEnable;
                            editedAdminUser.Mobile = model.Mobile;
                            editedAdminUser.ParkingId = sessionUser.ParkingId; // will assign from session
                            editedAdminUser.PrivilageGroupId = model.PrivilageGroupId;
                            editedAdminUser.UserTypeId = model.UserTypeId;
                            editedAdminUser.Image = model.Image;    // will be assign from IFormFile latter

                            _AdminUsersUnitOfWork.Repository.Update(editedAdminUser);
                            await _AdminUsersUnitOfWork.Commit();

                            if (model.Priv_tableData != null)
                            {
                                var currentAdminUser = _AdminUsersUnitOfWork.Repository.where(x => x.IsEnable == true && x.IsDeleted == false && x.Email == model.Email && x.Username == model.Username).FirstOrDefault();
                                var UserPrivilageList = new List<UserPrivilage>();
                                foreach (var item in model.Priv_tableData)
                                {
                                    var formPage = _FormPagesUnitOfWork.Repository.where(x => x.IsDeleted == false && x.IsEnable == true && x.FormKey == item.FormKey.Trim()).FirstOrDefault();
                                    UserPrivilageList.Add(new UserPrivilage
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
                                        ModifiedUserId = 8, // will be assign from session
                                        AdminUsersId = currentAdminUser.Id, // or model.Id.Value
                                        FormPagesId = formPage.Id
                                    });
                                }
                                _UserPrivilageUnitOfWork.Repository.Update(UserPrivilageList);
                                await _UserPrivilageUnitOfWork.Commit();
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", "Update Successed", "");
                            }
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", "Update Successed", "");
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", "Not Authorized User to do this action", "");
                            return Redirect("/Home/ClientSideError404");
                        }
                    }
                    return RedirectToAction("Index");
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
        public ActionResult img(IFormFile Input_IMG)
        {
            if (Input_IMG != null)
            {

                string FilePath = null;
                if (Input_IMG.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(Input_IMG.FileName);

                    //Assigning Unique Filename (Guid)
                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);

                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(myUniqueFileName, fileExtension);

                    // Combines two strings into a path.
                    var filepath =
                    new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "AdmistratorImg")).Root + $@"\{newFileName}";
                    FilePath = ImgPath + newFileName;
                    using (FileStream fs = System.IO.File.Create(filepath))
                    {
                        Input_IMG.CopyTo(fs);
                        fs.Flush();
                    }
                }
                return Json(FilePath);
            }
            return Json(null);
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
                if (Priv.User_Delete == true)
                {
                    var User = await _AdminUsersUnitOfWork.Repository.GetById_FindAsync(id);

                    User.IsEnable = false;
                    User.IsDeleted = true;
                    User.ModifiedUserId = sessionUser.UserId;
                    User.LastModifiedDate = DateTime.Now;

                    _AdminUsersUnitOfWork.Repository.Update(User);
                    await _AdminUsersUnitOfWork.Commit();

                    var UserPrivilageList = _UserPrivilageUnitOfWork.Repository.where(x => x.AdminUsersId == id).ToList();

                    if (UserPrivilageList != null)
                    {
                        foreach (var item in UserPrivilageList)
                        {
                            item.ModifiedUserId = sessionUser.UserId;
                            item.LastModifiedDate = DateTime.Now;
                            item.IsEnable = false;
                            item.IsDeleted = true;
                        }
                        _UserPrivilageUnitOfWork.Repository.Update(UserPrivilageList);
                        await _UserPrivilageUnitOfWork.Commit();
                        await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Delete", $"Success Deleting ID {id}", "");
                    }
                    await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Delete", $"Success Deleting ID {id}", "");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Delete", "Not Authorized to do this Action", "");
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
                        var AdminUserList = _AdminUsersUnitOfWork.Repository.where(x => x.IsDeleted == false && x.IsEnable == true).ToList();
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
                            worksheet.Cell(currentRow, 8).Value = "Fullname";
                            worksheet.Cell(currentRow, 9).Value = "Username";
                            worksheet.Cell(currentRow, 10).Value = "Email";
                            foreach (var item in AdminUserList)
                            {
                                currentRow++;
                                worksheet.Cell(currentRow, 1).Value = item.Id;
                                worksheet.Cell(currentRow, 2).Value = item.CreatedUserId;
                                worksheet.Cell(currentRow, 3).Value = item.ModifiedUserId;
                                worksheet.Cell(currentRow, 4).Value = item.CreatedDate;
                                worksheet.Cell(currentRow, 5).Value = item.LastModifiedDate;
                                worksheet.Cell(currentRow, 6).Value = item.IsEnable;
                                worksheet.Cell(currentRow, 7).Value = item.IsDeleted;
                                worksheet.Cell(currentRow, 8).Value = item.Fullname;
                                worksheet.Cell(currentRow, 9).Value = item.Username;
                                worksheet.Cell(currentRow, 10).Value = item.Email;
                            }
                            using (var stream = new MemoryStream())
                            {
                                workbook.SaveAs(stream);
                                var content = stream.ToArray();
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Excel", "Success to Download Excel file", "");
                                return File(content, "application/vnd.openxmlformats-officdocument.spreadsheetml.sheet", $"AdminUserList{DateTime.Now.Date.Hour}.xlsx");
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
