using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusFlow.src.models.DB
{
    public class NexusNodeDB
    {
        [BsonId]
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public List<string> ChildrenIds { get; set; }
        public string Data { get; set; }
        public int DataType { get; set; }
    }
}
