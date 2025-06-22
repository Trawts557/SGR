using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using SGR.Application.Contracts.Repository;
using SGR.Application.Dtos.Restaurant;
using SGR.Application.Interfaces.Repository;
using SGR.Domain.Base;
using System.Data;

namespace SGR.Persistence.Repositories
{
    public class MenuCategoryRepository : IMenuCategoryRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<MenuCategoryRepository> _logger;

        public MenuCategoryRepository(string connectionString, ILogger<MenuCategoryRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public async Task<OperationResult> AddAsync(CreateMenuCategoryDTO dto)
        {
            var result = new OperationResult();

            try
            {
                _logger.LogInformation("Creando nueva categoría de menú.");

                if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
                {
                    result.IsSuccess = false;
                    result.Message = "Nombre de categoría inválido.";
                    return result;
                }

                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_CreateMenuCategory", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Name", dto.Name);
                command.Parameters.AddWithValue("@RestaurantId", dto.RestaurantId);
                command.Parameters.AddWithValue("@CreatedAt", dto.CreatedAt);

                await connection.OpenAsync();
                var affected = await command.ExecuteNonQueryAsync();

                if (affected > 0)
                {
                    result.IsSuccess = true;
                    result.Message = "Categoría creada con éxito.";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "No se pudo crear la categoría.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear categoría de menú.");
                result.IsSuccess = false;
                result.Message = $"Error al crear categoría: {ex.Message}";
            }

            return result;
        }

        public async Task<OperationResult> UpdateAsync(ModifyMenuCategoryDTO dto)
        {
            var result = new OperationResult();

            try
            {
                _logger.LogInformation("Actualizando categoría de menú ID: {Id}", dto.IdMenuCategory);

                if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
                {
                    result.IsSuccess = false;
                    result.Message = "Datos de entrada inválidos.";
                    return result;
                }

                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_UpdateMenuCategory", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", dto.IdMenuCategory);
                command.Parameters.AddWithValue("@Name", dto.Name);
                command.Parameters.AddWithValue("@UpdatedBy", dto.UpdatedBy);

                await connection.OpenAsync();
                var affected = await command.ExecuteNonQueryAsync();

                if (affected > 0)
                {
                    result.IsSuccess = true;
                    result.Message = "Categoría actualizada con éxito.";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "No se pudo actualizar la categoría.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar categoría.");
                result.IsSuccess = false;
                result.Message = $"Error al actualizar categoría: {ex.Message}";
            }

            return result;
        }

        public async Task<OperationResult> DeleteAsync(DisableMenuCategoryDTO dto)
        {
            var result = new OperationResult();

            try
            {
                _logger.LogInformation("Deshabilitando categoría ID: {Id}", dto.IdMenuCategory);

                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_DisableMenuCategory", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", dto.IdMenuCategory);
                command.Parameters.AddWithValue("@DeletedBy", dto.DeletedBy);

                await connection.OpenAsync();
                var affected = await command.ExecuteNonQueryAsync();

                if (affected > 0)
                {
                    result.IsSuccess = true;
                    result.Message = "Categoría deshabilitada correctamente.";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "No se encontró la categoría o ya estaba deshabilitada.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al deshabilitar categoría.");
                result.IsSuccess = false;
                result.Message = $"Error al deshabilitar categoría: {ex.Message}";
            }

            return result;
        }

        public Task<OperationResult> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
