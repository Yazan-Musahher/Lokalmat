using ErrorOr;
using MediatR;
using Users.Application.Interfaces.Events;
using Users.Application.Interfaces.Presistence;
using Users.Domain.Events;
using Users.IntegrationEvents.Events;

namespace Users.Application.Events;

public sealed class UserRegisteredDomainEventHandler : INotificationHandler<UserRegisteredDomainEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly IIntegrationEventsPublisher _eventBus;

    public UserRegisteredDomainEventHandler(IUserRepository userRepository, IIntegrationEventsPublisher eventBus)
    {
        _userRepository = userRepository;
        _eventBus = eventBus;
    }

    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(notification.UserId, cancellationToken);

        if (user is null)
        {
            Error.Conflict(
                code: "user not found",
                description: $"User with id {notification.UserId} not found."
            );
        }

        await _eventBus.PublishAsync(new UserRegisteredIntegrationEvent(notification.UserId, notification.UserEmail), cancellationToken);
    }
}