using Microsoft.AspNetCore.Mvc;

namespace Asp_Project.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
