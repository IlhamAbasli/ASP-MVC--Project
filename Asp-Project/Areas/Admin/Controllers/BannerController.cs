using Asp_Project.Helpers.Extensions;
using Asp_Project.ViewModels.Banners.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Asp_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController : Controller
    {
        private readonly IBannerService _bannerService;
        private readonly IWebHostEnvironment _env;
        public BannerController(IBannerService bannerService,
                                IWebHostEnvironment env)
        {
            _bannerService = bannerService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _bannerService.GetAll();

            List<BannerVM> model = datas.Select(m => new BannerVM { Id = m.Id, Title = m.Title }).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> Create(BannerCreateVM request)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            if (!request.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "File type must be image");
                return View();
            }

            if (!request.Image.CheckFileSize(1))
            {
                ModelState.AddModelError("Image", "Image size must be less than 1 Mb");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "-" + request.Image.FileName;
            string path = Path.Combine(_env.WebRootPath, "img", fileName);
            await request.Image.SaveFileToLocalAsync(path);

            await _bannerService.Create(new Banner { Title = request.Title, Description = request.Description, Image = fileName });
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var existBanner = await _bannerService.GetById((int)id); 
            if(existBanner is null) return NotFound();

            BannerDetailVM model = new()
            {
                Title = existBanner.Title,
                Description = existBanner.Description,
                Image = existBanner.Image,
            };

            return View(model); 
        }
    }
}
