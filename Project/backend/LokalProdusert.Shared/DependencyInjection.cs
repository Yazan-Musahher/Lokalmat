using System.Reflection;
using LokalProdusert.Shared.EventBus;
using LokalProdusert.Shared.MongoDB;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using MongoDB.Driver;

namespace LokalProdusert.Shared;

using EventBusInmem = EventBus.EventBus;

public static class DependencyInjection
{
    public static IServiceCollection AddShared(
        this IServiceCollection services, 
        IConfiguration configuration
    )
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        // Register the mongoDB 
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

        services.AddSingleton<InMemoryMessageQueue>();

        services.AddSingleton<IEventBus, EventBusInmem>();

        return services;
    }
}

