using SGR.Web.Models;
using SGR.Web.Services.Interfaces;
using System.Text.Json;
using System.Net.Http.Json;

namespace SGR.Web.Services
{
    public class MenuCategoryService : IMenuCategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public MenuCategoryService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            var baseUrl = _configuration["ApiSettings:BaseUrl"];
            _httpClient.BaseAddress = new Uri(baseUrl!);
        }

        public async Task<List<MenuCategoryModel>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("MenuCategory/GetAllMenuCategory");

            if (!response.IsSuccessStatusCode)
            {
                return new List<MenuCategoryModel>();
            }

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<BaseResponse<List<MenuCategoryModel>>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result?.data ?? new List<MenuCategoryModel>();
        }

        public async Task<MenuCategoryModel?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"MenuCategory/GetMenuCategoryById?id={id}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<BaseResponse<MenuCategoryModel>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result?.data;
        }

        public async Task<bool> CreateAsync(MenuCategoryModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("MenuCategory/CreateMenuCategory", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(MenuCategoryModel model)
        {
            var response = await _httpClient.PutAsJsonAsync("MenuCategory/ModifyMenuCategory", model);
            return response.IsSuccessStatusCode;
        }
    }
}
