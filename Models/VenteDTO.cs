using System;
using System.Collections.Generic;

namespace C_Boutique.Models
{
    public class VenteDTO
    {
        public string IdMagasin { get; set; }
        public string IdClient { get; set; }
        public string Designation { get; set; }
        public string Daty { get; set; } // La date au format String pour faciliter la conversion
        public string Remarque { get; set; }
        public int EstPrevu { get; set; }
        public int Etat { get; set; }

        // Listes pour les produits et leurs informations associées
        public List<string> Produits { get; set; }      // Liste des IDs des produits
        public List<double> Quantites { get; set; }     // Liste des quantités pour chaque produit
        public List<double> Pu { get; set; }            // Liste des prix unitaires pour chaque produit
        public List<double> PuAchat { get; set; }       // Liste des prix d'achat pour chaque produit
        public List<double> PuVente { get; set; }       // Liste des prix de vente pour chaque produit
    }
}