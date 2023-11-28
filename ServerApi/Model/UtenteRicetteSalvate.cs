namespace ServerApi.Model;

public class UtenteRicettaSalvata
{
    public int UtenteId { get; set; }
    public Utente Utente { get; set; }

    public int RicettaId { get; set; }
    public Ricetta Ricetta { get; set; }
}