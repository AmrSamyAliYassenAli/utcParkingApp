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
    public class CustomerForgetPassController : Controller
    {
        
        
        private readonly IDataProtector _protector;
        private readonly IUnitOfWork<Customer> customerIUnitOfWork;

        public CustomerForgetPassController( IUnitOfWork<Customer> CustomerIUnitOfWork, IDataProtectionProvider provider)
        {
           
            this._protector = provider.CreateProtector("zxcvbnm"); ;
            customerIUnitOfWork = CustomerIUnitOfWork;
        }





        


        public ActionResult RestPassword(string id, string expDate)
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
            byte[] PasswordHash, PasswordSolt;
            createPasswordHash(password, out PasswordHash, out PasswordSolt);
            var ID = int.Parse(_protector.Unprotect(id));
            var customer = await customerIUnitOfWork.Repository.GetById(ID);
            customer.passwordSalt = PasswordSolt;
            customer.passwordHash = PasswordHash;
            customer.LastModifiedDate = DateTime.Now;
            customerIUnitOfWork.Repository.Update(customer);
            await customerIUnitOfWork.Commit();
            return View("SuccessfulOperation");
        }


        private void createPasswordHash(string password, out byte[] passwordHash, out byte[] PasswordSolt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                PasswordSolt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }
    }
}