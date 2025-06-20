using System.Security.Claims;
using FastkartAPI.DataBase.Models;
using FastkartAPI.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastkartAPI.Controllers
{
    [Controller]
    [Route("[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(
            UserService userService
            ) 
        {
            _userService = userService;
        }

        [HttpGet("dashboard/{id}")]
        public async Task<IActionResult> GetPageUser(Guid id)
        {
            var user = await _userService.GetById(id);
            return View("user-dashboard", user);
        }
        
        [HttpPost("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel model)
        {
            try
            {
                var user = await _userService.GetById(model.Id);
                if (user == null) return NotFound();
        
                await _userService.Update(model);
        
                return Ok(new { message = "Profile updated" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("current")]
        [Authorize] // Только для авторизованных пользователей
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                // Получаем ID пользователя из claims
                var userId = User.Claims.FirstOrDefault(x => x.Type == "userId").Value;

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var user = await _userService.GetById(Guid.Parse(userId));

                if (user == null)
                    return NotFound();

                return Ok(new
                {
                    userId = user.Id,
                    fullName = user.FullName,
                    role = user.Role.ToString()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
