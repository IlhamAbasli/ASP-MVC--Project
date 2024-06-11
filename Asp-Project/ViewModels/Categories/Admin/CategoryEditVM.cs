using System.ComponentModel.DataAnnotations;

namespace Asp_Project.ViewModels.Categories.Admin
{
    public class CategoryEditVM
    {
        [Required]
        [StringLength(15)]
        public string CategoryName { get; set; }    
    }
}
