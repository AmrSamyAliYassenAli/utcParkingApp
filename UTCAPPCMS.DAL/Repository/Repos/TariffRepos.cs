using System;
using System.Collections.Generic;
using System.Text;
using UTCAPPCMS.DAL.DBContext;
using UTCAPPCMS.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Repository.Interfaces;

namespace UTCAPPCMS.DAL.Repository.Repos
{
    public class TariffRepos : ITariffRepos
    {
        private UTCAPPCMS_DBContext _Context;
        public TariffRepos(UTCAPPCMS_DBContext _Context)
        {
            this._Context = _Context;
        }

        public async Task<List<TableTariff>> GetAllTariff(int? parkingid)
        {
            if(parkingid!=null)
            {
                return await _Context.TableTariff.Include(x => x.ParkingLocation).Where(x => x.IsEnable == true && x.IsDeleted == false && x.ParkingLocation.ParkingId == parkingid).ToListAsync();

            }else
            {
                return await _Context.TableTariff.Include(x => x.ParkingLocation).Where(x => x.IsEnable == true && x.IsDeleted == false).ToListAsync();

            }
        }
    }
}
