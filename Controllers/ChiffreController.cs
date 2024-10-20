using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StationWeb.Controllers
{
    public class ChiffreController : Controller
    {
        private readonly HttpClient _httpClient;

        public ChiffreController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ViewResult> Index()
        {
            return View();
        }
    }
}