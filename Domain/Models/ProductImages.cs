﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ProductImages : BaseEntity
    {
        public string Image {  get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
