namespace SGR.Web.Models
{
    public class RestaurantEditModel
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public int idOwner { get; set; }
        public int idRestaurant { get; set; }
        public DateTime createdAt { get; set; }
    }


    public class RestaurantEditResponse
    {
        public bool isSuccess { get; set; }
        public string? message { get; set; }
        public RestaurantModel? data { get; set; }
    }

}
