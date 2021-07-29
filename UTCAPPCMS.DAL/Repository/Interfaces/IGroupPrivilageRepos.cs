using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;

namespace UTCAPPCMS.DAL.Repository.Interfaces
{
    public interface IGroupPrivilageRepos
    {
        Task<List<GroupPrivilage>> GetALLPrivilage();
        Task<List<FormPages>> GetAllFormKeys();
        Task<List<GroupPrivilage>> EditthisId(int? id);
    }
}
