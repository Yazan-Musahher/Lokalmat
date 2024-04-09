namespace Payments.Contracts.Payment.CreatePayment
{
    public sealed record CreatePaymentRequest(
        Guid CustomerId,
        Guid OrderId,
        string PaymentMethod,
        decimal Amount
    );
}