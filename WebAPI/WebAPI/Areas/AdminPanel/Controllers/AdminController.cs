using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Route("[controller]")]
    [Controller]
    public class AdminController : Controller
    {

        [HttpGet("admin")]
        public async Task<IActionResult> Index() => View("seller-dashboard");
    }
}
