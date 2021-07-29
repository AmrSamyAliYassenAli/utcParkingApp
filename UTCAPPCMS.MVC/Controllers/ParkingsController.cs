using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.Repository.Interfaces;
using UTCAPPCMS.MVC.Helpers;
using UTCAPPCMS.MVC.Models;

namespace UTCAPPCMS.MVC.Controllers
{// Session and Privilage is Set
    public class ParkingsController : Controller
    {
        private readonly IUnitOfWork<Parking> _unitOfWork;
        private readonly IUnitOfWork<UserType> _UserTypeunitOfWork;
        private readonly IUnitOfWork<FormPages> _FormPagesunitOfWork;

        private readonly IWebHostEnvironment _iwebHost;
        private readonly IdefaultRepository _idefaultRepository;
        private readonly IUserManagmentRepos _userManagmentRepos;
        private readonly ISystemLogger _Logging;

        private readonly string FormKey = "Parking";
        private readonly string ImgPath = "/Files/Images/";

        public ParkingsController(IUnitOfWork<FormPages> _FormPagesunitOfWork,IUnitOfWork<UserType> _UserTypeunitOfWork,IdefaultRepository _idefaultRepository, IUnitOfWork<Parking> _unitOfWork, ISystemLogger _Logging, IUserManagmentRepos _userManagmentRepos, IWebHostEnvironment _iwebHost)
        {
            this._unitOfWork = _unitOfWork;
            this._iwebHost = _iwebHost;
            this._idefaultRepository = _idefaultRepository;
            this._Logging = _Logging;
            this._userManagmentRepos = _userManagmentRepos;
            this._UserTypeunitOfWork = _UserTypeunitOfWork;
            this._FormPagesunitOfWork = _FormPagesunitOfWork;
        }

        // GET: Parkings
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
                        var parking = new List<Parking>();
                        if (sessionUser.ParkingId == null || sessionUser.ParkingId == 0)    // Admin Account
                        {
                            parking = await _unitOfWork.Repository.where(x => x.IsDeleted == false && x.IsEnable == true).ToListAsync();
                            
                        }
                        else    // Company Accouint
                        {
                            parking = await _unitOfWork.Repository.where(x => x.IsDeleted == false && x.IsEnable == true && x.Id == sessionUser.ParkingId).ToListAsync();
                        }
                        
