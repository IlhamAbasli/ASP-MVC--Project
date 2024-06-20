namespace Asp_Project.ViewModels.Products.Admin
{
    public class ProductDetailVM
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Country { get; set; }
        public decimal? Price { get; set; }
        public int Count { get; set; }
        public int Rating { get; set; }
        public int SellingCount { get; set; }
        public List<ProductImageVM> ProductImages { get; set; }
    }
}
