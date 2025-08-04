using Microsoft.AspNetCore.Mvc;
using SGR.Web.Models;
using SGR.Web.Services.Interfaces;

namespace SGR.Web.Controllers
{
    public class MenuCategoryController : Controller
    {
        private readonly IMenuCategoryService _menuCategoryService;

        public MenuCategoryController(IMenuCategoryService menuCategoryService)
        {
            _menuCategoryService = menuCategoryService;
        }

        // GET: MenuCategoryController
        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await _menuCategoryService.GetAllAsync();
                return View(categories);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error al cargar categorías: {ex.Message}";
                return View(new List<MenuCategoryModel>());
            }
        }

        // GET: MenuCategoryController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var category = await _menuCategoryService.GetByIdAsync(id);

                if (category == null)
                {
                    ViewBag.ErrorMessage = "Categoría no encontrada.";
                    return View();
                }

                return View(category);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error al obtener la categoría: {ex.Message}";
                return View();
            }
        }

        // GET: MenuCategoryController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MenuCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuCategoryModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var success = await _menuCategoryService.CreateAsync(model);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "No se pudo crear la categoría.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error inesperado: {ex.Message}");
            }

            return View(model);
        }

        // GET: MenuCategoryController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var model = await _menuCategoryService.GetByIdAsync(id);

                if (model == null)
                {
                    return NotFound();
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al obtener la categoría: {ex.Message}");
                return View();
            }
        }

        // POST: MenuCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MenuCategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var success = await _menuCategoryService.UpdateAsync(model);

                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "No se pudo actualizar la categoría.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error inesperado: {ex.Message}");
            }

            return View(model);
        }
    }
}
