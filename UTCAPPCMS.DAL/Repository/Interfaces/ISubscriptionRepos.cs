using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;

namespace UTCAPPCMS.DAL.Repository.Interfaces
{
    public interface ISubscriptionRepos
    {
        List<Subscription> GetALLSub(int? parkingid);
        Task<List<SubscriptionDurations>> EditthisId(int id);
    }
}
