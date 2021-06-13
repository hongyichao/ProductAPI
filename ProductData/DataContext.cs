using Microsoft.EntityFrameworkCore;
using ProductEntity;

namespace ProductData
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
