using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.ViewModel;

namespace UTCAPPCMS.DAL.Repository.Interfaces
{
    public interface IdefaultRepository
    {
        Task<AdminUsers> GetLogedInData();
        Task<PrivilageViewModel> GetPrivilage(int? UserId, string FormKey);
        Task<ParkingLocations> GetParkingLocationById(int id);
    }
}
