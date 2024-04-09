using Payments.Domain.Models;
using Payments.Domain.OrderAggregate.ValueObjects;
using Payments.Domain.PaymentAggregate.Enums;
using Payments.Domain.PaymentAggregate.Events;

namespace Payments.Domain.PaymentAggregate;


public sealed class PaymentAggregate : AggregateRoot<PaymentId>
{
    public OrderId OrderId { get; private set; }
    public CustomerId CustomerId { get; private set; }
    public string? PaymentMethod { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime PaymentDate { get; private set; }

    public PaymentStatus PaymentStatus { get; private set; }

    private PaymentAggregate(
        PaymentId paymentId,
        CustomerId customerId, 
        OrderId orderId,
        string paymentMethod, 
        decimal amount,
        DateTime paymentDate)
        : base(paymentId)
    {
        CustomerId = customerId;
        OrderId = orderId;
        PaymentMethod = paymentMethod;
        Amount = amount;
        PaymentStatus = PaymentStatus.Pending;
        PaymentDate = paymentDate;
    }

    public static PaymentAggregate Create(
        CustomerId customerId,
        OrderId orderId,
        string paymentMethod, 
        decimal amount, 
        DateTime paymentDate
        )
    {
        var payment = new PaymentAggregate(
            PaymentId.CreateUnique(),
            customerId,
            orderId,
            paymentMethod,
            amount,
            paymentDate
            );
        payment.RaiseDomainEvent(new PaymentCreatedDomainEvent(payment.Id.Value, customerId.Value, orderId.Value, payment.PaymentStatus.ToString()));
        return payment;
    }

    public static PaymentAggregate Create(
        PaymentId paymentId,
        CustomerId customerId,
        OrderId orderId, 
        string paymentMethod, 
        decimal amount, 
        DateTime paymentDate)
    {
        return new(
            paymentId,
            customerId,
            orderId,
            paymentMethod,
            amount,
            paymentDate
            );
    }

    // update payment status
    public void UpdatePaymentStatus(PaymentStatus paymentStatus)
    {
        PaymentStatus = paymentStatus;
    }
}