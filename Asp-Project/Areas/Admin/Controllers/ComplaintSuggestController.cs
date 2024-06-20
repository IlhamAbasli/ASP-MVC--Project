using Asp_Project.ViewModels.Complaints.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Asp_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class ComplaintSuggestController : Controller
    {
        private readonly IComplaintService _complaintService;
        public ComplaintSuggestController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var complaints = await _complaintService.GetAll();

            List<ComplaintVM> datas = complaints.Select(m => new ComplaintVM { Id = m.Id, UserEmail = m.UserEmail, UserFullName = m.UserFullName, Complaint = m.UserSuggest }).ToList();
            return View(datas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var data = await _complaintService.GetById((int)id);
            if(data is null) return NotFound();

            await _complaintService.Delete(data);
            return RedirectToAction(nameof(Index)); 
        }
    }
}
