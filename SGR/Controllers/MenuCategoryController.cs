using Microsoft.AspNetCore.Mvc;
using SGR.Application.Contracts.Repository;
using SGR.Application.Dtos.Restaurant;
using SGR.Application.Dtos.Restaurant_and_Products.MenuCategory;
using SGR.Domain.Base;


namespace SGR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuCategoryController : ControllerBase
    {
        private readonly IMenuCategoryRepository _menuCategoryRepository;
        private readonly ILogger<MenuCategoryController> _logger;

        public MenuCategoryController(IMenuCategoryRepository menuCategoryRepository, ILogger<MenuCategoryController> logger)
        {
            _menuCategoryRepository = menuCategoryRepository;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todas las categorías de menú
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<OperationResult<IEnumerable<GetMenuCategoryDTO>>>> GetAll()
        {
            var result = await _menuCategoryRepository.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Obtiene una categoría de menú por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationResult<GetMenuCategoryDTO>>> GetById(int id)
        {
            var result = await _menuCategoryRepository.GetByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Crea una nueva categoría de menú
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMenuCategoryDTO dto)
        {
            var result = await _menuCategoryRepository.AddAsync(dto);
            return Ok(result);
        }

        /// <summary>
        /// Actualiza una categoría de menú
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ModifyMenuCategoryDTO dto)
        {
            var result = await _menuCategoryRepository.UpdateAsync(dto);
            return Ok(result);
        }

        /// <summary>
        /// Elimina (desactiva) una categoría de menú
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DisableMenuCategoryDTO dto)
        {
            var result = await _menuCategoryRepository.DeleteAsync(dto);
            return Ok(result);
        }

    }
}
