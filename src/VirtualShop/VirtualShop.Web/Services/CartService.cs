using System.Text;
using System.Text.Json;
using VirtualShop.Web.Models;
using VirtualShop.Web.Services.Contracts;

namespace VirtualShop.Web.Services
{
    public class CartService : ICartService
    {
        private const string API_ENDPOINT = "/api/cart";
        private readonly IHttpClientFactory httpClientFactory;
        private readonly JsonSerializerOptions? jsonSerializerOptions;
        private CartViewModel? cartViewModel = null;

        public CartService(IHttpClientFactory clientFactory)
        {
            httpClientFactory = clientFactory;
            jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<CartViewModel?> GetCartByUserIdAsync(string userId)
        {
            var client = httpClientFactory.CreateClient("Carts.API");

            using (var response = await client.GetAsync($"{API_ENDPOINT}/GetCart/{userId}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStreamAsync();
                    cartViewModel = await JsonSerializer.DeserializeAsync<CartViewModel?>(content, jsonSerializerOptions);
                }
                else
                    return null;
            }
            return cartViewModel;
        }

        public async Task<CartViewModel?> AddItemToCartAsync(CartViewModel cartViewModel)
        {
            var client = httpClientFactory.CreateClient("Carts.API");

            var stringContent = new StringContent(JsonSerializer.Serialize(cartViewModel), Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync($"{API_ENDPOINT}/AddCart/", stringContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStreamAsync();
                    this.cartViewModel = await JsonSerializer.DeserializeAsync<CartViewModel?>(content, jsonSerializerOptions);
                }
                else
                    return null;
            }
            return this.cartViewModel;
        }

        public async Task<CartViewModel?> UpdateCartAsync(CartViewModel cartViewModel)
        {
            var client = httpClientFactory.CreateClient("Carts.API");

            using (var response = await client.PutAsJsonAsync($"{API_ENDPOINT}/UpdateCart/", cartViewModel))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    this.cartViewModel = await JsonSerializer.DeserializeAsync<CartViewModel>(apiResponse, jsonSerializerOptions);
                }
                else
                    return null;
            }
            return this.cartViewModel;
        }

        public async Task<bool> RemoveItemFromCartAsync(int cartId)
        {
            var client = httpClientFactory.CreateClient("Carts.API");

            using (var response = await client.DeleteAsync($"{API_ENDPOINT}/DeleteCart/" + cartId))
            {
                if (response.IsSuccessStatusCode)
                    return true;
            }
            return false;
        }

        public Task<bool> ClearCartAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ApplyCouponAsync(CartViewModel cartViewModel, string couponCode)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveCouponAsync(string userId)
        {
            throw new NotImplementedException();
        }
        
        public Task<CartViewModel?> CheckoutAsync(CartHeaderViewModel cartHeaderViewModel)
        {
            throw new NotImplementedException();
        }
    }
}