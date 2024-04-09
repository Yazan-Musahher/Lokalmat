using Payments.Domain.Models;

namespace Payments.Domain.PaymentAggregate.Events;

public sealed record PaymentCreatedDomainEvent(Guid paymentId, Guid customerId, Guid orderId, string paymentStatus) : IDomainEvent
{
    public Guid PaymentId { get; init; } = paymentId;
    public Guid CustomerId { get; init; } = customerId;
    public Guid OrderId { get; init; } = orderId;
    public string PaymentStatus { get; init; } = paymentStatus;
}