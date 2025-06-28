using SGR.Application.Dtos.Reservation_and_Orders.FoodOrder;
using SGR.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Application.Contracts.Repository
{
    public interface IOrderItemRepository
    {
        
        Task<OperationResult> AddAsync(CreateOrderItemDTO dto);
        Task<OperationResult> RemoveAsync(int id);
        Task<OperationResult> GetByOrderIdAsync(int orderId);
        
    }
}
