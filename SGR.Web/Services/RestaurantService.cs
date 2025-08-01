using SGR.Web.Models;
using SGR.Web.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace SGR.Web.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly HttpClient _httpClient;

        public RestaurantService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7106/api/");
        }

        public async Task<List<RestaurantModel>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("Restaurant/GetAllRestaurants");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GetAllRestaurantsResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result?.data ?? new List<RestaurantModel>();
        }

        public async Task<RestaurantModel?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"Restaurant/GetRestaurantById?id={id}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GetRestaurantByIdResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result?.data;
        }

        public async Task<bool> CreateAsync(RestaurantModel model)
        {
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("Restaurant/CreateRestaurant", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(RestaurantEditModel model)
        {
            var response = await _httpClient.PutAsJsonAsync("Restaurant/ModifyRestaurant", model);
            return response.IsSuccessStatusCode;
        }
    }
}
