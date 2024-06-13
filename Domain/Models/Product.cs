using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int RatingCount { get; set; } = 0;
        public int CategoryId { get; set; }
        public int Count { get; set; } = 0;
        public int SellingCount { get; set; } = 0;
        public Category Category { get; set; }        
        public ICollection<ProductImages> ProductImages { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<ProductDetail> Details { get; set; }

    }
}
