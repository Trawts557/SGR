using Microsoft.Extensions.Logging;
using SGR.Application.Contracts.BusinessLogic;
using SGR.Application.Contracts.Repository;
using SGR.Application.Contracts.Repository.Reservations_and_Orders;
using SGR.Application.Dtos.Reservation_and_Orders;
using SGR.Domain.Base;

namespace SGR.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repository;
        private readonly ILogger<ReservationService> _logger;

        public ReservationService(IReservationRepository repository, ILogger<ReservationService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<OperationResult> AddAsync(CreateReservationDTO dto)
        {
            if (dto.ReservationDate < DateTime.Now)
            {
                return OperationResult.Failure("No puedes reservar en una fecha pasada.");
            }

            bool overlap = await _repository.HasOverlappingReservationAsync(dto.RestaurantId, dto.ReservationDate);

            if (overlap)
            {
                return OperationResult.Failure("Ya existe una reserva en ese horario para este restaurante.");
            }

            return await _repository.AddAsync(dto);
        }


        public async Task<OperationResult> CancelAsync(CancelReservationDTO dto)
        {
            if (dto.IdReservation <= 0)
                return OperationResult.Failure("ID de reservación inválido.");

            return await _repository.CancelAsync(dto);
        }

        public async Task<OperationResult<IEnumerable<GetReservationDTO>>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<OperationResult<GetReservationDTO>> GetByIdAsync(int id)
        {
            if (id <= 0)
                return OperationResult<GetReservationDTO>.Failure("ID inválido.");

            return await _repository.GetByIdAsync(id);
        }
    }
}
