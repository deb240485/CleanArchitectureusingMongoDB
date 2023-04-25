using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Application;
using CleanArchitecture.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CleanArchitecture.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterMongoDependencies(this IServiceCollection services)
        {
            //BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            //
            //
            //this didn't helped so changed.

            //BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            //BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;

            //BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            //services.Configure<DatabaseConfiguration>(configuration.GetSection("DatabaseConfiguration"));
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerReposiotry, CustomerRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
