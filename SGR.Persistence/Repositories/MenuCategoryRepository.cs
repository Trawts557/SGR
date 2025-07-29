using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using SGR.Application.Contracts.Repository;
using SGR.Application.Dtos.Restaurant;
using SGR.Application.Dtos.Restaurant_and_Products.MenuCategory;
using SGR.Domain.Base;
using System.Data;

namespace SGR.Persistence.Repositories
{
    public class MenuCategoryRepository : IMenuCategoryRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<MenuCategoryRepository> _logger;

        public MenuCategoryRepository(IConfiguration configuration, ILogger<MenuCategoryRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException(nameof(_connectionString), "La cadena de conexión no está configurada.");

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

                command.Parameters.AddWithValue("@p_Name", dto.Name);
                command.Parameters.AddWithValue("@p_Description", dto.Description);
                command.Parameters.AddWithValue("@p_RestaurantId", dto.RestaurantId);
                command.Parameters.AddWithValue("@p_CreatedAt", dto.CreatedAt);

                var pResult = new MySqlParameter("@pResult", MySqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(pResult);

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

                command.Parameters.AddWithValue("@p_Id", dto.IdMenuCategory);
                command.Parameters.AddWithValue("@p_Name", dto.Name);
                command.Parameters.AddWithValue("@p_Description", dto.Description);
                command.Parameters.AddWithValue("@p_UpdatedBy", dto.UpdatedBy);

                var pResult = new MySqlParameter("@pResult", MySqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(pResult);

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

                command.Parameters.AddWithValue("@p_Id", dto.IdMenuCategory);
                command.Parameters.AddWithValue("@p_DeletedBy", dto.DeletedBy);

                var pResult = new MySqlParameter("@pResult", MySqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(pResult);

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

        public async Task<OperationResult<IEnumerable<GetMenuCategoryDTO>>> GetAllAsync()
        {
            var result = new OperationResult<IEnumerable<GetMenuCategoryDTO>>();
            var categories = new List<GetMenuCategoryDTO>();

            try
            {
                _logger.LogInformation("Obteniendo todas las categorías de menú");

                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_GetAllMenuCategories", connection);
                command.CommandType = CommandType.StoredProcedure;

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    categories.Add(new GetMenuCategoryDTO(
                        reader.GetInt32("IdMenuCategory"),
                        reader.GetString("Name"),
                        reader.GetString("Description"),
                        reader.GetInt32("RestaurantId"),
                        reader.GetDateTime("CreatedAt")
                    ));
                }

                result.IsSuccess = true;
                result.Message = "Categorías obtenidas correctamente";
                result.Data = categories;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Error al obtener categorías: {ex.Message}";
                _logger.LogError(ex, "Error en GetAllAsync");
            }

            return result;
        }

        public async Task<OperationResult<GetMenuCategoryDTO>> GetByIdAsync(int id)
        {
            var result = new OperationResult<GetMenuCategoryDTO>();

            try
            {
                _logger.LogInformation("Obteniendo categoría de menú por ID: {IdMenuCategory}", id);

                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_GetMenuCategoryById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@p_Id", id);

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    var category = new GetMenuCategoryDTO(
                        reader.GetInt32("IdMenuCategory"),
                        reader.GetString("Name"),
                        reader.GetString("Description"),
                        reader.GetInt32("RestaurantId"),
                        reader.GetDateTime("CreatedAt")
                    );

                    result.IsSuccess = true;
                    result.Data = category;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Categoría no encontrada";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Error al obtener la categoría: {ex.Message}";
                _logger.LogError(ex, "Error en GetByIdAsync");
            }

            return result;
        }


    }
}
