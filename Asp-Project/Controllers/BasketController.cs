using Microsoft.AspNetCore.Mvc;

namespace Asp_Project.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
