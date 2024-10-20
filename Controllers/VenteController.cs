using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using C_Boutique.Models;
using System;

namespace C_Boutique.Controllers
{
    public class VenteController : Controller
    {   
        private readonly HttpClient _httpClient;
        private readonly ClientService _clientService;
        private readonly ProduitService _produitService;
    
        public VenteController(HttpClient httpClient, ClientService clientService, ProduitService produitService)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromSeconds(60);

            _clientService = clientService;
            _produitService = produitService;
        }
        public IActionResult Index()
        {
            // Initialisation correcte des objets, comme Model ou ViewData
            var model = new VenteDTO();
            model.produits = new List<string>();
            model.quantites = new List<double>();
            model.pu = new List<double>();
            model.puAchat = new List<double>();
            model.puVente = new List<double>();
            return View(model);
        }

        [Route("Vente/GetClientsByNameSuggestionAsync")]
        [HttpGet]
        public async Task<IActionResult> GetClientsByNameSuggestionAsync(string name)
        {
            var clientSuggestions = await _clientService.GetClientsByNameSuggestionAsync(name);
            return Json(clientSuggestions);
        }

        [Route("Vente/GetProduitsByNameSuggestionAsync")]
        [HttpGet]
        public async Task<IActionResult> GetProduitsByNameSuggestionAsync(string name)
        {
            var productSuggestions = await _produitService.GetProduitsByNameSuggestionAsync(name);
            return Json(productSuggestions);
        }

        
        // public async Task<IActionResult> InsertVente()
        // {
        //     string apiUrl = "http://localhost:8080/station/station/vente/cree";
        //     var response = await _httpClient.GetStringAsync(apiUrl);
        //     var clients = JsonConvert.DeserializeObject<List<Client>>(response);
        //     return View(clients);
        // }

        // [HttpPost]
        // [Route("Vente/InsertVente")]
        // public async Task<IActionResult> InsertVente(VenteDTO vente)
        // {
        //     string apiUrl = "http://localhost:8080/station/station/vente/creer";
        //     var jsonContent = JsonConvert.SerializeObject(vente);
        //     var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        //     var response = await _httpClient.PostAsync(apiUrl, content);

        //     if (response.IsSuccessStatusCode)
        //     {
        //         var jsonResponse = await response.Content.ReadAsStringAsync();
        //         var venteCreee = JsonConvert.DeserializeObject<Vente>(jsonResponse);

        //         return RedirectToAction("AfficherVente", new { vente = venteCreee });
        //     }
        //     else
        //     {
        //         ViewBag.ErrorMessage = "Erreur lors de la création de la vente";
        //         return View("Error");
        //     }
        // }

        [HttpPost]
[HttpPost]
public async Task<IActionResult> InsertVente(string Designation, string Daty, string Remarque, string IdClient, 
    List<string> Produits, List<double> Quantites, List<double> Pu, List<double> PuAchat, List<double> PuVente)
{
    // Initialisation de l'objet VenteDTO
    var vente = new VenteDTO
    {
        idMagasin = "PHARM001",
        designation = Designation,
        daty = DateTime.Parse(Daty).ToString("yyyy-MM-dd"),
        remarque = Remarque,
        idClient = IdClient,
        produits = new List<string>(),
        quantites = new List<double>(),
        pu = new List<double>(),
        puAchat = new List<double>(),
        puVente = new List<double>(),
    };

    // Remplissage des produits et quantités
    for (int i = 0; i < Produits.Count; i++)
    {
        vente.produits.Add(Produits[i]);
        vente.quantites.Add(Quantites[i]);
        vente.pu.Add(Pu[i]);
        vente.puAchat.Add(PuAchat[i]);
        vente.puVente.Add(PuVente[i]);
    }

    Console.WriteLine(JsonConvert.SerializeObject(vente));

    string apiUrl = "http://localhost:8080/station/station/vente/creer";
    var jsonContent = JsonConvert.SerializeObject(vente);
    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

    try
    {
        var response = await _httpClient.PostAsync(apiUrl, content);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonResponse);

            // Désérialisation de la réponse en objet Vente
            var venteCreee = JsonConvert.DeserializeObject<Vente>(jsonResponse);
            Console.WriteLine(JsonConvert.SerializeObject(venteCreee));

            // Stocker l'objet Vente et VenteDetails dans TempData
            TempData["VenteCreee"] = JsonConvert.SerializeObject(venteCreee);

            // Rediriger vers l'action "AfficherVente"
            TempData["SuccessMessage"] = "Vente créée avec succès";
            return RedirectToAction("AfficherVente");
        }
        else
        {
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
    catch (TaskCanceledException ex)
    {
        return StatusCode(408, "La requête a pris trop de temps à répondre.");
    }
    catch (Exception ex)
    {
        return StatusCode(500, "Une erreur est survenue : " + ex.Message);
    }
}




        



        [HttpGet]
        public IActionResult AfficherVente()
        {
            // Récupérer l'objet Vente depuis TempData
            var venteJson = TempData["VenteCreee"] as string;
            
            if (venteJson != null)
            {
                var vente = JsonConvert.DeserializeObject<Vente>(venteJson);
                return View(vente);
            }

            // Si aucune vente n'est trouvée dans TempData, afficher une vue vide ou un message d'erreur
            return View();
        }


        
        
    }
}