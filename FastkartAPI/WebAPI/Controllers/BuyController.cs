using FastkartAPI.Contracts.Contracts;
using FastkartAPI.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastkartAPI.Controllers
{
    [Controller]
    [Route("[controller]")]
    [Authorize]
    public class BuyController : Controller
    {
        private readonly CartService _cartService;
        private readonly OrderService _orderService;

        public BuyController(
            CartService cartService,
            OrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        private Guid GetCurrentUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == "userId");
            return Guid.Parse(userIdClaim.Value);
        }

        [HttpGet("cartPage")]
        public async Task<IActionResult> GetPage()
        {
            var userId = GetCurrentUserId();
            var cart = await _cartService.GetUserCart(userId);
            return View("cart",cart);
        }

        [HttpGet("cart")]
        public async Task<IActionResult> GetCart() 
        
        {
            var userId = GetCurrentUserId();
            var cart = await _cartService.GetUserCart(userId);
            return Ok(cart);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            var userId = GetCurrentUserId();
            await _cartService.AddToCart(userId, request.ItemId, request.Quantity);
            return Ok();
        }



        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> RemoveFromCart(Guid cartItemId)
        {
            await _cartService.RemoveFromCart(cartItemId);
            return Ok();
        }

        [HttpPut("{cartItemId}")]
        public async Task<IActionResult> UpdateCartItem(Guid cartItemId, [FromBody] UpdateCartRequest request)
        {
            await _cartService.UpdateCartItem(cartItemId, request.NewQuantity);
            return Ok();
        }

        [HttpGet("checkout")]
        public async Task<IActionResult> GetCheckoutPage()
        {
            var userId = GetCurrentUserId();
            var cartItems = await _cartService.GetUserCart(userId);
            return View("checkout", cartItems);
        }
        
        [HttpPost("placeOrder")]
        public async Task<IActionResult> PlaceOrder()
        {
            try
            {
                var userId = GetCurrentUserId();
                await _orderService.ProcessOrder(userId);
                await _cartService.ProcessOrder(userId);
                return RedirectToAction("GetPageSuccess"); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("success")]
        public async Task<IActionResult> GetPageSuccess() 
        {
            return View("order-success");
        }
    }
}
