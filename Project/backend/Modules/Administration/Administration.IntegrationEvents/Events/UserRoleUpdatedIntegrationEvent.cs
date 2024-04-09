using LokalProdusert.Shared.EventBus;

namespace Administration.IntegrationEvents.Events
{
    public sealed record UserRoleUpdatedIntegrationEvent(
        Guid UserId, 
        string Role) : IntegrationEvent;
   
}