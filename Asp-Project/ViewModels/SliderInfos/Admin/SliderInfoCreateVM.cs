using System.ComponentModel.DataAnnotations;

namespace Asp_Project.ViewModels.SliderInfos.Admin
{
    public class SliderInfoCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile BackgroundImage { get; set; }
    }
}
