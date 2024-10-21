using Microsoft.AspNetCore.Mvc;
using C_Boutique.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace C_Boutique.Controllers
{
    public class ProduitController : Controller
    {
        private readonly ProduitService _produitService;

        public ProduitController(ProduitService produitService)
        {
            _produitService = produitService;
        }

        // Action pour afficher la liste de tous les produits
        public async Task<IActionResult> Index()
        {
            var produits = await _produitService.GetProduitsAsync();
            return View(produits);  // Passe la liste des produits à la vue Index
        }

        // Action pour rechercher les produits par nom (suggestions)
        [HttpGet]
        public IActionResult ProduitSuggestions()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProduitSuggestions(string name)
        {
            
            var suggestions = await _produitService.GetProduitsByNameSuggestionAsync(name);
            return View("ProduitSuggestions", suggestions);  // Renvoie les suggestions à la vue
        }

        // Action pour afficher l'ID d'un produit à partir de son nom
        [HttpGet]
        public async Task<IActionResult> GetProduitIdByName()
        {
            string name = "Produit A";
            var produitId = await _produitService.GetProduitIdByNameAsync(name);
            return View("ProduitIdResult", produitId);  // Affiche l'ID dans une vue
        }

        // Action pour afficher le nom d'un produit à partir de son ID
        [HttpGet]
        public async Task<IActionResult> GetProduitNameById()
        {
            string id= "P002";
            var produitName = await _produitService.GetProduitNameByIdAsync(id);
            return View("ProduitNameResult", produitName);  // Affiche le nom dans une vue
        }
    }
}
