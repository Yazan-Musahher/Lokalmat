using ErrorOr;
using MediatR;

using Payments.Application.Payment.Models;
using Payments.Domain.PaymentAggregate;
namespace Payments.Application.Payment.Commands.CreatePayment;


public sealed record CreatePaymentCommand(
    Guid OrderId,
    Guid CustomerId,
    string PaymentMethod,
    decimal Amount,
    string SessionId,
    string UrlSuccess
    ) : IRequest<ErrorOr<(PaymentAggregate, PaymentResult)>>;
