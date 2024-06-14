using System.ComponentModel.DataAnnotations;

namespace Asp_Project.ViewModels.Banners.Admin
{
    public class BannerCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
