using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LokalProdusert.Shared.Outbox;

public sealed class OutboxMessage
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    [BsonElement("MessageType")]
    public string MessageType { get; set; } = string.Empty;
    [BsonElement("Message")]
    public string Message { get; set; } = string.Empty;
    [BsonElement("OccurredOn")]
    public DateTime OccurredOn { get; set; }
    [BsonElement("ProcessedOn")]
    public DateTime? ProcessedOn { get; set; }
    [BsonElement("Error")]
    public string? Error { get; set; }
}