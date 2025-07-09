using Microsoft.Extensions.Logging;
using SGR.Application.Contracts.BusinessLogic;
using SGR.Application.Contracts.Repository;
using SGR.Application.Dtos.Restaurant;
using SGR.Application.Dtos.Restaurant_and_Products.MenuCategory;
using SGR.Domain.Base;

namespace SGR.Application.Services
{
    public class MenuCategoryService : IMenuCategoryService
    {
        private readonly IMenuCategoryRepository _repository;
        private readonly ILogger<MenuCategoryService> _logger;

        public MenuCategoryService(IMenuCategoryRepository repository, ILogger<MenuCategoryService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<OperationResult> AddAsync(CreateMenuCategoryDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return OperationResult.Failure("El nombre de la categoría es obligatorio.");

            return await _repository.AddAsync(dto);
        }

        public async Task<OperationResult> UpdateAsync(ModifyMenuCategoryDTO dto)
        {
            if (dto.IdMenuCategory <= 0)
                return OperationResult.Failure("ID inválido.");

            return await _repository.UpdateAsync(dto);
        }

        public async Task<OperationResult> DeleteAsync(DisableMenuCategoryDTO dto)
        {
            if (dto.IdMenuCategory <= 0)
                return OperationResult.Failure("ID inválido.");

            return await _repository.DeleteAsync(dto);
        }

        public async Task<OperationResult<IEnumerable<GetMenuCategoryDTO>>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<OperationResult<GetMenuCategoryDTO>> GetByIdAsync(int id)
        {
            if (id <= 0)
                return OperationResult<GetMenuCategoryDTO>.Failure("ID inválido.");

            return await _repository.GetByIdAsync(id);
        }
    }
}
