using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sales.Infrastructure.Models;

public class ProductInventoryDto
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    [BsonElement("UnitsInStock")]
    public decimal? UnitsInStock { get; set; }
    [BsonElement("UnitsOnOrder")]
    public decimal? UnitsOnOrder { get; set; }
    [BsonElement("ReorderLevel")]
    public decimal? ReorderLevel { get; set; }
    [BsonElement("Location")]
    public string? Location { get; set; }
}
