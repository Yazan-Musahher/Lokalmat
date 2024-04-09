using LokalProdusert.Shared.EventBus;

namespace Payments.IntegrationEvents.Events;

public sealed record PaymentCreatedIntegrationEvent(Guid paymentId, Guid customerId, Guid orderId, string paymentStatus) : IntegrationEvent
{
    public Guid PaymentId { get; init; } = paymentId;
    public Guid CustomerId { get; init; } = customerId;
    public Guid OrderId { get; init; } = orderId;
    public string PaymentStatus { get; init; } = paymentStatus;
}