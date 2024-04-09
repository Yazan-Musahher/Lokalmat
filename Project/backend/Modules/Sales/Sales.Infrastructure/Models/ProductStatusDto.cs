using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Sales.Domain.ProductAggregate.Enums;

namespace Sales.Infrastructure.Models;

public class ProductStatusDto
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    [BsonElement("Status")]
    [BsonRepresentation(BsonType.String)]
    public Status Status { get; set; }
    [BsonElement("Description")]
    public string? Description { get; set; }
}
