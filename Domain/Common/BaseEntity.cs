﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    internal class BaseEntity 
    { 
        public int Id { get; set; }
        public bool SoftDeleted { get; set; } = false;
    }
}
