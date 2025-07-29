namespace SGR.Web.Models
{
    public class RestaurantModel
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public int idOwner { get; set; }
        public int idRestaurant { get; set; }
        public DateTime createdAt { get; set; }
    }

    public class GetAllRestaurantsResponse
    {
        public bool isSuccess { get; set; }
        public string? message { get; set; }
        public List<RestaurantModel>? data { get; set; }
    }
    public class GetRestaurantResponse
    {
        public bool isSuccess { get; set; }
        public string? message { get; set; }
        public RestaurantModel? data { get; set; }
    }

    public class GetRestaurantByIdResponse
    {
        public bool isSuccess { get; set; }
        public string? message { get; set; }
        public RestaurantModel? data { get; set; }
    }

}
