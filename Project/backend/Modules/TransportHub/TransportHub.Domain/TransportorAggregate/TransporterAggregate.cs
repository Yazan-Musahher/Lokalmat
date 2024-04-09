using TransportHub.Domain.Events;
using TransportHub.Domain.Models;
using TransportHub.Domain.TransporterAggregate.Entities;
using TransportHub.Domain.TransporterAggregate.ValueObjects;

namespace TransportHub.Domain.TransporterAggregate;

public sealed class TransporterAggregate : AggregateRoot<TransporterId>
{

    public string? CompanyName { get; private set; }
    public string? Description { get; private set; }
    public TransporterAddress Address { get; private set; }

    private TransporterAggregate(
        TransporterId transporterId, 
        string companyName, 
        string description, 
        
        TransporterAddress address)
        : base(transporterId)
    {
        CompanyName = companyName;
        Description = description;
        Address = address;
    }

    public static TransporterAggregate Create(
        string name, 
        string description, 
        string city, 
        string zipCode)
    {
        var transportor= new TransporterAggregate(
            TransporterId.CreateUnique(),
            name,
            description,
            TransporterAddress.Create(city, zipCode)
            );
        transportor.RaiseDomainEvent(new TransportorRegisteredDomainEvent(transportor.Id.Value, name));
        return transportor;
    }

    public static TransporterAggregate Create(
        TransporterId transportorId, 
        string name, 
        string description, 
        TransporterAddress address)
    {
        return new(
            transportorId,
            name,
            description,
            address
            );
    }
}
