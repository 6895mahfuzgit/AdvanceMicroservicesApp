using MongoDB.Driver;
using PlayCatelogServiceApp.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayCatelogServiceApp.Repositories
{
    public class ItemRepository
    {
        private const string collectionName = "items";
        private readonly IMongoCollection<Item> _dbCollection;
        private readonly FilterDefinitionBuilder<Item> _filterDefinitionBuilder = Builders<Item>.Filter;

        public ItemRepository()
        {
            var mongoClient = new MongoClient("mongo://localhost:27017");
            var database = mongoClient.GetDatabase("Catalog");
            _dbCollection = database.GetCollection<Item>(collectionName);
        }


        public async Task<IReadOnlyCollection<Item>> GetAllSync()
        {
            try
            {
                return await _dbCollection.Find(_filterDefinitionBuilder.Empty).ToListAsync();
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Item> GetSync(Guid id)
        {
            try
            {
                var filter = _filterDefinitionBuilder.Eq(entity => entity.Id, id);
                return await _dbCollection.Find(filter).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task CreateSync(Item item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException(nameof(item));
                }
                await _dbCollection.InsertOneAsync(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task UpdateSync(Item item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException(nameof(item));
                }
                var filter = _filterDefinitionBuilder.Eq(entity => entity.Id, item.Id);
                await _dbCollection.ReplaceOneAsync(filter, item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }


        public async Task RemoveSync(Item item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException(nameof(item));
                }

                var filter = _filterDefinitionBuilder.Eq(entity => entity.Id, item.Id);
                await _dbCollection.DeleteOneAsync(filter);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

    }
}
