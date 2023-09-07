using CurrencyConvertor.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConvertor.DAO
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
        {
        }

        public DbSet<CustomerEntity> Customers { get; set; }
    }
}
