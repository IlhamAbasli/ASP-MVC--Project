using Microsoft.AspNetCore.Mvc;

namespace Asp_Project.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
