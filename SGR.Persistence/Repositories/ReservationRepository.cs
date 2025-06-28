using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using SGR.Application.Contracts.Repository.Reservations_and_Orders;
using SGR.Application.Dtos.Reservation_and_Orders;
using SGR.Application.Dtos.Restaurant;
using SGR.Domain.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Persistence.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<ReservationRepository> _logger;

        public ReservationRepository(string connectionString, ILogger<ReservationRepository> logger)
        {
            _connectionString = connectionString;
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
                command.Parameters.AddWithValue("@CustomerId", dto.CustomerId);
                command.Parameters.AddWithValue("@RestaurantId", dto.RestaurantId);
                command.Parameters.AddWithValue("@ReservationDate", dto.ReservationDate);
                command.Parameters.AddWithValue("@Status", dto.Status);
                command.Parameters.AddWithValue("@CreatedAt", dto.CreatedAt);
                command.Parameters.AddWithValue("@CreatedBy", dto.CreatedBy);

                await connection.OpenAsync();
                var rowsAffected = await command.ExecuteNonQueryAsync();

                result.IsSuccess = rowsAffected > 0;
                result.Message = rowsAffected > 0 ? "Reservación creada con éxito." : "No se pudo crear la reservación.";
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
                command.Parameters.AddWithValue("@Id", dto.IdReservation);
                command.Parameters.AddWithValue("@Status", dto.Status);
                command.Parameters.AddWithValue("@UpdatedBy", dto.UpdatedBy);

                await connection.OpenAsync();
                var rowsAffected = await command.ExecuteNonQueryAsync();

                result.IsSuccess = rowsAffected > 0;
                result.Message = rowsAffected > 0 ? "Reservación actualizada." : "No se pudo actualizar la reservación.";
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
                command.Parameters.AddWithValue("@Id", dto.IdReservation);
                command.Parameters.AddWithValue("@DeletedBy", dto.DeletedBy);

                await connection.OpenAsync();
                var rowsAffected = await command.ExecuteNonQueryAsync();

                result.IsSuccess = rowsAffected > 0;
                result.Message = rowsAffected > 0 ? "Reservación cancelada correctamente." : "No se encontró la reservación.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Error al cancelar la reservación: {ex.Message}";
                _logger.LogError(ex, "Error en CancelAsync");
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
