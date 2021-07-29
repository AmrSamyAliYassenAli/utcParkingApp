using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UTCAPPCMS.DAL.Repository.Interfaces
{
    public interface IUnitOfWork<T> : IDisposable where T : class, IEntity
    {
        IRepository<T> Repository { get; }
        Task Commit();
    }
}
