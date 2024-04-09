using Sales.Application.Interfaces;
using Sales.Domain.OrderAggregate;
using Sales.Domain.ProductAggregate;

namespace Sales.Infrastructure.Persistence.Interceptors;

public class DomainEventPublisher : IDomainEventPublisher
{
    private readonly PuplishDomainEventsInterceptor _puplishDomainEventsInterceptor;
    
    public DomainEventPublisher(PuplishDomainEventsInterceptor puplishDomainEventsInterceptor)
    {
        _puplishDomainEventsInterceptor = puplishDomainEventsInterceptor;
    }
    
    public async Task SaveEventsAsync(OrderAggregate orderAggregate, CancellationToken cancellationToken)
    {
        var domainEvents = orderAggregate.GetDomainEvents.ToList();
        if (domainEvents.Any())
        {
            await _puplishDomainEventsInterceptor.StoreDomainEventsAsync(domainEvents, cancellationToken);
            orderAggregate.ClearDomainEvents();
        }
    }

    public async Task SaveProductEventsAsync(Product product, CancellationToken cancellationToken)
    {
        var domainEvents = product.GetDomainEvents.ToList();
        if (domainEvents.Any())
        {
            await _puplishDomainEventsInterceptor.StoreDomainEventsAsync(domainEvents, cancellationToken);
            product.ClearDomainEvents();
        }
    }

}

