using Asp_Project.Helpers.Extensions;
using Asp_Project.ViewModels.Ads.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Asp_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class AdvertisementController : Controller
    {
        private readonly IAdService _adService;
        private readonly IWebHostEnvironment _env;
        public AdvertisementController(IAdService adService,
                                       IWebHostEnvironment env)
        {
            _adService = adService;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var datas = await _adService.GetAll();

            List<AdVM> model = datas.Select(m => new AdVM { Id = m.Id, Title = m.Title }).ToList();

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
        public async Task<IActionResult> Create(AdCreateVM request)
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

            await _adService.Create(new Advertisement { Title = request.Title ,Description = request.Description, AdImage = fileName});
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            var existAd = await _adService.GetById((int) id);
            if (existAd is null) return NotFound();

            AdEditVM model = new() { Title = existAd.Title, Description = existAd.Description, OldImage = existAd.AdImage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AdEditVM request)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            if (id is null) return BadRequest();
            var existAd = await _adService.GetById((int)id);
            if (existAd is null) return NotFound();

            if(request.NewImage is not null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "File type must be image");
                    request.OldImage = existAd.AdImage;
                    return View(request);
                }                
                
                if (!request.NewImage.CheckFileSize(1))
                {
                    ModelState.AddModelError("NewImage", "Image size must be less than 1 Mb");
                    request.OldImage = existAd.AdImage;
                    return View(request);
                }

                string oldPath = Path.Combine(_env.WebRootPath, "img", existAd.AdImage);
                oldPath.DeleteFileFromLocal();

                string newFileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;
                string newPath = Path.Combine(_env.WebRootPath, "img", newFileName);
                await request.NewImage.SaveFileToLocalAsync(newPath);

                await _adService.Edit((int)id, new Advertisement { Title = request.Title, Description = request.Description, AdImage = newFileName });

            }
            else
            {
                await _adService.Edit((int) id,new Advertisement { Title = request.Title, Description = request.Description, AdImage = existAd.AdImage });
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var existAd = await _adService.GetById((int)id);
            if (existAd is null) return NotFound();

            await _adService.Delete(existAd);

            return RedirectToAction(nameof(Index));
        }
    }
}
