using SGR.Application.Dtos.User;
using SGR.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Application.Contracts.Repository
{
    public interface IUserRepository
    {
        
        Task<OperationResult> GetByIdAsync(int id);
        Task<OperationResult> RegisterCustomerAsync(RegisterCustomerDTO dto);
        Task<OperationResult> RegisterOwnerAsync(RegisterOwnerDTO dto);
        Task<OperationResult> RegisterAdminAsync(RegisterAdminDTO dto);
    }
}
