using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sales.Infrastructure.Models
{
    public class ShippingAddressDto
    {
        [BsonElement("streetName")]
        public string? StreetName { get; set; }
        [BsonElement("streetNumber")]
        public string? StreetNumber { get; set; }
        [BsonElement("postalCode")]
        public string? PostalCode { get; set; }
        [BsonElement("city")]
        public string? City { get; set; }
        [BsonElement("additionalDetails")]
        public string? AdditionalDetails { get; set; }
    }
}