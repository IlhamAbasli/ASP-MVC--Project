using Asp_Project.ViewModels.Comments;
using Asp_Project.ViewModels.Products.Admin;
using Domain.Models;

namespace Asp_Project.ViewModels.Products
{
    public class ProductDetailPageVM
    {
        public ProductDetailVM Product {  get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public CommentVM CommentData { get; set; }
        public List<Comment> ProductComments { get; set; }
    }
}
