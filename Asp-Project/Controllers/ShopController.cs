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
        public async Task<IActionResult> Index(string searchText,int page = 1)
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
    }
}
