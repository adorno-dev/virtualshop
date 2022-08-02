using System.Net.Http.Headers;
using System.Text.Json;
using VirtualShop.Web.Models;
using VirtualShop.Web.Services.Contracts;

namespace VirtualShop.Web.Services
{
    public class CategoryService : ICategoryService
    {
        private const string API_ENDPOINT = "/api/categories";
        private readonly IHttpClientFactory httpClientFactory;
        private readonly JsonSerializerOptions jsonSerializerOptions;
        private IEnumerable<CategoryViewModel>? categoriesViewModel = null;

        public CategoryService(IHttpClientFactory httpClientFactory)
        {
            this.jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<CategoryViewModel>?> GetAllCategories(string token)
        {
            using var client = httpClientFactory.CreateClient("Products.API");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            using (var response = await client.GetAsync(API_ENDPOINT))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStreamAsync();
                    categoriesViewModel = await JsonSerializer.DeserializeAsync<IEnumerable<CategoryViewModel>?>(content, jsonSerializerOptions);
                } else 
                    return null;
            }

            return categoriesViewModel;
        }
    }
}