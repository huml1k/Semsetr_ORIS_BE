using AutoMapper;
using FastkartAPI.Contracts.DTOs;
using FastkartAPI.DataBase.Models;
using FastkartAPI.DataBase.Models.Enums;
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
        public async Task<IActionResult> Index(string categories)
        {
            // Если есть параметр категории - загружаем по категориям
            if (!string.IsNullOrEmpty(categories))
            {
                var categoryNames = categories.Split(',');
                var categoryEnums = new List<TypeItemEnum>();

                foreach (var name in categoryNames)
                {
                    if (Enum.TryParse<TypeItemEnum>(name, out var categoryEnum))
                    {
                        categoryEnums.Add(categoryEnum);
                    }
                }

                if (categoryEnums.Any())
                {
                    var items = await _productService.GetByCategory(categoryEnums);
                    var result = _mapper.Map<IEnumerable<ProductCardDTO>>(items);
                    return View("index", result);
                }
            }

            // Иначе загружаем все продукты
            var allItems = await _productService.GetAll();
            var allResult = _mapper.Map<IEnumerable<ProductCardDTO>>(allItems);
            return View("index", allResult);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return BadRequest("Search term is required");

            var items = await _productService.Search(term);
            var result = _mapper.Map<IEnumerable<ProductCardDTO>>(items);

            // Всегда возвращаем частичное представление
            return PartialView("_ProductListPartial", result);
        }

        [HttpGet("GetByCategories")]
        public async Task<IActionResult> GetByCategories(string categories)
        {
            var categoryNames = categories.Split(',');
            var categoryEnums = new List<TypeItemEnum>();

            foreach (var name in categoryNames)
            {
                if (Enum.TryParse<TypeItemEnum>(name, out var categoryEnum))
                {
                    categoryEnums.Add(categoryEnum);
                }
            }

            if (!categoryEnums.Any())
            {
                return BadRequest("No valid categories provided");
            }

            var items = await _productService.GetByCategory(categoryEnums);
            var result = _mapper.Map<IEnumerable<ProductCardDTO>>(items);

            return PartialView("_ProductListPartial", result);
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var items = await _productService.GetAll();
            var result = _mapper.Map<IEnumerable<ProductCardDTO>>(items);
            return PartialView("_ProductListPartial", result);
        }
    }
}
