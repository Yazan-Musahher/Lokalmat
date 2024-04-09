using System.Reflection;
using Administration.Application.Interfaces.Presistence;
using Administration.Infrastructure.Persistence.Interceptors;
using Administration.Infrastructure.Persistence.Repositories;

using LokalProdusert.Shared.MongoDB;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

using Administration.Application.Interfaces.Events;
using Administration.Infrastructure.Presistence.Repositories;
using Users.Infrastructure.Persistence.Interceptors;




namespace Administration.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddAdminstrationInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {

        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
            
         // Bind the MongoDB settings from the configuration
        var mongoDBSection = configuration.GetSection("MongoDB");
        services.Configure<MongoDBSettings>(mongoDBSection.Bind);

        // Register the MongoDB client and database
         services.AddSingleton<IMongoClient>(serviceProvider =>
        {
            var settings = serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>().Value;
            return new MongoClient(settings.ConnectionURI);
        });

        services.AddSingleton<IMongoDatabase>(serviceProvider =>
        {
            var settings = serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>().Value;
            var client = serviceProvider.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.DatabaseName);
        });
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDomainEventPublisher, DomainEventPublisher>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<PuplishDomainEventsInterceptor>(sp =>
        {
            var database = sp.GetRequiredService<IMongoDatabase>();
            var unitOfWork = sp.GetRequiredService<IUnitOfWork>();
            return new PuplishDomainEventsInterceptor(database, unitOfWork);
        });

        
        services.AddScoped<IIntegrationEventsPublisher, IntegrationEventsPublisher>();



        return services;
    }
}
