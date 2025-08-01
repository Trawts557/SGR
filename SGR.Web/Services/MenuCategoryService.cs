using SGR.Web.Models;
using SGR.Web.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace SGR.Web.Services
{
    public class MenuCategoryService : IMenuCategoryService
    {
        private readonly HttpClient _httpClient;

        public MenuCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7106/api/");
        }

        public async Task<List<MenuCategoryModel>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("MenuCategory/GetAllMenuCategory");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GetAllMenuCategoryResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result?.data ?? new List<MenuCategoryModel>();
        }

        public async Task<MenuCategoryModel?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"MenuCategory/GetMenuCategoryById?id={id}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GetMenuCategoryResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result?.data;
        }

        public async Task<bool> CreateAsync(MenuCategoryModel model)
        {
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("MenuCategory/CreateMenuCategory", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(MenuCategoryModel model)
        {
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("MenuCategory/ModifyMenuCategory", content);
            return response.IsSuccessStatusCode;
        }
    }
}
