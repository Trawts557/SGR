using Microsoft.AspNetCore.Mvc;
using SGR.Web.Models;
using System.Text;
using System.Text.Json;
using SGR.Web.Services.Interfaces;

namespace SGR.Web.Controllers
{   

        public class RestaurantController : Controller
        {

        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;


        }

        // GET: RestaurantController
        public async Task<IActionResult> Index()
        {
            var restaurants = await _restaurantService.GetAllAsync();
            return View(restaurants);
        }

        // GET: RestaurantController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var restaurant = await _restaurantService.GetByIdAsync(id);

            if (restaurant == null)
            {
                ViewBag.ErrorMessage = "No se pudo encontrar el restaurante.";
            }

            return View(restaurant);
        }

        // GET: RestaurantController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RestaurantController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RestaurantModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var success = await _restaurantService.CreateAsync(model);

            if (success)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al crear el restaurante.");
            return View(model);
        }


        // GET: RestaurantController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var restaurant = await _restaurantService.GetByIdAsync(id);

            if (restaurant == null)
            {
                ViewBag.ErrorMessage = "No se encontró el restaurante.";
            }

            return View(restaurant);
        }

        // POST: RestaurantController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RestaurantEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var success = await _restaurantService.UpdateAsync(model);

            if (success)
            {
                return RedirectToAction("Index");
            }

            ViewBag.ErrorMessage = "Error al actualizar restaurante.";
            return View(model);
        }

    }

}
