using Administration.IntegrationEvents.Events;
using ErrorOr;
using MediatR;

using Users.Application.Interfaces.Presistence;
using Users.Domain.UserAggregate.Enums;

namespace Users.Application.Events;

public sealed class UserRoleUpdatedIntegrationEventHandler : INotificationHandler<UserRoleUpdatedIntegrationEvent>
{
    private readonly IUserRepository _userRepository;

    public UserRoleUpdatedIntegrationEventHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(UserRoleUpdatedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(@event.UserId, cancellationToken);
        if (user is null)
        {
            Error.Conflict(
                code: "user_not_found",
                description: $"User with id {@event.UserId} not found"
            );
        }
        UserRole role = (UserRole)Enum.Parse(typeof(UserRole), @event.Role);
        user!.UpdateRole(role);
        await _userRepository.UpdateAsync(user, cancellationToken);
    }
}