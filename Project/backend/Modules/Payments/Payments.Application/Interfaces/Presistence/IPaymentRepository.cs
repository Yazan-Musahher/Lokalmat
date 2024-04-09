using Payments.Domain.PaymentAggregate;

namespace Payments.Application.Interfaces;


public interface IPaymentRepository
{
    Task AddPaymentAsync(PaymentAggregate payment, CancellationToken cancellationToken);

    Task<PaymentAggregate?> GetPaymentId(Guid paymentId, CancellationToken cancellationToken);

    Task UpdatePaymentStatusAsync(PaymentAggregate payment, CancellationToken cancellationToken);
}