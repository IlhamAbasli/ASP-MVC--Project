using Microsoft.AspNetCore.Mvc;

namespace Asp_Project.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
