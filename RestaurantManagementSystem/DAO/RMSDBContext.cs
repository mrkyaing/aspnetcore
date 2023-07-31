using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.DAO {
    public class RMSDBContext :DbContext{
        public RMSDBContext(DbContextOptions<RMSDBContext> options) : base(options) {
        }

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<PositionEntity> Positions { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<TableEntity>   Tables { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderDetailEntity> OrderDetails { get; set; }
        public DbSet<InvoiceEntity> Invoices { get; set; }
    }
}
