using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using SGR.Application.Dtos.Restaurant;
using SGR.Application.Interfaces.Repository;
using SGR.Domain.Base;
using System.Data;

namespace SGR.Persistence.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<RestaurantRepository> _logger;

        public RestaurantRepository(IConfiguration configuration, ILogger<RestaurantRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException(nameof(_connectionString), "La cadena de conexión no está configurada.");
            _logger = logger;
        }

        public async Task<OperationResult> AddAsync(CreateRestaurantDTO createRestaurantDTO)
        {
            var result = new OperationResult();
            try
            {
                if (createRestaurantDTO is null || string.IsNullOrEmpty(createRestaurantDTO.Name))
                    return new OperationResult { IsSuccess = false, Message = "Datos inválidos" };

                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_CreateRestaurant", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@p_Name", createRestaurantDTO.Name);
                command.Parameters.AddWithValue("@p_Description", createRestaurantDTO.Description);
                command.Parameters.AddWithValue("@p_OwnerId", createRestaurantDTO.OwnerId);
                command.Parameters.AddWithValue("@p_CreatedAt", createRestaurantDTO.CreatedAt);

                var pResult = new MySqlParameter("@pResult", MySqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(pResult);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                result.IsSuccess = true;
                result.Message = pResult.Value?.ToString() ?? "Restaurante creado con éxito";
                _logger.LogInformation("Restaurante creado correctamente");
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Error: {ex.Message}";
                _logger.LogError(ex, "Error al crear restaurante");
            }

            return result;
        }

        public async Task<OperationResult> DeleteAsync(DisableRestaurantDTO dto)
        {
            var result = new OperationResult();
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_DisableRestaurant", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@p_Id", dto.IdRestaurant);
                command.Parameters.AddWithValue("@p_DeletedBy", dto.DeletedBy);

                await connection.OpenAsync();
                var affected = await command.ExecuteNonQueryAsync();

                result.IsSuccess = affected > 0;
                result.Message = affected > 0 ? "Restaurante deshabilitado" : "No se encontró el restaurante";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Error: {ex.Message}";
                _logger.LogError(ex, "Error al deshabilitar restaurante");
            }

            return result;
        }

        public async Task<OperationResult<IEnumerable<GetRestaurantDTO>>> GetAllAsync()
        {
            var result = new OperationResult<IEnumerable<GetRestaurantDTO>>();
            var list = new List<GetRestaurantDTO>();

            try
            {
                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_GetAllRestaurants", connection);
                command.CommandType = CommandType.StoredProcedure;

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    list.Add(new GetRestaurantDTO(
                        reader.GetString("Name"),
                        reader.GetString("Description"),
                        reader.GetInt32("OwnerId"),
                        reader.GetInt32("IdRestaurant"),
                        reader.GetDateTime("CreatedAt")
                    ));
                }

                result.IsSuccess = true;
                result.Data = list;
                result.Message = "Datos obtenidos";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Error: {ex.Message}";
                _logger.LogError(ex, "Error al obtener restaurantes");
            }

            return result;
        }

        public async Task<OperationResult<GetRestaurantDTO>> GetByIdAsync(int id)
        {
            var result = new OperationResult<GetRestaurantDTO>();

            try
            {
                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_GetRestaurantById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    result.IsSuccess = true;
                    result.Data = new GetRestaurantDTO(
                        reader.GetString("Name"),
                        reader.GetString("Description"),
                        reader.GetInt32("OwnerId"),
                        reader.GetInt32("IdRestaurant"),
                        reader.GetDateTime("CreatedAt")
                    );
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Restaurante no encontrado";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Error: {ex.Message}";
                _logger.LogError(ex, "Error al buscar restaurante");
            }

            return result;
        }

        public async Task<OperationResult> UpdateAsync(ModifyRestaurantDTO dto)
        {
            var result = new OperationResult();
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_UpdateRestaurant", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@p_Id", dto.IdRestaurant);
                command.Parameters.AddWithValue("@p_Name", dto.Name);
                command.Parameters.AddWithValue("@p_Description", dto.Description);
                command.Parameters.AddWithValue("@p_UpdatedBy", dto.UpdatedBy);

                var pResult = new MySqlParameter("@pResult", MySqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(pResult);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                result.IsSuccess = true;
                result.Message = pResult.Value?.ToString() ?? "Restaurante actualizado";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Error: {ex.Message}";
                _logger.LogError(ex, "Error al actualizar restaurante");
            }

            return result;
        }

    }
}
