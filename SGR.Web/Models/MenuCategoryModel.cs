namespace SGR.Web.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class MenuCategoryModel
    {
        public int? idMenuCategory { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public int restaurantId { get; set; }
        public DateTime createdAt { get; set; }
    }

}
