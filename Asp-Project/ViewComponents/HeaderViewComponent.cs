using Asp_Project.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Asp_Project.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly IBasketService _basketService;
        private readonly UserManager<AppUser> _userManager;
        public HeaderViewComponent(ISettingService settingService,
                                   IBasketService basketService,
                                   UserManager<AppUser> userManager)
        {
            _settingService = settingService;
            _basketService = basketService;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var setting = await _settingService.GetAll();

            Dictionary<string, string> values = new();

            foreach (KeyValuePair<int, Dictionary<string, string>> item in setting)
            {
                values.Add(item.Value["Key"], item.Value["Value"]);
            }

            HeaderVM response = new()
            {
                Settings = values,
            };


            AppUser user = new();
            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }


            ViewBag.BasketCount = await _basketService.GetBasketProductCount(user.Id);


            return await Task.FromResult(View(response));
        }
    }
}
