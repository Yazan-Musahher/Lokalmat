using LokalProdusert.Shared.EventBus;

namespace Users.IntegrationEvents.Events
{
    public sealed record UserRegisteredIntegrationEvent(
        Guid UserId, 
        string Email) : IntegrationEvent;
}