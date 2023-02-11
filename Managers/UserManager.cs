using NexusFlow.src.models;
using NexusFlow.src.core;
using API.Singletons;

namespace API.Managers
{
    public static class UserManager
    {
        public static void CreateUser(string name)
        {
            // Create user class
            User newUser = new User();
            newUser.Name = name;    
            newUser.Id = Guid.NewGuid().ToString();

            // Create root of user class
            NexusNodeDB root = new NexusNodeDB();
            root.DataType = (int)DataType.Raw;
            root.Data = "Your user root";
            root.Name = "Root";
            root.ParentId = null;
            root.Id = Guid.NewGuid().ToString();
            root.ChildrenIds = new List<string>();

            // Assign root
            newUser.rootId = root.Id;

            // Insert both objects into database
            var user_collection = MongoSingleton.Database.GetCollection<User>(TablesSingleton.Users);

            user_collection.InsertOne(newUser);

            var node_collection = MongoSingleton.Database.GetCollection<NexusNodeDB>(TablesSingleton.NexusNodes);

            node_collection.InsertOne(root);

        }
    }
}
