using Microsoft.AspNetCore.Mvc;

namespace Asp_Project.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
