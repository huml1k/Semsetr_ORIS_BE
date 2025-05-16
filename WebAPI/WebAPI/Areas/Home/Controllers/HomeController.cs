using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Areas.Home.Controllers
{
    [Controller]
    [Route("[controller]")]
    [Area("Home")]
    public class HomeController : Controller
    {
       
        [HttpGet("index")]
        public async Task<IActionResult> Index() => View("index");
        
    }
}
