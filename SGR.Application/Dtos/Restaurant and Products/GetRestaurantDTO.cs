
namespace SGR.Application.Dtos.Restaurant
{
    public record GetRestaurantDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int IdOwner { get; set; }
        public int IdRestaurant { get; set; }
        public DateTime CreatedAt { get; set; }

        public GetRestaurantDTO() { }

        public GetRestaurantDTO(string? name, string? description, int idOwner, int idRestaurant, DateTime createdAt)
        {
            Name = name;
            Description = description;
            IdOwner = idOwner;
            IdRestaurant = idRestaurant;
            CreatedAt = createdAt;
        }
    }
}
