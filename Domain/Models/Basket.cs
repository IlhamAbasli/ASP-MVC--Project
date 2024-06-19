using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Basket : BaseEntity
    {
        public string ProductName { get; set; }
        public string ProductImage {  get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductCount { get; set; }   
        public decimal ProductTotalPrice { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
