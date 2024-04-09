using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TransportHub.Application.Behaviors;
using TransportHub.Application.Events;


namespace TransportHub.Application;

public static class DepenedencyInjection
{
    public static IServiceCollection AddTransportHubApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DepenedencyInjection).Assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(typeof(OrderCreatedDomainEventHandler).Assembly);
        return services;
    }
}