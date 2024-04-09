using Sales.Domain.OrderAggregate;
using Sales.Domain.ProductAggregate;

namespace Sales.Application.Interfaces
{
    public interface IDomainEventPublisher
    {
        public Task SaveEventsAsync(OrderAggregate orderAggregate,
         CancellationToken cancellationToken);

        public Task SaveProductEventsAsync(Product product,
         CancellationToken cancellationToken);
    }
}