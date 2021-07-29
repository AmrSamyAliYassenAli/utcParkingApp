using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.DBContext;
using UTCAPPCMS.DAL.Repository.Interfaces;

namespace UTCAPPCMS.DAL.Repository.Repos
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        DbSet<T> Table { get; set; }

        //_context.Subscription.Include(x => x.Parking).Where(x=> x.IsDeleted == false && x.IsEnable == true).ToList();
       /* public List<T> AllInclude(Expression<Func<T, object>>[] includes)
        {
            return Table.Include(includes).ToList();
        }

        public T Get(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (Expression<Func<T, object>> include in includes)
                    query = query.Include(include);

            return ((DbSet<T>)query).Find(id);
        }*/
        public Repository(UTCAPPCMS_DBContext context)
        {
            this.Table = context.Set<T>();
        }
        // START GETS
        public async Task<IList<T>> AllAsync()
        {
            return await Table.AsNoTracking().ToListAsync();
        }
        public List<T> All()
        {
            return Table.ToList();
        }
        public async Task<T> GetById(int Id)
        {
            return await Table.SingleOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<T> GetById_FirstOrDefAsync(int Id)
        {
            return await Table.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<T> GetById_FindAsync(int Id)
        {
            return await Table.FindAsync(Id);
        }
        public bool Get_Any(int Id)
        {
            return Table.Any(x => x.Id == Id);
        }

        
        // END GETS
        // START SETS
        public async Task Add(T entity)
        {
            Table.AsNoTracking();
            await Table.AddAsync(entity);
        }
        public async Task Add(IEnumerable<T> entities) 
        {
            Table.AsNoTracking();
            await Table.AddRangeAsync(entities); 
        }
        // END SETS
        // START DELETE
        public void Delete(T entity) => Table.Remove(entity);

        public void Delete(IEnumerable<T> entities) => Table.RemoveRange(entities);
        // END DELETE
        // START UPDATE
        public void Update(T entity)
        {
            Table.AsNoTracking();           
            Table.Update(entity);
        }

        public void Update(IEnumerable<T> entities) => Table.UpdateRange(entities);
       /* public async Task UpdateAsync(T entity)
        {
            //Table.Entry(entity).State = EntityState.Modified; // exception when trying to change the state
           // await Table.SaveChangesAsync();
        }*/
        // END UPDATE
        // START CONDITIONS
        public IQueryable<T> where(Expression<Func<T, bool>> expression)
        {
            return Table.Where<T>(expression);
        }

        // END CONDITIONS

    }
}
