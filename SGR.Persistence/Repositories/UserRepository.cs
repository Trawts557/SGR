using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using SGR.Application.Contracts.Repository;
using SGR.Application.Dtos.User.SGR.Application.Dtos.Users;
using SGR.Application.Dtos.Users;
using SGR.Domain.Base;
using System.Data;

namespace SGR.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(IConfiguration configuration, ILogger<UserRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException(nameof(_connectionString), "La cadena de conexión no está configurada.");
            _logger = logger;
        }

        public async Task<OperationResult<IEnumerable<GetUserDTO>>> GetAllAsync()
        {
            var result = new OperationResult<IEnumerable<GetUserDTO>>();
            var users = new List<GetUserDTO>();

            try
            {
                _logger.LogInformation("Obteniendo todos los usuarios");

                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_GetAllUsers", connection);
                command.CommandType = CommandType.StoredProcedure;

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    users.Add(new GetUserDTO(
                        reader.GetInt32("IdUser"),
                        reader.GetString("Name"),
                        reader.GetString("LastName"),
                        reader.GetString("Email"),
                        reader.GetDateTime("CreatedAt")
                    ));
                }

                result.IsSuccess = true;
                result.Message = "Usuarioss obtenidos correctamente";
                result.Data = users;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Error al obtener usuarios: {ex.Message}";
                _logger.LogError(ex, "Error en GetAllAsync");
            }

            return result;
        }

        public async Task<OperationResult> RegisterCustomerAsync(RegisterCustomerDTO dto)
        {
            var result = new OperationResult();

            try
            {
                _logger.LogInformation("Registrando cliente...");

                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_RegisterCustomer", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@p_Name", dto.Name);
                command.Parameters.AddWithValue("@p_LastName", dto.LastName);
                command.Parameters.AddWithValue("@p_Email", dto.Email);
                command.Parameters.AddWithValue("@p_Password", dto.Password);
                command.Parameters.AddWithValue("@p_CreatedAt", dto.CreatedAt);
                command.Parameters.AddWithValue("@p_CreatedBy", dto.CreatedBy);

                var pResult = new MySqlParameter("@pResult", MySqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(pResult);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                result.IsSuccess = true;
                result.Message = pResult.Value?.ToString() ?? "Cliente registrado correctamente.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Error al registrar cliente: {ex.Message}";
                _logger.LogError(ex, "Error en RegisterCustomerAsync");
            }

            return result;
        }

        public async Task<OperationResult> RegisterOwnerAsync(RegisterOwnerDTO dto)
        {
            var result = new OperationResult();

            try
            {
                _logger.LogInformation("Registrando propietario...");

                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_RegisterOwner", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@p_Name", dto.Name);
                command.Parameters.AddWithValue("@p_LastName", dto.LastName);
                command.Parameters.AddWithValue("@p_Email", dto.Email);
                command.Parameters.AddWithValue("@p_Password", dto.Password);
                command.Parameters.AddWithValue("@p_CreatedAt", dto.CreatedAt);
                command.Parameters.AddWithValue("@p_CreatedBy", dto.CreatedBy);

                var pResult = new MySqlParameter("@pResult", MySqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(pResult);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                result.IsSuccess = true;
                result.Message = pResult.Value?.ToString() ?? "Propietario registrado correctamente.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Error al registrar propietario: {ex.Message}";
                _logger.LogError(ex, "Error en RegisterOwnerAsync");
            }

            return result;
        }

        public async Task<OperationResult> RegisterAdminAsync(RegisterAdminDTO dto)
        {
            var result = new OperationResult();

            try
            {
                _logger.LogInformation("Registrando administrador...");

                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_RegisterAdmin", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@p_Name", dto.Name);
                command.Parameters.AddWithValue("@p_LastName", dto.LastName);
                command.Parameters.AddWithValue("@p_Email", dto.Email);
                command.Parameters.AddWithValue("@p_Password", dto.Password);
                command.Parameters.AddWithValue("@p_CreatedAt", dto.CreatedAt);
                command.Parameters.AddWithValue("@p_CreatedBy", dto.CreatedBy);

                var pResult = new MySqlParameter("@pResult", MySqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(pResult);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                result.IsSuccess = true;
                result.Message = pResult.Value?.ToString() ?? "Administrador registrado correctamente.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Error al registrar administrador: {ex.Message}";
                _logger.LogError(ex, "Error en RegisterAdminAsync");
            }

            return result;
        }
    }
}
