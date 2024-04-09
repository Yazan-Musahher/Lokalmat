using LokalProdusert.Shared.EventBus;

namespace Payments.Application.Interfaces
{
    public interface IIntegrationEventsPublisher
    {
        public Task PublishAsync(IntegrationEvent @event, CancellationToken cancellationToken);
    }
}
