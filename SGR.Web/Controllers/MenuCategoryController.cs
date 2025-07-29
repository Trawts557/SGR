using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGR.Web.Models;
using System.Text;
using System.Text.Json;

namespace SGR.Web.Controllers
{
    public class MenuCategoryController : Controller
    {

        // GET: MenuCategoryController
        public async Task<IActionResult> Index()
        {
            GetAllMenuCategoryResponse? getAllMenuCategoryResponse = null;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7106/api/");
                    var response = await client.GetAsync("MenuCategory/GetAllMenuCategory");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();

                        getAllMenuCategoryResponse = JsonSerializer.Deserialize<GetAllMenuCategoryResponse>(responseString, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                    }
                    else
                    {
                        getAllMenuCategoryResponse = new GetAllMenuCategoryResponse
                        {
                            isSuccess = false,
                            message = $"Error al obtener categorías. Código de estado: {response.StatusCode}"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getAllMenuCategoryResponse = new GetAllMenuCategoryResponse
                {
                    isSuccess = false,
                    message = "Error al cargar categorías: " + ex.Message
                };
            }

            List<MenuCategoryModel> categoriesToView = new List<MenuCategoryModel>();
            if (getAllMenuCategoryResponse != null && getAllMenuCategoryResponse.isSuccess && getAllMenuCategoryResponse.data != null)
            {
                categoriesToView = getAllMenuCategoryResponse.data;
            }
            else
            {
                ViewBag.ErrorMessage = getAllMenuCategoryResponse?.message ?? "No se pudieron cargar las categorías.";
            }

            return View(categoriesToView);
        }


        // GET: MenuCategoryController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            MenuCategoryModel? category = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7106/api/");
                    var response = await client.GetAsync($"MenuCategory/GetMenuCategoryById?id={id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        var result = JsonSerializer.Deserialize<GetMenuCategoryResponse>(responseString, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (result != null && result.isSuccess && result.data != null)
                        {
                            category = result.data;
                        }
                        else
                        {
                            ViewBag.ErrorMessage = result?.message ?? "No se pudo obtener la categoria de menu.";
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = $"Error al obtener categoria de menu. Código: {response.StatusCode}";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al cargar la categoria de menu: " + ex.Message;
            }

            return View(category);
        }

        // GET: MenuCategoryController/Create
        public ActionResult Create()
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
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7106/api/");
                    var json = JsonSerializer.Serialize(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("MenuCategory/CreateMenuCategory", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }

                    var responseString = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Error al crear la categoria de menu: {responseString}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error inesperado: {ex.Message}");
            }

            return View(model);
        }

        // GET: MenuCategoryController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            MenuCategoryModel model = new();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7106/api/");
                    var response = await client.GetAsync($"MenuCategory/GetMenuCategoryById?id={id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        var result = JsonSerializer.Deserialize<GetMenuCategoryResponse>(responseString, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (result != null && result.data != null)
                        {
                            model = result.data;
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al obtener la categoría: {ex.Message}");
                return View(model);
            }

            return View(model);
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
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7106/api/");
                    var json = JsonSerializer.Serialize(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync("MenuCategory/ModifyMenuCategory", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }

                    var responseString = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Error al modificar la categoría: {responseString}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error inesperado: {ex.Message}");
            }

            return View(model);
        }

        // GET: MenuCategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MenuCategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }




    }
}
