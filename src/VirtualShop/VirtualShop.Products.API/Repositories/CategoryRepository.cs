using Microsoft.EntityFrameworkCore;
using VirtualShop.Products.API.Context;
using VirtualShop.Products.API.Models;
using VirtualShop.Products.API.Repositories.Contracts;

namespace VirtualShop.Products.API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext context;

        public CategoryRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesProducts()
        {
            return await context.Categories.Include(p => p.Products).AsNoTracking().ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return await context.Categories.AsNoTracking().FirstAsync(w => w.Id.Equals(id));
        }

        public async Task<Category> Create(Category category)
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> Update(Category category)
        {
            context.Entry<Category>(category).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> Delete(int id)
        {
            var category = await GetById(id);
            context.Categories.Remove(category);
            return category;
        }
    }
}