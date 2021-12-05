using PlayCommonApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PlayCommonApp.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task CreateSync(T entity);
        Task<IReadOnlyCollection<T>> GetAllSync();
        Task<IReadOnlyCollection<T>> GetAllSync(Expression<Func<T,bool>> filter);
        Task<T> GetSync(Guid id);
        Task<T> GetSync(Expression<Func<T, bool>> filter);
        Task RemoveSync(T entity);
        Task UpdateSync(T entity);
    }
}