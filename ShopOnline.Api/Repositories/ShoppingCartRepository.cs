using Microsoft.EntityFrameworkCore;
using ShopOnline.Api.Data;
using ShopOnline.Api.Entities;
using ShopOnline.Api.Repositories.Contracts;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Api.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShopOnlineDbContext _dbContext;
        public ShoppingCartRepository(ShopOnlineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private async Task<bool> CartItemExists(int cartId, int productId)
        {
            return await _dbContext.CartItems.AnyAsync(x => x.CartId == cartId && x.ProductId == productId);
        }

        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            if (await CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId) == false)
            {
                var item = await (from product in _dbContext.Products
                                  where product.Id == cartItemToAddDto.ProductId
                                  select new CartItem
                                  {
                                      CartId = cartItemToAddDto.CartId,
                                      ProductId = product.Id,
                                      Qty = cartItemToAddDto.Qty
                                  }).SingleOrDefaultAsync();
                if (item != null)
                {
                    var result = await _dbContext.CartItems.AddAsync(item);
                    await _dbContext.SaveChangesAsync();
                    return result.Entity;
                }
            }
            return null;
        }

        public Task<CartItem> DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CartItem> GetItem(int id)
        {
            return await (from cart in _dbContext.Carts
                         join cartItem in _dbContext.CartItems
                         on cart.Id equals cartItem.CartId
                         where cartItem.Id == id
                         select new CartItem
                         {
                             Id = cartItem.Id,
                             ProductId = cartItem.ProductId,
                             Qty = cartItem.Qty,
                             CartId = cartItem.CartId
                         }).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CartItem>> GetItems(int userId)
        {
            return await(from cart in  _dbContext.Carts
                         join cartItem in _dbContext. CartItems
                         on cart.Id equals cartItem.CartId
                         where cart.UserId == userId
                         select new CartItem
                         {
                             Id = cartItem.Id,
                             ProductId = cartItem.ProductId,
                             Qty = cartItem.Qty,
                             CartId = cartItem.CartId
                         }).ToListAsync();
        }

        public Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
