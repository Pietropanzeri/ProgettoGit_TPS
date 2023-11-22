namespace ServerApi.Model;

public class Utente
{
    public int UtenteId { get; set; }
    public string Username { get; set; }
    public int FotoId { get; set; }
    public string Password { get; set; }
    public ICollection<Ricetta> Ricetta { get; set;} = new HashSet<Ricetta>();
    public ICollection<UtenteRicettaSalvata> UtenteRicetteSalvate { get; set; } = null!;
    public ICollection<UtenteUtenteSeguito> UtenteUtentiSeguiti { get; set; } = null!;
    public ICollection<UtenteUtenteSeguito> UtenteUtentiChetiSeguono { get; set; } = null!;
    public FotoUtente FotoUtente { get; set; }

}
