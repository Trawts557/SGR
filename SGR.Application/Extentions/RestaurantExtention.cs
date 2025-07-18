using SGR.Application.Dtos.Restaurant;
using SGR.Domain.Entities;
using SGR.Domain.Entities.Restaurants_and_Products;

namespace SGR.Application.Extensions
{
    public static class RestaurantExtension
    {
        public static Restaurant ToEntity(this CreateRestaurantDTO dto)
        {
            return new Restaurant
            {
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
        }

        public static Restaurant ToEntity(this ModifyRestaurantDTO dto)
        {
            return new Restaurant
            {
                IdRestaurant = dto.IdRestaurant,
                Name = dto.Name,
                Description = dto.Description,
                UpdatedBy = dto.UpdatedBy,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public static Restaurant ToEntity(this DisableRestaurantDTO dto)
        {
            return new Restaurant
            {
                IdRestaurant = dto.IdRestaurant,
                DeletedBy = dto.DeletedBy,
                DeletedAt = DateTime.UtcNow,
                IsDeleted = true
            };
        }
    }
}
