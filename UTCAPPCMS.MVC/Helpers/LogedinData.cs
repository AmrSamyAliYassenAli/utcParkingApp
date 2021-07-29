using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.Repository.Interfaces;

namespace UTCAPPCMS.MVC.Helpers
{
    public class LogedinData
    {
        private readonly IUnitOfWork<AdminUsers> _adminUsers;
        public LogedinData(IUnitOfWork<AdminUsers> _adminUsers)
        {
            this._adminUsers = _adminUsers;
        }
        public async Task<AdminUsers> GetLogedInData()
        {
            int UserID=1;
            return await _adminUsers.Repository.GetById_FirstOrDefAsync(UserID);
        }
    }
}
