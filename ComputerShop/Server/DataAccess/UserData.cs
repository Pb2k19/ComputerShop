using ComputerShop.Shared.Models.User;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Text;

namespace ComputerShop.Server.DataAccess
{
    public class UserData : IUserData
    {
        private readonly CultureInfo decimalConvertionInfo = new CultureInfo("en-US");
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

            return await GetUsers(table);
        }
        public async Task<UserModel> GetUserByIdAsync(string id)
        {
            string query =
            $""" 
            exec user_by_id '{id}'
            """;

            DataTable table = new();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);
            using var dataAdapter = new SqlDataAdapter(command);

            command.CommandType = CommandType.Text;
            await Task.Run(() => dataAdapter.Fill(table));

            return await GetUser(table);
        }
        public Task CreateUser(UserModel user)
        {
            string query = string.Empty;
            if (user is RegisteredUser)
            {
                RegisteredUser registeredUser = (RegisteredUser)user;
                query =
                $""" 
                exec create_registered_user '{registeredUser.Email}', '{registeredUser.CreationDate}', '{registeredUser.DeliveryDetails.Name}', '{registeredUser.DeliveryDetails?.PostCode}', '{registeredUser.DeliveryDetails?.City}', '{registeredUser.DeliveryDetails?.Street}',
                '{registeredUser.DeliveryDetails?.HouseNumber}', '{registeredUser.DeliveryDetails?.PhoneNumber}', '{registeredUser.DeliveryDetails?.DeliveryMethod}', '{registeredUser.InvoiceDetails?.IsBusiness}', '{registeredUser.InvoiceDetails?.Nip}', '{registeredUser.Password}', '{registeredUser.Role}'
                """;
            }
            else
            {
                query =
                $""" 
                exec create_user '{user.Email}', '{user.CreationDate}'
                """;
            }

            DataTable table = new();
            return Task.Run(async () =>
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand(query, connection);
                using var dataAdapter = new SqlDataAdapter(command);

                command.CommandType = CommandType.Text;
                dataAdapter.Fill(table);

                await UpdateOrderItems(user);
                if (user is RegisteredUser)
                    await UpdatWishListItemsAsync((RegisteredUser)user);
            });
        }
        public Task UpdateUserAsync(UserModel user)
        {
            string query = string.Empty;

            if(user is RegisteredUser)
            {
                RegisteredUser registeredUser = (RegisteredUser)user;
                query =
                $""" 
                exec update_registered_user '{registeredUser.Id}', '{registeredUser.Email}', '{registeredUser.CreationDate}', '{registeredUser.DeliveryDetails.Name}', '{registeredUser.DeliveryDetails?.PostCode}', '{registeredUser.DeliveryDetails?.City}', '{registeredUser.DeliveryDetails?.Street}',
                '{registeredUser.DeliveryDetails?.HouseNumber}', '{registeredUser.DeliveryDetails?.PhoneNumber}', '{registeredUser.DeliveryDetails?.DeliveryMethod}', '{registeredUser.InvoiceDetails?.IsBusiness}', '{registeredUser.InvoiceDetails?.Nip}', '{registeredUser.Password}', '{registeredUser.Role}'
                """;
            }
            else
            {
                query = 
                $""" 
                exec update_user '{user.Id}', '{user.Email}', '{user.CreationDate}'
                """;
            }

            return Task.Run(async () =>
            {
                DataTable table = new();

                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand(query, connection);
                using var dataAdapter = new SqlDataAdapter(command);

                command.CommandType = CommandType.Text;
                dataAdapter.Fill(table);

                await UpdateOrderItems(user);
                if (user is RegisteredUser)
                    await UpdatWishListItemsAsync((RegisteredUser)user);
            });
        }

        public async Task<RegisteredUser?> GetRegisteredUserByEmailAsync(string email)
        {
            string query =
            $"""
            exec get_registered_user_by_email '{email}'
            """;

            DataTable table = new();
            await Task.Run(() =>
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand(query, connection);
                using var dataAdapter = new SqlDataAdapter(command);

                command.CommandType = CommandType.Text;
                dataAdapter.Fill(table);
            });

            return await GetRegisteredUser(table);
        }

        public async Task<RegisteredUser?> GetAnyUserByEmailAsync(string email)
        {
            string query =
            $""" 
            exec get_any_user_by_email '{email}'
            """;

            DataTable table = new();
            await Task.Run(() =>
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand(query, connection);
                using var dataAdapter = new SqlDataAdapter(command);

                command.CommandType = CommandType.Text;
                dataAdapter.Fill(table);
            });

            return await GetRegisteredUser(table);
        }

        public async Task<RegisteredUser?> GetRegisteredUserByIdAsync(string id)
        {
            string query =
            $""" 
            exec get_registered_user_by_id '{id}'
            """;

            DataTable table = new();

            await Task.Run(() =>
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand(query, connection);
                using var dataAdapter = new SqlDataAdapter(command);

                command.CommandType = CommandType.Text;
                dataAdapter.Fill(table);
            });

            return await GetRegisteredUser(table);
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


        private async Task<List<UserModel>> GetUsers(DataTable data)
        {
            UserModel user;
            List<UserModel> users = new();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                user = new()
                {
                    CreationDate = DateTime.Parse(data.Rows[i]["creation_date"].ToString()),
                    Email = data.Rows[i]["email"].ToString(),
                    Id = data.Rows[i]["um_id"].ToString()
                };
                user.Orders = await GetOrders(user.Id);
                users.Add(user);
            }

            return users;
        }

        private async Task<UserModel> GetUser(DataTable data)
        {
            UserModel user = new()
            {
                CreationDate = DateTime.Parse(data.Rows[0]["creation_date"].ToString()),
                Email = data.Rows[0]["email"].ToString(),
                Id = data.Rows[0]["um_id"].ToString()
            };

            user.Orders = await GetOrders(user.Id);

            return user;
        }

        private async Task<RegisteredUser> GetRegisteredUser(DataTable data)
        {
            RegisteredUser user = new()
            {
                Password = data.Rows[0]["password"].ToString(),
                Role = data.Rows[0]["role"].ToString(),
                CreationDate = DateTime.Parse(data.Rows[0]["creation_date"].ToString()),
                Email = data.Rows[0]["email"].ToString(),
                Id = data.Rows[0]["um_id"].ToString()
            };

            user.WishList = await GetWishList(user.Id);
            user.Orders = await GetOrders(user.Id);
            user.DeliveryDetails = await GetDeliveryDetails(user.Id);
            user.InvoiceDetails = await GetInvoiceDetails(user.Id);

            return user;
        }

        private async Task<List<OrderModel>> GetOrders(string userId)
        {
            string query =
            $""" 
            exec get_orders_by_user '{userId}' 
            """;

            DataTable table = new();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);
            using var dataAdapter = new SqlDataAdapter(command);

            command.CommandType = CommandType.Text;
            await Task.Run(() => dataAdapter.Fill(table));

            List<OrderModel> orders = new();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var order = new OrderModel
                {
                    Id = table.Rows[i]["om_id"].ToString(),
                    DeliveryDetails = await GetDeliveryDetails(userId),                    
                    InvoiceDetails = await GetInvoiceDetails(userId),
                    OrderDate = DateTime.Parse(table.Rows[0]["order_date"].ToString()),
                    State = table.Rows[i]["state"].ToString(),
                    Total = decimal.Parse(table.Rows[0]["total"].ToString(), decimalConvertionInfo)
                };
                order.CartItems = await GetCartItem(order.Id);
                orders.Add(order);
            }

            return orders;
        }

        private async Task<WishListModel> GetWishList(string userId)
        {
            string query =
            $""" 
            exec get_whishlisted_products_by_user '{userId}'
            """;

            DataTable table = new();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);
            using var dataAdapter = new SqlDataAdapter(command);

            command.CommandType = CommandType.Text;
            await Task.Run(() => dataAdapter.Fill(table));

            WishListModel wishlist = new();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                wishlist.List.Add(new WishListItem
                {
                    Date = DateTime.Parse(table.Rows[0]["creation_date"].ToString()),
                    ProductId = table.Rows[i]["id"].ToString()
                });
            }

            return wishlist;
        }

        private async Task<DeliveryDetails> GetDeliveryDetails(string userId)
        {
            string query =
            $""" 
            exec get_delivery_details_by_user '{userId}'
            """;

            DataTable table = new();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);
            using var dataAdapter = new SqlDataAdapter(command);

            command.CommandType = CommandType.Text;
            await Task.Run(() => dataAdapter.Fill(table));

            if (table.Rows.Count == 0)
                return new DeliveryDetails();

            DeliveryDetails deliveryDetails = new()
            {
                City = table.Rows[0]["city"].ToString(),
                DeliveryMethod = table.Rows[0]["delivery_method"].ToString(),
                Email = table.Rows[0]["email"].ToString(),
                HouseNumber = table.Rows[0]["house_number"].ToString(),
                Name = table.Rows[0]["name"].ToString(),
                PaymentMethod = "Przy odbiorze",
                PhoneNumber = table.Rows[0]["phone_number"].ToString(),
                PostCode = table.Rows[0]["post_code"].ToString(),
                Street = table.Rows[0]["street"].ToString()
            };

            return deliveryDetails;
        }

        private async Task<InvoiceDetails> GetInvoiceDetails(string userId)
        {
            string query =
            $""" 
            exec get_invoice_details_by_user '{userId}'
            """;

            DataTable table = new();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);
            using var dataAdapter = new SqlDataAdapter(command);

            command.CommandType = CommandType.Text;
            await Task.Run(() => dataAdapter.Fill(table));

            if (table.Rows.Count == 0)
                return new InvoiceDetails();

            InvoiceDetails deliveryDetails = new()
            {
                City = table.Rows[0]["city"].ToString(),
                Name = table.Rows[0]["name"].ToString(),
                Street = table.Rows[0]["street"].ToString(),
                IsBusiness = Convert.ToBoolean(table.Rows[0]["is_business"].ToString()),
                Nip = table.Rows[0]["nip"].ToString(),
            };

            return deliveryDetails;
        }

        private async Task<List<CartItem>> GetCartItem(string orderId)
        {
            string query =
            $""" 
            exec get_order_item '{orderId}'
            """;

            DataTable table = new();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);
            using var dataAdapter = new SqlDataAdapter(command);

            command.CommandType = CommandType.Text;
            await Task.Run(() => dataAdapter.Fill(table));

            if (table.Rows.Count == 0)
                return new List<CartItem>();

            List<CartItem> items = new();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                items.Add(new CartItem
                {
                    Price = decimal.Parse(table.Rows[0]["price"].ToString(), decimalConvertionInfo),
                    Quantity = 1,
                    ProductId = table.Rows[0]["p_id"].ToString()
                });
            }

            return items;
        }

        public async Task UpdateOrderItems(UserModel user)
        {
            StringBuilder stringBuilder = new();

            foreach (var item in user.Orders)
            {
                stringBuilder.Append(
                """

                """);
            }

            string query = stringBuilder.ToString();

            await Task.Run(() =>
            {
                DataTable table = new();

                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand(query, connection);
                using var dataAdapter = new SqlDataAdapter(command);

                command.CommandType = CommandType.Text;
                dataAdapter.Fill(table);
            });
        }

        public async Task UpdatWishListItemsAsync(RegisteredUser user)
        {
            StringBuilder stringBuilder = new();

            foreach (var item in user.WishList.List)
            {
                stringBuilder.Append(
                """

                """);
            }

            string query = stringBuilder.ToString();

            await Task.Run(() =>
            {
                DataTable table = new();

                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand(query, connection);
                using var dataAdapter = new SqlDataAdapter(command);

                command.CommandType = CommandType.Text;
                dataAdapter.Fill(table);
            });
        }
    }
}
