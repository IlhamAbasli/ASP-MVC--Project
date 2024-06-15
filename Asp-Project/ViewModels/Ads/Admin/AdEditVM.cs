using System.ComponentModel.DataAnnotations;

namespace Asp_Project.ViewModels.Ads.Admin
{
    public class AdEditVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string OldImage { get; set; }
        public IFormFile NewImage { get; set; }
    }
}
