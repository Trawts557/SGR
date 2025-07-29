using Microsoft.AspNetCore.Mvc;
using SGR.Application.Contracts.Repository;
using SGR.Application.Dtos.Users;

namespace SGR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpPost("registerOwner")]
        public async Task<IActionResult> RegisterOwner([FromBody] RegisterOwnerDTO dto)
        {
            var result = await _userRepository.RegisterOwnerAsync(dto);
            return Ok(result);
        }

        [HttpPost("registerAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminDTO dto)
        {
            var result = await _userRepository.RegisterAdminAsync(dto);
            return Ok(result);
        }

        /*
        [HttpGet("GetAll")]
        public async Task<ActionResult<OperationResult>> GetAll()
        {
            var result = await _userRepository.GetAllAsync();
            return Ok(result);
        }

        [HttpPost("registerCustomer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerDTO dto)
        {
            var result = await _userRepository.RegisterCustomerAsync(dto);
            return Ok(result);
        }
        */

    }
}
