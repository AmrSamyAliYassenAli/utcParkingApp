using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using UTCAPPCMS.MVC.Helpers;
using UTCAPPCMS.DAL.DBContext;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using UTCAPPCMS.MVC.Models;
using Newtonsoft.Json;

namespace UTCAPPCMS.MVC.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUnitOfWork<AdminUsers> _unitOfWork;
        private readonly ISystemLogger _Logging;
        string FormKey = "Register";
        public RegisterController(IUnitOfWork<AdminUsers> _unitOfWork, ISystemLogger _Logging)
        {
            this._unitOfWork = _unitOfWork;
            this._Logging = _Logging;
        }
        public async Task<IActionResult> Login()
        {
            try
            {
                await _Logging.TraceLogsAsync(0, FormKey, "Login", "User is not Loged in yet Success Get index View", "");
                return View();
            }
            catch (Exception ex)
            {
                await _Logging.TraceLogsAsync(0, FormKey, "Login", ex.Message, "");
                return Redirect("/Home/ServerSideError500");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {            
            if (email != null && password != null  )
            {
                string password_hashed = HashingHelperMD5.CreateMD5(password);
                var user = _unitOfWork.Repository.where(x => x.Email == email && x.Password == password_hashed && x.IsDeleted == false && x.IsEnable == true).FirstOrDefault();
                if (user != null)
                {
                    var loginUser = new CurrentLoginUser {
                        UserId = user.Id,
                        ParkingId = user.ParkingId,
                        UserTypeId = user.UserTypeId.Value,
                        PrivilageGroupId = user.PrivilageGroupId,
                        UserName = user.Username,
                        UserImg = user.Image
                    };
                    HttpContext.Session.SetString("SessionUser",JsonConvert.SerializeObject(loginUser));
                    await _Logging.TraceLogsAsync(0, FormKey, "Login", $"User: {email} Login Success", "");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    await _Logging.TraceLogsAsync(0, FormKey, "Login", $"User: {email} Invalid Login Attempt , Error in Entering UserName or Password ", "");
                    ViewBag.LoginError= "Invalid Login Attempt";
                    return View();
                }               
            }
            else
            {
                await _Logging.TraceLogsAsync(0, FormKey, "Login", $"User: {email} Invalid Login Attempt , Error in Entering UserName or Password ", "");
                ViewBag.LoginError = "Invalid Login Attempt";
                return View();
            }
        }

        [Route("logout")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var sessionUser = JsonConvert.DeserializeObject<CurrentLoginUser>(HttpContext.Session.GetString("SessionUser"));
            await _Logging.TraceLogsAsync(0, FormKey, "logout", $"User: {sessionUser.UserName} Logged out From System", "");
            HttpContext.Session.Remove("SessionUser");
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> Profile()
        {
            var sessionUser = JsonConvert.DeserializeObject<CurrentLoginUser>(HttpContext.Session.GetString("SessionUser"));
            await _Logging.TraceLogsAsync(0, FormKey, "Profile", "Success View Prifile Page of User", "");
            var User =await _unitOfWork.Repository.GetById_FindAsync(sessionUser.UserId);
            ViewBag.UserImg = User.Image;
            return View(User);
        }
        public IActionResult Forgot_Password()
        {
            return View();
        }

    }
}
