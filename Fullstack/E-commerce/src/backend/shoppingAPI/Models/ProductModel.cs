namespace shoppingAPI.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string? URL { get; set; }
        public string? Category { get; set; }
        public ICollection<CartItemModel>? CartItems { get; set; }
    }
}
