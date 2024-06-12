using System.ComponentModel.DataAnnotations;

namespace Asp_Project.ViewModels.Settings.Admin
{
    public class SettingEditVM
    {
        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
