using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Administration.Infrastructure.Persistence.Models
{
    public class UserDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; } = string.Empty;
        [BsonElement("UserRole")]
        public string UserRole { get; set; } = string.Empty;
        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; }
    }
}