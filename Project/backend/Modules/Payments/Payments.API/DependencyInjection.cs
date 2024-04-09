using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Payments.API.Common.Errors;

using Sales.API.Common.Mapping;

namespace Payments.API;

public static class DepenedencyInjection
{
    public static IServiceCollection AddPaymentsAPI(this IServiceCollection services)
    {
        services.AddSingleton<ProblemDetailsFactory, PaymentProblemDetailsFactory>();
        services.AddMappings();
        return services;
    }

    public static IApplicationBuilder UsePaymentsAPI(this IApplicationBuilder app)
    {
        return app;
    }
}