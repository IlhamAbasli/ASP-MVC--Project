using Asp_Project.ViewModels;
using Asp_Project.ViewModels.Basket;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Services.Interfaces;
using System.Diagnostics;

namespace Asp_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly ISliderService _sliderService;
        private readonly ISliderInfoService _sliderInfoService;
        private readonly IFeatureService _featureService;
        private readonly IAdService _adService;
        private readonly IBannerService _bannerService;
        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IBasketService _basketService;

        public HomeController(ICategoryService categoryService,
                              IProductService productService,
                              ISliderService sliderService,
                              ISliderInfoService sliderInfoService,
                              IFeatureService featureService,
                              IAdService adService,
                              IBannerService bannerService,
                              IHttpContextAccessor accessor,
                              IBasketService basketService,
                              UserManager<AppUser> userManager)
        {
            _categoryService = categoryService;
            _productService = productService;
            _sliderService = sliderService;
            _sliderInfoService = sliderInfoService;
            _featureService = featureService;
            _adService = adService;
            _bannerService = bannerService;
            _accessor = accessor;
            _basketService = basketService;
            _userManager = userManager; 
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAll();   
            var products = await _productService.GetAll();
            var sliders = await _sliderService.GetAll();
            var sliderInfos = await _sliderInfoService.GetAll();
            var features = await _featureService.GetAll();
            var ads = await _adService.GetAll();  
            var banners = await _bannerService.GetAll();
            var vegetables = await _productService.GetVegetables();
            var bestSellers = await _productService.GetBestSellerProducts();

            HomeVM model = new()
            {
                Categories = categories,
                Products = products,
                Sliders = sliders,
                SliderInfo = sliderInfos.FirstOrDefault(),
                Features = features,
                Ads = ads,
                Vegetables = vegetables,
                Banner = banners.FirstOrDefault(),
                BestSellers = bestSellers,
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddProductToBasket(int? id)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return Problem();
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (id is null) return BadRequest();
            var dbProduct = await _productService.GetById((int)id);
            if (await _basketService.ExistProduct(dbProduct.Name,user.Id))
            {
                await _basketService.IncreaseExistProductCount(dbProduct.Name,user.Id);
                return Ok();
            }
            

            Basket basket = new()
            {
                ProductName = dbProduct.Name,
                ProductImage = dbProduct.ProductImages.FirstOrDefault(m => m.IsMain).Image,
                ProductCount = 1,
                ProductPrice = dbProduct.Price,
                UserId = user.Id,
            };
            await _basketService.Create(basket);

            List<Basket> products = await _basketService.GetBasketByUser(user.Id);


            int count = await _basketService.GetBasketProductCount(user.Id);
            return Ok(new { count });
        }
    }
}
