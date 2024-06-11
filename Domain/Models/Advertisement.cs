using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Advertisement : BaseEntity
    {
        public string AdImage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
