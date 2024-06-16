using Asp_Project.Helpers;
using Domain.Models;

namespace Asp_Project.ViewModels
{
    public class ProductPageVM
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public Paginate<Product> Pagination { get; set; }
    }
}
