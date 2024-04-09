using ErrorOr;
using MediatR;
using Payments.Application.Interfaces;
using Payments.Application.Interfaces.Presistence;
using Payments.Application.Payment.Models;
using Payments.Domain.OrderAggregate.ValueObjects;
using Payments.Domain.PaymentAggregate;
using Payments.Infrastructure.Presistence.Repositories; 

namespace Payments.Application.Payment.Commands.CreatePayment;


public sealed class CreatePaymentCommandHandler : 
        IRequestHandler<CreatePaymentCommand, ErrorOr<(PaymentAggregate, PaymentResult)>>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentProcessor _paymentProcessor;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDomainEventPublisher _domainEventPublisher;

    public CreatePaymentCommandHandler(
                IPaymentRepository paymentRepository, 
                IPaymentProcessor paymentProcessor, 
                IUnitOfWork unitOfWork,
                IDomainEventPublisher domainEventPublisher)
    {
        _paymentRepository = paymentRepository;
        _paymentProcessor = paymentProcessor;
        _unitOfWork = unitOfWork;
        _domainEventPublisher = domainEventPublisher;
    }

    public async Task<ErrorOr<(PaymentAggregate, PaymentResult)>> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var payment = PaymentAggregate.Create(
            CustomerId.Create(command.CustomerId),
            OrderId.Create(command.OrderId),
            command.PaymentMethod,
            command.Amount,
            DateTime.Now
        );


        var result  = await _paymentProcessor.CreateCheckoutSessionAsync(payment, cancellationToken);
        if (result.IsError)
        {
            return Error.NotFound(result.ToString() ?? "General.NotFound");
        }

        var paymentResult = new PaymentResult
        {
            SessionId = result.Value!.SessionId,
            Url = result.Value.Url
        };

        await _unitOfWork.AddOperation(_paymentRepository.AddPaymentAsync(payment, cancellationToken));
        await _unitOfWork.AddOperation(_domainEventPublisher.SaveEventsAsync(payment, cancellationToken));
        await _unitOfWork.CommitChanges(cancellationToken);
        

        return (payment , paymentResult);
    }

}
