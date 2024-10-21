using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;

namespace C_Boutique.Models {
    public class ClientService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private const string CacheKey = "clientList";

    // Attribut pour stocker la liste des clients
    private List<Client> _clients = null;

    public ClientService(HttpClient httpClient, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _cache = cache;
    }

    // Méthode pour obtenir la liste des clients
    public async Task<List<Client>> GetClientsAsync()
    {
        if (_clients == null)
        {
            // Si la liste n'est pas déjà en cache, la charger depuis l'API
            _clients = await FetchClientsFromApiAsync();
        }
        
        return _clients;
    }

    // Méthode pour récupérer les clients depuis l'API
    private async Task<List<Client>> FetchClientsFromApiAsync()
    {
        string apiUrl = "http://localhost:8080/station/station/client/list";
        var response = await _httpClient.GetStringAsync(apiUrl);
        return JsonConvert.DeserializeObject<List<Client>>(response);
    }

    // Méthode pour rafraîchir le cache des clients
    public void RefreshClients()
    {
        _clients = null;  // Réinitialise la liste des clients
    }

    // Méthode pour obtenir l'ID d'un client en fonction de son nom
    public async Task<string> GetClientIdByNameAsync(string clientName)
    {
        // Charge la liste des clients si elle n'est pas déjà en mémoire
        if (_clients == null)
        {
            await GetClientsAsync();
        }

        // Cherche le client par son nom
        var client = _clients.FirstOrDefault(c => c.Nom.Equals(clientName, System.StringComparison.OrdinalIgnoreCase));

        return client?.Id; // Retourne l'ID ou null si le client n'est pas trouvé
    }

    // Méthode pour obtenir le nom d'un client en fonction de son ID
    public async Task<string> GetClientNameByIdAsync(string clientId)
    {
        // Charge la liste des clients si elle n'est pas déjà en mémoire
        if (_clients == null)
        {
            await GetClientsAsync();
        }

        // Cherche le client par son ID
        var client = _clients.FirstOrDefault(c => c.Id.Equals(clientId, System.StringComparison.OrdinalIgnoreCase));

        return client?.Nom; // Retourne le nom ou null si le client n'est pas trouvé
    }

    // Méthode pour rechercher les clients dont le nom contient la suggestion
    public async Task<List<object>> GetClientsByNameSuggestionAsync(string suggestionNom)
    {
        var clients = await GetClientsAsync();
        var filteredClients = clients
            .Where(c => c.Nom.Contains(suggestionNom, System.StringComparison.OrdinalIgnoreCase))  // Ignorer la casse
            .Select(c => new 
            {
                Id = c.Id,   // Remplacez `Id` par le nom de la propriété qui correspond à l'ID dans votre modèle de client
                Nom = c.Nom
            })
            .ToList<object>();  // Convertir en liste d'objets

        return filteredClients;
    }
}

}
