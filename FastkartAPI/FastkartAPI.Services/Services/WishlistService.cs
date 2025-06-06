using FastkartAPI.DataBase.Models;
using FastkartAPI.DataBase.Repositories.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastkartAPI.Services.Services
{
    public class WishlistService
    {
        private readonly ICartModelRepository _repository;
        private readonly CartModel _cartModel;

        public WishlistService(
            ICartModelRepository repository,
            CartModel cartModel) 
        {
            _repository = repository;
            _cartModel = cartModel;
        }

        public async Task AddItemInCart() 
        {

        }
    }
}
