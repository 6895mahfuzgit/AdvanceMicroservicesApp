using PlayCatelogServiceApp.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayCatelogServiceApp.Repositories
{
    public interface IItemRepository
    {
        Task CreateSync(Item item);
        Task<IReadOnlyCollection<Item>> GetAllSync();
        Task<Item> GetSync(Guid id);
        Task RemoveSync(Item item);
        Task UpdateSync(Item item);
    }
}