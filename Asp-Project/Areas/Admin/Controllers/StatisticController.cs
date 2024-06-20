using Asp_Project.ViewModels.Features.Admin;
using Asp_Project.ViewModels.Statistic.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Services.Interfaces;

namespace Asp_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="SuperAdmin,Admin")]
    public class StatisticController : Controller
    {
        private readonly IStatisticService _statisticService;
        public StatisticController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var statistics = await _statisticService.GetAll();

            List<StatisticVM> model = statistics.Select(m => new StatisticVM { Id = m.Id, Title = m.Title, Count = m.Count }).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StatisticCreateVM request)
        {
            if (!ModelState.IsValid) return View();

            await _statisticService.Create(new Statistic { Title = request.Title, Count = request.Count });
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var existFeature = await _statisticService.GetById((int)id);
            if (existFeature is null) return NotFound();

            await _statisticService.Delete(existFeature);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            var existStat = await _statisticService.GetById((int)id);
            if (existStat is null) return NotFound();

            StatisticEditVM model = new()
            {
                Title = existStat.Title,
                Count = existStat.Count,
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, StatisticEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            if (id is null) return BadRequest();
            var existStat = await _statisticService.GetById((int)id);
            if (existStat is null) return NotFound();

            await _statisticService.Edit((int)id, new Statistic { Title = request.Title, Count = request.Count });
            return RedirectToAction(nameof(Index));

        }
    }
}
