namespace ServerApi.Model;

public class FotoUtente
{
    public int FotoId { get; set; } 
    public byte[] FotoData { get; set; }

    public int? UtenteId { get; set; }
    public Utente Utente { get; set; }
}