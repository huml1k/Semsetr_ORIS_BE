using FastkartAPI.Contracts.DTOs;
using FastkartAPI.DataBase.Models;
using FastkartAPI.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Route("[controller]")]
    [Controller]
    public class AdminController : Controller
    {
        private readonly ProductService _productService;

        public AdminController(
            ProductService productService) 
        {
            _productService = productService;
        }

        [HttpGet("index")]
        public async Task<IActionResult> Index() => View("seller-dashboard");

        [HttpPost("addProduct")]
        public async Task<IActionResult> CreateItem([FromBody] CreateProductDTO item) 
        {
             await _productService.Create(item);
            return Ok("Продукт был добавлен"); 
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts() 
        {
            var result = await _productService.GetAll();

            return Ok(result);
        }

        [HttpDelete("deleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromBody] Guid id)
        {
            await _productService.Delete(id);

            return Ok("Продукт успешно удален");
        }

        [HttpPost("updateProduct")]
        public async Task<IActionResult> UpdateProductInfo([FromBody] ItemStore itemStore) 
        {
            await _productService.Update(itemStore);
            return Ok("Данные успешно обновлены");
        }
    }
}
