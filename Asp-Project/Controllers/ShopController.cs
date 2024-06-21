using Asp_Project.Helpers;
using Asp_Project.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Services.Interfaces;

namespace Asp_Project.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IBasketService _basketService;
        public ShopController(IProductService productService, 
                              ICategoryService categoryService,
                              UserManager<AppUser> userManager,
                              IBasketService basketService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _userManager = userManager;
            _basketService = basketService;
        }
        [HttpGet]   
        public async Task<IActionResult> Index(string searchText,int? price,int? categoryId,string sortType,int page = 1)
        {
            if(searchText is not null)
            {
                var paginatedSearchedDatas = await _productService.GetAllSearchedPaginatedDatas(page, searchText);
                int searchedProductCount = await _productService.GetSearchedCount(searchText);
                int searchedPageCount = _productService.GetPageCount(searchedProductCount, 9);

                Paginate<Product> searchPagination = new(paginatedSearchedDatas, searchedPageCount, page);

                var searchCategories = await _categoryService.GetAll();
                var products = await _productService.GetAll();

                ProductPageVM searchModel = new()
                {
                    Categories = searchCategories,
                    Products = products,
                    Pagination = searchPagination
                };


                return View(searchModel);
            }
            else if(price is not null)
            {
                var filteredDatas = await _productService.GetAllPriceFilteredPaginatedDatas(page, (int)price);
                var filteredProductCount = await _productService.GetPriceFilteredCount((int)price);
                var filteredPageCount = _productService.GetPageCount(filteredProductCount, 9);

                Paginate<Product> filterPagination = new(filteredDatas, filteredPageCount, page);

                var searchCategories = await _categoryService.GetAll();
                var products = await _productService.GetAll();
                ProductPageVM filterModel = new()
                {
                    Categories = searchCategories,
                    Products = products,
                    Pagination = filterPagination,
                };

                return View(filterModel);
            }
            else if(categoryId is not null)
            {
                var categoryFilteredDatas = await _productService.GetCategoryFilteredPaginatedDatas(page, (int)categoryId);
                var categoryFilteredProductCount = await _productService.GetCategoryFilteredCount((int)categoryId);
                var categoryFilteredPageCount = _productService.GetPageCount(categoryFilteredProductCount, 9);

                Paginate<Product> categoryFilterPagination = new(categoryFilteredDatas, categoryFilteredPageCount, page);

                var searchCategories = await _categoryService.GetAll();
                var products = await _productService.GetAll();
                ProductPageVM categoryFilterModel = new()
                {
                    Categories = searchCategories,
                    Products = products,
                    Pagination = categoryFilterPagination,
                };

                return View(categoryFilterModel);
            }
            else if(sortType is not null)
            {
                var sortedDatas = await _productService.GetSortedPaginatedDatas(page, sortType);
                var sortedCount = await _productService.GetCount();
                var sortedPageCount = _productService.GetPageCount(sortedCount, 9);

                Paginate<Product> sortedPagination = new(sortedDatas, sortedPageCount, page);

                var searchCategories = await _categoryService.GetAll();
                var products = await _productService.GetAll();
                ProductPageVM sortedModel = new()
                {
                    Categories = searchCategories,
                    Products = products,
                    Pagination = sortedPagination,
                };

                return View(sortedModel);
            }
            else
            {
                var paginatedDatas = await _productService.GetAllPaginatedDatas(page);
                int productCount = await _productService.GetCount();
                int pageCount = _productService.GetPageCount(productCount, 9);

                Paginate<Product> pagination = new(paginatedDatas, pageCount, page);

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
            if (await _basketService.ExistProduct(dbProduct.Name, user.Id))
            {
                await _basketService.IncreaseExistProductCount(dbProduct.Name, user.Id);
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
            return Ok(new {count});
        }
    }
}
