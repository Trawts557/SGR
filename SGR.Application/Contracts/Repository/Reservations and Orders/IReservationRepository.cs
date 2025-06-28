using SGR.Application.Dtos.Reservation_and_Orders;
using SGR.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Application.Contracts.Repository.Reservations_and_Orders
{
    public interface IReservationRepository
    {
        
        Task<OperationResult> AddAsync(CreateReservationDTO dto);
        Task<OperationResult> UpdateAsync(ModifyReservationDTO dto);
        Task<OperationResult> CancelAsync(CancelReservationDTO dto);
        Task<OperationResult> GetAllAsync();
        Task<OperationResult> GetByIdAsync(int id);
        
    }
}
