using LokalProdusert.Shared.EventBus;

namespace Users.Application.Interfaces.Events
{
    public interface IIntegrationEventsPublisher
    {
        public Task PublishAsync(IntegrationEvent @event, CancellationToken cancellationToken);
    }
}
