using System;

namespace C_Boutique.Models
{
    public class Produit
    {
        public string Id { get; set; }
        public string Val { get; set; }
        public string Desce { get; set; }
        public double PuAchat { get; set; }
        public double PuVente { get; set; }
        public string IdUnite { get; set; }
        public string IdSousCategorie { get; set; }
        public string Presentation { get; set; }
        public double SeuilMin { get; set; }
        public double SeuilMax { get; set; }
        public double PuAchatUSD { get; set; }
        public double PuAchatEuro { get; set; }
        public double PuAchatAutreDevise { get; set; }
        public double PuVenteUSD { get; set; }
        public double PuVenteEuro { get; set; }
        public double PuVenteAutreDevise { get; set; }
        public int IsAchat { get; set; }
        public int IsVente { get; set; }

        // Nouveaux attributs ajoutés pour Categorie et TypeProduit
        public string IdCategorie { get; set; }
        public string ValCategorie { get; set; }
        public string IdType { get; set; }
        public string ValType { get; set; }
    }
}