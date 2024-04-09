namespace Payments.Contracts.Payment.CreatePayment;


public sealed record CreatePaymentResponse(
    string PaymentId,
    string CustomerId,
    string OrderId,
    string PaymentMethod,
    decimal Amount,
    DateTime DateTime,
    PaymentResultResponse PaymentResultResponse
);

public sealed record PaymentResultResponse(
    string SessionId,
    string Url
);
