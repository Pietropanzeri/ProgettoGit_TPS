namespace ServerApi.Model;

public class UtenteUtenteSeguito
{
    public int UtenteId { get; set; }
    public Utente Utente { get; set; }

    public int UtenteSeguitoId { get; set; }
    public Utente UtenteSeguito { get; set; }
    
}
