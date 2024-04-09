using ErrorOr;

using Payments.Application.Payment.Models;
using Payments.Domain.PaymentAggregate;
namespace Payments.Infrastructure.Presistence.Repositories;

public interface IPaymentProcessor
{
    Task<ErrorOr<PaymentResult?>> CreateCheckoutSessionAsync(PaymentAggregate paymentAggregate, CancellationToken cancellationToken = default);
    //Task<PaymentResult> ProcessPaymentAsync(PaymentAggregate paymentAggregate, CancellationToken cancellationToken = default);
}