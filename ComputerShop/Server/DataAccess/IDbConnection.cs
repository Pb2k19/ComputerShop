using ComputerShop.Shared.Models;
using ComputerShop.Shared.Models.User;
using MongoDB.Driver;

namespace ComputerShop.Server.DataAccess
{
    public interface IDbConnection
    {
        IMongoCollection<Category> CategoryCollection { get; }
        string CategoryCollectionName { get; }
        string DbName { get; }
        MongoClient MongoClient { get; }
        IMongoCollection<OrderModel> OrderCollection { get; }
        string OrderCollectionName { get; }
        IMongoCollection<Product> ProductCollection { get; }
        string ProductCollectionName { get; }
        IMongoCollection<UserModel> UserCollection { get; }
        string UserCollectionName { get; }
    }
}