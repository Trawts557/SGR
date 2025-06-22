using SGR.Application.Dtos.Restaurant;
using SGR.Domain.Base;
using SGR.Domain.Entities.Restaurants_and_Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Application.Interfaces.Repository 
{
    public interface IRestaurantRepository 
    {
        Task<OperationResult> GetByIdAsync(int id);
        Task<OperationResult> GetAllAsync();
        Task<OperationResult> AddAsync(CreateRestaurantDTO createRestaurantDTO);
        Task<OperationResult> UpdateAsync(ModifyRestaurantDTO modifyRestaurantDTO);
        Task<OperationResult> DeleteAsync(DisableRestaurantDTO disableRestaurantDTO);
    }
}
