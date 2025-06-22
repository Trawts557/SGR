using SGR.Application.Dtos.Restaurant_and_Products.Products;
using SGR.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Application.Contracts.Repository
{
    public interface IProductRepository
    {
        Task<OperationResult> AddAsync(CreateProductDTO dto);
        Task<OperationResult> UpdateAsync(ModifyProductDTO dto);
        Task<OperationResult> DeleteAsync(DisableProductDTO dto);
        Task<OperationResult> GetAllAsync();
        Task<OperationResult> GetByIdAsync(int id);
    }
}
