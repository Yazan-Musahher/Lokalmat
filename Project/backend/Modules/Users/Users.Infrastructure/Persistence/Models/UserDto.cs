using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Users.Infrastructure.Persistence.Models
{
    public class UserDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        [BsonElement("FirstName")]
        public string FirstName { get; set; } = string.Empty;
        [BsonElement("LastName")]
        public string LastName { get; set; } =  string.Empty;
        [BsonElement("Email")]
        public string Email { get; set; } = string.Empty;
        [BsonElement("Password")]
        public string Password { get; set; } = string.Empty;
        [BsonElement("UserRole")]
        public string UserRole { get; set; } = string.Empty;
        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; }
    }
}