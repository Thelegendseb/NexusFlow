using API.Singletons;
using API.NodeServices;
using API.Models.DTO;
using API.Models.DB;
using MongoDB.Driver;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

namespace API.Managers
{
    public class AdminManager
    {

        public static IActionResult ClearAll()
        {

            // get all collections

            var accesstoken_collection = MongoSingleton.Database.GetCollection<AccessTokenDB>(TablesSingleton.AccessTokens);

            var node_collection = MongoSingleton.Database.GetCollection<NexusNodeDB>(TablesSingleton.Nodes);

            var user_collection = MongoSingleton.Database.GetCollection<UserDB>(TablesSingleton.Users);

            // clear all collections

            accesstoken_collection.DeleteMany(x => true);

            node_collection.DeleteMany(x => true);

            user_collection.DeleteMany(x => true);

            return new OkResult();

        }

    }
}
