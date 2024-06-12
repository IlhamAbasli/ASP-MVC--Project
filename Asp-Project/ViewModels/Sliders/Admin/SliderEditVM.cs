using System.ComponentModel.DataAnnotations;

namespace Asp_Project.ViewModels.Sliders.Admin
{
    public class SliderEditVM
    {
        [Required]
        public string SliderText { get; set; }
        public string ExistImage { get; set; }
        public IFormFile NewImage { get; set; }
    }
}
