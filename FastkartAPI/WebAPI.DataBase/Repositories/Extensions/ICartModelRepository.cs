using FastkartAPI.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastkartAPI.DataBase.Repositories.Extensions
{
    public interface ICartModelRepository
    {
        public Task Create(Guid userId, Guid itemId, int qty);

        public Task Update(Guid cartItemId, int newQty); 

        public Task Delete(Guid cartItemId);
        
        public Task<List<CartModel>> GetByUserId(Guid id);

        public Task DeleteByID(Guid id);

        public Task<CartModel> GetById(Guid id);
    }
}
