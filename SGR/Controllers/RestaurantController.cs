using Microsoft.AspNetCore.Mvc;
using SGR.Application.Dtos.Restaurant;
using SGR.Application.Interfaces.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SGR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantController(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        // GET: api/<RestaurantController>
        [HttpGet("GetRestaurant")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _restaurantRepository.GetAllAsync();

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Data); // Aquí devuelves la lista de restaurantes
        }

        // GET api/<RestaurantController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RestaurantController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRestaurantDTO restaurantDto)
        {
            var result = await _restaurantRepository.AddAsync(restaurantDto);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok("Restaurante creado exitosamente.");
        }


        // PUT api/<RestaurantController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RestaurantController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
