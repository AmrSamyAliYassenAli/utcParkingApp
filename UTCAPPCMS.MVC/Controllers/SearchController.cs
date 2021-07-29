using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.Repository.Interfaces;
using UTCAPPCMS.DAL.Repository.Repos;
using UTCAPPCMS.MVC.Models;

namespace UTCAPPCMS.MVC.Controllers
{
    public class SearchController : Controller
    {
        private readonly string FormKey = "Search";
        private readonly ISubscriptionRepos subscription;
        private readonly ISubscriptionDurationsRops subscriptionDurationsRops;
        private readonly ICustomerRepos customerRepos;

        private readonly IdefaultRepository _idefaultRepository;
        public UnitOdWork<SubscriptionDurations> SubscriptionDurations { get; }

        private readonly ISystemLogger _Logging;
        public SearchController(ISubscriptionRepos subscription, ISubscriptionDurationsRops subscriptionDurationsRops, ICustomerRepos customerRepos, IdefaultRepository _idefaultRepository, ISystemLogger _Logging)
        {
            this.subscription = subscription;
            this.subscriptionDurationsRops = subscriptionDurationsRops;
            this.customerRepos = customerRepos;
            this._idefaultRepository = _idefaultRepository;
            this._Logging = _Logging;
        }

        public async Task<ActionResult> Filter(List<Customer> cus)
        {
            var sessionUser = JsonConvert.DeserializeObject<CurrentLoginUser>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserImg = sessionUser.UserImg;
            ViewBag.UserName = sessionUser.UserName;
            var Priv = await _idefaultRepository.GetPrivilage(sessionUser.UserId, FormKey.Trim());
            try
            {
                if (Priv != null)
                {
                    if (Priv.User_Search == true)
                    {
                        FilterModel filterModel = new FilterModel();
                        if (cus.Count() != 0)
                        {
                            filterModel.Customers = cus.ToList();
                            filterModel.Phone = null;
                        }

                        ViewBag.SubscriptionType = new SelectList(subscription.GetALLSub(sessionUser.ParkingId), "Id", "Name");
                        await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "View", "Success Get index View", "");
                        return View("Filter", filterModel);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> FilterDone(FilterModel model)
        {
            var sessionUser = JsonConvert.DeserializeObject<CurrentLoginUser>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserImg = sessionUser.UserImg;
            ViewBag.UserName = sessionUser.UserName;
            var Priv = await _idefaultRepository.GetPrivilage(sessionUser.UserId, FormKey.Trim());
            try
            {
                if (Priv != null)
                {
                    if (Priv.User_Search == true)
                    {
                        List<Customer> cust = new List<Customer>();
                        ViewBag.SubscriptionType = new SelectList(subscription.GetALLSub(sessionUser.ParkingId), "Id", "Name");


                        if ((model.Phone != null) || ((model.FromExpireDate != null) && (model.ToExpireDate != null)) || (model.FromCreateDate != null && model.ToCreateDate != null) || ((model.SubscriptionType != -1) && (model.SubscriptionDurationID != -1)))
                        {
                            cust = customerRepos.GetALLCustomers().Where(c => (c.ParkingId==sessionUser.ParkingId)&&((c.Mobile == model.Phone) || ((c.ExpireDate >= model.FromExpireDate) && (c.ExpireDate <= model.ToExpireDate)) || (c.SubscriptionDurationId == model.SubscriptionDurationID) || ((c.CreatedDate >= model.FromCreateDate) && (c.CreatedDate <= model.ToCreateDate)))).Select(c => c).ToList<Customer>();
                        }

                        else
                        {
                            cust = customerRepos.GetALLCustomers().Where(x=>x.IsDeleted==false&&x.ParkingId==sessionUser.ParkingId).ToList();
                        }


                        FilterModel filter = new FilterModel();
                        filter.Customers = cust;
                        await _Logging.TraceLogsAsync(sessionUser.UserId, FormKey, "View", "Success Get index View", "");
                        return View("Filter", filter);
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

        public JsonResult GetDurationList(int SubscriptionType)
        {

            List<SubscriptionDurations> durations = subscriptionDurationsRops.GetALLSubDu().Where(x => x.SubscriptionId == SubscriptionType).ToList();

            return Json(new SelectList(durations, "Id", "DaysCount"));

        }

        //DaysCount


        [HttpPost]
        public void Enable(int cust)
        {
            var cus = customerRepos.FindCust(cust);
            if (cus.IsEnable)
            {
                cus.IsEnable = false;
            }
            else { cus.IsEnable = true; }
            customerRepos.Update(cus);
        }


        public async Task<ActionResult> Details(int id)
        {
            var sessionUser = JsonConvert.DeserializeObject<CurrentLoginUser>(HttpContext.Session.GetString("SessionUser"));
            ViewBag.UserImg = sessionUser.UserImg;
            ViewBag.UserName = sessionUser.UserName;
            var Priv = await _idefaultRepository.GetPrivilage(sessionUser.UserId, FormKey.Trim());

            if(Priv!=null)
            {
                if(Priv.User_View == true)
                {
                    var model = new CustomerInfo();
                    var cust = customerRepos.GetALLCustomers().Find(c => c.Id == id);
                    var CV = customerRepos.GetCustomerVehicles(id);
                    model.VehiclesList = CV;
                    model.customer = cust;
                    if (cust.SubscriptionDuration != null)
                    {
                        model.SubName = cust.SubscriptionDuration.Subscription.Name;
                    }

                    return View(model);
                }
                else
                {
                    return Redirect("/Home/ClientSideError404");
                }
            }
            else
            {
                return Redirect("/Home/ClientSideError404");
            }
        }
    }
}