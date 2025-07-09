using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using SGR.Application.Contracts.Repository.Reservations_and_Orders;
using SGR.Application.Dtos.Reservation_and_Orders;
using SGR.Domain.Base;
using System.Data;

namespace SGR.Persistence.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<ReservationRepository> _logger;

        public ReservationRepository(IConfiguration configuration, ILogger<ReservationRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException(nameof(_connectionString), "La cadena de conexión no está configurada.");
            _logger = logger;
        }

        public async Task<OperationResult> AddAsync(CreateReservationDTO dto)
        {
            var result = new OperationResult();
            try
            {
                _logger.LogInformation("Creando nueva reservación...");

                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_CreateReservation", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@p_CustomerId", dto.CustomerId);
                command.Parameters.AddWithValue("@p_RestaurantId", dto.RestaurantId);
                command.Parameters.AddWithValue("@p_ReservationDate", dto.ReservationDate);
                command.Parameters.AddWithValue("@p_Status", dto.Status);
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
                result.Message = pResult.Value?.ToString() ?? "Reservación creada correctamente.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Error al crear la reservación: {ex.Message}";
                _logger.LogError(ex, "Error en AddAsync");
            }

            return result;
        }

        public async Task<OperationResult> UpdateAsync(ModifyReservationDTO dto)
        {
            var result = new OperationResult();
            try
            {
                _logger.LogInformation("Actualizando reservación ID: {Id}", dto.IdReservation);

                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_UpdateReservation", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@p_IdReservation", dto.IdReservation);
                command.Parameters.AddWithValue("@p_Status", dto.Status);
                command.Parameters.AddWithValue("@p_UpdatedBy", dto.UpdatedBy);

                var pResult = new MySqlParameter("@pResult", MySqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(pResult);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                result.IsSuccess = true;
                result.Message = pResult.Value?.ToString() ?? "Reservación actualizada.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Error al actualizar la reservación: {ex.Message}";
                _logger.LogError(ex, "Error en UpdateAsync");
            }

            return result;
        }

        public async Task<OperationResult> CancelAsync(CancelReservationDTO dto)
        {
            var result = new OperationResult();
            try
            {
                _logger.LogInformation("Cancelando reservación ID: {Id}", dto.IdReservation);

                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_CancelReservation", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@p_IdReservation", dto.IdReservation);
                command.Parameters.AddWithValue("@p_DeletedBy", dto.DeletedBy);

                var pResult = new MySqlParameter("@pResult", MySqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(pResult);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                result.IsSuccess = true;
                result.Message = pResult.Value?.ToString() ?? "Reservación cancelada correctamente.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Error al cancelar la reservación: {ex.Message}";
                _logger.LogError(ex, "Error en CancelAsync");
            }

            return result;
        }

        public async Task<OperationResult<IEnumerable<GetReservationDTO>>> GetAllAsync()
        {
            var result = new OperationResult<IEnumerable<GetReservationDTO>>();
            var reservations = new List<GetReservationDTO>();

            try
            {
                _logger.LogInformation("Obteniendo todas las reservaciones...");

                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_GetAllReservations", connection);
                command.CommandType = CommandType.StoredProcedure;

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    reservations.Add(new GetReservationDTO(
                        reader.GetInt32("IdReservation"),
                        reader.GetInt32("CustomerId"),
                        reader.GetInt32("RestaurantId"),
                        reader.GetDateTime("ReservationDate"),
                        reader.GetString("Status"),
                        reader.GetDateTime("CreatedAt")
                    ));
                }

                result.IsSuccess = true;
                result.Message = "Reservaciones obtenidas correctamente.";
                result.Data = reservations;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Error al obtener las reservaciones: {ex.Message}";
                _logger.LogError(ex, "Error en GetAllAsync");
            }

            return result;
        }

        public async Task<OperationResult<GetReservationDTO>> GetByIdAsync(int id)
        {
            var result = new OperationResult<GetReservationDTO>();

            try
            {
                _logger.LogInformation("Obteniendo reservación por ID: {Id}", id);

                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_GetReservationById", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@p_IdReservation", id);

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    var reservation = new GetReservationDTO(
                        reader.GetInt32("IdReservation"),
                        reader.GetInt32("CustomerId"),
                        reader.GetInt32("RestaurantId"),
                        reader.GetDateTime("ReservationDate"),
                        reader.GetString("Status"),
                        reader.GetDateTime("CreatedAt")
                    );

                    result.IsSuccess = true;
                    result.Data = reservation;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Reservación no encontrada.";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Error al obtener la reservación: {ex.Message}";
                _logger.LogError(ex, "Error en GetByIdAsync");
            }

            return result;
        }

        public async Task<bool> HasOverlappingReservationAsync(int restaurantId, DateTime reservationDate)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                using var command = new MySqlCommand("sp_CheckReservationOverlap", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@p_RestaurantId", restaurantId);
                command.Parameters.AddWithValue("@p_ReservationDate", reservationDate);

                await connection.OpenAsync();
                var result = await command.ExecuteScalarAsync();

                return Convert.ToInt32(result) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en HasOverlappingReservationAsync");
                return true;
            }
        }


    }
}
