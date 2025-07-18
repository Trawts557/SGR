using SGR.Application.Contracts.Repository;
using SGR.Application.Dtos.Restaurant;
using SGR.Application.Dtos.Restaurant_and_Products.MenuCategory;
using SGR.Domain.Base;

namespace SGR.Tests.Mocks
{
    public class MenuCategoryRepositoryMock : IMenuCategoryRepository
    {
        private readonly List<GetMenuCategoryDTO> _menuCategories = new();
        private int _idCounter = 1;

        public async Task<OperationResult> AddAsync(CreateMenuCategoryDTO dto)
        {
            await Task.CompletedTask;

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return OperationResult.Failure("El nombre es requerido.");
            }

            var newCategory = new GetMenuCategoryDTO(
                _idCounter++,
                dto.Name!,
                dto.Description!,
                 1,
                DateTime.UtcNow
            );

            _menuCategories.Add(newCategory);
            return OperationResult.Success("Categoría creada correctamente.");
        }

        public async Task<OperationResult> DeleteAsync(DisableMenuCategoryDTO dto)
        {
            await Task.CompletedTask;

            var category = _menuCategories.FirstOrDefault(c => c.IdMenuCategory == dto.IdMenuCategory);
            if (category == null)
            {
                return OperationResult.Failure("Categoría no encontrada.");
            }

            _menuCategories.Remove(category);
            return OperationResult.Success("Categoría eliminada correctamente.");
        }

        public async Task<OperationResult<IEnumerable<GetMenuCategoryDTO>>> GetAllAsync()
        {
            await Task.CompletedTask;
            return OperationResult<IEnumerable<GetMenuCategoryDTO>>.Success(_menuCategories);
        }

        public async Task<OperationResult<GetMenuCategoryDTO>> GetByIdAsync(int id)
        {
            await Task.CompletedTask;

            var category = _menuCategories.FirstOrDefault(c => c.IdMenuCategory == id);
            if (category == null)
            {
                return OperationResult<GetMenuCategoryDTO>.Failure("Categoría no encontrada.");
            }

            return OperationResult<GetMenuCategoryDTO>.Success(category);
        }

        public async Task<OperationResult> UpdateAsync(ModifyMenuCategoryDTO dto)
        {
            await Task.CompletedTask;

            var category = _menuCategories.FirstOrDefault(c => c.IdMenuCategory == dto.IdMenuCategory);
            if (category == null)
            {
                return OperationResult.Failure("Categoría no encontrada.");
            }

            category.Name = dto.Name!;
            category.Description = dto.Description!;

            return OperationResult.Success("Categoría actualizada correctamente.");
        }
    }
}
