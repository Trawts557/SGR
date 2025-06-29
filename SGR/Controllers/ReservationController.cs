using Microsoft.AspNetCore.Mvc;
using SGR.Application.Contracts.Repository.Reservations_and_Orders;
using SGR.Application.Dtos.Reservation_and_Orders;
using SGR.Domain.Base;
using SGR.Persistence.Repositories;

namespace SGR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ILogger<ReservationController> _logger;

        public ReservationController(IReservationRepository reservationRepository, ILogger<ReservationController> logger)
        {
            _reservationRepository = reservationRepository;
            _logger = logger;
        }

        // GET: api/Reservation
        [HttpGet]
        public async Task<ActionResult<OperationResult<IEnumerable<GetReservationDTO>>>> GetAllReservations()
        {
            var result = await _reservationRepository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Reservation/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationResult<GetReservationDTO>>> GetReservationById(int id)
        {
            var result = await _reservationRepository.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservationDTO dto)
        {
            var result = await _reservationRepository.AddAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ModifyReservationDTO dto)
        {
            var result = await _reservationRepository.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Cancel([FromBody] CancelReservationDTO dto)
        {
            var result = await _reservationRepository.CancelAsync(dto);
            return Ok(result);
        }
    }
}
