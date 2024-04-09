using TransportHub.Domain.Models;
using TransportHub.Domain.TransporterAggregate.ValueObjects;

namespace TransportHub.Domain.TransporterAggregate.Entities;

public sealed class TransporterAddress : Entity<TransporterAddressId>
{
    public string? City { get; private set; }
    public string? ZipCode { get; private set; }

    private TransporterAddress(TransporterAddressId transportorAddressId,
     string city, string zipCode)
     : base(transportorAddressId)
    {
        City = city;
        ZipCode = zipCode;
    }

    public static TransporterAddress Create(string city, string zipCode)
    {
        return new(
            TransporterAddressId.CreateUnique(), 
            city,
            zipCode
            );
    }

    public static TransporterAddress Create(
        TransporterAddressId transporterAddressId, 
        string city, 
        string zipCode)
    {
        return new(
            transporterAddressId,
            city,
            zipCode
            );
    }
}
