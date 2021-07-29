using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.DBContext;
using UTCAPPCMS.DAL.Models;
using UTCAPPCMS.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace UTCAPPCMS.DAL.Repository.Repos
{
    public class GroupPrivilageRepos : IGroupPrivilageRepos
    {
        private UTCAPPCMS_DBContext _Context;
        public GroupPrivilageRepos(UTCAPPCMS_DBContext _Context)
        {
            this._Context = _Context;
        }
        public async Task<List<GroupPrivilage>> GetALLPrivilage()
        {
            return await _Context.GroupPrivilage.Include(x => x.FormPages).Include(x => x.UserGroup).Include(x => x.Parking).Where(x => x.IsDeleted == false && x.IsEnable == true).ToListAsync();
        }
        public async Task<List<FormPages>> GetAllFormKeys()
        {
            return await _Context.FormPages.ToListAsync();
        }
        public async Task<List<GroupPrivilage>> EditthisId(int?id)
        {
            return await _Context.GroupPrivilage.Include(x => x.FormPages).Include(x => x.UserGroup).Include(x => x.Parking).Where(x=>x.IsDeleted == false && x.IsEnable == true && x.UserGroupId == id.Value).ToListAsync();
        }
        public async Task<List<UserGroup>> GetUserGroups()
        {            
            var x= _Context.GroupPrivilage.Include(x => x.FormPages).Include(x => x.UserGroup).Include(x => x.Parking).Where(x => x.IsDeleted == false && x.IsEnable == true).ToListAsync();
            return await _Context.UserGroup.Where(x=>x.IsDeleted == false && x.IsEnable == true).ToListAsync();
        }

        public void Modified(GroupPrivilage groupPrivilage)
        {
            //context.Entry(author).State = EntityState.Modified;

            _Context.Entry(groupPrivilage).State = EntityState.Modified;

        }
    }
}
