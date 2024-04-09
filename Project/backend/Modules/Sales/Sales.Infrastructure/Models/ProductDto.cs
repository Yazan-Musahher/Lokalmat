using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Sales.Infrastructure.Models;

public class ProductDto
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    [BsonElement("productName")]
    public string? ProductName { get; set; }
    [BsonElement("unitPrice")]
    public decimal? UnitPrice { get; set; }
    [BsonElement("description")]
    public string? Description { get; set; }
    [BsonElement("categoryId")]
    [BsonRepresentation(BsonType.String)]
    public Guid CategoryId { get; set; }
    [BsonElement("productDiscounts")]
    public List<ProductDiscountDto>? ProductDiscountDtos { get; set; }
    [BsonElement("images")]
    public List<ProductImageDto>? Images { get; set; }
    [BsonElement("inventory")]
    public List<ProductInventoryDto>? InventoryDtos { get; set; }
    [BsonElement("status")]
    public List<ProductStatusDto>? StatusDtos { get; set; }
    [BsonElement("createdDate")]
    public DateTime? CreatedDateTime { get; set; }
}