using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
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
{
    public class ParkingImagesController : Controller
    {
        private readonly IUnitOfWork<ParkingImages> unitOfWork;
        private readonly string ImgPath = "/Files/ParkingImages/";
        private readonly string FormKey = "ParkingImages";
        private readonly IdefaultRepository _idefaultRepository;

        private readonly ISystemLogger _Logging;
        public ParkingImagesController(IUnitOfWork<ParkingImages> unitOfWork, IdefaultRepository _idefaultRepository, ISystemLogger _Logging)
        {
            this.unitOfWork = unitOfWork;
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
                        var parkingImgList = await unitOfWork.Repository.where(x => x.IsDeleted == false && x.IsEnable == true&&x.ParkingId== sessionUser.ParkingId).ToListAsync();
                        if (parkingImgList != null)
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "View", "Success Get index View", "");
                            return View(parkingImgList);
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
                            var parkingImg = await unitOfWork.Repository.GetById_FirstOrDefAsync(id.Value);
                            if (parkingImg == null)
                            {
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit", "Data NotFound in Table", "");
                                return Redirect("/Home/ClientSideError404");
                            }

                            ParkingImageModel model = new ParkingImageModel { Parking_Images = parkingImg };
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit", "Success Get Update View", "");
                            return View(model);
                        }
                        else
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit", "Not Authorized to View This Page", "");
                            return Redirect("/Home/ClientSideError404");
                        }

                    }
                    else // for add
                    {
                        if (Priv.User_Add == true)
                        {
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
        [AllowAnonymous]
        public async Task<IActionResult> Create(ParkingImageModel parking)
        {
            var sessionUser = JsonConvert.DeserializeObject<CurrentLoginUser>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserImg = sessionUser.UserImg;
            ViewBag.UserName = sessionUser.UserName;
            var Priv = await _idefaultRepository.GetPrivilage(sessionUser.UserId, FormKey.Trim());
            try
            {
                if (Priv != null)
                {
                    if (parking.Parking_Images.Id == 0) // for add
                    {
                        if (Priv.User_Add == true)
                        {
                            string FilePath = null;


                            if (parking.File != null)
                            {
                                if (parking.File.Length > 0)
                                {
                                    //Getting FileName
                                    var fileName = Path.GetFileName(parking.File.FileName);

                                    //Assigning Unique Filename (Guid)
                                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                                    //Getting file Extension
                                    var fileExtension = Path.GetExtension(fileName);

                                    // concatenating  FileName + FileExtension
                                    var newFileName = String.Concat(myUniqueFileName, fileExtension);

                                    // Combines two strings into a path.
                                    var filepath =
                                    new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "ParkingImages")).Root + $@"\{newFileName}";
                                    FilePath = ImgPath + newFileName;
                                    using (FileStream fs = System.IO.File.Create(filepath))
                                    {
                                        parking.File.CopyTo(fs);
                                        fs.Flush();
                                    }
                                }
                            }

                            if (ModelState.IsValid)
                            {
                                try
                                {
                                    var ParImg = new ParkingImages
                                    {
                                        FromDate = parking.Parking_Images.FromDate,
                                        ToDate = parking.Parking_Images.ToDate,
                                        IsEnable = parking.Parking_Images.IsEnable,
                                        TitleAr = parking.Parking_Images.TitleAr,
                                        TitleEn = parking.Parking_Images.TitleEn,
                                        Path = FilePath,
                                        CreatedDate = DateTime.Now,
                                        LastModifiedDate = DateTime.Now,
                                        IsDeleted = false,
                                        CreatedUserId = sessionUser.UserId,
                                        ModifiedUserId = 0,
                                    };
                                    await unitOfWork.Repository.Add(ParImg);
                                    await unitOfWork.Commit();
                                    await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", "Add Successed", "");

                                }
                                catch (Exception ex)
                                {

                                    await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add", ex.Message, "");
                                    return Redirect("/Home/ServerSideError500");
                                }

                            }
                            else
                            {
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", "Model State is InValid", "");
                            }
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", "Add Successed", "");
                            return Redirect("/ParkingImages/Index");
                        }
                        else
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Add PostMethod", "Not Authorized User to do this action", "");
                            return Redirect("/Home/ClientSideError404");
                        }

                    }
                    else
                    {
                        if (Priv.User_Edit == true)
                        {
                            if (parking.File != null)
                            {
                                string FilePath = null;
                                if (parking.File.Length > 0)
                                {
                                    //Getting FileName
                                    var fileName = Path.GetFileName(parking.File.FileName);

                                    //Assigning Unique Filename (Guid)
                                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                                    //Getting file Extension
                                    var fileExtension = Path.GetExtension(fileName);

                                    // concatenating  FileName + FileExtension
                                    var newFileName = String.Concat(myUniqueFileName, fileExtension);

                                    //var OldFilePath = (await unitOfWork.Repository.GetById_FirstOrDefAsync(parking.Parking_Images.Id)).Path;
                                    //string FUllOldFilePath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")).Root;
                                    //System.IO.File.Delete(FUllOldFilePath);


                                    // Combines two strings into a path.
                                    var filepath =
                                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "ParkingImages")).Root + $@"\{newFileName}";
                                    FilePath = ImgPath + newFileName;
                                    using (FileStream fs = System.IO.File.Create(filepath))
                                    {
                                        parking.File.CopyTo(fs);
                                        fs.Flush();
                                    }

                                    parking.Parking_Images.Path = FilePath;
                                }
                            }

                            unitOfWork.Repository.Update(parking.Parking_Images);
                            await unitOfWork.Commit();
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", "Update Successed", "");
                            return Redirect("/ParkingImages/Index");
                        }
                        else
                        {
                            await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Edit PostMethod", "User is Authorized on this Action", "");
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
                        var parkingImage = await unitOfWork.Repository.GetById_FirstOrDefAsync(id);
                        parkingImage.LastModifiedDate = DateTime.Now;

                        parkingImage.IsEnable = false;
                        parkingImage.IsDeleted = true;

                        unitOfWork.Repository.Update(parkingImage);
                        await unitOfWork.Commit();
                        await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Delete", $"Success Deleting ID {id}", "");
                        return Redirect("/ParkingImages/Index");
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
                        var parkings = unitOfWork.Repository.All();
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("PromotionsList");
                            var currentRow = 1;
                            worksheet.Cell(currentRow, 1).Value = "Id";
                            worksheet.Cell(currentRow, 2).Value = "CreatedUserId";
                            worksheet.Cell(currentRow, 3).Value = "ModifiedUserId";
                            worksheet.Cell(currentRow, 4).Value = "CreatedDate";
                            worksheet.Cell(currentRow, 5).Value = "LastModifiedDate";
                            worksheet.Cell(currentRow, 6).Value = "IsEnable";
                            worksheet.Cell(currentRow, 7).Value = "IsDeleted";
                            worksheet.Cell(currentRow, 8).Value = "English Title";
                            worksheet.Cell(currentRow, 9).Value = "Arabic Title";
                            worksheet.Cell(currentRow, 10).Value = "FromDate";
                            worksheet.Cell(currentRow, 11).Value = "ToDate";
                            worksheet.Cell(currentRow, 12).Value = "Path";
                            worksheet.Cell(currentRow, 13).Value = "Order Number";
                            worksheet.Cell(currentRow, 14).Value = "IsMain";
                            worksheet.Cell(currentRow, 15).Value = "ParkingId";
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
                                worksheet.Cell(currentRow, 8).Value = item.TitleEn;
                                worksheet.Cell(currentRow, 9).Value = item.TitleAr;
                                worksheet.Cell(currentRow, 10).Value = item.FromDate;
                                worksheet.Cell(currentRow, 11).Value = item.ToDate;
                                worksheet.Cell(currentRow, 12).Value = item.Path;
                                worksheet.Cell(currentRow, 13).Value = item.OrderNo;
                                worksheet.Cell(currentRow, 14).Value = item.IsMain;
                                worksheet.Cell(currentRow, 15).Value = item.ParkingId;
                            }
                            using (var stream = new MemoryStream())
                            {
                                workbook.SaveAs(stream);
                                var content = stream.ToArray();
                                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "Excel", "Success to Download Excel file", "");
                                return File(content, "application/vnd.openxmlformats-officdocument.spreadsheetml.sheet", $"PromotionsList{DateTime.Now.Date.Hour}.xlsx");
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