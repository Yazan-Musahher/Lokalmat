using TransportHub.Domain.TransporterAggregate;

namespace TransportHub.Application.Interfaces
{
    public interface IDomainEventPublisher
    {
        public Task SaveEventsAsync(TransporterAggregate transporterAggregate,
         CancellationToken cancellationToken);
    }
}