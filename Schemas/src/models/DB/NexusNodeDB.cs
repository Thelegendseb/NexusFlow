using MongoDB.Bson.Serialization.Attributes;

namespace NexusFlow.models.DB
{
    public class NexusNodeDB
    {
        [BsonId]
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
        public int DataType { get; set; }
    }
}
