using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sales.Infrastructure.Models
{
    public class OrderAggregateDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        [BsonElement("customerId")]
        [BsonRepresentation(BsonType.String)]
        public Guid CustomerId { get; set; }
        [BsonElement("orderDate")]
        public DateTime OrderDate { get; set; }
        [BsonElement("orderStatus")]
        public string OrderStatus { get; set; } = string.Empty;

        [BsonElement("address")]
        public ShippingAddressDto Address { get; set; } = new ShippingAddressDto();
        [BsonElement("billingAddress")]
        public BillingAddressDto BillingAddress { get; set; } = new BillingAddressDto();

        [BsonElement("orderItems")]
        public List<OrderItemDto>? OrderItems { get; set; }
        [BsonElement("paymentStatus")]
        public string PaymentStatus { get; set; } = string.Empty;
        [BsonElement("shippingMethod")]
        public string ShippingMethod { get; set; } = string.Empty;
        [BsonElement("totalAmount")]
        public decimal TotalAmount { get; set; }
    }
}