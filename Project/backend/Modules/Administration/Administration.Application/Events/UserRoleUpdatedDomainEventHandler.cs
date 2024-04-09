using Administration.Application.Interfaces.Events;
using Administration.Application.Interfaces.Presistence;
using Administration.Domain.Events;
using Administration.IntegrationEvents.Events;

using ErrorOr;

using MediatR;

namespace Administration.Application.Events;

public sealed class UserRoleUpdatedDomainEventHandler : INotificationHandler<UserRoleUpdatedDomainEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly IIntegrationEventsPublisher _eventBus;

    public UserRoleUpdatedDomainEventHandler(IUserRepository userRepository, IIntegrationEventsPublisher eventBus)
    {
        _userRepository = userRepository;
        _eventBus = eventBus;
    }

    public async Task Handle(UserRoleUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(notification.UserId, cancellationToken);

        if (user is null)
        {
            Error.Conflict(
                code: "user not found",
                description: $"User with id {notification.UserId} not found."
            );
        }

        await _eventBus.PublishAsync(new UserRoleUpdatedIntegrationEvent(notification.UserId, notification.Role), cancellationToken);
    }
}