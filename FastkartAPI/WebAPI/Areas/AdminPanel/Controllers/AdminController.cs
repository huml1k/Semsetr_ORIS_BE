using FastkartAPI.Contracts.Contracts;
using FastkartAPI.Contracts.DTOs;
using FastkartAPI.DataBase.Models;
using FastkartAPI.DataBase.Models.Enums;
using FastkartAPI.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Route("[controller]")]
    [Controller]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ProductService _productService;
        private readonly OrderService _orderService;
        private readonly UserService _userService;

        public AdminController(
            ProductService productService,
            OrderService orderService,
            UserService userService)
        {
            _productService = productService;
            _orderService = orderService;
            _userService = userService;
        }

        [HttpGet("/profile")]
        public async Task<IActionResult> Index()
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == "userId").Value;

            var user = await _userService.GetById(Guid.Parse(userIdClaim));

            if (user.Role == RoleEnum.Admin) 
            {
                var result = await _productService.GetAll();
                return View("seller-dashboard", result);
            }
            else 
            {
                return RedirectPermanent("/home/index");
            }
        } 

        [HttpGet("addProductPage")]
        public async Task<IActionResult> GetPageAddProduct() => View("CreateProduct");


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

        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetByID(Guid id) 
        {
            var item = await _productService.GetById(id);

            return View("EditProduct", item);
        }

        [HttpDelete("deleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productService.Delete(id);

            return Ok("Продукт успешно удален");
        }

        [HttpPost("updateProduct")]
        public async Task<IActionResult> UpdateProductInfo([FromForm] ItemStore itemStore) 
        {
            await _productService.Update(itemStore);
            return Ok("Данные успешно обновлены");
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetAllOrders([FromQuery] int page = 1, [FromQuery] int pageSize = 10) 
        {
            var result = await _orderService.GetListOrders();

            var totalOrders = result.Count;

            var orders = result
                .Skip((page -1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(new PaginatedResponse<OrderModel>
            {
                Items = orders,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = totalOrders,
                TotalPages = (int)Math.Ceiling(totalOrders / (double)pageSize)
            });
        }

        [HttpPost("updateOrderStatus")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] UpdateOrderStatusDTO dto)
        {
            await _orderService.UpdateStatusOrder(dto.OrderId, dto.Status);
            return Ok();
        }
    }
}
