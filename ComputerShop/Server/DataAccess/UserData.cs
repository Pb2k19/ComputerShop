using ComputerShop.Shared.Models.User;
using MongoDB.Driver;

namespace ComputerShop.Server.DataAccess
{
    public class UserData : IUserData
    {
        private readonly IMongoCollection<UserModel> users;

        public UserData(IDbConnection dbConnection)
        {
            users = dbConnection.UserCollection;
        }
        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            return (await users.FindAsync(_ => true)).ToList();
        }
        public async Task<UserModel> GetUserByIdAsync(string id)
        {
            return (await users.FindAsync(x => x.Id.Equals(id))).FirstOrDefault();
        }
        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            return (await users.FindAsync(x => x.Email.Equals(email))).FirstOrDefault();
        }
        public Task CreateUser(UserModel user)
        {
            return users.InsertOneAsync(user);
        }
        public Task UpdateUserAsync(UserModel user)
        {
            FilterDefinition<UserModel>? filter = Builders<UserModel>.Filter.Eq(nameof(UserModel.Id), user.Id);
            return users.ReplaceOneAsync(filter, user, new ReplaceOptions { IsUpsert = true });
        }
        public async Task<OrderModel?> GetOrderAsync(string id)
        {
            return (await GetAllUsersAsync()).Select(u => u.Orders.FirstOrDefault(o => o.Id.Equals(id))).FirstOrDefault();
        }
        public async Task<OrderModel?> GetOrderAsync(string orderId, string userId)
        {
            return (await GetUserByIdAsync(userId)).Orders.FirstOrDefault(o => o.Id.Equals(orderId));
        }
        public async Task UpdateOrderAsync(OrderModel order)
        {
            var user = (await GetAllUsersAsync()).FirstOrDefault(u => u.Orders.Any(o => o.Id.Equals(order.Id)));
            if (user == null)
                throw new Exception("Order not found");
            await UpdateOrderAsync(order, user.Id);
        }
        public async Task UpdateOrderAsync(OrderModel order, string userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");
            int index = user.Orders.IndexOf(user.Orders.FirstOrDefault(order => order.Id.Equals(order.Id)));
            if (index == -1)
                user.Orders.Add(order);
            else
                user.Orders[index] = order;
            await UpdateUserAsync(user);
        }
    }
}
