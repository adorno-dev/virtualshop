using AutoMapper;
using VirtualShop.Products.API.DTOs;
using VirtualShop.Products.API.Models;
using VirtualShop.Products.API.Repositories.Contracts;
using VirtualShop.Products.API.Services.Contracts;

namespace VirtualShop.Products.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository repository;
        private readonly IMapper mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var categories = await repository.GetAll();
            return mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesProducts()
        {
            var categories = await repository.GetCategoriesProducts();
            return mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetCategoryById(int id)
        {
            var category = await repository.GetById(id);
            return mapper.Map<CategoryDTO>(category);
        }

        public async Task AddCategory(CategoryDTO categoryDTO)
        {
            var category = mapper.Map<Category>(categoryDTO);
            await repository.Create(category);
            categoryDTO.Id = category.Id;
        }

        public async Task UpdateCategory(CategoryDTO categoryDTO)
        {
            var category = mapper.Map<Category>(categoryDTO);
            await repository.Update(category);
        }

        public async Task RemoveCategory(int id)
        {
            var category = await repository.GetById(id);
            await repository.Delete(category.Id);
        }
    }
}