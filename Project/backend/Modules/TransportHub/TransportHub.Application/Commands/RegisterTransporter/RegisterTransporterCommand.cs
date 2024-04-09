using ErrorOr;
using MediatR;
using TransportHub.Domain.TransporterAggregate;

namespace TransportHub.Application.Commands.RegisterTransporter;

public record RegisterTransporterCommand(
    string CompanyName, 
    string Description, 
    Address AddressCommnad
    ) : IRequest<ErrorOr<TransporterAggregate>>;

public record Address(
    string City, 
    string ZipCode
    );