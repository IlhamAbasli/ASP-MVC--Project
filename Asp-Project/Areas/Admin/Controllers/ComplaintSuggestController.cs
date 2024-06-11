using Microsoft.AspNetCore.Mvc;

namespace Asp_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ComplaintSuggestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
