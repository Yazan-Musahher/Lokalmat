using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Payments.Infrastructure.Models
{
    public sealed class PaymentDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        [BsonElement("CustomerId")]
        [BsonRepresentation(BsonType.String)]
        public Guid CustomerId { get; set; }
        [BsonElement("OrderId")]
        [BsonRepresentation(BsonType.String)]
        public Guid OrderId { get; set; }
        [BsonElement("PaymentMethod")]
        public string PaymentMethod { get; set; } = string.Empty;
        [BsonElement("Amount")]
        public decimal Amount { get; set; }
        [BsonElement("PaymentStatus")]
        public string PaymentStatus { get; set; } = string.Empty;
        [BsonElement("PaymentDate")]
        public DateTime PaymentDate { get; set; }

    }
}