using FastkartAPI.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastkartAPI.DataBase.Repositories.Extensions
{
    public interface IWishlistModelRepository
    {
        public Task Create(WishListModel wishList);

        public Task Update(WishListModel wishList);

        public Task Delete(WishListModel wishList);
        
        public Task<List<WishListModel>> GetByUserId(Guid userId);
    }
}
