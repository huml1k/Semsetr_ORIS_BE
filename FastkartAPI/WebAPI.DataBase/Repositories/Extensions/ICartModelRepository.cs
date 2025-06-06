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
        public Task Create(CartModel cart);

        public Task Update(CartModel cart); 

        public Task Delete(CartModel cart);
        
        public Task<List<CartModel>> GetByUserId(Guid id);

        public Task DeleteByID(Guid id);

        public Task<CartModel> GetById(Guid id);
    }
}
