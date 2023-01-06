using ComputerShop.Shared.Models.Products;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace ComputerShop.Server.DataAccess
{
    public class ProductData : IProductData
    {
        private readonly string connectionString;
        private readonly CultureInfo decimalConvertionInfo = new CultureInfo("en-US");

        public ProductData(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            string query =
            """ 
            exec get_all_products
            """;

            DataTable table = new();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);
            using var dataAdapter = new SqlDataAdapter(command);

            command.CommandType = CommandType.Text;
            await Task.Run(() => dataAdapter.Fill(table));

            return GetProducts(table);
        }
        public async Task<List<Product>> GetAllPublicProductsAsync()
        {
            return (await GetAllProductsAsync()).Where(p => p.IsPublic && !p.IsRemoved).ToList();
        }
        public async Task<List<Product>> GetAllHiddenProductsAsync()
        {
            return (await GetAllProductsAsync()).Where(p => !p.IsPublic).ToList();
        }
        public async Task<List<Product>> GetAllRemovedProductsAsync()
        {
            return (await GetAllProductsAsync()).Where(p => p.IsRemoved).ToList();
        }
        public async Task<Product> GetProductAsync(string id)
        {
            string query =
            $""" 
            exec product_by_id {id}
            """;

            DataTable table = new();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);
            using var dataAdapter = new SqlDataAdapter(command);

            command.CommandType = CommandType.Text;
            await Task.Run(() => dataAdapter.Fill(table));

            return GetProduct(table);
        }
        public async Task<Product> GetPublicProductAsync(string id)
        {
            string query =
            $""" 
            exec product_by_id_and_is_public {id}
            """;

            DataTable table = new();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);
            using var dataAdapter = new SqlDataAdapter(command);

            command.CommandType = CommandType.Text;
            await Task.Run(() => dataAdapter.Fill(table));

            return GetProduct(table);
        }
        public Task AddProductAsync(Product product)
        {
            string query =
            $""" 
            exec insert_product {product.Name}, {product.Manufacturer}, {product.Description}, {product.Category}, {product.Price}, {product.PriceBeforeDiscount}, 
                {product.IsPublic}, {product.IsRemoved}, {product.IsAvaliable}, {product.IsHiglighted}, {product.CareationDate}, {product.LastUpdateDate}, {product.WarantyMonths}
            """;

            DataTable table = new();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);
            using var dataAdapter = new SqlDataAdapter(command);

            command.CommandType = CommandType.Text;
            return Task.Run(() => dataAdapter.Fill(table));
        }
        public async Task UpdateProductAsync(Product product)
        {
            string query =
            $""" 
            exec update_product {product.Name}, {product.Manufacturer}, {product.Description}, {product.Category}, {product.Price}, {product.PriceBeforeDiscount}, 
                {product.IsPublic}, {product.IsRemoved}, {product.IsAvaliable}, {product.IsHiglighted}, {product.CareationDate}, {product.LastUpdateDate}, {product.WarantyMonths}
            """;

            DataTable table = new();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(query, connection);
            using var dataAdapter = new SqlDataAdapter(command);

            command.CommandType = CommandType.Text;
            await Task.Run(() => dataAdapter.Fill(table));
        }

        public List<Product> GetProducts(DataTable data)
        {
            List<Product> products = new();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                products.Add(new Product
                {
                    Manufacturer = data.Rows[i]["manufacturer"].ToString(),
                    Description = data.Rows[i]["description"].ToString(),
                    WarantyMonths = Convert.ToInt32(data.Rows[i]["waranty_months"].ToString()),
                    CareationDate = Convert.ToDateTime(data.Rows[i]["creation_date"].ToString()),
                    Category = data.Rows[i]["category"].ToString(),
                    Id = data.Rows[i]["p_id"].ToString(),
                    Images = new() { new Shared.Models.Image() }, //tmp
                    IsAvaliable = Convert.ToBoolean(data.Rows[i]["is_avaliable"]),
                    IsHiglighted = Convert.ToBoolean(data.Rows[i]["is_highlighted"]),
                    IsPublic = Convert.ToBoolean(data.Rows[i]["is_public"]),
                    IsRemoved = Convert.ToBoolean(data.Rows[i]["is_removed"]),
                    LastUpdateDate = Convert.ToDateTime(data.Rows[i]["last_update_time"].ToString()),
                    Name = data.Rows[i]["Name"].ToString(),
                    Price = decimal.Parse(data.Rows[i]["price"].ToString(), decimalConvertionInfo),
                    PriceBeforeDiscount = decimal.Parse(data.Rows[i]["price_before_discount"].ToString(), decimalConvertionInfo)
                });
            }

            return products;
        }

        private Product? GetProduct(DataTable data)
        {
            if (data.Rows.Count == 0)
                return null;

            return new Product
            {
                Manufacturer = data.Rows[0]["manufacturer"].ToString(),
                Description = data.Rows[0]["description"].ToString(),
                WarantyMonths = Convert.ToInt32(data.Rows[0]["waranty_months"].ToString()),
                CareationDate = Convert.ToDateTime(data.Rows[0]["creation_date"].ToString()),
                Category = data.Rows[0]["category"].ToString(),
                Id = data.Rows[0]["p_id"].ToString(),
                Images = new() { new Shared.Models.Image()}, //tmp
                IsAvaliable = Convert.ToBoolean(data.Rows[0]["is_avaliable"]),
                IsHiglighted = Convert.ToBoolean(data.Rows[0]["is_highlighted"]),
                IsPublic = Convert.ToBoolean(data.Rows[0]["is_public"]),
                IsRemoved = Convert.ToBoolean(data.Rows[0]["is_removed"]),
                LastUpdateDate = Convert.ToDateTime(data.Rows[0]["last_update_time"].ToString()),
                Name = data.Rows[0]["name"].ToString(),
                Price = decimal.Parse(data.Rows[0]["price"].ToString(), decimalConvertionInfo),
                PriceBeforeDiscount = decimal.Parse(data.Rows[0]["price_before_discount"].ToString(), decimalConvertionInfo)
            };
        }
    }
}
