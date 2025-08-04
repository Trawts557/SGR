using System.Text.Json.Serialization;

namespace SGR.Web.Models
{
    public class RestaurantModel
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public int ownerId { get; set; }
        public int idRestaurant { get; set; }
        public DateTime createdAt { get; set; }
    }

}
