using Asp_Project.ViewModels.Features.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Asp_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class FeatureController : Controller
    {
        private readonly IFeatureService _featureService;
        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _featureService.GetAll();

            List<FeatureVM> model = datas.Select(m=>new FeatureVM { Id = m.Id,Title = m.FeatureName,Description = m.FeatureDesc}).ToList();
            
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FeatureCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _featureService.Create(new Feature { FeatureName = request.Title, FeatureDesc = request.Description });
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var existFeature = await _featureService.GetById((int)id);
            if (existFeature is null) return NotFound();

            await _featureService.Delete(existFeature);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            var existFeature = await _featureService.GetById((int)id);
            if (existFeature is null) return NotFound();

            FeatureEditVM model = new()
            {
                Title = existFeature.FeatureName,
                Description = existFeature.FeatureDesc,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,FeatureEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            if (id is null) return BadRequest();
            var existFeature = await _featureService.GetById((int)id);
            if (existFeature is null) return NotFound();

            await _featureService.Edit((int) id,new Feature { FeatureName = request.Title, FeatureDesc = request.Description });    
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var existFeature = await _featureService.GetById((int)id);
            if (existFeature is null) return NotFound();

            FeatureDetailVM model = new() { Title = existFeature.FeatureName, Description = existFeature.FeatureDesc, };    

            return View(model);
        }
    }
}
