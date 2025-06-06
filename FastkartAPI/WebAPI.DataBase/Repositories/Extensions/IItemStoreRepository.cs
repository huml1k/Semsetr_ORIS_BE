using FastkartAPI.DataBase.Models.Enums;
using FastkartAPI.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastkartAPI.DataBase.Repositories.Interfaces
{
    public interface IItemStoreRepository
    {
        public Task<List<ItemStore>> GetAll();

        public Task<ItemStore> GetById(Guid id);

        public Task<ItemStore> GetByName(string name);

        public Task<List<ItemStore>> GetByCategory(TypeItemEnum type);

        public Task Create(ItemStore user);

        public Task Delete(Guid id);

        public Task Update(ItemStore item);
    }
}
