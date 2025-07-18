using SGR.Application.Dtos.Restaurant;
using SGR.Tests.Mocks;
using Xunit;

namespace SGR.Tests.Repositories
{
    public class RestaurantRepositoryTest
    {
        private readonly RestaurantRepositoryMock _repository;

        public RestaurantRepositoryTest()
        {
            _repository = new RestaurantRepositoryMock();
        }

        [Fact]
        public async Task AddAsync_Should_Add_Restaurant()
        {
            var dto = new CreateRestaurantDTO
            {
                Name = "Sushi House",
                Description = "Japanese food",
            };

            var result = await _repository.AddAsync(dto);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task AddAsync_Should_Fail_If_Name_Is_Empty()
        {
            var dto = new CreateRestaurantDTO
            {
                Name = "",
                Description = "No name"
            };

            var result = await _repository.AddAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El nombre del restaurante es obligatorio.", result.Message);
        }

        [Fact]
        public async Task ExistsByName_Should_Return_True_If_Name_Exists()
        {
            await _repository.AddAsync(new CreateRestaurantDTO
            {
                Name = "KFC",
                Description = "Fried chicken",
            });

            var exists = await _repository.ExistsByNameAsync("KFC");
            Assert.True(exists);
        }

        [Fact]
        public async Task ExistsByName_Should_Return_False_If_Name_Does_Not_Exist()
        {
            var exists = await _repository.ExistsByNameAsync("NonExistingRestaurant");
            Assert.False(exists);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Restaurant()
        {
            var createDto = new CreateRestaurantDTO
            {
                Name = "Taco Bell",
                Description = "Mexican food",
            };

            await _repository.AddAsync(createDto);
            var result = await _repository.GetByIdAsync(1);

            Assert.True(result.IsSuccess);
            Assert.Equal("Taco Bell", result.Data!.Name);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Fail_If_Id_Is_Invalid()
        {
            var result = await _repository.GetByIdAsync(-1);

            Assert.False(result.IsSuccess);
            Assert.Equal("ID inválido.", result.Message);
        }

        [Fact]
        public async Task UpdateAsync_Should_Update_Restaurant()
        {
            await _repository.AddAsync(new CreateRestaurantDTO
            {
                Name = "Original Name",
                Description = "Old Description"
            });

            var updateDto = new ModifyRestaurantDTO
            {
                IdRestaurant = 1,
                Name = "Updated Name",
                Description = "New Description"
            };

            var result = await _repository.UpdateAsync(updateDto);

            Assert.True(result.IsSuccess);

            var updated = await _repository.GetByIdAsync(1);
            Assert.Equal("Updated Name", updated.Data!.Name);
        }

        [Fact]
        public async Task UpdateAsync_Should_Fail_If_Id_Is_Invalid()
        {
            var updateDto = new ModifyRestaurantDTO
            {
                IdRestaurant = 0,
                Name = "Any",
                Description = "Any"
            };

            var result = await _repository.UpdateAsync(updateDto);
            Assert.False(result.IsSuccess);
            Assert.Equal("ID inválido.", result.Message);
        }

        [Fact]
        public async Task DeleteAsync_Should_Remove_Restaurant()
        {
            await _repository.AddAsync(new CreateRestaurantDTO
            {
                Name = "Pizza Place",
                Description = "Italian",
            });

            var deleteResult = await _repository.DeleteAsync(new DisableRestaurantDTO
            {
                IdRestaurant = 1,
                DeletedBy = "admin"
            });

            Assert.True(deleteResult.IsSuccess);
        }

        [Fact]
        public async Task DeleteAsync_Should_Fail_If_Id_Is_Invalid()
        {
            var deleteResult = await _repository.DeleteAsync(new DisableRestaurantDTO
            {
                IdRestaurant = 0,
                DeletedBy = "admin"
            });

            Assert.False(deleteResult.IsSuccess);
            Assert.Equal("ID inválido.", deleteResult.Message);
        }
    }
}
