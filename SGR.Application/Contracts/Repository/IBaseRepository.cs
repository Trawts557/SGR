using SGR.Domain.Base;
using SGR.Domain.Entities.Restaurants_and_Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Application.Interfaces.Repository
{
        public interface IBaseRepository<T> where T : class
        {
            Task<OperationResult<T>> GetByIdAsync(int id);
            Task<OperationResult<IEnumerable<T>>> GetAllAsync();
            Task<OperationResult> AddAsync(T entity);
            Task<OperationResult> UpdateAsync(T entity);
            Task<OperationResult> DeleteAsync(int id);
        }

    }
