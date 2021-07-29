using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.DBContext;
using UTCAPPCMS.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace UTCAPPCMS.DAL.Repository.Repos
{
    public class UnitOdWork<T> : IUnitOfWork<T> where T : class, IEntity
    {
         private readonly UTCAPPCMS_DBContext context;
         public IRepository<T> Repository { get; }
         public UnitOdWork(UTCAPPCMS_DBContext context)
         {
           // context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            this.context = context;
             Repository = new Repository<T>(context);
         }
        public async Task Commit() 
        {            
            try
            {
              var x=  await context.SaveChangesAsync();
                string b = "";
            }
          catch(Exception ex)
            {
                string a = "";
            }

        }

         public async void Dispose() => await context.DisposeAsync();
    }
}
