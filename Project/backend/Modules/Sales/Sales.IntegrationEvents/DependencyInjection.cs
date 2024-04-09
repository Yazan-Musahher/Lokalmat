using MediatR;
using Microsoft.Extensions.DependencyInjection;

using Sales.IntegrationEvents.Events;


namespace Sales.IntegrationEvents
{
    public static class DepenedencyInjection
    {
        public static IServiceCollection AddSalesIntegrationEvents(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DepenedencyInjection).Assembly);
            return services;
        }
    }
}
