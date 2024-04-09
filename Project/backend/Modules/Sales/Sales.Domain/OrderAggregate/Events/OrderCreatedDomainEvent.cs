using Sales.Domain.Models;
namespace Sales.Domain.Events;

public sealed record OrderCreatedDomainEvent(Guid orderId, Guid customerId, Guid productId) :IDomainEvent
{
    public Guid OrderId { get; init; } = orderId;
    public Guid CustomerId { get; init; } = customerId;
    public Guid ProductId { get; init; } = productId;
}
