using LokalProdusert.Shared.EventBus;

namespace Sales.IntegrationEvents.Events;

public sealed record OrderCreatedIntegrationEvent(Guid orderId, Guid customerId, Guid productId) : IntegrationEvent
{
    public Guid OrderId { get; init; } = orderId;
    public Guid CustomerId { get; init; } = customerId;
    public Guid ProductId { get; init; } = productId;
}