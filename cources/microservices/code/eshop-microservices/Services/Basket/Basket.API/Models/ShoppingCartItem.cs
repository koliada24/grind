namespace Basket.API.Models
{
    public class ShoppingCartItem
    {
        public Guid Id { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public int Quanity { get; set; } = default!;
        public string Color { get; set; } = default!;
        public decimal Price { get; set; } = default!;

    }
}
