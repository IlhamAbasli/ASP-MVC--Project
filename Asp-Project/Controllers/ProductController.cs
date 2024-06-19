using Asp_Project.ViewModels.Products;
using Asp_Project.ViewModels.Products.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Asp_Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService,    
                                 ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index(int? id)
        {
            if (id is null) return BadRequest();

            var existProduct = await _productService.GetById((int)id);

            if(existProduct is  null) return NotFound();

            ProductDetailVM product = new()
            {
                Name = existProduct.Name,
                Description = existProduct.Description,
                Price = existProduct.Price,
                Rating = existProduct.RatingCount,
                Category = existProduct.Category.Name,
                ProductImages = existProduct.ProductImages.Select(m => new ProductImageVM { Image = m.Image, IsMain = m.IsMain }).ToList(),
                Country = existProduct.Details.FirstOrDefault().OriginCountry,
            };

            List<Category> categories = await _categoryService.GetAll(); 
            List<Product> products = await _productService.GetAll();

            ProductDetailPageVM model = new()
            {
                Product = product,
                Products = products,
                Categories = categories,
            };

            return View(model);
        }
    }
}
