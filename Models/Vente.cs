using System.Text.Json.Serialization;

public class Vente
{
    public string id { get; set; }
    public string designation { get; set; }
    public string idMagasin { get; set; }
    
    
    public string remarque { get; set; }
    public int etat { get; set; }
    public string idOrigine { get; set; }
    public string idClient { get; set; }
    public int estPrevu { get; set; }
    public DateTime? datyPrevu { get; set; }
    [JsonPropertyName("venteDetails")]
    public List<VenteDetails> venteDetails { get; set; }
}
