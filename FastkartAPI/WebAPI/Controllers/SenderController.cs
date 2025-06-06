using Microsoft.AspNetCore.Mvc;

namespace FastkartAPI.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class SenderController : Controller
    {
        [HttpGet("contact-us")]
        public async Task<IActionResult> GetContactUs() => View("contact-us");
    }
}
