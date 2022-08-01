using VirtualShop.Products.API.DTOs;

namespace VirtualShop.Products.API.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();        
        Task<ProductDTO> GetProductById(int id);
        Task AddProduct(ProductDTO productDTO);
        Task UpdateProduct(ProductDTO productDTO);
        Task RemoveProduct(int id);
    }
}