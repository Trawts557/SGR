using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGR.Domain.Entities.Restaurants_and_Products;
using SGR.Web.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace SGR.Web.Controllers
{
        public class RestaurantController : Controller

    {
        // GET: RestaurantController
        public async Task<IActionResult> Index()
        {
            GetAllRestaurantsResponse? getAllRestaurantsResponse = null;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7106/api/");
                    var response = await client.GetAsync("Restaurant/GetAllRestaurants");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();

                        getAllRestaurantsResponse = JsonSerializer.Deserialize<GetAllRestaurantsResponse>(responseString, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                    }
                    else
                    {
                        getAllRestaurantsResponse = new GetAllRestaurantsResponse
                        {
                            isSuccess = false,
                            message = $"Error al obtener restaurantes. Código de estado: {response.StatusCode}"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getAllRestaurantsResponse = new GetAllRestaurantsResponse
                {
                    isSuccess = false,
                    message = "Error al cargar restaurantes: " + ex.Message
                };
            }

            List<RestaurantModel> restaurantsToView = new List<RestaurantModel>();
            if (getAllRestaurantsResponse != null && getAllRestaurantsResponse.isSuccess && getAllRestaurantsResponse.data != null)
            {
                restaurantsToView = getAllRestaurantsResponse.data;
            }
            else
            {
                ViewBag.ErrorMessage = getAllRestaurantsResponse?.message ?? "No se pudieron cargar los restaurantes.";
            }

            return View(restaurantsToView);
        }

        // GET: RestaurantController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            RestaurantModel? restaurant = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7106/api/");
                    var response = await client.GetAsync($"Restaurant/GetRestaurantById?id={id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        var result = JsonSerializer.Deserialize<GetRestaurantByIdResponse>(responseString, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (result != null && result.isSuccess && result.data != null)
                        {
                            restaurant = result.data;
                        }
                        else
                        {
                            ViewBag.ErrorMessage = result?.message ?? "No se pudo obtener el restaurante.";
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = $"Error al obtener restaurante. Código: {response.StatusCode}";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al cargar restaurante: " + ex.Message;
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

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7106/api/");
                    var json = JsonSerializer.Serialize(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("Restaurant/CreateRestaurant", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }

                    var responseString = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Error al crear el restaurante: {responseString}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error inesperado: {ex.Message}");
            }

            return View(model);
        }

        // GET: RestaurantController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            RestaurantModel? restaurant = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7106/api/");
                    var response = await client.GetAsync($"Restaurant/GetRestaurantById?id={id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        var result = JsonSerializer.Deserialize<GetRestaurantByIdResponse>(responseString, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (result != null && result.isSuccess && result.data != null)
                        {
                            restaurant = result.data;
                        }
                        else
                        {
                            ViewBag.ErrorMessage = result?.message ?? "No se pudo obtener el restaurante.";
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = $"Error al obtener restaurante. Código: {response.StatusCode}";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al cargar restaurante: " + ex.Message;
            }

            return View(restaurant);
        }


        // POST: RestaurantController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit(RestaurantEditModel model)
        {
            RestaurantEditResponse? editResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7106/api/");
                    var response = await client.PutAsJsonAsync("Restaurant/ModifyRestaurant", model);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        var result = JsonSerializer.Deserialize<RestaurantEditResponse>(responseString, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                    }
                    else
                    {
                        ViewBag.ErrorMessage = $"Error al EDITAR restaurante. Código: {response.StatusCode}";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al actualizar restaurante: " + ex.Message;
            }

            return View(editResponse);
        }
    }
    
}
