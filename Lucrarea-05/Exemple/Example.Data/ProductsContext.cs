using Example.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Example.Data
{
    public class ProductsContext: DbContext
    {
        public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
        {
        }

        public DbSet<ProductDto> Products { get; set; }

        public DbSet<OrderHeaderDto> OrderHeaders { get; set; }

        public DbSet<OrderLineDto> OrderLines { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderHeaderDto>().ToTable("OrderHeader").HasKey(s => s.OrderHeaderId);
            modelBuilder.Entity<ProductDto>().ToTable("Product").HasKey(s => s.ProductId);
            modelBuilder.Entity<OrderLineDto>().ToTable("OrderLine").HasKey(s => s.OrderLineId);
        }
    }
}
