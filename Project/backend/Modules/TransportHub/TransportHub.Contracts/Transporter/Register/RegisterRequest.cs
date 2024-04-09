namespace TransportHub.Contracts.Transporter.Register;

public record RegisterTransporterRequest(
    string CompanyName, 
    string Description,
    AddressRequest AddressRequest
    );

public record AddressRequest(string City, string ZipCode);