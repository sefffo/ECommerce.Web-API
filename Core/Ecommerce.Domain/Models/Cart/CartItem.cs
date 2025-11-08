namespace Ecommerce.Domain.Models.Cart
{
    public class CartItem
    {
        public int Id { set; get; }
        public string Name { set; get; } = null!;
        public string PictureUrl { set; get; } = null!;
        public decimal Price { set; get; }
        public int Quantity { set; get; }
    }
}
