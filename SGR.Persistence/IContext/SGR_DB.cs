using Microsoft.EntityFrameworkCore;
using SGR.Domain.Entities.Payment;
using SGR.Domain.Entities.Reservations__Payment_and_Orders;
using SGR.Domain.Entities.Restaurants_and_Products;
using SGR.Domain.Entities.Review__Notifications_and_Analytics;
using SGR.Domain.Entities.Users;

namespace SGR.Persistence.IContext
{
    public class SGR : DbContext
    {
        public SGR(DbContextOptions<SGR> options)
            : base(options) { }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<FoodOrder> FoodOrders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Analytics> Analytics { get; set; }
    }
}
