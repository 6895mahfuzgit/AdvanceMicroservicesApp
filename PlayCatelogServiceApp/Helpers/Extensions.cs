using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using PlayCatelogServiceApp.Entities;
using PlayCatelogServiceApp.Repositories;
using PlayCatelogServiceApp.Settings;
using System;

namespace PlayCatelogServiceApp.Helpers
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
        }

        public static Item AsCreateModel(this CreateItemDto item)
        {
            return new Item()
            {
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                CreatedDate = DateTime.UtcNow
            };
        }

        public static Item AsUpdateModel(this UpdateItemDto item, Guid id)
        {
            return new Item()
            {
                Id = id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                CreatedDate = DateTime.UtcNow
            };
        }


        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            services.AddSingleton(serviceProvider =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                var mongoSettings = configuration.GetSection(nameof(MongoDBSettings)).Get<MongoDBSettings>();
                var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                var mongoClient = new MongoClient(mongoSettings.ConnectionString);
                return mongoClient.GetDatabase(serviceSettings.ServiceName);
            });

            return services;
        }


        public static IServiceCollection AddMongoCollection<T>(this IServiceCollection services, string collectionName) where T : IEntity
        {
            services.AddSingleton<IRepository<T>>(serviceProvider =>
            {

                var database = serviceProvider.GetRequiredService<IMongoDatabase>();
                return new MongoRepository<T>(database, collectionName);
            });

            return services;
        }

    }
}
