using Microsoft.EntityFrameworkCore;
using shoppingAPI.Models;

namespace shoppingAPI.Context
{
    public class myDbContext : DbContext
    {
        public myDbContext(DbContextOptions options) : base(options) {}
        public DbSet<CartItemModel> CartItem { get; set; }
        public DbSet<CartModel> CartModel { get; set; }
        public DbSet<OrderModel> Order { get; set; }
        public DbSet<ProductModel> Product { get; set; }
        public DbSet<UserModel> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.HasKey(x => x.UserId);
                entity.Property(x => x.UserName).HasMaxLength(50);
                entity.Property(x => x.Email).HasMaxLength(50);
                entity.Property(x => x.Address).HasMaxLength(50);
                entity.Property(x => x.PostCode).HasMaxLength(50).HasColumnType("int"); ;
                entity.Property(x => x.City).HasMaxLength(50);
                entity.Property(x => x.Country).HasMaxLength(50);
                entity.Property(x => x.Phone).HasMaxLength(50);
                entity.Property(x => x.Password).HasMaxLength(50);
                entity.HasMany(x => x.Orders).WithOne(e => e.User).HasForeignKey(e => e.UserID);
            });

            modelBuilder.Entity<ProductModel>(entity =>
            {
                entity.HasKey(x => x.ProductId);
                entity.Property(x => x.ProductCode).HasMaxLength(50);
                entity.Property(x => x.ProductName).HasMaxLength(50);
                entity.Property(x => x.Description).HasMaxLength(50);
                entity.Property(x => x.Price).HasColumnType("decimal(18,2)");
                entity.Property(x => x.Quantity).HasColumnType("int");
                entity.Property(x => x.URL).HasMaxLength(50);
                entity.Property(x => x.Category).IsRequired(false).HasMaxLength(50);
                entity.HasMany(x => x.CartItems).WithOne(e => e.Product).HasForeignKey(x => x.ProductId);
            });

            modelBuilder.Entity<OrderModel>(entity =>
            {
                entity.HasKey(x => x.OrderId);
                entity.Property(x => x.Date);
                entity.Property(x => x.ShippingAddress).HasMaxLength(50);
                entity.Property(x => x.OrderStatus).HasMaxLength(50);
                entity.Property(x => x.TotalPrice).HasMaxLength(50).HasColumnType("int"); ;
                entity.HasOne(x => x.User).WithMany(e => e.Orders).HasForeignKey(x => x.UserID);
            });

            modelBuilder.Entity<CartModel>(entity =>
            {
                entity.HasKey(x => x.CartId);
                entity.Property(x => x.TotalPrice).HasColumnType("int"); ;
                entity.HasMany(x => x.CartItems).WithOne(e => e.Cart).HasForeignKey(e => e.CartId);
            });

            modelBuilder.Entity<CartItemModel>(entity =>
            {
                entity.HasKey(x => x.CartItemId);
                entity.Property(x => x.Quantity).HasColumnType("int");

            });
        }
    }
}
