using Asp_Project.ViewModels.Categories.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Asp_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAll();

            List<CategoryVM> model = categories.Select(m => new CategoryVM { Id = m.Id, CategoryName = m.Name }).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM request)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            bool isExist = await _categoryService.CategoryIsExist(request.CategoryName);

            if (isExist)
            {
                ModelState.AddModelError("CategoryName", "This category has already exist");
                return View();
            }

            await _categoryService.Create(new Category { Name = request.CategoryName });

            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var category = await _categoryService.GetById((int)id);

            if(category is null) return NotFound();

            await _categoryService.Delete((int)id);  
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var category = await _categoryService.GetById((int)id);

            if (category is null) return NotFound();

            CategoryEditVM model = new() { CategoryName = category.Name };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CategoryEditVM request)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            if (id is null) return BadRequest();

            var category = await _categoryService.GetById((int)id);

            if (category is null) return NotFound();

            await _categoryService.Edit((int) id,new Category { Name = request.CategoryName });

            return RedirectToAction(nameof(Index));
        }


    }
}
