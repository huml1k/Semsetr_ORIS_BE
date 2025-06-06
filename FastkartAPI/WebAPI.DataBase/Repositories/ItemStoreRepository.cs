using FastkartAPI.DataBase.Models;
using FastkartAPI.DataBase.Models.Enums;
using FastkartAPI.DataBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataBase;

namespace FastkartAPI.DataBase.Repositories
{
    public class ItemStoreRepository : IItemStoreRepository
    {
        private readonly MyApplicationContext _context;

        public ItemStoreRepository(MyApplicationContext context) 
        {
            _context = context;
        }

        public async Task Create(ItemStore user)
        {
            var result = user.CreateItem(user);

            await _context.AddAsync(result);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            await _context.ItemsStore
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<List<ItemStore>> GetAll()
        {
            var result = await _context.ItemsStore
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

        public async Task<List<ItemStore>> GetByCategory(TypeItemEnum type)
        {
            var result = await _context.ItemsStore
                .AsNoTracking()
                .Where(x => x.TypeItem.Contains(type))
                .ToListAsync();

            return result;
        }

        public async Task<ItemStore> GetById(Guid id)
        {
            var result = await _context.ItemsStore
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<ItemStore> GetByName(string name)
        {
            var result = await _context.ItemsStore
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == name);

            return result;
        }

        public Task Update(ItemStore item)
        {
            throw new NotImplementedException();
        }
    }
}
