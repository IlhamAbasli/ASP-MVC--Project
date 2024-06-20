﻿using Asp_Project.Helpers.Extensions;
using Asp_Project.ViewModels.Banners.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Asp_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
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
        [Authorize(Roles = "SuperAdmin")]
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

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            var existBanner = await _bannerService.GetById((int)id);
            if (existBanner is null) return NotFound();

            BannerEditVM model = new()
            {
                Title = existBanner.Title,
                Description = existBanner.Description,
                ExistImage = existBanner.Image
            };

            return View(model); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BannerEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id is null) return BadRequest();
            var existBanner = await _bannerService.GetById((int)id);
            if (existBanner is null) return NotFound();

            if (request.NewImage is not null)
            {
                if (!request.NewImage.CheckFileType("image/")){
                    ModelState.AddModelError("NewImage", "File type must be image");
                    request.ExistImage = existBanner.Image;
                    return View(request);
                }

                if (!request.NewImage.CheckFileSize(1))
                {
                    ModelState.AddModelError("NewImage", "Image size must be less than 1 Mb");
                    request.ExistImage = existBanner.Image;
                    return View(request);
                }

                string oldPath = Path.Combine(_env.WebRootPath, "img", existBanner.Image);
                oldPath.DeleteFileFromLocal();

                string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;
                string path = Path.Combine(_env.WebRootPath, "img", fileName);
                await request.NewImage.SaveFileToLocalAsync(path);

                await _bannerService.Edit((int) id,new Banner { Title = request.Title, Description = request.Description, Image = fileName });
            }
            else
            {
                await _bannerService.Edit((int)id, new Banner { Title = request.Title, Description = request.Description, Image = existBanner.Image });
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var existBanner = await _bannerService.GetById((int)id);
            if (existBanner is null) return NotFound();

            await _bannerService.Delete(existBanner);
            return RedirectToAction(nameof(Index));
        }
    }
}
