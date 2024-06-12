using System.ComponentModel.DataAnnotations;

namespace Asp_Project.ViewModels.SliderInfos.Admin
{
    public class SliderInfoEditVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string ExistBackground { get; set; }
        public IFormFile NewBackground { get; set; }
    }
}
