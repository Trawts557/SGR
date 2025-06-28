using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Application.Dtos.Reservation_and_Orders.FoodOrder
{
    public record UpdateFoodOrderStatusDTO
    {
        public int IdFoodOrder { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
