
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using SGR.Application.Dtos.Restaurant;
using SGR.Application.Interfaces.Repository;
using SGR.Domain.Base;
using SGR.Domain.Entities.Restaurants_and_Products;
using SGR.Persistence.IContext;
using System.Data;
using System.Runtime.InteropServices;

namespace SGR.Persistence.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {

        private readonly string _connectionString;
        public readonly IConfiguration _configuration;
        private readonly ILogger<RestaurantRepository> _logger;

        //Constructor
        public RestaurantRepository(IConfiguration configuration, ILogger<RestaurantRepository> logger)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException(nameof(_connectionString), "La cadena de conexión no está configurada.");
            _logger = logger;
        }


        //AÑADIR RESTAURANTE
        public async Task<OperationResult> AddAsync(CreateRestaurantDTO createRestaurantDTO)
        {
            OperationResult presult = new OperationResult();
            try
            {
                _logger.LogInformation("Añadiendo nuevo restaurante");

                //Validaciones de campo name    
                if (createRestaurantDTO is null)
                {
                    presult.IsSuccess = false;
                    presult.Message = "CreateRestaurantDto no puede ser null";
                    return presult;

                }

                if (string.IsNullOrEmpty(createRestaurantDTO.Name))
                {
                    presult.IsSuccess = false;
                    presult.Message = "Name no puede ser null";
                    return presult;

                }

                if (createRestaurantDTO.Name.Length > 100)
                {
                    presult.IsSuccess = false;
                    presult.Message = "Name no puede tener mas de 100 caracteres";
                    return presult;
                        
                }

                //Validacion de campo Descripcion

                if (string.IsNullOrEmpty(createRestaurantDTO.Description))
                {
                    presult.IsSuccess = false;
                    presult.Message = "Description no puede ser null";
                    return presult;

                }

                using (var context = new MySqlConnection(_connectionString))
                {
                    using (var command = new MySqlCommand("sp_CreateRestaurant", context))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Name", createRestaurantDTO.Name);
                        command.Parameters.AddWithValue("@Description", createRestaurantDTO.Description);
                        command.Parameters.AddWithValue("@OwnerId", createRestaurantDTO.OwnerId);
                        command.Parameters.AddWithValue("@CreatedAt", createRestaurantDTO.CreatedAt);

                        
                        MySqlParameter p_result = new MySqlParameter("@presult", System.Data.SqlDbType.VarChar)
                        {
                            Direction = System.Data.ParameterDirection.Output
                        };                
                        
                        
                        await context.OpenAsync();
                        var result = await command.ExecuteNonQueryAsync();

                        if (result > 0)
                        {
                            presult.IsSuccess = true;
                            presult.Message = "Restaurante creado con exito";
                            _logger.LogInformation("Restaurante {Name} creado con exito", result);
                        }
                        else
                        {
                            presult.IsSuccess = false;
                            presult.Message = "Fallo al crear el Restaurante";
                            _logger.LogError("Fallo al crear el Restaurante");
                        }
                        return presult;
                    }
                }
            }

            catch (Exception ex)
            {
                presult.IsSuccess = false;
                presult.Message = $"Fallo al crear el Restaurante: {ex.Message}";
                _logger.LogError(ex, "Fallo al crear el Restaurante {Message}", ex.Message);
            }
            return presult;
        }

        //BORRAR RESTAURANTE
        public async Task<OperationResult> DeleteAsync(DisableRestaurantDTO dto)
        {
            var result = new OperationResult();
            try
            {
                _logger.LogInformation("Deshabilitando restaurante ID: {IdRestaurant}", dto.IdRestaurant);

                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_DisableRestaurant", connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", dto.IdRestaurant);
                command.Parameters.AddWithValue("@DeletedBy", dto.DeletedBy);

                await connection.OpenAsync();
                var affectedRows = await command.ExecuteNonQueryAsync();

                if (affectedRows > 0)
                {
                    result.IsSuccess = true;
                    result.Message = "Restaurante deshabilitado correctamente";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "No se encontró el restaurante o ya estaba deshabilitado";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al deshabilitar restaurante");
                result.IsSuccess = false;
                result.Message = $"Error al deshabilitar restaurante: {ex.Message}";
            }

            return result;
        }

        //OBTENER TODOS LOS RESTAURANTES
        public async Task<OperationResult<IEnumerable<GetRestaurantDTO>>> GetAllAsync()

        {
            var result = new OperationResult<IEnumerable<GetRestaurantDTO>>();
            var restaurants = new List<GetRestaurantDTO>();

            try
            {
                _logger!.LogInformation("Obteniendo todos los restaurantes");

                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_GetAllRestaurants", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    restaurants.Add(new (
                        reader.GetString("Name"),
                        reader.GetString("Description"),
                        reader.GetInt32("OwnerId"),
                        reader.GetInt32("IdRestaurant"),
                        reader.GetDateTime("CreatedAt")
                    ));
                }

                result.IsSuccess = true;
                result.Message = "Restaurantes obtenidos correctamente";
                result.Data = restaurants;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Error al obtener restaurantes: {ex.Message}";
                _logger!.LogError(ex, "Error en GetAllAsync");
            }

            return result;
        }


        //OBTENER RESTAURANTE POR ID
        public async Task<OperationResult<GetRestaurantDTO>> GetByIdAsync(int id)
        {
            var result = new OperationResult<GetRestaurantDTO>();

            try
            {
                _logger!.LogInformation("Obteniendo restaurante por ID: {Id}", id);

                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_GetRestaurantById", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    var restaurant = new GetRestaurantDTO(
                        reader.GetString("Name"),
                        reader.GetString("Description"),
                        reader.GetInt32("OwnerId"),
                        reader.GetInt32("IdRestaurant"),
                        reader.GetDateTime("CreatedAt")
                    );

                    result.IsSuccess = true;
                    result.Data = restaurant;
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
                result.Message = $"Error al obtener restaurante: {ex.Message}";
                _logger!.LogError(ex, "Error en GetByIdAsync");
            }

            return result;
        }


        //MODIFICAR RESTAURANTE
        public async Task<OperationResult> UpdateAsync(ModifyRestaurantDTO dto)
        {
            var result = new OperationResult();
            try
            {
                _logger.LogInformation("Modificando restaurante ID: {IdRestaurant}", dto.IdRestaurant);

                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_UpdateRestaurant", connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", dto.IdRestaurant);
                command.Parameters.AddWithValue("@Name", dto.Name);
                command.Parameters.AddWithValue("@Description", dto.Description);
                command.Parameters.AddWithValue("@UpdatedBy", dto.UpdatedBy);

                await connection.OpenAsync();
                var affectedRows = await command.ExecuteNonQueryAsync();

                if (affectedRows > 0)
                {
                    result.IsSuccess = true;
                    result.Message = "Restaurante actualizado correctamente";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "No se pudo actualizar el restaurante";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar restaurante");
                result.IsSuccess = false;
                result.Message = $"Error al actualizar restaurante: {ex.Message}";
            }

            return result;
        }

        Task<OperationResult> IRestaurantRepository.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
