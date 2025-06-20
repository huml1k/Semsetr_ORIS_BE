using FastkartAPI.DataBase.Models;
using FastkartAPI.DataBase.Models.Enums;
using FastkartAPI.DataBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public async Task<ItemStore> GetById(Guid id)
        {
            var result = await _context.ItemsStore
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<List<ItemStore>> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new List<ItemStore>();

            return await _context.ItemsStore
                .AsNoTracking()
                .Where(p => EF.Functions.Like(p.Name, $"%{searchTerm}%"))
                .ToListAsync();
        }

        public async Task<ItemStore> GetByName(string name)
        {
            var result = await _context.ItemsStore
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == name);

            return result;
        }

        public async Task Update(ItemStore item)
        {
            _context.ItemsStore.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ItemStore>> GetByCategory(List<TypeItemEnum> categories)
        {
            return await _context.ItemsStore
            .AsNoTracking()
            .Where(x => x.TypeItem.Any(t => categories.Contains(t)))
            .ToListAsync();
        }

        public async Task DecreaseStock(Guid itemId, int quantity)
        {
            var item = await _context.ItemsStore.FindAsync(itemId);
            if (item == null) throw new Exception("Товар не найден");

            if (item.Stock < quantity)
                throw new Exception("Недостаточно товара в наличии");

            item.Stock -= quantity;
            _context.ItemsStore.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCountItems()
        {
            return await _context.ItemsStore
                .CountAsync();
        }
    }
}
