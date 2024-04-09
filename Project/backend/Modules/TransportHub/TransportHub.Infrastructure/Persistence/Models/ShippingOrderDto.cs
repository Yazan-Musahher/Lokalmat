using MongoDB.Bson.Serialization.Attributes;

namespace TransportHub.Infrastructure.Persistence.Models
{
    public class ShippingOrderDto
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid OrderId { get; set; }
        [BsonElement("CustomerId")]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid CustomerId { get; set; }
        [BsonElement("ProductId")]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid ProductId { get; set; }
    }
}