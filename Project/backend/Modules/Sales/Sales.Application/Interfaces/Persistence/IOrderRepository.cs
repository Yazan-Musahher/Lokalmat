using Sales.Domain.OrderAggregate;

namespace Sales.Application.interfaces;

public interface IOrderRepository
{
    // Add Order
    public Task AddOrderAsync(OrderAggregate order, CancellationToken cancellationToken);
    // Find Order by Id
    public Task<OrderAggregate?> FindByIdAsync(Guid orderId, CancellationToken cancellationToken);

    // Update Order
    public Task UpdateAsync(OrderAggregate order, CancellationToken cancellationToken);
}