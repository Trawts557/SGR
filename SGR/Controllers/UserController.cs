using Microsoft.AspNetCore.Mvc;
using SGR.Application.Contracts.Repository;
using SGR.Application.Dtos.User.SGR.Application.Dtos.Users;
using SGR.Application.Dtos.Users;
using SGR.Domain.Base;

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

        [HttpGet()]
        public async Task<ActionResult<OperationResult>> GetAll()
        {
            var result = await _userRepository.GetAllAsync();
            return Ok(result);
        }

        [HttpPost("register/customer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerDTO dto)
        {
            var result = await _userRepository.RegisterCustomerAsync(dto);
            return Ok(result);
        }

        [HttpPost("register/owner")]
        public async Task<IActionResult> RegisterOwner([FromBody] RegisterOwnerDTO dto)
        {
            var result = await _userRepository.RegisterOwnerAsync(dto);
            return Ok(result);
        }

        [HttpPost("register/admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminDTO dto)
        {
            var result = await _userRepository.RegisterAdminAsync(dto);
            return Ok(result);
        }
    }
}
