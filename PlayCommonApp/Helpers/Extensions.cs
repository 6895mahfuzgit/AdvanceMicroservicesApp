using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using PlayCommonApp.Entities;
using PlayCommonApp.Repositories;
using PlayCommonApp.Settings;
using System;

namespace PlayCommonApp.Helpers
{
    public static class Extensions
    {
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
