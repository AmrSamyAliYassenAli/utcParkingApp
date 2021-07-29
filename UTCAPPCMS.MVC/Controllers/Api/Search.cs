using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.Repository.Interfaces;

namespace UTCAPPCMS.MVC.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class Search : ControllerBase
    {
        private readonly IUnitOfWork<Area> _AreaUnitOfWork;

        public Search(IUnitOfWork<Area> _AreaUnitOfWork)
        {
            this._AreaUnitOfWork = _AreaUnitOfWork;
        }
        [Produces("application/json")]
        [HttpGet("search2")]
        public async Task<IActionResult> AreasAutoSearch()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names = _AreaUnitOfWork.Repository.where(a => a.NameEn.Contains(term)).Select(a => new { a.NameEn } ).ToList();
                return Ok(names);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> AreaSearch()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names = _AreaUnitOfWork.Repository.where(a => a.NameEn.Contains(term)).Select(a=>a.NameEn).ToList();                
                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
