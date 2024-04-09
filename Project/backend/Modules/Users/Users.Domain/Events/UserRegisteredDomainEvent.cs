using Users.Domain.Models;

namespace Users.Domain.Events;

public sealed record UserRegisteredDomainEvent(Guid userId, string userEmail) : IDomainEvent
{
    public Guid UserId { get; init; } = userId;
    public string UserEmail { get; init; } = userEmail;
}