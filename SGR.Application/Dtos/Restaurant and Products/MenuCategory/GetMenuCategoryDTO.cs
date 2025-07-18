using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Application.Dtos.Restaurant_and_Products.MenuCategory
{
    public record GetMenuCategoryDTO
    {
        public int IdMenuCategory { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int RestaurantId { get; set; }
        public DateTime CreatedAt { get; set; }

        public GetMenuCategoryDTO(int idMenuCategory, string? name, string? description, int restaurantId, DateTime createdAt)
        {
            IdMenuCategory = idMenuCategory;
            Name = name;
            Description = description;
            RestaurantId = restaurantId;
            CreatedAt = createdAt;
        }

        public GetMenuCategoryDTO(){ }
    }

}
