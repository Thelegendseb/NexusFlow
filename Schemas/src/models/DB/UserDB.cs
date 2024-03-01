using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusFlow.models.DB
{

    public class UserDB
    {
        [BsonId]
        public string Id { get; set; }
        public string RootId { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }

}



