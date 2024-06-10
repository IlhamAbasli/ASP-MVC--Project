using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Asp_Project.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
