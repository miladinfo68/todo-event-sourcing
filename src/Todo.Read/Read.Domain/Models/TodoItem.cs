using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Read.Domain.Models
{
    [BsonIgnoreExtraElements]
    public class TodoItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("aggregateId")]
        public Guid AggregateId { get; set; }

        [BsonElement("customerId")]
        public Guid CustomerId { get; set; }

        [BsonElement("title")]
        public string Title { get; set; } = string.Empty;

        [BsonElement("content")]
        public string Content { get; set; } = string.Empty;

        [BsonElement("status")]
        public int Status { get; set; }
    }
}
