using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using MongoDB.Driver;

namespace ComputerShop.Server.DataAccess
{
    public class DbConnection : IDbConnection
    {
        private readonly string connectionId = "MongoDB";
        private readonly IMongoDatabase mongoDB;
        public string DbName { get; private set; }
        public string CategoryCollectionName { get; } = "categories";
        public string UserCollectionName { get; } = "users";
        public string ProductCollectionName { get; } = "products";
        public string OrderCollectionName { get; } = "orders";

        public MongoClient MongoClient { get; private set; }
        public IMongoCollection<UserModel> UserCollection { get; private set; }
        public IMongoCollection<Product> ProductCollection { get; private set; }
        public IMongoCollection<OrderModel> OrderCollection { get; private set; }


        public DbConnection(IConfiguration configuration)
        {
            var x = configuration.GetConnectionString(connectionId);
            MongoClient = new MongoClient(x);
            DbName = configuration.GetValue("DatabaseName", "computershop");
            mongoDB = MongoClient.GetDatabase(DbName);

            UserCollection = mongoDB.GetCollection<UserModel>(UserCollectionName);
            ProductCollection = mongoDB.GetCollection<Product>(ProductCollectionName);
            OrderCollection = mongoDB.GetCollection<OrderModel>(OrderCollectionName);
        }
    }
}
