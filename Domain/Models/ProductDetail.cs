using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ProductDetail : BaseEntity
    {
        public decimal Weight { get; set; } = 1;
        public string OriginCountry { get; set; }
        public int QualityId { get; set; }
        public Quality Qualities { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        
    }
}
