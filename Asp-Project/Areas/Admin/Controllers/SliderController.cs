using Asp_Project.Helpers.Extensions;
using Asp_Project.ViewModels.Sliders.Admin;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Asp_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IWebHostEnvironment _env;
        public SliderController(ISliderService sliderService,
                                IWebHostEnvironment env) 
        {
            _sliderService = sliderService;
            _env = env;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _sliderService.GetAll();

            List<SliderVM> model = datas.Select(m => new SliderVM { Id = m.Id, SliderText = m.SliderText }).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!request.SliderImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("SliderImage", "File type must be image");
                return View();
            }

            if (!request.SliderImage.CheckFileSize(3))
            {
                ModelState.AddModelError("SliderImage", "File size must be less than 3 Mb");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "-" + request.SliderImage.FileName;

            string path = Path.Combine(_env.WebRootPath, "img", fileName);

            await request.SliderImage.SaveFileToLocalAsync(path);

            await _sliderService.Create(new Slider { SliderText = request.SliderText, SliderImage = fileName });
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var existSlider = await _sliderService.GetById((int)id);

            if (existSlider is null) return NotFound();

            SliderDetailVM response = new() { SliderImage = existSlider.SliderImage, SliderText = existSlider.SliderText };
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var existSlider = await _sliderService.GetById((int)id);

            if (existSlider is null) return NotFound();

            SliderEditVM response = new()
            {
                SliderText = existSlider.SliderText,
                ExistImage = existSlider.SliderImage
            };

            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return(View());
            }

            if (id is null) return BadRequest();

            var existSlider = await _sliderService.GetById((int)id);

            if (existSlider is null) return NotFound();

            if(request.NewImage is not null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "File type must be image");
                    request.ExistImage = existSlider.SliderImage;
                    return View(request);
                }

                if (!request.NewImage.CheckFileSize(3))
                {
                    ModelState.AddModelError("NewImage", "File size must be less than 3 Mb");
                    request.ExistImage = existSlider.SliderImage;
                    return View(request);
                }

                string oldPath = Path.Combine(_env.WebRootPath, "img", existSlider.SliderImage);
                oldPath.DeleteFileFromLocal();


                string newFileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;
                string newPath = Path.Combine(_env.WebRootPath, "img", newFileName);
                await request.NewImage.SaveFileToLocalAsync(newPath);

                await _sliderService.Edit((int)id, new Slider { SliderText = request.SliderText, SliderImage = newFileName });

            }
            else
            {
                await _sliderService.Edit((int)id, new Slider { SliderText = request.SliderText, SliderImage = existSlider.SliderImage });
            }


            return RedirectToAction(nameof(Index));
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var existSlider = await _sliderService.GetById((int)id);

            if (existSlider is null) return NotFound();

            string existImage = Path.Combine(_env.WebRootPath,"img",existSlider.SliderImage);

            existImage.DeleteFileFromLocal();

            await _sliderService.Delete(existSlider);
            return RedirectToAction(nameof(Index));

        }
    }
}
