using Microsoft.EntityFrameworkCore;
using VirtualShop.Products.API.Models;

namespace VirtualShop.Products.API.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) {}

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
    }
}