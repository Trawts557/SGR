using Microsoft.AspNetCore.Mvc;
using SGR.Application.Dtos.Restaurant;
using SGR.Application.Contracts.BusinessLogic;
using SGR.Domain.Base;

namespace SGR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRestaurantDTO dto)
        {
            var result = await _restaurantService.AddAsync(dto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _restaurantService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _restaurantService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ModifyRestaurantDTO dto)
        {
            var result = await _restaurantService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DisableRestaurantDTO dto)
        {
            var result = await _restaurantService.DeleteAsync(dto);
            return Ok(result);
        }
    }
}
