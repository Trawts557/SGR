using SGR.Application.Dtos.Restaurant;
using SGR.Application.Dtos.Restaurant_and_Products.MenuCategory;
using SGR.Tests.Mocks;
using Xunit;

namespace SGR.Tests.Repositories
{
    public class MenuCategoryRepositoryTest
    {
        private readonly MenuCategoryRepositoryMock _repository;

        public MenuCategoryRepositoryTest()
        {
            _repository = new MenuCategoryRepositoryMock();
        }

        [Fact]
        public async Task AddAsync_Should_Add_Category()
        {
            var dto = new CreateMenuCategoryDTO { Name = "Postres" };

            var result = await _repository.AddAsync(dto);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_Categories()
        {
            await _repository.AddAsync(new CreateMenuCategoryDTO { Name = "Bebidas" });

            var result = await _repository.GetAllAsync();

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Category()
        {
            await _repository.AddAsync(new CreateMenuCategoryDTO { Name = "Carnes" });

            var result = await _repository.GetByIdAsync(1);

            Assert.True(result.IsSuccess);
            Assert.Equal("Carnes", result.Data!.Name);
        }

        [Fact]
        public async Task UpdateAsync_Should_Modify_Category()
        {
            await _repository.AddAsync(new CreateMenuCategoryDTO { Name = "Vegano" });

            var result = await _repository.UpdateAsync(new ModifyMenuCategoryDTO
            {
                IdMenuCategory = 1,
                Name = "Veganos"
            });

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task DeleteAsync_Should_Remove_Category()
        {
            await _repository.AddAsync(new CreateMenuCategoryDTO { Name = "Ensaladas" });

            var result = await _repository.DeleteAsync(new DisableMenuCategoryDTO
            {
                IdMenuCategory = 1,
                DeletedBy = "test"
            });

            Assert.True(result.IsSuccess);
        }
    }
}
