using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.DAO {
    public class RMSDBContext :DbContext{
        public RMSDBContext(DbContextOptions<RMSDBContext> options) : base(options) {
        }

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
    }
}
