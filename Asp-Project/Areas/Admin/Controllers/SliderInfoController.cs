using Asp_Project.Helpers.Extensions;
using Asp_Project.ViewModels.SliderInfos.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Services.Interfaces;

namespace Asp_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class SliderInfoController : Controller
    {
        private readonly ISliderInfoService _sliderInfoService;
        private readonly IWebHostEnvironment _env;
        public SliderInfoController(ISliderInfoService sliderInfoService, 
                                    IWebHostEnvironment env)
        {
            _sliderInfoService = sliderInfoService;
            _env = env;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _sliderInfoService.GetAll();

            List<SliderInfoVM> model = datas.Select(m => new SliderInfoVM {Id = m.Id, Title = m.Title }).ToList();

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
        public async Task<IActionResult> Create(SliderInfoCreateVM request)
        {
            if(!ModelState.IsValid)
            {
                return View(request);
            }

            if(!request.BackgroundImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("BackgroundImage", "File type must be image");
                return View();
            }

            if (!request.BackgroundImage.CheckFileSize(1))
            {
                ModelState.AddModelError("BackgroundImage", "Image size must be less than 1 Mb");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "-" + request.BackgroundImage.FileName;

            string path = Path.Combine(_env.WebRootPath, "img", fileName);

            await request.BackgroundImage.SaveFileToLocalAsync(path);

            await _sliderInfoService.Create(new SliderInfo { Title = request.Title , Description = request.Description, BackgroundImage = fileName});
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var existSliderInfo = await _sliderInfoService.GetById((int)id);

            if (existSliderInfo is null) return NotFound();

            SliderInfoEditVM model = new()
            {
                Title = existSliderInfo.Title,
                Description = existSliderInfo.Description,
                ExistBackground = existSliderInfo.BackgroundImage
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderInfoEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id is null) return BadRequest();

            var existSliderInfo = await _sliderInfoService.GetById((int)id);

            if (existSliderInfo is null) return NotFound();


            if(request.NewBackground is not null)
            {
                if (!request.NewBackground.CheckFileType("image/"))
                {
                    ModelState.AddModelError("BackgroundImage", "File type must be image");
                    request.ExistBackground = existSliderInfo.BackgroundImage;
                    return View(request);
                }

                if (!request.NewBackground.CheckFileSize(1))
                {
                    ModelState.AddModelError("BackgroundImage", "Image size must be less than 1 Mb");
                    request.ExistBackground = existSliderInfo.BackgroundImage;
                    return View(request);
                }

                string oldPath = Path.Combine(_env.WebRootPath, "img", existSliderInfo.BackgroundImage);
                oldPath.DeleteFileFromLocal();


                string newFileName = Guid.NewGuid().ToString() + "-" + request.NewBackground.FileName;
                string newPath = Path.Combine(_env.WebRootPath, "img", newFileName);
                await request.NewBackground.SaveFileToLocalAsync(newPath);

                await _sliderInfoService.Edit((int)id, new SliderInfo { Title = request.Title,Description = request.Description,BackgroundImage = newFileName });
            }
            else
            {
                await _sliderInfoService.Edit((int)id, new SliderInfo { Title = request.Title, Description = request.Description, BackgroundImage = existSliderInfo.BackgroundImage });
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var existSliderInfo = await _sliderInfoService.GetById((int)id);

            if (existSliderInfo is null) return NotFound();

            string existImage = Path.Combine(_env.WebRootPath, "img", existSliderInfo.BackgroundImage);

            existImage.DeleteFileFromLocal();

            await _sliderInfoService.Delete(existSliderInfo);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var existSliderInfo = await _sliderInfoService.GetById((int)id);

            if (existSliderInfo is null) return NotFound();

            SliderInfoDetailVM model = new()
            {
                Title = existSliderInfo.Title,
                Description = existSliderInfo.Description,
                BackgorundImage = existSliderInfo.BackgroundImage
            };

            return View(model);
        }
    }
}
