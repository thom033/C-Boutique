using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;

namespace StationWeb.Controllers
{
    public class VenteController : Controller
    {   
        private readonly HttpClient _httpClient;

        public VenteController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ViewResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> InsertVente()
        {
            string apiUrl = "http://localhost:8080/station/station/vente/cree";
            var response = await _httpClient.GetStringAsync(apiUrl);
            var clients = JsonConvert.DeserializeObject<List<Client>>(response);
            return View(clients);
        }
        
        
    }
}