                        if (parking != null)
                        {

                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "View", "Success Get index View", "");
                            return View(parking);
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
                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "View",ex.Message, "");
                return Redirect("/Home/ServerSideError500");
            }    
        }

        public async Task<IActionResult> Create(int? id)
        {
            var sessionUser = JsonConvert.DeserializeObject<CurrentLoginUser>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserImg = sessionUser.UserImg;
            ViewBag.UserName = sessionUser.UserName;
            ViewBag.ID = id;
            var Priv =await _idefaultRepository.GetPrivilage(sessionUser.UserId, FormKey.Trim());
            try
            {
                if (Priv != null)
                {
                    ViewBag.SessionParkingId = sessionUser.ParkingId;
                    if (id != null)// for Edit
                    {
                        ViewBag.IdUpdate = id;
                        if (Priv.User_Edit == true)
                        {
                            if (id == null)
                            {
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit", $"Error Null Refrance : Id ({id.Value}) is Equal to null", "");
                                return NotFound();
                            }

                            var parking = await _unitOfWork.Repository.GetById_FindAsync(id.Value);
                            if (parking == null)
                            {
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit", $"Error Null Refrance : Parking Id ({id.Value}) is NotFound", "");
                                return NotFound();
                            }
                            var VMEditParking = new EditParkingViewModel
                            {
                                ReferanceKey = parking.ReferanceKey,
                                AddressAr = parking.AddressAr,
                                AddressEn = parking.AddressEn,
                                NameAr = parking.NameAr,
                                NameEn = parking.NameEn,
                                ContactEmail = parking.ContactEmail,
                                ContactName = parking.ContactName,
                                ContactPhone = parking.ContactPhone,
                                CreatedDate = parking.CreatedDate,
                                CreatedUserId = parking.CreatedUserId,
                                Description = parking.Description,
                                IsDeleted = parking.IsDeleted,
                                IsEnable = parking.IsEnable,
                                ModifiedUserId = parking.ModifiedUserId,
                                LastModifiedDate = parking.LastModifiedDate,
                                Id = parking.Id,
                                LogoPath = parking.Logo,
                                InvoiceLogoPath = parking.Invoicelogo,
                                Logo = null,
                                Invoicelogo = null
                            };
                            ViewBag.logo = parking.Logo;
                            ViewBag.inlogo = parking.Invoicelogo;
                            ViewBag.Edit = true;
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit", "Success Get Update View", "");
                            return View(VMEditParking);
                        }
                        else
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit", "Not Authorized to View This Page", "");
                            return Redirect("/Home/ClientSideError404");
                        }
                    }
                    else// for Add
                    {
                        if (Priv.User_Add == true)
                        {
                            ViewBag.Edit = false;
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
            catch(Exception ex)
            {
                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add", ex.Message, "");
                return Redirect("/Home/ServerSideError500");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id,IFormFile ifilelogo, IFormFile ifileInvoice, Parking parking)
        {
            var sessionUser = JsonConvert.DeserializeObject<CurrentLoginUser>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserImg = sessionUser.UserImg;
            ViewBag.UserName = sessionUser.UserName;
            ViewBag.ID = id;         
            var Priv =await _idefaultRepository.GetPrivilage(sessionUser.UserId, FormKey.Trim());
            string logo = null, inlogo = null;
            try
            {
                if (Priv != null)
                {
                    if (id != null)// for Edit
                    {
                        
                        if (Priv.User_Edit == true)
                        {
                            if (id != parking.Id)
                            {
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", $"Error Null Refrance : Id ({id.Value}) is Equal to null", "");
                                return NotFound();
                            }

                            if (ifilelogo != null)
                            {
                                if (ifilelogo.Length > 0)
                                {
                                    //Getting FileName
                                    var fileName = Path.GetFileName(ifilelogo.FileName);

                                    //Assigning Unique Filename (Guid)
                                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                                    //Getting file Extension
                                    var fileExtension = Path.GetExtension(fileName);

                                    // concatenating  FileName + FileExtension
                                    var newFileName = String.Concat(myUniqueFileName, fileExtension);

                                    // Combines two strings into a path.
                                    var filepath =
                                    new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images")).Root + $@"\{newFileName}";
                                    logo = ImgPath + newFileName;
                                    ViewBag.Logo = logo;
                                    using (FileStream fs = System.IO.File.Create(filepath))
                                    {
                                        ifilelogo.CopyTo(fs);
                                        fs.Flush();
                                    }

                                }
                                else
                                {
                                    await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", $"Error in Image Upload of Logo", "");
                                }
                            }
                            else
                            {
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", $"Error null value in Image Upload of Logo", "");
                            }
                            if (ifileInvoice != null)
                            {
                                if (ifileInvoice.Length > 0)
                                {
                                    //Getting FileName
                                    var fileName = Path.GetFileName(ifileInvoice.FileName);

                                    //Assigning Unique Filename (Guid)
                                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                                    //Getting file Extension
                                    var fileExtension = Path.GetExtension(fileName);

                                    // concatenating  FileName + FileExtension
                                    var newFileName = String.Concat(myUniqueFileName, fileExtension);

                                    // Combines two strings into a path.
                                    var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images")).Root + $@"\{newFileName}";
                                    //var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{newFileName}";
                                    inlogo = ImgPath + newFileName;
                                    ViewBag.InLogo = inlogo;
                                    using (FileStream fs = System.IO.File.Create(filepath))
                                    {
                                        ifileInvoice.CopyTo(fs);
                                        fs.Flush();
                                    }
                                    // inlogo = filepath;
                                }
                                else
                                {
                                    await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", $"Error in Image Upload of InVoice Logo", "");
                                }
                            }
                            else
                            {
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", $"Error null Value in Image Upload of InVoice Logo", "");
                            }
                            if (ModelState.IsValid)
                            {
                                try
                                {
                                    var oldparking = await _unitOfWork.Repository.GetById_FirstOrDefAsync(id.Value);
                                    parking.LastModifiedDate = DateTime.Now;
                                    parking.CreatedDate = oldparking.CreatedDate;
                                    parking.ModifiedUserId = sessionUser.UserId;
                                    parking.CreatedUserId = oldparking.CreatedUserId; // Id of Current loged in user
                                    parking.IsDeleted = oldparking.IsDeleted;
                                    parking.IsEnable = oldparking.IsEnable;
                                    if (logo == null) { logo = oldparking.Logo; }
                                    if (inlogo == null) { inlogo = oldparking.Invoicelogo; }
                                    parking.Logo = logo;
                                    parking.Invoicelogo = inlogo;
                                    _unitOfWork.Repository.Update(parking);
                                    await _unitOfWork.Commit();
                                }
                                catch (DbUpdateConcurrencyException ex)
                                {
                                    if (!ParkingExists(parking.Id))
                                    {
                                        await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", $"ERROR : Not Exists Parking Id : {parking.Id}", "");
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
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", "User is Authorized on this Action", "");
                            return View(parking);
                        }
                        else
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", "Not Authorized User to do this action", "");
                            return Redirect("/Home/ClientSideError404");
                        }
                    }
                    else// for Add
                    {
                        if (Priv.User_Add == true)
                        {
                            if (ifilelogo != null)
                            {
                                if (ifilelogo.Length > 0)
                                {
                                    //Getting FileName
                                    var fileName = Path.GetFileName(ifilelogo.FileName);

                                    //Assigning Unique Filename (Guid)
                                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                                    //Getting file Extension
                                    var fileExtension = Path.GetExtension(fileName);

                                    // concatenating  FileName + FileExtension
                                    var newFileName = String.Concat(myUniqueFileName, fileExtension);

                                    // Combines two strings into a path.
                                    var filepath =
                                    new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images")).Root + $@"\{newFileName}";
                                    logo = ImgPath + newFileName;
                                    ViewBag.Logo = logo;
                                    using (FileStream fs = System.IO.File.Create(filepath))
                                    {
                                        ifilelogo.CopyTo(fs);
                                        fs.Flush();
                                    }
                                }
                                else
                                {
                                    await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", $"Error in Image Upload of Logo", "");
                                }
                            }
                            else
                            {
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", $"Error in Image Upload of Logo", "");
                            }
                            if (ifileInvoice != null)
                            {
                                if (ifileInvoice.Length > 0)
                                {
                                    //Getting FileName
                                    var fileName = Path.GetFileName(ifileInvoice.FileName);

                                    //Assigning Unique Filename (Guid)
                                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                                    //Getting file Extension
                                    var fileExtension = Path.GetExtension(fileName);

                                    // concatenating  FileName + FileExtension
                                    var newFileName = String.Concat(myUniqueFileName, fileExtension);

                                    // Combines two strings into a path.
                                    var filepath =
                            new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images")).Root + $@"\{newFileName}";
                                    inlogo = ImgPath + newFileName;
                                    ViewBag.InLogo = inlogo;
                                    using (FileStream fs = System.IO.File.Create(filepath))
                                    {
                                        ifileInvoice.CopyTo(fs);
                                        fs.Flush();
                                    }
                                    // inlogo = filepath;
                                }
                                else
                                {
                                    await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", $"Error in Image Upload of Invoice Logo", "");
                                }
                            }
                            else
                            {
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", $"Error in Image Upload of Invoice Logo", "");
                            }
                            if (ModelState.IsValid)
                            {
                                parking.IsDeleted = false;
                                parking.CreatedDate = DateTime.Now;
                                parking.LastModifiedDate = DateTime.Now;
                                parking.ModifiedUserId = sessionUser.UserId;
                                parking.CreatedUserId = sessionUser.UserId; // Id of Current loged in user
                                parking.Logo = logo;
                                parking.Invoicelogo = inlogo;
                                await _unitOfWork.Repository.Add(parking);
                                await _unitOfWork.Commit();
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", "Add Successed", "");
                                if (sessionUser.ParkingId == null || sessionUser.ParkingId == 0)    // Admin Account
                                {
                                    // Create Company Account to this new Parking
                                    var currentParking = await _unitOfWork.Repository.where(p => p.CreatedDate == parking.CreatedDate && p.ReferanceKey == parking.ReferanceKey).FirstOrDefaultAsync();
                                    var UserType = await _UserTypeunitOfWork.Repository.where(u=>u.NameEn == "CompanyAdmin".Trim()).FirstOrDefaultAsync();
                                    var FormPages =await _FormPagesunitOfWork.Repository.where(f=>f.IsDeleted == false && f.IsEnable == true).ToListAsync();
                                    
                                    var User = new AdminUsers {
                                        CreatedDate = DateTime.Now,
                                        CreatedUserId = sessionUser.UserId,
                                        ModifiedUserId = 0,
                                        LastModifiedDate = DateTime.Now,
                                        IsDeleted = parking.IsDeleted,
                                        IsEnable = parking.IsEnable,
                                        Fullname = parking.NameEn,
                                        Email = parking.CompanyEmail,
                                        Mobile = parking.ContactPhone,
                                        Username = parking.ContactEmail,
                                        Password = HashingHelperMD5.CreateMD5(parking.CompanyPassword),// will be Hashed
                                        Image = "/images/userlogo.png",
                                        ParkingId = currentParking.Id, // Set From Session logged in User
                                        UserTypeId = UserType.Id,
                                        PrivilageGroupId = null
                                    };
                                    var Privilage = new List<UserPrivilage>();
                                    foreach (var item in FormPages)
                                    {
                                        Privilage.Add(new UserPrivilage {
                                            Add = true,
                                            Edit = true,
                                            View = true,
                                            Search = true,
                                            Delete = true,
                                            Print = true,
                                            IsEnable = parking.IsEnable,
                                            IsDeleted = parking.IsDeleted,
                                            CreatedDate = parking.CreatedDate,
                                            LastModifiedDate = DateTime.Now,
                                            CreatedUserId = sessionUser.UserId,
                                            ModifiedUserId = 0,                                            
                                            FormPagesId = item.Id
                                        });
                                    }
                                    
                                    await _userManagmentRepos.CreateCompanyAccount(User, Privilage,sessionUser.UserId);                                    
                                }
                                return RedirectToAction(nameof(Index));
                            }
                            else
                            {
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", "Model State is InValid", "");
                            }
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", "User is Authorized on this Action", "");
                            return View(parking);
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
            catch(Exception ex)
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
                        var parking = await _unitOfWork.Repository.GetById_FindAsync(id);
                        parking.LastModifiedDate = DateTime.Now;
                        parking.ModifiedUserId = sessionUser.UserId;
                        parking.IsEnable = false;
                        parking.IsDeleted = true;
                        _unitOfWork.Repository.Update(parking);
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
            catch(Exception ex)
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
                            var worksheet = workbook.Worksheets.Add("Parkings");
                            var currentRow = 1;
                            worksheet.Cell(currentRow, 1).Value = "Id";
                            worksheet.Cell(currentRow, 2).Value = "CreatedUserId";
                            worksheet.Cell(currentRow, 3).Value = "ModifiedUserId";
                            worksheet.Cell(currentRow, 4).Value = "CreatedDate";
                            worksheet.Cell(currentRow, 5).Value = "LastModifiedDate";
                            worksheet.Cell(currentRow, 6).Value = "IsEnable";
                            worksheet.Cell(currentRow, 7).Value = "IsDeleted";
                            worksheet.Cell(currentRow, 8).Value = "NameAr";
                            worksheet.Cell(currentRow, 9).Value = "NameEn";
                            worksheet.Cell(currentRow, 10).Value = "ReferanceKey";
                            worksheet.Cell(currentRow, 11).Value = "ContactName";
                            worksheet.Cell(currentRow, 12).Value = "ContactEmail";
                            worksheet.Cell(currentRow, 13).Value = "ContactPhone";
                            worksheet.Cell(currentRow, 14).Value = "CompanyFkId";
                            worksheet.Cell(currentRow, 15).Value = "AddressEn";
                            worksheet.Cell(currentRow, 16).Value = "AddressAr";
                            worksheet.Cell(currentRow, 17).Value = "Description";
                            worksheet.Cell(currentRow, 18).Value = "Logo";
                            worksheet.Cell(currentRow, 19).Value = "Invoicelogo";
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
                                worksheet.Cell(currentRow, 8).Value = item.NameAr;
                                worksheet.Cell(currentRow, 9).Value = item.NameEn;
                                worksheet.Cell(currentRow, 10).Value = item.ReferanceKey;
                                worksheet.Cell(currentRow, 11).Value = item.ContactName;
                                worksheet.Cell(currentRow, 12).Value = item.ContactEmail;
                                worksheet.Cell(currentRow, 13).Value = item.ContactPhone;
                                worksheet.Cell(currentRow, 15).Value = item.AddressEn;
                                worksheet.Cell(currentRow, 16).Value = item.AddressAr;
                                worksheet.Cell(currentRow, 17).Value = item.Description;
                                worksheet.Cell(currentRow, 18).Value = item.Logo;
                                worksheet.Cell(currentRow, 19).Value = item.Invoicelogo;
                            }
                            using (var stream = new MemoryStream())
                            {
                                workbook.SaveAs(stream);
                                var content = stream.ToArray();
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Excel", "Success to Download Excel file", "");
                                return File(content, "application/vnd.openxmlformats-officdocument.spreadsheetml.sheet", $"ParkingList{DateTime.Now.Date.Hour}.xlsx");
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
            catch(Exception ex)
            {
                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Delete", ex.Message, "");
                return Redirect("/Home/ServerSideError500");
            }  
        }
        private bool ParkingExists(int id)
        {
            return _unitOfWork.Repository.Get_Any(id);
        }      
    }
}
