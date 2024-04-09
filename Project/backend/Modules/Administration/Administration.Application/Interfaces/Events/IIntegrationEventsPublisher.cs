using LokalProdusert.Shared.EventBus;

namespace Administration.Application.Interfaces.Events
{
    public interface IIntegrationEventsPublisher
    {
        public Task PublishAsync(IntegrationEvent @event, CancellationToken cancellationToken);
    }
}
