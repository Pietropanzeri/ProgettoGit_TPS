namespace ServerApi.Model;
public class Ricetta
{
    public int RicettaId { get; set; }
    public int UtenteId { get; set; }
    public string Nome { get; set; }
    public string Preparazione { get; set; }
    public int? Tempo { get; set; }
    public int? Difficolta { get; set; }
    public int Piatto { get; set; }
    public Utente Utente { get; set; }
    public ICollection<Foto> Fotos { get; set; } = new HashSet<Foto>();
    public ICollection<RicettaIngrediente> RicettaIngredienti { get; set; } = null!;
    public ICollection<UtenteRicettaSalvata> UtenteRicetteSalvate { get; set; } = null!;
}
