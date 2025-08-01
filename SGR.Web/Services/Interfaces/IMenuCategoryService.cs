using SGR.Web.Models;

namespace SGR.Web.Services.Interfaces
{
    public interface IMenuCategoryService
    {
        Task<List<MenuCategoryModel>> GetAllAsync();
        Task<MenuCategoryModel?> GetByIdAsync(int id);
        Task<bool> CreateAsync(MenuCategoryModel model);
        Task<bool> UpdateAsync(MenuCategoryModel model);
    }
}
