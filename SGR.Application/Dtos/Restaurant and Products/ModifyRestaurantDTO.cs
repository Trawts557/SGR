using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Application.Dtos.Restaurant
{
    public record ModifyRestaurantDTO
    {
        public int IdRestaurant { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
