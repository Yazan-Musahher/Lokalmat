using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sales.Infrastructure.Models
{
    public class ProductImageDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("url")]
        public string? Url { get; set; }
        [BsonElement("alt")]
        public string? Alt { get; set; }
        [BsonElement("title")]
        public string? Title { get; set; }
        [BsonElement("contentType")]
        public string? ContentType { get; set; }
    }
}