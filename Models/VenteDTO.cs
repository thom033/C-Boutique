using System;
using System.Collections.Generic;

namespace C_Boutique.Models
{
    public class VenteDTO
    {
        public string idMagasin { get; set; }
        public string idClient { get; set; }
        public string designation { get; set; }
        public string daty { get; set; } // La date au format String pour la conversion
        public string remarque { get; set; }
        public int estPrevu { get; set; }
        public int etat { get; set; }

        // Listes pour les produits et leurs informations associées
        public List<string> produits { get; set; }      // Liste des IDs des produits
        public List<double> quantites { get; set; }     // Liste des quantités pour chaque produit
        public List<double> pu { get; set; }             // Liste des prix unitaires pour chaque produit
        public List<double> puAchat { get; set; }        // Liste des prix d'achat pour chaque produit
        public List<double> puVente { get; set; }        // Liste des prix de vente pour chaque produit

        // Constructeur par défaut
        public VenteDTO()
        {
            produits = new List<string>();
            quantites = new List<double>();
            pu = new List<double>();
            puAchat = new List<double>();
            puVente = new List<double>();
        }
    }
}
