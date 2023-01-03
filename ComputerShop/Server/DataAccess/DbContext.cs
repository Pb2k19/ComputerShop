using ComputerShop.Shared.Models.Products;
using ComputerShop.Shared.Models.User;
using Microsoft.EntityFrameworkCore;

namespace ComputerShop.Server.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext MongoClient { get; private set; }
        public DbSet<UserModel> Users { get; private set; }
        public DbSet<Product> Products { get; private set; }
        public DbSet<OrderModel> Orders { get; private set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        
    }
}
