namespace Client.Model;

public class Utente
{
    public int UtenteId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int FotoId{ get; set; }

    public Utente(){ }
}