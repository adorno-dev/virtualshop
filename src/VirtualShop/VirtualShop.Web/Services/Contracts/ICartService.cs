using VirtualShop.Web.Models;

namespace VirtualShop.Web.Services.Contracts
{
    public interface ICartService
    {
        Task<CartViewModel?> GetCartByUserIdAsync(string userId);
        Task<CartViewModel?> AddItemToCartAsync(CartViewModel cartViewModel);
        Task<CartViewModel?> UpdateCartAsync(CartViewModel cartViewModel);
        Task<bool> RemoveItemFromCartAsync(int cartId);

        Task<bool> ApplyCouponAsync(CartViewModel cartViewModel);
        Task<bool> RemoveCouponAsync(string userId);
        Task<bool> ClearCartAsync(string userId);

        Task<CartHeaderViewModel?> CheckoutAsync(CartHeaderViewModel cartHeaderViewModel);
    }
}