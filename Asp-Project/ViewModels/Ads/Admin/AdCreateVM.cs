using System.ComponentModel.DataAnnotations;

namespace Asp_Project.ViewModels.Ads.Admin
{
    public class AdCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
