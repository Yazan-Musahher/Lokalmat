using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sales.Infrastructure.Models.Categories;

public class CategoryDto
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    [BsonElement("categoryName")]
    public string? CategoryName { get; set; }

    [BsonElement("productIds")]
    [BsonRepresentation(BsonType.String)]
    public List<Guid> ProductIds { get; set; } = new();
}