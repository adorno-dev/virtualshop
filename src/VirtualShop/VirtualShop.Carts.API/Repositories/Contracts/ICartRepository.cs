using VirtualShop.Carts.API.DTOs;

namespace VirtualShop.Carts.API.Repositories.Contracts
{
    public interface ICartRepository
    {
        Task<CartDTO> GetCartByUserIdAsync(string userId);
        Task<CartDTO> UpdateCartAsync(CartDTO cartDTO);
        Task<bool> CleanCartAsync(string userId);
        Task<bool> DeleteCouponAsync(string userId);
        
        Task<bool> ApplyCouponAsync(string userId, string couponCode);
        Task<bool> DeleteItemCartAsync(int cartItemId);
    }
}