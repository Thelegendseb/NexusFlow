using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Models.DB
{
    public class AccessTokenDB
    {
        [BsonId]
        public string Id { get; set; }
        public string UserId { get; set; }
    }
}
