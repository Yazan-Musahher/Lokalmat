using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sales.Infrastructure.Models;

public class BillingAddressDto
{
    [BsonElement("Name")]
    public string? Name { get; set; }
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