using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using C_Boutique.Models;

namespace C_Boutique.Controllers
{
    public class StockController : Controller
    {
        private readonly HttpClient _httpClient;

        // Constructor to inject HttpClient
        public StockController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<IActionResult> Index(DateTime? dateDebut, DateTime? dateFin)
        {
            if (!dateDebut.HasValue || !dateFin.HasValue)
            {
                // If no dates provided, return empty result or show an error
                ViewBag.Message = "Please provide both start and end dates.";
                return View(new List<MvtStockProduitDTO>());
            }

            string apiUrl = $"http://localhost:8080/station/station/mvtstock/entre-dates?dateDebut={dateDebut?.ToString("yyyy-MM-dd")}&dateFin={dateFin?.ToString("yyyy-MM-dd")}";

            try
            {
                var response = await _httpClient.GetStringAsync(apiUrl);
                var stockList = JsonConvert.DeserializeObject<List<MvtStockProduitDTO>>(response);

                return View(stockList);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error fetching stock data: " + ex.Message;
                return View(new List<MvtStockProduitDTO>());
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new List<C_Boutique.Models.MvtStockProduitDTO>());
        }
    }
}
