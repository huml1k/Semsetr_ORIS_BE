using FastkartAPI.DataBase.Models;
using FastkartAPI.DataBase.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataBase;

namespace FastkartAPI.DataBase.Repositories;

public class CartModelRepository : ICartModelRepository
{
    private readonly MyApplicationContext _context;
    

    public CartModelRepository(MyApplicationContext context)
    {
        _context = context;
    }

    public async Task Create(Guid userId, Guid itemId, int qty)
    {
        // Проверяем наличие товара
        var item = await _context.ItemsStore.FindAsync(itemId);
        if (item == null || item.Stock < qty)
            throw new Exception("Недостаточно товара в наличии");

        // Ищем существующую запись в корзине
        var existingCartItem = await _context.Cart
            .FirstOrDefaultAsync(c => c.UserId == userId && c.ItemId == itemId);

        if (existingCartItem != null)
        {
            existingCartItem.Qty += qty;
            _context.Cart.Update(existingCartItem);
        }
        else
        {
            var newCartItem = new CartModel
            {
                UserId = userId,
                ItemId = itemId,
                Qty = qty
            };
            await _context.Cart.AddAsync(newCartItem);
        }

        await _context.SaveChangesAsync();
    }

    public async Task Update(Guid cartItemId, int newQty)
    {
        var cartItem = await _context.Cart
            .Include(c => c.Item)
            .FirstOrDefaultAsync(c => c.Id == cartItemId);

        if (cartItem == null) return;

        if (cartItem.Item.Stock < newQty)
            throw new Exception("Недостаточно товара в наличии");

        cartItem.Qty = newQty;
        _context.Cart.Update(cartItem);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid cartItemId)
    {
        var cartItem = await _context.Cart.FindAsync(cartItemId);
        if (cartItem != null)
        {
            _context.Cart.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<CartModel>> GetByUserId(Guid id)
    {
        return await _context.Cart
            .Include(c => c.Item)
            .Where(c => c.UserId == id)
            .ToListAsync();
    }

    public async Task DeleteByID(Guid id)
    {
        var cartItems = await _context.Cart
            .Where(c => c.UserId == id)
            .ToListAsync();

        _context.Cart.RemoveRange(cartItems);
        await _context.SaveChangesAsync();
    }

    public async Task<CartModel> GetById(Guid id)
    {
        return await _context.Cart
                .Include(c => c.Item)
                .FirstOrDefaultAsync(c => c.Id == id);
    }
}