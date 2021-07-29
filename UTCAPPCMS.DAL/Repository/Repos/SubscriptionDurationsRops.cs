using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.DBContext;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.Repository.Interfaces;

namespace UTCAPPCMS.DAL.Repository.Repos
{
    public class SubscriptionDurationsRops : ISubscriptionDurationsRops
    {
        private readonly UTCAPPCMS_DBContext context;

        public SubscriptionDurationsRops(UTCAPPCMS_DBContext _Context)
        {
            context = _Context;
        }
        public List<SubscriptionDurations> GetALLSubDu()
        {
           return context.SubscriptionDurations.Include(x => x.Subscription).ToList();
            //return context.SubscriptionDurations.Include(x => x.Subscription).Where(x => x.IsDeleted == false && x.IsEnable == true).ToList();
        }
        public async Task<List<SubscriptionDurations>> EditthisId(int id)
        {
            return await context.SubscriptionDurations.Include(x => x.Subscription).Where(x => x.IsEnable == true && x.IsDeleted == false && x.SubscriptionId == id).ToListAsync();
        }
    }
}
//.Where(x => x.IsDeleted == false && x.IsEnable == true)