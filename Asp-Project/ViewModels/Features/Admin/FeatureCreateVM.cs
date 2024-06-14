using System.ComponentModel.DataAnnotations;

namespace Asp_Project.ViewModels.Features.Admin
{
    public class FeatureCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
