using SGR.Application.Dtos.Restaurant;
using SGR.Application.Services;
using SGR.Tests.Mocks;
using Xunit;

namespace SGR.Application.Test
{
    public class MenuCategoryServiceTests
    {
        private readonly MenuCategoryService _service;

        public MenuCategoryServiceTests()
        {
            var mockRepo = new MenuCategoryRepositoryMock();
            _service = new MenuCategoryService(mockRepo, null!);
        }

        [Fact]
        public async Task AddAsync_Should_Add_MenuCategory()
        {
            var dto = new CreateMenuCategoryDTO
            {
                Name = "Postres",
                Description = "Dulces fríos",
                RestaurantId = 1
            };

            await _service.AddAsync(dto);
            var result = await _service.GetAllAsync();

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Contains(result.Data!, c => c.Name == "Postres");
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_List()
        {
            await _service.AddAsync(new CreateMenuCategoryDTO
            {
                Name = "Bebidas",
                Description = "Refrescos y jugos",
                RestaurantId = 1
            });

            var result = await _service.GetAllAsync();

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data!);
            var list = result.Data!.ToList();
            Assert.Equal("Bebidas", list[0].Name);
        }
    }
}
