using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;

namespace C_Boutique.Models
{
    public class ProduitService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private const string CacheKey = "produitList";  // Clé de cache pour les produits

        // Attribut pour stocker la liste des produits
        private List<Produit> _produits = null;

        public ProduitService(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }

        // Méthode pour obtenir la liste des produits
        public async Task<List<Produit>> GetProduitsAsync()
        {
            if (_produits == null)
            {
                // Si la liste n'est pas déjà en cache, la charger depuis l'API
                _produits = await FetchProduitsFromApiAsync();
            }

            return _produits;
        }

        // Méthode pour récupérer les produits depuis l'API
        private async Task<List<Produit>> FetchProduitsFromApiAsync()
        {
            string apiUrl = "http://localhost:8080/station/station/produit/list";
            var response = await _httpClient.GetStringAsync(apiUrl);
            return JsonConvert.DeserializeObject<List<Produit>>(response);
        }

        // Méthode pour rafraîchir le cache des produits
        public void RefreshProduits()
        {
            _produits = null;  // Réinitialise la liste des produits
        }

        // Méthode pour obtenir l'ID d'un produit en fonction de son nom
        public async Task<string> GetProduitIdByNameAsync(string produitName)
        {
            // Charge la liste des produits si elle n'est pas déjà en mémoire
            if (_produits == null)
            {
                await GetProduitsAsync();
            }

            // Cherche le produit par son nom
            var produit = _produits.FirstOrDefault(p => p.Val.Equals(produitName, System.StringComparison.OrdinalIgnoreCase));

            return produit?.Id; // Retourne l'ID ou null si le produit n'est pas trouvé
        }

        // Méthode pour obtenir le nom d'un produit en fonction de son ID
        public async Task<string> GetProduitNameByIdAsync(string produitId)
        {
            // Charge la liste des produits si elle n'est pas déjà en mémoire
            if (_produits == null)
            {
                await GetProduitsAsync();
            }

            // Cherche le produit par son ID
            var produit = _produits.FirstOrDefault(p => p.Id.Equals(produitId, System.StringComparison.OrdinalIgnoreCase));

            return produit?.Val; // Retourne le nom ou null si le produit n'est pas trouvé
        }

        // Méthode pour rechercher les produits dont le nom contient la suggestion
        public async Task<List<object>> GetProduitsByNameSuggestionAsync(string suggestionNom)
        {
            var produits = await GetProduitsAsync();
            var filteredProduits = produits
                .Where(p => p.Val.Contains(suggestionNom, System.StringComparison.OrdinalIgnoreCase))  // Ignorer la casse
                .Select(p => new 
                {
                    Id = p.Id,   // Remplacez `Id` par le nom de la propriété qui correspond à l'ID dans votre modèle de produit
                    Val = p.Val,
                    Pu = p.PuVente,
                    PuAchat = p.PuAchat,
                    PuVente = p.PuVente
                })
                .ToList<object>();  // Convertir en liste d'objets

            return filteredProduits;
        }
    }
}
