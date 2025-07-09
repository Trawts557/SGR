using SGR.Application.Dtos.Restaurant;
using SGR.Domain.Base;

namespace SGR.Application.Contracts.BusinessLogic
{
    public interface IRestaurantService
    {
        Task<OperationResult> AddAsync(CreateRestaurantDTO dto);
        Task<OperationResult> UpdateAsync(ModifyRestaurantDTO dto);
        Task<OperationResult> DeleteAsync(DisableRestaurantDTO dto);
        Task<OperationResult<IEnumerable<GetRestaurantDTO>>> GetAllAsync();
        Task<OperationResult<GetRestaurantDTO>> GetByIdAsync(int id);
    }
}
