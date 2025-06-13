using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Domain.Entities.Reservations__Payment_and_Orders
{
    public class FoodOrder
    {
        public int IdOrder { get; set; }
        public DateTime dateTime { get; set; }
        public string Status { get; set; }
        public int IdCustomer { get; set ; }
        public int IdRestaurant { get; set; }
    }
}
