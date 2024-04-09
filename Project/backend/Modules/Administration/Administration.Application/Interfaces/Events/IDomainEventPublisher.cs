using Administration.Domain.AdminAggregate;

namespace Administration.Application.Interfaces.Events
{
    public interface IDomainEventPublisher
    {
        public Task SaveEventsAsync(User user, CancellationToken cancellationToken);
    }
}