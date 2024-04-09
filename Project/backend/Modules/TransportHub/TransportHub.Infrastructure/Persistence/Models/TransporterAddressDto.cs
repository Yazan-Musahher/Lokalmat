using MongoDB.Bson.Serialization.Attributes;

namespace TransportHub.Infrastructure.Persistence.Models;

public class TransporterAddressDto
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid Id { get; set; }
    [BsonElement("City")]
    public string City { get; set; } = string.Empty;
    [BsonElement("ZipCode")]
    public string ZipCode { get; set; } = string.Empty;
}
