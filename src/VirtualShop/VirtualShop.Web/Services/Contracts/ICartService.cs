using VirtualShop.Web.Models;

namespace VirtualShop.Web.Services.Contracts
{
    public interface ICartService
    {
        Task<CartViewModel?> GetCartByUserIdAsync(string userId, string token);
        Task<CartViewModel?> AddItemToCartAsync(CartViewModel cartViewModel, string token);
        Task<CartViewModel?> UpdateCartAsync(CartViewModel cartViewModel, string token);
        Task<bool> RemoveItemFromCartAsync(int cartId, string token);

        Task<bool> ApplyCouponAsync(CartViewModel cartViewModel, string couponCode, string token);
        Task<bool> RemoveCouponAsync(string userId, string token);
        Task<bool> ClearCartAsync(string userId, string token);

        Task<CartViewModel?> CheckoutAsync(CartHeaderViewModel cartHeaderViewModel, string token);
    }
}