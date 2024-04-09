
using Payments.Domain.PaymentAggregate;

namespace Payments.Application.Interfaces
{
    public interface IDomainEventPublisher
    {
        public Task SaveEventsAsync(PaymentAggregate paymentAggregate, CancellationToken cancellationToken);
    }
}