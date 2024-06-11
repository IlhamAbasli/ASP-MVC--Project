using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Slider : BaseEntity
    {
        public string SliderImage { get; set; }
        public string SliderText { get; set; }
    }
}
