using System.Reflection;
using System.Text;

using LokalProdusert.Shared.MongoDB;
using Mapster;
using MapsterMapper;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using MongoDB.Driver;

using Users.Application.Interfaces.Autentication;
using Users.Application.Interfaces.Events;
using Users.Application.Interfaces.Presistence;
using Users.Infrastructure.Autentication;
using Users.Infrastructure.Persistence.Interceptors;
using Users.Infrastructure.Persistence.Repositories;
using Users.Infrastructure.Presistence.Repositories;

namespace Users.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddUsersInfrastructure(
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

        services.AddAuth(configuration);

        services.AddScoped<IIntegrationEventsPublisher, IntegrationEventsPublisher>();
        

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        var jwtSettingsSection = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettingsSection);

        services.AddSingleton(Options.Create(jwtSettingsSection));
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettingsSection.Issuer,
                ValidAudience = jwtSettingsSection.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettingsSection.Secret))
            });

        return services;
    }
}
