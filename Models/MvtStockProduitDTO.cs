using System;

namespace C_Boutique.Models
{
    public class MvtStockProduitDTO
    {
        public string IdProduit { get; set; }
        public double Entree { get; set; }
        public double Sortie { get; set; }
        public double PuVente { get; set; }
        public double PuAchat { get; set; }

        // Constructor (Optional: if you want to create an instance with initial values)
        public MvtStockProduitDTO(string idProduit, double entree, double sortie, double puVente, double puAchat)
        {
            IdProduit = idProduit;
            Entree = entree;
            Sortie = sortie;
            PuVente = puVente;
            PuAchat = puAchat;
        }

        // ToString method for easy display or debugging
        public override string ToString()
        {
            return $"IdProduit: {IdProduit}, Entree: {Entree}, Sortie: {Sortie}, PuVente: {PuVente}, PuAchat: {PuAchat}";
        }
    }
}
