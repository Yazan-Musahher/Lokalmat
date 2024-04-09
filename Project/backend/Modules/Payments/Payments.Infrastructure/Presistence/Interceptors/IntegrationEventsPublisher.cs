using LokalProdusert.Shared.EventBus;
using Payments.Application.Interfaces;


namespace Payments.IntegrationEvents.Presistence.Interceptors;

public class IntegrationEventsPublisher : IIntegrationEventsPublisher
{
    private readonly IEventBus _eventBus;

    public IntegrationEventsPublisher(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task PublishAsync(IntegrationEvent @event, CancellationToken cancellationToken)
    {
        await _eventBus.PublishAsync(@event, cancellationToken);
    }
}
