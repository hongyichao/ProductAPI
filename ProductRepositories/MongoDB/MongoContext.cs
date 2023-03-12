using MongoDB.Driver;
using ProductRepositories.MongoDB.DataModels;

namespace ProductRepositories.MongoDB
{
    public class MongoContext : MongoClient
    {
        public IMongoCollection<Product> Products { get; set; }

        public MongoContext(string connectionString) : base(connectionString)
        {
            var database = GetDatabase("local");
            Products = database.GetCollection<Product>("products");
        }
    }
}
