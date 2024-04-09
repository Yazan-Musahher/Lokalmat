using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

using Users.API.Common.Errors;
using Users.API.Common.Mapping;


namespace Users.API;

public static class DepenedencyInjection
{
    public static IServiceCollection AddUsersAPI(this IServiceCollection services)
    {
        services.AddSingleton<ProblemDetailsFactory, PaymentProblemDetailsFactory>();
        services.AddMappings();
        return services;
    }

    public static IApplicationBuilder UseUsersAPI(this IApplicationBuilder app)
    {
        return app;
    }
}