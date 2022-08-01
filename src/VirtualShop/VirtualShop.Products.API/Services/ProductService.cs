using AutoMapper;
using VirtualShop.Products.API.DTOs;
using VirtualShop.Products.API.Models;
using VirtualShop.Products.API.Repositories.Contracts;
using VirtualShop.Products.API.Services.Contracts;

namespace VirtualShop.Products.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var products = await repository.GetAll();
            return mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            var product = await repository.GetById(id);
            return mapper.Map<ProductDTO>(product);
        }

        public async Task AddProduct(ProductDTO productDTO)
        {
            var product = mapper.Map<Product>(productDTO);
            await repository.Create(product);
            productDTO.Id = product.Id;
        }

        public async Task UpdateProduct(ProductDTO productDTO)
        {
            var product = mapper.Map<Product>(productDTO);
            await repository.Update(product);
        }

        public async Task RemoveProduct(int id)
        {
            var product = await repository.GetById(id);
            await repository.Delete(product.Id);
        }
    }
}