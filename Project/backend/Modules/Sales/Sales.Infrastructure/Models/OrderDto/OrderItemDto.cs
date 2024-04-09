using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sales.Infrastructure.Models;

public class OrderItemDto
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    [BsonElement("productId")]
    [BsonRepresentation(BsonType.String)]
    public Guid ProductId { get; set; }
    [BsonElement("productName")]
    public string? ProductName { get; set; }
    [BsonElement("quantity")]
    public int Quantity { get; set; }
    [BsonElement("unitPrice")]
    public decimal UnitPrice { get; set; }
    [BsonElement("totalPrice")]
    public decimal TotalPrice { get; set; }
    [BsonElement("description")]
    public string? Description { get; set; }
}
