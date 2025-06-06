using AutoMapper;
using FastkartAPI.Contracts.DTOs;
using FastkartAPI.DataBase.Models;
using FastkartAPI.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace FastkartAPI.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ProductService _productService;

        public HomeController(
            ProductService productService,
            IMapper mapper) 
        {
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet("index")]
        public async Task<IActionResult> Index() 
        {
            var items = await _productService.GetAll();
            var result = _mapper.Map<IEnumerable<ProductCardDTO>>(items);
            return View("index", result);
        } 
    }
}
