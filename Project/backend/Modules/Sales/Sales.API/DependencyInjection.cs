using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

using Sales.API.Common.Errors;
using Sales.API.Common.Mapping;

namespace Sales.API;

public static class DepenedencyInjection
{
    public static IServiceCollection AddSalesAPI(this IServiceCollection services)
    {
        services.AddSingleton<ProblemDetailsFactory, SalesProblemDetailsFactory>();
        services.AddMappings();
        return services;
    }

    public static IApplicationBuilder UseSalesAPI(this IApplicationBuilder app)
    {
        return app;
    }
}