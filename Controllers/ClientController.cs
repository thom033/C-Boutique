using Microsoft.AspNetCore.Mvc;
using C_Boutique.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace C_Boutique.Controllers
{
    public class ClientController : Controller
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        // Action pour afficher la liste de tous les clients dans la vue
        public async Task<IActionResult> Index()
        {
            var clients = await _clientService.GetClientsAsync();
            return View(clients);  // Passe la liste des clients à la vue Index
        }

        // Action pour afficher une vue avec la recherche de suggestions de clients par nom
        [HttpGet]
        public IActionResult ClientSuggestions()
        {
            return View();
        }

        // Action pour rechercher les clients par nom
        [HttpPost]
        public async Task<IActionResult> ClientSuggestions(string name)
        {
            var suggestions = await _clientService.GetClientsByNameSuggestionAsync(name);
            return View("ClientSuggestions", suggestions);  // Renvoie les suggestions à la vue
        }

        // Action pour afficher l'ID d'un client en fonction de son nom
        [HttpGet]
        public async Task<IActionResult> GetClientIdByName()
        {
            var name = "divers";
            var clientId = await _clientService.GetClientIdByNameAsync(name);
            return View("ClientIdResult", clientId);  // Affiche l'ID dans une vue
        }

        // Action pour afficher le nom d'un client en fonction de son ID
        [HttpGet]
        public async Task<IActionResult> GetClientNameById()
        {
            var id = "CLI000054";
            var clientName = await _clientService.GetClientNameByIdAsync(id);
            return View("ClientNameResult", clientName);  // Affiche le nom dans une vue
        }
    }
}
