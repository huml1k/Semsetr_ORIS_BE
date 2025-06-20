using FastkartAPI.DataBase.Models;
using FastkartAPI.DataBase.Models.Enums;
using FastkartAPI.DataBase.Repositories.Extensions;

namespace FastkartAPI.Services.Services
{
    public class OrderService
    {
        private readonly IOrderModelRepository _orderRepository;
        private readonly ICartModelRepository _cartRepository;

        public OrderService(
            IOrderModelRepository orderRepository,
            ICartModelRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }

        public async Task ProcessOrder(Guid userId)
        {
            // Получаем товары в корзине
            var cartItems = await _cartRepository.GetByUserId(userId);

            // Рассчитываем общую сумму
            decimal totalPrice = cartItems.Sum(item => item.Item.Price * item.Qty);

            // Создаем заказ
            var order = new OrderModel
            {
                UserId = userId,
                TotalPrice = totalPrice,
                Status = StatusEnum.Pending
            };

            // Сохраняем заказ
            await _orderRepository.Create(order);
        }

        public async Task UpdateStatusOrder(Guid orderId, StatusEnum status) 
        {
            await _orderRepository.Update(orderId, status);
        }

        public async Task<List<OrderModel>> GetListById(Guid userId) 
        {
           return await _orderRepository.GetByUserID(userId);
        }

        public async Task<List<OrderModel>> GetListOrders() 
        {
            return await _orderRepository.GetAll();
        }

        public async Task<int> GetCountPending() 
        {
            return await _orderRepository.GetCountPending();
        }

        public async Task<int> GetCountSucces() 
        {
            return await _orderRepository.GetCountSales();
        }
    }
}
