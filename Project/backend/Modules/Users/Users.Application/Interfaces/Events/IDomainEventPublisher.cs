using Users.Domain.UserAggregate;

namespace Users.Application.Interfaces.Events
{
    public interface IDomainEventPublisher
    {
        public Task SaveEventsAsync(User user, CancellationToken cancellationToken);
    }
}