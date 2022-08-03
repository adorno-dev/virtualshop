using Microsoft.EntityFrameworkCore;
using VirtualShop.Carts.API.Models;

namespace VirtualShop.Carts.API.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Product> Products => Set<Product>();
        public DbSet<CartHeader> CartHeaders => Set<CartHeader>();
        public DbSet<CartItem> CartItems => Set<CartItem>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            //Product
            mb.Entity<Product>()
              .HasKey(c => c.Id);

            mb.Entity<Product>()
              .Property(c => c.Id)
              .ValueGeneratedNever();

            mb.Entity<Product>()
              .Property(c => c.Name)
              .HasMaxLength(100)
              .IsRequired();

            mb.Entity<Product>()
              .Property(c => c.Description)
              .HasMaxLength(800)
              .IsRequired();

            mb.Entity<Product>()
              .Property(c => c.ImageURL)
              .HasMaxLength(255)
              .IsRequired();

            mb.Entity<Product>()
              .Property(c => c.CategoryName)
              .HasMaxLength(100)
              .IsRequired();

            mb.Entity<Product>()
              .Property(c => c.Price)
              .HasPrecision(12, 2);

            //CartHeader
            mb.Entity<CartHeader>()
              .Property(c => c.UserId)
              .HasMaxLength(255)
              .IsRequired();

            mb.Entity<CartHeader>()
              .Property(c => c.CouponCode)
              .HasMaxLength(100);
        }
    }
}