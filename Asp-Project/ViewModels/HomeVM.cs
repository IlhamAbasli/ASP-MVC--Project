using Domain.Models;

namespace Asp_Project.ViewModels
{
    public class HomeVM
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set;}
        public List<Slider> Sliders { get; set; }
        public SliderInfo SliderInfo { get; set; }
        public List<Product> Vegetables { get; set; }
        public List<Product> BestSellers { get; set; }
        public List<Feature> Features { get; set; }
        public List<Advertisement> Ads { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Domain.Models.Statistic> Statistics { get; set; }
        public Banner Banner { get; set; }

    }
}
