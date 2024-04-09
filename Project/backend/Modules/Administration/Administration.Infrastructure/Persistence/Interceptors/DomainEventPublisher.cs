using Administration.Application.Interfaces.Events;
using Administration.Domain.AdminAggregate;

namespace Administration.Infrastructure.Persistence.Interceptors
{
    public class DomainEventPublisher : IDomainEventPublisher
    {
        private readonly PuplishDomainEventsInterceptor _puplishDomainEventsInterceptor;
        
        public DomainEventPublisher(PuplishDomainEventsInterceptor puplishDomainEventsInterceptor)
        {
            _puplishDomainEventsInterceptor = puplishDomainEventsInterceptor;
        }

        public async Task SaveEventsAsync(User user, CancellationToken cancellationToken)
        {
            var domainEvents = user.GetDomainEvents.ToList();
            if (domainEvents.Any())
            {
                await _puplishDomainEventsInterceptor.StoreDomainEventsAsync(domainEvents, cancellationToken);
                user.ClearDomainEvents();
            }
        }
    }
}


