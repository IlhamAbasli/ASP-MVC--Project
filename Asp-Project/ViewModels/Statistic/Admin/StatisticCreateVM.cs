using System.ComponentModel.DataAnnotations;

namespace Asp_Project.ViewModels.Statistic.Admin
{
    public class StatisticCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Count { get; set; }
    }
}
