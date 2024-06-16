using Asp_Project.Helpers;
using Asp_Project.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Asp_Project.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ShopController(IProductService productService, 
                              ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        [HttpGet]   
        public async Task<IActionResult> Index(int page = 1)
        {
            var paginatedDatas = await _productService.GetAllPaginatedDatas(page);
            int pageCount = await _productService.GetPageCount(9);

            Paginate<Product> pagination = new(paginatedDatas, pageCount,page);

            var categories = await _categoryService.GetAll();
            var products = await _productService.GetAll();

            ProductPageVM model = new()
            {
                Categories = categories,
                Products = products,
                Pagination = pagination
            };


            return View(model);
        }
    }
}
