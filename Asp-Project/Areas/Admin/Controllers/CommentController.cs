using Asp_Project.ViewModels.Comments.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Services.Interfaces;
using System.Windows.Input;

namespace Asp_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="SuperAdmin,Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public async Task<IActionResult> Index()
        {
            var comments = await _commentService.GetAll();

            List<CommentAdminVM> model = comments.Select(m => new CommentAdminVM { Id = m.Id, UserEmail = m.User.Email, UserFullName = m.User.FullName, ProductName = m.Product.Name, PostDate = m.CreatedDate.ToString("MMMM,dd,yyyy"), Comment = m.UserComment }).ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var data = await _commentService.GetById((int)id);
            if (data is null) return NotFound();

            await _commentService.Delete(data);
            return RedirectToAction(nameof(Index));
        }
    }
}
