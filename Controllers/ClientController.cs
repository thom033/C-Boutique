using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

public class ClientController : Controller
{
    private readonly HttpClient _httpClient;

    public ClientController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Action pour afficher la liste des clients
    public async Task<IActionResult> Index()
    {
        string apiUrl = "http://localhost:8080/station/station/client/list";
        var response = await _httpClient.GetStringAsync(apiUrl);
        var clients = JsonConvert.DeserializeObject<List<Client>>(response);
        return View(clients);
    }
}
