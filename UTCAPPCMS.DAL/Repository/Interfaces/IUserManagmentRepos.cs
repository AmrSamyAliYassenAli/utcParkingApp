using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;

namespace UTCAPPCMS.DAL.Repository.Interfaces
{
    public interface IUserManagmentRepos
    {
        Task<List<UserPrivilage>> GetALLPrivilage();
        Task<List<UserPrivilage>> EditthisId(int? id);
        Task<AdminUsers> GetUserById(int? id);
        Task CreateCompanyAccount(AdminUsers users, List<UserPrivilage> userPrivilages, int sessionUserId);
    }
}
