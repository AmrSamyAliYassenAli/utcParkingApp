using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Repository.Interfaces;
using UTCAPPCMS.MVC.Models;

namespace UTCAPPCMS.MVC.Controllers
{
    // testr
    public class HomeController : Controller
    {
        private readonly ISystemLogger _Logging;
        string FormKey = "Home";
        public HomeController(ISystemLogger _Logging)
        {
            this._Logging = _Logging;
        }

        public async Task< IActionResult> Index()
        {
            var sessionUser = JsonConvert.DeserializeObject<CurrentLoginUser>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserImg = sessionUser.UserImg;
            ViewBag.UserName = sessionUser.UserName;
            try
            {
                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "View", "Success Get index View", "");
                return View();
            }
            catch (Exception ex)
            {
                await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "View", ex.Message, "");
                return Redirect("/Home/ServerSideError500");
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ClientSideError404()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ServerSideError500()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
