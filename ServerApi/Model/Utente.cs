namespace ServerApi.Model;

public class Utente
{
    public int UtenteId { get; set; }
    public string Username { get; set; }
    public ICollection<Ricetta> Ricetta { get; set;} = new HashSet<Ricetta>();
    public ICollection<UtenteRicettaSalvata> UtenteRicetteSalvate { get; set; } = null!;
    public ICollection<UtenteUtenteSeguito> UtenteUtentiSeguiti { get; set; } = null!;
    public ICollection<UtenteUtenteSeguito> UtenteUtentiChetiSeguono { get; set; } = null!;
}
