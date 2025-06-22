using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Domain.Entities.Reservations__Payment_and_Orders
{
    public class Reservation
    {
        public int IdReservation { get; set; }
        public DateTime dateTime { get; set; }
        public int PeopleCount { get; set; }
        public string? Status { get; set; }
        public int IdCustomer { get; set; }
        public int IdRestaurant { get; set; }

    }
}
