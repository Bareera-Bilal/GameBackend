using MongoDB.Driver;
using P10_WebApi.Models;
using Microsoft.Extensions.Configuration;

namespace P10_WebApi.Services
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;

        // Constructor - takes IConfiguration to get connection info from appsettings.json
        public MongoDbService(IConfiguration configuration)
        {
            // Get the connection string from appsettings.json
            var connectionString = configuration.GetSection("MongoDB:ConnectionString").Value;

            // Get the database name from appsettings.json
            var databaseName = configuration.GetSection("MongoDB:DatabaseName").Value;

            // Create MongoClient and get database
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        // Expose Users collection
        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    }
}

