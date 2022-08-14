using System.Text.Json;
using VirtualShop.Web.Models;
using VirtualShop.Web.Services.Contracts;

namespace VirtualShop.Web.Services
{
    public class CouponService : ICouponService
    {
        private const string API_ENDPOINT = "/api/coupon";
        private readonly IHttpClientFactory httpClientFactory;
        private readonly JsonSerializerOptions jsonSerializerOptions;
        private CouponViewModel? couponViewModel = null;

        public CouponService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            this.jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<CouponViewModel?> GetDiscountCoupon(string couponCode)
        {
            var client = httpClientFactory.CreateClient("Discount.API");

            using (var response = await client.GetAsync($"{API_ENDPOINT}/{couponCode}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStreamAsync();

                    couponViewModel = await JsonSerializer.DeserializeAsync<CouponViewModel>(content, jsonSerializerOptions);
                }
            }

            return couponViewModel;
        }
    }
}