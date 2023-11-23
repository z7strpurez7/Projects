namespace shoppingAPI.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public string Date { get; set; }
        public string ShippingAddress { get; set; }
        public string OrderStatus { get; set; }
        public int TotalPrice { get; set; }
        public int UserID { get; set; }
        public UserModel User { get; set; }
    }
}
