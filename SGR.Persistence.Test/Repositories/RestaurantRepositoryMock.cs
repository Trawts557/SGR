using SGR.Application.Dtos.Restaurant;
using SGR.Application.Interfaces.Repository;
using SGR.Domain.Base;

namespace SGR.Tests.Mocks
{
    public class RestaurantRepositoryMock : IRestaurantRepository
    {
        private readonly List<GetRestaurantDTO> _restaurants = new();

        public Task<OperationResult> AddAsync(CreateRestaurantDTO dto)
        {
            var newRestaurant = new GetRestaurantDTO
            {
                IdRestaurant = _restaurants.Count + 1,
                Name = dto.Name,
                Description = dto.Description
            }; 

            _restaurants.Add(newRestaurant);

            return Task.FromResult(OperationResult.Success("Restaurante añadido."));
        }

        public Task<OperationResult> DeleteAsync(DisableRestaurantDTO dto)
        {
            var restaurant = _restaurants.FirstOrDefault(r => r.IdRestaurant == dto.IdRestaurant);
            if (restaurant == null)
                return Task.FromResult(OperationResult.Failure("No existe."));

            _restaurants.Remove(restaurant);
            return Task.FromResult(OperationResult.Success("Restaurante eliminado."));
        }

        public Task<OperationResult<IEnumerable<GetRestaurantDTO>>> GetAllAsync()
        {
            return Task.FromResult(OperationResult<IEnumerable<GetRestaurantDTO>>.Success(_restaurants));
        }

        public Task<OperationResult<GetRestaurantDTO>> GetByIdAsync(int id)
        {
            var restaurant = _restaurants.FirstOrDefault(r => r.IdRestaurant == id);
            if (restaurant == null)
                return Task.FromResult(OperationResult<GetRestaurantDTO>.Failure("No encontrado."));

            return Task.FromResult(OperationResult<GetRestaurantDTO>.Success(restaurant));
        }

        public Task<OperationResult> UpdateAsync(ModifyRestaurantDTO dto)
        {
            var restaurant = _restaurants.FirstOrDefault(r => r.IdRestaurant == dto.IdRestaurant);
            if (restaurant == null)
                return Task.FromResult(OperationResult.Failure("No encontrado."));

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;

            return Task.FromResult(OperationResult.Success("Actualizado."));
        }

        public Task<bool> ExistsByNameAsync(string name)
        {
            var exists = _restaurants.Any(r => r.Name == name);
            return Task.FromResult(exists);
        }
    }
}
