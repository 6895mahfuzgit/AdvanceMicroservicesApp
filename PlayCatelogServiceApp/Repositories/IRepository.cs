using PlayCatelogServiceApp.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayCatelogServiceApp.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task CreateSync(T entity);
        Task<IReadOnlyCollection<T>> GetAllSync();
        Task<T> GetSync(Guid id);
        Task RemoveSync(T entity);
        Task UpdateSync(T entity);
    }
}