using MongoDB.Driver;
using PlayCatelogServiceApp.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayCatelogServiceApp.Repositories
{
    public class MongoRepository<T> : IRepository<T> where T : IEntity
    {
        //private const string collectionName = "items";
        private readonly IMongoCollection<T> _dbCollection;
        private readonly FilterDefinitionBuilder<T> _filterDefinitionBuilder = Builders<T>.Filter;

        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            //var mongoClient = new MongoClient("mongodb://localhost:27017");
            //var database = mongoClient.GetDatabase("Catalog");
            _dbCollection = database.GetCollection<T>(collectionName);
        }


        public async Task<IReadOnlyCollection<T>> GetAllSync()
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

        public async Task<T> GetSync(Guid id)
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

        public async Task CreateSync(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                await _dbCollection.InsertOneAsync(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task UpdateSync(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                var filter = _filterDefinitionBuilder.Eq(entity => entity.Id, entity.Id);
                await _dbCollection.ReplaceOneAsync(filter, entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }


        public async Task RemoveSync(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                var filter = _filterDefinitionBuilder.Eq(entity => entity.Id, entity.Id);
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
