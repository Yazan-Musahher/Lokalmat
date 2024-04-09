using LokalProdusert.Shared.EventBus;

using Users.Application.Interfaces.Events;


namespace Users.Infrastructure.Persistence.Interceptors;

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
