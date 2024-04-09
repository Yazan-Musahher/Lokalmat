using LokalProdusert.Shared.EventBus;

namespace Sales.Application.Interfaces;

public interface IIntegrationEventsPublisher
{
    public Task PublishAsync(IntegrationEvent @event, CancellationToken cancellationToken);
}
