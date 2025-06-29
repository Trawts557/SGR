using Microsoft.AspNetCore.Mvc;
using SGR.Application.Dtos.Restaurant;
using SGR.Application.Interfaces.Repository;
using SGR.Domain.Base;

namespace SGR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantRepository _repository;

        public RestaurantController(IRestaurantRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRestaurantDTO dto)
        {
            var result = await _repository.AddAsync(dto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ModifyRestaurantDTO dto)
        {
            var result = await _repository.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DisableRestaurantDTO dto)
        {
            var result = await _repository.DeleteAsync(dto);
            return Ok(result);
        }
    }
}
