using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Application.Dtos.Restaurant
{
    public record CreateMenuCategoryDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; } 
        public int RestaurantId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
