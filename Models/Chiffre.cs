using Newtonsoft.Json;
using System;

namespace C_Boutique.Models
{
    public class Chiffre
    {
        [JsonProperty("date_vente")]
        public long DateVenteUnix { get; set; }

        [JsonIgnore]
        public DateTime DateVente 
        { 
            get 
            { 
                // Convert Unix timestamp to DateTime and add one day
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(DateVenteUnix);
                return dateTimeOffset.DateTime.AddDays(1);
            } 
        }

        [JsonProperty("chiffre_affaire")]
        public double? ChiffreAffaire { get; set; }

        [JsonProperty("total_depense")]
        public double? TotalDepense { get; set; }

        [JsonProperty("benefice")]
        public double? Benefice { get; set; }
    }
}
