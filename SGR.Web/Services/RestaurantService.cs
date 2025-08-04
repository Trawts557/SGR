using SGR.Web.Models;
using SGR.Web.Services.Interfaces;
using System.Text.Json;


namespace SGR.Web.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public RestaurantService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            var baseUrl = _configuration["ApiSettings:BaseUrl"];
            _httpClient.BaseAddress = new Uri(baseUrl!);
        }

        public async Task<List<RestaurantModel>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("Restaurant/GetAllRestaurants");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<BaseResponse<List<RestaurantModel>>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result?.data ?? new List<RestaurantModel>();
        }

        public async Task<RestaurantModel?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"Restaurant/GetRestaurantById?id={id}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<BaseResponse<RestaurantModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result?.data;
        }

        public async Task<bool> CreateAsync(RestaurantModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("Restaurant/CreateRestaurant", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(RestaurantModel model)
        {
            var response = await _httpClient.PutAsJsonAsync("Restaurant/ModifyRestaurant", model);
            return response.IsSuccessStatusCode;
        }
    }
}
