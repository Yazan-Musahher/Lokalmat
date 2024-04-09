using MongoDB.Bson.Serialization.Attributes;

namespace TransportHub.Infrastructure.Persistence.Models;

public class TransporterDto
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid Id { get; set; }
    [BsonElement("CompanyName")]
    public string? CompanyName { get; set; }
    [BsonElement("Description")]
    public string? Description { get; set; }
    [BsonElement("Address")]
    public TransporterAddressDto AddressDto { get; set; } = new();
}
