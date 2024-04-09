using Payments.Application.Interfaces;
using Payments.Domain.PaymentAggregate;
using Payments.Infrastructure.Presistence.Interceptors;

public class DomainEventPublisher : IDomainEventPublisher
{
    private readonly PuplishDomainEventsInterceptor _puplishDomainEventsInterceptor;
    
    public DomainEventPublisher(PuplishDomainEventsInterceptor puplishDomainEventsInterceptor)
    {
        _puplishDomainEventsInterceptor = puplishDomainEventsInterceptor;
    }
    
    public async Task SaveEventsAsync(PaymentAggregate paymentAggregate, CancellationToken cancellationToken)
    {
        var domainEvents = paymentAggregate.GetDomainEvents.ToList();
        if (domainEvents.Any())
        {
            await _puplishDomainEventsInterceptor.StoreDomainEventsAsync(domainEvents, cancellationToken);
            paymentAggregate.ClearDomainEvents();
        }
    }

}

