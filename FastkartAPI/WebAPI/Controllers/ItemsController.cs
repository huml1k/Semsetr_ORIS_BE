using AutoMapper;
using FastkartAPI.Contracts.DTOs;
using FastkartAPI.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace FastkartAPI.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class ItemsController : Controller
    {
        private readonly ProductService _productService;
        private readonly IMapper _mapper;

        public ItemsController(
            ProductService productService,
            IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet("index")]
        public async Task<IActionResult> GetPage() 
        {
            return View("product");
        } 

        [HttpGet("All")]
        public async Task<IActionResult> GetAll() 
        {
            var item = await _productService.GetAll();
            return Ok(item);
        }

        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetItemById(Guid id) 
        {
            var result = await _productService.GetById(id);

            return View("product", result);
        }

        [HttpGet("itemName")]
        public async Task<IActionResult> GetItemByName([FromBody] string name) 
        {
            var item = await _productService.GetByName(name);
            var result = _mapper.Map<ProductCardDTO>(item);

            return Ok(result);
        }
    }
}
