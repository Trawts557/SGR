using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Application.Dtos.Reservation_and_Orders
{
    public record ModifyReservationDTO
    {
        public int IdReservation { get; set; }
        public string? Status { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
