using VirtualShop.Web.Models;

namespace VirtualShop.Web.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>?> GetAllProducts(string token);
        Task<ProductViewModel?> FindProductById(int id, string token);
        Task<ProductViewModel?> CreateProduct(ProductViewModel product, string token);
        Task<ProductViewModel?> UpdateProduct(ProductViewModel product, string token);
        Task<bool> DeleteProduct(int id, string token);
    }
}
