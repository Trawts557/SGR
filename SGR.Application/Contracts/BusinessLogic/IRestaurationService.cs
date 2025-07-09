using SGR.Application.Dtos.Reservation_and_Orders;
using SGR.Domain.Base;

namespace SGR.Application.Contracts.BusinessLogic
{
    public interface IReservationService
    {
        Task<OperationResult> AddAsync(CreateReservationDTO dto);
        Task<OperationResult> CancelAsync(CancelReservationDTO dto);
        Task<OperationResult<IEnumerable<GetReservationDTO>>> GetAllAsync();
        Task<OperationResult<GetReservationDTO>> GetByIdAsync(int id);
    }
}
