namespace Asp_Project.ViewModels.Basket
{
    public class BasketVM
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public decimal ProductTotalPrice { get; set; }
        public string Image { get; set; }
    }
}
