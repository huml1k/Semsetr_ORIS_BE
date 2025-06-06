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

    public async Task Create(CartModel cart)
    {
        // Проверяем, есть ли уже товар в корзине
        var existingItem = await _context.Cart
            .FirstOrDefaultAsync(c => c.UserId == cart.UserId && c.ItemId == cart.ItemId);

        if (existingItem != null)
        {
            existingItem.Qty += cart.Qty;
            await Update(existingItem);
        }
        else
        {
            await _context.Cart.AddAsync(cart);
            await _context.SaveChangesAsync();
        }
    }

    public async Task Update(CartModel cart)
    {
        _context.Cart.Update(cart);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(CartModel cart)
    {
        _context.Cart.Remove(cart);
        await _context.SaveChangesAsync();
    }

    public async Task<List<CartModel>> GetByUserId(Guid id)
    {
        var result = await _context.Cart
            .AsNoTracking()
            .Where(x => x.UserId == id)
            .ToListAsync();
        
        return result;
    }

    public async Task DeleteByID(Guid id)
    {
        var cartItem = await GetById(id);
        if (cartItem != null)
        {
            await Delete(cartItem);
        }
    }

    public async Task<CartModel> GetById(Guid id)
    {
        return await _context.Cart
                .Include(c => c.Item)
                .FirstOrDefaultAsync(c => c.Id == id);
    }
}