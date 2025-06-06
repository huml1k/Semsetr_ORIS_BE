using FastkartAPI.DataBase.Models;
using FastkartAPI.DataBase.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataBase;

namespace FastkartAPI.DataBase.Repositories;

public class WishlistModelRepository : IWishlistModelRepository
{
    private readonly MyApplicationContext _context;

    public WishlistModelRepository(MyApplicationContext context)
    {
        _context = context;
    }

    public async Task Create(WishListModel wishList)
    {
        throw new NotImplementedException();
    }

    public Task Update(WishListModel wishList)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(WishListModel wishList)
    {
        throw new NotImplementedException();
    }

    public async Task<List<WishListModel>> GetByUserId(Guid userId)
    {
        var result = await _context.WishLists
            .AsNoTracking()
            .Where(w => w.UserId == userId)
            .ToListAsync();
        
        return result;
    }
}