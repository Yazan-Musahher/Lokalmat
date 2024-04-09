using TransportHub.Application.Interfaces;
using TransportHub.Domain.TransporterAggregate;
using TransportHub.Infrastructure.Persistence.Interceptors;

namespace TransportHub.Infrastructure.Persistence.Interceptors;

public class DomainEventPublisher : IDomainEventPublisher
{
    private readonly PuplishDomainEventsInterceptor _puplishDomainEventsInterceptor;
    
    public DomainEventPublisher(PuplishDomainEventsInterceptor puplishDomainEventsInterceptor)
    {
        _puplishDomainEventsInterceptor = puplishDomainEventsInterceptor;
    }
    
    public async Task SaveEventsAsync(TransporterAggregate orderAggregate, CancellationToken cancellationToken)
    {
        var domainEvents = orderAggregate.GetDomainEvents.ToList();
        if (domainEvents.Any())
        {
            await _puplishDomainEventsInterceptor.StoreDomainEventsAsync(domainEvents, cancellationToken);
            //orderAggregate.ClearDomainEvents();
        }
    }

}

