using ComputerShop.Shared.Models.User;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ComputerShop.Server.DataAccess
{
    public class UserData : IUserData
    {
        string connectionString;

        public UserData(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            string query =
            """ 
            Select * from [Sklep].[dbo].[UserModel]
            """;

            DataTable table = new();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);
            using var dataAdapter = new SqlDataAdapter(command);

            command.CommandType = CommandType.Text;
            await Task.Run(() => dataAdapter.Fill(table));

            return GetUsers(table);
        }
        public async Task<UserModel> GetUserByIdAsync(string id)
        {
            string query =
            $""" 
            exec user_by_id {id}
            """;

            DataTable table = new();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);
            using var dataAdapter = new SqlDataAdapter(command);

            command.CommandType = CommandType.Text;
            await Task.Run(() => dataAdapter.Fill(table));

            return GetUser(table);
        }
        public Task CreateUser(UserModel user)
        {
            string query =
            $""" 
            exec create_user {user.Email}, {user.CreationDate}
            """;

            DataTable table = new();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);
            using var dataAdapter = new SqlDataAdapter(command);

            command.CommandType = CommandType.Text;
            return Task.Run(() => dataAdapter.Fill(table));
        }
        public Task UpdateUserAsync(UserModel user)
        {
            string query =
            $""" 
            exec update_user {user.Id}, {user.Email}, {user.CreationDate}
            """;

            DataTable table = new();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);
            using var dataAdapter = new SqlDataAdapter(command);

            command.CommandType = CommandType.Text;
            return Task.Run(() => dataAdapter.Fill(table));
        }

        public async Task<RegisteredUser?> GetRegisteredUserByEmailAsync(string email)
        {
            string query =
            $""" 
            exec get_registered_user_by_email {email}
            """;

            DataTable table = new();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);
            using var dataAdapter = new SqlDataAdapter(command);

            command.CommandType = CommandType.Text;
            await Task.Run(() => dataAdapter.Fill(table));

            return GetRegisteredUser(table);
        }

        public async Task<RegisteredUser?> GetAnyUserByEmailAsync(string email)
        {
            string query =
            $""" 
            exec get_any_user_by_email {email}
            """;

            DataTable table = new();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);
            using var dataAdapter = new SqlDataAdapter(command);

            command.CommandType = CommandType.Text;
            await Task.Run(() => dataAdapter.Fill(table));

            return GetRegisteredUser(table);
        }

        public async Task<RegisteredUser?> GetRegisteredUserByIdAsync(string id)
        {
            string query =
            $""" 
            exec get_registered_user_by_id {id}
            """;

            DataTable table = new();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);
            using var dataAdapter = new SqlDataAdapter(command);

            command.CommandType = CommandType.Text;
            await Task.Run(() => dataAdapter.Fill(table));

            return GetRegisteredUser(table);
        }

        public async Task<OrderModel?> GetOrderAsync(string id)
        {
            return (await GetAllUsersAsync()).Select(u => u.Orders.FirstOrDefault(o => o.Id.Equals(id))).FirstOrDefault();
        }
        public async Task<OrderModel?> GetOrderAsync(string orderId, string userId)
        {
            return (await GetUserByIdAsync(userId)).Orders.FirstOrDefault(o => o.Id.Equals(orderId));
        }
        public async Task<OrderModel?> GetFirstUnpaidOrderAsync(string userId)
        {
            return (await GetUserByIdAsync(userId)).Orders.OrderByDescending(o => o.OrderDate).FirstOrDefault(o => o.State == OrderStates.Unpaid);
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


        private List<UserModel> GetUsers(DataTable data)
        {
            UserModel user = new();
            List<UserModel> users = new();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                user = new()
                {
                    Orders = new(), //tmp

                    CreationDate = DateTime.Parse(data.Rows[i]["CreationDate"].ToString()),
                    Email = data.Rows[i]["Email"].ToString(),
                    Id = data.Rows[i]["id"].ToString()
                };
                users.Add(user);
            }

            return users;
        }

        private UserModel GetUser(DataTable data)
        {
            UserModel user = new()
            {
                Orders = new(), //tmp

                CreationDate = DateTime.Parse(data.Rows[0]["CreationDate"].ToString()),
                Email = data.Rows[0]["Email"].ToString(),
                Id = data.Rows[0]["id"].ToString()
            };

            return user;
        }

        private RegisteredUser GetRegisteredUser(DataTable data)
        {
            RegisteredUser user = new()
            {
                Orders = new(), //tmp
                WishList = new(), //tmp
                DeliveryDetails = new(), //tmp
                InvoiceDetails = new(), //tmp

                Password = data.Rows[0]["password"].ToString(),
                Role = data.Rows[0]["Role"].ToString(),
                CreationDate = DateTime.Parse(data.Rows[0]["CreationDate"].ToString()),
                Email = data.Rows[0]["Email"].ToString(),
                Id = data.Rows[0]["id"].ToString()
            };

            return user;
        }
    }
}
