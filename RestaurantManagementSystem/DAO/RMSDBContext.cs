using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.DAO {
    public class RMSDBContext :DbContext{
        public RMSDBContext(DbContextOptions<RMSDBContext> options) : base(options) {
        }

        public DbSet<Product> Products { get; set; }
    }
}
