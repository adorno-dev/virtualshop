using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VirtualShop.Carts.API.Context;
using VirtualShop.Carts.API.DTOs;
using VirtualShop.Carts.API.Models;
using VirtualShop.Carts.API.Repositories.Contracts;

namespace VirtualShop.Carts.API.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        private async Task PersistProductInDatabase(CartDTO cartDTO, Cart cart)
        {
            var productId = cartDTO.CartItems.Select(s => s.ProductId).FirstOrDefault();
            var storedProduct = await context.Products.FindAsync(productId);

            if (storedProduct is null) {
                var product = cart.CartItems.Select(s => s.Product).FirstOrDefault();
                if (product is not null) {
                    context.Products.Add(product);
                    await context.SaveChangesAsync();
                }
            }
        }

        private async Task CreateCartHeaderAndItems(Cart cart)
        {
            context.CartHeaders.Add(cart.CartHeader);

            await context.SaveChangesAsync();

            var cartItem = cart.CartItems.First();
            cartItem.CartHeaderId = cart.CartHeader.Id;
            cartItem.Product = null;

            context.CartItems.Add(cartItem);

            await context.SaveChangesAsync();
        }

        private async Task UpdateQuantityAndItems(CartDTO cartDTO, Cart cart, CartHeader cartHeader)
        {
            var cartItemDTO = cartDTO.CartItems.First();

            var cartDetail = await context.CartItems.AsNoTracking().FirstOrDefaultAsync(w => 
                w.ProductId.Equals(cartItemDTO.ProductId) && 
                w.CartHeaderId.Equals(cartHeader.Id));
            
            var cartItem = cart.CartItems.First();

            if (cartDetail is null) {    
                cartItem.CartHeaderId = cartHeader.Id;
                cartItem.Product = null;
                context.CartItems.Add(cartItem);
                await context.SaveChangesAsync();
            } else {
                cartItem.Product = null;
                cartItem.Id = cartDetail.Id;
                cartItem.Quantity += cartDetail.Quantity;
                cartItem.CartHeaderId = cartDetail.CartHeaderId;
                context.CartItems.Update(cartItem);
                await context.SaveChangesAsync();
            }
        }

        public CartRepository(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CartDTO> GetCartByUserIdAsync(string userId)
        {
            var cart = new Cart();
            cart.CartHeader = await context.CartHeaders.FirstAsync(x => x.UserId.Equals(userId));
            cart.CartItems = context.CartItems.Where(c => c.CartHeaderId.Equals(cart.CartHeader.Id)).Include(c => c.Product);
            return mapper.Map<CartDTO>(cart);
        }

        public async Task<CartDTO> UpdateCartAsync(CartDTO cartDTO)
        {
            var cart = mapper.Map<Cart>(cartDTO);

            // Persists the product just in case
            await PersistProductInDatabase(cartDTO, cart);

            var cartHeader = await context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(w => w.UserId.Equals(cart.CartHeader.UserId));

            if (cartHeader is null)
                await CreateCartHeaderAndItems(cart);
            else
                await UpdateQuantityAndItems(cartDTO, cart, cartHeader);
            
            return mapper.Map<CartDTO>(cart);
        }

        public async Task<bool> CleanCartAsync(string userId)
        {
            var cartHeader = await context.CartHeaders.FirstOrDefaultAsync(w => w.UserId.Equals(userId));

            if (cartHeader is null)
                return false;
            
            var cartItems = await context.CartItems.Where(w => w.CartHeaderId.Equals(cartHeader.Id)).ToListAsync();

            if (cartItems is not null)
                context.CartItems.RemoveRange(cartItems);

            context.CartHeaders.Remove(cartHeader);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteItemCartAsync(int cartItemId)
        {
            var cartItem = await context.CartItems.FirstOrDefaultAsync(w => w.Id.Equals(cartItemId));

            if (cartItem is null)
                return false;

            var cartItemsCount = context.CartItems.Where(w => w.CartHeaderId.Equals(cartItem.CartHeaderId)).Count();

            // Removes the cart if there was only one item.
            if (cartItemsCount.Equals(1)) {
                var cartHeader = await context.CartHeaders.FirstAsync(w => w.Id.Equals(cartItem.CartHeaderId));
                context.CartItems.Remove(cartItem);
                context.CartHeaders.Remove(cartHeader);
            }
            else context.CartItems.Remove(cartItem);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ApplyCouponAsync(string userId, string couponCode)
        {
            var cartHeaderApplyCoupon = await context.CartHeaders.FirstOrDefaultAsync(c => c.UserId.Equals(userId));

            if (cartHeaderApplyCoupon is null)
                return false;
            
            cartHeaderApplyCoupon.CouponCode = couponCode;

            context.CartHeaders.Update(cartHeaderApplyCoupon);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCouponAsync(string userId)
        {
            var cartHeaderApplyCoupon = await context.CartHeaders.FirstOrDefaultAsync(c => c.UserId.Equals(userId));

            if (cartHeaderApplyCoupon is null)
                return false;
            
            cartHeaderApplyCoupon.CouponCode = string.Empty;

            context.CartHeaders.Update(cartHeaderApplyCoupon);

            await context.SaveChangesAsync();

            return true;
        }
    }
}