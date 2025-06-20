using FastkartAPI.DataBase.Models;
using FastkartAPI.DataBase.Models.Enums;
using FastkartAPI.DataBase.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DataBase;

namespace FastkartAPI.DataBase.Repositories
{
    public class OrderModelRepository : IOrderModelRepository
    {
        private readonly MyApplicationContext _context;

        public OrderModelRepository(
            MyApplicationContext context) 
        {
            _context = context;
        }

        public async Task Create(OrderModel order)
        {
            await _context.OrderModels.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderModel>> GetAll()
        {
            return await _context.OrderModels
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<OrderModel>> GetByUserID(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Invalid User ID");

            return await _context.OrderModels
                .AsNoTracking()
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<int> GetCountPending()
        {
            return await _context.OrderModels
                .AsNoTracking()
                .Where(x => x.Status == StatusEnum.Pending)
                .CountAsync();
        }

        public async Task<int> GetCountSales()
        {
            return await _context.OrderModels
                .AsNoTracking()
                .Where(x => x.Status == StatusEnum.Success)
                .CountAsync();
        }

        public async Task Update(Guid orderId, StatusEnum newStatus)
        {
            var order = await _context.OrderModels.FirstOrDefaultAsync(x => x.OrderId == orderId);
            order.Status = newStatus;
            await _context.SaveChangesAsync();
        }
    }
}
