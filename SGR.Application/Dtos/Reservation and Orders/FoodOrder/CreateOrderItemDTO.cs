using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Application.Dtos.Reservation_and_Orders.FoodOrder
{
    public record CreateOrderItemDTO
    {
        public int FoodOrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }  
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}
