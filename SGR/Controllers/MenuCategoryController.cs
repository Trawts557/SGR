using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using SGR.Application.Contracts.BusinessLogic;
using SGR.Application.Dtos.Restaurant;
using SGR.Application.Dtos.Restaurant_and_Products.MenuCategory;
using SGR.Domain.Base;

namespace SGR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuCategoryController : ControllerBase
    {
        private readonly IMenuCategoryService _menuCategoryService;
        private readonly ILogger<MenuCategoryController> _logger;

        public MenuCategoryController(IMenuCategoryService menuCategoryService, ILogger<MenuCategoryController> logger)
        {
            _menuCategoryService = menuCategoryService;
            _logger = logger;
        }

        [HttpGet("GetAllMenuCategory")]
        public async Task<ActionResult<OperationResult<IEnumerable<GetMenuCategoryDTO>>>> GetAll()
        {
            var result = await _menuCategoryService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("GetMenuCategoryById")]
        public async Task<ActionResult<OperationResult<GetMenuCategoryDTO>>> GetById(int id)
        {
            var result = await _menuCategoryService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("CreateMenuCategory")]
        public async Task<IActionResult> Create([FromBody] CreateMenuCategoryDTO dto)
        {
            var result = await _menuCategoryService.AddAsync(dto);
            return Ok(result);
        }

        [HttpPut("ModifyMenuCategory")]
        public async Task<IActionResult> Update([FromBody] ModifyMenuCategoryDTO dto)
        {
            var result = await _menuCategoryService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("DisableMenuCategory")]
        public async Task<IActionResult> Delete([FromBody] DisableMenuCategoryDTO dto)
        {
            var result = await _menuCategoryService.DeleteAsync(dto);
            return Ok(result);
        }

    }
}
