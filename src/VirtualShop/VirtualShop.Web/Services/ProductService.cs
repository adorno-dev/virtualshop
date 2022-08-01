using System.Text;
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
        private ProductViewModel? productViewModel = null;
        private IEnumerable<ProductViewModel>? productsViewModel = null;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            this.jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<ProductViewModel>?> GetAllProducts()
        {
            using var client = httpClientFactory.CreateClient("Products.API");

            using (var response = await client.GetAsync(API_ENDPOINT))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStreamAsync();
                    productsViewModel = await JsonSerializer.DeserializeAsync<IEnumerable<ProductViewModel>?>(content, jsonSerializerOptions);
                } else 
                    return null;
            }

            return productsViewModel;
        }

        public async Task<ProductViewModel?> FindProductById(int id)
        {
            using var client = httpClientFactory.CreateClient("Products.API");

            using (var response = await client.GetAsync($"{API_ENDPOINT}/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStreamAsync();
                    productViewModel = await JsonSerializer.DeserializeAsync<ProductViewModel?>(content, jsonSerializerOptions);
                } else 
                    return null;
            }

            return productViewModel;
        }

        public async Task<ProductViewModel?> CreateProduct(ProductViewModel product)
        {
            var jsonProduct = JsonSerializer.Serialize(product);
            var stringContent = new StringContent(jsonProduct, Encoding.UTF8, "application/json");

            using var client = httpClientFactory.CreateClient("Products.API");
            using (var response = await client.PostAsync(API_ENDPOINT, stringContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content  = await response.Content.ReadAsStreamAsync();
                    productViewModel = await JsonSerializer.DeserializeAsync<ProductViewModel?>(content, jsonSerializerOptions);
                } else 
                    return null;
            }

            return productViewModel;
        }

        public async Task<ProductViewModel?> UpdateProduct(ProductViewModel product)
        {
            var jsonProduct = JsonSerializer.Serialize(product);

            using var client = httpClientFactory.CreateClient("Products.API");
            using (var response = await client.PutAsJsonAsync(API_ENDPOINT, product))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content  = await response.Content.ReadAsStreamAsync();
                    productViewModel = await JsonSerializer.DeserializeAsync<ProductViewModel?>(content, jsonSerializerOptions);
                } else 
                    return null;
            }

            return productViewModel;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            using var client = httpClientFactory.CreateClient("Products.API");
            using (var response = await client.DeleteAsync($"{API_ENDPOINT}/{id}"))
                return response.IsSuccessStatusCode;
        }
    }
}
