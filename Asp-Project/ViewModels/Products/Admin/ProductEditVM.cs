using Domain.Models;

namespace Asp_Project.ViewModels.Products.Admin
{
    public class ProductEditVM
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public string ProductCountry { get; set; }
        public int CategoryId { get; set; }
        public int QualityId { get; set; }
        public List<ProductEditImageVM> ExistProductImages {  get; set; }
        public List<IFormFile> NewProductImages { get; set; }
        public List<ProductDetail> Details { get; set; }
    }
}
