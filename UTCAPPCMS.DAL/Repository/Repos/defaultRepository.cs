using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.DBContext;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.Repository.Interfaces;
using UTCAPPCMS.DAL.ViewModel;
using Microsoft.EntityFrameworkCore;



namespace UTCAPPCMS.DAL.Repository.Repos
{
    public class defaultRepository : IdefaultRepository
    {
        private readonly UTCAPPCMS_DBContext _db;

        public defaultRepository(UTCAPPCMS_DBContext _db)
        {
            this._db = _db;
        }
        /// <summary>
        /// GetLogedInData (InProcess)
        /// </summary>
        /// <returns>User Logged in Data</returns>
        public async Task<AdminUsers> GetLogedInData()
        {
           // int UserID = 2; 
            return await _db.AdminUsers.Where(x => x.Id == 2).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task< PrivilageViewModel> GetPrivilage(int? UserId, string FormKey)
        {
            try
            {
                var AdminUser = _db.AdminUsers.FirstOrDefault(x => x.Id == UserId.Value);
                var page = _db.FormPages.Where(x => x.FormKey == FormKey).FirstOrDefault();
                
                if ((AdminUser.PrivilageGroupId == 0 || AdminUser.PrivilageGroupId==null) && page != null)
                {
                    var CustomPrivilage = await _db.UserPrivilage.FirstOrDefaultAsync(x => x.AdminUsersId.Value == UserId.Value&&x.FormPagesId.Value==page.Id);
            
                    return new PrivilageViewModel
                    {
                        User_Add = CustomPrivilage.Add,
                        User_View = CustomPrivilage.View,
                        User_Edit = CustomPrivilage.Edit,
                        User_Delete = CustomPrivilage.Delete,
                        User_Print = CustomPrivilage.Print,
                        User_Search = CustomPrivilage.Search
                    };
                }
                else if (AdminUser.PrivilageGroupId > 0 && page != null)
                {
                   var GroupPrivilage =await _db.GroupPrivilage.FirstOrDefaultAsync(x => x.Id == AdminUser.PrivilageGroupId.Value&&x.FormPagesId.Value== page.Id);
                   if(GroupPrivilage != null)
                    {
                        return new PrivilageViewModel
                        {
                            User_Add = GroupPrivilage.Add,
                            User_View = GroupPrivilage.View,
                            User_Edit = GroupPrivilage.Edit,
                            User_Delete = GroupPrivilage.Delete,
                            User_Print = GroupPrivilage.Print,
                            User_Search = GroupPrivilage.Search
                        };
                    }
                    else { return null; }
                }
                else
                {
                    return null;
                }
            }
            catch(Exception)
            {
                return null;
            }
        }

        public async Task<ParkingLocations> GetParkingLocationById(int id)
        {
            try
            {
               var location=await _db.ParkingLocations.AsNoTracking().Where(x => x.Id == id).Include(x => x.Parking).AsNoTracking().FirstOrDefaultAsync();
                return location;
            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
