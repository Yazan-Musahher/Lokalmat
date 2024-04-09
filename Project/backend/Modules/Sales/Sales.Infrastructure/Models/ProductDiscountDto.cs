using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sales.Infrastructure.Models;

public class ProductDiscountDto
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    [BsonElement("discountPercentage")]
    public decimal DiscountPercentage { get; set; }
    [BsonElement("startDate")]
    public DateTime StartDate { get; set; }
    [BsonElement("endDate")]
    public DateTime EndDate { get; set; }
    [BsonElement("categoryId")]
    [BsonRepresentation(BsonType.String)]
    public Guid? CategoryId { get; set; }

}