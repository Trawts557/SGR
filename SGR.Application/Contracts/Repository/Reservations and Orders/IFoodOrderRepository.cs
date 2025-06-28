using SGR.Application.Dtos.Reservation_and_Orders.FoodOrder;
using SGR.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Application.Contracts.Repository
{
    public interface IFoodOrderRepository
    {
        
           Task<OperationResult> AddAsync(CreateFoodOrderDTO dto);
           Task<OperationResult> UpdateStatusAsync(UpdateFoodOrderStatusDTO dto);
           Task<OperationResult> GetAllAsync();
           Task<OperationResult> GetByIdAsync(int id);
           
    }
}
