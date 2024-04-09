using ErrorOr;
using MediatR;
using Payments.Application.Interfaces;
using Payments.Domain.PaymentAggregate.Enums;
using Payments.IntegrationEvents.Events;

namespace Payments.Application.Payment.Commands.UpdatePaymentStatus;


public sealed class ConfirmPaymentCommandHandler : IRequestHandler<ConfirmPaymentCommand, ErrorOr<Success>>
{
    public readonly IPaymentRepository _paymentRepository;
    public readonly IIntegrationEventsPublisher _eventsBus;
    public ConfirmPaymentCommandHandler(IPaymentRepository paymentRepository, IIntegrationEventsPublisher eventsBus)
    {
        _paymentRepository = paymentRepository;
        _eventsBus = eventsBus;
    }


    public async Task<ErrorOr<Success>> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        var payment = await _paymentRepository.GetPaymentId(request.PaymentId, cancellationToken);

        if (payment is null)
        {
           return Error.NotFound("Payment not found");
        }

        payment.UpdatePaymentStatus(PaymentStatus.Paid);

        await _paymentRepository.UpdatePaymentStatusAsync(payment, cancellationToken);

        await _eventsBus.PublishAsync(new PaymentCreatedIntegrationEvent(payment.Id.Value, payment.CustomerId.Value, payment.OrderId.Value, payment.PaymentStatus.ToString() ), cancellationToken);

        return new Success();
    }
}