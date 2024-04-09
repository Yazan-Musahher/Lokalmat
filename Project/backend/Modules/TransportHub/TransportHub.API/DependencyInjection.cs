using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TransportHub.API.Common.Mapping;


namespace TransportHub.API;

public static class DepenedencyInjection
{
    public static IServiceCollection AddTransportHubAPI(this IServiceCollection services)
    {
        services.AddMappings();
        return services;
    }

    public static IApplicationBuilder UseTransportHubAPI(this IApplicationBuilder app)
    {
        return app;
    }
}