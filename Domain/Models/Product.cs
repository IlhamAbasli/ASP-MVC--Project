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
        public int RatingCount { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }        
        public int DetailId { get; set; }
        public ProductDetail Detail { get; set; }
        public ICollection<ProductImages> ProductImages { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
