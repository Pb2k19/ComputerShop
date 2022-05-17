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
        public Task UpdateUser(UserModel user)
        {
            FilterDefinition<UserModel>? filter = Builders<UserModel>.Filter.Eq(nameof(UserModel.Id), user.Id);
            return users.ReplaceOneAsync(filter, user, new ReplaceOptions { IsUpsert = true });
        }
    }
}
