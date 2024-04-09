namespace TransportHub.Contracts.Transporter.Register;

public record RegisterTransporterResponse(
    Guid TransporterId,
    string CompanyName, 
    string Description,
    Address AddressResponse
    );

public record Address(
    string AddressId,
    string City, 
    string ZipCode);