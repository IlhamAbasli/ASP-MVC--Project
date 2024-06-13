using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Asp_Project.ViewModels.Products.Admin
{
    public class ProductCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public string OriginCountry { get; set; }
        public int ProductCount { get; set; }
        public int QualityId { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public List<IFormFile> Images { get; set; }
        public List<ProductDetail> ProductDetails { get; set; }
    }
}
