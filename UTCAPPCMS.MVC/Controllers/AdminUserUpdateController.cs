using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
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
    public class AdminUserUpdateController : Controller
    {
        private readonly IUnitOfWork<AdminUsers> unitOfWork;
        private readonly IdefaultRepository _idefaultRepository;
        private readonly string ImgPath = "/Files/AminUsersImgs/";
        public AdminUserUpdateController(IUnitOfWork<AdminUsers> unitOfWork, IdefaultRepository _idefaultRepository)
        {
            this.unitOfWork = unitOfWork;
            this._idefaultRepository = _idefaultRepository;
        }


        public async Task<IActionResult> Create(int? id)
        {
            if (id != null) // for edit
            {
                var adminUsers = await unitOfWork.Repository.GetById_FirstOrDefAsync(id.Value);
                if (adminUsers == null)
                {
                    return NotFound();
                }
                else
                {
                    var model = new UpdateUserInfoModel() { Admin = adminUsers };
                    return View(model);
                }
            }

            else
            {
                return View();
            }
        }



        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(UpdateUserInfoModel update)
        {
            if (update.File != null)
            {
                string FilePath = null;
                if (update.File.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(update.File.FileName);

                    //Assigning Unique Filename (Guid)
                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);

                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(myUniqueFileName, fileExtension);

                    


                    // Combines two strings into a path.
                    var filepath =
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "AminUsersImgs")).Root + $@"\{newFileName}";
                    FilePath = ImgPath + newFileName;
                    using (FileStream fs = System.IO.File.Create(filepath))
                    {
                        update.File.CopyTo(fs);
                        fs.Flush();
                    }

                    update.Admin.Image= FilePath;
                }
            }
            update.Admin.LastModifiedDate = DateTime.Now;

            unitOfWork.Repository.Update(update.Admin);
            await unitOfWork.Commit();
            return View();
        }

        }
}