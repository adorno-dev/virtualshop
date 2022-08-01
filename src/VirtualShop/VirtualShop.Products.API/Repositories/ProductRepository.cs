using Microsoft.EntityFrameworkCore;
using VirtualShop.Products.API.Context;
using VirtualShop.Products.API.Models;
using VirtualShop.Products.API.Repositories.Contracts;

namespace VirtualShop.Products.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext context;

        public ProductRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await context.Products.Include(c => c.Category).AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await context.Products.Include(c => c.Category).AsNoTracking().FirstAsync(w => w.Id.Equals(id));
        }

        public async Task<Product> Create(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            context.Entry<Product>(product).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Delete(int id)
        {
            var product = await GetById(id);
            context.Products.Remove(product);
            return product;
        }
    }
}