using C_Boutique.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using C_Boutique.Models;

namespace C_Boutique.Controllers
{
    public class ChiffreController : Controller
    {
        private readonly HttpClient _httpClient;

        public ChiffreController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new List<Chiffre>());
        }

        [HttpPost]
        public async Task<IActionResult> Index(DateTime dateDebut, DateTime dateFin)
        {
            string url = $"http://localhost:8080/station/station/chiffre/getChiffres?dateDebut={dateDebut.ToString("yyyy-MM-dd")}&dateFin={dateFin.ToString("yyyy-MM-dd")}";

            try
            {
                // Send a GET request to the Java web service
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response JSON into a list of Chiffre objects
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var chiffres = JsonConvert.DeserializeObject<List<Chiffre>>(jsonResponse);
                    Console.WriteLine(jsonResponse);
                    Console.WriteLine(JsonConvert.SerializeObject(chiffres));

                    // Pass the list of chiffres to the view for display
                    return View(chiffres);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    ViewBag.Message = "Aucun chiffre trouvé entre les dates spécifiées.";
                }
                else
                {
                    ViewBag.Message = "Erreur lors de la récupération des chiffres d'affaires.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Erreur: {ex.Message}";
            }

            // If there is an error, return the view with an empty list
            return View(new List<Chiffre>());
        }
    }
}
