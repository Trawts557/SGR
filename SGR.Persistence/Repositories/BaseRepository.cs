
using Azure;
using Microsoft.EntityFrameworkCore;
using SGR.Application.Interfaces.Repository;
using SGR.Domain.Base;
using SGR.Persistence.IContext;

namespace SGR.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly IContext.SGR_DB _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(IContext.SGR_DB context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<OperationResult<T>> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity == null
                ? OperationResult<T>.Failure("Entity not found.")
                : OperationResult<T>.Success(entity);
        }

        public async Task<OperationResult<IEnumerable<T>>> GetAllAsync()
        {
            var list = await _dbSet.ToListAsync();
            return OperationResult<IEnumerable<T>>.Success(list);
        }

        public async Task<OperationResult> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return OperationResult.Success();
        }

        public async Task<OperationResult> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(); 
            return OperationResult.Success("Entity updated");
        }


        public async Task<OperationResult> DeleteAsync(int id)
        {
            var result = await GetByIdAsync(id);
            if (!result.IsSuccess || result.Data == null)
                return OperationResult.Failure("Entity not found.");

            _dbSet.Remove(result.Data);
            return OperationResult.Success();
        }

        Task<OperationResult<T>> IBaseRepository<T>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<OperationResult<IEnumerable<T>>> IBaseRepository<T>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<OperationResult> IBaseRepository<T>.AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        Task<OperationResult> IBaseRepository<T>.UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        Task<OperationResult> IBaseRepository<T>.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }

}
