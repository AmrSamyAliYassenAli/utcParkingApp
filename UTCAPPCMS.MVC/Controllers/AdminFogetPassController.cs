using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.Repository.Interfaces;
using UTCAPPCMS.MVC.Helpers;
using UTCAPPCMS.MVC.Models;

namespace UTCAPPCMS.MVC.Controllers
{
    public class AdminFogetPassController : Controller
    {
        private readonly IUnitOfWork<ForgetPasswordAdminUser> unitOfWork;
        private readonly IUnitOfWork<AdminUsers> AdminUsersIUnitOfWork;
        private readonly IDataProtector _protector;

        public AdminFogetPassController(IUnitOfWork<ForgetPasswordAdminUser> PasswordunitOfWork, IUnitOfWork<AdminUsers> AdminUsersIUnitOfWork, IDataProtectionProvider provider)
        {
            this.unitOfWork = PasswordunitOfWork;
            this.AdminUsersIUnitOfWork = AdminUsersIUnitOfWork;
            this._protector = provider.CreateProtector("adadasas"); ;
        }

        

        

        public ActionResult GetUser()
        {
            return  View();
        }
        [HttpPost]
        public async Task<IActionResult> GetUser(string forgit) 
        {
            ForgetPasswordAdminUser adminUser = new ForgetPasswordAdminUser();
            var User = await AdminUsersIUnitOfWork.Repository.where(u=>(u.Email==forgit)&&(u.IsEnable==true)&&(u.IsDeleted==false)).FirstOrDefaultAsync();
            if (User!=null)
            {
                adminUser.AdminUsersId = User.Id;
                adminUser.Email = forgit;
                adminUser.CreatedDate = DateTime.Now;
                adminUser.LastModifiedDate = DateTime.Now;
                adminUser.Body = "";
                adminUser.IsSent = true;
                adminUser.IsHold = false;
                string EncID = _protector.Protect(adminUser.AdminUsersId.ToString());
                string EncDate = _protector.Protect(adminUser.CreatedDate.Value.AddDays(1).ToString());
                string Link = "http://localhost:64645/AdminFogetPass/RestPassword/" + EncID + "?expDate=" + EncDate;
                adminUser.Body = Link;
                await unitOfWork.Repository.Add(adminUser);
                await AdminUsersIUnitOfWork.Commit();
                //return RedirectToAction("RestPassword", "AdminFogetPass",new {id= EncID, expDate= EncDate });
                return RedirectToAction("Login", "Register");
            }
            else
            {
                ForgitPass model = new ForgitPass{ NotFound="Please enter a valid Email" };
                return View(model);
            }
            
            

        }


        public ActionResult RestPassword(string id,string expDate)
        {
            
            var ExpDate = DateTime.Parse(_protector.Unprotect(expDate));
            var model = new ResetAdminPassword();
            if (ExpDate >= DateTime.Now)
            {
                model.Id = id;
                
                return View(model);
            }
            return View("ExpiredLink");

        }

        [HttpPost]
        [ActionName("RestPassword")]
        public async Task<IActionResult> RestPasswordConfirm(string id, string password)
        {
            var ID = int.Parse(_protector.Unprotect(id));
            var User = await AdminUsersIUnitOfWork.Repository.GetById(ID);
            User.Password = HashingHelperMD5.CreateMD5(password);
            User.LastModifiedDate = DateTime.Now;
            AdminUsersIUnitOfWork.Repository.Update(User);
            await AdminUsersIUnitOfWork.Commit();
            return RedirectToAction("Login", "Register");
        }
    }
}
