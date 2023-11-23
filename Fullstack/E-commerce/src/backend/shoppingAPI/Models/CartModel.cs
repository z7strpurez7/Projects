namespace shoppingAPI.Models
{
    public class CartModel
    {
       
        public int CartId { get; set; }
 
        public int TotalPrice { get; set; }


        public int UserId { get; set; }
        public ICollection<CartItemModel> CartItems { get; set; }
        public UserModel User { get; set; }
    }
}
