namespace shoppingAPI.Models
{
    public class CartItemModel
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public CartModel Cart { get; set; }
        public ProductModel Product { get; set; }
    }
}
