using System.ComponentModel.DataAnnotations;

namespace Asp_Project.ViewModels.Banners.Admin
{
    public class BannerEditVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string ExistImage { get; set; }
        public IFormFile NewImage { get; set; }
    }
}
