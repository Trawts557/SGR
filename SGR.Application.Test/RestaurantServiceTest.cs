using SGR.Application.Dtos.Restaurant;
using SGR.Application.Services;
using SGR.Tests.Mocks;
using Xunit;

namespace SGR.Application.Test
{
    public class RestaurantServiceTests
    {
        private readonly RestaurantRepositoryMock _mockRepository;
        private readonly RestaurantService _service;

        public RestaurantServiceTests()
        {
            _mockRepository = new RestaurantRepositoryMock();
            _service = new RestaurantService(_mockRepository, null!);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnList()
        {
            await _service.AddAsync(new CreateRestaurantDTO
            {
                Name = "Pizza Hut",
                Description = "Pizza"
            });

            var result = await _service.GetAllAsync();

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data!);
            var list = result.Data!.ToList();
            Assert.Equal("Pizza Hut", list[0].Name);

        }

        [Fact]
        public async Task AddAsync_ShouldInsertRestaurant()
        {
            var dto = new CreateRestaurantDTO
            {
                Name = "Burger King",
                Description = "Burgers"
            };

            await _service.AddAsync(dto);

            var listResult = await _service.GetAllAsync();

            Assert.True(listResult.IsSuccess);
            Assert.NotNull(listResult.Data);
            Assert.Contains(listResult.Data!, r => r.Name == "Burger King");
        }
    }
}
