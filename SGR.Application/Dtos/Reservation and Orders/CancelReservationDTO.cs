using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Application.Dtos.Reservation_and_Orders
{
    public record CancelReservationDTO
    {
        public int IdReservation { get; set; }
        public string? DeletedBy { get; set; }
    }
}
