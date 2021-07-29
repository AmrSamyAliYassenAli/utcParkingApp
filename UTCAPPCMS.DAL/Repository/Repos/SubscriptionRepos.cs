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
    public class SubscriptionRepos: ISubscriptionRepos
    {
        private UTCAPPCMS_DBContext _Context;

        public SubscriptionRepos(UTCAPPCMS_DBContext _Context)
        {
            this._Context = _Context;
        }

        public List<Subscription> GetALLSub(int? parkingid)
        {
            return _Context.Subscription.Include(x => x.Parking).Where(x => x.IsDeleted == false && x.IsEnable == true&&x.ParkingId==parkingid).ToList();
        }

        public async Task<List<SubscriptionDurations>> EditthisId(int id)
        {
            return await _Context.SubscriptionDurations.Include(x => x.Subscription).Where(x => x.IsDeleted == false && x.IsEnable == true && x.SubscriptionId == id).ToListAsync();
        }

    }
}
