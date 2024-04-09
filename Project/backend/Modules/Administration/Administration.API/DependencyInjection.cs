using Administration.API.Common.Errors;
using Administration.API.Common.Mapping;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;


namespace Administration.API;

public static class DepenedencyInjection
{
    public static IServiceCollection AddAdministrationAPI(this IServiceCollection services)
    {
        services.AddSingleton<ProblemDetailsFactory, AdminProblemDetailsFactory>();
        services.AddMappings();
        return services;
    }

    public static IApplicationBuilder UseAdministrationAPI(this IApplicationBuilder app)
    {
        return app;
    }
}