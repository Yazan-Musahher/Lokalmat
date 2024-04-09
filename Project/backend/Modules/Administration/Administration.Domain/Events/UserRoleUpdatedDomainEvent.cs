using Administration.Domain.Models;

namespace Administration.Domain.Events;

public sealed record UserRoleUpdatedDomainEvent(Guid userId, string role) : IDomainEvent
{
    public Guid UserId { get; init; } = userId;
    public string Role { get; init; } = role;
}