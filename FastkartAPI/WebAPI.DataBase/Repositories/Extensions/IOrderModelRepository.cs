using FastkartAPI.DataBase.Models;
using FastkartAPI.DataBase.Models.Enums;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastkartAPI.DataBase.Repositories.Extensions
{
    public interface IOrderModelRepository
    {
        public Task Create(OrderModel order);

        public Task Update(Guid orderId, StatusEnum newStatus);

        public Task<List<OrderModel>> GetAll();

        public Task<List<OrderModel>> GetByUserID(Guid userId);

        public Task<int> GetCountSales();

        public Task<int> GetCountPending();
    }
}
