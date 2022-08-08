using Microsoft.EntityFrameworkCore;
using VirtualShop.Discount.API.Models;

namespace VirtualShop.Discount.API.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Coupon> Coupons => Set<Coupon>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Coupon>()
              .HasData(
                new Coupon { Id = 1, CouponCode = "VS_PROMO_10", Discount = 10 },
                new Coupon { Id = 2, CouponCode = "VS_PROMO_20", Discount = 20 }
              );
        }
    }
}