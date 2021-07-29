using System;
using System.Collections.Generic;
using System.Text;
using UTCAPPCMS.DAL.DBContext;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace UTCAPPCMS.DAL.Repository.Repos
{
    public class UserManagmentRepos : IUserManagmentRepos
    {
        private UTCAPPCMS_DBContext _Context;
        private readonly ISystemLogger _Logging;
        public UserManagmentRepos(UTCAPPCMS_DBContext _Context, ISystemLogger _Logging)
        {
            this._Context = _Context;
            this._Logging = _Logging;
        }

        public async Task<List<UserPrivilage>> GetALLPrivilage()
        {
            return await _Context.UserPrivilage.Include(x => x.FormPages).Include(x=>x.AdminUsers).Where(x => x.IsDeleted == false && x.IsEnable == true).ToListAsync();
        }

        public async Task<List<UserPrivilage>> EditthisId(int?id)
        {
            return await _Context.UserPrivilage.Include(x => x.FormPages).Include(x => x.AdminUsers).Where(x => x.IsDeleted == false && x.IsEnable == true && x.AdminUsersId == id).ToListAsync();
        }

        public async Task CreateCompanyAccount(AdminUsers users,List<UserPrivilage> userPrivilages,int sessionUserId)
        {
            try
            {
                _Context.AdminUsers.Add(users);
                await _Context.SaveChangesAsync();
                var currentAdminUser = await _Context.AdminUsers.Where(a => a.Email == users.Email && a.Username == users.Username).FirstOrDefaultAsync();
                foreach (var item in userPrivilages)
                {
                    item.AdminUsersId = currentAdminUser.Id;
                }
                _Context.UserPrivilage.AddRange(userPrivilages);
                await _Context.SaveChangesAsync();

                await _Logging.TraceLogsAsync(sessionUserId, "Parking", "Add PostMethod Create Company Account ", $"SUCCESS Create Company Account {users.Email} & {users.Password} For Parking Id : {users.ParkingId}", "");
            }
            catch(Exception ex)
            {
                await _Logging.TraceLogsAsync(sessionUserId, "Parking", "Add PostMethod Create Company Account ", $"Faild to Create Company Account : {ex.Message}", "");
            }
           
        }
        public async Task<AdminUsers> GetUserById(int? id)
        {
            return await _Context.AdminUsers.Where(x => x.IsDeleted == false  && x.Id == id).FirstOrDefaultAsync();
        }
    }
}
