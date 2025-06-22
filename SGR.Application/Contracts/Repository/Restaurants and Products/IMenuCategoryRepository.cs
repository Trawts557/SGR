using SGR.Application.Dtos.Restaurant;
using SGR.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Application.Contracts.Repository
{
    public interface IMenuCategoryRepository
    {
        Task<OperationResult> AddAsync(CreateMenuCategoryDTO dto);
        Task<OperationResult> UpdateAsync(ModifyMenuCategoryDTO dto);
        Task<OperationResult> DeleteAsync(DisableMenuCategoryDTO dto);
        Task<OperationResult> GetAllAsync();
        Task<OperationResult> GetByIdAsync(int id);
    }
}
