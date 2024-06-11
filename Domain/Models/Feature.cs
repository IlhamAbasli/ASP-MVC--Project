using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Feature : BaseEntity
    {
        public string FeatureName { get; set; }
        public string FeatureDesc { get; set; }
    }
}
