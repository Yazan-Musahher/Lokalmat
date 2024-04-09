using Administration.Application.Interfaces.Presistence;
using Administration.Application.Users.Commands;

using MediatR;

using Microsoft.Extensions.Logging;

using Users.Domain.UserAggregate.Enums;
using Users.IntegrationEvents.Events;

namespace Administration.Application.Events;


internal sealed class UserRegisteredIntegrationEventHandler : INotificationHandler<UserRegisteredIntegrationEvent>
{
    private readonly ILogger<UserRegisteredIntegrationEventHandler> _logger;
    private readonly IMediator _mediator;

    public UserRegisteredIntegrationEventHandler(
        ILogger<UserRegisteredIntegrationEventHandler> logger,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Handle(UserRegisteredIntegrationEvent @event, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _logger.LogInformation("Handling integration event: {IntegrationEvent}", @event);

        var userCommand = new UserCommand(
            @event.UserId,
            @event.Email,
            UserRole.Customer.ToString()
        );

        var result = await _mediator.Send(userCommand, cancellationToken);

        if (result.IsError)
        {
            _logger.LogError("Error handling integration event: {IntegrationEvent}. Error: {Error}", @event, result.IsError);
        }
    }
}