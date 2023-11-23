namespace shoppingAPI.Models
{
    public class UserModel
    {
      public int UserId { get; set; }
      public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int PostCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public CartModel Cart { get; set; }

        public ICollection<OrderModel> Orders { get; set; }
    }
}
