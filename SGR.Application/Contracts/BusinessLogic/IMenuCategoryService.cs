using SGR.Application.Dtos.Restaurant;
using SGR.Application.Dtos.Restaurant_and_Products.MenuCategory;
using SGR.Domain.Base;

namespace SGR.Application.Contracts.BusinessLogic
{
    public interface IMenuCategoryService
    {
        Task<OperationResult> AddAsync(CreateMenuCategoryDTO dto);
        Task<OperationResult> UpdateAsync(ModifyMenuCategoryDTO dto);
        Task<OperationResult> DeleteAsync(DisableMenuCategoryDTO dto);
        Task<OperationResult<IEnumerable<GetMenuCategoryDTO>>> GetAllAsync();
        Task<OperationResult<GetMenuCategoryDTO>> GetByIdAsync(int id);
    }
}
