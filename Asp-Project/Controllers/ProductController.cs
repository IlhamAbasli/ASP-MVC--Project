using Asp_Project.ViewModels.Products.Admin;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Asp_Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index(int? id)
        {
            if (id is null) return BadRequest();

            var existProduct = await _productService.GetById((int)id);

            if(existProduct is  null) return NotFound();

            ProductDetailVM model = new()
            {
                Name = existProduct.Name,
                Description = existProduct.Description,
                Price = existProduct.Price,
                Rating = existProduct.RatingCount,
                Category = existProduct.Category.Name,
                ProductImages = existProduct.ProductImages.Select(m => new ProductImageVM { Image = m.Image, IsMain = m.IsMain }).ToList(),
                Country = existProduct.Details.FirstOrDefault().OriginCountry,
            };
            
            return View(model);
        }
    }
}
