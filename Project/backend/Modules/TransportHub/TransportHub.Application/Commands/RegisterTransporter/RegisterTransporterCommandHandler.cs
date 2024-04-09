using ErrorOr;
using MediatR;

using TransportHub.Application.Interfaces;
using TransportHub.Domain.TransporterAggregate;

namespace TransportHub.Application.Commands.RegisterTransporter;


public class RegisterCommandHandler : 
    IRequestHandler<RegisterTransporterCommand, ErrorOr<TransporterAggregate>>
{

    private readonly ITransporterRepository _transporterRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDomainEventPublisher _domainEventPublisher;
    
    public RegisterCommandHandler(ITransporterRepository transporterRepository, 
        IUnitOfWork unitOfWork,
        IDomainEventPublisher domainEventPublisher)
    {
        _transporterRepository = transporterRepository;
        _unitOfWork = unitOfWork;
        _domainEventPublisher = domainEventPublisher;
    }


    public async Task<ErrorOr<TransporterAggregate>> Handle(
            RegisterTransporterCommand request, 
            CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var transporter = TransporterAggregate.Create(
            request.CompanyName,
            request.Description,
            request.AddressCommnad.City,
            request.AddressCommnad.ZipCode
            
            );
        
        // add transporter to the database
        await _unitOfWork.AddOperation(_transporterRepository.AddTransporterAsync(transporter, cancellationToken));
        // publish domain events
        await _domainEventPublisher.SaveEventsAsync(transporter, cancellationToken);
        // commit the transaction
        await _unitOfWork.CommitChanges(cancellationToken);

        return transporter;
    }
}