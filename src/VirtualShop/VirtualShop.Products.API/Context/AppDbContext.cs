using Microsoft.EntityFrameworkCore;
using VirtualShop.Products.API.Models;

namespace VirtualShop.Products.API.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) {}

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            // Categories
            mb.Entity<Category>()
              .ToTable("categories")
              .HasKey(c => c.Id);
            
            mb.Entity<Category>()
              .Property(c => c.Name)
              .HasMaxLength(100)
              .IsRequired();
            
            // Products
            mb.Entity<Product>()
              .ToTable("products")
              .Property(c => c.Name)
              .HasMaxLength(100)
              .IsRequired();

            mb.Entity<Product>()
              .Property(c => c.Description)
              .HasMaxLength(255)
              .IsRequired();

            mb.Entity<Product>()
              .Property(c => c.ImageURL)
              .HasMaxLength(255)
              .IsRequired();

            mb.Entity<Product>()
              .Property(c => c.Price)
              .HasPrecision(12, 2);
            
            // Relationship
            mb.Entity<Category>()
              .HasMany(p => p.Products)
              .WithOne(c => c.Category)
              .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);
            
            // Seed
            mb.Entity<Category>()
              .HasData(
                new Category {Id=1, Name="Material Escolar"},
                new Category {Id=2, Name="Acessorios"}
              );
        }
    }
}