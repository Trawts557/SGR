using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Domain.Entities.Reservations__Payment_and_Orders
{
    public class OrderItem
    {
        public int IdItem { get; set; }
        public int IdOrder { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public double Subtotal { get; set; }


    }
}
