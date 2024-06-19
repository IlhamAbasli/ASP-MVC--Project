using Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace Asp_Project.ViewModels.Products.Admin
{
    public class ProductEditVM
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public string ProductPrice { get; set; }
        [Required]
        public int ProductCount { get; set; }
        [Required]
        public string ProductCountry { get; set; }
        public int CategoryId { get; set; }
        public int QualityId { get; set; }
        [IntegerValidator(MinValue = 0,MaxValue = 5)]
        [Required]
        public int RatingCount { get; set; }
        public List<ProductEditImageVM> ExistProductImages {  get; set; }
        public List<IFormFile> NewProductImages { get; set; }
        public List<ProductDetail> Details { get; set; }
    }
}
