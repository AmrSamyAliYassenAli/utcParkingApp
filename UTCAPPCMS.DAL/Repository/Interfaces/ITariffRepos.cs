using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;

namespace UTCAPPCMS.DAL.Repository.Interfaces
{
    public interface ITariffRepos
    {
        Task<List<TableTariff>> GetAllTariff(int? parkingid);
    }
}
