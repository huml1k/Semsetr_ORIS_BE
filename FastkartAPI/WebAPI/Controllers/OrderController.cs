using FastkartAPI.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastkartAPI.Controllers
{
    [Controller]
    [Route("[controller]")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(
            OrderService orderService
            ) 
        {
            _orderService = orderService;
        }

        [HttpGet("OrdersListUser")]
        public async Task<IActionResult> GetUserListOrders() 
        {
            try
            {
                var userId = GetCurrentUserId();
                var result = await _orderService.GetListById(userId);
                return Ok(result);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetListOrders() 
        {
            var result = await _orderService.GetListOrders();
            return Ok(result);
        }

        [HttpGet("orderCountPending")]
        public async Task<IActionResult> GetPendingOrderCount() 
        {
            var result = await _orderService.GetCountPending();
            return Ok(result);
        }

        [HttpGet("orderCount")]
        public async Task<IActionResult> GetOrderCount() 
        {
            var result = await _orderService.GetCountSucces();
            return Ok(result);
        }

        private Guid GetCurrentUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == "userId");
            return Guid.Parse(userIdClaim.Value);
        }
    }
}
