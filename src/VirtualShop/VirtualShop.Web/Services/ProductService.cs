using System.Text.Json;
using VirtualShop.Web.Models;
using VirtualShop.Web.Services.Contracts;

namespace VirtualShop.Web.Services
{
    public class ProductService : IProductService
    {
        private const string API_ENDPOINT = "/api/products";
        private readonly JsonSerializerOptions jsonSerializerOptions;
        private readonly IHttpClientFactory httpClientFactory;
        private ProductViewModel? productViewModel;
        private IEnumerable<ProductViewModel>? productsViewModel;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            this.jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            this.httpClientFactory = httpClientFactory;
        }

        public Task<IEnumerable<ProductViewModel>> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Task<ProductViewModel> FindProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductViewModel> CreateProduct(ProductViewModel product)
        {
            throw new NotImplementedException();
        }

        public Task<ProductViewModel> UpdateProduct(ProductViewModel product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
}
