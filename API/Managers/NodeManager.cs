using NexusFlow.src.models;
using NexusFlow.src.core;
using API.Singletons;
using NexusFlow.src.models.DB;
using Data.src.models.DB;
using MongoDB.Driver;

namespace API.Managers
{
    public static class NodeManager
    {
        public static NexusNodeDB GetUserRoot(string accesstoken)
        {
            var accesstoken_collection = MongoSingleton.Database.GetCollection<AccessTokenDB>(TablesSingleton.AccessTokens);

            var node_collection = MongoSingleton.Database.GetCollection<NexusNodeDB>(TablesSingleton.Nodes);

            List<AccessTokenDB> tokens = accesstoken_collection.Find(x => x.Id == accesstoken).ToList();

            if (tokens.Count == 0)
            {
                return null;
            }
            else
            {
                AccessTokenDB token = tokens.First();

                NexusNodeDB root = node_collection.Find(x => x.OwnerId == token.UserId && string.IsNullOrEmpty(x.ParentId)).ToList()[0];

                return root;

            }


        }
    }
}
