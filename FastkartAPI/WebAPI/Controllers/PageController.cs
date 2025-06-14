using Microsoft.AspNetCore.Mvc;

namespace FastkartAPI.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class PageController : Controller
    {
        [HttpGet("404")]
        public async Task<IActionResult> Get404() => View("404");

        [HttpGet("504")]
        public async Task<IActionResult> Get504() => View("404");
    }
}
