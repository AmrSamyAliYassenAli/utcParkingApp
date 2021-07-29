using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UTCAPPCMS.DAL.Repository.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
       // List<T> AllInclude(string expression);
        Task Add(T entity);
        Task Add(IEnumerable<T> entities);
        void Update(T entity);
        void Update(IEnumerable<T> entities);
      //  Task UpdateAsync(T entity);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
        Task<IList<T>> AllAsync();
        List<T> All();
        Task<T> GetById(int Id);
        Task<T> GetById_FirstOrDefAsync(int Id);
        Task<T> GetById_FindAsync(int Id);
        bool Get_Any(int Id);
        IQueryable<T> where(Expression<Func<T, bool>> expression);
    }
}
