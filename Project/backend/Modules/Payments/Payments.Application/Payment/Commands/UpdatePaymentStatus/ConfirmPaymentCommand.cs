using ErrorOr;
using MediatR;

namespace Payments.Application.Payment.Commands.UpdatePaymentStatus
{
    public record ConfirmPaymentCommand(
        Guid PaymentId,
        string SessionId
        ) : IRequest<ErrorOr<Success>>;
}