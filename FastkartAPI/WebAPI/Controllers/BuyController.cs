using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastkartAPI.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class BuyController : Controller
    {
        [HttpGet("cart")]
        public async Task<IActionResult> GetCart() => View("cart");

        [HttpGet("checkout")]
        public async Task<IActionResult> GetCheckout() => View("checkout");

        [HttpGet("compare")]
        public async Task<IActionResult> GetComapre() => View("compare");

        [HttpGet("wishlist")]
        public async Task<IActionResult> GetWishList() => View("wishlist");
    }
}
