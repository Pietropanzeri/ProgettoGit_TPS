using ServerApi.Model;

namespace ServerApi.ModelDTO;

public class UtenteDTO
{
    public int UtenteId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public UtenteDTO(){}
    public UtenteDTO(Utente utente)
    {
        UtenteId = utente.UtenteId;
        Username = utente.Username;
        Password = utente.Password;
    }
}