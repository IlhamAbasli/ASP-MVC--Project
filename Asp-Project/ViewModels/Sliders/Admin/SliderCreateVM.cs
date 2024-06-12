using System.ComponentModel.DataAnnotations;

namespace Asp_Project.ViewModels.Sliders.Admin
{
    public class SliderCreateVM
    {
        [Required]
        public IFormFile SliderImage { get; set; }
        [Required]
        public string SliderText { get; set; }
    }
}
