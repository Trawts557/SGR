using Microsoft.EntityFrameworkCore;
using SGR.Domain.Entities.Payment;
using SGR.Domain.Entities.Reservations__Payment_and_Orders;
using SGR.Domain.Entities.Restaurants_and_Products;
using SGR.Domain.Entities.Review__Notifications_and_Analytics;
using SGR.Domain.Entities.Users;

namespace SGR.Persistence.IContext
{
    public class SGR_DB : DbContext
    {
        public SGR_DB(DbContextOptions<SGR_DB> options)
            : base(options) { }

        //Db set para las tablas que manipula RestaurantRepository
        public DbSet<Restaurant> Restaurants { get; set; }

        //DB Set para las tablas que manipula UserRepository
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Customer> Customers { get; set; }

    }

}
