using Microsoft.Extensions.Logging;
using SGR.Application.Contracts.BusinessLogic;
using SGR.Application.Contracts.Repository;
using SGR.Application.Dtos.Restaurant;
using SGR.Application.Interfaces.Repository;
using SGR.Domain.Base;

namespace SGR.Application.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _repository;
        private readonly ILogger<RestaurantService> _logger;

        public RestaurantService(IRestaurantRepository repository, ILogger<RestaurantService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<OperationResult> AddAsync(CreateRestaurantDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return OperationResult.Failure("El nombre del restaurante es obligatorio.");

            if (await _repository.ExistsByNameAsync(dto.Name))
                return OperationResult.Failure("Ya existe un restaurante con ese nombre.");

            return await _repository.AddAsync(dto);
        }


        public async Task<OperationResult> UpdateAsync(ModifyRestaurantDTO dto)
        {
            if (dto.IdRestaurant <= 0)
                return OperationResult.Failure("ID inválido.");

            return await _repository.UpdateAsync(dto);
        }

        public async Task<OperationResult> DeleteAsync(DisableRestaurantDTO dto)
        {
            if (dto.IdRestaurant <= 0)
                return OperationResult.Failure("ID inválido.");

            return await _repository.DeleteAsync(dto);
        }

        public async Task<OperationResult<IEnumerable<GetRestaurantDTO>>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<OperationResult<GetRestaurantDTO>> GetByIdAsync(int id)
        {
            if (id <= 0)
                return OperationResult<GetRestaurantDTO>.Failure("ID inválido.");

            return await _repository.GetByIdAsync(id);
        }
    }
}
