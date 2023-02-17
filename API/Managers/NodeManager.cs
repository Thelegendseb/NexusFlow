using NexusFlow.src.models;
using API.Singletons;
using NexusFlow.src.models.DB;
using Data.src.models.DB;
using MongoDB.Driver;
using Schemas.src.models.DTO;
using System.Linq.Expressions;
using NexusFlow.src.services;
using Microsoft.AspNetCore.Mvc;

namespace API.Managers
{
    public static class NodeManager
    {
        public static NexusNodeDTO GetUserRoot(string accesstoken)
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

                return new NexusNodeDTO()
                {
                    Id = root.Id,
                    DataType = 0,
                    Name = root.Name
                };
            }

        }

        public static List<NexusNodeDTO> GetChildNodes(string accesstoken, string parentid)
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

                List<NexusNodeDB> childNodeDB = node_collection.Find(x => x.OwnerId == token.UserId && x.ParentId == parentid).ToList();

                List<NexusNodeDTO> childNodeDTO = new List<NexusNodeDTO>();

                foreach (NexusNodeDB node in childNodeDB)
                {
                    NexusNodeDTO newNodeDTO = new NexusNodeDTO()
                    {
                        Id = node.Id,
                        Name = node.Name,
                        DataType = node.DataType,
                    };

                    childNodeDTO.Add(newNodeDTO);

                }

                return childNodeDTO;

            }

        }

        public static NexusNodeDataDTO GetNodeData(string accesstoken, string nodeid)
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

                NexusNodeDB node = node_collection.Find(x => x.OwnerId == token.UserId && x.Id == nodeid).ToList()[0];

                string script_;
                string data_;

                switch (node.DataType)
                {
                    case 0:

                        data_ = node.Data;
                        script_ = "";

                        break;
                    case 1:

                        script_ = node.Data;
                        // Throws error
                        data_ = ScriptRunner.Run(node.Data).Result;

                        break;
                    default:

                        data_ = node.Data;
                        script_ = "";

                        break;

                }

                NexusNodeDataDTO nodeDataDTO = new NexusNodeDataDTO()
                {
                    Data = data_,
                    Script = script_
                };

                return nodeDataDTO;

            }

        }

        public static IActionResult AddNode(NexusNodeCreationDTO newNode, string accesstoken, string parentid)
        {

            var accesstoken_collection = MongoSingleton.Database.GetCollection<AccessTokenDB>(TablesSingleton.AccessTokens);

            var node_collection = MongoSingleton.Database.GetCollection<NexusNodeDB>(TablesSingleton.Nodes);

            List<AccessTokenDB> tokens = accesstoken_collection.Find(x => x.Id == accesstoken).ToList();

            if (tokens.Count == 0)
            {
                return new UnauthorizedResult();
            }
            else
            {
                AccessTokenDB token = tokens.First();

                NexusNodeDB newNodeDB = new NexusNodeDB() { 
                  Name = newNode.Name,
                  Id = Guid.NewGuid().ToString(),
                  ParentId = parentid,
                  Data = newNode.Data,
                  DataType = newNode.DataType,
                  OwnerId = token.UserId
                };

                node_collection.InsertOne(newNodeDB);

                return new OkResult();

            }

        }

        public static IActionResult DeleteNode(string accesstoken, string nodeid)
        {
            var accesstoken_collection = MongoSingleton.Database.GetCollection<AccessTokenDB>(TablesSingleton.AccessTokens);

            var node_collection = MongoSingleton.Database.GetCollection<NexusNodeDB>(TablesSingleton.Nodes);

            List<AccessTokenDB> tokens = accesstoken_collection.Find(x => x.Id == accesstoken).ToList();

            if (tokens.Count == 0)
            {
                return new UnauthorizedResult();
            }
            else
            {
                AccessTokenDB token = tokens.First();

                node_collection.DeleteOne(x => x.Id == nodeid);

                return new OkResult();

            }

        }

        public static IActionResult EditNode(NexusNodeCreationDTO newNode, string accesstoken, string nodeid)
        {
            var accesstoken_collection = MongoSingleton.Database.GetCollection<AccessTokenDB>(TablesSingleton.AccessTokens);

            var node_collection = MongoSingleton.Database.GetCollection<NexusNodeDB>(TablesSingleton.Nodes);

            List<AccessTokenDB> tokens = accesstoken_collection.Find(x => x.Id == accesstoken).ToList();

            if (tokens.Count == 0)
            {
                 return new UnauthorizedResult();
            }
            else
            {
                 AccessTokenDB token = tokens.First();

           
                NexusNodeDB oldNodeDB = node_collection.Find(x => x.Id == nodeid).ToList()[0];

                NexusNodeDB newNodeDB = new NexusNodeDB 
                { 
                Id = oldNodeDB.Id,
                OwnerId = oldNodeDB.OwnerId,              
                ParentId = oldNodeDB.ParentId,
                Data = newNode.Data,    
                DataType = newNode.DataType,
                Name = newNode.Name
                };
                // construct newNodeDB from oldNodeDB 

                node_collection.ReplaceOne(x => x.Id == nodeid, newNodeDB);

                return new OkResult();

            }

        }

    }
}


