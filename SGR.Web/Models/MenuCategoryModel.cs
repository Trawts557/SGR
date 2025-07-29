namespace SGR.Web.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class MenuCategoryModel
    {
        public int idMenuCategory { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public int restaurantId { get; set; }
        public DateTime createdAt { get; set; }
    }
    public class GetAllMenuCategoryResponse
    {
        public bool isSuccess { get; set; }
        public string? message { get; set; }
        public List<MenuCategoryModel>? data { get; set; }
    }
    public class GetMenuCategoryResponse
    {   
        public bool isSuccess { get; set; }
        public string? message { get; set; }
        public MenuCategoryModel? data { get; set; }
    }


}
