using SGR.Web.Models;

namespace SGR.Web.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<List<RestaurantModel>> GetAllAsync();
        Task<RestaurantModel?> GetByIdAsync(int id);
        Task<bool> CreateAsync(RestaurantModel model);
        Task<bool> UpdateAsync(RestaurantModel model);
    }
}
