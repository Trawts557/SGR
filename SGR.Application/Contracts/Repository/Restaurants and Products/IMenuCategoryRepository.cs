using SGR.Application.Dtos.Restaurant;
using SGR.Application.Dtos.Restaurant_and_Products.MenuCategory;
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
            Task<OperationResult<IEnumerable<GetMenuCategoryDTO>>> GetAllAsync();
            Task<OperationResult<GetMenuCategoryDTO>> GetByIdAsync(int id);
            Task<OperationResult> AddAsync(CreateMenuCategoryDTO dto);
            Task<OperationResult> UpdateAsync(ModifyMenuCategoryDTO dto);
            Task<OperationResult> DeleteAsync(DisableMenuCategoryDTO dto);


    }

}
