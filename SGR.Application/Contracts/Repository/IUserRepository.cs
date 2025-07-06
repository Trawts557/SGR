using SGR.Application.Dtos.Restaurant_and_Products.MenuCategory;
using SGR.Application.Dtos.User.SGR.Application.Dtos.Users;
using SGR.Application.Dtos.Users;
using SGR.Domain.Base;
namespace SGR.Application.Contracts.Repository
{
    public interface IUserRepository
    {

        Task<OperationResult<IEnumerable<GetUserDTO>>> GetAllAsync();
        Task<OperationResult> RegisterCustomerAsync(RegisterCustomerDTO dto);
        Task<OperationResult> RegisterOwnerAsync(RegisterOwnerDTO dto);
        Task<OperationResult> RegisterAdminAsync(RegisterAdminDTO dto);
    }
}
