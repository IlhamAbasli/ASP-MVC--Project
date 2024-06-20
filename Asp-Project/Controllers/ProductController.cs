using Asp_Project.ViewModels.Comments;
using Asp_Project.ViewModels.Products;
using Asp_Project.ViewModels.Products.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Asp_Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICommentService _commentService;
        public ProductController(IProductService productService,
                                 ICategoryService categoryService,
                                 UserManager<AppUser> userManager,
                                 ICommentService commentService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _userManager = userManager;
            _commentService = commentService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            if (id is null) return BadRequest();

            var existProduct = await _productService.GetById((int)id);

            if (existProduct is null) return NotFound();

            ProductDetailVM product = new()
            {
                Id = existProduct.Id,
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
            List<Comment> comments = await _commentService.GetCommentsByProduct(existProduct.Id);

            AppUser user = new();
            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }

            CommentVM commentData = new()
            {
                UserId = user.Id,
                UserEmail = user.Email,
                UserName = user.FullName,
                ProductId = existProduct.Id,
            };

            ProductDetailPageVM model = new()
            {
                Product = product,
                Products = products,
                Categories = categories,
                CommentData = commentData,
                ProductComments = comments
            };

            return View(model);
        }

        [HttpPost]  
        public async Task<IActionResult> AddComment(string userId,int productId,string comment)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Problem();
            }

            await _commentService.Create(new Comment { ProductId = productId, UserId = userId,UserComment = comment});
            return Ok();
        }
    }
}
