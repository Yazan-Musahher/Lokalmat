using Users.Application.Interfaces.Events;
using Users.Domain.UserAggregate;

namespace Users.Infrastructure.Persistence.Interceptors
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


