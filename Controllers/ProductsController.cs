using Microsoft.AspNetCore.Mvc;
using StationBoutique.Models;
using System.Collections.Generic;

namespace StationBoutique.Controllers
{
    public class ProductsController : Controller
    {
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 1000 },
            new Product { Id = 2, Name = "Phone", Price = 500 }
        }; 

        // Afficher la liste des produits
        public IActionResult Index()
        {
            return View(products);
        }

        // Formulaire pour ajouter un produit
        public IActionResult Create()
        {
            return View();
        }

        // Ajouter un nouveau produit (POST)
        [HttpPost]
        public IActionResult Create(Product product)
        {
            product.Id = products.Count + 1;
            products.Add(product);
            return RedirectToAction("Index");
        }
    }
}
