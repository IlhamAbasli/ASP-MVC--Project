using Asp_Project.ViewModels.Basket;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.Services.Interfaces;

namespace Asp_Project.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly UserManager<AppUser> _userManager;
        public BasketController(IBasketService basketService,
                                UserManager<AppUser> userManager)
        {
            _basketService = basketService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            AppUser user = new();
            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }

            var products = await _basketService.GetBasketByUser(user.Id);

            List<BasketVM> model = products.Select(m=> new BasketVM { Id = m.Id, Name = m.ProductName, Count = m.ProductCount, Price = m.ProductPrice,Image = m.ProductImage, ProductTotalPrice = m.ProductPrice * m.ProductCount}).ToList();

            ViewBag.TotalPrice = model.Sum(m => m.Price * m.Count);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> IncreaseProductCount(int? id)
        {
            if (id is null) return BadRequest();

            AppUser user = new();
            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }

            Basket existProduct = await _basketService.GetBasketProductById((int)id);

            if (existProduct is not null)
            {
                await _basketService.IncreaseExistProductCount(existProduct.ProductName, user.Id);
            }

            List<Basket> products = await _basketService.GetBasketByUser(user.Id);


            int count = await _basketService.GetBasketProductCount(user.Id);
            decimal totalPrice = products.Sum(m=>m.ProductCount * m.ProductPrice);
            int productCount = existProduct.ProductCount;
            decimal productTotalPrice = existProduct.ProductPrice * existProduct.ProductCount;


            return Ok(new { count, totalPrice, productCount, productTotalPrice });
        }

        [HttpPost]
        public async Task<IActionResult> DecreaseProductCount(int? id)
        {
            if (id is null) return BadRequest();

            AppUser user = new();
            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }

            Basket existProduct = await _basketService.GetBasketProductById((int)id);

            if (existProduct is not null && existProduct.ProductCount > 1)
            {
                await _basketService.DecreaseExistProductCount(existProduct.ProductName, user.Id);
            }

            List<Basket> products = await _basketService.GetBasketByUser(user.Id);


            int count = await _basketService.GetBasketProductCount(user.Id);
            decimal totalPrice = products.Sum(m => m.ProductCount * m.ProductPrice);
            int productCount = existProduct.ProductCount;
            decimal productTotalPrice = existProduct.ProductPrice * existProduct.ProductCount;


            return Ok(new { count, totalPrice, productCount, productTotalPrice });
        }



        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id is null) return BadRequest();

            AppUser user = new();
            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }

            Basket existProduct = await _basketService.GetBasketProductById((int)id);

            await _basketService.Remove(existProduct);

            List<Basket> products = await _basketService.GetBasketByUser(user.Id);


            int count = await _basketService.GetBasketProductCount(user.Id);
            decimal totalPrice = products.Sum(m => m.ProductCount * m.ProductPrice);


            return Ok(new { count, totalPrice });
        }



    }
}
