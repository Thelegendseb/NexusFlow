using MongoDB.Driver;

namespace API.Singletons
{
    public static class MongoSingleton
    {
        private static readonly IMongoClient _client;
        private static readonly IMongoDatabase _database;

        static MongoSingleton()
        {
            var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>()
    .Build();

            string connectionString = configuration["DBConnectionString"];
            string databaseString = configuration["DBDatabaseString"];
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseString);
        }

        public static IMongoClient Client
        {
            get { return _client; }
        }

        public static IMongoDatabase Database
        {
            get { return _database; }
        }
    }
}
