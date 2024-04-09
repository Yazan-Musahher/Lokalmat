using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

using Administration.Application.Behaviors;
using Administration.Application.Events;


namespace Administration.Application;

public static class DepenedencyInjection
{
    public static IServiceCollection AddAdministrationApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DepenedencyInjection).Assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        //services.AddMediatR(typeof(UserRegisteredIntegrationEventHandler).Assembly);
        return services;
    }
}