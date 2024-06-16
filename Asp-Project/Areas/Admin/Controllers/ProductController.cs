using Asp_Project.Helpers.Extensions;
using Asp_Project.ViewModels.Products.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Services.Interfaces;

namespace Asp_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _env;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService,
                                 IWebHostEnvironment env,
                                 ICategoryService categoryService)
        {
            _productService = productService;
            _env = env;
            _categoryService = categoryService;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAll();
            List<ProductVM> model = products.Select(m => new ProductVM { Id = m.Id, ProductName = m.Name }).ToList();


            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAll();
            var qualities = await _productService.GetQualities();
            ViewBag.categories = new SelectList(categories, "Id", "Name");
            ViewBag.qualities = new SelectList(qualities, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM request)
        {            
            var categories = await _categoryService.GetAll();
            var qualities = await _productService.GetQualities();
            ViewBag.categories = new SelectList(categories, "Id", "Name");
            ViewBag.qualities = new SelectList(qualities, "Id", "Name");

            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach (var item in request.Images)
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Images", "File type must be image");
                    return View();
                }

                if (!item.CheckFileSize(2))
                {
                    ModelState.AddModelError("Images", "Image size must be less than 2");
                    return View();
                }
            }

            List<ProductImageVM> images = new();

            foreach (var item in request.Images)
            {
                string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

                string path = Path.Combine(_env.WebRootPath, "img", fileName);

                await item.SaveFileToLocalAsync(path);

                images.Add(new ProductImageVM
                {
                    Image = fileName,
                });
            }

            images.FirstOrDefault().IsMain = true;

            List<ProductDetail> details = new()
            {
                new ProductDetail
                {
                    OriginCountry = request.OriginCountry,
                    QualityId = request.QualityId,

                }
            };

            Product product = new()
            {
                Name = request.Name,
                Description = request.Description,
                Price = decimal.Parse(request.Price),
                CategoryId = request.CategoryId,
                Count = request.ProductCount,
                ProductImages = images.Select(m => new ProductImages { Image = m.Image, IsMain = m.IsMain }).ToList(),
                Details = details
            };

            await _productService.Create(product);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var product = await _productService.GetById((int)id);

            if (product is null) return NotFound();

            ProductDetailVM model = new()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category.Name,
                ProductImages = product.ProductImages.Select(m => new ProductImageVM { Image = m.Image, IsMain = m.IsMain }).ToList(),
                Rating = product.RatingCount,
                Count = product.Count,
                Country = product.Details.FirstOrDefault().OriginCountry,
                SellingCount = product.SellingCount,
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var categories = await _categoryService.GetAll();
            var qualities = await _productService.GetQualities();
            ViewBag.categories = new SelectList(categories, "Id", "Name");
            ViewBag.qualities = new SelectList(qualities, "Id", "Name");

            if (id is null) return BadRequest();
            var product = await _productService.GetById((int)id);
            if (product is null) return NotFound();

            ProductEditVM model = new()
            {
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductPrice = product.Price.ToString(),
                ProductCount = product.Count,
                ProductCountry = product.Details.FirstOrDefault().OriginCountry,
                ExistProductImages = product.ProductImages.Select(m => new ProductEditImageVM { Id = m.Id, Name = m.Image, IsMain = m.IsMain, ProductId = m.ProductId }).ToList(),
                CategoryId = product.CategoryId,
                QualityId = product.Details.FirstOrDefault().QualityId,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int? id, int? productId)
        {
            if (id is null) return BadRequest();

            Product product = await _productService.GetById((int)productId);

            if (product is null) return NotFound();


            var existImage = product.ProductImages.FirstOrDefault(m=> m.Id == id);

            if (existImage.IsMain)
            {
                return Problem();
            }

            string path = Path.Combine(_env.WebRootPath, "img", existImage.Image);
            path.DeleteFileFromLocal();

            await _productService.DeleteImage(existImage);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeMainImage(int? id,int? productId)
        {
            if(id is null || productId is null) return BadRequest();

            Product product = await _productService.GetById((int)productId);

            if (product is null) NotFound();

            await _productService.ChangeMainImage(product, (int)id);

            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,ProductEditVM request)
        {
            var categories = await _categoryService.GetAll();
            var qualities = await _productService.GetQualities();
            ViewBag.categories = new SelectList(categories, "Id", "Name");
            ViewBag.qualities = new SelectList(qualities, "Id", "Name");
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id is null) return BadRequest();

            var existProduct = await _productService.GetById((int)id);

            if(existProduct is null) return NotFound();

            List<ProductImages> images = existProduct.ProductImages.ToList();

            if(request.NewProductImages is not null)
            {
                foreach (var item in request.NewProductImages)
                {
                    if (!item.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("NewProductImages", "File type must be image");
                        request.ExistProductImages = existProduct.ProductImages.Select(m=>new ProductEditImageVM {Id = m.Id, Name = m.Image, IsMain = m.IsMain, ProductId = m.ProductId}).ToList();
                        return View(request);
                    }

                    if (!item.CheckFileSize(2))
                    {
                        ModelState.AddModelError("NewProductImages", "Image size must be less than 2");
                        request.ExistProductImages = existProduct.ProductImages.Select(m => new ProductEditImageVM { Id = m.Id, Name = m.Image, IsMain = m.IsMain, ProductId = m.ProductId }).ToList();
                        return View(request);
                    }
                }

                foreach (var item in request.NewProductImages)
                {
                    string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

                    string path = Path.Combine(_env.WebRootPath,"img",fileName);
                    
                    await item.SaveFileToLocalAsync(path);

                    images.Add(new ProductImages
                    {
                        Image = fileName
                    });
                }
            }

            List<ProductDetail> details = new()
            {
                new ProductDetail
                {
                    QualityId = request.QualityId,
                    OriginCountry = request.ProductCountry,
                }
            };

            Product product = new()
            {
                Name = request.ProductName,
                Description = request.ProductDescription,
                Price = decimal.Parse(request.ProductPrice),
                CategoryId = request.CategoryId,
                Details = details,
                Count = request.ProductCount,
                ProductImages = images
            };

            await _productService.Edit((int) id,product);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            Product existProduct = await _productService.GetById((int)id);
            if (existProduct is null) return NotFound();

            foreach (var item in existProduct.ProductImages)
            {
                var path = Path.Combine(_env.WebRootPath, "img", item.Image);
                path.DeleteFileFromLocal();
            }

            await _productService.Delete(existProduct);
            return RedirectToAction(nameof(Index));
        }
    }
}

